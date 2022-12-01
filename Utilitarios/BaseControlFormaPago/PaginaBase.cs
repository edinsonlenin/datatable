using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebTab;
using Infragistics.WebUI.WebCombo;
using Seriva.Bolsa.Presentacion.Utilitarios.Controles;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Utilitarios;
using Seriva.UtilitariosWebII.Controls.ListBox;
using Seriva.UtilitariosWebII.ViewState;
using MensajeAplicacion = Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion;

namespace Seriva.Bolsa.Presentacion.Utilitarios.BaseControlFormaPago
{
    /// <summary>
    /// Summary description for BaseWebForm.
    /// </summary>
    public class PaginaBase : Page, IBarraMenu
	{
		#region CurrentInstanceName

		public string CurrentInstanceName
		{
			get
			{
				if(Sesion==null || Sesion.CurrentInstance==null)
				{
					FormsAuthentication.SignOut();
					Session.Abandon();
					Response.Redirect("../Seguridad/Caducidad.aspx",true);
					return null;
				}
				else
				{
					return Sesion.CurrentInstance;
				}
			}
		}

		#endregion

		#region Protected

		/// <summary>
		/// Enlace a la clase de Lenguaje
		/// </summary>
		protected internal MensajeAplicacion MensajeAplicacion = new MensajeAplicacion();

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

		#endregion

		#region Private

		private PaginaBaseManejoError paginaBaseManejoError;
		private PaginaBaseTabs paginaBaseTabs;
		private PaginaBaseDatosEstructurales paginaBaseDatosEstructurales;
		private PaginaBaseDropDowns paginaBaseDropDowns;
		private PaginaBaseGrids paginaBaseGrids;
		private Page pagina;

		#endregion

		#region Propiedades

		/// <summary>
		/// Enlace a la clase de manejo de Tabs
		/// </summary>
		protected internal PaginaBaseTabs PaginaBaseTabs
		{
			get
			{
				if (paginaBaseTabs == null)
				{
					paginaBaseTabs = new PaginaBaseTabs(CurrentInstanceName, this);
				}
				return paginaBaseTabs;
			}
		}

		/// <summary>
		/// Enlace a la clase de Manejo de Errores
		/// </summary>
		protected internal PaginaBaseManejoError PaginaBaseManejoError
		{
			get
			{
				if (paginaBaseManejoError == null)
				{
					paginaBaseManejoError = new PaginaBaseManejoError(this);
				}
				return paginaBaseManejoError;
			}
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

		protected internal PaginaBaseGrids PaginaBaseGrids
		{
			get
			{
				if(paginaBaseGrids==null)
				{
					paginaBaseGrids=new PaginaBaseGrids();
				}
				return paginaBaseGrids;
			}
		}

		#endregion

		#region Page_Load

		/// <summary>
		/// Carga la página
		/// </summary>
		/// <param name="sender">Objeto que genera el evento</param>
		/// <param name="e">Objeto de datos</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (sender != null)
				{
					pagina = (Page) sender;
				}

				if (Session[bs_Sesion.ID_Sesion] == null)
				{
					FormsAuthentication.SignOut();
					Response.Write("<script>window.parent.location.href='../Seguridad/Caducidad.aspx';</script>");
				}

				string barraEstado = "<script>window.status = '';</script>";
				RegisterStartupScript("barraEstado", barraEstado);
			}
			catch
			{
				FormsAuthentication.SignOut();
				Response.Write("<script>window.parent.location.href='../Seguridad/Caducidad.aspx';</script>");
			}
		}

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 09/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 09/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Carga la Página y genera los tabs
		/// </summary>
		/// <param name="sender">Objeto que genera el evento</param>
		/// <param name="e">Objeto de datos</param>
		/// <param name="ultraWebTab">Tab en el que se cargarán los datos</param>
		protected void Page_Load(object sender, EventArgs e, UltraWebTab ultraWebTab)
		{
			if (Session[bs_Sesion.ID_Sesion] == null)
			{
				FormsAuthentication.SignOut();
				Response.Redirect("../Seguridad/Caducidad.aspx", true);
			}
			PaginaBaseTabs.CrearTabUI(ultraWebTab);

			if (!Page.IsPostBack)
			{
				PaginaBaseTabs.GenerarTabDinamico(ultraWebTab, -1, null);
			}
			string barraEstado = "<script>window.status = '';</script>";
			RegisterStartupScript("barraEstado", barraEstado);
		}

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 09/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 09/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Carga la página, genera los Tabs y navega a la página deseada
		/// </summary>
		/// <param name="sender">Objeto que genera el evento</param>
		/// <param name="e">Objeto de datos</param>
		/// <param name="ultraWebTab">Tab en el que se cargarán los datos</param>
		/// <param name="opcion">Opción que se seleccionar</param>
		/// <param name="opciones">Parmetros que se pasarán a la página hija</param>
		protected void Page_Load(object sender, EventArgs e, UltraWebTab ultraWebTab, int opcion, NameValueCollection opciones)
		{
			if (Session[bs_Sesion.ID_Sesion] == null)
			{
				FormsAuthentication.SignOut();
				Response.Redirect("../Seguridad/Caducidad.aspx", true);
			}
			PaginaBaseTabs.CrearTabUI(ultraWebTab);

			if (!Page.IsPostBack)
			{
				StringDictionary cadenas = new StringDictionary();
				foreach (string clave in opciones.Keys)
				{
					//TODO: Constante???
					if (clave != "aumentado")
					{
						cadenas.Add(clave, opciones[clave]);
					}
				}
				PaginaBaseTabs.GenerarTabDinamico(ultraWebTab, opcion, cadenas);
			}
			string barraEstado = "<script>window.status = '';</script>";
			RegisterStartupScript("barraEstado", barraEstado);
		}

