using System;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Seriva.Bolsa.Entidad.Cliente;
using Seriva.Bolsa.Entidad.Estructurales;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Bolsa.Herramientas.Controles;
using Seriva.Bolsa.Negocio.Cliente;
using Seriva.Bolsa.Negocio.Estructurales;
using Seriva.Bolsa.Negocio.General.Comision;
using Seriva.Bolsa.Negocio.RentaFija.MercadoPrimario;
using Seriva.Bolsa.Presentacion.Utilitarios.BaseControlFormaPago;
using Seriva.UtilitariosWebII.Persistence;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{
    public delegate void FormaPagoEventHandler(object sender, FormaPagoEventArgs e);

	/// <summary>
	///	Summary description for FormaPago.
	/// </summary>
	public class FormaPago : ControlBase
	{
		#region Controles Utilizados
		//adolfo
		protected string faltanumeroCuenta;
		protected string  faltanumeroCuentaDestinoSwift;
		protected string  faltaTextoRecibe;

		protected string  fallaIntebancario;
		protected string faltaFormaPago;
		protected string noConfigurada;
		protected string faltanumerocuentaCCI;
		//

		protected Label	lblFormaPago;
		protected HtmlGenericControl hgcCuerpo;
		protected Panel panLBTR;
		protected Label lblBancoLBTR;
		protected Label lblNumeroCuentaLBTR;
		protected Infragistics.WebUI.WebDataInput.WebTextEdit txtNumeroCCI;
		protected System.Web.UI.WebControls.Panel panCargoAbono;
		protected DropDownList wcbCuenta;
		protected System.Web.UI.WebControls.Label lblTipoCuenta;
		protected System.Web.UI.WebControls.TextBox txtTipoCuenta;
		protected System.Web.UI.WebControls.Label lblMoneda;
		protected System.Web.UI.WebControls.TextBox txtMoneda;
		protected System.Web.UI.WebControls.Label lblNombreCuenta;
		protected System.Web.UI.WebControls.TextBox txtNombreCuenta;
		protected System.Web.UI.WebControls.DropDownList ddlBanco;
		protected System.Web.UI.WebControls.DropDownList ddlFormasPago;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCambioEnComboFormaPago;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdFormaPagoSeleccionada;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdTrabajaConValidacionPrevia;
		protected System.Web.UI.WebControls.Label lblCuentaValidacion;
		protected System.Web.UI.WebControls.Label lblCuenta;
		protected System.Web.UI.WebControls.Label lblTextoCuentaValidacion;
		protected System.Web.UI.WebControls.Label lblTipoValidacion;
		protected System.Web.UI.WebControls.Label lblTextoTipoValidacion;
		protected System.Web.UI.WebControls.Label lblMonedaValidacion;
		protected System.Web.UI.WebControls.Label lblTextoMonedaValidacion;
		protected System.Web.UI.WebControls.Label lblNombreCuentaValidacion;
		protected System.Web.UI.WebControls.Label lblTextoNombreCuentaValidacion;
		protected System.Web.UI.WebControls.Panel panCargoAbonoValidacion;
		protected System.Web.UI.WebControls.Label lblBancoLBTRValidacion;
		protected System.Web.UI.WebControls.Label lblTextoBancoLBTRValidacion;
		protected System.Web.UI.WebControls.Label lblNumeroCCI;
		protected System.Web.UI.WebControls.Label lblTextoNumeroCCI;
		protected System.Web.UI.WebControls.Panel panLBTRValidacion;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdEsValidacionPrevia;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hdWcbCuentaSelectedData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdWcbCuentaMonedaSelectedData;
		//		protected string mensajeErrorValidacionCCI;
		//		protected string mensajeErrorLongitudCCI;
		protected string cuentaSeleccionada;
		protected string dataWcbCuenta;
		protected string dataWcbCuentaMoneda;
		protected string dataWcbCuentaTipoCuenta;

		protected bool optionVacio= false;
		#endregion Controles Utilizados

		#region Private
		protected int codigoCliente; //Deja de ser session
		protected int codigoMoneda;
		protected int codigoFormaPagoMoneda;
		protected bool scroll=true;

		[StatefulMember(StateLocation.Session)] protected int codigoProducto;
		[StatefulMember(StateLocation.Session)] protected int codigoTipoOperacionTransaccion;
		[StatefulMember(StateLocation.Session)] protected int codigoCanal;

		[StatefulMember(StateLocation.Session)] protected int codigoTipoFormaPago;
		[StatefulMember(StateLocation.Session)] protected int codigoCuentaMDC;
		[StatefulMember(StateLocation.Session)] protected bool flagValidaConMDC = true;

		//SUBASTA
		//Forma Pago LBTR
		[StatefulMember(StateLocation.Session)] protected int FP_LbtrBanco;
		[StatefulMember(StateLocation.Session)] protected string FP_LbtrNumeroCheque;

		//Forma Pago Cargo/Abono
		[StatefulMember(StateLocation.Session)] protected int FP_CargoAbonoCodCuenta;
		[StatefulMember(StateLocation.Session)] protected int FP_CargoAbonoTipoCuenta;
		[StatefulMember(StateLocation.Session)] protected string FP_CargoAbonoNumeroCuenta;

		//Forma Pago Swift
		[StatefulMember(StateLocation.Session)] protected int FP_SwiftBanco;
		[StatefulMember(StateLocation.Session)] protected int FP_SwiftCodCuenta;
		[StatefulMember(StateLocation.Session)] protected int FP_SwiftTipoCuenta;
		[StatefulMember(StateLocation.Session)] protected string FP_SwiftNumeroCuenta;
		[StatefulMember(StateLocation.Session)] protected string FP_SwiftNumero;
		[StatefulMember(StateLocation.Session)] protected int FP_SwiftTipoMensaje;
		[StatefulMember(StateLocation.Session)] protected int FP_SwiftMonedaCuenta;

		protected string FORMAS_PAGO_ENTIDAD = "FORMAS_PAGO_ENTIDAD";
		//Este tipo de operación no tiene formas de pago configuradas
		protected string mensajeFormasPagoNoConfigurada;
		protected string mensajeExisteOrdenPreviaConCaracteristicasSimilares;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdTipoFormaPagoSeleccionada;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCuentaBancaria;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.CustomValidator csvFormaPago;
		protected System.Web.UI.WebControls.Panel panSwift;
		protected System.Web.UI.WebControls.Label lblCuentaSwift;
		protected System.Web.UI.WebControls.Label lblTipoCuentaSwift;
		protected System.Web.UI.WebControls.Label lblNombreCuentaSwift;
		protected DropDownList wcbCuentaSwift;
		protected System.Web.UI.WebControls.TextBox txtTipoCuentaSwift;
		protected System.Web.UI.WebControls.Label lblMonedaSwift;
		protected System.Web.UI.WebControls.TextBox txtMonedaSwift;
		protected System.Web.UI.WebControls.TextBox txtNombreCuentaSwift;
		protected System.Web.UI.WebControls.Label lblSwiftRecibe;
		protected System.Web.UI.WebControls.TextBox txtSwiftRecibe;
		protected System.Web.UI.WebControls.Label lblCuentaValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTextoCuentaValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTipoValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTextoTipoValidacionSwift;
		protected System.Web.UI.WebControls.Label lblMonedaValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTextoMonedaValidacionSwift;
		protected System.Web.UI.WebControls.Label lblNombreCuentaValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTextoNombreCuentaValidacionSwift;
		protected System.Web.UI.WebControls.Panel panSwiftValidacion;
		protected System.Web.UI.WebControls.Label lblRecibeValidacionSwift;
		protected System.Web.UI.WebControls.Label lblTextoRecibeValidacionSwift;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCodTipoMensajeSwift;
		protected System.Web.UI.WebControls.CustomValidator csvCodigoSwift;
		protected System.Web.UI.WebControls.CustomValidator csvCuentaSwift;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdConfiguracionFormaPagoOK;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdSeleccionoAlguno;
		protected System.Web.UI.WebControls.Panel pnlGeneral;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBanco;
		protected TextBox wteNumeroCheque;
		protected System.Web.UI.WebControls.CustomValidator csvInterbancarios;
		protected System.Web.UI.WebControls.CustomValidator csvNumeroCuenta;
		protected System.Web.UI.WebControls.Button btnRefrescarCuentasSistematics;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIdentificadorControl;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMoverScroll;
		protected Seriva.Bolsa.Herramientas.Controles.MessageBox msbError;
		protected MensajeBoxServerSN msbConfirmar;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdCuentaSeleccionada;
		protected string etiquetaSeleccioneUnaOpcion;

		protected string cuentaDefaultDolar = string.Empty;	//MSY: TA000183741
		protected string cuentaDefaultSoles = string.Empty;	//MSY: TA000183741

		#endregion Private

		#region Propiedades

		public bool esScrollable
		{
			get
			{
				return scroll;
			}
			set
			{
				scroll=value;
			}

		}
		public string Identificador
		{
			get
			{
				return this.hdIdentificadorControl.Value;
			}
			set
			{
				this.hdIdentificadorControl.Value = value;
			}
		}
		/// <summary>
		/// Código del Producto
		/// </summary>
		public int CodigoProducto
		{
			get {
				//if (Convert.ToInt32(wcbFormaPago.DataValue) ==

				return codigoProducto;
			}
			set { codigoProducto = value; }
		}

		/// <summary>
		/// Código del Tipo de Operación
		/// </summary>
		public int CodigoTipoOperacionTransaccion
		{
			get { return codigoTipoOperacionTransaccion; }
			set { codigoTipoOperacionTransaccion = value; }
		}

		/// <summary>
		/// Código del Canal
		/// </summary>
		public int CodigoCanal
		{
			get { return codigoCanal; }
			set { codigoCanal = value; }
		}

		/// <summary>
		/// Código de la Moneda
		/// </summary>
		public int CodigoMoneda
		{
			get
			{
				if (codigoMoneda != 0 && codigoMoneda != int.MinValue) return codigoMoneda;
				else
				{
					string identificador = this.IdentificadorSessionMoneda;
					object codigo = Session[identificador];
					if (codigo == null )codigoMoneda = int.MinValue;
					else codigoMoneda = Convert.ToInt32(codigo);

					return codigoMoneda;
				}
			}
			set
			{
				codigoMoneda          = value;
				string identificador = this.IdentificadorSessionMoneda;
				object codigo = Session[identificador];
				if (codigo == null ) Session.Add(identificador,value);
				else Session[identificador] = value;
			}
		}

		/// <summary>
		/// Código del Cliente
		/// </summary>
		public int CodigoCliente
		{
			get
			{
				if (codigoCliente != 0 && codigoCliente != int.MinValue) return codigoCliente;
				else
				{
					string identificador = this.IdentificadorSessionCliente;
					object codigo = Session[identificador];
					if (codigo == null )codigoCliente = int.MinValue;
					else codigoCliente = Convert.ToInt32(codigo);

					return codigoCliente;
				}
			}
			set
			{
				codigoCliente          = value;
				string identificador = this.IdentificadorSessionCliente;
				object codigo = Session[identificador];
				if (codigo == null ) Session.Add(identificador,value);
				else Session[identificador] = value;
			}
		}

		/// <summary>
		/// Código del Tipo de Forma de Pago
		/// </summary>
		public int CodigoFormaPagoMoneda
		{
			get
			{
				if (codigoFormaPagoMoneda != 0 && codigoFormaPagoMoneda != int.MinValue) return codigoFormaPagoMoneda;
				else
				{
					string identificador = this.IdentificadorSessionTipoFormaPagoMoneda;
					object codigo = Session[identificador];
					if (codigo == null )codigoFormaPagoMoneda = int.MinValue;
					else codigoFormaPagoMoneda = Convert.ToInt32(codigo);

					return codigoFormaPagoMoneda;
				}
			}
			set
			{
				codigoFormaPagoMoneda          = value;
				string identificador = this.IdentificadorSessionTipoFormaPagoMoneda;
				object codigo = Session[identificador];
				if (codigo == null ) Session.Add(identificador,value);
				else Session[identificador] = value;
			}
		}

		/// <summary>
		/// Código de la Forma de Pago Moneda
		/// </summary>
		public int CodigoTipoFormaPago
		{
			get { return codigoTipoFormaPago; }
			set { codigoTipoFormaPago = value; }
		}

		public bool EstadoConfiguracionOK
		{
			get
			{
				if (hdConfiguracionFormaPagoOK.Value == null || hdConfiguracionFormaPagoOK.Value.Equals(ConstantesBolsa.NO ))
				return false;
				else
				return true;
			}

		}

		public int CodigoCuentaMDC
		{
			get { return codigoCuentaMDC; }
			set { codigoCuentaMDC = value; }
		}
		public bool FlagValidaConMDC
		{
			get { return flagValidaConMDC; }
			set { flagValidaConMDC = value; }
		}

		public string IdentificadorSessionDatosFP
		{
			get
			{
				if (this.Identificador == null) Identificador = String.Empty;

				string identificadorSession = ConstantesSession.DATOS_FORMAS_PAGO;
				if (!Identificador.Equals(String.Empty))
					identificadorSession = String.Concat(identificadorSession,"_",Identificador);
				return identificadorSession;
			}
		}
		public string IdentificadorSessionCliente
		{
			get
			{
				if (this.Identificador == null) Identificador = String.Empty;

				string identificadorSession = ConstantesSession.FORMA_PAGO_CODIGO_CLIENTE;
				if (!Identificador.Equals(String.Empty))
					identificadorSession = String.Concat(identificadorSession,"_",Identificador);
				return identificadorSession;
			}
		}
		public string IdentificadorSessionMoneda
		{
			get
			{
				if (this.Identificador == null) Identificador = String.Empty;

				string identificadorSession = ConstantesSession.FORMA_PAGO_CODIGO_MONEDA;
				if (!Identificador.Equals(String.Empty))
					identificadorSession = String.Concat(identificadorSession,"_",Identificador);
				return identificadorSession;
			}
		}
		public string IdentificadorSessionTipoFormaPagoMoneda
		{
			get
			{
				if (this.Identificador == null) Identificador = String.Empty;

				string identificadorSession = ConstantesSession.FORMA_PAGO_CODTIPOFORMAPAGOMONEDA;
				if (!Identificador.Equals(String.Empty))
					identificadorSession = String.Concat(identificadorSession,"_",Identificador);
				return identificadorSession;
			}
		}
		#endregion Propiedades

		public event FormaPagoEventHandler MensajeError;

		protected virtual void OnMensajeErrorEvent(FormaPagoEventArgs e)
		{
			if (MensajeError != null)
				MensajeError(this,e);
		}

		public event EventHandler CargarValorNeto;

		protected virtual void OnCargarValorNetoEvent(EventArgs e)
		{
			if (CargarValorNeto != null)
				CargarValorNeto(this,e);
		}

		//
		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ddlFormasPago.SelectedIndexChanged += new System.EventHandler(this.ddlFormasPago_SelectedIndexChanged);
			//this.wcbCuenta.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wcbCuenta_SelectedRowChanged);
			//this.wcbCuenta.InitializeLayout += new Infragistics.WebUI.WebCombo.InitializeLayoutEventHandler(this.wcbCuenta_InitializeLayout);
			//this.wcbCuenta.InitializeRow += new Infragistics.WebUI.WebCombo.InitializeRowEventHandler(this.wcbCuenta_InitializeRow);
			this.btnRefrescarCuentasSistematics.Click += new System.EventHandler(this.btnRefrescarCuentasSistematics_Click);
			//this.wcbCuentaSwift.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wcbCuentaSwift_SelectedRowChanged);
			//this.wcbCuentaSwift.InitializeLayout += new Infragistics.WebUI.WebCombo.InitializeLayoutEventHandler(this.wcbCuentaSwift_InitializeLayout);
			this.msbConfirmar.OnYesChoosed += new Seriva.Bolsa.Herramientas.Controles.MensajeBoxServerSN.YesChoosed(this.msbConfirmar_OnYesChoosed);
			this.msbConfirmar.OnNoChoosed += new Seriva.Bolsa.Herramientas.Controles.MensajeBoxServerSN.NoChoosed(this.msbConfirmar_OnNoChoosed);
			this.Unload += new System.EventHandler(this.FormaPago_Unload);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.FormaPago_Init);

		}

		#endregion

		#region Eventos

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
//				mensajeErrorValidacionCCI = MensajeAplicacion.ObtenerMensaje("MensajeErrorCuentaCCI");
//				mensajeErrorLongitudCCI = MensajeAplicacion.ObtenerMensaje("MensajeErrorLongitudCCI")
				fallaIntebancario=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancarios");
				this.faltanumerocuentaCCI=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancariosCuentaCCI");
				this.faltaFormaPago=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvFormaPago");
				this.faltanumeroCuenta= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvNumeroCuenta");
				this.faltanumeroCuentaDestinoSwift= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvNumeroCuentaSwift");
				this.faltaTextoRecibe= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvTextoRecibe");
				this.noConfigurada= MensajeAplicacion.ObtenerMensaje("MensajeFormasPagoNoConfigurada");

				hgcCuerpo.Attributes.Clear();
				this.RegistrarJavaScript();
				this.InicializarVariables();
				if(esScrollable)
					hdMoverScroll.Value=ConstantesBolsa.SI;

				if (!this.IsPostBack)
				{
					hdSeleccionoAlguno.Value="N";
					hdConfiguracionFormaPagoOK.Value = ConstantesBolsa.NO;
					FormatoControl formato = new FormatoControl();
					//formato.AplicarFormato(wcbCuenta, Unit.Pixel(520));
					//formato.AplicarFormato(wcbCuenta, Unit.Pixel(420));
					//formato.AplicarFormato(wcbCuentaSwift, Unit.Pixel(520));
					//formato.AplicarFormato(wcbCuentaSwift, Unit.Pixel(420));

					this.lblFormaPago.Text				= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblFormaPago");

					this.lblCuenta.Text					= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblCuenta");
					this.lblNombreCuenta.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNombreCuenta");
					this.lblTipoCuenta.Text				= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblTipoCuenta");
					this.lblMoneda.Text					= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblMoneda");
					this.lblCuentaSwift.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblCuenta");
					this.lblNombreCuentaSwift.Text		= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNombreCuenta");
					this.lblTipoCuentaSwift.Text		= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblTipoCuenta");
					this.lblMonedaSwift.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblMoneda");
					this.lblSwiftRecibe.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblSwiftRecibe");

					this.lblCuentaValidacionSwift.Text					= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblCuenta");
					this.lblNombreCuentaValidacionSwift.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNombreCuenta");
					this.lblTipoValidacionSwift.Text				= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblTipoCuenta");
					this.lblMonedaValidacionSwift.Text					= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblMoneda");
					this.lblRecibeValidacionSwift.Text					= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblSwiftRecibe");

					this.lblBancoLBTR.Text				= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblBancoLBTR");
					this.lblNumeroCuentaLBTR.Text		= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNumeroCuentaLBTR");

					this.lblBancoLBTRValidacion.Text	= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblBancoLBTR");
					this.lblNumeroCCI.Text				= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNumeroCuentaLBTR");

					this.lblCuentaValidacion.Text		= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblCuenta");
					this.lblTipoValidacion.Text			= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblTipoCuenta");
					this.lblNombreCuentaValidacion.Text	= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblNombreCuenta");
					this.lblMonedaValidacion.Text		= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.lblMoneda");

					this.btnRefrescarCuentasSistematics.Text = MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.btnRefrescarCuentasSistematics");
					this.csvInterbancarios.ErrorMessage	= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancarios");
					this.csvNumeroCuenta.ErrorMessage	= MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvNumeroCuenta");
					this.csvFormaPago.ErrorMessage      = MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvFormaPago");
					this.csvCuentaSwift.ErrorMessage    = MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvCuentaSwift");

					if(this.csvCodigoSwift != null)
					{
                        this.csvCodigoSwift.ErrorMessage    = MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvCodigoSwift");
                    }					
				}

				#region Primer Cambio de Seleccion desde que se Cargó el Combo

				//Las opciones del ddlFormasPago se han cargado con AJAX; por este motivo la primera vez se tienen que volver a cargar en el servidor
				//Ya que no se mantienen los valores cargados por javascript
				if (hdCambioEnComboFormaPago.Value == ConstantesBolsa.SI)
				{
					hdCambioEnComboFormaPago.Value = ConstantesBolsa.NO;
					string identificador = this.IdentificadorSessionDatosFP;
					Hashtable datosFormaPago = (Hashtable)Session[identificador];
					int codigoMoneda = Convert.ToInt32(datosFormaPago["CODIGOMONEDA"]);
					int codigoProducto = Convert.ToInt32(datosFormaPago["CODIGOPRODUCTO"]);
					int codigoCanal = Convert.ToInt32(datosFormaPago["CODIGOCANAL"]);
					string tipoOperacion = Convert.ToString(datosFormaPago["TIPOOPERACION"]);
					if (codigoProducto.ToString()==ConstantesBolsa.TIPO_MERCADO_CONTINUO ||
						 codigoProducto.ToString()==ConstantesBolsa.TIPO_MERCADO_DINERO)
						tipoOperacion=tipoOperacion.Substring(0, 3);
					string modalidad = Convert.ToString(datosFormaPago["MODALIDAD"]);
					this.codigoProducto = Convert.ToInt32(datosFormaPago["CODIGOPRODUCTO"]);
					codigoTipoOperacionTransaccion = new Comision(CurrentInstanceName).ObtenerCodigoTransaccion(modalidad, tipoOperacion, codigoProducto);
					DataSet tablaValoresFormaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerListaFormaPago(codigoProducto, Sesion.CodigoEmpresa, codigoTipoOperacionTransaccion, codigoCanal, codigoMoneda);
					ddlFormasPago.DataSource = tablaValoresFormaPago.Tables["SER_FORMAPAGODISPONIBLE"];
					ddlFormasPago.DataTextField = "NOMBRE";
					ddlFormasPago.DataValueField = "CODFORMAPAGOMONEDA";
					ddlFormasPago.DataBind();
					ddlFormasPago.SelectedValue = hdFormaPagoSeleccionada.Value;
					ddlFormasPago.Enabled = true;
					CodigoFormaPagoMoneda = Convert.ToInt32(hdFormaPagoSeleccionada.Value);
					codigoTipoFormaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerTipoFormaPago(CodigoFormaPagoMoneda);
					wcbCuenta.SelectedIndex = -1;
					if (flagValidaConMDC)
					{
						ObtenerCuentaMDCPorProducto();
					}

					if (tipoOperacion == ConstantesBolsa.TIPO_OPERACION_COMPRA)
					{
						ValidarSiMuestraLiquidacionBCR();
					}

					MostrarControlesFormaPago();
				}
				else 
				{
                    CuentasDinerariasDataSet cuentasDinerariasClienteDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerCuentasDinerariasPropiasPorCliente(CodigoCliente.ToString());
                    LoadDataWebCombo(cuentasDinerariasClienteDataSet);
                }

				#endregion Primer Cambio de Seleccion desde que se Cargó el Combo

				#region Si el control fue inicializado con validación previa

				if(hdTrabajaConValidacionPrevia.Value == ConstantesBolsa.SI)
				{
					hdTrabajaConValidacionPrevia.Value = ConstantesBolsa.NO;
					string identificador = this.IdentificadorSessionDatosFP;
					Hashtable datosFormaPago = (Hashtable)Session[identificador];
					int codigoMoneda					= Convert.ToInt32(datosFormaPago["CODIGOMONEDA"]);
					int codigoProducto					= Convert.ToInt32(datosFormaPago["CODIGOPRODUCTO"]);
					int codigoCanal						= Convert.ToInt32(datosFormaPago["CODIGOCANAL"]);
					string tipoOperacion				= Convert.ToString(datosFormaPago["TIPOOPERACION"]);

					string modalidad					= Convert.ToString(datosFormaPago["MODALIDAD"]);

					if (datosFormaPago["FORMAFECHALIQUIDACION"]!=null)
					{
						string formaFechaLiquidacion		= Convert.ToString(datosFormaPago["FORMAFECHALIQUIDACION"]);
						if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA)
						{
							modalidad=formaFechaLiquidacion;
						}
					}

					int codigoValor						= Convert.ToInt32(datosFormaPago["CODIGOVALOR"]);
					int codigoMaestro                   = Convert.ToInt32(datosFormaPago["CODIGOMAESTRO"]);
					string caval						= Convert.ToString(datosFormaPago["CAVAL"]);
					Session.Remove(identificador);

					if(codigoMaestro.Equals(null))
						CargarDatosFijos(codigoProducto, codigoCanal, codigoMoneda, caval, tipoOperacion, codigoValor, modalidad);
					else
						CargarDatosFijos(codigoProducto, codigoCanal, codigoMoneda, caval, tipoOperacion, codigoValor, codigoMaestro, modalidad);
				}

				#endregion Si el control fue inicializado con validación previa

			}catch
			{

			}
		}

		private void LoadDataWebCombo(CuentasDinerariasDataSet cuentasDinerariasClienteDataSet) 
		{
            DatosEstructuralesDataSet datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_MONEDA", 0);

            if (cuentasDinerariasClienteDataSet != null && cuentasDinerariasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"].Rows.Count > 0)
            {
                ClienteBolsaDataSet clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClienteBolsa(CodigoCliente);
                cuentaDefaultDolar = (clienteBolsaDataSet.SER_CLIENTEBOLSA[0].IsCODCUENTADOLARNull())
                                         ? string.Empty
                                         : clienteBolsaDataSet.SER_CLIENTEBOLSA[0].CODCUENTADOLAR.ToString();
                cuentaDefaultSoles = (clienteBolsaDataSet.SER_CLIENTEBOLSA[0].IsCODCUENTASOLESNull())
                                         ? string.Empty
                                         : clienteBolsaDataSet.SER_CLIENTEBOLSA[0].CODCUENTASOLES.ToString();

                dataWcbCuenta = SerializeDatatableForDrowpDownList(cuentasDinerariasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"], "CODCUENTABANCARIACLIENTE", "NOCUENTA");
                dataWcbCuentaMoneda = JsonConvert.SerializeObject(datosEstructuralesDataSet.SER_MONEDA);

				datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOCUENTABANCARIA", 0);
                dataWcbCuentaTipoCuenta = JsonConvert.SerializeObject(datosEstructuralesDataSet.SER_TIPOCUENTABANCARIA);
            }
        }

		private void ValidarSiMuestraLiquidacionBCR()
		{
			try
			{
				HtmlInputHidden hdNombrePagina = (HtmlInputHidden)Parent.FindControl("hdNombrePagina");
				if(hdNombrePagina != null && EsIngresoOIngresoRapido(hdNombrePagina.Value))
				{
					bool ocultarLiquidacionBCR = new ClienteBolsa(CurrentInstanceName).OcultarFormaPagoBCR(CodigoCliente);
					if(ocultarLiquidacionBCR)
					{
						string bcrPen = ConstantesBolsa.TIPO_FORMA_PAGO_LIQUIDACION_BCR_PEN.ToString();
						string bcrUsd = ConstantesBolsa.TIPO_FORMA_PAGO_LIQUIDACION_BCR_USD.ToString();

						for (int i=0; i < ddlFormasPago.Items.Count; i++)
						{
							if(ddlFormasPago.Items[i].Value == bcrPen || ddlFormasPago.Items[i].Value == bcrUsd)
							{
								ddlFormasPago.Items.Remove(ddlFormasPago.Items[i]);
							}
						}
					}
				}
			}
			catch
			{
				//
			}
		}

		private bool EsIngresoOIngresoRapido(string nombrePagina)
		{
			if(nombrePagina == "IngresoOrdenRF" || nombrePagina == "IngresoOrdenRV" || nombrePagina == "IngresoRapidoRV" || nombrePagina == "IngresoOrdenExtrabursatilRV" || nombrePagina == "IngresoRapidoOrdenExtrabursatilRV" || nombrePagina == "IngresoOrdenExtrabursatilRF")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// * Método para inicializar el valor de Cuenta MDC
		/// * Este método lee el valor que tiene seleccionado el control de cuenta MDC de la página padre que contiene
		///   al control de forma de pago.
		/// * El caso de Subastas y Operaciones de Reporte son casos excepcionales por la estructura de sus páginas
		/// * Los demás casos son estándares, el control se llama  wcbCuentaMDC.
		/// * Para las pantallas de ingreso de órdenes de RV se usa un DropDownList llamado ddlCuentaMDC en lugar del WebCombo.
		/// </summary>
		private void ObtenerCuentaMDCPorProducto()
		{
			if(codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_PRIMARIO))
			{
				HtmlInputHidden hdCodCuentaMDC = (HtmlInputHidden)Parent.FindControl("hdCodCuentaMDC");
				codigoCuentaMDC  = Convert.ToInt32(hdCodCuentaMDC.Value);
			}else if(codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_OPERACION_REPORTE))
			{
				//TODO: Revisar casuística cuando sea Mercado Operacion Reporte

				//WebPanel panelPadre = (WebPanel)Parent.FindControl("wpnDatosCliente");
    //            DropDownList comboMDC = (DropDownList)panelPadre.FindControl("wcbCuentaMDC");
				//codigoCuentaMDC = Convert.ToInt32(comboMDC.SelectedValue);
			}else
			{
				bool esMercadoExtrabursatilRV = (codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE));
				bool esNegociacionFisico = false;
				if (codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE))
				{
					esNegociacionFisico = ((DropDownList)Parent.FindControl("ddlModalidad")).SelectedValue == ConstantesBolsa.MODALIDAD_OPERACION_FISICO;
				}
				//Para el mercado extrabursatil y para la modalidad de Físicos no se debe de seleccionar la cuenta MDC
				if(!(esMercadoExtrabursatilRV && esNegociacionFisico))
				{
                    DropDownList comboMDC = (DropDownList)Parent.FindControl("wcbCuentaMDC");
					if(comboMDC != null)
						codigoCuentaMDC = Convert.ToInt32(comboMDC.SelectedValue);
					else
					{
						DropDownList dropdownMDC = (DropDownList)Parent.FindControl("ddlCuentaMDC");
						if(dropdownMDC != null)
							codigoCuentaMDC = Convert.ToInt32(dropdownMDC.SelectedValue);
					}
				}
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormaPago_Init(object sender, EventArgs e)
		{
			new Manager().LoadPageState(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormaPago_Unload(object sender, EventArgs e)
		{
			new Manager().SavePageState(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void wcbCuenta_SelectedRowChanged(object sender, SelectedRowChangedEventArgs e)
		//{
		//	//RefrescarDatosDeCuentaBancariaElegida();
		//	VerificarMonedaDeOrden();
		//}

		/// <summary>
		/// 
		/// </summary>
		protected void wcbCuenta_SelectedRowChanged(object sender, EventArgs e)
        {
			VerificarMonedaDeOrden();
		}
		private void RefrescarDatosDeCuentaBancariaElegida()
		{
			//Cargo los Valores del Tipo y Nombre de Cuenta Seleccionados
			//string tipoCuenta				= Convert.ToString(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("CODTIPOCUENTABANCARIA").Text);
			//string nombreCuenta				= Convert.ToString(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("NOMBRE").Value);
			//string moneda				    = Convert.ToString(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("CODMONEDA").Text);
			//hdCuentaSeleccionada.Value		= wcbCuenta.DataValue.ToString();

			//txtTipoCuenta.Text				= tipoCuenta;
			//txtNombreCuenta.Text			= nombreCuenta;
			//txtMoneda.Text					= moneda;

			////SUBASTA
			//FP_CargoAbonoCodCuenta		= Convert.ToInt32(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("CODCUENTABANCARIACLIENTE").Value);
			//FP_CargoAbonoTipoCuenta	    = Convert.ToInt32(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("CODTIPOCUENTABANCARIA").Value);
			//FP_CargoAbonoNumeroCuenta	= Convert.ToString(wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("NOCUENTA").Value);
			hdConfiguracionFormaPagoOK.Value = ConstantesBolsa.SI;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void wcbCuentaSwift_SelectedRowChanged(object sender, SelectedRowChangedEventArgs e)
		//{
			//Cargo los Valores del Tipo y Nombre de Cuenta Seleccionados
			//string tipoCuenta				= Convert.ToString(wcbCuentaSwift.Rows[wcbCuentaSwift.SelectedIndex].Cells.FromKey("CODTIPOCUENTABANCARIA").Text);
			//string nombreCuenta				= Convert.ToString(wcbCuentaSwift.Rows[wcbCuentaSwift.SelectedIndex].Cells.FromKey("NOMBRE").Value);
			//string moneda				    = Convert.ToString(wcbCuentaSwift.Rows[wcbCuentaSwift.SelectedIndex].Cells.FromKey("CODMONEDA").Text);
			//txtTipoCuentaSwift.Text				= tipoCuenta;
			//txtNombreCuentaSwift.Text			= nombreCuenta;
			//txtMonedaSwift.Text					= moneda;

			//this.FP_SwiftTipoMensaje           = Convert.ToInt32(this.hdCodTipoMensajeSwift.Value);
			//this.FP_SwiftCodCuenta             = Convert.ToInt32(wcbCuentaSwift.SelectedRow.Cells.FromKey("CODCUENTABANCARIACLIENTE").Value);
			//this.FP_SwiftTipoCuenta            = Convert.ToInt32(wcbCuentaSwift.SelectedRow.Cells.FromKey("CODTIPOCUENTABANCARIA").Value);
			//this.FP_SwiftNumeroCuenta          = Convert.ToString(wcbCuentaSwift.SelectedRow.Cells.FromKey("NOCUENTA").Value);
			//this.FP_SwiftBanco                 = Convert.ToInt32(wcbCuentaSwift.SelectedRow.Cells.FromKey("CODBANCO").Value);
			//this.FP_SwiftMonedaCuenta          = Convert.ToInt32(wcbCuentaSwift.SelectedRow.Cells.FromKey("CODMONEDA").Value);

			//DatosEstructuralesDataSet datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_BANCO", this.FP_SwiftBanco);
			//this.FP_SwiftNumero             = datosEstructuralesDataSet.Tables["SER_BANCO"].Select("CODBANCO = "+this.FP_SwiftBanco)[0].ToString();
			//this.FP_SwiftNumero = datosEstructuralesDataSet.SER_BANCO[0][0].ToString();

		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wcbCuentaSwift_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
		{
			//foreach (UltraGridColumn columna in e.Layout.Bands[0].Columns)
			//{
			//	if (columna.BaseColumnName != "NOCUENTA" && columna.BaseColumnName != "CODBANCO" && columna.BaseColumnName != "NOMBRE" && columna.BaseColumnName != "CODMONEDA")
			//	{
			//		columna.ServerOnly = true;
			//	}
			//}
			//ConfigurarEtiquetasWebCombo(wcbCuentaSwift);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ddlFormasPago_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
//				if(hdCambioEnComboFormaPago.Value == null || hdCambioEnComboFormaPago.Value != ConstantesBolsa.NO)
//				{
//					//alert(document.Form1.ucFormaPago_ddlFormasPago.value);
//					hdCambioEnComboFormaPago.Value = ConstantesBolsa.SI;
//					hdFormaPagoSeleccionada.Value = ddlFormasPago.SelectedValue;
//				}
				CodigoFormaPagoMoneda = Convert.ToInt32(ddlFormasPago.SelectedItem.Value);
				hdFormaPagoSeleccionada.Value = ddlFormasPago.SelectedItem.Value;
				codigoTipoFormaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerTipoFormaPago(CodigoFormaPagoMoneda);
				MostrarControlesFormaPago();
			}
			catch(Exception ex)
			{
				MessageBox controlMensaje = (MessageBox)Parent.FindControl("msbError");
				bool rethrow = ExceptionPolicy.HandleException(ex,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if(rethrow)
				{
					controlMensaje.Text = ex.Message;
				}
				else
				{
					controlMensaje.Text = this.MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefrescarCuentasSistematics_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClienteBolsa clienteBolsa = new ClienteBolsa(CurrentInstanceName);

				//Actualizar los datos de las cuentas en el estructural sincronizando con Sistematics, y consulta al estructural la data actualizada
				//CuentasDinerariasDataSet cuentasDinerariasDataSet = clienteBolsa.ObtenerCuentasDinerariasPropiasPorCliente(codigoCliente,Sesion.CodigoUsuario,true);
				CuentasDinerariasDataSet cuentasDinerariasDataSet = clienteBolsa.ObtenerCuentasDinerariasPropiasPorCliente(CodigoCliente,Sesion.CodigoUsuario,true);

				//Obtenemos el código de cuenta seleccionada
				bool esCuentaBancariaSeleccionada = (wcbCuenta.SelectedIndex > -1);
				int codigoCuentaSeleccionada = Convert.ToInt32(wcbCuenta.SelectedValue);

				//Recargar el control wcbCuenta con las cuentas del cliente
				CargarComboCuentasBancarias(cuentasDinerariasDataSet);

				if(esCuentaBancariaSeleccionada)
				{
					//Si aún existe la cuenta antes seleccionada, seleccionarla otra vez
					CuentasDinerariasDataSet.SER_CUENTABANCARIACLIENTERow filaSeleccionada = cuentasDinerariasDataSet.SER_CUENTABANCARIACLIENTE.FindByCODCUENTABANCARIACLIENTE(codigoCuentaSeleccionada);
					if(filaSeleccionada != null)
					{
						//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,filaSeleccionada.CODCUENTABANCARIACLIENTE.ToString());
						VerificarMonedaDeOrden();
						//RefrescarDatosDeCuentaBancariaElegida();
					}
					else
					{
						//Si ya no existe,mostrar el mensaje de seleccionar una

					}
				}
				else
				{
					//Si no estaba cargada una cuenta, seguir con el mensaje de seleccionar una
				}
			}
			catch(Exception ex)
			{
				MessageBox controlMensaje = (MessageBox)Parent.FindControl("msbError");
				bool rethrow = ExceptionPolicy.HandleException(ex,ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if(rethrow)
				{
					controlMensaje.Text = ex.Message;
				}
				else
				{
					controlMensaje.Text = this.MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
				}
			}
		}

		#endregion Eventos

		#region Forma de Pago Cargo Abono

		/// <summary>
		/// Permite Cargar los Datos de la Forma de Pago Cargo Abono
		/// </summary>
		private void CargarDatosCargoAbono()
		{
			CuentasDinerariasDataSet cuentasDinerariasDefaultDataSet = null;

			if(codigoCuentaMDC != 0)
			{
				cuentasDinerariasDefaultDataSet = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerCuentasDinerariasDefault(Sesion.CodigoEmpresa,Sesion.CodigoNegocio,CodigoCliente,codigoCuentaMDC);
			}

			CuentasDinerariasDataSet cuentasDinerariasClienteDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerCuentasDinerariasPropiasPorCliente(CodigoCliente.ToString());
			CargarComboCuentasBancarias(cuentasDinerariasClienteDataSet);

            string cuentaDefault = new ClienteBolsa(CurrentInstanceName).ObtenerCuentaDefaultPorMoneda(CodigoCliente,CodigoMoneda);
			bool cuentaDefaultSeleccionada = false;

			if (cuentasDinerariasDefaultDataSet != null)
			{
				//Si existe una cuenta default para la moneda
				CuentasDinerariasDataSet.SER_CUENTABANCARIACLIENTERow[] cuentasSeleccionadas = (CuentasDinerariasDataSet.SER_CUENTABANCARIACLIENTERow[]) cuentasDinerariasDefaultDataSet.SER_CUENTABANCARIACLIENTE.Select("CODMONEDA = " + CodigoMoneda);
				if(cuentasSeleccionadas.Length == 1)
				{
					if (codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_OPERACION_REPORTE)||
						codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_PRIMARIO)
						)
					{
						//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentasSeleccionadas[0].CODCUENTABANCARIACLIENTE.ToString());
						VerificarMonedaDeOrden();
					//	RefrescarDatosDeCuentaBancariaElegida();
					}
					else
					{
						bool esMercadoExtrabursatilRV = (codigoProducto == Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE));
						bool esNegociacionFisico = false;
						if(esMercadoExtrabursatilRV)
						{
							esNegociacionFisico = ((DropDownList)Parent.FindControl("ddlModalidad")).SelectedValue == ConstantesBolsa.MODALIDAD_OPERACION_FISICO;
						}
						//Para el mercado extrabursatil y para la modalidad de Físicos no se debe de seleccionar la cuenta bancaria default
						if(!(esMercadoExtrabursatilRV && esNegociacionFisico))
						{
						  //FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentasSeleccionadas[0].CODCUENTABANCARIACLIENTE.ToString());
							if(cuentaDefault!=null)
							{
								//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentaDefault);
								wcbCuenta.SelectedValue = cuentaDefault;
								cuentaDefaultSeleccionada = true;
								RefrescarDatosDeCuentaBancariaElegida();
							}
						}
					}
				}
				else
				{
					//	 FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentasSeleccionadas[0].CODCUENTABANCARIACLIENTE.ToString());
					if((codigoProducto== Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA))&& (cuentaDefault!=null))
					{
						//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentaDefault);
						wcbCuenta.SelectedValue = cuentaDefault;
						cuentaDefaultSeleccionada = true;
						RefrescarDatosDeCuentaBancariaElegida();
					}
				}
			}
			else{

				if((codigoProducto== Convert.ToInt32(ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA))&& (cuentaDefault!="null"))
				{
					//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,cuentaDefault);
					wcbCuenta.SelectedValue = cuentaDefault;
					cuentaDefaultSeleccionada = true;
					RefrescarDatosDeCuentaBancariaElegida();
				}
			}

			//if(wcbCuenta.SelectedValue == null)
			if(!cuentaDefaultSeleccionada)
			{
				//wcbCuenta.DisplayValue	= MensajeAplicacion.ObtenerEtiqueta(ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION);

				//StringBuilder MyScript = new StringBuilder();
				//MyScript.Append("<script language=\"javascript\">");
				//MyScript.Append("marcarCuentaVacia();");
				//MyScript.Append("</script>");
				//Page.RegisterStartupScript("marcarCuentaVacia", MyScript.ToString());
				optionVacio = true;

				txtMoneda.Text = String.Empty;
				txtNombreCuenta.Text = String.Empty;
				txtTipoCuenta.Text = String.Empty;
			}
		}

        private string SerializeDatatableForDrowpDownList(DataTable data, string idField, string valueField)
        {
            var idColumn = new DataColumn("id", typeof(string))
            {
                Expression = idField
            };

            var textColumn = new DataColumn("text", typeof(string))
            {
                Expression = valueField
            };

            data.Columns.Add(idColumn);
            data.Columns.Add(textColumn);

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentasClienteDataSet"></param>
        private void CargarComboCuentasBancarias(CuentasDinerariasDataSet cuentasClienteDataSet)
		{
			if(cuentasClienteDataSet!= null && cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"].Rows.Count>0)
			{
				//DatosEstructuralesDataSet datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_MONEDA", 0);

				//				//msy2021 --------------------------------------------------
				//				DataTable dtCuentasDinerariasCliente = cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"].Clone();
				//				dtCuentasDinerariasCliente.Columns.Add("ESDEFAULT", typeof(string));
				//
				//				foreach(DataRow dr in dtCuentasDinerariasCliente.Rows)
				//				{
				//					if(dr["NOCUENTA"].ToString() == String.Empty)
				//					{
				//						dr["ESDEFAULT"] = "X";
				//					}
				//				}
				//
				////				wcbCuenta.DataSource = cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"];
				//				wcbCuenta.DataSource = dtCuentasDinerariasCliente;
				//				//fin:msy2021 --------------------------------------------------

				// DataRow filaOpcion = cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"].NewRow();
				//filaOpcion["CODCUENTABANCARIACLIENTE"] = null;
				//filaOpcion["NOCUENTA"] = "";
				//cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"].Rows.InsertAt(filaOpcion, 0);

				wcbCuenta.DataSource = cuentasClienteDataSet.Tables["SER_CUENTABANCARIACLIENTE"];
				 
				wcbCuenta.DataValueField = "CODCUENTABANCARIACLIENTE";
				wcbCuenta.DataTextField = "NOCUENTA";
				wcbCuenta.DataBind();
				wcbCuenta.SelectedIndex = -1;


				LoadDataWebCombo(cuentasClienteDataSet);

                //CargarDropDown(wcbCuenta, "CODMONEDA", datosEstructuralesDataSet.SER_MONEDA, "NOMBRE", "CODMONEDA");

                //datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOCUENTABANCARIA", 0);
				//CargarDropDown(wcbCuenta, "CODTIPOCUENTABANCARIA", datosEstructuralesDataSet.SER_TIPOCUENTABANCARIA, "NOMBRE", "CODTIPOCUENTABANCARIA");

				//if(wcbCuenta.DataValue == null)
				//{
				//	wcbCuenta.DisplayValue	= MensajeAplicacion.ObtenerEtiqueta(ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION);
				//	txtMoneda.Text = String.Empty;
				//	txtNombreCuenta.Text = String.Empty;
				//	txtTipoCuenta.Text = String.Empty;
				//}
			}

		}

		#endregion Forma de Pago Cargo Abono

		#region Forma de Pago LBTR

		private void CargarDatosLBTR()
		{
			//int codigoBancoLocal = Convert.ToInt32(new ParametroBolsa(CurrentInstanceName).ObtenerValorParametro(ConstantesBolsa.GEN_CODIGO_BANCO_BCP));
			int codigoBancoLocal = Convert.ToInt32(ConstantesBolsa.CODIGO_BANCO_DE_CREDITO);

			DataTable tablaBancos = new DatosEstructurales(CurrentInstanceName).ObtenerBancosLBTR().Tables["SER_BANCO"];
			DataRow[] bancos =  tablaBancos.Select("CODBANCO = " + codigoBancoLocal);
			if(bancos.Length==1)
			{
				tablaBancos.Rows.Remove(bancos[0]);
			}
			FuncionesGenerales.of_LlenarDropDownList(ddlBanco,tablaBancos,"CODBANCO","NOMBRE",true);
		}

		#endregion Forma de Pago LBTR

		#region Forma de Pago Swift

		private void CargarDatosSwift()
		{
			DatosEstructuralesDataSet datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_MONEDA", 0);

			ClienteBolsa clienteBolsa = new ClienteBolsa(CurrentInstanceName);
			int codigoTipoMensajeSwift = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerCodigoMensajePorFormaPago(CodigoFormaPagoMoneda);
			if(codigoTipoMensajeSwift!=-1)
			{
				hdCodTipoMensajeSwift.Value = codigoTipoMensajeSwift.ToString();
			}
			else
			{
				hdCodTipoMensajeSwift.Value = String.Empty;
			}
			DataSet	cuentasCliente		= clienteBolsa.ObtenerCuentasDinerariasOtrosBancosSwift(CodigoCliente,CodigoMoneda);
			string codigoSwiftCliente = clienteBolsa.ObtenerSwiftClienteJuridico(CodigoCliente);
			//TODO No se usará forma pago default
			//DataSet		cuentasCliente		= new Negocio.Cliente.Cliente(CurrentInstanceName).ObtenerCuentasBancariasCliente(codigoCliente, codigoCuentaMDC, codigoBancoLocal);
			//DataRow[] cuentaDefault	= null;  //= new DataRow[2];//cuentasCliente.Tables["SER_CUENTABANCARIACLIENTE"].Select(string.Format("CODCUENTAMDC={0} AND CODEMPRESA={1} AND CODNEGOCIO={2} AND CODMONEDA={3}", codigoCuentaMDC, Sesion.CodigoEmpresa, Sesion.CodigoNegocio, codigoMoneda));

			wcbCuentaSwift.DataSource = cuentasCliente.Tables["SER_CUENTABANCARIACLIENTE"];
			wcbCuentaSwift.DataValueField = "CODCUENTABANCARIACLIENTE";
			wcbCuentaSwift.DataTextField = "NOCUENTA";
			wcbCuentaSwift.DataBind();

			//CargarDropDown(wcbCuentaSwift, "CODMONEDA", datosEstructuralesDataSet.SER_MONEDA, "NOMBRE", "CODMONEDA");

			datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOCUENTABANCARIA", 0);
			//CargarDropDown(wcbCuentaSwift, "CODTIPOCUENTABANCARIA", datosEstructuralesDataSet.SER_TIPOCUENTABANCARIA, "NOMBRE", "CODTIPOCUENTABANCARIA");

			datosEstructuralesDataSet = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_BANCO", 0);
			//CargarDropDown(wcbCuentaSwift, "CODBANCO", datosEstructuralesDataSet.SER_BANCO, "NOMBRE", "CODBANCO");

			if(wcbCuentaSwift.SelectedValue == null)
			{
				//wcbCuentaSwift.DisplayValue	= MensajeAplicacion.ObtenerEtiqueta(ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION);
				txtMoneda.Text = String.Empty;
				txtNombreCuenta.Text = String.Empty;
				txtTipoCuenta.Text = String.Empty;
			}
			txtSwiftRecibe.Text = (codigoSwiftCliente != null)? codigoSwiftCliente : String.Empty;
		}

		#endregion Forma de Pago Swift

		#region Limpiar Valores
		/// <summary>
		/// Permite Limpiar los Valores de las Formas de Pago
		/// </summary>
		public void LimpiarValores()
		{
			codigoTipoFormaPago = ConstantesBolsa.VALOR_NULO_ENTERO;
			ddlFormasPago.Items.Clear();
			ddlFormasPago.Enabled = false;

			wcbCuenta.Items.Clear();
			wcbCuenta.SelectedValue = string.Empty;

			ddlBanco.Items.Clear();
			txtNombreCuenta.Text = string.Empty;
			txtTipoCuenta.Text	= string.Empty;
			wteNumeroCheque.Text = string.Empty;
			int valorNulo	= ConstantesBolsa.VALOR_NULO_ENTERO;
			codigoProducto	= valorNulo;
			codigoTipoOperacionTransaccion	= valorNulo;
			codigoCanal	= valorNulo;
			CodigoMoneda = valorNulo;
			CodigoCliente = valorNulo;
			CodigoFormaPagoMoneda = valorNulo;
			codigoTipoFormaPago	= valorNulo;
			hdCambioEnComboFormaPago.Value = String.Empty;
			hdTrabajaConValidacionPrevia.Value = String.Empty;
			hdFormaPagoSeleccionada.Value = String.Empty;
			hdTrabajaConValidacionPrevia.Value = String.Empty;
			hdEsValidacionPrevia.Value = String.Empty;
			hdConfiguracionFormaPagoOK.Value = ConstantesBolsa.NO;
			codigoCuentaMDC = 0;
			flagValidaConMDC = true;
			MostrarControlesFormaPago();
			Identificador = String.Empty;
			this.CodigoCliente = ConstantesBolsa.VALOR_NULO_ENTERO;
		}

		#endregion Limpiar Valores

		#region Lógica para Mostrar Controles

		/// <summary>
		/// Permite Mostrar los Controles de acuerdo a la Forma de Pago Seleccionada
		/// </summary>
		private void MostrarControlesFormaPago()
		{

			if (codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_ABONO || codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_CARGO)
			{
				//MSY: TA000183741
				ClienteBolsaDataSet clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClienteBolsa(CodigoCliente);
				cuentaDefaultDolar = (clienteBolsaDataSet.SER_CLIENTEBOLSA[0].IsCODCUENTADOLARNull())
				                     	? string.Empty
				                     	: clienteBolsaDataSet.SER_CLIENTEBOLSA[0].CODCUENTADOLAR.ToString();
				cuentaDefaultSoles = (clienteBolsaDataSet.SER_CLIENTEBOLSA[0].IsCODCUENTASOLESNull())
				                     	? string.Empty
				                     	: clienteBolsaDataSet.SER_CLIENTEBOLSA[0].CODCUENTASOLES.ToString();
				//---FIN----
				panCargoAbono.Visible			= true;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
				CargarDatosCargoAbono();
			}
			else if (codigoTipoFormaPago  == ConstantesBolsa.TIPO_FORMA_PAGO_EFECTIVO)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
			}
			else if (codigoTipoFormaPago  == ConstantesBolsa.TIPO_FORMA_PAGO_SWIFT)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= true;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
				CargarDatosSwift();

			}
			else if (codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_CHEQUE)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
			}
			else if(codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_GGTT)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
			}
			else if(codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_LIQUIDACION_BCR)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
			}
			else if(codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_LBTR)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= true;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
				CargarDatosLBTR();
			}
			else if (codigoTipoFormaPago == ConstantesBolsa.VALOR_NULO_ENTERO)
			{
				panCargoAbono.Visible			= false;
				panLBTR.Visible					= false;
				panSwift.Visible				= false;
				panCargoAbonoValidacion.Visible	= true;
				panLBTRValidacion.Visible		= true;
				panSwiftValidacion.Visible		= true;
			}
		}

		#endregion Lógica para Mostrar Controles

		#region Lógica para Proteger Controles

		/// <summary>
		/// Permite Proteger o Desproteger los Controles de acuerdo a la Forma de Pago Seleccionada
		/// </summary>
		/// <param name="protegerControles">'True' protege los Controles y 'False' en caso contrario</param>
		public void ProtegerControlesFormaPago(bool protegerControles)
		{
			ddlFormasPago.Enabled		= false;
			if (codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_ABONO || codigoTipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_CARGO)
			{
				wcbCuenta.Enabled		= false;
			}
		}

		#endregion Lógica para Proteger Controles

		#region Auxiliar

		public void RegistrarJavaScript()
		{
			ddlFormasPago.Attributes.Add("onChange","javascript:actualizarFormaPagoSeleccionada();");
			RegistrarVariableJScript("constanteSI",ConstantesBolsa.SI);
//			RegistrarVariableJScript("mensajeErrorValidacionCCI",mensajeErrorValidacionCCI);
			RegistrarVariableJScript("mensajeErrorValidacionCCI",MensajeAplicacion.ObtenerMensaje("ConfirmarGrabarOrdenRV"));
		}

		public void InicializarVariables()
		{
			this.mensajeFormasPagoNoConfigurada = this.MensajeAplicacion.ObtenerMensaje("MensajeFormasPagoNoConfigurada");
			this.mensajeExisteOrdenPreviaConCaracteristicasSimilares = this.MensajeAplicacion.ObtenerMensaje("MensajeExisteOrdenPreviaConCaracteristicasSimilares");
			this.etiquetaSeleccioneUnaOpcion = this.MensajeAplicacion.ObtenerEtiqueta(ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION);
		}

		#region Cargar Datos Fijos

		#region No Consulta

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string caval, string tipoOperacion, int codigoValor, int codigoMaestro, string modalidad)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,caval,tipoOperacion,codigoValor,codigoMaestro,modalidad,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,null,false,false);
		}

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string caval, string tipoOperacion, int codigoValor, string modalidad)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,caval,tipoOperacion,codigoValor,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,null,false, false);
		}

		#endregion No Consulta

		#region Asumido

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string tipoOperacion, string modalidad)
		{
			throw new Exception(" Deprecated. Cambio en el uso de este método. Correo 2006/10/18 ");
			//this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,String.Empty,tipoOperacion,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,-1,-1,null,false,true);
		}

		public void CargarDatosFijos(int codigoProducto,int codigoOrden,int codigoCanal, int codigoMoneda, string tipoOperacion, string modalidad)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,String.Empty,tipoOperacion,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,codigoOrden,-1,null,false,true);
		}

		#endregion Asumido

		#region Consulta

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string tipoOperacion,string modalidad,int codigoOrden,int formaPagoMoneda)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,String.Empty,tipoOperacion,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,codigoOrden,formaPagoMoneda,null,true, false);
		}

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string tipoOperacion,string modalidad, Hashtable datosFormaPago,int formaPagoMoneda)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,String.Empty,tipoOperacion,ConstantesBolsa.VALOR_NULO_ENTERO,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,ConstantesBolsa.VALOR_NULO_ENTERO,formaPagoMoneda, datosFormaPago,true, false);

		}

		//adolfo
		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string tipoOperacion,string modalidad, Hashtable datosFormaPago,int formaPagoMoneda,string caval,int emision,int maestro,int codOrden)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,caval,tipoOperacion,emision,maestro,modalidad,ConstantesBolsa.VALOR_NULO_ENTERO,formaPagoMoneda, datosFormaPago,true, false);
							   //(int codigoProducto, int codigoCanal, int codigoMoneda, string caval, string tipoOperacion, int codigoValor, int codigoMaestro, string modalidad, int codigoOrden, int formaPagoMonedaSeleccionada, Hashtable datosFormaPago, bool esConsulta, bool esAsumidoPrepago)
		}

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, int codCliente, string tipoOperacion, int codSubasta, string modalidad,int codigoOrden,int formaPagoMoneda)
		{
			this.CargarDatosFijos(codigoProducto,codigoCanal,codigoMoneda,codCliente.ToString(),tipoOperacion,codSubasta,ConstantesBolsa.VALOR_NULO_ENTERO,modalidad,codigoOrden,formaPagoMoneda,null,true, false);

		}

		#endregion Consulta

		#region Principal

		public void CargarDatosFijos(int codigoProducto, int codigoCanal, int codigoMoneda, string caval, string tipoOperacion, int codigoValor, int codigoMaestro, string modalidad, int codigoOrden, int formaPagoMonedaSeleccionada, Hashtable datosFormaPago, bool esConsulta, bool esAsumidoPrepago)
		{
			string  formaFechaLiquidacion = string.Empty;
			string tipoOpFFL = string.Empty;

			if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA)
			{
				formaFechaLiquidacion= modalidad;
				modalidad= ConstantesBolsa.TIPO_MODALIDAD_NORMAL;
			}
			if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_DINERO
				 || codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_CONTINUO  )
			{
				tipoOpFFL = tipoOperacion;
				tipoOperacion=tipoOperacion.Substring(0,3);
				modalidad= ConstantesBolsa.TIPO_MODALIDAD_NORMAL;
			}

			Negocio.General.FormaPago.FormaPago formaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName);
			codigoTipoOperacionTransaccion = new Comision(CurrentInstanceName).ObtenerCodigoTransaccion(modalidad,tipoOperacion,codigoProducto);
			DataSet tablaValoresFormaPago = formaPago.ObtenerListaFormaPago(codigoProducto,Sesion.CodigoEmpresa,codigoTipoOperacionTransaccion,codigoCanal,codigoMoneda);

			ddlFormasPago.DataSource = tablaValoresFormaPago.Tables["SER_FORMAPAGODISPONIBLE"];
			ddlFormasPago.DataTextField = "NOMBRE";
			ddlFormasPago.DataValueField = "CODFORMAPAGOMONEDA";
			ddlFormasPago.DataBind();

			if(esConsulta)
			{
				ddlFormasPago.SelectedValue = formaPagoMonedaSeleccionada.ToString();
				CodigoFormaPagoMoneda = formaPagoMonedaSeleccionada;
				hdEsValidacionPrevia.Value = ConstantesBolsa.SI;
			}
			else
			{
				if(esAsumidoPrepago)
				{
					codigoTipoFormaPago = ConstantesBolsa.TIPO_FORMA_PAGO_ABONO;
					//formaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName);
					CodigoFormaPagoMoneda = formaPago.ObtenerFormaPagoMoneda(codigoTipoFormaPago,codigoMoneda);
					ddlFormasPago.SelectedValue = CodigoFormaPagoMoneda.ToString();
					hdEsValidacionPrevia.Value = ConstantesBolsa.SI;
				}else
				{
					ddlFormasPago.SelectedValue = hdFormaPagoSeleccionada.Value;
					CodigoFormaPagoMoneda = Convert.ToInt32(hdFormaPagoSeleccionada.Value);
				}
			}

			if(!esAsumidoPrepago)
			{
				codigoTipoFormaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerTipoFormaPago(CodigoFormaPagoMoneda);
			}

			wcbCuenta.SelectedIndex = -1;

			string[] cadenas = null;

			#region Carga Textos

