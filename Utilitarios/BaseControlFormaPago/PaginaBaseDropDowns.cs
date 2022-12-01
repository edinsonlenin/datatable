using System;
using System.Data;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.UtilitariosWebII.Controls.ListBox;

namespace Seriva.Bolsa.Presentacion.Utilitarios.BaseControlFormaPago
{
	/*--------------------------------------------------------------------------------------------------------------------------

				  CREADO POR              : Carlos Esquivel
				  FECHA DE CREACION       : 03/08/2005
				  MODIFICADO POR          :
				  FECHA DE MODIFICACION   :
				  REVISADO POR            :
				  FECHA DE REVISION       :

			--------------------------------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// Summary description for PaginaBaseDropDowns.
	/// </summary>
	public sealed class PaginaBaseDropDowns
	{
		internal PaginaBaseDropDowns()
		{
		}

		/// <summary>
		/// Carga un SerivaListBox
		/// </summary>
		/// <param name="slbControl">SerivaListBox a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarSerivaListBox(SerivaListBox slbControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, bool limpiarValores)
		{
			if(limpiarValores)
			{
				if(slbControl.ItemCount>0)
				{
					slbControl.ClearAll();
				}
			}
			slbControl.DataSource=fuenteDatos;
			slbControl.DataValueField=columnaValor;
			slbControl.DataTextField=columnaVisualizar;
			slbControl.DataBind();
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="columnaOrdenar">Columna de la fuente de datos a utilizar para ordenar del DropDown</param>
		/// <param name="ordenAscendente">'true'</param>
		public void CargarDropDownConOrdenamiento(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string columnaOrdenar, bool ordenAscendente)
		{
			try
			{
				DataView vista			= fuenteDatos.DefaultView;
				string tipoOrdenamiento = string.Empty;
				if(ordenAscendente)
				{
					tipoOrdenamiento = " " + ConstantesBolsa.TIPO_ORDENAMIENTO_ASCENDENTE;
				}
				else
				{
					tipoOrdenamiento = " " + ConstantesBolsa.TIPO_ORDENAMIENTO_DESCENDENTE;
				}
				vista.Sort				= columnaOrdenar + tipoOrdenamiento;
				control.DataSource		= vista.Table;
				control.DataTextField	= columnaVisualizar;
				control.DataValueField	= columnaValor;
				control.DataBind();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}

			}
		}

		/// <summary>
		/// Carga un DropDownList
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="campoVisualizarAInsertar">Descripción de la Fila a Insertar</param>
		/// <param name="campoValorAInsertar">Valor de la Fila a Insertar</param>
		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string campoVisualizarAInsertar, string campoValorAInsertar)
		{
			ddlControl.DataSource = fuenteDatos;
			ddlControl.DataTextField = columnaVisualizar;
			ddlControl.DataValueField = columnaValor;
			ddlControl.DataBind();
			if (campoValorAInsertar != null && campoVisualizarAInsertar != null)
			{
				ListItem item = new ListItem(campoVisualizarAInsertar, campoValorAInsertar);
				ddlControl.Items.Insert(0, item);
			}
		}

		/// <summary>
		/// Carga un DropDownList con Datos Ordenados
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="columnaOrdenar">Columna sobre la cual se van a ordenar los datos</param>
		/// <param name="campoVisualizarAInsertar">Descripción de la Fila a Insertar</param>
		/// <param name="campoValorAInsertar">Valor de la Fila a Insertar</param>
		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor,string columnaOrdenar, string campoVisualizarAInsertar, string campoValorAInsertar)
		{
			DataView vista = fuenteDatos.DefaultView;
			vista.Sort = columnaOrdenar;
			ddlControl.DataSource = vista.Table;
			ddlControl.DataTextField = columnaVisualizar;
			ddlControl.DataValueField = columnaValor;
			ddlControl.DataBind();
			if (campoValorAInsertar != null && campoVisualizarAInsertar != null)
			{
				ListItem item = new ListItem(campoVisualizarAInsertar, campoValorAInsertar);
				ddlControl.Items.Insert(0, item);
			}
		}

		/// <summary>
		/// Carga un DropDownList
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor)
		{
			CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor, null, null);
		}

		/// <summary>
		/// Carga un DropDownList con Datos Ordenados
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="columnaOrdenar">Columna sobre la cual se van a ordenar los datos</param>
		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string columnaOrdenar)
		{
			DataView vista = fuenteDatos.DefaultView;
			vista.Sort = columnaOrdenar;
			CargarDropDownList(ddlControl, vista.Table, columnaVisualizar, columnaValor, null, null);
		}

		/// <summary>
		/// Carga un DropDownList insertando una fila
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Arreglo de DataRows con la información deseada</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDownList(DropDownList ddlControl, DataRow[] fuenteDatos, string columnaVisualizar, string columnaValor)
		{
			CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor, null, null);
		}

		/// <summary>
		/// Carga un DropDownList insertando una fila
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Arreglo de DataRows con la información deseada</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="campoVisualizarAInsertar">Valor que se visualizará en la fila insertada</param>
		/// <param name="campoValorAInsertar">Valor que se mostrará en la fila insertada</param>		 
		public void CargarDropDownList(DropDownList ddlControl, DataRow[] fuenteDatos, string columnaVisualizar, string columnaValor, string campoVisualizarAInsertar, string campoValorAInsertar)
		{
			ddlControl.DataSource		= fuenteDatos;
			ddlControl.DataTextField	= columnaVisualizar;
			ddlControl.DataValueField	= columnaValor;
			ddlControl.DataBind();
			if (campoValorAInsertar != null && campoVisualizarAInsertar != null)
			{
				ListItem item = new ListItem(campoVisualizarAInsertar, campoValorAInsertar);
				ddlControl.Items.Insert(0, item);
			}
		}

		/// <summary>
		/// Llena un DropDown en una Grilla
		/// </summary>
		/// <param name="ultraWebGrid">Grilla a utilizar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDown(UltraWebGrid ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor)
		{
			try
			{
				ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
				ValueList valueList = ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).ValueList;
				valueList.DataSource = fuenteDatos;
				valueList.DisplayMember = columnaVisualizar;
				valueList.ValueMember = columnaValor;
				valueList.DataBind();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}

			}
		}

		/// <summary>
		/// Llena un DropDown en una Grilla indicando el band
		/// </summary>
		/// <param name="ultraWebGrid">Grilla a utilizar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// /// <param name="banda">Posicion del campo band</param>
		public void CargarDropDown(UltraWebGrid ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor, int banda)
		{
			ultraWebGrid.Bands[banda].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
			ValueList valueList = ultraWebGrid.Bands[banda].Columns.FromKey(columnaDropDown).ValueList;
			valueList.DataSource = fuenteDatos;
			valueList.DisplayMember = columnaVisualizar;
			valueList.ValueMember = columnaValor;
			valueList.DataBind();
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDown(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor)
		{
			try
			{
				control.DataSource = fuenteDatos;
				control.DataTextField = columnaVisualizar;
				control.DataValueField = columnaValor;
				control.DataBind();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}

			}
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="columnaOrdenar">Columna de la fuente de datos a utilizar para ordenar los Datos del DropDown</param>
		public void CargarDropDown(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string columnaOrdenar)
		{
			try
			{
				DataView vista = fuenteDatos.DefaultView;
				vista.Sort = columnaOrdenar;
				control.DataSource = vista.Table;
				control.DataTextField = columnaVisualizar;
				control.DataValueField = columnaValor;
				control.DataBind();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}

			}
		}

		/// <summary>
		/// Carga una lista de valores en una columna de un WebCombo
		/// </summary>
		/// <param name="control">WebCombo a cargar</param>
		/// <param name="columna">Nombre de la columna donde se cargarán los datos</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDown(WebCombo control, string columna, DataTable fuenteDatos, string columnaVisualizar, string columnaValor)
		{
			try
			{
				control.Columns.FromKey(columna).Type = ColumnType.DropDownList;

				ValueList valueList = control.Columns.FromKey(columna).ValueList;
				valueList.DataSource = fuenteDatos;
				valueList.DisplayMember = columnaVisualizar;
				valueList.ValueMember = columnaValor;
				valueList.DataBind();
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}

			}
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="descripcionVisualizar">Descripción a visualizar</param>
		public void CargarDropDownConTexto(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string descripcionVisualizar)
		{
			control.DataSource = fuenteDatos;
			control.DataTextField = columnaVisualizar;
			control.DataValueField = columnaValor;
			control.DataBind();
			control.DisplayValue = descripcionVisualizar;
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="datoInsertarVisualizar">Texto a visualizar en la fila insertada</param>
		/// <param name="datoInsertarValor">Datos a almacenar en la fila insertada</param>
		public void CargarDropDown(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string datoInsertarVisualizar, object datoInsertarValor)
		{
			DataRow fila = fuenteDatos.NewRow();
			fila[columnaVisualizar] = datoInsertarVisualizar;
			fila[columnaValor] = datoInsertarValor;
			fuenteDatos.Rows.InsertAt(fila, 0);
			control.DataSource = fuenteDatos;
			control.DataTextField = columnaVisualizar;
			control.DataValueField = columnaValor;
			control.DataBind();
		}

	}
}
