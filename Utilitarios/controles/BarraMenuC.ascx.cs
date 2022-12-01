using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Seriva.Bolsa.Entidad.Seguridad;
using Seriva.Bolsa.Negocio.Seguridad;
using Seriva.Utilitarios;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{
    /// <summary>
    ///	Summary description for BarraMenuC.
    /// </summary>
    public class BarraMenuC : UserControl
	{
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

		#region Controles

		protected ImageButton btImprimir;
		protected ImageButton btInsertar;
		protected ImageButton btBorrar;
		protected ImageButton btGuardar;
		protected ImageButton btnAnterior;
		protected ImageButton btnSiguiente;
		protected ImageButton btnUndo;
		protected ImageButton btnSalir;
		protected ImageButton Imagebutton1;
		protected ImageButton btnInsertarDetalle;
		protected ImageButton btnAprobar;
		protected ImageButton btnRechazar;
		protected HtmlGenericControl P;
		protected bs_Sesion datosSesion;
		protected string rutaImagenes = string.Empty;

		private bool generarValidacionBotonInsertar = false;
		private bool generarValidacionBotonImprimir = false;
		private bool generarValidacionBotonBorrar = false;
		private bool generarValidacionBotonGuardar = false;
		private bool generarValidacionBotonSiguiente = false;
		private bool generarValidacionBotonSalir = false;
		private bool generarValidacionBotonUndo = false;
		private bool generarValidacionBotonAnterior = false;
		private bool generarValidacionBotonAprobar = false;
		private bool generarValidacionBotonRechazar = false;
		private bool generarValidacionBotonInsertarDetalle = false;
		#endregion Controles

		#region Eventos

		private void Page_Load(object sender, EventArgs e)
		{
			datosSesion = (bs_Sesion) Session[bs_Sesion.ID_Sesion];
			_currentInstanceName=datosSesion.CurrentInstance;
			this.obtenerPathImagenes();
			if (!Page.IsPostBack)
			{
				OcultarBotones();
			}
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Imprimir, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btImprimir_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Imprimir");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Insertar, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btInsertar_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Insertar");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Eliminar, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btBorrar_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Borrar");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Guardar, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btGuardar_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Grabar");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Anterior, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAnterior_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Anterior");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Siguiente, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSiguiente_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Siguiente");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Deshacer, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnUndo_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Undo");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Salir, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSalir_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Salir");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Insertar Detalle, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnInsertarDetalle_Click(object sender, ImageClickEventArgs e)
		{
			Ejecutar_Metodo("InsertarDetalle");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Aprobar, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAprobar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Aprobar");
		}

		/// <summary>
		/// Permite ejecutar la acción del botón Rechazar, llamando a su equivalente
		/// en la página contenedora
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRechazar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Ejecutar_Metodo("Rechazar");
		}
		#endregion Eventos

		#region Métodos

		/// <summary>
		/// Determina si el botón Imprimir desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonImprimir
		{
			set
			{
				generarValidacionBotonImprimir = value;
			}
		}

		/// <summary>
		/// Determina si el botón Imprimir desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonInsertar
		{
			set
			{
				generarValidacionBotonInsertar = value;
			}
		}

		/// <summary>
		/// Determina si el botón Borrar desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonBorrar
		{
			set
			{
				generarValidacionBotonBorrar = value;
			}
		}

		/// <summary>
		/// Determina si el botón Guardar desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonGuardar
		{
			set
			{
				generarValidacionBotonGuardar = value;
			}
		}

		/// <summary>
		/// Determina si el botón Siguiente desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonSiguiente
		{
			set
			{
				generarValidacionBotonSiguiente = value;
			}
		}

		/// <summary>
		/// Determina si el botón Salir desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonSalir
		{
			set
			{
				generarValidacionBotonSalir = value;
			}
		}

		/// <summary>
		/// Determina si el botón Deshacer desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonUndo
		{
			set
			{
				generarValidacionBotonUndo = value;
			}
		}

		/// <summary>
		/// Determina si el botón Anterior desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonAnterior
		{
			set
			{
				generarValidacionBotonAnterior = value;
			}
		}

		/// <summary>
		/// Determina si el botón Insertar detalle desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonInsertarDetalle
		{
			set
			{
				generarValidacionBotonInsertarDetalle = value;
			}
		}

		/// <summary>
		/// Determina si el botón Aprobar desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonAprobar
		{
			set
			{
				generarValidacionBotonAprobar = value;
			}
		}

		/// <summary>
		/// Determina si el botón Rechazar desencadena una validación
		/// </summary>
		public bool GenerarValidacionBotonRechazar
		{
			set
			{
				generarValidacionBotonRechazar = value;
			}
		}

		/// <summary>
		/// Permite ejecutar a través de Reflection un Método declarado en la página contenedora del control
		/// </summary>
		/// <param name="nombre">Nombre del Método a ejecutar</param>
		private void Ejecutar_Metodo(string nombre)
		{
			Type type = Page.GetType();
			if(type!=null)
			{
				MethodInfo mi = type.GetMethod(nombre);
				if(mi!=null)
				{
					mi.Invoke(Page, null);
				}
			}
		}

		/// <summary>
		/// Permite ocultar los botones de acción a los que tiene acceso el usuario de acuerdo
		/// a su perfil de seguridad indicado en la base de datos Estructural
		/// </summary>
		private void OcultarBotones()
		{
			OpcionUsuarioDataSet seguridadDataSet = new OpcionUsuarioDataSet();
			Permisos permisos = new Permisos(CurrentInstanceName);

			string url = Page.Request.RawUrl.ToLower();

			seguridadDataSet = permisos.CargarPermisosUsuarioPagina(url, datosSesion.CodigoPerfilUsuario);

			if (seguridadDataSet == null)
			{
				return;
			}

			Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion mensajeAplicacion = new Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion();

			foreach (DataRow fila in seguridadDataSet.SER_ACCIONPORPERFILUSUARIO.Rows)
			{
				int codigoAccion = Convert.ToInt32(fila["CODACCION"]);
				TipoAccion accion = (TipoAccion) Enum.Parse(typeof (TipoAccion), Enum.GetName(typeof (TipoAccion), codigoAccion));

				switch (accion)
				{
					case TipoAccion.Imprimir:
						btImprimir.Visible = true;
						btImprimir.CausesValidation = generarValidacionBotonImprimir;
						btImprimir.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonImprimir");
						break;
					case TipoAccion.Grabar:
						btGuardar.Visible = true;
						btGuardar.CausesValidation = generarValidacionBotonGuardar;
						btGuardar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonGrabar");
						break;
					case TipoAccion.GuardarComo:
						// TODO : No tiene instrucciones
						break;
					case TipoAccion.Insertar:
						btInsertar.Visible = true;
						btInsertar.CausesValidation = generarValidacionBotonInsertar;
						btInsertar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonInsertar");
						break;
					case TipoAccion.Recuperar:
						break;
					case TipoAccion.Borrar:
						btBorrar.Visible = true;
						btBorrar.CausesValidation = generarValidacionBotonBorrar;
						btBorrar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonBorrar");
						break;
					case TipoAccion.Ordenar:
						break;
					case TipoAccion.Agrupar:
						break;
					case TipoAccion.Buscar:
						break;
					case TipoAccion.Filtrar:
						break;
					case TipoAccion.Anterior:
						btnAnterior.Visible = true;
						btnAnterior.CausesValidation = generarValidacionBotonAnterior;
						btnAnterior.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonAnterior");
						break;
					case TipoAccion.Siguiente:
						btnSiguiente.Visible = true;
						btnSiguiente.CausesValidation = generarValidacionBotonSiguiente;
						btnSiguiente.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonSiguiente");
						break;
					case TipoAccion.Undo:
						btnUndo.Visible = true;
						btnUndo.CausesValidation = generarValidacionBotonUndo;
						btnUndo.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonUndo");
						break;
					case TipoAccion.Salir:
						btnSalir.Visible = true;
						btnSalir.CausesValidation = generarValidacionBotonSalir;
						btnSalir.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonSalir");
						break;
					case TipoAccion.AgregarDetalle:
						btnInsertarDetalle.Visible = true;
						btnInsertarDetalle.CausesValidation = generarValidacionBotonInsertarDetalle;
						btnInsertarDetalle.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonInsertarDetalle");
						break;
					case TipoAccion.Aprobar:
						btnAprobar.Visible = true;
						btnAprobar.CausesValidation = generarValidacionBotonAprobar;
						btnAprobar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonAprobar");
						break;
					case TipoAccion.Rechazar:
						btnRechazar.Visible = true;
						btnRechazar.CausesValidation = generarValidacionBotonRechazar;
						btnRechazar.ToolTip = mensajeAplicacion.ObtenerMensaje("ToolTipBotonRechazar");
						break;
				}
			}
		}

		/// <summary>
		/// Permite obtener la ruta de las imagenes, aún bajo cualquier profundidad de carpetas
		/// </summary>
		private void obtenerPathImagenes()
		{
			string path = Page.Request.Url.AbsolutePath;

			string[] resPath = path.Split('/');

			for(int i = 1; i < resPath.Length - 2; i++)
			{
				rutaImagenes = rutaImagenes + "../";
			}

			rutaImagenes = rutaImagenes + "/Utilitarios/imagenes/";
		}
		#endregion Métodos

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			///this.obtenerPathImagenes();
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
			this.btnInsertarDetalle.Click += new System.Web.UI.ImageClickEventHandler(this.btnInsertarDetalle_Click);
			this.btnAprobar.Click += new System.Web.UI.ImageClickEventHandler(this.btnAprobar_Click);
			this.btnRechazar.Click += new System.Web.UI.ImageClickEventHandler(this.btnRechazar_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion
	}
}