//			if(esAsumidoPrepago)
//			{
//				cadenas = formaPago.ObtenerTextosFormaPagoOrdenAsumida(codigoProducto,tipoOperacion,codigoMoneda);
//			}else
//			{
			if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA)
			{
				if(esConsulta || esAsumidoPrepago)
				{
					cadenas = new Seriva.Bolsa.Negocio.RentaVariable.Orden(CurrentInstanceName).ObtenerTextosFormaPagoOrden(CodigoCliente,codigoOrden,esAsumidoPrepago);
				}
				else
				{
					cadenas = new Seriva.Bolsa.Negocio.RentaVariable.Orden(CurrentInstanceName).ObtenerTextosFormaPagoOrdenPrevia(CodigoCliente,caval,tipoOperacion,Convert.ToInt32(codigoValor),modalidad);
				}
			}
			else if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_PRIMARIO)
			{
				//SUBASTA
				if (tipoOperacion == ConstantesBolsa.TIPO_OPERACION_COMPRA)
				{
					if (codigoOrden == ConstantesBolsa.VALOR_NULO_ENTERO) //Seteo de Formas de Pago en misma página de forma temporal antes de guardar definitivamente los datos, los datos se obtienen de datos en session.
					{
						cadenas = new FormaPagoSubasta(CurrentInstanceName).ObtenerTextosFormaPagoTemp(CodigoCliente, codigoMoneda,datosFormaPago);
					}
					else
					{
						int codigoSubasta = codigoValor;
						if(esConsulta) //Orden ya existe y se consulta sus datos previos desde BD
						{
							cadenas = new FormaPagoSubasta(CurrentInstanceName).ObtenerTextosFormaPagoOrden(CodigoCliente,codigoOrden, codigoMoneda);
						}
						else  //los datos de la forma de pago se obtiene de ordenes previa
						{
							cadenas = new FormaPagoSubasta(CurrentInstanceName).ObtenerTextosFormaPagoOrdenPrevia(CodigoCliente,codigoSubasta,codigoMoneda);
						}
					}
				}
				if (tipoOperacion == ConstantesBolsa.TIPO_OPERACION_VENTA)
				{
					int codigoSubasta = codigoOrden;
					if (codigoSubasta != ConstantesBolsa.VALOR_NULO_ENTERO)
						cadenas = new FormaPagoSubasta(CurrentInstanceName).ObtenerTextosFormaPagoEmisor(CodigoCliente,codigoSubasta, codigoMoneda);
					else
						cadenas = new FormaPagoSubasta(CurrentInstanceName).ObtenerTextosFormaPagoTemp(CodigoCliente, codigoMoneda ,datosFormaPago);

				}
			}
			else if((codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_CONTINUO)||(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_DINERO))
			{
				if (codigoOrden!= ConstantesBolsa.VALOR_NULO_ENTERO)
				{
					if(esConsulta || esAsumidoPrepago)
					{
						cadenas = new Seriva.Bolsa.Negocio.RentaFija.MercadoSecundario.OrdenRF(CurrentInstanceName).ObtenerTextosFormaPagoOrden(CodigoCliente,codigoOrden,esAsumidoPrepago);
					}
					else
					{
						cadenas = new Seriva.Bolsa.Negocio.RentaFija.MercadoSecundario.OrdenRF(CurrentInstanceName).ObtenerTextosFormaPagoOrdenPrevia(CodigoCliente,caval,tipoOpFFL,Convert.ToInt32(codigoValor),Convert.ToInt32(codigoMaestro),codigoProducto.ToString());
					}
				}
				else
				{
						//se us el mismo metodo de alonso para obtener los campos guardasos en session para la forma de pago seleccionada
						cadenas = new Seriva.Bolsa.Negocio.RentaFija.MercadoSecundario.OrdenRF(CurrentInstanceName).ObtenerTextosFormaPagoTemp(CodigoCliente, codigoMoneda,datosFormaPago,caval,tipoOpFFL,codigoValor,codigoMaestro,codigoProducto);

				}
			}
			else if((codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE))
			{
				if(esConsulta || esAsumidoPrepago)
				{
					cadenas = new Seriva.Bolsa.Negocio.BackOffice.OrdenExtraBursatil(CurrentInstanceName).ObtenerTextosFormaPagoOrden(CodigoCliente,codigoOrden,esAsumidoPrepago);
				}
				else
				{
					cadenas = new Seriva.Bolsa.Negocio.BackOffice.OrdenExtraBursatil(CurrentInstanceName).ObtenerTextosFormaPagoOrdenPrevia(CodigoCliente,caval,tipoOperacion,Convert.ToInt32(codigoValor),modalidad);
				}
			}
			else if(codigoProducto.ToString() == ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA)
			{
				if(esConsulta || esAsumidoPrepago)
				{
					cadenas = new Seriva.Bolsa.Negocio.RentaFija.MercadoSecundario.OrdenExtrabursatilRF(CurrentInstanceName).ObtenerTextosFormaPagoOrden(CodigoCliente,codigoOrden,esAsumidoPrepago);
				}
				else
				{
					cadenas = new Seriva.Bolsa.Negocio.RentaFija.MercadoSecundario.OrdenExtrabursatilRF(CurrentInstanceName).ObtenerTextosFormaPagoOrdenPrevia(CodigoCliente,caval,tipoOperacion,Convert.ToInt32(codigoValor),Convert.ToInt32(codigoMaestro),formaFechaLiquidacion);
				}

			}

			#endregion Carga Textos

			int tipoFormaPago = Convert.ToInt32(cadenas[1]);
			hdTipoFormaPagoSeleccionada.Value = tipoFormaPago.ToString();
			if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_LBTR)
			{
				panLBTRValidacion.Visible = true;
				ddlFormasPago.Enabled = false;
				lblTextoBancoLBTRValidacion.Text = cadenas[2];
				lblTextoNumeroCCI.Text = cadenas[3];
			}
			else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_CARGO)
			{
				panCargoAbonoValidacion.Visible = true;
				ddlFormasPago.Enabled = false;
				lblTextoTipoValidacion.Text = cadenas[2];
				lblTextoCuentaValidacion.Text = cadenas[3];
				lblTextoNombreCuentaValidacion.Text = cadenas[4];
				lblTextoMonedaValidacion.Text = cadenas[5];
			}
			else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_ABONO)
			{
				panCargoAbonoValidacion.Visible = true;
				ddlFormasPago.Enabled = false;
				lblTextoTipoValidacion.Text = cadenas[2];
				lblTextoCuentaValidacion.Text = cadenas[3];
				lblTextoNombreCuentaValidacion.Text = cadenas[4];
				lblTextoMonedaValidacion.Text = cadenas[5];
			}
			else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_SWIFT)
			{
				panSwiftValidacion.Visible = true;
				ddlFormasPago.Enabled = false;
				lblTextoTipoValidacionSwift.Text = cadenas[2];
				lblTextoCuentaValidacionSwift.Text = cadenas[3];
				lblTextoNombreCuentaValidacionSwift.Text = cadenas[4];
				lblTextoMonedaValidacionSwift.Text = cadenas[5];
				lblTextoRecibeValidacionSwift.Text = cadenas[6];

			}
			else
			{
				ddlFormasPago.Enabled = false;
			}
			panCargoAbono.Visible = false;
			panLBTR.Visible = false;
			panSwift.Visible = false;
		}

		#endregion Principal

		#endregion Cargar Datos Fijos

		#endregion Auxiliar

		#region Obtener Datos de Forma de Pago Seleccionada
		public Hashtable ObtenerFormasPagoConfiguradas()
		{
			Hashtable datosFormaPago = null;
			if (this.EstadoConfiguracionOK)
			{
				datosFormaPago = new Hashtable();
				int codigoFormaPagoMoneda = Convert.ToInt32(ddlFormasPago.SelectedValue);
				int tipoFormaPago = new Negocio.General.FormaPago.FormaPago(CurrentInstanceName).ObtenerTipoFormaPago(codigoFormaPagoMoneda);

				var selectedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(hdWcbCuentaSelectedData.Value);

				datosFormaPago.Add("CODFORMAPAGOMONEDA",codigoFormaPagoMoneda);
				datosFormaPago.Add("CODTIPOFORMAPAGO",tipoFormaPago);

				if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_LBTR)
				{
					datosFormaPago.Add("CODBANCOCLIENTE",Convert.ToInt32(ddlBanco.SelectedValue));
					datosFormaPago.Add("NUMEROCUENTA",wteNumeroCheque.Text);
				}
				else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_CARGO)
				{
					datosFormaPago.Add("CODCUENTABANCARIA", wcbCuenta.SelectedValue);
					datosFormaPago.Add("CODTIPOCUENTABANCARIA", selectedData["CODTIPOCUENTABANCARIA"]);
					datosFormaPago.Add("NUMEROCUENTA", selectedData["NOCUENTA"]);
				}
				else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_ABONO)
				{
					datosFormaPago.Add("CODCUENTABANCARIA", wcbCuenta.SelectedValue);
					datosFormaPago.Add("CODTIPOCUENTABANCARIA", selectedData["CODTIPOCUENTABANCARIA"]);
					datosFormaPago.Add("NUMEROCUENTA", selectedData["NOCUENTA"]);
				}
				else if(tipoFormaPago == ConstantesBolsa.TIPO_FORMA_PAGO_SWIFT)
				{
					//datosFormaPago.Add("CODCUENTABANCARIA",wcbCuentaSwift.DataValue);
					//datosFormaPago.Add("CODTIPOCUENTABANCARIA",wcbCuentaSwift.SelectedRow.Cells.FromKey("CODTIPOCUENTABANCARIA").Value);
					//datosFormaPago.Add("NUMEROCUENTA",wcbCuentaSwift.SelectedRow.Cells.FromKey("NOCUENTA").Value);
					//datosFormaPago.Add("CODMONEDACUENTA",wcbCuentaSwift.SelectedRow.Cells.FromKey("CODMONEDA").Value);
					//datosFormaPago.Add("CODBANCOCLIENTE",wcbCuentaSwift.SelectedRow.Cells.FromKey("CODBANCO").Value);
					datosFormaPago.Add("SWIFTDESTINO",new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales().SER_BANCO.FindByCODBANCO(Convert.ToInt32(datosFormaPago["CODBANCOCLIENTE"])).SWIFT);
					datosFormaPago.Add("CODTIPOMENSAJESWIFT",this.hdCodTipoMensajeSwift.Value);

				}
			}
			return datosFormaPago;
		}
		#endregion

		public void VerificarMonedaDeOrden()
		{
			//Se verifica si la moneda de la orden coincide con la moneda de la cuenta dineraria seleccionada
			//Si no coincide se lanzara un mensaje de confirmación.
			//int codMonedaCuentaBancaria = (int)wcbCuenta.Rows[wcbCuenta.SelectedIndex].Cells.FromKey("CODMONEDA").Value;

			
			//var selectedData = hdWcbCuentaMonedaSelectedData.Value;
			var selectedData = hdWcbCuentaMonedaSelectedData.Value.Trim() == "" ? "0" : hdWcbCuentaMonedaSelectedData.Value;

			int codMonedaCuentaBancaria = int.Parse(selectedData);// wcbCuenta.SelectedValue);
            int codMonedaOrden = CodigoMoneda;

			if (codMonedaCuentaBancaria != codMonedaOrden){
				this.msbConfirmar.MostrarConfirmacion(MensajeAplicacion.ObtenerMensaje("FormaPago.ConfirmarGrabarMoneda"), "Confirmar", true, true);
			}
			else{
				RefrescarDatosDeCuentaBancariaElegida();
			}
		}

		private void msbConfirmar_OnYesChoosed(object sender, string Key)
		{
			if(Key == "Confirmar")
			{
				RefrescarDatosDeCuentaBancariaElegida();
			}
		}

		private void msbConfirmar_OnNoChoosed(object sender, string Key)
		{
			if (txtTipoCuenta.Text != "")
			{
				//FuncionesGenerales.of_SelectItemWc(this.wcbCuenta,hdCuentaSeleccionada.Value);
			}
			else
			{
				wcbCuenta.SelectedIndex = -1;
			    //wcbCuenta.DisplayValue	= MensajeAplicacion.ObtenerEtiqueta(ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION);
			}
		}
	}
}
