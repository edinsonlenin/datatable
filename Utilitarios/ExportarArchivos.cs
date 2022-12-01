using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using Seriva.Bolsa.Herramientas.Constantes;
using System.IO;
using Seriva.EnterpriseLibraryExceptions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Seriva.Bolsa.Herramientas.Lenguaje;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Summary description for CSVHelper.
    /// </summary>
    public class ExportarArchivos
	{
		#region Atributos y sus metodos

		private string separadorExportar;
		private string separadorEnData;
		private DataSet datosDataSet;
		private String ruta;
		private String nombreArchivo;
		private String path;
		private String tipoReporte;
		private String nombreCabeceras = string.Empty;
		private String mensajeRetorno = null;
		private Int64  numeroRegistroMax;
		//private string 

		public ExportarArchivos()
		{
		}

		public Int64 NumeroRegistroMax
		{
			get { return numeroRegistroMax; }
			set { numeroRegistroMax = value; }
		}

		public String MensajeRetorno
		{
			get { return mensajeRetorno; }
			set { mensajeRetorno = value; }
		}

		public String NombreCabeceras
		{
			get { return nombreCabeceras; }
			set { nombreCabeceras = value; }
		}

		public String TipoReporte
		{
			get { return tipoReporte; }
			set { tipoReporte = value; }
		}

		public String Ruta
		{
			get { return ruta; }
			set { ruta = value; }
		}

		public String NombreArchivo
		{
			get { return nombreArchivo; }
			set { nombreArchivo = value; }
		}

		public DataSet DatosDataSet
		{
			get { return datosDataSet; }
			set { datosDataSet = value; }
		}

		/// <summary>
		/// Separador con la que se exportará en el Archivo.
		/// </summary>
		public string SeparadorExportar
		{
			get { return separadorExportar ; }
			set { separadorExportar  = value; }
		}

		/// <summary>
		/// Identifica la separacion entre columna de la Data, esta será reemplaza con SeparadorExportar.
		/// Usar en el caso que la fila de la Data esté anidado en una sola Columna. 
		/// </summary>
		public string SeparadorEnData
		{
			get { return separadorEnData; }
			set { separadorEnData  = value; }
		}

		#endregion

		/// <summary>
		/// Permite Exportar un Archivo CSV.
		/// </summary>
		/// <param name="usarCabeceraDatSet"> Usar la cabecera del DataSet o no</param>
		/// <param name="pagina"> Page</param>
		/// <param name="fileFisico"> Si es False no usa el parametro Ruta. Solo para Exportacion en CSV, otros ignoran</param>
		public  void ExportarDataSetToArchivo(bool usarCabeceraDatSet, Page pagina, bool fileFisico)
		{
			StreamWriter sw;
			try
			{
				if (datosDataSet.Tables.Count == 0 )
					return;

				ConstruirPath(fileFisico, pagina);

				sw = prepararArchivo();

				if(usarCabeceraDatSet)
					llenarCabecera(sw, this.datosDataSet.Tables[0]);
				else
					sw.WriteLine(this.NombreCabeceras);

				if (datosDataSet.Tables[0].Columns.Count == 1 )
					llenarCuerpoParaDataEnUnaColumna(sw, this.datosDataSet.Tables[0]);
				else
					llenarCuerpoParaDataEnVariasColumnas(sw, this.datosDataSet.Tables[0]);

				sw.Close();

				if(this.datosDataSet.Tables[0].Rows.Count >= this.NumeroRegistroMax)
					mensajeRetorno = "El archivo se guardó en la ruta parametrizada.";
				else
					descargar(pagina);

			}
			catch(Exception oException)
			{
				bool rethrow = ExceptionPolicy.HandleException(oException,new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorGeneral"));
				if (rethrow)
					throw new Exception(oException.Message);
				else
					throw  new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorGeneral"));
			}

		}

		#region Preparar Archivo
		/// <summary>
		/// Construye el Path
		/// </summary>
		/// <param name="fileFisico"> Si es False, no usa la Ruta</param>
		/// <param name="pagina"></param>

		public void ConstruirPath(bool fileFisico, Page pagina)
		{
			this.path = (fileFisico) ? this.Ruta + this.NombreArchivo : pagina.MapPath(this.NombreArchivo);
		}

		public void eliminarArchivo(){
			try
			{
				System.IO.File.Delete(this.path);

			}
			catch(DirectoryNotFoundException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.DirectorioNoEncontrado"));
			}
			catch(UnauthorizedAccessException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.AccesoNoPermitido"));
			}
			catch(IOException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorDeEscritura"));
			}
			catch(NotSupportedException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorNoSoportado"));
			}
			catch(Exception oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorGeneral"));
			}

		}
		/// <summary>
		/// Preparar Archivo.
		/// </summary>
		/// <returns> StreamWriter </returns>
		public StreamWriter prepararArchivo()
		{
			try
			{
				FileStream ostrm;
				ostrm = new FileStream(this.path, FileMode.Create,FileAccess.ReadWrite);
				StreamWriter sw = new StreamWriter(ostrm, Encoding.UTF8);

				return sw;
			}
			catch(DirectoryNotFoundException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.DirectorioNoEncontrado"));
			}
			catch(UnauthorizedAccessException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.AccesoNoPermitido"));
			}
			catch(IOException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorDeEscritura"));
			}
			catch(NotSupportedException oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorNoSoportado"));
			}
			catch(Exception oException)
			{
				ExceptionPolicy.HandleException(oException,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				throw new ExcepcionLogicaNegocio(new MensajeAplicacion().ObtenerMensaje("ReporteClientes.ErrorGeneral"));
			}

		}

		#endregion

		#region Llenado de Data
		public void llenarCabecera(StreamWriter sw, DataTable table)
		{
			String nombreCabeceras = string.Empty;
			int numColum = table.Columns.Count;

			for(int i = 0; i < numColum; i++)
			{
				nombreCabeceras =  table.Columns[i].ColumnName;
				sw.Write(nombreCabeceras);
				if (i < numColum - 1) sw.Write(this.SeparadorExportar );
			}
			sw.Write(sw.NewLine);
			//sw.WriteLine(nombreCabeceras.TrimEnd(';'));
		}

		public void llenarCuerpoParaDataEnUnaColumna(StreamWriter sw, DataTable table)
		{
			String xdato;
			foreach(DataRow  fila in table.Rows){
				xdato = HttpUtility.HtmlDecode( fila[0].ToString() ).Trim().Replace(',','-').Replace(this.SeparadorEnData, this.SeparadorExportar );
				sw.WriteLine(xdato.ToString());
			}

		}

		public void llenarCuerpoParaDataEnVariasColumnas(StreamWriter sw, DataTable table)
		{
			String xdato;
			StringBuilder  datos;
			int index = 0;
			int numColum = table.Columns.Count;
			foreach(DataRow  fila in table.Rows)
			{
				datos =  new StringBuilder();
				for( index = 0; index < numColum ; index++)
				{
					xdato= HttpUtility.HtmlDecode(fila[index].ToString()).Trim().Replace(',','-');
					datos.Append(xdato);
					datos.Append(this.SeparadorExportar);
				}
				xdato = datos.ToString().Substring(0 , datos.ToString().Length - this.SeparadorExportar .Length );
				sw.WriteLine(xdato.ToString());
			}

		}

		#endregion

		#region Descarga
		public void descargar(Page pagina)
		{

			String tipoContenidoArchivo;
			if(this.TipoReporte.Equals(ConstantesBolsa.REPORTE_ARCHIVO_CSV))
			{
				tipoContenidoArchivo = "application/vnd.csv";
				descargarArchivo(pagina, tipoContenidoArchivo);
			}
			else
			{
				tipoContenidoArchivo = "application/octet-stream";
				descargarArchivo(pagina, tipoContenidoArchivo);
			}

		}

		public void exportarPorPopUp(Page pagina)
		{
			try
			{
				FileInfo archivo = new FileInfo(this.path);

				pagina.Response.Clear();
				pagina.Response.AddHeader("content-disposition", "attachment; filename="+archivo.Name);
				pagina.Response.Charset = string.Empty;
				pagina.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				pagina.Response.ContentType = "application/vnd.csv";
				//pagina.Response.Charset = "UTF-8";
				//pagina.Response.ContentEncoding = System.Text.Encoding.Unicode;

				pagina.Response.WriteFile(archivo.FullName);

				pagina.Response.Flush();
				pagina.Response.End();

			}
			catch (Exception ex)
			{

			}

		}

		private void descargarArchivo(Page pagina , String tipoContenidoArchivo)
		{
			FileInfo archivo = new FileInfo(this.path);
			pagina.Response.Clear();
			pagina.Response.AddHeader("Content-Disposition", "attachment; filename=" + archivo.Name);
			pagina.Response.AddHeader("Content-Length", archivo.Length.ToString());
			pagina.Response.ContentType = tipoContenidoArchivo;
			pagina.Response.WriteFile(archivo.FullName);
			pagina.Response.End();
		}

		#endregion
	}

}
