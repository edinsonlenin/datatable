using System;
using System.Reflection;
using System.Collections;
using Seriva.Bolsa.Herramientas.Constantes;
using System.Text;
using Seriva.Bolsa.Herramientas.Controles;
using Seriva.Utilitarios;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{

    /// <summary>
    ///		Summary description for BarraMenu.
    /// </summary>
    public class BarraMenu : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.ImageButton btInsertar;
		protected System.Web.UI.WebControls.ImageButton btBorrar;
		protected System.Web.UI.WebControls.ImageButton btGuardar;
		protected System.Web.UI.WebControls.ImageButton btImprimir;
		protected System.Web.UI.WebControls.ImageButton btnSiguiente;
		protected System.Web.UI.WebControls.ImageButton btnSalir;
		protected System.Web.UI.WebControls.ImageButton btnUndo;
		protected System.Web.UI.HtmlControls.HtmlGenericControl P1;
		protected System.Web.UI.WebControls.ImageButton btnAnterior;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Prueba;
		protected bs_Sesion datosSesion;
		private bool mostrarBarra = false;

		private bool generarValidacionBotonInsertar = false;
		private bool generarValidacionBotonImprimir = false;
		private bool generarValidacionBotonBorrar = false;
		private bool generarValidacionBotonGuardar = false;
		private bool generarValidacionBotonSiguiente = false;
		private bool generarValidacionBotonSalir = false;
		private bool generarValidacionBotonUndo = false;
		private bool generarValidacionBotonAnterior = false;

		#region CurrentInstanceName

		private string _currentInstanceName;

		public string CurrentInstanceName
		{
			get
			{
				return _currentInstanceName;
			}
		}

		#endregion

		public bool MostrarBarra
		{
			set
			{
				mostrarBarra = value;
			}
		}

		public bool GenerarValidacionBotonImprimir
		{
			set
			{
				generarValidacionBotonImprimir = value;
			}
		}

		public bool GenerarValidacionBotonInsertar
		{
			set
			{
				generarValidacionBotonInsertar = value;
			}
		}

		public bool GenerarValidacionBotonBorrar
		{
			set
			{
				generarValidacionBotonBorrar = value;
			}
		}

		public bool GenerarValidacionBotonGuardar
		{
			set
			{
				generarValidacionBotonGuardar = value;
			}
		}

		public bool GenerarValidacionBotonSiguiente
		{
			set
			{
				generarValidacionBotonSiguiente = value;
			}
		}

		public bool GenerarValidacionBotonSalir
		{
			set
			{
				generarValidacionBotonSalir = value;
			}
		}

		public bool GenerarValidacionBotonUndo
		{
			set
			{
				generarValidacionBotonUndo = value;
			}
		}

		public bool GenerarValidacionBotonAnterior
		{
			set
			{
				generarValidacionBotonAnterior = value;
			}
		}

		string pathImagenes = string.Empty;

		private void obtenerPathImagenes()
		{
			string path = Page.Request.Url.AbsolutePath;

			string[] resPath = path.Split('/');

			for(int i = 1; i < resPath.Length - 2; i++)
			{
				pathImagenes = pathImagenes + "../";
			}

			pathImagenes = pathImagenes + ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.Substring(ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.IndexOf("/",2)+1);

			StringBuilder MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("var pathImagenes='");
			MyScript.Append(pathImagenes);
			MyScript.Append("';");
			MyScript.Append("</script>");
			Page.RegisterStartupScript("var",MyScript.ToString());
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			datosSesion = (bs_Sesion) Session[bs_Sesion.ID_Sesion];
			_currentInstanceName=datosSesion.CurrentInstance;

			if (Page.IsPostBack)
				return;
			OcultarBotones();
			if (mostrarBarra)
			{
				Prueba.Attributes.Add("style","Z-INDEX: 1; BACKGROUND-ATTACHMENT: fixed; LEFT: 2px; BACKGROUND-IMAGE: url(" + pathImagenes + "superbarra.jpg); WIDTH: 734px; BACKGROUND-REPEAT: no-repeat; POSITION: absolute; TOP: 5px; HEIGHT: 29px");
			}
			else
			{
				Prueba.Attributes.Add("style","Z-INDEX: 1; BACKGROUND-ATTACHMENT: fixed; LEFT: 2px; WIDTH: 734px; BACKGROUND-REPEAT: no-repeat; POSITION: absolute; TOP: 5px; HEIGHT: 29px");
			}
		}

		private void Ejecutar_Metodo(string nombre)
		{
			Type type	  = Page.GetType();
			MethodInfo mi = type.GetMethod(nombre);
			mi.Invoke(Page, null);
		}

		private bool ValidarUsuario()
		{
			return true;
		}

		private void OcultarBotones()
		{
			Hashtable Htb = new Hashtable();
			Htb = (Hashtable) this.Session[ConstantesSession.OPCIONES_MENU];
			//Htb = Conversores.convertirDataTableaHashtable((DataTable) this.Session[ConstantesSession.OPCIONES_MENU]);

			if(Htb == null)
				return;

			Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion mensajeAplicacion = new Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion();

			foreach(object key in Htb.Keys)
			{
				switch((OpcionesMenu.opciones)key)
				{
					case OpcionesMenu.opciones.Imprimir :
						btImprimir.Visible = (bool)Htb[key];
						btImprimir.CausesValidation = generarValidacionBotonImprimir;
						btImprimir.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonImprimir");
						break;
					case OpcionesMenu.opciones.Insertar :
						btInsertar.Visible = (bool)Htb[key];
						btInsertar.CausesValidation = generarValidacionBotonInsertar;
						btInsertar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonInsertar");
						break;
					case OpcionesMenu.opciones.Borrar :
						btBorrar.Visible = (bool)Htb[key];
						btBorrar.CausesValidation = generarValidacionBotonBorrar;
						btBorrar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonBorrar");
						break;
					case OpcionesMenu.opciones.Guardar :
						btGuardar.Visible = (bool)Htb[key];
						btGuardar.CausesValidation = generarValidacionBotonGuardar;
						btGuardar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonGuardar");
						break;
					case OpcionesMenu.opciones.Anterior :
						btnAnterior.Visible = (bool)Htb[key];
						btnAnterior.CausesValidation = generarValidacionBotonAnterior;
						btnAnterior.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonAnterior");
						break;
					case OpcionesMenu.opciones.Siguiente :
						btnSiguiente.Visible = (bool)Htb[key];
						btnSiguiente.CausesValidation = generarValidacionBotonSiguiente;
						btnSiguiente.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonSiguiente");
						break;
					case OpcionesMenu.opciones.Deshacer :
						btnUndo.Visible= (bool)Htb[key];
						btnUndo.CausesValidation = generarValidacionBotonUndo;
						btnUndo.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonUndo");
						break;
					case OpcionesMenu.opciones.Salir :
						btnSalir.Visible = (bool)Htb[key];
						btnSalir.CausesValidation = generarValidacionBotonSalir;
						btnSalir.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonSalir");
						break;
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.obtenerPathImagenes();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btImprimir.Click += new System.Web.UI.ImageClickEventHandler(this.btImprimir_Click);
			this.btInsertar.Click += new System.Web.UI.ImageClickEventHandler(this.btInsertar_Click);
			this.btBorrar.Click += new System.Web.UI.ImageClickEventHandler(this.btBorrar_Click);
			this.btGuardar.Click += new System.Web.UI.ImageClickEventHandler(this.btGuardar_Click);
			this.btnAnterior.Click += new System.Web.UI.ImageClickEventHandler(this.btnAnterior_Click);
			this.btnSiguiente.Click += new System.Web.UI.ImageClickEventHandler(this.btnSiguiente_Click);
			this.btnUndo.Click += new System.Web.UI.ImageClickEventHandler(this.btnUndo_Click);
			this.btnSalir.Click += new System.Web.UI.ImageClickEventHandler(this.btnSalir_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btImprimir_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("ImprimirSimple");
		}

		private void btInsertar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("InsertarSimple");
		}

		private void btBorrar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("BorrarSimple");
		}

		private void btGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("GuardarSimple");
		}

		private void btnAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("AnteriorSimple");
		}

		private void btnSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("SiguienteSimple");
		}

		private void btnUndo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("UndoSimple");
		}

		private void btnSalir_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("SalirSimple");
		}

	}
}
