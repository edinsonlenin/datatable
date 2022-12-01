<%@ Register TagPrefix="cc1" Namespace="Seriva.Bolsa.Herramientas.Controles" Assembly="Seriva.Bolsa.Herramientas" %>
<%@ Page Language="c#" CodeBehind="PaginaBusqueda.aspx.cs" AutoEventWireup="false" Inherits="Seriva.Bolsa.Presentacion.Utilitarios.PaginaBusqueda" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title><%=this.TituloPagina%>
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.10)">    
    <script type="text/javascript" language="javascript" src="../Utilitarios/JScript/jsUtil.js"></script>

    <link href="../src/assets/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Utilitarios/seriva-stiles.css" type="text/css" rel="stylesheet">    
    <link href="../src/css/custom.datatable.css" rel="stylesheet" />
    <link href="../Utilitarios/cliente.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Utilitarios/Jscript/jsUtil.js"></script>       
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-3.6.0.min.js"></script>

    <script type="text/javascript" language="javascript" src="../src/assets/dataTable/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript" src="../src/assets/dataTable/resize.datatable.js"></script>
    <script type="text/javascript" src="../src/js/CustomDataTable.js"></script>

    <script type="text/javascript" language="javascript">
        let tipoDato = "<%=this.tipoDato%>";
        let longitudDato = "<%=this.longitud%>";
        let mensaje = "<%=this.mensajeLongitud%>";
        
        let table;

        jQuery(document).ready(function (evt) {
            let documentTypes = convertToJsonFromControlValue("<%= uwgResultado2_documentTypes.ClientID %>");
            let resultsData = convertToJsonFromControlValue("<%= uwgResultado2_data.ClientID %>");
            let rowNegativeStates = convertToJsonFromControlValue("<%= uwgResultado2_rowStates.ClientID %>");

            console.log(rowNegativeStates);

            table = new BaseDataTable('#uwgResultado2', {
                scrollY: '185px',
                scrollColapse: true,
                data: resultsData,
                order: [[4, 'asc']],
                columns: [
                    { data: 'CORRELATIVO', visible: true },
                    { data: 'CODCLIENTE', visible: true},
                    {
                        data: 'CODTIPOIDENTIFICACION',
                        width: '21%',
                        render: (data, type, row) => {
                            let currentDocument = documentTypes.find(p => p.CODTIPOIDENTIFICACION === parseInt(data.toString()));
                            return currentDocument.NOMBRE;
                        }
                    },                   
                    { data: 'IDENTIFICACION', width: '21%' },
                    { data: 'DESCRIPCION', width: '46%' }
                ],
                dblClickHandler: ($td, rowData) => {
                    uwgResultado_DblClickHandler($td, rowData);
                },
                createdRow: (row, data, dataIndex) => {
                    let isRowNegative = rowNegativeStates[data.CODCLIENTE];
                    if (isRowNegative) {
                        $(row).addClass('bg-danger');
                    }                    
                }
            });   

            table.init();
        });

        function validarSoloNumeros() {
            if (event.keyCode < 45 || event.keyCode > 57) {
                event.returnValue = false;
                return false;
            }
            return true;
        }

        function validarAlfaNumericos() {
            if ((event.keyCode > 32 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97)) {
                event.returnValue = false;
                return false;
            }
            return true;
        }

        function verificarFormato() {
            validarSoloNumeros();
        }

        function mostrarMensajeZeroResultados() {
            alert(mensajeZeroResultados);
        }

        function validaBotonBuscar() {
            if (document.Form1.ddlBuscar.value =="<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.BUSCARREGISTRO%>") {
                var registro = document.Form1.txtRegistro.value;
                if (registro == "") {
                    alert(registronNoDatos);
                    return false;
                }
                return true;
            }

            if (document.Form1.ddlBuscar.value =="<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.BUSCARCAVAL%>") {
                var caval = document.Form1.txtCodigoCaval.value;
                if (caval == "") {
                    alert(cavalNoDatos);
                    return false;
                }
                return true;
            }

            if (document.Form1.ddlBuscar.value =="<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.BUSCARIDENTIFICACION%>") {
                if ((document.Form1.txtNumIDC.value == "") && (document.Form1.txtNumCorrelativo.value == "")) {
                    alert(identificacionNoDatos);
                    return false;
                }
                document.Form1.hdIDC.value = document.Form1.txtNumIDC.value;
                document.Form1.hdCorrelativo.value = document.Form1.txtNumCorrelativo.value;
                return true;
            }

            if (document.Form1.ddlBuscar.value =="<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.BUSCARRAZONSOCIAL%>") {
                var razonsocial = document.Form1.txtRazonSocial.value;
                if (razonsocial == "") {
                    alert(razonsocialNoDatos);
                    return false;
                }

                if (razonsocial.length < <%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_RAZONSOCIAL%>) {
                    alert(razonsocialInvalida);
                    return false;
                }
                return true;
            }

            if (document.Form1.ddlBuscar.value =="<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.BUSCARNOMBRE%>") {
                var nombre = document.Form1.txtNombre.value;
                var apellidoP = document.Form1.txtApellidoP.value;
                var apellidoM = document.Form1.txtApellidoM.value;

                if ((nombre == "") && (apellidoP == "") && (apellidoM == "")) {
                    alert(nombreapellidoNoDatos);
                    return false;
                }

                if (nombre.length < <%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_NOMBRE%>) {
                    alert(nombreInvalido);
                    return false;
                }

                if (apellidoP.length < <%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.CLIENTES_NUM_MINIMO_CARACT_BUSQ_APELLIDO%>) {
                    alert(apellidoPInvalido);
                    return false;
                }
                return true;
            }
            return true;
        }

        function CerrarVentana(codigo, descripcion) {
            var padre = window.opener;
            if ("<%=ActualizarPadre %>" == "SI")
                padre.document.Form1.submit();
            this.close();
        }

        function AsignaEnfoque(txt) {
            document.getElementById(txt).focus();
        }
        function SeleccionTipobusqueda() {
            var opt = document.getElementById('ddlBuscar').options[document.getElementById('ddlBuscar').selectedIndex].value;
            switch (opt) {
                case '001':			// ConstantesBolsa.BUSCARNOMBRE
                    AsignaEnfoque('txtNombre');
                    break;
                case '002':			// ConstantesBolsa.BUSCARRAZONSOCIAL
                    AsignaEnfoque('txtRazonSocial');
                    break;
                case '003':	       // ConstantesBolsa.BUSCARIDENTIFICACION
                    AsignaEnfoque('txtNumIDC');
                    break;
                case '004':		  // ConstantesBolsa.BUSCARREGISTRO
                    AsignaEnfoque('txtRegistro');
                    break;
                case '005':       // ConstantesBolsa.BUSCARCAVAL
                    AsignaEnfoque('txtCodigoCaval');
                    break;
            }
        }

        function buscaCorrelativo() {
            return validarSoloNumeros();
        }

        function buscaIDC() {
            if (tipoDato == 'N')
                return validarSoloNumeros();
            else
                return validarAlfaNumericos();
        }

        function Buscar() {
            var charCode = event.keyCode;
            if (charCode == 13) {
                if (document.getElementById("txtCodigoCaval") != null) {
                    if (document.getElementById("txtCodigoCaval").value != "")
                        formatoNumeroCaval();
                }
                event.cancelBubble = true;
                document.Form1.btnBuscar.click();
                return false;
            }
            return true;
        }

        function uwgResultado_DblClickHandler($cell, rowData) {  
            let correlativo = rowData.CORRELATIVO;
            let codTipoIdentificacion = rowData.CODTIPOIDENTIFICACION;
            let identificacion = rowData.IDENTIFICACION;
            let codCliente = rowData.CODCLIENTE;
            let descripcion = rowData.DESCRIPCION;

            var padre = window.opener;
            var nombrePadre = padre.document.getElementById('hdNombrePagina');
            if (padre.document.getElementById('hdNombrePagina') == null) {
                nombrePadre = ''
            } else {
                nombrePadre = padre.document.getElementById('hdNombrePagina').value;
            }

            if (nombrePadre == 'IngresoOrdenRV') {
                padre.RecargarClienteConBusqueda(codTipoIdentificacion,
                    identificacion,
                    correlativo);

                this.close();
                return;
            }

            if (nombrePadre == 'OrdenesImpresionRV') {               
                if (correlativo == null) {
                    correlativo = "";
                }
                padre.cargarIdentificacionCliente(codTipoIdentificacion,
                    identificacion, correlativo);
                this.close();
                return;
            }

            if (nombrePadre == 'OrdenesImpresionRF') {
                if (correlativo == null) {
                    correlativo = "";
                }
                padre.cargarIdentificacionCliente(codTipoIdentificacion,
                    identificacion, correlativo);
                this.close();
                return;
            }

            if (nombrePadre == 'MapaAsignacion') {
                if (correlativo == null) {
                    correlativo = "";
                }
                padre.cargarIdentificacionCliente(codTipoIdentificacion,
                    identificacion, correlativo);
                this.close();
                return false;
            }
            /*
            if(nombrePadre == 'ReporteComisiones'){
                var correlativo = myrow.getCellFromKey("CORRELATIVO").getValue();
                if(correlativo == null)
                {
                    correlativo = "";
                }
                padre.cargarIdentificacionCliente(myrow.getCellFromKey("CODTIPOIDENTIFICACION").getValue(),
                    myrow.getCellFromKey("IDENTIFICACION").getValue(),correlativo);
                this.close();
                return;
            }
            */           
            var recarga = 'SI';
            var estadoSesion = RentaVariableAjaxMetodos.ValidarSesionActiva().value;
            if (estadoSesion == 0) {
                this.close();
                padre.location.href = '../Seguridad/Caducidad.aspx';
                return false;
            }

            var tipoCliente = document.getElementById('hdTipoCliente').value;
            var res = '';

            if (tipoCliente == 'O' || tipoCliente == 'C') {
                res = RentaVariableAjaxMetodos.GuardaSeleccionClienteEnSesion_TipoCliente(codCliente, descripcion, recarga, tipoCliente).value;
            }
            else {
                res = RentaVariableAjaxMetodos.GuardaSeleccionClienteEnSesion(codCliente, descripcion, recarga).value;
            }

            if (nombrePadre == 'IngresoRapidoRV') {
                padre.document.getElementById('hdCodigoCliente').value = codigoCliente;
                padre.document.getElementById('hdDescripcionCliente').value = descripcion;
                padre.RecargarCliente(codigoCliente);
            } else {
                padre.document.Form1.submit();
            }
            this.close();
        }

        function formatoNumeroCaval() {
            var CodigoCaval = trimCadena(document.getElementById('txtCodigoCaval').value);
            if (CodigoCaval.length == 0) return;
            numeroCaracteresCodCaval = constanteNumeroCaracteresCodCaval;
            limite = numeroCaracteresCodCaval - CodigoCaval.length;
            ceros = "";
            for (i = 0; i < limite; i++) {
                ceros = ceros + "0";
            }
            document.getElementById('txtCodigoCaval').value = ceros + CodigoCaval;

            //Obteniendo el estado del numero caval del cliente	
            var paginaPadre = window.opener;
            var nombrePaginaPadre = paginaPadre.document.getElementById('hdNombrePagina');
            if (paginaPadre.document.getElementById('hdNombrePagina') == null) {
                nombrePaginaPadre = ''
            } else {
                nombrePaginaPadre = paginaPadre.document.getElementById('hdNombrePagina').value;
            }
            if (nombrePaginaPadre == 'MapaAsignacion') {
                document.getElementById('hdEstadoCaval').value = -1;//al asignarle -1 buscará los cavales activos e inactivos
            }
            if (nombrePaginaPadre == 'OrdenesImpresionRV') {
                document.getElementById('hdEstadoCaval').value = -1;
            }
            if (nombrePaginaPadre == 'OrdenesImpresionRF') {
                document.getElementById('hdEstadoCaval').value = -1;
            }
            if (nombrePaginaPadre == 'ReporteComisiones') {
                document.getElementById('hdEstadoCaval').value = -1;
            }
            if (nombrePaginaPadre == '') {
                document.getElementById('hdEstadoCaval').value = -1;
            }
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table2" cellspacing="1"
            cellpadding="1" width="100%" border="0">
            <tr>
                <td width="3%"></td>
                <td style="height: 18px">
                    <cc1:MessageBox ID="msbError" runat="server"></cc1:MessageBox>
                </td>
                <td style="height: 18px" align="right" colspan="3">&nbsp;<asp:Label ID="lblTitulo" runat="server" CssClass="TitularesPaginas"></asp:Label></td>
                <td style="height: 18px"></td>
            </tr>
            <tr>
                <td width="3%"></td>
                <td style="height: 18px">&nbsp;</td>
                <td style="height: 18px" align="right" colspan="3"></td>
                <td style="height: 18px"></td>
            </tr>
            <tr>
                <td width="3%"></td>
                <td width="10%"></td>
                <td width="50%">
                    <table onkeydown="return Buscar()" cellspacing="1" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="TablasTitulos" style="height: 19px" width="20%">
                                <asp:Label ID="lblBuscar" runat="server"></asp:Label></td>
                            <td style="height: 22px" width="35%">
                                <asp:DropDownList ID="ddlBuscar" runat="server" CssClass="texto-negro-peque-mayus" Width="90%" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblNombre" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="texto-negro-peque-mayus" Width="90%" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblApellidoP" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtApellidoP" runat="server" CssClass="texto-negro-peque-mayus" Width="90%"
                                    MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblApellidoM" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtApellidoM" runat="server" CssClass="texto-negro-peque-mayus" Width="90%"
                                    MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="texto-negro-peque-mayus" Width="90%"
                                    MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblTipoIDC" runat="server"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlTipoIDC" runat="server" CssClass="texto-negro-peque-mayus" Width="90%" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblNumDoc" runat="server"></asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="txtNumIDC" runat="server" CssClass="texto-negro-peque-mayus" Width="58%"></asp:TextBox><asp:TextBox ID="txtNumCorrelativo" runat="server" CssClass="texto-negro-peque-mayus" Width="32%"
                                    MaxLength="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblNRegistro" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtRegistro" runat="server" CssClass="texto-negro-peque-mayus" Width="90%" MaxLength="50"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TablasTitulos">
                                <asp:Label ID="lblCodigoCaval" runat="server"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCodigoCaval" runat="server" CssClass="texto-negro-peque-mayus" Width="90%"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td valign="top" width="10%" colspan="2">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAzul"></asp:Button></td>
                <td width="5%"></td>
            </tr>
            <tr>
                <td class="texto-rojo-peque" align="right" colspan="5" height="10">
                    <asp:Label ID="lblArchivoNegativo" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="TablasItemAzul" align="center" colspan="4">
                    <asp:Label ID="lblGrilla" runat="server"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="BordeCeldaBolsa" align="center" colspan="4">
                    <asp:HiddenField ID="uwgResultado2_data" runat="server" />
                    <asp:HiddenField ID="uwgResultado2_rowStates" runat="server" />
                    <asp:HiddenField ID="uwgResultado2_documentTypes" runat="server" />

                    <table id="uwgResultado2" class="cell-border compact tabla-general stripe">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th><%= MensajeAplicacion.ObtenerEtiqueta("PaginaBusqueda.aspx.uwgResultado.CODTIPOIDENTIFICACION") %></th>
                                <th><%= MensajeAplicacion.ObtenerEtiqueta("PaginaBusqueda.aspx.uwgResultado.IDENTIFICACION") %> </th>
                                <th><%= MensajeAplicacion.ObtenerEtiqueta("PaginaBusqueda.aspx.uwgResultado.DESCRIPCION") %></th>
                            </tr>
                        </thead>
                    </table>
              

                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="TablasTitulos" colspan="5"></td>
            </tr>
        </table>
        <input id="hdCorrelativo" type="hidden" name="hdTipoClienteBolsa" runat="server">
        <input id="hdIDC" type="hidden" name="hdTipoClienteBolsa" runat="server">
        <input id="hdTipoDato" type="hidden" name="hdTipoClienteBolsa" runat="server">
        <input id="hdLongitud" type="hidden" name="hdTipoClienteBolsa" runat="server">
        <input id="hdEstadoCaval" type="hidden" name="hdEstadoCaval" runat="server">
        <input id="hdTipoCliente" type="hidden" name="hdOfertante" runat="server">
        <%=MyScript%>
        <script language="javascript" type="text/javascript">

            $(document).ready(function (evt) {
                SeleccionTipobusqueda();

                $("#btnBuscar").on("click", validaBotonBuscar);

                if (document.all.item('txtNumCorrelativo'))
                    $("#txtNumCorrelativo").on("onkeypress", buscaCorrelativo);
                if (document.all.item('txtNumIDC')) {
                    $("#txtNumIDC").on("onkeypress", buscaIDC);
                    document.all.item('txtNumIDC').setAttribute('maxLength', longitudDato);
                }
            });
        </script>
    </form>
</body>
</html>
