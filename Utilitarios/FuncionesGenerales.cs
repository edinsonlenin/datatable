using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Seriva.Utilitarios;
using System.Collections.Generic;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Summary description for FuncionesGenerales.
    /// </summary>
    public class FuncionesGenerales
	{
		//		protected int i_Residuo,i_DigObt;
		protected const int MINIMOMINUSCULAS = 97;
		protected const int MAXIMOMINUSCULAS = 122;
		protected const int MINIMOMAYUSCULAS = 65;
		protected const int MAXIMOMAYUSCULAS = 90;
		protected const int MINIMONUMEROS = 48;
		protected const int MAXIMONUMEROS = 57;

		public static StringBuilder ScriptRefrescoPadre(string mensaje, bool cerrarVentana)
		{
			StringBuilder  MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("\t $(document).ready(function(evt){ refrescarPadre('");
			MyScript.Append(mensaje);
			MyScript.Append("'");

			if(cerrarVentana)
			{
				MyScript.Append(",true)");
			}
			else
			{
				MyScript.Append(", false)");
			}

			MyScript.Append(";});</script>");
			return MyScript;
		}

		public static StringBuilder ScriptRefrescoCambiosPadre(string mensaje, bool cerrarVentana)
		{
			StringBuilder  MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("\t $(document).ready(function(evt){ refrescarCambiosPadre('");
			MyScript.Append(mensaje);
			MyScript.Append("'");

			if(cerrarVentana)
			{
				MyScript.Append(",true)");
			}
			else
			{
				MyScript.Append(",false)");
			}
			MyScript.Append(";});</script>");
			return MyScript;
		}

		public static StringBuilder ScriptRefrescoPadre()
		{
			StringBuilder  MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("\tjavascript:refrescarPadreEntidad()");
			MyScript.Append("</script>");
			return MyScript;
		}

		public static StringBuilder ScriptMensajeFinalOperacion(string mensaje)
		{
			StringBuilder  MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("\tjavascript:mostrarMensajeFinalOperacion('");
			MyScript.Append(mensaje);
			MyScript.Append("')");
			MyScript.Append("</script>");
			return MyScript;
		}

		/// <summary>
		/// Genera un dataset a partir de 1 ultrawebgrid
		/// </summary>
		/// <param name="UltraWebGrid1">UltraWebGrid origen</param>
		/// <param name="al_EstadoFila">Estado de las filas o nulo</param>
		/// <returns>DataSet que contiene la información del UltraWebGrid</returns>
		public static DataSet of_UltraWebGridFlatToDataSet(UltraWebGrid UltraWebGrid1, ArrayList al_EstadoFila)
		{
			DataSet ds_Aux = new DataSet();
			int i_TotalColumna;
			int i_TotalFila;

			//Tabla 1
			ds_Aux.Tables.Add();
			//Columnas
			i_TotalColumna = UltraWebGrid1.Columns.Count;
			for (int i=0;i<i_TotalColumna;i++)
				ds_Aux.Tables[0].Columns.Add(UltraWebGrid1.Columns[i].BaseColumnName, Type.GetType(UltraWebGrid1.Columns[i].DataType));
			//Filas
			i_TotalFila = UltraWebGrid1.Rows.Count;
			for (int i=0;i<i_TotalFila;i++)
			{
				DataRow dr_Fila = ds_Aux.Tables[0].NewRow();
				for (int j=0;j<i_TotalColumna;j++)
					if ( UltraWebGrid1.Rows[i].Cells[j].Value != null )
						dr_Fila[j] = UltraWebGrid1.Rows[i].Cells[j].Value;
					else
						dr_Fila[j] = DBNull.Value;
				ds_Aux.Tables[0].Rows.Add(dr_Fila);
				if ( al_EstadoFila != null && al_EstadoFila.Count == i_TotalFila )
				{
					switch( al_EstadoFila[i].ToString() )
					{
						case "Insert": // No se hace nada
							break;
						case "Delete":
							dr_Fila.AcceptChanges();
							dr_Fila.Delete();
							break;
						case "Update":
							// Modifico una celda para obtener un estado modificado
							dr_Fila.AcceptChanges();
							object obj_Temp = dr_Fila[0];
							dr_Fila[0] = DBNull.Value;
							dr_Fila[0] = obj_Temp;
							break;
						case "Remove":
							ds_Aux.Tables[0].Rows.Remove(dr_Fila);
							break;
					}
				}
			}
			return ds_Aux;
		}
		/// <summary>
		/// Función para eliminar un item de un ddl
		/// </summary>
		/// <param name="ddl"></param>
		public static void of_DeleteItemDdl(DropDownList ddl)
		{
			for (int i=0; i< ddl.Items.Count;i++)
			{
				ddl.Items.RemoveAt(i);
				i = i-1;
			}
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataSet del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarDropDownList(DropDownList ddl, DataSet ds, string id, string text)
		{
			ddl.Items.Clear();
			if(ds == null || ds.Tables.Count == 0)
				return;
			DataView dt_Vista = new DataView(ds.Tables[0]);
			dt_Vista.Sort = text;
			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="dt">Objeto DataTable del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarDropDownList(DropDownList ddl, DataTable dt, string id, string text)
		{
			ddl.Items.Clear();
			ddl.DataSource = dt;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		public static void of_LlenarDropDownListAnio(DropDownList ddl, int anios)
		{
			ddl.Items.Clear();
			int anioActual = DateTime.Now.Year;

			for(int i=anioActual;i>=anioActual-anios;i--)
			{
				ddl.Items.Add(new ListItem(i.ToString(),i.ToString()));
			}
		}

		/// <summary>
		/// Procedimiento que carga valores de un DataTable en un objeto WebCombo
		/// </summary>
		/// <param name="wc">Objeto WebCombo que despliega lista de opciones</param>
		/// <param name="dt">Objeto DataSet del cual se toma la información a cargarse. Se toma la primera tabla.</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad DataValueField del WebCombo</param>
		/// <param name="text">Corresponde al campo del cual se obtiene el texto que se visualiza en el objeto WebCombo</param>
		/// <param name="ordenar">Indica si los valores del WebCombo estarán ordenados por el campo texto en forma ascendente.</param>
		public static void of_LlenarWebCombo(WebCombo wc, DataSet ds, string id, string text, bool ordenar)
		{
			DataView dt_Vista = new DataView(ds.Tables[0]);
			if(ordenar)
				dt_Vista.Sort = text;

			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DropDownLayout.DropdownWidth = 200;
			wc.DropDownLayout.ColHeadersVisible = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.No;
			wc.DataBind();
			for(int i=0; i< wc.Columns.Count ; i++)
			{
				if(wc.Columns[i].BaseColumnName != text)
					wc.Columns[i].Hidden = true;
			}
		}

		/// <summary>
		/// Procedimiento que carga valores de un DataTable en un objeto WebCombo
		/// </summary>
		/// <param name="wc">Objeto WebCombo que despliega lista de opciones</param>
		/// <param name="dt">Objeto DataTable del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad DataValueField del WebCombo</param>
		/// <param name="text">Corresponde al campo del cual se obtiene el texto que se visualiza en el objeto WebCombo</param>
		/// <param name="ordenar">Indica si las columnas del WebCombo estarán ordenados</param>
		/// <param name="columnasordenar">Cadena de campos concatenados en la forma CAMPO1_DATATABLE DESC|CAMPO2_DATATABLE|CAMPO3_DATATABLE... pueden incluirse ASC DESC para ordenar</param>
		/// <param name="columnasmostrar">Cadena de campos concatenados en la forma CAMPO1_DATATABLE|CAMPO2_DATATABLE|CAMPO3_DATATABLE... </param>
		/// <param name="anchodropdown">ancho del dropdown del webcombo</param>
		public static void of_LlenarWebComboVariasColumnas(WebCombo wc, DataTable dt, string id, string text, bool ordenar, string columnasordenar,string columnasmostrar, int anchodropdown)
		{
			DataView dt_Vista = new DataView(dt);
			string cadenaorden = string.Empty;
			char[] sep = {'|'};

			string[] arrColumnasOrdenar = columnasordenar.Split(sep);
			string[] arrColumnasMostrar = columnasmostrar.Split(sep);
			int[] arrColumnasIndex = new int[arrColumnasMostrar.Length];
			int[] arrColumnasIndexAnt = new int[arrColumnasMostrar.Length];

			if(ordenar)
			{
				if(columnasordenar.Length > 0)
				{
					for (int i=0;i< arrColumnasOrdenar.Length;i++)
					{
						cadenaorden += arrColumnasOrdenar[i] + ",";
					}

					cadenaorden = cadenaorden.Substring(0,cadenaorden.Length -1);

					dt_Vista.Sort = cadenaorden;
				}
			}

			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DataBind();

			for(int i=0; i< wc.Columns.Count ; i++)
			{
				int numeroColumna =-1;

				for(int j=0; j< arrColumnasMostrar.Length ; j++){

					if(arrColumnasMostrar[j] == wc.Columns[i].BaseColumnName.ToString())
						numeroColumna = j;
				}

				if(numeroColumna==-1)
					wc.Columns[i].Hidden = true;
			}

			for(int j=0; j< arrColumnasMostrar.Length ; j++){

				arrColumnasIndex[j] = wc.Columns.FromKey(arrColumnasMostrar[j]).Index;
			}

			for(int j=0; j< arrColumnasIndex.Length ; j++)
			{

				arrColumnasIndexAnt[j] = arrColumnasIndex[j];
			}

			Array.Sort(arrColumnasIndex);

			for(int j=0; j< arrColumnasMostrar.Length ; j++)
			{
				wc.Columns.FromKey(arrColumnasMostrar[j]).Move(arrColumnasIndex[j]);
			}

			new FormatoControl().AplicarFormato(wc, (int)FormatoWebCombo.Estandar);
			wc.DropDownLayout.DropdownWidth =anchodropdown;

		}

		public static void of_LlenarWebComboVariasColumnas1(WebCombo wc, DataTable dt, string id, string text, bool ordenar, string columnasordenar,string columnasmostrar, string anchodropdown)
		{
			DataView dt_Vista = new DataView(dt);
			string cadenaorden = string.Empty;
			char[] sep = {'|'};

			string[] arrColumnasOrdenar = columnasordenar.Split(sep);
			string[] arrColumnasMostrar = columnasmostrar.Split(sep);
			int[] arrColumnasIndex = new int[arrColumnasMostrar.Length];
			int[] arrColumnasIndexAnt = new int[arrColumnasMostrar.Length];

			if(ordenar)
			{
				if(columnasordenar.Length > 0)
				{
					for (int i=0;i< arrColumnasOrdenar.Length;i++)
					{
						cadenaorden += arrColumnasOrdenar[i] + ",";
					}

					cadenaorden = cadenaorden.Substring(0,cadenaorden.Length -1);

					dt_Vista.Sort = cadenaorden;
				}
			}

			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DataBind();

			for(int i=0; i< wc.Columns.Count ; i++)
			{
				int numeroColumna =-1;

				for(int j=0; j< arrColumnasMostrar.Length ; j++)
				{

					if(arrColumnasMostrar[j] == wc.Columns[i].BaseColumnName.ToString())
						numeroColumna = j;
				}

				if(numeroColumna==-1)
					wc.Columns[i].Hidden = true;
			}

			for(int j=0; j< arrColumnasMostrar.Length ; j++)
			{

				arrColumnasIndex[j] = wc.Columns.FromKey(arrColumnasMostrar[j]).Index;
			}

			for(int j=0; j< arrColumnasIndex.Length ; j++)
			{

				arrColumnasIndexAnt[j] = arrColumnasIndex[j];
			}

			Array.Sort(arrColumnasIndex);

			for(int j=0; j< arrColumnasMostrar.Length ; j++)
			{
				wc.Columns.FromKey(arrColumnasMostrar[j]).Move(arrColumnasIndex[j]);
			}

			new FormatoControl().AplicarFormato(wc, (int)FormatoWebCombo.Estandar);

			string[] arrAnchosColumnas = anchodropdown.Split(sep);

			for(int i=0; i< arrColumnasMostrar.Length ; i++)
			{
				wc.Columns.FromKey(arrColumnasMostrar[i]).Width = Unit.Pixel(Convert.ToInt32(arrAnchosColumnas[i]));
			}

			wc.DropDownLayout.DropdownWidth =Convert.ToInt32(arrAnchosColumnas[0])+ Convert.ToInt32(arrAnchosColumnas[1]);

		}

        /// <summary>
        /// Procedimiento que carga valores de un DataTable en un objeto WebCombo
        /// </summary>
        /// <param name="wc">Objeto WebCombo que despliega lista de opciones</param>
        /// <param name="dt">Objeto DataTable del cual se toma la información a cargarse</param>
        /// <param name="id">Corresponde al valor que debe cargarse en la propiedad DataValueField del WebCombo</param>
        /// <param name="text">Corresponde al campo del cual se obtiene el texto que se visualiza en el objeto WebCombo</param>
        /// <param name="ordenar">Indica si los valores del WebCombo estarán ordenados por el campo texto en forma ascendente.</param>
        public static void of_LlenarWebComboVariasColumnas(DropDownList wc, DataTable dt, HiddenField storageData, string id, string text, bool ordenar)
        {
            DataView dt_Vista = new DataView(dt);
            if (ordenar)
                dt_Vista.Sort = text;

            wc.Items.Clear();
            wc.DataSource = dt_Vista;
            wc.DataValueField = id;
            wc.DataTextField = text;
            wc.DataBind();

			storageData.Value = DatatableHelper.SerializeDatatableForDrowpDownList(dt, id, text);
        }

        /// <summary>
        /// Procedimiento que carga valores de un DataTable en un objeto WebCombo
        /// </summary>
        /// <param name="wc">Objeto WebCombo que despliega lista de opciones</param>
        /// <param name="dt">Objeto DataTable del cual se toma la información a cargarse</param>
        /// <param name="id">Corresponde al valor que debe cargarse en la propiedad DataValueField del WebCombo</param>
        /// <param name="text">Corresponde al campo del cual se obtiene el texto que se visualiza en el objeto WebCombo</param>
        /// <param name="ordenar">Indica si los valores del WebCombo estarán ordenados por el campo texto en forma ascendente.</param>
        public static void of_LlenarWebComboVariasColumnas(WebCombo wc, DataTable dt, string id, string text, bool ordenar)
		{
			DataView dt_Vista = new DataView(dt);
			if(ordenar)
				dt_Vista.Sort = text;

			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DataBind();

		}

		public static void of_LlenarWebComboVariasColumnas(DropDownList wc, DataView dt_Vista, HiddenField storageData, string id, string text) 
		{
            wc.Items.Clear();
            wc.DataSource = dt_Vista;
            wc.DataValueField = id;
            wc.DataTextField = text;
            wc.DataBind();

            storageData.Value = DatatableHelper.SerializeDatatableForDrowpDownList(dt_Vista.ToTable(), id, text);
        }


        public static void of_LlenarWebComboVariasColumnas(WebCombo wc, DataView dt_Vista, string id, string text)
		{
			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DataBind();

		}

		/// <summary>
		/// Procedimiento que carga valores de un DataTable en un objeto WebCombo
		/// </summary>
		/// <param name="wc">Objeto WebCombo que despliega lista de opciones</param>
		/// <param name="dt">Objeto DataTable del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad DataValueField del WebCombo</param>
		/// <param name="text">Corresponde al campo del cual se obtiene el texto que se visualiza en el objeto WebCombo</param>
		/// <param name="ordenar">Indica si los valores del WebCombo estarán ordenados por el campo texto en forma ascendente.</param>
		public static void of_LlenarWebCombo(WebCombo wc, DataTable dt, string id, string text, bool ordenar)
		{
			DataView dt_Vista = new DataView(dt);
			if(ordenar)
				dt_Vista.Sort = text;

			wc.Rows.Clear();
			wc.DataSource = dt_Vista;
			wc.DataValueField = id;
			wc.DataTextField = text;
			wc.DropDownLayout.DropdownWidth = 200;
			wc.DropDownLayout.ColHeadersVisible = Infragistics.WebUI.UltraWebGrid.ShowMarginInfo.No;
			wc.DataBind();
			for(int i=0; i< wc.Columns.Count ; i++)
			{
				if(wc.Columns[i].BaseColumnName != text)
					wc.Columns[i].Hidden = true;
			}
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataSet del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarRadioButtonList(RadioButtonList rbl, DataSet ds, string id, string text)
		{
			rbl.Items.Clear();
			rbl.DataSource = ds;
			rbl.DataValueField = id;
			rbl.DataTextField = text;
			rbl.DataBind();
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataSet del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarDropDownList(DropDownList ddl, DataSet ds, string id, string text, bool b_Ordenar)
		{
			DataView dt_Vista = new DataView(ds.Tables[0]);
			if(b_Ordenar)
				dt_Vista.Sort = text;

			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataTable del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		/// <param name="b_Ordenar">Indica si los valores deben estar ordenados en forma ascendente.</param>
		public static void of_LlenarDropDownList(DropDownList ddl, DataTable dt, string id, string text, bool b_Ordenar)
		{
			DataView dt_Vista = new DataView(dt);
			if(b_Ordenar)
			dt_Vista.Sort = text;

			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataSet del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarDropDownList(DropDownList ddl, DataView dt_Vista, string id, string text)
		{
			dt_Vista.Sort = text;
			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		public static void of_LlenarDropDownList(DropDownList ddl, DataView dt_Vista, string id, string text, bool b_Ordenar)
		{
			if(b_Ordenar)
				dt_Vista.Sort = text;
			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		/// <summary>
		/// Procedimiento que carga valores en un objeto DropDownList
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="ds">Objeto DataSet del cual se toma la información a cargarse</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="text">Corresponde al texto que se visualiza en el objeto ddl</param>
		public static void of_LlenarDropDownListOrdenado(DropDownList ddl, DataSet ds, string id, string text, string ps_CriterioOrden)
		{
			DataView dt_Vista = new DataView(ds.Tables[0]);
			dt_Vista.Sort = text + " " + ps_CriterioOrden;
			ddl.Items.Clear();
			ddl.DataSource = dt_Vista;
			ddl.DataValueField = id;
			ddl.DataTextField = text;
			ddl.DataBind();
		}

		/// <summary>
		/// Añade un item a un ddl 
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="name">Corresponde al texto que se visualiza en el objeto ddl</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="indice">Corresponde a la posición en donde se ubica el item añadido</param>
		public static void of_AddItemDdl(DropDownList ddl,string name,string id,int indice)
		{
			// Añado un item al campo DropDownList
			ListItem item = new ListItem(name,id);
			ddl.Items.Insert(indice,item);
		}
		/// <summary>
		/// Añade un item a un ddl 
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="name">Corresponde al texto que se visualiza en el objeto ddl</param>
		/// <param name="id">Corresponde al valor que debe cargarse en la propiedad Value del ddl</param>
		/// <param name="indice">Corresponde a la posición en donde se ubica el item añadido</param>
		public static void of_AddItemRbl(RadioButtonList rbl,string name,string id,int indice)
		{
			// Añado un item al campo DropDownList
			ListItem item = new ListItem(name,id);
			rbl.Items.Insert(indice,item);
		}


		/// <summary>
		/// Método para Truncar
		/// </summary>
		/// <param name="ddl">Redondeo al Número Menor cercano</param>
		/// <param name="id"></param>
		public static decimal TruncateDecimal(decimal value, int precision)
		{
			//return Math.Round(value - Convert.ToDecimal((0.5 / Math.Pow(10, precision))), precision);
			decimal stepper = (decimal)(Math.Pow(10.0, (double)precision));
			int temp = (int)(stepper * value);
			return (decimal)temp / stepper;
		}

		/// <summary>
		/// Encuentre un item del ddl por el campo Value
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="id"></param>
		public static ListItem of_FindItemDdlValue(DropDownList ddl, string id)
		{
			// Marco como seleccionado el campo del parametro en la lista
			foreach(ListItem item in ddl.Items )
			{
				string item_value = item.Value;
				if (item_value == id)
					return item;
			}
			return null;
		}
		/// <summary>
		/// Encuentre un item del ddl por el campo Text
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="id"></param>
		public static ListItem of_FindItemDdlText(DropDownList ddl, string Text)
		{
			// Marco como seleccionado el campo del parametro en la lista
			foreach(ListItem item in ddl.Items )
			{
				string item_Text = item.Text;
				if (item_Text == Text)
					return item;
			}
			return null;
		}
		/// <summary>
		/// Selecciona un item del ddl por el Value
		/// </summary>
		/// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
		/// <param name="id"></param>
		public static void of_SelectItemDdl(DropDownList ddl, string id)
		{
			// Marco como seleccionado el campo del parametro en la lista
			int index = 0;
			foreach(ListItem item in ddl.Items )
			{
				string item_value = item.Value;
				if (item_value == id)
				{
					ddl.SelectedIndex = index;
					break;
				}
				index = index + 1;
			}
		}

		public static void of_SelectItemWc(WebCombo wc, string id)
		{
			// Marco como seleccionado el campo del parametro en la lista
			int index = 0;
			foreach(UltraGridRow item in wc.Rows )
			{
				string item_value = item.Cells.FromKey(wc.DataValueField).Value.ToString();
				if (item_value.Equals(id))
				{
					wc.SelectedIndex = index;
					break;
				}
				index = index + 1;
			}
		}

        public static int of_SelectItemWc(DataTable dataTable, string key, string id)
        {
            // Marco como seleccionado el campo del parametro en la lista
            int index = -1;
            foreach (DataRow item in dataTable.Rows)
            {
                index = index + 1;
                string item_value = item[key].ToString();
                if (item_value.Equals(id))
                {                    
					return index;                   
                }                
            }
			return index;
        }

        /// <summary>
        /// Selecciona un item del ddl por el Text
        /// </summary>
        /// <param name="ddl">Objeto DropDownList que despliega lista de opciones</param>
        /// <param name="Text"></param>
        public static void of_SelectItemDdlText(DropDownList ddl, string Text)
		{
			// Marco como seleccionado el campo del parametro en la lista
			int index = 0;
			foreach(ListItem item in ddl.Items )
			{
				string item_Text = item.Text;
				if (item_Text == Text)
				{
					ddl.SelectedIndex = index;
					break;
				}
				index = index + 1;
			}
		}

		/// <summary>
		/// Selecciona un item del rbl
		/// </summary>
		/// <param name="rbl">Objeto RadioButtonList que despliega lista de opciones</param>
		/// <param name="id"></param>
		public static void of_SelectItemRbl(RadioButtonList  rbl, string id)
		{
			// Marco como seleccionado el campo del parametro en la lista
			int index = 0;
			foreach(ListItem item in rbl.Items )
			{
				string item_value = item.Value;
				if (item_value == id)
				{
					rbl.SelectedIndex = index;
					break;
				}
				index = index + 1;
			}
		}

		public void of_GenerarValidacionesGridCliente( UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage, string s_FormatoFechaUsuario,int _Numtablas)
		{
			GenerarValidacionesGrid(Grid,ds_aux,s_Separador,MyPage,s_FormatoFechaUsuario ,true,_Numtablas,true);
		}

		public void of_GenerarValidacionesGridClienteSinFecha( UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage,int _Numtablas)
		{
			GenerarValidacionesGrid(Grid,ds_aux,s_Separador,MyPage,"",false,_Numtablas,true);
		}

		public void of_GenerarValidacionesGridClienteSinFechaSinNegativos( UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage,int _Numtablas)
		{
			GenerarValidacionesGrid(Grid,ds_aux,s_Separador,MyPage,"",false,_Numtablas,false);
		}

		public void of_GenerarValidacionesGridClienteSinNegativos( UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage,int _Numtablas)
		{
			GenerarValidacionesGrid(Grid,ds_aux,s_Separador,MyPage,"",false,_Numtablas,false);
		}

		private void GenerarValidacionesGrid(UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage, string s_FormatoFechaUsuario , bool _GenerarCalendario,int _Numtablas, bool _NumNegativos)
		{
			string condicion_if = string.Empty;
			string s_colnumericas = string.Empty;
			string s_coldecimal = string.Empty;
			string s_coldoubles = string.Empty;
			string s_colstring = string.Empty;

			for( int j=0;j<_Numtablas;j++)
				for(int i = 0;i < ds_aux.Tables[j].Columns.Count;i++)
				{
					if (ds_aux.Tables[j].Columns[i].DataType.ToString() == "System.String")
						s_colstring    += "(column.Key == \"" + ds_aux.Tables[j].Columns[i].ColumnName + "\")  || ";
					if ( _GenerarCalendario )
						if (ds_aux.Tables[j].Columns[i].DataType.ToString() == "System.DateTime")
						{
							condicion_if   += "(column.Key == \"" + ds_aux.Tables[j].Columns[i].ColumnName + "\")  || ";
						}
					if (ds_aux.Tables[j].Columns[i].DataType.ToString() == "System.Int32")
						s_colnumericas += "(column.Key == \"" + ds_aux.Tables[j].Columns[i].ColumnName + "\")  || ";
					if (ds_aux.Tables[j].Columns[i].DataType.ToString() == "System.Decimal" )
						s_coldecimal   += "(column.Key == \"" + ds_aux.Tables[j].Columns[i].ColumnName + "\")  || ";
					if (ds_aux.Tables[j].Columns[i].DataType.ToString() == "System.Double" )
						s_coldoubles   += "(column.Key == \"" + ds_aux.Tables[j].Columns[i].ColumnName + "\")  || ";
				}

			MyPage.Response.Output.WriteLine("<script language=\"javascript\">\n");
			MyPage.Response.Output.WriteLine("var separadordec = '" + s_Separador + "';");
			//MyPage.Response.Output.WriteLine("var FormatoGridFecha = '" + s_FormatoFechaUsuario + "';");
			//Genera para abrir el calendario en el click

			if (condicion_if != "" && _GenerarCalendario == true)
			{
				condicion_if = condicion_if.Substring(0,condicion_if.Length - 3);
				condicion_if = condicion_if.Substring(0,condicion_if.Length - 3);
				MyPage.Response.Output.WriteLine("\tfunction MouseDown(tableName, itemName, keyStroke)\n");
				MyPage.Response.Output.WriteLine("\t{\n\t\ttry{\n\t\tvar column = igtbl_getColumnById(itemName);\n");
				MyPage.Response.Output.WriteLine("\t\n\t\tvar cell = igtbl_getCellById(itemName);\n");
				//MyPage.Response.Output.WriteLine("\t\n\t\talert('Item: ' + itemName + '  Cell: ' + cell + ' Valor: ' + cell.getValue() );\n");
				MyPage.Response.Output.WriteLine("\t\n\t\tvar valor = cell.getValue();\n");
				MyPage.Response.Output.WriteLine("\t\n\t\tvar formato = '" + s_FormatoFechaUsuario +"';\n");		//Atención se puso en codigo duro el formato de calendario MM/dd/yyyy para la version de infragistic 3.1
				MyPage.Response.Output.WriteLine("\t\tif(" + condicion_if + "))\n\t\t{\n");

				//No permitir editar la fecha actual cuando es empresa 
				MyPage.Response.Output.WriteLine("\t\t\tif((window.location.pathname.indexOf('empresa.aspx')>0 && (column.Key == 'FechaActual')) || (window.location.pathname.indexOf('definicionvertice.aspx')>0 && (column.Key == 'FechaCreacion'))){\n");	//nuevo
				MyPage.Response.Output.WriteLine("\t\t\t\tvar aux='';\n");		//nuevo
				MyPage.Response.Output.WriteLine("\t\t\t}\n");					//nuevo
				MyPage.Response.Output.WriteLine("\t\t\telse{\n");				//nuevo

				//S.P. cambio para no permitir abrir nuevas ventanas de calendario mientras una esta abierta
				MyPage.Response.Output.WriteLine("\t\t\t\twindow.showModalDialog('../../Util/Herramientas/Calendar.aspx?lista=' + itemName + '&valor=' + valor + '&formato=' + formato + '&catalogo=si', window, 'scroll:no; dialogWidth:310px; dialogHeight:305px; menubar=no; resizable:no; status=no; dialogLeft:300px; dialogTop:220px');\n");
				//S.P 17/may/2004

				MyPage.Response.Output.WriteLine("\t\t\t}\n\t\t\treturn 0;\n");
				MyPage.Response.Output.WriteLine("\t\t}\n\t\t}\n\t\tcatch(objError){}\n");					//nuevo
				MyPage.Response.Output.WriteLine("\t}\n\tfunction SetearFecha(id_Cell, Fecha){");
				//MyPage.Response.Output.WriteLine("\n\t\talert(Fecha);");
				MyPage.Response.Output.WriteLine("\n\t\tvar cell = igtbl_getCellById(id_Cell);");
				//MyPage.Response.Output.WriteLine("\n\t\twindow.document.all(id_Cell).innerHTML = Fecha.toString();");
				MyPage.Response.Output.WriteLine("\n\t\tcell.setValue(Fecha);\n\t}");

				Grid.DisplayLayout.ClientSideEvents.MouseDownHandler = "MouseDown";
			}

			MyPage.Response.Output.WriteLine("\tfunction EditKeyDown1(tableName, itemName, keyStroke)\n");
			MyPage.Response.Output.WriteLine("\t{\n\t\tvar column = igtbl_getColumnById(itemName);\n");
			//valida cuando son columnas tipo cadenas
			if (s_colstring != "")
			{
				s_colstring = s_colstring.Substring(0,s_colstring.Length - 3);
				MyPage.Response.Output.WriteLine("\t\tif(" + s_colstring + ")\n\t\t{\n");
				//MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke == 226)  ){\n");
				MyPage.Response.Output.WriteLine("\t\t\tif(keyStroke == 226 || keyStroke==188 || keyStroke==219 /*|| keyStroke==54*/){\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\talert('No se Puede ingresar este Tipo de Caracteres.'  + keyStroke );");
				MyPage.Response.Output.WriteLine("\t\t\t\t\treturn 1;");
				MyPage.Response.Output.WriteLine("\t\t\t}");
				MyPage.Response.Output.WriteLine("\t\t}");
			}
			//valida cuando son columnas enteras
			if (s_colnumericas != "")
			{
				s_colnumericas = s_colnumericas.Substring(0,s_colnumericas.Length - 3);

				MyPage.Response.Output.WriteLine("\t\tif(" + s_colnumericas + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				// aumentado jj
				if ( _NumNegativos )
				{
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 109 /*menos del numpad */ ||\n");
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 189 /*menos del teclado */ ||\n");
				}

				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse{\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\tevent.returnValue = false; alert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'  );\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\treturn 1;\t\t\t\t\n}\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t}\n");
			}
			if ( s_coldecimal != "")
			{
				s_coldecimal = s_coldecimal.Substring(0,s_coldecimal.Length - 3);
				string s_SimboloDec = "188";

				if ( s_Separador != null  )
				{
					if ( s_Separador == "." )
						s_SimboloDec = "190";
					if ( s_Separador == "," )
						s_SimboloDec = "188";
				}

				MyPage.Response.Output.WriteLine("\t\tif(" + s_coldecimal + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				if ( _NumNegativos )
				{
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 109 /*menos del numpad */ ||\n");
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 189 /*menos del teclado */ ||\n");
				}
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == " + s_SimboloDec + " /*decimal del teclado */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse{\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\tevent.returnValue = false; alert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'   );\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\treturn 1;\t\t\t\t\n}\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t}\n");
			}
			if ( s_coldoubles != "")
			{
				s_coldoubles = s_coldoubles.Substring(0,s_coldoubles.Length - 3);
				string s_SimboloDec = "188";

				if ( s_Separador != null  )
				{
					if ( s_Separador == "." )
						s_SimboloDec = "190";
					if ( s_Separador == "," )
						s_SimboloDec = "188";
				}

				MyPage.Response.Output.WriteLine("\t\tif(" + s_coldoubles + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				if ( _NumNegativos )
				{
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 109 /*menos del numpad */ ||\n");
					MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 189 /*menos del teclado */ ||\n");
				}
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == " + s_SimboloDec + " /*decimal del teclado */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse{\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\tevent.returnValue = false; alert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'   );\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\treturn 1;\t\t\t\t\n}\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t}\n");
			}
			MyPage.Response.Output.WriteLine("\t\t\n\t\treturn 0;\n\t}\n");

			//Modificado por: FT
			//Aumento la condicion para que si se escribio una funcion javascript para KeyDown en la pagina
			//se evite que se sobreescriba con esta de default
			if(Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler==null || Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler.Trim()=="")
				Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler = "EditKeyDown1";
			//fin de aumento FT

			MyPage.Response.Output.WriteLine("\n</script>");
		}

		public void GenerarValidacionesMatriz(UltraWebGrid Grid, DataSet ds_aux, string s_Separador, Page MyPage)
		{
			string s_colnumericas = string.Empty;
			string s_coldecimal = string.Empty;
			string s_coldoubles = string.Empty;

			for(int i = 0;i < ds_aux.Tables[0].Columns.Count;i++)
			{
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.Int32")
					s_colnumericas += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.Decimal" )
					s_coldecimal   += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.Double" )
					s_coldoubles   += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
			}

			MyPage.Response.Output.WriteLine("<script language=\"javascript\">\n");
			MyPage.Response.Output.WriteLine("var separadordec = '" + s_Separador + "';");

			MyPage.Response.Output.WriteLine("\tfunction EditKeyDown1(tableName, itemName, keyStroke)\n");
			MyPage.Response.Output.WriteLine("\t{\n\t\tvar column = igtbl_getColumnById(itemName);\n");
			//valida cuando son columnas enteras
			if (s_colnumericas != "")
			{
				s_colnumericas = s_colnumericas.Substring(0,s_colnumericas.Length - 3);

				MyPage.Response.Output.WriteLine("\t\tif(" + s_colnumericas + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse\n{");
				MyPage.Response.Output.WriteLine("\t\t\t\t\tevent.returnValue = false;\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\ttalert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'  );\n\t\t}\n");
				MyPage.Response.Output.WriteLine("\t\t\t}\n");
			}
			if ( s_coldecimal != "")
			{
				s_coldecimal = s_coldecimal.Substring(0,s_coldecimal.Length - 3);
				string s_SimboloDec = "188";

				if ( s_Separador != null  )
				{
					if ( s_Separador == "." )
						s_SimboloDec = "190";
					if ( s_Separador == "," )
						s_SimboloDec = "188";
				}

				MyPage.Response.Output.WriteLine("\t\tif(" + s_coldecimal + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == " + s_SimboloDec + " /*decimal del teclado */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\t{event.returnValue = false;alert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'  );}\n\t\t}\n");
			}
			if ( s_coldoubles != "")
			{
				s_coldoubles = s_coldoubles.Substring(0,s_coldoubles.Length - 3);
				string s_SimboloDec = "188";

				if ( s_Separador != null  )
				{
					if ( s_Separador == "." )
						s_SimboloDec = "190";
					if ( s_Separador == "," )
						s_SimboloDec = "188";
				}

				MyPage.Response.Output.WriteLine("\t\tif(" + s_coldoubles + ")\n\t\t{\n");
				MyPage.Response.Output.WriteLine("\t\t\tif( (keyStroke >= 48 && keyStroke < 58  /*0-9*/ ) || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 37 && keyStroke <= 40 /*Arrow keys*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    (keyStroke >= 96 && keyStroke <= 105 /*Num Pad*/)  || \n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 8  /*backspace*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 9  /*tab*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 27 /*escape*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 36 /*inicio*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 35 /*fin*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 46 /*del*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 110 /*decimal del numpad */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == " + s_SimboloDec + " /*decimal del teclado */ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 16 /*shift*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 18 /*alt*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 17 /*control*/ ||\n");
				MyPage.Response.Output.WriteLine("\t\t\t    keyStroke == 13 /*enter*/) \n");
				MyPage.Response.Output.WriteLine("\t\t\t    return 0 /*Acepta los valores*/;\n");
				MyPage.Response.Output.WriteLine("\t\t\telse\n");
				MyPage.Response.Output.WriteLine("\t\t\t\t\t{event.returnValue = false;alert('Unicamente Valores Númericos pueden ser ingresados en esta columna.'  );}\n\t\t}\n");
			}
			MyPage.Response.Output.WriteLine("\t\t\n\t\treturn 0;\n\t}\n");
			//Modificado por: FT
			//Aumento la condicion para que si se escribio una funcion javascript para KeyDown en la pagina
			//se evite que se sobreescriba con esta de default
			if(Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler==null || Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler.Trim()=="")
				Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler = "EditKeyDown1";
			//fin de aumento FT
			MyPage.Response.Output.WriteLine("\n</script>");
		}

		/// <summary>
		/// Genera código javascript para validar el ingreso de información según el tipo de dato del campo del ultrawebgrid
		/// puede añadir un evento MouseDownHandler y EditKeyDown
		/// </summary>
		/// <param name="Grid">ultrawebgrid al que se asocia el evento y código generado</param>
		/// <param name="ds_aux">dataset asociado al ultrawebgrid de donde se leen los tipos de datos de los campos</param>
		/// <param name="s_Separador">separador de decimales</param>
		/// <returns>Código que se debe escribir en javascript</returns>
		private string of_GenerarValidacionesGridCliente( UltraWebGrid Grid, DataSet ds_aux, string s_Separador)
		{
			string s_CodigoFinal = string.Empty;
			string condicion_if = string.Empty;
			string s_colnumericas = string.Empty;
			string s_coldecimal = string.Empty;

			for(int i = 0;i < ds_aux.Tables[0].Columns.Count;i++)
			{
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.DateTime")
					condicion_if   += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.Int32")
					s_colnumericas += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
				if (ds_aux.Tables[0].Columns[i].DataType.ToString() == "System.Decimal" )
					s_coldecimal   += "(column.Key == \"" + ds_aux.Tables[0].Columns[i].ColumnName + "\")  || ";
			}

			s_CodigoFinal = "<script language=\"javascript\">\n";
			s_CodigoFinal += "var separadordec = '" + s_Separador + "';";

			//Genera para abrir el calendario en el click
			if (condicion_if != "")
			{
				condicion_if = condicion_if.Substring(0,condicion_if.Length - 3);
				s_CodigoFinal += "\tfunction MouseDown(tableName, itemName, keyStroke)\n";
				s_CodigoFinal += "\t{\n\t\ttry{\n\t\tvar column = igtbl_getColumnById(itemName);\n";
				s_CodigoFinal += "\t\tif(" + condicion_if + ")\n\t\t{\n";

				//s_CodigoFinal += "\t\t\twindow.open('../../Util/Herramientas/Calendar.aspx?lista=' + itemName,'','dependant=yes,screenX=0,screenY=0,height=285,width=310,menubar=no,scrollbars=no,status=no,toolbar=no');\n";
				//S.P. cambio para no permitir abrir nuevas ventanas de calendario mientras una esta abierta
				s_CodigoFinal += "\t\t\t\twindow.showModalDialog('../../Util/Herramientas/Calendar.aspx?lista=' + itemName + '&valor=' + valor + '&formato=' + formato + '&catalogo=si', window, 'scroll:no; dialogWidth:310px; dialogHeight:305px; menubar=no; resizable:no; status=no; dialogLeft:300px; dialogTop:220px');\n";
				//S.P. 17/may/2004

				s_CodigoFinal += "\t\t}\n\t\treturn 0;\n";
				s_CodigoFinal += "\t}\n\tfunction SetearFecha(id_Cell, Fecha){";
				s_CodigoFinal += "\n\t\tvar cell = igtbl_getCellById(id_Cell);";
				s_CodigoFinal += "\n\t\tcell.setValue(Fecha);\n\t}catch(objError){}\n\t}";
				Grid.DisplayLayout.ClientSideEvents.MouseDownHandler = "MouseDown";
			}

			//valida cuando son columnas enteras
			if (s_colnumericas != "")
			{
				s_colnumericas = s_colnumericas.Substring(0,s_colnumericas.Length - 3);

				s_CodigoFinal += "\tfunction EditKeyDown(tableName, itemName, keyStroke)\n";
				s_CodigoFinal += "\t{\n\t\tvar column = igtbl_getColumnById(itemName);\n";
				s_CodigoFinal += "\t\tif(" + s_colnumericas + ")\n\t\t{\n";
				s_CodigoFinal += "\t\t\tif( (keyStroke < 48 || keyStroke > 57) && (keyStroke < 96 || keyStroke > 105)  )\n";
				s_CodigoFinal += "\t\t\t\tif(keyStroke != 13 && keyStroke != 27 && keyStroke != 8 && keyStroke != 46 && keyStroke != 39 && keyStroke != 37 && keyStroke != 35 && keyStroke != 110 )\n";
				s_CodigoFinal += "\t\t\t\t\t{event.returnValue = false; talert('Unicamente Valores Númericos pueden ser ingresados en esta columna.');}\n}";

				if ( s_coldecimal != "")
				{
					s_coldecimal = s_coldecimal.Substring(0,s_coldecimal.Length - 3);
					s_CodigoFinal += "\t\tif(" + s_coldecimal + ")\n\t\t{\n";
					s_CodigoFinal += "\t\t\tif( (keyStroke < 48 || keyStroke > 57) && (keyStroke < 96 || keyStroke > 105)  )\n";
					string s_SimboloDec = "8";//Backspace
					if ( s_Separador != null  )
					{
						if ( s_Separador == "." )
							s_SimboloDec = "188";
						if ( s_Separador == "." )
							s_SimboloDec = "190";
					}
					s_CodigoFinal += "\t\t\t\tif(keyStroke != 13 && keyStroke != 27 && keyStroke != 8 && keyStroke != 46 && keyStroke != 39 && keyStroke != 37 && keyStroke != 35 && keyStroke != 110 && keyStroke != " + s_SimboloDec + ")\n";
					s_CodigoFinal += "\t\t\t\t\t{event.returnValue = false;alert('Unicamente valores númericos pueden ser ingresados en esta columna.  ' );}\n\t\t}\n";
				}
				s_CodigoFinal += "\t\t\n\t\treturn 0;\n\t}\n";
				//Modificado por: FT
				//Aumento la condicion para que si se escribio una funcion javascript para KeyDown en la pagina
				//se evite que se sobreescriba con esta de default
				if(Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler==null || Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler.Trim()=="")
					Grid.DisplayLayout.ClientSideEvents.EditKeyDownHandler = "EditKeyDown";
				//fin de aumento FT
				s_CodigoFinal += "\n</script>";
			}
			return s_CodigoFinal;
		}

		/// <summary>
		/// Trunca el Valor a NumDecimal decimales
		/// </summary>
		/// <param name="Valor">Valor que se trunca</param>
		/// <param name="NumDecimal">Número de decimales validos</param>
		/// <returns>Valor truncado</returns>
		public double Truncar(double Valor, int NumDecimal)
		{
			string s_Valor = Valor.ToString();
			if ( s_Valor.IndexOf(".") > 0 )
			{
				int i_IndicePunto = s_Valor.IndexOf(".");
				if ( i_IndicePunto + NumDecimal + 1 >= s_Valor.Length )
					return Valor;
				else
					return double.Parse(s_Valor.Substring(0, s_Valor.IndexOf(".")+NumDecimal+1));
			}
			else
				return Valor;
		}

		/// <summary>
		/// Trunca el Valor a NumDecimal decimales
		/// </summary>
		/// <param name="Valor">cadena que representa el Valor que se trunca</param>
		/// <param name="NumDecimal">Número de decimales validos</param>
		/// <returns>Valor truncado</returns>
		public string Truncar(string Valor, int NumDecimal)
		{
			if ( Valor.IndexOf(".") > 0 )
			{
				int i_IndicePunto = Valor.IndexOf(".");
				if ( i_IndicePunto + NumDecimal + 1 >= Valor.Length )
					return Valor;
				else
					return Valor.Substring(0,Valor.IndexOf(".")+NumDecimal+1);
			}
			else
				return Valor;
		}

		/// <summary>
		/// Descripción : Coloca una función javascript que deberá abrir un determinado reporte de crystal 
		/// Objetivo    : Abrir un reporte en una página popup dentro de del frame
		/// Creado por      : Fredy Pachacama
		/// Fecha creación  : 24/01/2004
		/// Modificado por  : 
		/// F. Modificación :
		/// </summary>
		/// <param name="ps_NombreReporte"></param>
		/// <param name="swp_Pagina"></param>
		public static void of_AbreReportePopup(string ps_NombreReporte, System.Web.UI.Page swp_Pagina)
		{
			swp_Pagina.Response.Write("<script language=\"javascript\">\n");
			swp_Pagina.Response.Write("\tfunction of_OpenReporte()\n");
			swp_Pagina.Response.Write("\t{\n");
			swp_Pagina.Response.Write("       window.open('../../Reporte/re_gui/" + ps_NombreReporte + "', '', 'height=550,width=800,menubar=no,screenY=50,screenX=0,scrollbars=yes,resizable=yes');\n");
			swp_Pagina.Response.Write("\t}\n");
			swp_Pagina.Response.Write("\tof_OpenReporte()\n");
			swp_Pagina.Response.Write("</script>");
		}

		public static string of_ObtenerNombrePaginaAspx(Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string ls_Pagina = null;
			int li_UltimaOcurrencia = 0;

			if (e.Cell != null)
			{
				if (e.Cell.Tag != null)
				{
					ls_Pagina = e.Cell.Tag.ToString();
					li_UltimaOcurrencia = ls_Pagina.LastIndexOf("/");
					ls_Pagina = ls_Pagina.Substring(li_UltimaOcurrencia + 1);
					//li_UltimaOcurrencia = ls_Pagina.IndexOf("?");
					//ls_Pagina = ls_Pagina.Remove(li_UltimaOcurrencia, 1).Trim();
					return ls_Pagina;
				}
			}
			return "";
		}

		public static void diseñarTitulo(System.Web.UI.Page pagina, string tituloPagina, int posicionArriba, int posicionIzquierda)
		{
			System.Web.UI.WebControls.Table tTitulo = new  System.Web.UI.WebControls.Table();
			tTitulo.BorderWidth = Unit.Pixel(0);
			tTitulo.BorderStyle = BorderStyle.Outset;
			tTitulo.Width  = Unit.Percentage(100);

			TableRow tRow = new System.Web.UI.WebControls.TableRow();
			TableCell tCell = new System.Web.UI.WebControls.TableCell();
			tCell.HorizontalAlign = HorizontalAlign.Right;
			tCell.VerticalAlign = VerticalAlign.Top;
			tCell.Style.Add("font","bolder 11px Arial, Verdana, Helvetica");
			tCell.Style.Add("color","#000000");
			tCell.Style.Add("text-transform","uppercase");
			tCell.Text = tituloPagina;
			tCell.Width = Unit.Percentage(100);
			tCell.Height = Unit.Pixel(18);
			tRow.Cells.Add(tCell);

			tTitulo.Rows.Add(tRow);

			Panel recuadro = new Panel();
			recuadro.Width = Unit.Percentage(100);
			recuadro.Height	= Unit.Pixel(20);
			recuadro.BorderStyle = BorderStyle.None;
			recuadro.BorderWidth = Unit.Pixel(0);
			recuadro.Style.Add("TOP",posicionArriba.ToString());
			recuadro.Style.Add("LEFT",posicionIzquierda.ToString());
			recuadro.Controls.Add(tTitulo);

			pagina.Controls.AddAt(pagina.Controls.Count, recuadro);
		}
		public static Hashtable ConversorSesion(bs_Sesion Sesion, string codigoProducto)
		{
			Hashtable hashSesion = new Hashtable();
			hashSesion.Add("CodigoCliente",Sesion.CodigoCliente);
			hashSesion.Add("CodigoClienteEmpresa",Sesion.CodigoClienteEmpresa);
			hashSesion.Add("CodigoEmpresa",Sesion.CodigoEmpresa);
			hashSesion.Add("CodigoEstructuraUsuario",Sesion.CodigoEstructuraUsuario);
			hashSesion.Add("CodigoMonedaLocal",Sesion.CodigoMonedaLocal);
			hashSesion.Add("CodigoMonedaReferencia",Sesion.CodigoMonedaReferencia);
			hashSesion.Add("CodigoNegocio",Sesion.CodigoNegocio);
			hashSesion.Add("CodigoPais",Sesion.CodigoPais);
			hashSesion.Add("CodigoPerfilUsuario",Sesion.CodigoPerfilUsuario);
			hashSesion.Add("CodigoTipoProducto",Sesion.CodigoTipoProducto);
			hashSesion.Add("CodigoUnidadNegocio",Sesion.CodigoUnidadNegocio);
            hashSesion.Add("CodigoUsuario",Sesion.CodigoUsuario);
			hashSesion.Add("CodigoProducto",codigoProducto);

			return hashSesion;
		}
		/// <summary>
		/// En cada uno de los tabs del UltraWebTab pasado como parámetro, en el query string de la URL,
		/// si el parámetro no existe este se agrega, en caso contrario, este se actualiza.
		/// </summary>
		/// <param name="ultraWebTab">UltraWebTab al que se le va a agregar o actualizar un parámetro en la URL.</param>
		/// <param name="nombreParametro">Nombre del parámetro a agregar o actualizar.</param>
		/// <param name="valorParametro">Valor del parámetro a agregar o actualizar.</param>
		/// 
		//Nuevo Metodo
		public static List<Tabs> AgregarParametroURLTabList(List<Tabs> tablist, string nombreParametro, string valorParametro, string tab = "*")
		{				
			if (tab == "*")
            {
                foreach (Tabs t in tablist)
                {
                    if (t.TargetUrl.Contains("?"))                    
						t.TargetUrl = t.TargetUrl +"&"+ nombreParametro + "=" + valorParametro;
                    else
						t.TargetUrl = t.TargetUrl + "?" + nombreParametro + "=" + valorParametro;
				}
            }
            else
            {
				
				if(tablist.FindAll(x => x.Text == tab).Count > 0)
                {
					Tabs t = tablist.Find(x => x.Text == tab);
					t.TargetUrl = t.TargetUrl.Contains("?") ? t.TargetUrl + "&" + nombreParametro + "=" + valorParametro : t.TargetUrl + "?" + nombreParametro + "=" + valorParametro;
				}							
			}
			return tablist;
		}
		public static void AgregarParametroURLTabs(Infragistics.WebUI.UltraWebTab.UltraWebTab ultraWebTab, string nombreParametro, string valorParametro)
		{
			string urlInicial;
			StringBuilder urlFinal = new StringBuilder();
			int posicionInicial;
			int posicionFinal;
			int posicionPrimerParametro;
			string caracterSeparacion;

			foreach(Infragistics.WebUI.UltraWebTab.Tab tab in ultraWebTab.Tabs)
			{
				urlInicial		= tab.ContentPane.TargetUrl;
				posicionInicial	= urlInicial.IndexOf(nombreParametro)-1;
				posicionPrimerParametro	= urlInicial.IndexOf("?");

				if(posicionPrimerParametro < 0)
				{//no existe ningún parámetro
					caracterSeparacion = "?";
				}
				else
				{//existe por lo menos un parámetro
					if(posicionInicial>0 && urlInicial.Substring(posicionInicial,1) == "?")
					{//se está actualizando el primer parámetro
						caracterSeparacion = "?";
					}
					else
					{
						caracterSeparacion = "&";
					}
				}

				if(posicionInicial < 0)
				{//el parámetro no existe
					posicionInicial	= urlInicial.Length;
					posicionFinal	= urlInicial.Length;
				}
				else
				{
					posicionFinal	= urlInicial.IndexOf("&", posicionInicial+1);
					if(posicionFinal<0)
					{
						posicionFinal = urlInicial.Length;
					}
				}

				urlFinal.Remove(0,urlFinal.Length);
				urlFinal.Append(urlInicial.Substring(0,posicionInicial));
				urlFinal.Append(caracterSeparacion);
				urlFinal.Append(nombreParametro);
				urlFinal.Append("=");
				urlFinal.Append(valorParametro);
				urlFinal.Append(urlInicial.Substring(posicionFinal));

				tab.ContentPane.TargetUrl = urlFinal.ToString();
			}
		}

		# region Habilita y deshabilita WebControls
		/// <summary>
		/// Descripción : Toma los controles de una página los recorre y de acuerdo a
		/// cierto criterio los pone como enable = true o false
		/// </summary>
		/// <param name="Page"></param>
		/// <param name="ht_Parametros"></param>
		public static void of_ActivarControles(System.Web.UI.Page Page, Hashtable ht_Parametros)
		{
			string ls_Parametro = string.Empty;

			foreach (Control obj_Control in Page.Controls)
			{
				foreach (Control obj_ControlHijo in obj_Control.Controls)
				{
					if (obj_ControlHijo is TextBox)
					{
						ls_Parametro = ((TextBox)obj_ControlHijo).ID;
						ls_Parametro = ls_Parametro.Substring(ls_Parametro.LastIndexOf("_", ls_Parametro.Length) + 1);
						if (ht_Parametros[ls_Parametro] != null)
							((TextBox)obj_ControlHijo).Enabled = true;
						else ((TextBox)obj_ControlHijo).Enabled = false;
					}

					if (obj_ControlHijo is DropDownList)
					{
						ls_Parametro = ((DropDownList)obj_ControlHijo).ID;
						ls_Parametro = ls_Parametro.Substring(ls_Parametro.LastIndexOf("_", ls_Parametro.Length) + 1);
						if (ht_Parametros[ls_Parametro] != null)
							((DropDownList)obj_ControlHijo).Enabled = true;
						else ((DropDownList)obj_ControlHijo).Enabled = false;
					}
					if (obj_ControlHijo is HtmlInputButton)
					{
						ls_Parametro = ((Button)obj_ControlHijo).ID;
						ls_Parametro = ls_Parametro.Substring(ls_Parametro.LastIndexOf("_", ls_Parametro.Length) + 1);
						if (ht_Parametros[ls_Parametro] != null)
							((Button)obj_ControlHijo).Enabled = true;
						else ((Button)obj_ControlHijo).Enabled = false;
					}
				}
			}
		}

		#region Llenar cualquier lista
		/// <summary>
		/// objetivo    : Llenar una lista cualesquiera
		/// descripción : 
		/// Creado por			:	FREDY PACHACAMA
		///	Fecha Creacion		:	11-Julio-2003
		///	Modificado por		:	FREDY PACHACAMA
		///	Fecha modificacion	:	30-Sept-2003
		/// </summary>
		/// <param name="pddl_Lista"></param>
		/// <param name="pas_items"></param>
		/// <param name="pi_Orden"></param>
		public static void f_LlenarLista(DropDownList pddl_Lista,string[] pas_items,int[] pi_Orden)
		{
			int i;
			for (i = 0;i < pas_items.GetLength(0);i++)
			{
				pddl_Lista.Items.Insert(i,pas_items[i]);
				pddl_Lista.Items[i].Value = pi_Orden[i].ToString();
			}
		}
		#endregion
		#endregion

		public static void of_LimpiarWebCombo(WebCombo webCombo)
		{
			webCombo.SelectedIndex=-1;
			webCombo.DisplayValue=String.Empty;
			webCombo.Rows.Clear();
		}

		/// <summary>
		/// Devuelve la ruta raíz de la aplicación
		/// </summary>
		/// <param name="ddl">Devuelve la ruta raíz de la aplicación</param>
		public static string ObtenerRutaAplicacion(Page pagina)
		{
			string valorRetorno = null;
			if (pagina != null)
			{
				valorRetorno = pagina.Request.ApplicationPath;
			}
			return valorRetorno;
		}
		public static string ObtenerRutaPagina(Page pagina)
		{
			string valorRetorno = null;
			if (pagina != null)
			{
				string rutaAplicacion = ObtenerRutaAplicacion(pagina);
				if (pagina.Request.Path.ToLower().StartsWith(rutaAplicacion.ToLower()))
				{
					valorRetorno = pagina.Request.Path.Substring(rutaAplicacion.Length);
				}
				else
				{
					valorRetorno = pagina.Request.Path;
				}
			}
			return valorRetorno;
		}
	}
}
