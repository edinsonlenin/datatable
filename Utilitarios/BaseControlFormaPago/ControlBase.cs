using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;
using Seriva.Utilitarios;
using Seriva.UtilitariosWebII.Controls.ListBox;
using MensajeAplicacion = Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion;

namespace Seriva.Bolsa.Presentacion.Utilitarios.BaseControlFormaPago
{
	/// <summary>
	/// Summary description for ControlBase.
	/// </summary>
	public class ControlBase: UserControl
	{
		public ControlBase()
		{

		}

		#region CurrentInstanceName

		public string CurrentInstanceName
		{
			get
			{
				if(Sesion==null || Sesion.CurrentInstance==null)
				{
					return null;
				}
				else
				{
					return Sesion.CurrentInstance;
				}
			}
		}

		#endregion

		#region Private

		private PaginaBaseDropDowns paginaBaseDropDowns;
		private PaginaBaseDatosEstructurales paginaBaseDatosEstructurales;

		#endregion

		#region Protected

		/// <summary>
		/// Enlace a la clase de Lenguaje
		/// </summary>
		protected internal Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion MensajeAplicacion = new MensajeAplicacion();

		/// <summary>
		/// Enlace a la Sesion Actual
		/// </summary>
		protected internal bs_Sesion sesion;

		public bs_Sesion Sesion
		{
			get
			{
				bs_Sesion valorRetorno=null;
				if(Session[bs_Sesion.ID_Sesion]!=null)
				{
					valorRetorno=(bs_Sesion)Session[bs_Sesion.ID_Sesion];
				}
				return valorRetorno;
			}
		}

		/// <summary>
		/// Método que registra crea una variable javascript y le setea un valor.
		/// </summary>
		/// <param name="nombre">Nombre de la variable a crear.</param>
		/// <param name="valor">Valor que tendrá la variable.</param>
		protected void RegistrarVariableJScript(string nombre, string valor)
		{
			StringBuilder MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("var ");
			MyScript.Append(nombre);
			MyScript.Append("='");
			MyScript.Append(valor);
			MyScript.Append("';");
			MyScript.Append("</script>");
			Page.RegisterStartupScript(nombre,MyScript.ToString());
		}

		protected internal PaginaBaseDatosEstructurales PaginaBaseDatosEstructurales
		{
			get
			{
				if(paginaBaseDatosEstructurales==null)
				{
					paginaBaseDatosEstructurales=new PaginaBaseDatosEstructurales(CurrentInstanceName);
				}
				return paginaBaseDatosEstructurales;
			}
		}

		#endregion

		#region Propiedades

		protected internal PaginaBaseDropDowns PaginaBaseDropDowns
		{
			get
			{
				if(paginaBaseDropDowns==null)
				{
					paginaBaseDropDowns=new PaginaBaseDropDowns();
				}
				return paginaBaseDropDowns;
			}
		}

		#endregion Propiedades

		#region Manejo de DropDowns

