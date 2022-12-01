using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Ajax;
using Seriva.Bolsa.Entidad.Cliente;
using Seriva.Bolsa.Entidad.Estructurales;
using Seriva.Bolsa.Entidad.General;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Bolsa.Negocio.Cliente;
using Seriva.Bolsa.Negocio.Estructurales;
using Seriva.Bolsa.Negocio.General;
using System.Drawing;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Summary description for PaginaBusqueda.
    /// </summary>
    public class PaginaBusqueda : PaginaBase
    {
        //protected string dataJsonResult;
        //protected string documentTypes;

        #region atributos
        protected DataSet BusquedaDataSet;
        int codigo = ConstantesBolsa.TABLA_BUSCARPOR;
        protected ItemTablaDataSet ItemTabla;
        protected ClienteBolsaDataSet clienteBolsaDataSet;
        protected DatosEstructuralesDataSet.SER_TIPOIDENTIFICACIONDataTable datosEstructuralesDTTIPOIDENTIFICACION;
        protected string TituloPagina;
        protected StringBuilder MyScript = new StringBuilder();
        protected TextBox txtRegistro;
        protected Label lblNRegistro;
        protected TextBox txtNumCorrelativo;
        protected TextBox txtNumIDC;
        protected Label lblNumDoc;
        protected DropDownList ddlTipoIDC;
        protected Label lblTipoIDC;
        protected TextBox txtRazonSocial;
        protected Label lblRazonSocial;
        protected TextBox txtApellidoM;
        protected Label lblApellidoM;
        protected TextBox txtApellidoP;
        protected Label lblApellidoP;
        protected Label lblNombre;
        protected TextBox txtCodigoCaval;
        protected Label lblCodigoCaval;
        protected DropDownList ddlBuscar;
        protected Label lblBuscar;
        protected Label lblTitulo;
        protected TextBox txtNombre;
        protected Button btnBuscar;
        protected Label lblGrilla;
        protected String tipoDato;
        protected String mensajeLongitud;
        protected int longitud;
        #endregion

        protected HiddenField uwgResultado2_data;
        protected HiddenField uwgResultado2_rowStates;
        protected HiddenField uwgResultado2_documentTypes;

        protected Seriva.Bolsa.Herramientas.Controles.MessageBox msbError;
        protected System.Web.UI.WebControls.Label lblArchivoNegativo;
        protected bool flagArchivoNegativo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdLongitud;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdTipoDato;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdIDC;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdCorrelativo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdEstadoCaval;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdTipoCliente;
        protected string esBusquedaEmisor;

        public enum TipoBusqueda
        {
            Cliente = 1,
            Contraparte = 2,
            Reportado = 3
        }
        protected string ActualizarPadre;

        private new void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.TituloPagina = this.MensajeAplicacion.ObtenerEtiqueta("PaginaBusqueda.aspx.lblTitulo");
                Utility.RegisterTypeForAjax(typeof(RentaVariable.RentaVariableAjaxMetodos));

                if (Request.QueryString["TipoCliente"] != null)
                {
                    hdTipoCliente.Value = Request.QueryString["TipoCliente"];
                }

                try
                {
                    if (Request.QueryString["BusquedaEmisor"] != null)
                    {
                        esBusquedaEmisor = Convert.ToString(Request.QueryString["BusquedaEmisor"]);
                    }
                    else
                    {
                        esBusquedaEmisor = ConstantesBolsa.NO;
                    }
                    this.flagArchivoNegativo = false;
                }
                catch
                {
                    throw new Exception(this.MensajeAplicacion.ObtenerMensaje("MensajeErrorBusqueda"));
                }
                msbError.Text = null;
                RegistrarJScript();
                cambioDDLTipoIDC();
                if (!Page.IsPostBack)
                {
                    string respuesta = (String)Session["Mensaje"];

                    if (respuesta != null)
                    {
                        if (!respuesta.ToString().Trim().Equals(""))
                        {
                            msbError.Text = respuesta;
                            Session.Remove("Mensaje");
                        }
                    }
                    Session.Remove("Mensaje");
                    CargaDataSets();
                    configurarEtiquetasGenerales();
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
                if (rethrow)
                {
                    msbError.Text = ex.Message;
                }
                else
                {
                    msbError.Text = this.MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
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
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlBuscar.SelectedIndexChanged += new System.EventHandler(this.CambiaBuscar);
            this.ddlTipoIDC.SelectedIndexChanged += new System.EventHandler(this.ddlTipoIDC_SelectedIndexChanged);
            this.btnBuscar.Click += new System.EventHandler(this.BuscarClientes);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region funciones
        private void RegistrarJScript()
        {
            ParametroBolsa parametroBolsa = new ParametroBolsa(CurrentInstanceName);
            txtNumCorrelativo.MaxLength = ConstantesBolsa.LONGITUD_CAMPO_CORRELATIVO_IDC;
            txtCodigoCaval.MaxLength = Convert.ToInt32(parametroBolsa.ObtenerValorParametro(ConstantesBolsa.GEN_NUMERO_CARACTERES_CAVAL));

            this.RegistrarVariableJScript("mensajeZeroResultados", this.MensajeAplicacion.ObtenerMensaje("PaginaBusqueda.ZeroResultados"));
            this.RegistrarVariableJScript("razonsocialInvalida", MensajeAplicacion.ObtenerMensaje("MensajeMinimoRazonSocial") + " " + ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_RAZONSOCIAL);
            this.RegistrarVariableJScript("nombreInvalido", MensajeAplicacion.ObtenerMensaje("MensajeMinimoNombre") + " " + ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_NOMBRE);
            this.RegistrarVariableJScript("apellidoPInvalido", MensajeAplicacion.ObtenerMensaje("MensajeMinimoApellidoP") + " " + ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_APELLIDO);
            this.RegistrarVariableJScript("identificacionNoDatos", MensajeAplicacion.ObtenerMensaje("MensajeNoDatosIdentificacion"));
            this.RegistrarVariableJScript("razonsocialNoDatos", MensajeAplicacion.ObtenerMensaje("MensajeNoDatosRazonSocial"));
            this.RegistrarVariableJScript("nombreapellidoNoDatos", MensajeAplicacion.ObtenerMensaje("MensajeNoDatosNombreApellido"));
            this.RegistrarVariableJScript("registronNoDatos", MensajeAplicacion.ObtenerMensaje("MensajeNoDatosRegistro"));
            this.RegistrarVariableJScript("cavalNoDatos", MensajeAplicacion.ObtenerMensaje("MensajeNoDatosCaval"));
            this.RegistrarVariableJScript("constanteNumeroCaracteresCodCaval", parametroBolsa.ObtenerValorParametro(ConstantesBolsa.GEN_NUMERO_CARACTERES_CAVAL));

            this.txtCodigoCaval.Attributes.Add("onBlur", "formatoNumeroCaval();");
            this.txtCodigoCaval.Attributes.Add("onkeypress", "javascript:verificarFormato();");

            mensajeLongitud = MensajeAplicacion.ObtenerMensaje("mensajeLongitud");
        }
        private void CargaDataSets()
        {   //por default es el natural,,existen varios, como tengo q filtrar asi q crear una vista primero
            datosEstructuralesDTTIPOIDENTIFICACION = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOIDENTIFICACION", 0).SER_TIPOIDENTIFICACION;

            FuncionesGenerales.of_LlenarDropDownList(this.ddlTipoIDC, datosEstructuralesDTTIPOIDENTIFICACION, "CODTIPOIDENTIFICACION", "NOMBRE");
            ItemTabla = new ItemTabla(CurrentInstanceName).ObtenerPorCodigoTabla(codigo);

            if (esBusquedaEmisor == ConstantesBolsa.SI)
            {
                ItemTabla.SER_ITEMTABLA[0].Delete();
                FuncionesGenerales.of_LlenarDropDownList(this.ddlBuscar, ItemTabla.SER_ITEMTABLA, "CODIGOITEM", "NOMBRELARGO", true);
                this.ddlBuscar.SelectedValue = ConstantesBolsa.BUSCARRAZONSOCIAL;
                #region
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = false;
                this.lblApellidoP.Visible = false;
                this.lblApellidoM.Visible = false;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = true;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = false;
                this.txtApellidoP.Visible = false;
                this.txtNombre.Visible = false;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = true;
                this.lblArchivoNegativo.Visible = false;
                this.txtCodigoCaval.Visible = false;

                #endregion
            }
            else
            {
                FuncionesGenerales.of_LlenarDropDownList(this.ddlBuscar, ItemTabla.SER_ITEMTABLA, "CODIGOITEM", "NOMBRELARGO", true);
                this.ddlBuscar.SelectedValue = ConstantesBolsa.BUSCARNOMBRE;
                #region
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = true;
                this.lblApellidoP.Visible = true;
                this.lblApellidoM.Visible = true;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = false;
                this.lblArchivoNegativo.Visible = false;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = true;
                this.txtApellidoP.Visible = true;
                this.txtNombre.Visible = true;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = false;
                this.txtCodigoCaval.Visible = false;
                #endregion
            }
            //llenado por default con vacio
            clienteBolsaDataSet = new ClienteBolsaDataSet();
            //llenarGrilla(this.uwgResultado, clienteBolsaDataSet.SER_CLIENTE);
            //CargarDropDown(this.uwgResultado, datosEstructuralesDTTIPOIDENTIFICACION, "CODTIPOIDENTIFICACION", "NOMBRE", "CODTIPOIDENTIFICACION");

            this.uwgResultado2_data.Value = JsonConvert.SerializeObject(clienteBolsaDataSet.SER_CLIENTE);
            this.uwgResultado2_documentTypes.Value = JsonConvert.SerializeObject(datosEstructuralesDTTIPOIDENTIFICACION);
            this.uwgResultado2_rowStates.Value = JsonConvert.SerializeObject(GetRowNegativeStates(clienteBolsaDataSet.SER_CLIENTE));
        }

        public bool existeEnBolsa(string codigo)
        {
            bool valor = false; //por default pasa
            ClienteBolsaDataSet ClientesBolsa = new ClienteBolsa(CurrentInstanceName).ObtenerClienteBolsa(Convert.ToInt32(codigo));
            if (ClientesBolsa.SER_CLIENTEBOLSA.Rows.Count == 0)
            {
                return valor;
            }
            else
            {
                valor = true;
                return valor;
            }
        }

        private void BuscarClientes(object sender, System.EventArgs e)
        {
            try
            {
                datosEstructuralesDTTIPOIDENTIFICACION = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOIDENTIFICACION", 0).SER_TIPOIDENTIFICACION;

                if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARIDENTIFICACION)
                {
                    string idc = this.ddlTipoIDC.SelectedValue.ToString();
                    string numIDC = this.hdIDC.Value.ToString().ToUpper().Trim();
                    string correlativo = this.hdCorrelativo.Value.ToString().Trim();
                    clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClientesIDC(idc, numIDC, correlativo);
                }

                if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARRAZONSOCIAL) //juridico
                {
                    string razon = this.txtRazonSocial.Text.ToUpper().Trim();
                    clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClientesJURIDICO(razon);
                }
                if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARNOMBRE) //natural //a serivicios
                {
                    string nombre = this.txtNombre.Text.ToUpper().Trim();
                    string apellido = this.txtApellidoP.Text.ToUpper().Trim();
                    string apellidoM = this.txtApellidoM.Text.ToUpper().Trim();
                    clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClientesNatural(nombre, apellido, apellidoM);
                }
                if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARREGISTRO) //natural //a serivicios
                {
                    string numRegistro = this.txtRegistro.Text.ToUpper().Trim();
                    clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClientesBolsaRegistro(numRegistro);
                }
                if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARCAVAL)
                {
                    string estadoCaval = ConstantesBolsa.FILTRO_TODOS;
                    if (hdEstadoCaval.Value == "")
                    {
                        estadoCaval = ConstantesBolsa.ESTADO__ACTIVO;
                    }
                    string numeroCaval = this.txtCodigoCaval.Text.ToUpper().Trim();
                    clienteBolsaDataSet = new ClienteBolsa(CurrentInstanceName).ObtenerClientesBolsaCaval(numeroCaval, estadoCaval);
                    Session.Add("CodigoCavalSeleccionado", numeroCaval);
                }

                //llenarGrilla(this.uwgResultado, clienteBolsaDataSet.SER_CLIENTE);
                //CargarDropDown(this.uwgResultado, datosEstructuralesDTTIPOIDENTIFICACION, "CODTIPOIDENTIFICACION", "NOMBRE", "CODTIPOIDENTIFICACION");

                this.uwgResultado2_data.Value = JsonConvert.SerializeObject(clienteBolsaDataSet.SER_CLIENTE);
                this.uwgResultado2_documentTypes.Value = JsonConvert.SerializeObject(datosEstructuralesDTTIPOIDENTIFICACION);
                this.uwgResultado2_rowStates.Value = JsonConvert.SerializeObject(GetRowNegativeStates(clienteBolsaDataSet.SER_CLIENTE));

                if (clienteBolsaDataSet.SER_CLIENTE.Rows.Count == 0)
                {
                    StringBuilder MyScript = new StringBuilder();
                    MyScript.Append("<script language=\"javascript\">");
                    MyScript.Append("mostrarMensajeZeroResultados()");
                    MyScript.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "mostrarMensaje", MyScript.ToString());
                }
            }
            catch (Exception oException)
            {
                bool rethrow = ExceptionPolicy.HandleException(oException, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
                if (rethrow)
                {
                    msbError.Text = oException.Message;
                }
                else
                {
                    msbError.Text = MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
                }
            }
        }

        private void Cambia()
        {
            if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARREGISTRO)
            {
                this.lblNRegistro.Visible = true;
                this.lblNombre.Visible = false;
                this.lblApellidoP.Visible = false;
                this.lblApellidoM.Visible = false;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = false;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = false;
                this.txtApellidoP.Visible = false;
                this.txtNombre.Visible = false;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = true;
                this.txtRazonSocial.Visible = false;
                this.txtCodigoCaval.Visible = false;
            }
            if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARIDENTIFICACION)
            {
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = false;
                this.lblApellidoP.Visible = false;
                this.lblApellidoM.Visible = false;
                this.lblTipoIDC.Visible = true;
                this.lblNumDoc.Visible = true;
                this.lblRazonSocial.Visible = false;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = false;
                this.txtApellidoP.Visible = false;
                this.txtNombre.Visible = false;
                this.txtNumIDC.Visible = true;
                this.txtNumCorrelativo.Visible = true;
                this.ddlTipoIDC.Visible = true;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = false;
                this.txtCodigoCaval.Visible = false;
                cambioDDLTipoIDC();
            }
            if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARRAZONSOCIAL)
            {
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = false;
                this.lblApellidoP.Visible = false;
                this.lblApellidoM.Visible = false;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = true;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = false;
                this.txtApellidoP.Visible = false;
                this.txtNombre.Visible = false;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = true;
                this.txtCodigoCaval.Visible = false;
            }
            if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARNOMBRE)
            {
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = true;
                this.lblApellidoP.Visible = true;
                this.lblApellidoM.Visible = true;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = false;
                this.lblCodigoCaval.Visible = false;

                this.txtApellidoM.Visible = true;
                this.txtApellidoP.Visible = true;
                this.txtNombre.Visible = true;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = false;
                this.txtCodigoCaval.Visible = false;
            }
            if (this.ddlBuscar.SelectedValue == ConstantesBolsa.BUSCARCAVAL)
            {
                this.lblNRegistro.Visible = false;
                this.lblNombre.Visible = false;
                this.lblApellidoP.Visible = false;
                this.lblApellidoM.Visible = false;
                this.lblTipoIDC.Visible = false;
                this.lblNumDoc.Visible = false;
                this.lblRazonSocial.Visible = false;
                this.lblCodigoCaval.Visible = true;

                this.txtApellidoM.Visible = false;
                this.txtApellidoP.Visible = false;
                this.txtNombre.Visible = false;
                this.txtNumIDC.Visible = false;
                this.txtNumCorrelativo.Visible = false;
                this.ddlTipoIDC.Visible = false;
                this.txtRegistro.Visible = false;
                this.txtRazonSocial.Visible = false;
                this.txtCodigoCaval.Visible = true;
            }
            clienteBolsaDataSet = new ClienteBolsaDataSet();
            //llenarGrilla(this.uwgResultado, clienteBolsaDataSet.SER_CLIENTE);
            
            this.uwgResultado2_data.Value = JsonConvert.SerializeObject(clienteBolsaDataSet.SER_CLIENTE);
            this.uwgResultado2_documentTypes.Value = JsonConvert.SerializeObject(datosEstructuralesDTTIPOIDENTIFICACION);
            this.uwgResultado2_rowStates.Value = JsonConvert.SerializeObject(GetRowNegativeStates(clienteBolsaDataSet.SER_CLIENTE));

            this.txtApellidoM.Text = string.Empty;
            this.txtApellidoP.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtNumIDC.Text = string.Empty;
            this.txtNumCorrelativo.Text = string.Empty;
            this.txtRazonSocial.Text = string.Empty;
            this.txtRegistro.Text = string.Empty;
            this.txtCodigoCaval.Text = string.Empty;
        }
        private void CambiaBuscar(object sender, EventArgs e)
        {
            Cambia();
        }       

        private Dictionary<string, bool> GetRowNegativeStates(DataTable table)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();

            foreach (DataRow currentRow in table.Rows)
            {
                try
                {
                    int codCliente = Convert.ToInt32(currentRow["CODCLIENTE"].ToString());
                    ClienteBolsaDataSet.SER_CLIENTERow filaCliente = this.clienteBolsaDataSet.SER_CLIENTE.FindByCODCLIENTE(codCliente);
                    if (!filaCliente.IsCODSUBTIPOCLIENTENull())
                    {
                        int codSubTipo = filaCliente.CODSUBTIPOCLIENTE;
                        if (codSubTipo == ConstantesBolsa.CLIENTE_NEGATIVO_FUSIONADO_X_ABSORCION ||
                            codSubTipo == ConstantesBolsa.CLIENTE_NEGATIVO ||
                            codSubTipo == ConstantesBolsa.CLIENTES_NEGATIVO_FUSIONADO_X_DISOLUCION
                            )
                        {
                            //e.Row.Style.BackColor = Color.Red;
                            this.flagArchivoNegativo = true;
                            result.Add(currentRow["CODCLIENTE"].ToString(), true);
                        }
                    }

                    if (this.flagArchivoNegativo)
                    {
                        lblArchivoNegativo.Visible = true;
                        lblArchivoNegativo.Text = MensajeAplicacion.ObtenerMensaje("PaginaBusqueda.MensajeArchivoNegativo");
                    }
                }
                catch (Exception oException)
                {
                    bool rethrow = ExceptionPolicy.HandleException(oException, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
                    if (rethrow)
                    {
                        msbError.Text = oException.Message;
                    }
                    else
                    {
                        msbError.Text = this.MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
                    }
                }
            }

            return result;
        } 

        //TODO: REVISAR
        private void uwgResultado_InitializeRow(DataRow e)
        {
            try
            {
                int codCliente = Convert.ToInt32(e["CODCLIENTE"].ToString());
                ClienteBolsaDataSet.SER_CLIENTERow filaCliente = this.clienteBolsaDataSet.SER_CLIENTE.FindByCODCLIENTE(codCliente);
                if (!filaCliente.IsCODSUBTIPOCLIENTENull())
                {
                    int codSubTipo = filaCliente.CODSUBTIPOCLIENTE;
                    if (codSubTipo == ConstantesBolsa.CLIENTE_NEGATIVO_FUSIONADO_X_ABSORCION ||
                        codSubTipo == ConstantesBolsa.CLIENTE_NEGATIVO ||
                        codSubTipo == ConstantesBolsa.CLIENTES_NEGATIVO_FUSIONADO_X_DISOLUCION
                        )
                    {
                        //e.Row.Style.BackColor = Color.Red;
                        this.flagArchivoNegativo = true;
                    }
                }
                if (this.flagArchivoNegativo)
                {
                    lblArchivoNegativo.Visible = true;
                    lblArchivoNegativo.Text = MensajeAplicacion.ObtenerMensaje("PaginaBusqueda.MensajeArchivoNegativo");
                }
            }
            catch (Exception oException)
            {
                bool rethrow = ExceptionPolicy.HandleException(oException, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
                if (rethrow)
                {
                    msbError.Text = oException.Message;
                }
                else
                {
                    msbError.Text = this.MensajeAplicacion.ObtenerMensaje("MensajeErrorPresentacion");
                }
            }
        }

        private void cambioDDLTipoIDC()
        {
            if (ddlTipoIDC.SelectedValue != "")
            {
                if (datosEstructuralesDTTIPOIDENTIFICACION == null)
                    datosEstructuralesDTTIPOIDENTIFICACION = new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales("SER_TIPOIDENTIFICACION", Convert.ToInt32(ddlTipoIDC.SelectedValue)).SER_TIPOIDENTIFICACION;
                longitud = Convert.ToInt32(datosEstructuralesDTTIPOIDENTIFICACION[0]["LONGITUD"]);
                tipoDato = datosEstructuralesDTTIPOIDENTIFICACION[0]["TIPODATO"].ToString();
                txtNumCorrelativo.Text = String.Empty;
                txtNumIDC.Text = String.Empty;
            }
        }

        private void ddlTipoIDC_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cambioDDLTipoIDC();
        }
        #endregion
    }
}
