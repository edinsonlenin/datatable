<%@ Register TagPrefix="cc1" Namespace="Seriva.Bolsa.Herramientas.Controles" Assembly="Seriva.Bolsa.Herramientas" %>
<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="FormaPago.ascx.cs" Inherits="Seriva.Bolsa.Presentacion.Utilitarios.Controles.FormaPago" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<body id="hgcCuerpo" runat="server">
    

    <div class="row mb-1 mt-1">
        <div class="col-2 TablasTitulos text-end p-1">
            <asp:Label ID="lblFormaPago" runat="server"> Forma Pago</asp:Label>
        </div>
        <div class="col-10 ps-0">
            <asp:DropDownList ID="ddlFormasPago" runat="server" CssClass="texto-negro-peque-mayus" Width="100%" Height="100%"
                AutoPostBack="True" OnSelectedIndexChanged="ddlFormasPago_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CustomValidator ID="csvFormaPago" runat="server" ErrorMessage="*" Display="Dynamic" ClientValidationFunction="ValidaFormaPago"></asp:CustomValidator>
        </div>
    </div>

    <asp:Panel ID="panCargoAbono" runat="server" Width="100%" Visible="False">
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end p-1">
                <asp:Label ID="lblCuenta" runat="server">Cuenta</asp:Label>
            </div>
            <div class="col-10 ps-0">
                <div class="d-flex justify-content-center">
                    <input id="hdWcbCuentaSelectedData" type="hidden" name="hdWcbCuentaSelectedData" runat="server" />
                    <input id="hdWcbCuentaMonedaSelectedData" type="hidden" name="hdWcbCuentaMonedaSelectedData" runat="server" />
                    <asp:DropDownList ID="wcbCuenta" runat="server" Width="100%" OnSelectedIndexChanged="wcbCuenta_SelectedRowChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:CustomValidator ID="csvNumeroCuenta" runat="server" ErrorMessage="*" Display="Dynamic" ClientValidationFunction="ValidaCuentaCargoAbono"></asp:CustomValidator>&nbsp;&nbsp;&nbsp;&nbsp;
				    <asp:Button ID="btnRefrescarCuentasSistematics" runat="server" CssClass="BotonAzul" Width="50px" Text="..." CausesValidation="False"></asp:Button>
                </div>
            </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end p-1">
                <asp:Label ID="lblTipoCuenta" runat="server">Tipo</asp:Label>
            </div>
            <div class="col-4 ps-0">
                <asp:TextBox ID="txtTipoCuenta" runat="server" CssClass="texto-negro-peque-FondoGris2" Width="100%"
                    Enabled="False"></asp:TextBox>
            </div>
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblMoneda" runat="server">Moneda</asp:Label>
            </div>
            <div class="col-4">
                <asp:TextBox ID="txtMoneda" runat="server" CssClass="texto-negro-peque-FondoGris2" Width="75px"
                    Enabled="False" Wrap="False" MaxLength="10" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-2 TablasTitulos text-end p-1">
                <asp:Label ID="lblNombreCuenta" runat="server">Nombre</asp:Label>
            </div>
            <div class="col-10 ps-0">
                <asp:TextBox ID="txtNombreCuenta" runat="server" CssClass="texto-negro-peque-FondoGris2" Width="100%"
                    Enabled="False"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panCargoAbonoValidacion" runat="server" Width="100%" Visible="False">
        <div class="row">
            <div class="col-2 TablasTitulos text-end ps-1">
                 <asp:Label ID="lblCuentaValidacion" runat="server"></asp:Label>
            </div>
            <div class="col-2 TablasTitulos text-start ps-0">
                <asp:Label ID="lblTextoCuentaValidacion" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
             <div class="col-2 TablasTitulos text-end ps-1">
                 <asp:Label ID="lblTipoValidacion" runat="server">Tipo</asp:Label>
             </div>
            <div class="col-4 TablasTitulos text-start ps-0">
                <asp:Label ID="lblTextoTipoValidacion" runat="server"></asp:Label>
            </div>
            <div class="col-1 TablasTitulos text-end ps-1">
                <asp:Label ID="lblMonedaValidacion" runat="server">Moneda</asp:Label>
            </div>
            <div class="col-4 TablasTitulos text-start ps-0">
                <asp:Label ID="lblTextoMonedaValidacion" runat="server">Moneda</asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-2 TablasTitulos text-end ps-1">
                <asp:Label ID="lblNombreCuentaValidacion" runat="server">Nombre</asp:Label>
            </div>
             <div class="col-10 TablasTitulos text-start ps-0">
                 <asp:Label ID="lblTextoNombreCuentaValidacion" runat="server">Nombre</asp:Label>
             </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panLBTR" runat="server" Width="100%" Visible="False">
        <div class="row mb-1">
             <div class="col-2 TablasTitulos text-end p-1">
                 <asp:Label ID="lblBancoLBTR" runat="server">Banco</asp:Label>
             </div>
             <div class="col-10 ps-0"> 
                 <asp:DropDownList ID="ddlBanco" runat="server" Width="100%" CssClass="texto-negro-peque-mayus"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvBanco" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="ddlBanco"></asp:RequiredFieldValidator>
             </div>
         </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end p-1">
                 <asp:Label ID="lblNumeroCuentaLBTR" runat="server">Número CCI</asp:Label>
            </div>
            <div class="col-10 ps-0">
                <asp:TextBox ID="wteNumeroCheque" runat="server" Width="100%" CssClass="texto-negro-peque-mayus"></asp:TextBox>
                <asp:CustomValidator ID="csvInterbancarios" runat="server" ClientValidationFunction="ValidaInterbancarios"
                        Display="Dynamic" ErrorMessage="*"></asp:CustomValidator>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panSwift" runat="server" Width="100%" Visible="False">
        <div class="row mb-1">
             <div class="col-2 TablasTitulos text-end">
                 <asp:Label ID="lblCuentaSwift" runat="server">Cuenta</asp:Label>
             </div>
             <div class="col-10">
                 <asp:DropDownList ID="wcbCuentaSwift" runat="server" Width="100%"></asp:DropDownList>
                 <asp:CustomValidator ID="csvCuentaSwift" runat="server" ErrorMessage="*" Display="Dynamic" ClientValidationFunction="ValidaCuentaSwift"></asp:CustomValidator>
             </div>
         </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                 <asp:Label ID="lblTipoCuentaSwift" runat="server">Tipo</asp:Label>
            </div>
            <div class="col-4">
                <asp:TextBox ID="txtTipoCuentaSwift" runat="server" Width="100%" CssClass="texto-negro-peque-FondoGris2"
                        Enabled="False"></asp:TextBox>
            </div>
             <div class="col-2 TablasTitulos text-end">
                 <asp:Label ID="lblMonedaSwift" runat="server">Moneda</asp:Label>
             </div>
            <div class="col-4">
                <asp:TextBox ID="txtMonedaSwift" runat="server" Width="100%" CssClass="texto-negro-peque-FondoGris2"
                        Enabled="False" ReadOnly="True" MaxLength="10" Wrap="False"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblNombreCuentaSwift" runat="server">Nombre</asp:Label>
            </div>
            <div class="col-10">
                <asp:TextBox ID="txtNombreCuentaSwift" runat="server" Width="100%" CssClass="texto-negro-peque-FondoGris2"
                        Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblSwiftRecibe" runat="server">Swift Recibe</asp:Label>
            </div>
            <div class="col-10">
                <asp:TextBox ID="txtSwiftRecibe" runat="server" Width="100%" CssClass="texto-negro-peque-FondoGris2"
                        Enabled="False"></asp:TextBox>
                <input id="hdCodTipoMensajeSwift" type="hidden" name="hdCodTipoMensajeSwift" runat="server">
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panSwiftValidacion" runat="server" Width="100%" Visible="False">
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblCuentaValidacionSwift" runat="server"></asp:Label>
            </div>
            <div class="col-4">
                <asp:Label ID="lblTextoCuentaValidacionSwift" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblTipoValidacionSwift" runat="server">Tipo</asp:Label>
            </div>
            <div class="col-4">
                <asp:Label ID="lblTextoTipoValidacionSwift" runat="server"></asp:Label>
            </div>
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblMonedaValidacionSwift" runat="server">Moneda</asp:Label>
            </div>
             <div class="col-4">
                 <asp:Label ID="lblTextoMonedaValidacionSwift" runat="server">Moneda</asp:Label>
             </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblNombreCuentaValidacionSwift" runat="server">Nombre</asp:Label>
            </div>
            <div class="col-4">
                <asp:Label ID="lblTextoNombreCuentaValidacionSwift" runat="server">Nombre</asp:Label>
            </div>
            <div class="col-2 TablasTitulos text-end">
                <asp:Label ID="lblRecibeValidacionSwift" runat="server">Swift Recibe</asp:Label>
            </div>
            <div class="col-4">
                <asp:Label ID="lblTextoRecibeValidacionSwift" runat="server">Swift Recibe</asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panLBTRValidacion" runat="server" Width="100%" Visible="False">
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end p-1">
                 <asp:Label ID="lblBancoLBTRValidacion" runat="server">Banco</asp:Label>
            </div>
            <div class="col-10 TablasTitulos text-start ps-0">
                <asp:Label ID="lblTextoBancoLBTRValidacion" runat="server">Banco</asp:Label>
            </div>
        </div>
        <div class="row mb-1">
            <div class="col-2 TablasTitulos text-end p-1">
                <asp:Label ID="lblNumeroCCI" runat="server">Número CCI</asp:Label>
            </div>
            <div class="col-10 TablasTitulos text-start ps-0">
                <asp:Label ID="lblTextoNumeroCCI" runat="server">Número CCI</asp:Label>
            </div>
        </div>        
    </asp:Panel>

    
    <input id="hdCambioEnComboFormaPago" type="hidden" name="hdCambioEnComboFormaPago" runat="server">
    <input id="hdIdentificadorControl" type="hidden" name="hdIdentificadorControl" runat="server">
    <input id="hdFormaPagoSeleccionada" type="hidden" name="hdFormaPagoSeleccionada" runat="server">
    <input id="hdTipoFormaPagoSeleccionada" type="hidden" name="hdTipoFormaPagoSeleccionada"
        runat="server">
    <input id="hdTrabajaConValidacionPrevia" type="hidden" name="hdTrabajaConValidacionPrevia"
        runat="server">
    <input id="hdEsValidacionPrevia" type="hidden" name="hdEsValidacionPrevia" runat="server">
    <input id="hdConfiguracionFormaPagoOK" type="hidden" name="hdEsValidacionPrevia" runat="server"><input id="hdSeleccionoAlguno" type="hidden" name="hdEsValidacionPrevia" runat="server">
    <input id="hdCuentaSeleccionada" type="hidden" name="hdCuentaSeleccionada" runat="server">
    <cc1:MensajeBoxServerSN ID="msbConfirmar" runat="server"></cc1:MensajeBoxServerSN>
    <cc1:MessageBox ID="msbError" runat="server"></cc1:MessageBox>

    <script lang="javascript" type="text/javascript">
        let coins = JSON.parse('<%= string.IsNullOrEmpty(this.dataWcbCuentaMoneda) ? "[]" : this.dataWcbCuentaMoneda %>');
        let accountTypes = JSON.parse('<%= string.IsNullOrEmpty(this.dataWcbCuentaTipoCuenta) ? "[]" : this.dataWcbCuentaTipoCuenta %>');

        $(document).ready(function (evt) {
            initializeFormaPagoControls();
        });

        function initializeFormaPagoControls() {
            let existWcbCuenta = $('#<%= wcbCuenta.ClientID %>').length > 0;
            if (!existWcbCuenta) return;

            <%if (this.optionVacio) { %>
            $('#<%= wcbCuenta.ClientID %>').prepend('<option></option>');
            $('#<%= wcbCuenta.ClientID %>').val(-1);
            <% }  %>

            let wcbCuentaWebCombo = new BaseWebCombo('#<%= wcbCuenta.ClientID %>', {
                datatableOptions: {
                    autoWidth: false,
                    columns: [
                        {
                            data: 'CODCUENTABANCARIACLIENTE',
                            name: 'D',
                            width: "5px",
                            render: (data, type, row, meta) => {
                                let cuentaDefaultSoles = "<%= this.cuentaDefaultSoles %>";
                                let cuentaDefaultDolar = "<%= this.cuentaDefaultDolar %>";

                                return `<input type="checkbox" disabled="disabled" ${data === cuentaDefaultSoles || data === cuentaDefaultDolar ? "checked" : ""} />`;
                            }
                        },
                        {
                            data: 'CODTIPOCUENTABANCARIA',
                            name: 'CODTIPOCUENTABANCARIA',
                            width: "200px",
                            visible: false
                        },
                        {
                            data: 'CODMONEDA',
                            name: 'Moneda',
                            width: "20px",
                            render: (data, type, row, meta) => {
                                let currentCoin = coins.find(p => p.CODMONEDA.toString() === data);
                                return currentCoin.NOMBRE;
                            }
                        },
                        {
                            data: 'NOMBRE',
                            name: 'Nombre',
                            width: "200px"
                        },
                        {
                            data: 'NOCUENTA',
                            name: '# Cta',
                            width: "100px"
                        }],
                    scrollY: '150px',
                    scrollColapse: true
                },
                data: JSON.parse('<%= string.IsNullOrEmpty(this.dataWcbCuenta) ? "[]" : this.dataWcbCuenta %>'),
                placeholder: '<%= MensajeAplicacion.ObtenerEtiqueta(Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SELECCION_ELIJA_UNA_OPCION) %>',
                selectHandler: async (e) => {
                    let rowData = e.params.data;
                    if (rowData) {
                        let currentAccountType = accountTypes.find(p => p.CODTIPOCUENTABANCARIA.toString() === rowData.CODTIPOCUENTABANCARIA.toString());
                        let currentCoin = coins.find(p => p.CODMONEDA.toString() === rowData.CODMONEDA.toString());

                        $("#<%= this.txtTipoCuenta.ClientID %>").val(currentAccountType.NOMBRE);
                        $("#<%= this.txtMoneda.ClientID %>").val(currentCoin.NOMBRE);
                        $("#<%= this.txtNombreCuenta.ClientID %>").val(rowData.NOMBRE);

                        $('#<%= hdWcbCuentaSelectedData.ClientID %>').val(JSON.stringify(rowData));

                        $('#<%= hdWcbCuentaMonedaSelectedData.ClientID %>').val(rowData.CODMONEDA);
                    }
                }
            });
            wcbCuentaWebCombo.init();
        }

        function validaFORMAPAGO() {

            var retorno;
            retorno = "";
            var valor = document.Form1.ucFormaPago_ddlFormasPago.value;
            if (valor == "") {
                retorno = "<%=noConfigurada%>";
            }
            else {
                if (valor < 0) {
                    retorno = "<%=faltaFormaPago%>";
                }
                else {
                    if (document.all["ucFormaPago_panCargoAbonoValidacion"] != undefined
                        && document.all["ucFormaPago_panCargoAbonoValidacion"].style.display == "") //se muestra
                    {
                    }
                    else {
                        if (document.all["ucFormaPago_panSwiftValidacion"] != undefined
                            && document.all["ucFormaPago_panSwiftValidacion"].style.display == "") {
                        }
                        else {

                            if (document.all["ucFormaPago_panCargoAbono"] != undefined //se muestra
                                && document.all["ucFormaPago_panCargoAbono"].style.display == "") {
                                var valor = obtenerCeldaFilaActivaGrillaFromKey("#<%= wcbCuenta.ClientID %>", "NOCUENTA");
                                if (valor == undefined) {
                                    retorno = "<%=faltanumeroCuenta%>";
                                }
                            }
                            else {
                                if (document.all["ucFormaPago_panSwift"] != undefined //se muestra
                                    && document.all["ucFormaPago_panSwift"].style.display == "") {
                                    var valor = document.Form1.ucFormaPago_txtNombreCuentaSwift.value;
                                    if (valor == "" || valor == undefined) {
                                        retorno = "<%=faltanumeroCuentaDestinoSwift%>";
                                    }
                                    else {
                                        var recibe = document.Form1.ucFormaPago_txtSwiftRecibe.value;
                                        if (recibe == "" || recibe == undefined) {
												//retorno="<%=faltaTextoRecibe%>";
                                        }
                                    }
                                }
                                else {
                                    if (document.all["ucFormaPago_panLBTR"] != undefined //se muestra
                                        && document.all["ucFormaPago_panLBTR"].style.display == "") {

                                        var numeroCuenta = document.Form1.ucFormaPago_wteNumeroCheque_t.value;
                                        var banco = document.Form1.ucFormaPago_ddlBanco.value;
                                        var valido = BackOfficeAjaxMetodos.ValidaFormaPagoInterbancarios(banco, numeroCuenta);
                                        var validaEstructuraCuenta = validarCuentaCCI(document.Form1.ucFormaPago_wteNumeroCheque.value);
                                        var flag = valido.value;
                                        if (flag == true) {
												//document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                                            if (!validaEstructuraCuenta) {

                                                retorno = "<%=fallaIntebancario%>";
                                            }
                                            if (numeroCuenta == '')
                                                retorno = "<%=faltanumerocuentaCCI%>";
                                        }
                                        else {
												//document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                                            if (numeroCuenta == '')
                                                retorno = "<%=faltanumerocuentaCCI%>";
                                            else
                                                retorno = "<%=fallaIntebancario%>";
                                        }
                                    }
                                    else {
                                        retorno = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return retorno;
        }
        function ValidaCuentaCargoAbono(source, arguments) {

            var valor = obtenerCeldaFilaActivaGrillaFromKey("#<%= wcbCuenta.ClientID %>", "NOCUENTA");
            var esValidacionPrevia = (document.Form1.ucFormaPago_hdEsValidacionPrevia.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>");
            //if (valor != undefined)
            if (valor !== "") 
            {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                arguments.IsValid = true;
            } else {
                if (!esValidacionPrevia) {
                    document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                    alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvNumeroCuenta")%>");
                    arguments.IsValid = false;

                } else {
                    arguments.IsValid = true;
                }
            }
        }
        function ValidaCuentaSwift(source, arguments) {

            var valor = obtenerCeldaFilaActivaGrillaFromKey("ucFormaPagowcbCuentaSwiftxGrid", "NOCUENTA");
            var esValidacionPrevia = (document.Form1.ucFormaPago_hdEsValidacionPrevia.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>");
            if (valor != undefined) {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                arguments.IsValid = true;
            } else {
                if (!esValidacionPrevia) {
                    document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                    arguments.IsValid = false;
                    alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvCuentaSwift")%>");
                } else {
                    arguments.IsValid = true;
                }

            }
        }
        function ValidaCodigoSwift(source, arguments) {

            var valor = document.Form1.ucFormaPago_txtSwiftRecibe.value;
            var esValidacionPrevia = (document.Form1.ucFormaPago_hdEsValidacionPrevia.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>");
            if (valor != null && valor != "") {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                arguments.IsValid = true;
            } else {
                if (!esValidacionPrevia) {
                    document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                    arguments.IsValid = false;
                } else {
                    arguments.IsValid = true;
                }
            }
        }
        function ValidaInterbancarios(source, arguments) {
            var numeroCuenta = document.Form1.ucFormaPago_wteNumeroCheque.value;
            if (numeroCuenta == '')
                source.errormessage ="<%=faltanumerocuentaCCI%>";
            else
                source.errormessage ="<%=fallaIntebancario%>";

            var banco = document.Form1.ucFormaPago_ddlBanco.value;
            var valido = BackOfficeAjaxMetodos.ValidaFormaPagoInterbancarios(banco, numeroCuenta);
            var validaEstructuraCuenta = validarCuentaCCI(document.Form1.ucFormaPago_wteNumeroCheque.value)
            var flag = valido.value;
            var esValidacionPrevia = (document.Form1.ucFormaPago_hdEsValidacionPrevia.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>");
            if (flag == true) {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                if (validaEstructuraCuenta) {
                    arguments.IsValid = true;
                } else {
                    arguments.IsValid = false;
                    if (numeroCuenta == '')
                        alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancariosCuentaCCI")%>");
                    else
                        alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancarios")%>");
                }
            } else {
                if (!esValidacionPrevia) {
                    document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                    arguments.IsValid = false;
                    if (numeroCuenta == '')
                        alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancariosCuentaCCI")%>");
                    else
                        alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvInterbancarios")%>");
                } else {
                    arguments.IsValid = true;
                }

            }
        }

        //LBTR
        //Nuevos - Modulo Pagos
        function validarCuentaCCI(cuenta) {
            if (cuenta.length == 0) {
                return true;
            }

            if (cuenta.length != 20) {
                //alert(mensajeErrorLongitudCCI);
                return false;
            }
            else {

                var objetoCodigoAlterno = BackOfficeAjaxMetodos.ObtieneCodigoAlternoBanco(document.Form1.ucFormaPago_ddlBanco.value);
                var codigoBanco = objetoCodigoAlterno.value;

                if (codigoBanco.length > 3)
                    codigoBanco = codigoBanco.substring(1, 4);

                if (cuenta.substring(0, 3) != codigoBanco) {
                    //alert(mensajeErrorValidacionCCI);
                    return false;
                }
                else {

                    //Valida Banco y Plaza
                    var digito;
                    digito = obtieneDigito(cuenta.substring(0, 6));
                    if (parseInt(cuenta.substring(18, 19), 10) != digito) {
                        //alert(mensajeErrorValidacionCCI);
                        return false;
                    }

                    //Valida Cuenta
                    digito = obtieneDigito(cuenta.substring(6, 18));
                    if (parseInt(cuenta.substring(19, 20), 10) != digito) {
                        //alert(mensajeErrorValidacionCCI);
                        return false;
                    }
                }

            }
            return true;
        }
        function obtieneDigito(cadena) {
            var i;
            var valor;
            var factor;
            var suma;
            var temp;
            var operando;
            var resto;
            var decenas;
            var base;
            var digito;

            factor = 2;
            suma = 0;
            temp = 0;
            digito = 0;
            valor = 0;

            for (i = 0; i < cadena.length; i++) {
                if (factor == 2)
                    factor = 1;
                else
                    factor = 2;

                valor = parseInt(cadena.substring(i, i + 1), 10);
                temp = factor * valor;
                operando = obtieneSumatoria(temp.toString(10));
                suma = suma + operando;
            }

            decenas = Math.floor(suma / 10);
            resto = suma % 10;

            if (resto > 0)
                base = (decenas + 1) * 10;
            else
                base = decenas * 10;
            digito = base - suma;
            return digito;
        }
        function obtieneSumatoria(cadena) {
            var suma;
            var i;
            var valor;
            suma = 0;
            valor = 0;
            for (i = 0; i < cadena.length; i++) {
                valor = parseInt(cadena.substring(i, i + 1), 10);
                suma = suma + valor;
            }
            return suma;
        }
        //FIN LBTR

        function ValidaFormaPago(source, arguments) {
            //debugger;
            var valor = document.Form1.ucFormaPago_ddlFormasPago.value;
            if (valor == "" || valor < 0) {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>";
                arguments.IsValid = false;
                alert("<%=MensajeAplicacion.ObtenerEtiqueta("FormaPago.ascx.csvFormaPago")%>");
            } else {
                document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.SI%>";
                arguments.IsValid = true;
            }
        }
        function actualizarFormaPagoSeleccionada() {
            //Se enviará sólo la primera vez	

            if (document.Form1.ucFormaPago_hdCambioEnComboFormaPago.value != "N") {
                //alert(document.Form1.ucFormaPago_ddlFormasPago.value);
                document.Form1.ucFormaPago_hdCambioEnComboFormaPago.value = "S";

                document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = document.Form1.ucFormaPago_ddlFormasPago.value;
            }
            document.Form1.ucFormaPago_hdSeleccionoAlguno.value = "S";
            return true;
        }
        function cargarIdentificadorControlFormaPago(identificador) {
            document.Form1.ucFormaPago_hdIdentificadorControl.value = identificador;
        }
        function cargarControlFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, numeroCaval, codigoValor, codigoMaestro, esAsumidoPorCredibolsa) {

            actualizarFormaPagoSeleccionada();

            document.Form1.ucFormaPago_hdSeleccionoAlguno.value = "N";
            document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "N";

            var tipoOper = "";
            var OpFFL = "";
            if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>")
                || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                OpFFL = tipoOperacion;
                tipoOper = tipoOperacion.substring(0, 3);
                tipoOperacion = tipoOper;
            }

            if (!esAsumidoPorCredibolsa) {
                var resOrdenPrevia = null;
                if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA%>") {
                    resOrdenPrevia = RentaVariableAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia(numeroCaval, tipoOperacion, codigoValor, modalidad);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_PRIMARIO%>") {
                    //SUBASTAS
                    //numeroCaval = codigoCliente
                    //codigoValor = codigoSubasta
                    if (tipoOperacion == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_OPERACION_COMPRA%>") {
                        var codigoCliente = numeroCaval;
                        var codigoSubasta = codigoValor;
                        resOrdenPrevia = SubastasAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia(codigoCliente, codigoSubasta, codigoMoneda);
                    }
                    else {
                        var codigoCliente = numeroCaval;
                        resOrdenPrevia = SubastasAjaxMetodos.ObtenerDatosFormaPagoPrevia(codigoCliente);
                    }
                } else if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>") || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                    resOrdenPrevia = RentaFijaAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia(numeroCaval, OpFFL, codigoValor, codigoMaestro, codigoProducto);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE%>") {
                    resOrdenPrevia = BackOfficeAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia(numeroCaval, tipoOperacion, codigoValor, modalidad);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA%>") {
                    resOrdenPrevia = ExtrabursatilRFAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia(numeroCaval, tipoOperacion, codigoValor, codigoMaestro, modalidad);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_OPERACION_REPORTE%>") {
                    resOrdenPrevia = ReporteAjaxMetodos.ObtenerDatosFormaPagoOrdenPrevia();
                }

                // Demas mercados
                //}			
                if (!resOrdenPrevia.error) {

                    cadenas = resOrdenPrevia.value;
                    if (cadenas != null && cadenas.length > 0) {
                        document.Form1.ucFormaPago_hdConfiguracionFormaPagoOK.value = "S";
                        document.Form1.ucFormaPago_hdSeleccionoAlguno.value = "S";

                        if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>")
                            || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, OpFFL, modalidad, true);
                        }
                        else {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, true);
                        }

                        document.Form1.ucFormaPago_hdEsValidacionPrevia.value = "S";
                        if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_LBTR%>") {
                            if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                document.all["ucFormaPago_panLBTRValidacion"].style.display = "";
                                document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                document.all["ucFormaPago_lblTextoBancoLBTRValidacion"].innerText = cadenas[2];
                                document.all["ucFormaPago_lblTextoNumeroCCI"].innerText = cadenas[3];
                            }
                            if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                            }
                        } else if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_CARGO%>") {
                                if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                    document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                                    document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                    document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                    document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                    document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                    document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                    document.all["ucFormaPago_lblTextoTipoValidacion"].innerText = cadenas[2];
                                    document.all["ucFormaPago_lblTextoCuentaValidacion"].innerText = cadenas[3];
                                    document.all["ucFormaPago_lblTextoNombreCuentaValidacion"].innerText = cadenas[4];
                                    document.all["ucFormaPago_lblTextoMonedaValidacion"].innerText = cadenas[5];
                                }
                                if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                    document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                                }
                                if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                    document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                                }
                            } else if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_ABONO%>") {
                                if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                    document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                                    document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                    document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                    document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                    document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                    document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                    document.all["ucFormaPago_lblTextoTipoValidacion"].innerText = cadenas[2];
                                    document.all["ucFormaPago_lblTextoCuentaValidacion"].innerText = cadenas[3];
                                    document.all["ucFormaPago_lblTextoNombreCuentaValidacion"].innerText = cadenas[4];
                                    document.all["ucFormaPago_lblTextoMonedaValidacion"].innerText = cadenas[5];
                                }
                                if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                    document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                                }
                                if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                    document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                                }
                            } else if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_SWIFT%>") {
                            if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                document.all["ucFormaPago_panSwiftValidacion"].style.display = "";
                                document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                document.all["ucFormaPago_lblTextoTipoValidacionSwift"].innerText = cadenas[2];
                                document.all["ucFormaPago_lblTextoCuentaValidacionSwift"].innerText = cadenas[3];
                                document.all["ucFormaPago_lblTextoNombreCuentaValidacionSwift"].innerText = cadenas[4];
                                document.all["ucFormaPago_lblTextoMonedaValidacionSwift"].innerText = cadenas[5];
                                document.all["ucFormaPago_lblTextoRecibeValidacionSwift"].innerText = cadenas[6];
                            }
                            if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                            }
                        } else {
                            document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                            document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                            document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                            document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                            document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                            if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                            }
                        }
                        if (codigoProducto != "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_PRIMARIO%>") {
                            alert("<%=this.mensajeExisteOrdenPreviaConCaracteristicasSimilares%>");
                        }
                    } else {
                        if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>")
                            || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, OpFFL, modalidad, false);
                        }
                        else {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, false);
                        }
                        document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "";
                        document.Form1.ucFormaPago_hdEsValidacionPrevia.value = "N";
                    }
                }
            } else {

                var resOrdenAsumida;
                if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA%>") {
                    resOrdenAsumida = RentaVariableAjaxMetodos.ObtenerDatosFormaPagoOrdenAsumida(codigoProducto, tipoOperacion, codigoMoneda);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE%>") {
                    resOrdenAsumida = BackOfficeAjaxMetodos.ObtenerDatosFormaPagoOrdenAsumida(codigoProducto, tipoOperacion, codigoMoneda);
                } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>" || codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>" || codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA%>") {
                    resOrdenAsumida = RentaFijaAjaxMetodos.ObtenerDatosFormaPagoOrdenAsumida(codigoProducto, tipoOperacion, codigoMoneda);
                }
                if (!resOrdenAsumida.error) {
                    cadenas = resOrdenAsumida.value;
                    if (cadenas != null && cadenas.length > 0) {
                        if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>")
                            || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, OpFFL, modalidad, true);
                        }
                        else {
                            cargarControles(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, true);
                        }

                        document.Form1.ucFormaPago_hdEsValidacionPrevia.value = "S";
                        if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_CARGO%>") {
                            if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                                document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                document.all["ucFormaPago_lblTextoTipoValidacion"].innerText = cadenas[2];
                                document.all["ucFormaPago_lblTextoCuentaValidacion"].innerText = cadenas[3];
                                document.all["ucFormaPago_lblTextoNombreCuentaValidacion"].innerText = cadenas[4];
                                document.all["ucFormaPago_lblTextoMonedaValidacion"].innerText = cadenas[5];
                            }
                            if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                            }
                        } else if (cadenas[1] == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_ABONO%>") {
                            if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                                document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                                document.Form1.ucFormaPago_ddlFormasPago.value = cadenas[0];
                                document.Form1.ucFormaPago_ddlFormasPago.disabled = true;
                                document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value = "S";
                                document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = cadenas[0];
                                document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value = cadenas[1];
                                document.all["ucFormaPago_lblTextoTipoValidacion"].innerText = cadenas[2];
                                document.all["ucFormaPago_lblTextoCuentaValidacion"].innerText = cadenas[3];
                                document.all["ucFormaPago_lblTextoNombreCuentaValidacion"].innerText = cadenas[4];
                                document.all["ucFormaPago_lblTextoMonedaValidacion"].innerText = cadenas[5];
                            }
                            if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                                document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                            }
                            if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                                document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                            }
                        }
                    }
                }
            }
        }

        function cargarControles(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, esValidacion) {

            //Encontrar alguna forma de referenciar la tabla por nombre
            var CODIGO_TABLA_FORMA_PAGO = 3;
            var formasPago = null;
            if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_RUEDA_BOLSA%>") {
                formasPago = RentaVariableAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad);
            } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_PRIMARIO%>") {
                formasPago = SubastasAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad);
            } else if ((codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_DINERO%>") || (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_CONTINUO%>")) {
                formasPago = RentaFijaAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad);
            } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_VARIABLE%>") {
                formasPago = BackOfficeAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad);
            } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_EXTRABURSATIL_RENTA_FIJA%>") {
                formasPago = ExtrabursatilRFAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad);
            } else if (codigoProducto == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_MERCADO_OPERACION_REPORTE%>") {
                var identificador = document.Form1.ucFormaPago_hdIdentificadorControl.value;
                if (identificador == null) identificador = "";
                formasPago = ReporteAjaxMetodos.ObtenerDatosFormaPago(codigoMoneda, codigoProducto, codigoCanal, tipoOperacion, modalidad, identificador);
            }
            // Demas mercados
            //}	


            borrarOpciones();
            ocultarPaneles(esValidacion);
            if (!formasPago.error) {
                if (!esValidacion) {
                    document.Form1.ucFormaPago_ddlFormasPago.disabled = false;
                    //document.Form1.ucFormaPago_ddlFormasPago.disabled = false;
                }
                let dataset = JSON.parse(formasPago.value);
                dataset = dataset.SER_FORMAPAGODISPONIBLE;
                if (dataset.length > 0) {
                    for (var i = 0; i < dataset.length; i++) {
                        var opcion = document.createElement("option");
                        opcion.value = dataset[i].CODFORMAPAGOMONEDA;
                        opcion.text = dataset[i].NOMBRE;
                        document.Form1.ucFormaPago_ddlFormasPago.add(opcion);
                    }

                    //Añadimos el elemento -1
                    document.Form1.ucFormaPago_ddlFormasPago.add(new Option('<%=this.etiquetaSeleccioneUnaOpcion%>', '-1'));
                    document.Form1.ucFormaPago_ddlFormasPago.value = -1;
                    //Inicializamos los flags de carga
                    document.Form1.ucFormaPago_hdCambioEnComboFormaPago.value = "";
                    document.Form1.ucFormaPago_hdFormaPagoSeleccionada.value = "";

                } else {
                    alert("<%=this.mensajeFormasPagoNoConfigurada%>");
                }
            }
        }
        function borrarOpciones() {

            while (document.Form1.ucFormaPago_ddlFormasPago.options.length > 0) {
                document.Form1.ucFormaPago_ddlFormasPago.options[0] = null;
            }
        }
        function ocultarPaneles(esValidacion) {

            if (eval(document.all["ucFormaPago_panCargoAbono"])) {
                //alert("Panel Cargo Abono Recuperado")
                document.all["ucFormaPago_panCargoAbono"].style.display = "none";
            }
            if (eval(document.all["ucFormaPago_panLBTR"])) {
                //alert("Panel Cargo Abono Recuperado")
                document.all["ucFormaPago_panLBTR"].style.display = "none";
            }
            if (eval(document.all["ucFormaPago_panSwift"])) {
                //alert("Panel Cargo Abono Recuperado")
                document.all["ucFormaPago_panSwift"].style.display = "none";
            }
            if (!esValidacion) {
                if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                }
                if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                }
                if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                }

            }
        }
        function wcbCuenta_BeforeDropDown(webComboId) {
            //Add code to handle your event here.
            var mover = document.Form1.ucFormaPago_hdMoverScroll.value;
            if (mover == constanteSI) {
                self.scroll(1, 750);
            }
        }

    </script>
    <script>		
        controlarVisibilidadPaneles();
        function controlarVisibilidadPaneles() {

            if (document.Form1.ucFormaPago_hdTrabajaConValidacionPrevia.value == "") {
                if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                }
                if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                }
                if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                    //alert("Panel Cargo Abono Recuperado")
                    document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                }
            }
            if (document.Form1.ucFormaPago_hdEsValidacionPrevia.value == "S") {
                if (document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_LBTR%>") {
                    if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panLBTRValidacion"].style.display = "";
                    }
                    if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                    }
                } else if (document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_CARGO%>") {
                    if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                    }
                    if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                    }

                } else if (document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_ABONO%>") {
                    if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "";
                    }
                    if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                    }

                } else if (document.Form1.ucFormaPago_hdTipoFormaPagoSeleccionada.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.TIPO_FORMA_PAGO_SWIFT%>") {
                    if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panSwiftValidacion"].style.display = "";
                    }

                } else {
                    if (eval(document.all["ucFormaPago_panLBTRValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panLBTRValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panCargoAbonoValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panCargoAbonoValidacion"].style.display = "none";
                    }
                    if (eval(document.all["ucFormaPago_panSwiftValidacion"])) {
                        //alert("Panel Cargo Abono Recuperado")
                        document.all["ucFormaPago_panSwiftValidacion"].style.display = "none";
                    }
                }
            }
        }
    </script>
    <input id="hdMoverScroll" type="hidden" name="hdMoverScroll" runat="server">
</body>