		/// <summary>
		/// Carga un SerivaListBox
		/// </summary>
		/// <param name="slbControl">SerivaListBox a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public  void CargarSerivaListBox(SerivaListBox slbControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, bool limpiarValores)
		{
			PaginaBaseDropDowns.CargarSerivaListBox(slbControl,fuenteDatos,columnaVisualizar,columnaValor,limpiarValores);
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
			PaginaBaseDropDowns.CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor, campoVisualizarAInsertar, campoValorAInsertar);
		}

		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor,string columnaOrdenar, string campoVisualizarAInsertar, string campoValorAInsertar)
		{
			PaginaBaseDropDowns.CargarDropDownList(ddlControl,fuenteDatos,columnaVisualizar,columnaValor,columnaOrdenar, campoVisualizarAInsertar, campoValorAInsertar);
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
			PaginaBaseDropDowns.CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor);
		}

		public void CargarDropDownList(DropDownList ddlControl, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string columnaOrdenar)
		{
			PaginaBaseDropDowns.CargarDropDownList(ddlControl,fuenteDatos,columnaVisualizar,columnaValor,columnaOrdenar);
		}

		/// <summary>
		/// Carga un DropDownList
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Arreglo de DataRows con la información deseada</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		public void CargarDropDownList(DropDownList ddlControl, DataRow[] fuenteDatos, string columnaVisualizar, string columnaValor)
		{
			PaginaBaseDropDowns.CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor);
		}

		/// <summary>
		/// Carga un DropDownList insertando una fila
		/// </summary>
		/// <param name="ddlControl">DropDownList a Cargar</param>
		/// <param name="fuenteDatos">Arreglo de DataRows con la información deseada</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="campoVisualizarAInsertar">Valor que se visualizar en la fila insertada</param>
		/// <param name="campoValorAInsertar">Valor que se mostrar en la fila insertada</param>
		public void CargarDropDownList(DropDownList ddlControl, DataRow[] fuenteDatos, string columnaVisualizar, string columnaValor, string campoVisualizarAInsertar, string campoValorAInsertar)
		{
			PaginaBaseDropDowns.CargarDropDownList(ddlControl, fuenteDatos, columnaVisualizar, columnaValor, campoVisualizarAInsertar, campoValorAInsertar);
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
			PaginaBaseDropDowns.CargarDropDown(ultraWebGrid, fuenteDatos, columnaDropDown, columnaVisualizar, columnaValor);
		}

		/// <summary>
		/// Llena un DropDown en una Grilla indicando el band
		/// </summary>
		/// <param name="ultraWebGrid">Grilla a utilizar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		///<param name="banda">Banda que se cargar</param>
		public void CargarDropDown(UltraWebGrid ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor, int banda)
		{
			PaginaBaseDropDowns.CargarDropDown(ultraWebGrid, fuenteDatos, columnaDropDown, columnaVisualizar, columnaValor, banda);
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
			PaginaBaseDropDowns.CargarDropDown(control, fuenteDatos, columnaVisualizar, columnaValor);
		}

		/// <summary>
		/// Carga un WebCombo que no pertenece a un UltraWebGrid insertando una fila
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="datoInsertarVisualizar">Texto a visualizar en la fila insertada</param>
		/// <param name="datoInsertarValor">Datos a almacenar en la fila insertada</param>
		public void CargarDropDown(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string datoInsertarVisualizar, object datoInsertarValor)
		{
			PaginaBaseDropDowns.CargarDropDown(control, fuenteDatos, columnaVisualizar, columnaValor, datoInsertarVisualizar, datoInsertarValor);
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
			PaginaBaseDropDowns.CargarDropDown(control, columna, fuenteDatos, columnaVisualizar, columnaValor);
		}

		#endregion

		#region Manejo de Datos Estructurales

		/// <summary>
		/// Valida los tipos de controles que van a manejar el idioma
		/// </summary>
		/// <param name="tipoControl"></param>
		/// <returns>Tipo de Control</returns>
		private bool ValidarTipoControl(string tipoControl)
		{
			//string[] Tiposcontrol = {"Label", "Button", "HtmlInputButton", "LinkButton", "HyperLink", "CheckBox", "RadioButton", "CompareValidator", "RequiredFieldValidator", "RangeValidator", "RegularExpressionValidator", "CustomValidator", "RadioButtonList"};
			string[] Tiposcontrol = {"Label", "Button", "HtmlInputButton", "LinkButton", "HyperLink", "CheckBox", "RadioButton", "RadioButtonList"};

			if (Array.IndexOf(Tiposcontrol, tipoControl) >= 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Setea el mensaje a visualizar en una propiedad determinóada del control dependiendo el tipo de control
		/// </summary>
		/// <param name="tipoControl">Tipo de Control</param>
		/// <param name="control">Control</param>
		/// <param name="mensaje">Mensaje a visualizar</param>
		private void ColocarMensajeControl(string tipoControl, Control control, string mensaje)
		{
			switch (tipoControl)
			{
				case "Label":
					((Label) control).Text = mensaje;
					break;
				case "Button":
					((Button) control).Text = mensaje;
					break;
				case "LinkButton":
					((LinkButton) control).Text = mensaje;
					break;
				case "HyperLink":
					((HyperLink) control).Text = mensaje;
					break;
				case "CheckBox":
					((CheckBox) control).Text = mensaje;
					break;
				case "RadioButton":
					((RadioButton) control).Text = mensaje;
					break;
				case "RadioButtonList":

					break;
				case "HtmlInputButton":
					((HtmlInputButton) control).Value = mensaje;
					break;
				case "RequiredFieldValidator":
					((RequiredFieldValidator) control).Text = "*";
					((RequiredFieldValidator) control).ToolTip = mensaje;
					((RequiredFieldValidator) control).ErrorMessage = mensaje;
					break;
				case "RangeValidator":
					((RangeValidator) control).Text = "*";
					((RangeValidator) control).ToolTip = mensaje;
					((RangeValidator) control).ErrorMessage = mensaje;
					break;
				case "CompareValidator":
					((CompareValidator) control).Text = "*";
					((CompareValidator) control).ToolTip = mensaje;
					((CompareValidator) control).ErrorMessage = mensaje;
					break;
				case "RegularExpressionValidator":
					((RegularExpressionValidator) control).Text = "*";
					((RegularExpressionValidator) control).ToolTip = mensaje;
					((RegularExpressionValidator) control).ErrorMessage = mensaje;
					break;
				case "CustomValidator":
					((CustomValidator) control).Text = "*";
					((CustomValidator) control).ToolTip = mensaje;
					((CustomValidator) control).ErrorMessage = mensaje;
					break;
			}
		}

		/// <summary>
		/// Recorre los controles de la pagina y setea los mensajes en base a un archivo de recurso
		/// </summary>
		/// <param name="nombrePagina">Nombre de la Pagina</param>
		/// <param name="control">Control</param>
		private void CargaMensajeControles(string nombrePagina, Control control)
		{
			if (control.HasControls())
			{
				foreach (Control control1 in control.Controls)
				{
					if (control1.HasControls())
					{
						CargaMensajeControles(nombrePagina, control1);
					}
					else
					{
						string tipoControl = control1.GetType().Name;

						if (ValidarTipoControl(tipoControl))
						{
							string nombreControl = control1.ID;
							ColocarMensajeControl(tipoControl, control1, MensajeAplicacion.ObtenerEtiqueta(nombrePagina + "." + nombreControl));

						}
					}
				}
			}
		}

		/// <summary>
		/// Obtiene los Datos Estructurales
		/// </summary>
		protected DataSet RecuperarDatosEstructurales()
		{
			return PaginaBaseDatosEstructurales.RecuperarDatosEstructurales();
		}

		#endregion

		#region Manejo de Etiquetas
		/// <summary>
		/// Configura las etiquetas del Header del grid en base a un archivo de recurso
		/// </summary>
		/// <param name="grid"></param>
		protected void ConfigurarEtiquetasUltraWebGrid(UltraWebGrid grid)
		{
			for (int i = 0; i < grid.Bands.Count; i++)
			{
				foreach (UltraGridColumn columna in grid.Bands[i].Columns)
				{
					if (!columna.ServerOnly)
					{
						columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(this.ID + "." + grid.ID + "." + columna.Header.Key);
					}
				}
			}

		}

		/// <summary>
		/// Configura las etiquetas del Header del grid en base a un archivo de recurso
		/// </summary>
		/// <param name="webCombo">WebCombo a configurar</param>
		protected void ConfigurarEtiquetasWebCombo(WebCombo webCombo)
		{
			foreach (UltraGridColumn columna in webCombo.Columns)
			{
				columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(this.ID + "." + webCombo.ID + "." + columna.Header.Caption);
			}

		}

		/// <summary>
		/// Configura las etiquetas de los controles de la página
		/// </summary>
		protected void ConfigurarEtiquetasGenerales()
		{
			if (MensajeAplicacion == null)
			{
				MensajeAplicacion = new MensajeAplicacion();
			}
			CargaMensajeControles(this.ID, this);
		}

		#endregion Manejo de Etiquetas

	}
}