		#endregion

		#region Web Form Designer generated code

		/// <summary>
		/// Inicializa la pgina
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Load += new EventHandler(Page_Load);

		}

		#endregion

		#region Manejo de Idioma

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
		/// Obtiene el nombre exacto de la pagina
		/// </summary>
		/// <param name="rutaPagina"></param>
		/// <returns></returns>
		private string ObtenerNombrePagina(string rutaPagina)
		{
			string[] res = rutaPagina.Split('/');
			int i = res.GetUpperBound(0);
			string valor = res[i];
			return valor;
		}

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
						columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(ObtenerNombrePagina(Page.Request.Url.AbsolutePath) + "." + grid.ID + "." + columna.Header.Key);
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
				columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(ObtenerNombrePagina(Page.Request.Url.AbsolutePath) + "." + webCombo.ID + "." + columna.Header.Caption);
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
			CargaMensajeControles(ObtenerNombrePagina(Page.Request.Url.AbsolutePath), this);
		}

		#endregion

		#region Implementation of IBarraMenu

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Impresion()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Grabar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Exportar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Insertar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Ordenar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Desordenar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Agregar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Borrar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Primero()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Ultimo()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Buscar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Filtrar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Ayuda()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Anterior()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Siguiente()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void OcultarFil()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void OcultarCol()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void MostrarFil()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void MostrarCol()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Agrupar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void Desagrupar()
		{
		}

		/// <summary>
		/// Método para IBarraMenu
		/// </summary>
		public void InsertarDetalle()
		{
		}

		#endregion

		#region Manejo de Grids

		/// <summary>
		/// Insertar fila a un ultrawebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		protected void InsertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid)
		{
			PaginaBaseGrids.InsertarFilaUltraWebGrid(ultraWebGrid);
		}

		/// <summary>
		/// Insertar fila a un ultrawebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		/// <param name="band">Band donde insertara</param>
		/// <param name="campos">Campos que se insertarn</param>
		protected void InsertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, int band, Hashtable campos)
		{
			PaginaBaseGrids.InsertarFilaUltraWebGrid(ultraWebGrid, band, campos);
		}

		/// <summary>
		/// Insertar fila a un ultrawebGrid e inicializar algunos valores 
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		/// <param name="campos">Hashtable donde las llaves corresponden al nombre del campo y el value es el valor a asignar</param>
		protected void insertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, Hashtable campos)
		{
			PaginaBaseGrids.insertarFilaUltraWebGrid(ultraWebGrid,campos);
		}

		/*--------------------------------------------------------------------------------------------------------------------------
			CREADO POR				: Luis Marcilla
			FECHA DE CREACION		: 14/12/2005
			MODIFICADO POR			: Luis Marcilla
			FECHA DE MODIFICACION	: 14/12/2005
			REVISADO POR			: Luis Marcilla
			FECHA DE REVISION		: 14/12/2005
		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Inserta una fila en la UltraWebGrid en la pagina correspondiente
		/// </summary>
		/// <param name="grid">UltraWebGrid con paginación al que se le inserta una fila</param>
		/// <param name="fuenteDataSet">Fuente de datos asociado a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		protected void ControlarPaginacionAlInsertar(UltraWebGrid grid, DataSet fuenteDataSet, string nombreTabla )
		{
			PaginaBaseGrids.ControlarPaginacionAlInsertar(grid,fuenteDataSet,nombreTabla);
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		/// <param name="camposClavePrimaria">Arreglo de los campos que son llaves primarias</param>
		protected void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla, string[] camposClavePrimaria)
		{
			PaginaBaseGrids.EliminarFilaUltraWebGrid(ultraWebGrid, dataSet, nombreTabla, camposClavePrimaria);
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		protected void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla)
		{
			PaginaBaseGrids.EliminarFilaUltraWebGrid(ultraWebGrid, dataSet, nombreTabla);
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		/// <param name="llave">Clave primaria de la tabla</param>
		protected void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla, string llave)
		{
			PaginaBaseGrids.EliminarFilaUltraWebGrid(ultraWebGrid, dataSet, nombreTabla, llave);
		}

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTablas">arreglo de nombres de las tablas a actualizar, 0 tabla padre 1 tabla hija</param>
		/// <param name="llaves">arreglo de nombres de los campos PK</param>
		protected void PreparaDatosAGrabarMaestroDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string[] nombreTablas, string[] llaves)
		{
			PaginaBaseGrids.PreparaDatosAGrabarMaestroDetalle(ultraWebGrid, fuenteDatos, nombreTablas, llaves);
		}

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">Nombre de la tabla a actualizar</param>
		/// <param name="llave">Clave primaria de la tabla</param>
		protected void PrepararDatosAGrabar(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave)
		{
			PaginaBaseGrids.PrepararDatosAGrabar(ultraWebGrid, fuenteDatos, nombreTabla, llave, true);
		}

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">Nombre de la tabla a actualizar</param>
		/// <param name="llave">Clave primaria de la tabla</param>
		protected void PrepararDatosAGrabarConColumnasExtras(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave)
		{
			PaginaBaseGrids.PrepararDatosAGrabarConColumnasExtras(ultraWebGrid, fuenteDatos, nombreTabla, llave, true);
		}

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">Clave primaria de la tabla</param>
		/// <param name="comparaCeldas">desea comparar anivel de celdas</param>
		protected void PrepararDatosAGrabar(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, bool comparaCeldas)
		{
			PaginaBaseGrids.PrepararDatosAGrabar(ultraWebGrid, fuenteDatos, nombreTabla, llave, comparaCeldas);
		}

		/*--------------------------------------------------------------------------------------------------------------------------
	  CREADO POR              : Luis Marcilla
	  FECHA DE CREACION       : 08/08/2005
	  MODIFICADO POR          :
	  FECHA DE MODIFICACION   :
	  REVISADO POR            :
	  FECHA DE REVISION       :
	--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">Nombre de la clave primaria de la tabla</param>
		/// <param name="filaPadre">Fila padre del cual se requiere su clave</param>
		/// <param name="llavePadre">Nombre de la clave primaria de la fila padre</param>
		protected void PrepararDatosAGrabarDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, DataRow filaPadre, string llavePadre)
		{
			PaginaBaseGrids.PrepararDatosAGrabarDetalle(ultraWebGrid, fuenteDatos, nombreTabla, llave, true, filaPadre, llavePadre);
		}

		/*--------------------------------------------------------------------------------------------------------------------------
	  CREADO POR              : Luis Marcilla
	  FECHA DE CREACION       : 08/08/2005
	  MODIFICADO POR          :
	  FECHA DE MODIFICACION   :
	  REVISADO POR            :
	  FECHA DE REVISION       :
	--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">Nombre de la clave primaria de la tabla</param>
		/// <param name="comparaCeldas">Si desea comprobar si hay cambios</param>
		/// <param name="filaPadre">Fila padre del cual se requiere su clave</param>
		/// <param name="llavePadre">Nombre de la clave primaria de la fila padre</param>
		protected void PrepararDatosAGrabarDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, bool comparaCeldas, DataRow filaPadre, string llavePadre)
		{
			PaginaBaseGrids.PrepararDatosAGrabarDetalle(ultraWebGrid, fuenteDatos, nombreTabla, llave, comparaCeldas, filaPadre, llavePadre);
		}

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 14/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 14/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar informacin</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		protected void PrepararDatosAGrabarSinIdentity(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla) //, string llave, string valorllave)
		{
			PaginaBaseGrids.PrepararDatosAGrabarSinIdentity(ultraWebGrid, fuenteDatos, nombreTabla, true);
		}

		protected void PrepararDatosAGrabarSinIdentity(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, bool comparaCeldas) //, string llave, string valorllave)
		{
			PaginaBaseGrids.PrepararDatosAGrabarSinIdentity(ultraWebGrid, fuenteDatos, nombreTabla, comparaCeldas);
		}

		#endregion

		#region Manejo de Errores

		/// <summary>
		/// Devuelve la constante que identifica los errores de presentacin
		/// </summary>
		/// <returns></returns>
		protected string ObtenerMensajeErrorPresentacion()
		{
			return PaginaBaseManejoError.ObtenerMensajeErrorPresentacion();
		}

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 14/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 14/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Muestra un mensaje y limpia la pantalla
		/// </summary>
		/// <param name="mensaje">El mensaje a mostrar. Pasar null para no mostrar un mensaje</param>
		protected void LimpiarPantalla(string mensaje)
		{
			PaginaBaseManejoError.LimpiarPantalla(mensaje);
		}

		/// <summary>
		/// Permite Mostar un Mensaje y Cerrar la Pgina Actual
		/// </summary>
		/// <param name="mensaje">Mensaje a Mostrar</param>
		protected internal void CerrarPantalla(string mensaje)
		{
			PaginaBaseManejoError.CerrarPantalla(mensaje);
		}

		#endregion

		#region Manejo de Datos Estructurales

		/// <summary>
		/// Obtiene los Datos Estructurales
		/// </summary>
		protected DataSet RecuperarDatosEstructurales()
		{
			return PaginaBaseDatosEstructurales.RecuperarDatosEstructurales();
		}

		#endregion

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
		/// Carga un WebCombo que no pertenece a un UltraWebGrid
		/// </summary>
		/// <param name="control">WebCombo a Cargar</param>
		/// <param name="fuenteDatos">Fuente de datos a utilizar</param>
		/// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
		/// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
		/// <param name="descripcionVisualizar">Descripción a visualizar</param>
		public void CargarDropDown(WebCombo control, DataTable fuenteDatos, string columnaVisualizar, string columnaValor, string descripcionVisualizar)
		{
			PaginaBaseDropDowns.CargarDropDownConTexto(control, fuenteDatos, columnaVisualizar, columnaValor, descripcionVisualizar);
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

		#region CodigoNegocio

		public int CodigoNegocio
		{
			get
			{
				return Sesion.CodigoNegocio;
			}
		}

		#endregion

		#region CodigoTipoProducto

		public int CodigoTipoProducto
		{
			get
			{
				return ConstantesBolsa.CODIGO_TIPO_PRODUCTO;
			}
		}

		#endregion

		#region Manejo de ViewState

		protected override void SavePageStateToPersistenceMedium(object viewState)
		{
			if ( !new Manager().SavePageStateToPersistenceMedium(pagina, viewState))
			{
				base.SavePageStateToPersistenceMedium(viewState);
			}
		}

		protected object LoadPageStateFromPersistenceMedium(Page paginaDestino)
		{
			object valorRetorno = null;
			valorRetorno = new Manager().LoadPageStateFromPersistenceMedium(paginaDestino);
			if (valorRetorno == null)
			{
				valorRetorno = base.LoadPageStateFromPersistenceMedium();
			}
			return valorRetorno;
		}

		#endregion

		#region Manejo de Estado

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			new Seriva.UtilitariosWebII.Persistence.Manager().SavePageState(pagina);
		}

		protected void CargarEstadoPagina(Page paginaDestino)
		{
			new Seriva.UtilitariosWebII.Persistence.Manager().LoadPageState(paginaDestino);
		}

		#endregion

		#region Manejo Longitud Campos

		public void SetearLongitudControl(TextBox control,int longitud)
		{
		  	control.MaxLength=longitud;
		}

		public void SetearLongitudControl(UltraWebGrid grilla,int banda,string [] nombresColumnas,int []longitudes)
		{
			for(int indice=0;indice<nombresColumnas.Length;indice++)
			{
				grilla.Bands[banda].Columns.FromKey(nombresColumnas[indice]).FieldLen=longitudes[indice];
			}
		}
		#endregion

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
			PaginaBaseDropDowns.CargarDropDownConOrdenamiento(control, fuenteDatos, columnaVisualizar, columnaValor, columnaOrdenar, ordenAscendente);
		}
	}
}
