using System;
using System.Data;
using System.Drawing;
using Infragistics.Excel;
using System.Web;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Summary description for ExcelHelper.
    /// </summary>
    public class ExcelHelper
	{

		/// <summary>
		/// This helper function will take an Excel Workbook 
		/// and write it to the response stream. In this fashion, 
		/// the end user will be prompted to download or open the
		/// resulting Excel file. 
		/// </summary>
		/// <param name="theWorkBook"></param>
		/// <param name="FileName"></param>
		/// <param name="resp"></param>
		public static void WriteToResponse(Workbook theWorkBook, string FileName, HttpResponse  resp)
		{
			//Create the Stream class
			System.IO.MemoryStream theStream = new System.IO.MemoryStream();

			//Write the in memory Workbook object to the Stream
			BIFF8Writer.WriteWorkbookToStream(theWorkBook, theStream);

			//Create a Byte Array to contain the stream
			byte[] byteArr = (byte[])Array.CreateInstance(typeof(byte), theStream.Length);

			theStream.Position = 0;
			theStream.Read(byteArr, 0, (int)theStream.Length);
			theStream.Close();

			resp.Clear();

			//resp.ContentType = "application/docx; charset=utf-8";
			resp.ContentType = "application/vnd.ms-excel";
			resp.ContentType = "application/xls";
			resp.AddHeader("content-disposition", "attachment; filename=" + FileName);
			//resp.AddHeader("content-disposition", "Inline; filename=" + FileName);

			resp.BinaryWrite(byteArr);

			HttpContext.Current.ApplicationInstance.CompleteRequest();
			//resp.End();
			resp.Flush();//tambien se puede usar esto.

		}

		/// <summary>
		/// This will save the WorkBook to a disk file
		/// </summary>
		/// <param name="theWorkBook"></param>
		/// <param name="theFile"></param>
		public static void WriteToDisk(Workbook theWorkBook, string theFile)
		{
			BIFF8Writer.WriteWorkbookToFile(theWorkBook, theFile);
		}

		public static Workbook DataSetToExcel(DataSet ds)
		{
			Workbook MyBook = new Workbook();
			Worksheet MySheet;
			int iRow = 0;
			int iCell = 0;
			int sizeColum=0;

			foreach (DataTable t in ds.Tables)
			{
				MySheet = MyBook.Worksheets.Add(t.TableName);
				iCell = 0;

				foreach (DataColumn col in t.Columns)
				{
					iRow = 0;
					MySheet.Rows[iRow].Cells[iCell].Value = col.ColumnName.ToUpper();

					MySheet.Rows[iRow].Cells[iCell].CellFormat.FillPatternForegroundColor = ColorTranslator.FromHtml("#AAD3F2");
					MySheet.Rows[iRow].Cells[iCell].CellFormat.TopBorderColor =ColorTranslator.FromHtml("#808080");
					//MySheet.Rows[iRow].Cells[iCell].CellFormat.BottomBorderColor =ColorTranslator.FromHtml("#808080"); 
					MySheet.Rows[iRow].Cells[iCell].CellFormat.RightBorderColor =ColorTranslator.FromHtml("#808080");
					//MySheet.Rows[iRow].Cells[iCell].CellFormat.LeftBorderColor =ColorTranslator.FromHtml("#808080"); 
					MySheet.Rows[iRow].Cells[iCell].CellFormat.Font.Color= ColorTranslator.FromHtml("#000093");
					MySheet.Rows[iRow].Cells[iCell].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
					MySheet.Rows[iRow].CellFormat.Alignment = Infragistics.Excel.HorizontalCellAlignment.Center;
					//MySheet.Rows[iRow].CellFormat.Font.Name= "Arial";
					//MySheet.Rows[iRow].CellFormat.Font.Height = 160;

					sizeColum =MySheet.Rows[iRow].Cells[iCell].Value.ToString().Trim().Length;
					iRow += 1;
					MySheet.Columns[iCell].CellFormat.Font.Name ="Arial";
					MySheet.Columns[iCell].CellFormat.Font.Height = 160;

					foreach (DataRow r in t.Rows)
					{
						if (!(r[col.ColumnName] is byte[]))
							MySheet.Rows[iRow].Cells[iCell].Value = r[col.ColumnName];
						else
							MySheet.Rows[iRow].Cells[iCell].Value = r[col.ColumnName].ToString();

						//MySheet.Rows[iRow].CellFormat.Font.Name= "Arial";
						//MySheet.Rows[iRow].CellFormat.Font.Height = 160;

						MySheet.Rows[iRow].Cells[iCell].CellFormat.TopBorderColor =ColorTranslator.FromHtml("#808080");
						MySheet.Rows[iRow].Cells[iCell].CellFormat.RightBorderColor =ColorTranslator.FromHtml("#808080");
						if(iRow%2 == 0)
							 MySheet.Rows[iRow].Cells[iCell].CellFormat.FillPatternForegroundColor = ColorTranslator.FromHtml("#F7F7F7");

						sizeColum = (MySheet.Rows[iRow].Cells[iCell].Value.ToString().Trim().Length >sizeColum)? MySheet.Rows[iRow].Cells[iCell].Value.ToString().Trim().Length : sizeColum;
						iRow += 1;
						if(iRow>65535)
							break;
					}
					MySheet.Rows[iRow-1].Cells[iCell].CellFormat.BottomBorderColor=ColorTranslator.FromHtml("#808080");

					MySheet.Columns[iCell].Width =(sizeColum+2)*256;
					iCell += 1;

				} //DataColumn

			} //DataTable

			return MyBook;
		}

	}
}
