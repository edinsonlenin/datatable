var PTO_DEC='.';

if (window.jQuery && window.Page_IsValid !== null && window.Page_IsValid !== undefined) {
	jQuery(document).ready(function(evt){
		$("form").on("submit", function(e){
			let isValid = Page_IsValid;
	
			if(!isValid) {
				ValidatorIndividualAlert();
			}
		});
	});
}

function ValidatorIndividualAlert() {
	var i;
	for (i = 0; i < Page_Validators.length; i++) {
		if (!Page_Validators[i].isvalid) {
			var msg;
			msg = Page_Validators[i].errormessage;
			if (!msg)
				msg = Page_Validators[i].id

			alert(msg);
			break;
		}
	}
}

function obtenerFoco(){
	self.focus();
}

function CerrarPopup()
{
	window.close();
}

function cerrarVentana(){
	window.close();
	if(eval(event)){
	  event.returnValue=false;
	  event.cancel = true;
        }

}

function EncerarPagina(url,mensaje)
{
	alert(mensaje);
	location.href = url;
}

function EncerarPaginaBlanca(url)
{
	location.href = url;
}


function abrirPopupBusqueda(pagina,height,width)
{
	window.open(pagina,"busqueda", "top=" + (self.screen.availHeight-height)/2 + ",left="+(self.screen.availWidth-width)/2 +  ",height="+height +",width=" + width +",menubar=no,toolBar=no,status=no");
}

function abrirPopupComentariosExtrabursatilRF(valor,parametroHeight,parametroWidth){
	abrirPopUpConScroll("ComentariosOrdenRF.aspx?codigoOrden="+valor,"Comentarios",300,300);	
}

function abrirPopupComentariosExtrabursatil(valor,parametroHeight,parametroWidth){
	abrirPopUpConScroll("../RentaVariable/ComentariosOrden.aspx?codigoOrden="+valor,"Comentarios",300,300);	
}


function abrirPopupComentarios(valor,parametroHeight,parametroWidth){
	abrirPopUpConScroll("ComentariosOrden.aspx?codigoOrden="+valor,"Comentarios",300,300);	
}

function abrirPopupComentariosRentaFija(valor,parametroHeight,parametroWidth){
	abrirPopUpConScroll("ComentariosOrdenRF.aspx?codigoOrden="+valor,"Comentarios",300,300);	
}
function abrirPopupComentariosRentaFijaREPO(valor,parametroHeight,parametroWidth){
	abrirPopUpConScroll("../MercadoSecundario/ComentariosOrdenRF.aspx?codigoOrden="+valor,"Comentarios",300,300);	
}

function convertToJsonFromControlValue(controlId) {
	let rawStringData = $(`#${controlId}`).val();
    return !rawStringData || rawStringData === '' ? [] : JSON.parse(rawStringData);
}


function refrescarPadreEntidad(){
	var padre = window.opener;
	padre.Form1.submit();
	window.close();	
}


function refrescarPadre(){
	var padre = window.opener;
	padre.Form1.submit();
	window.close();	
}

//Para la administracion de ordenes rv
function refrescarPadre(mensaje,cerrarVentana){
	var padre = window.opener;
	padre.Form1.submit();
	alert(mensaje);	
	if(cerrarVentana){
		window.close();
	}
}

//Para la administracion de ordenes rv
function refrescarCambiosPadre(mensaje, cerrarVentana){
	var padre = window.opener;
	// padre.Form1.submit();
	padre.document.getElementById('btnRecargar').click();
	alert(mensaje);	
	if(cerrarVentana){
		window.close();
	}
}

//Para la administracion de ordenes rv
function refrescarPadreSioSi(mensaje){
	var padre = window.opener;
	padre.Form1.submit();
	alert(mensaje);	
	window.close();
	
}

function refrescarPadreSioSi_sinMensaje(){
	var padre = window.opener;
	padre.Form1.submit();
	
	window.close();
	
}

function mostrarMensajeFinalOperacion(mensaje){
	document.Form1.submit();
	alert(mensaje);	
}

function CountChars(pObj,pLim){

      /* funcion que valida el ingreso de data en las TEXTAREA del formulario */ 

      var intLimit = pLim; 
      numChar =   eval("document.all."+ pObj + ".value.length");
      texto =     eval("document.all."+ pObj + ".value");
      if(numChar>=intLimit){
            eval("document.all." + pObj + ".value=texto.substring(0,intLimit-1)");
			//TODO Manejar mensaje
            alert ("La cantidad de caracteres permitidos" + String.fromCharCode(13) + "es de " + intLimit + ".");
      }        
}


function abrirPopupOfertaPublica(pagina,height,width){
	abrirPopUp(pagina,"OfertaPublica",230,400);
}

function abrirPopupNegociacionLotes(pagina,height,width){	
	abrirPopUp(pagina,"NegociacionLotes",180,400);
}

function abrirPopupBusquedaValores(pagina,height,width)
{
	abrirPopUp(pagina,"BusquedaValor",height,width);	
}

function abrirPopUpPrepagoReportes(pagina,height,width)
{
	abrirPopUp(pagina,"BusquedaPrepago",height,width);	
}

function abrirPopUp(pagina,nombre,height,width)
{
	var miVentana = window.open(pagina, nombre, "top=" + (self.screen.availHeight-height)/2 + ",left="+(self.screen.availWidth-width)/2 +  ",height="+height +",width=" + width +",menubar=no,toolBar=no,status=yes,titlebar=no");	
	return miVentana;
}

function abrirPopUplimpio(pagina,nombre,height,width)
{
	var miVentana = window.open(pagina, nombre, "top=" + (self.screen.availHeight-height)/2 + ",left="+(self.screen.availWidth-width)/2 +  ",height="+height +",width=" + width +",menubar=no,toolBar=no,status=no,titlebar=no");	
	return miVentana;
}

function abrirPopUpConScroll(pagina,nombre,height,width)
{
	window.open(pagina, nombre, "top=" + (self.screen.availHeight-height)/2 + ",left="+(self.screen.availWidth-width)/2 +  ",height="+height +",width=" + width +",menubar=no,toolBar=no,status=yes,scrollbars=1");	
}

function abrirReporte(pagina,nombre,height,width)
{
	window.open(pagina, nombre, "top=" + (self.screen.availHeight-height)/2 + ",left="+(self.screen.availWidth-width)/2 +  ",height="+height +",width=" + width +",menubar=yes,resizable=yes,toolBar=no,status=no,titlebar=no,scrollbars=yes");		
}

function AbrirVisor(ruta)
{
	var w =screen.width*0.55;
	var h = screen.height;
	window.open(ruta, "Reporte", "top=" + (self.screen.availHeight - h) / 2 + ",left=" + (self.screen.availWidth - w) / 2 + ",height=" + h + ",width=" + w + ",menubar=yes,resizable=yes,toolBar=no,status=no,titlebar=no,scrollbars=yes");
  //window.open(ruta,window,'scroll:yes; dialogWidth:'+w+'; dialogHeight:'+h+'; menubar=yes; resizable:yes;screenX=0;screenY=90; status=no;');
}

//Cierra el Popup y llena datos en controles en la pagina padre
function CerrarPopupPrepago(codigo,descripcion,numeroPolizaOPR,codigoPolizaOPR,codigoAsociacion,cantidadTotalValor,codigoMaestro,codigoSubTipoProducto)
{	
	var padre = window.opener;
	
	if(padre.document.all[controlA]){
		padre.document.all[controlA].value = descripcion;
	}	
	
	if(padre.document.all["hdCodigoValorSeleccionado"]){
		padre.document.all["hdCodigoValorSeleccionado"].value=codigo;
	}
	
	if(padre.document.all["hdCodigoMaestro"]){
		padre.document.all["hdCodigoMaestro"].value=codigoMaestro;
	}
	
	if(padre.document.all["hdCodigoSubTipoProducto"]){
		padre.document.all["hdCodigoSubTipoProducto"].value=codigoSubTipoProducto;
	}
		
	if(padre.document.all["hdFlagCambioValor"]){
		padre.document.all["hdFlagCambioValor"].value="S";
	}
	
	if(padre.document.all["hdFlagPrepago"]){
		padre.document.all["hdFlagPrepago"].value="S";
	}
	
	if(padre.document.all["hdNumeroPolizaOR"]){
		padre.document.all["hdNumeroPolizaOR"].value = numeroPolizaOPR;
	}
	
	if(padre.document.all["hdCodigoPolizaOR"]){
		padre.document.all["hdCodigoPolizaOR"].value = codigoPolizaOPR;
	}

	if(padre.document.all["hdCantidadTotalValor"]){
		padre.document.all["hdCantidadTotalValor"].value = cantidadTotalValor;
	}
	
	if(padre.document.all["hdCodigoAsociacionOR"]){
		padre.document.all["hdCodigoAsociacionOR"].value = codigoAsociacion;
	}
	
	padre.document.forms[0].submit();
	this.close();
}

//Cierra el Popup y llena datos en controles en la pagina padre
function CerrarPopupBusquedaValores(codigo,descripcion,emisor,nombrelargo,nombrecorto)
{		
	var padre = window.opener;
	if(padre.document.all[controlA]){
		padre.document.all[controlA].value = descripcion;
	}else{
		if(padre.document.all["hidValorSeleccionado"]){
			padre.document.all["hidValorSeleccionado"].value="S";
		}	
		if(padre.document.all["hidNombreValor"]){
			padre.document.all["hidNombreValor"].value=descripcion;
		}						
	}
	if(padre.document.all[controlB]){

		padre.document.all[controlB].value=codigo;
	}

	if(padre.document.all[controlC]){
		padre.document.all[controlC].value=emisor;
	}
	
	if(padre.document.all[controlD]){
		padre.document.all[controlD].value=nombrelargo;
	}

	if(padre.document.all[controlE]){
		padre.document.all[controlE].value=nombrecorto;
	}

	if(padre.document.all["hdCodigoValorSeleccionado"]){
		padre.document.all["hdCodigoValorSeleccionado"].value=codigo;
	}

	if(padre.document.all["hdFlagCambioValor"]){
		padre.document.all["hdFlagCambioValor"].value="S";
	}

	padre.document.forms[0].submit();
	this.close();
	//return false;
}

//Cierra el Popup y llena datos en controles en la pagina padre para el caso de subastas.
function CerrarPopupBusquedaValoresPorEmisor(codigo,descripcion,emisor,nombrelargo,nombrecorto,fechaemision)
{		
	var padre = window.opener;
	if(padre.document.all[controlA]){
		padre.document.all[controlA].value = descripcion;
	}else{
		if(padre.document.all["hidValorSeleccionado"]){
			padre.document.all["hidValorSeleccionado"].value="S";
		}	
		if(padre.document.all["hidNombreValor"]){
			padre.document.all["hidNombreValor"].value=descripcion;
		}						
	}
	if(padre.document.all[controlB]){

		padre.document.all[controlB].value=codigo;
	}

	if(padre.document.all[controlC]){
		padre.document.all[controlC].value=emisor;
	}
	
	if(padre.document.all[controlD]){
		padre.document.all[controlD].value=nombrelargo;
	}

	if(padre.document.all[controlE]){
		padre.document.all[controlE].value=nombrecorto;
	}
	
	if(padre.document.all[controlF]){
	   padre.document.all[controlF].value=fechaemision;
	}

	if(padre.document.all["hdCodigoValorSeleccionado"]){
		padre.document.all["hdCodigoValorSeleccionado"].value=codigo;
	}

	if(padre.document.all["hdFlagCambioValor"]){
		padre.document.all["hdFlagCambioValor"].value="S";
	}

	padre.document.forms[0].submit();
	this.close();
}

function CerrarPopupNegociacionLotes(){
	var cantidad = document.Form1.txtCantidad.value;
	var padre = window.opener;
	padre.document.all[controlA].value = cantidad;								
	padre.document.all["hdEsLote"].value ="S";												   
	padre.document.all["hdValorLoteVerificado"].value = "S";							
	padre.calcularComision();
	padre.aplicaFormato(padre.document.Form1.txtCantidad);		
}
function CerrarPopupNegociacionLotesNo(){
	var padre = window.opener;
	padre.document.all["hdValorLoteVerificado"].value = "S";
	if(padre.document.Form1.hdValorOfertaPublicaVerificado.value == "<%=Seriva.Bolsa.Herramientas.Constantes.ConstantesBolsa.NO%>"){
		padre.document.Form1.submit();
	} 									
	window.close();
}


function CerrarPopupOfertaPublica(){
	var codigo = document.Form1.hdCodigoOfertaPublica.value;
	var esCompra = document.Form1.hdEsCompra.value;
	//alert(codigo);
	var padre = window.opener;
	//alert(padre);
	padre.document.all["hdEsOfertaPublica"].value ="S";
	padre.document.all["hdCodigoOfertaPublica"].value =codigo;
	padre.document.all["hdValorOfertaPublicaVerificado"].value = "S";
	padre.document.all["hdCodigoOfertaPublicaConfigurada"].value = "";
	padre.document.all["hdEsCompraOfertaPublica"].value = esCompra;
	padre.document.forms[0].submit();
	window.focus();
}	

function CerrarPopupOfertaPublicaNo(codigo){
	var padre = window.opener;
	padre.document.all["hdValorOfertaPublicaVerificado"].value = "S";								
	window.close();
}
////Cierra el Popup y llena datos en las celdas de una grilla en la pagina padre
function CerrarPopupBusquedaDesdeGrillaValores()
{		
	var padre = window.opener;			
	padre.AsignarDatosPadreValores(controlA, datos);						
	this.close();
}
//Llena la suma de datos de una celda en el Footer de una grilla
function actualizarFooter(id_Grid, id_Cell){
	var resultado=0;
	var column = igtbl_getColumnById(id_Cell);
	var row = igtbl_getRowById(id_Cell);			
	var cell = igtbl_getCellById(id_Cell);			
	var partes = id_Cell.split("_");		
	var newcell;
	var cellvalor='';

	for(var nrow=0;nrow<row.OwnerCollection.Grid.Rows.length;nrow++){
		newcell=igtbl_getCellById(partes[0]+'_'+nrow+'_'+column.Index);
		if(newcell.getValue() != null){		
			resultado+=parseFloat(newcell.getValue());
		}
	}	
	var celle=igtbl_getElementById(id_Grid+"f_0_"+column.Index);
	celle.innerText=aplicarFormato(resultado);	
}
function actualizarFooterValor(id_Grid, id_Cell, valor){	
	var column = igtbl_getColumnById(id_Cell);
	var row = igtbl_getRowById(id_Cell);

	var celle=igtbl_getElementById(id_Grid+"f_0_"+column.Index);
	celle.innerText=aplicarFormato(valor);
}
function actualizarFooterColumna(id_Grid, indiceColumna){
	var resultado=0;			
	var newcell;
	var oGrid = igtbl_getGridById(id_Grid);
	var nroFilas= oGrid.Rows.length;
	for(var nrow=0;nrow<nroFilas;nrow++){
		newcell=igtbl_getCellById(id_Grid+'rc_'+nrow+'_'+indiceColumna);
		if(newcell==null){//la fila fue borrada
			nroFilas++;
		}else{				
				resultado+=parseFloat(newcell.getValue());			
		}
	}	
	var celle=igtbl_getElementById(id_Grid+"f_0_"+indiceColumna);
	celle.innerText=aplicarFormato(resultado);
	return resultado;	
}

function actualizarFooterColumna_OPR(id_Grid, indiceColumna){
	var resultado=0;			
	var newcell;
	var oGrid = igtbl_getGridById(id_Grid);
	var nroFilas= oGrid.Rows.length;
	for(var nrow=0;nrow<nroFilas;nrow++){
		newcell=igtbl_getCellById(id_Grid+'rc_'+nrow+'_'+indiceColumna);
		if(newcell==null){//la fila fue borrada
			nroFilas++;
		}else{
			if(newcell.getValue() != null){	
				if(newcell.getValue() != ''){				
					//resultado+=parseFloat(newcell.getValue());
					resultado+=parseFloat(eliminaComas(newcell.MaskedValue));
				}
			}
		}
	}	
	var celle=igtbl_getElementById(id_Grid+"f_0_"+indiceColumna);
	resultado=resultado.toFixed(2);
	celle.innerText=aplicarFormato(resultado);
	return resultado;	
}


function calcularTotalColumna(id_Grid, id_Cell){	
	var resultado=0;
	var column = igtbl_getColumnById(id_Cell);
	var row = igtbl_getRowById(id_Cell);			
	var cell = igtbl_getCellById(id_Cell);			
	var partes = id_Cell.split("_");		
	var newcell;
	var cellvalor='';
		
	for(var nrow=0;nrow<row.OwnerCollection.Grid.Rows.length;nrow++)
	{
		newcell=igtbl_getCellById(partes[0]+'_'+nrow+'_'+column.Index);
		if(newcell.getValue() != null)
		{
			resultado+=parseFloat(newcell.getValue());
		}
	}	
	return resultado;
}

//Aplica el formato adecuado a los valores numericos en base a la cultura de la aplicacion
function aplicarFormato(fuente){
	var auxFuente=fuente;	
	auxFuente=ReemplazarCaracter(auxFuente,'(','');
	auxFuente=ReemplazarCaracter(auxFuente,')','');
	auxFuente=ReemplazarCaracter(auxFuente,',','');
	auxFuente=ReemplazarCaracter(auxFuente,' ','');	
	fuente=auxFuente;	
	
	var simbolo   = '';
	var decimales = fuente;
	var decimos   = '';
	
	var formato   = formatoDecimalCultura;
	var tipo      = 'double';
	var inicio = 0;			
	if(decimales.indexOf(PTO_DEC) != -1) {
		if(fuente.charAt(0) == '-') inicio = 1;
		decimales = fuente.substring(inicio, fuente.indexOf(PTO_DEC));		
		decimos   = fuente.substring(fuente.indexOf(PTO_DEC));		
	}
	if(tipo == 'double') {
		fuente = '';
		if(inicio == 1) fuente += '-';
		
		//O.V. Aumentado 07-DIC-2003
		//Esto se aumento debido a que no funcionaba para negativos.
		var caracterNeg='';
		if(decimales.indexOf('-')>-1)
			caracterNeg='-';		
		decimales=decimales.replace('-','');		
		fuente += caracterNeg+formatoDecimal(decimales) + decimos;		
	}
	
	if(fuente.indexOf('-') != -1) {
		var indice = fuente.indexOf('-');
		fuente = fuente.substring(0, indice) + '(-) ' + fuente.substring(indice + 1);		
	}

	if(fuente.indexOf(simbolo) == -1){
		fuente = simbolo + fuente;		
	}
	
	if(formato.indexOf('.') != -1) {
	
		var df     = formato.substring(formato.indexOf('.') + 1).length;
		var actual = 0;
		
		if(fuente.indexOf(PTO_DEC) == -1) {
			fuente = fuente + PTO_DEC;
		}
		else {
			actual = fuente.substring(fuente.indexOf(PTO_DEC) + 1).length;
		}

		if(df > actual) {
			for(var i = 0; i < df - actual; i++) {
				fuente= fuente + '0';
			}
		}
		else {
			var period = fuente.indexOf(PTO_DEC);
			decimales = fuente.substring(0, period);
			decimos   = fuente.substring(period + 1, period + 1 + df);
			
			fuente = decimales + PTO_DEC + decimos;
		}		
	}
	else{
		fuente = maskAmountAux(fuente); //O.V. 28-ENERO-2004 Pues cuando era entero no se estaba formateando
	}		
	return fuente;
}

//Asigna los datos de una Array a las celdas de una grilla
function AsignarDatosPadreValores(id_Cell, datos)
{	
	if(datos[0] != document.forms[0].idNumeroCuentaOrigen.value)
	{
		var row = igtbl_getRowById(id_Cell);
		
		var resultado=0;
		var cantidadTraspasar = document.forms[0].txtCantidadTotalValoresTraspasar.value;
		var cell = igtbl_getCellById(id_Cell);			
		
		var partes = id_Cell.split("_");
				
		var newcell;
		var newcellCod;
		var cellvalor='';
		var existe = false;
		for(var nrow=0;nrow<row.OwnerCollection.Grid.Rows.length;nrow++)
		{
			newcell=igtbl_getCellById(partes[0]+'_'+nrow+'_6');
			newcellCod=igtbl_getCellById(partes[0]+'_'+nrow+'_1');
			if(newcell.getValue() != null)
			{
				resultado+=parseFloat(newcell.getValue());
			}
			
			if(newcellCod.getValue() == datos[0])
			{
				existe = true;
			}
		}
		
		if ( existe != true)
		{
			row.getCell(1).setValue(datos[0]);
			row.getCell(2).setValue(datos[1]);
			row.getCell(3).setValue(datos[2]);
			row.getCell(4).setValue(datos[3]);
			row.getCell(5).setValue(datos[4]);
			
			var aux = cantidadTraspasar - resultado;
			
			if(aux > 0)
			{
				row.getCell(6).setValue(aux);
			}
			else
			{
				row.getCell(6).setValue(0);
			}
		}
			
		else
		{
			alert(mensajeExisteCuentaMDC);
		}
	}
	else
	{
		alert(mensajeCuentaMDC);
	}
}		

//Abre un popup desde una grilla			
function abrirPopupBusquedaDesdeGrillaValores(tableName, itemName, keyStroke)
{
	window.open("PaginaBusquedaValores.aspx?TipoBusqueda=1&controlA="+itemName,"busqueda", " top=100, left= 100, height=250,width=600,menubar=no,status=yes");
}		

function Trim(control) {
	s=control.value
	while (s.substring(0,1) == ' ') {
		s = s.substring(1,s.length);
	}
	while (s.substring(s.length-1,s.length) == ' ') {
		s = s.substring(0,s.length-1);
	}
	control.value=s;
	return s;
}

function trimCadena(cadena) {
	s=cadena
	while (s.substring(0,1) == ' ') {
		s = s.substring(1,s.length);
	}
	while (s.substring(s.length-1,s.length) == ' ') {
		s = s.substring(0,s.length-1);
	}
	return s;
}

function TrimStr(strCad)
{
	var strCadena = new String(strCad);
	retorno = "";
	retorno2 = "";

	if (strCadena != "")
	{
		i = strCadena.length;
		while ((i==0) || (strCadena.substring(i-1,i) == " "))
			i--;
		retorno = strCadena.substring(0,i);
		i = 0;
		while ((i==retorno.length) || (strCadena.substring(i,i+1) == " "))
			i++;
		retorno2 = retorno.substring(i,retorno.length);
	}
	if (retorno2 == " ")
	{
		retorno2 = "";
	}
	return retorno2;
}

	
function setLength(id, length){
	document.getElementById(id).setAttribute('maxLength', length);
}

function validarNumero(sText)
{
	var ValidChars = "0123456789";
	var IsNumber=true;
	var Char;

	for (i = 0; i < sText.length; i++) 
	{ 
		Char = sText.charAt(i); 
		if (ValidChars.indexOf(Char) == -1) 
		{
			window.alert("Debe ingresar solo valores numéricos");
			document.Form1.txtNombre.focus();
			return false;
		}
	}
	return true;		   
}
function limpiar()
{
	var grid = igtbl_getGridById("ugwCuentasMDC");
		var row = igtbl_getActiveRow("ugwCuentasMDC");
		if(row != null)
			igtbl_selectRow("ugwCuentasMDC", row.Element.id, false, false);
		else {
			var cell = igtbl_getActiveCell("ugwCuentasMDC");
			if(cell != null)
				igtbl_selectCell("ugwCuentasMDC", cell.Element.id, false, false);
		}
		return 0;
	
}	
function Mensajes(cadena){
	alert(cadena);	
}
function validarnumericoonkeypress() {
	//var sKey
	//sKey = String.fromCharCode(window.event.keyCode);
	//if (!(sKey >= "0" && sKey <= "9")) { window.event.keyCode = 0; window.event.width = 0; }
	//code is the decimal ASCII representation of the pressed key.    
	var code = (window.event.which) ? window.event.which : window.event.keyCode;
	if (code == 8) { // backspace.      
		return true;
	} else if (code >= 48 && code <= 57) { // is a number.      
		return true;
	} else { // other keys.
		return false;
	}
}


function validarletrasonkeypress(){
	var sKey
	sKey = String.fromCharCode(window.event.keyCode);
	if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == " "))) 
	{
		if(!(sKey == "&")){window.event.keyCode = 0;}
	}	
}

function validaralfanumericoonkeypress(){
	var sKey
	sKey = String.fromCharCode(window.event.keyCode);
	if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == " "))) 
	{
		if(!(sKey >= "0" && sKey <= "9")) {window.event.keyCode = 0;}
	}
	/*if (sKey <= "z" && sKey >= "a"){
		window.event.keyCode = window.event.keyCode -32
	}*/
}

function bloquearWebCombo(idWebCombo, esBloqueo){
	//alert(idWebCombo);
	var	webCombo = igcmbo_getComboById(idWebCombo);
	//alert(webCombo);
	if(webCombo==null){
		alert("No se encuentra el webCombo a bloquear");
		return;
	}
	webCombo.Loaded = !esBloqueo;	
	var nombreCombo = idWebCombo + "_input";	
	document.all[nombreCombo].disabled = esBloqueo;	
}

function seleccionarFilaWebCombo(idWebCombo, nroFila){
	var idGrilla = idWebCombo + "xGrid";
	//quitamos la selección a la fila activa (en caso exista)
	var fila=igtbl_getActiveRow(idGrilla);

	if(fila!=null){
		igtbl_selectRow(idGrilla, fila.Element.id, false, true);
	}
	//activamos y seleccionamos la fila pasada como parámetro
	igtbl_selectRow(idGrilla, idGrilla + "r_" + nroFila, true, true);
	igtbl_setActiveRow(idGrilla,igtbl_getElementById(idGrilla + "r_" + nroFila));	
}

function deseleccionarFilaWebCombo(idWebCombo){
	var idGrilla = idWebCombo + "xGrid";
	//quitamos la selección a la fila activa (en caso exista)
	var fila=igtbl_getActiveRow(idGrilla);
	if(fila!=null){
		igtbl_selectRow(idGrilla, fila.Element.id, false,true);
	}	
	var nombreCombo = idWebCombo + "_input";
	document.all[nombreCombo].value="";
}

function seleccionarFilaGrilla(idGrilla, nroFila){	
	//quitamos la selección a la fila activa (en caso exista)
	var fila=igtbl_getActiveRow(idGrilla);
	if(fila!=null){
		igtbl_selectRow(idGrilla, fila.Element.id, false);
	}
	
	//activamos y seleccionamos la fila pasada como parámetro
	igtbl_setActiveRow(idGrilla,igtbl_getElementById(idGrilla + "r_" + nroFila));	
	igtbl_selectRow(idGrilla, idGrilla + "r_" + nroFila, true);		
}

function obtenerCeldaFilaActivaGrilla(idGrilla, nroColumna){
	var filaActiva = igtbl_getActiveRow(idGrilla);
	if(filaActiva==null){
		return;
	}
	
	var valor = filaActiva.getCell(nroColumna).getValue();
	return valor;
}
function obtenerTextoCeldaFilaActivaGrillaFromKey(idGrilla, nombreColumna){
		
	var filaActiva = igtbl_getActiveRow(idGrilla);
	if(filaActiva==null){
		return;
	}
	
	var valor = filaActiva.getCellFromKey(nombreColumna).getValue(true);
	return valor;
}

function obtenerCeldaFilaActivaGrillaFromKey(idGrilla, nombreColumna){
	let $webComboData = $(idGrilla).select2('data');
	if ($webComboData != null && $webComboData.length > 0) {

		return nombreColumna ? $webComboData[0][nombreColumna] : $webComboData[0];
    }
	return '';
}

function obtenerCeldaFilaActivaWebCombo(idWebCombo, nroColumna){
	return obtenerCeldaFilaActivaGrilla(idWebCombo, nroColumna);
}

function obtenerCeldaFilaActivaWebComboFromKey(idWebCombo, nombreColumna){
	return obtenerCeldaFilaActivaGrillaFromKey(idWebCombo, nombreColumna);
}

function eliminaParentesis(cadenaInicial)
{
	
	var a=cadenaInicial;
	var c="";
	var d="(";
	var b=a;
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	d=")";
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	
	b=b.replace(" ","");
	return b;
}

function eliminaPuntos(cadenaInicial){
	var a=cadenaInicial;
	var c="";
	var d=".";
	var b=a;
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	return b;
}
function eliminaComas(cadenaInicial){
	var a=cadenaInicial.toString();
	var c="";
	var d=",";
	var b=a;
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	return b;
}
function cambiaComasxPunto(cadenaInicial){
	var a=cadenaInicial;
	var c=".";
	var d=",";
	var b=a;
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	return b;
}
function cambiaPComasxPunto(cadenaInicial){
	var a=cadenaInicial;
	var c=".";
	var d=";";
	var b=a;
	while(b.indexOf(d,0)!=-1){
		b=b.replace(d,c);
	}
	return b;
}

function esCero(idTextBox){				
	var valor;
	var esCero = false;
	
	valor = parseFloat(eliminaComas(document.all[idTextBox].value));
	if(valor==0){
		esCero = true;
	}
	return esCero;
}
function validaSeleccionGrilla(idGrilla){
	var esValido = false;
	
	var fila = igtbl_getActiveRow(idGrilla);
	if(fila!=null){
		esValido = true;		
	}
	return esValido;
}
//RC
function devolverCantidadDeControl(controlObtenido){
	var cantidad=0;
	if (controlObtenido.value!='')
	{cantidad=parseFloat(eliminaComas(controlObtenido.value));}	
	return cantidad;
	}	

function devolverPosicionValor(nombregrilla,pvalor,indiceAComparar){
	try{
		var gridName=nombregrilla;		
		var	rowElem=igtbl_getFirstSibRow(gridName,igtbl_getElementById(nombregrilla+"r_0"));
		var i=0;		
		while(rowElem)
		{			
			var rowObj=igtbl_getRowById(rowElem.id);
			if (rowElem) {
			if (rowObj.getCell(indiceAComparar).getValue()==pvalor) return i;}
			i++;
			rowElem=igtbl_getNextSibRow(gridName,rowElem);
		}		
		return -1;
	}
	catch(exception)
	{
		//alert(exception);
		return -1;
	}
}

function contar(nombregrilla){
	try{
		var oGrid = igtbl_getGridById(nombregrilla);
		return oGrid.Rows.length;
	}catch(exception){
		return -1;
	}
}		

function devolverValorDeGrilla(nombregrilla,pvalor,indiceAComparar,indiceADevolver){
	try{
		var gridName=nombregrilla;
		var	rowElem=igtbl_getFirstSibRow(gridName,igtbl_getElementById(nombregrilla+"r_0"));
		while(rowElem)
		{
			var rowObj=igtbl_getRowById(rowElem.id);
			if (rowElem) {
			if (rowObj.getCell(indiceAComparar).getValue()==pvalor) return rowObj.getCell(indiceADevolver).getValue();}
			rowElem=igtbl_getNextSibRow(gridName,rowElem);
		} 
		return 0;
	}
	catch(exception)
	{
		//alert(exception);
		return 0;
	}
}		

//Funcion que remplaza un caracter determinado con '' en una cadena determinada.
function ReemplazarCaracter( inText, inFindStr, inReplStr, inCaseSensitive ) {      
   var searchFrom = 0;
   var offset = 0;
   var outText = '';
   var searchText = '';
   inText=inText.toString();
   inReplStr=inReplStr.toString();
   
   if ( inCaseSensitive == null ) {
      inCaseSensitive = false;
   }
   if ( inCaseSensitive ) {
      searchText = inText.toLowerCase();
      inFindStr = inFindStr.toLowerCase();
   } else {
      searchText = inText;
   }
   
   inFindStr=inFindStr.toString();
   searchFrom=searchFrom.toString();   
   offset = searchText.indexOf( inFindStr, searchFrom );
   while ( offset != -1 ) {
      outText += inText.substring( searchFrom, offset );
      outText += inReplStr;
      searchFrom = offset + inFindStr.length;
      offset = searchText.indexOf( inFindStr, searchFrom );
   }
   outText += inText.substring( searchFrom, inText.length );
   
   return ( outText );
}

function wcbCombo_AfterSelectChange(webComboId){
	eval("document.all.txtControl_" + webComboId + ".value=document.all." + webComboId + "_input.value");
}

function ocultarControl(controlId, ocultar){
	if(ocultar){
		document.all[controlId].style.visibility = "hidden";
	}else{
		document.all[controlId].style.visibility = "";
	}
}

function  EjecutarOnBlurPersonalizado(variable){
	
}



function obtenerElementoEnPadre(id){	
	var p = window.parent;
	if(p == null) return null;
	var doc = p.document;
	if(doc == null) return null;
	return doc.getElementById(id);
}

function cambiarFondo(panel,flag){
	if(flag==1){
		panel.style.backgroundColor= "#E0F0FF";		
	}else{
		panel.style.backgroundColor= "#F0F8FF";
	}
}
function situaFoco(id){				
	
    var objeto = document.getElementById(id);
	objeto.focus();
	
}
function formatearCadena(cadena, valorReemplazar){
	var cadenaFormateada;
	if((cadena.indexOf('{') != -1) && (cadena.indexOf('}') != -1)){
		var indiceInferior = cadena.indexOf('{');
		var indiceSuperior = cadena.indexOf('}');
		cadenaFormateada = cadena.substring(0, indiceInferior) + valorReemplazar + cadena.substring(indiceSuperior + 1);		
	}else{
		cadenaFormateada = cadena;
	}	
	return cadenaFormateada;
}



	//ANF
	function findPosX(obj) 
	{ 
		var curleft = 0; 
		if (obj.offsetParent) 
		{ 
		while (obj.offsetParent) 
		{ 
		curleft += obj.offsetLeft; 
		obj = obj.offsetParent; 
		} 
		} 
		else if (obj.x) 
		curleft += obj.x; 
		return curleft; 
	}
	
	//ANF
	function findPosY(obj) 
	{ 
		var curtop = 0; 
		if (obj.offsetParent) 
		{ 
		while (obj.offsetParent) 
		{ 
		curtop += obj.offsetTop; 
		obj = obj.offsetParent; 
		} 
		} 
		else if (obj.y) 
		curtop += obj.y; 
		return curtop; 
	}
	
	function sumarValores(strSumando,strOtroSumando)
	{
		var suma=parseFloat(strSumando)+parseFloat(strOtroSumando);		
		return suma;
	}
	
	function restarValores(strValor,strOtroValor)
	{
		var resta=parseFloat(strValor)-parseFloat(strOtroValor);
		return resta;
	}
	function formatearCadenaCeros(control) 
	{
		//Quita ceros a la izquierda de la cadena
		var cad=eliminaComas(control.value);		
		while ((cad.substring(0,1) == '0') && (cad.substring(0,2) != '.')) {
			cad = cad.substring(1,cad.length);
		}
		if(cad.substring(0,1)=='.')
		{
			cad='0'+cad;
		}			
		control.value=cad;
		return cad;
	}
/***************************************************************************************
Objetivo			:	Controla la combinacion de teclas.
Creado por			:	GES
Fecha creacion		:	13-Febrero-2007
****************************************************************************************/
// Se usa en el <body> asignando a los eventos onkeypress y onkeydown
function disableCtrlKeyCombination(metodo)
{
	//list all key you want to disable
	var forbiddenKeys = new Array(8, 8); //Coloco al menos 2 elementos para que se contruya un vector
	//list all CTRL + key combinations you want to disable
	var forbiddenCtrlKeys = new Array('n');
	//list all ALT + key combinations you want to disable
	var forbiddenAltKeys = new Array(37, 39); //Rows
	//list all key numbers you want to disable
	var forbiddenFKeys = new Array(113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123);
	//La tecla F1 (112) tambien debe manejarse en el evento on help
	
	var key;
	var isCtrl;
	var isAlt;

	if(window.event)
	{
		key = window.event.keyCode;     //IE
		if(window.event.ctrlKey)
			isCtrl = true;
		else
			isCtrl = false;
				
		if(window.event.altKey)
			isAlt = true;
		else
			isAlt = false;
	}
	else
	{
		key = e.which;     //firefox
		if(e.ctrlKey)
			isCtrl = true;
		else
			isCtrl = false;
	}

	//if ctrl is pressed check if other key is in forbidenKeys array
	if(isCtrl)
	{
			for(i=0; i<forbiddenCtrlKeys .length; i++)
			{
					//case-insensitive comparation
					if(forbiddenCtrlKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase())
					{
							alert('La combinacion CTRL + '
									+String.fromCharCode(key)
									+' ha sido inhabilitada.');
							return false;
					}
			}
	}
	else if(isAlt)
	{
		for(i=0; i<forbiddenAltKeys.length; i++)
		{
				if(forbiddenAltKeys[i] == key)
				{
						alert('La combinacion ha sido inhabilitada.');
						return false;
				}
		}
	}
	else
	{
		if(metodo == 'onkeydown')
		{
			for(i=0; i<forbiddenFKeys.length; i++)
			{
				if(forbiddenFKeys[i] == key)
				{
						event.returnValue = false;
						event.keyCode = null;
						alert('La tecla ha sido inhabilitada.');
						return false;
				}
			}
		}

		if (document.activeElement.tagName == 'BODY')
		{
			for(i=0; i<forbiddenKeys.length; i++)
			{
				if(forbiddenKeys[i] == key)
				{
						event.returnValue = false;
						event.keyCode = null;
						alert('La tecla ha sido inhabilitada.');
						return false;
				}
			}
		}
	}
	return true;
}
/***************************************************************************************
Fin Controla la combinacion de teclas
****************************************************************************************/

/***************************************************************************************
Objetivo			:	Controla general de ora y traida de datos de arreglo.
Creado por			:	GES
Fecha creacion		:	19-Agosto-2022
****************************************************************************************/
// Se usa para obtener la fecha y la hora y obtener el dato de un arreglo
//Parametros "F" y "H" 
function convertirFecha(fecha, tipotraer) {
	var fechaingresada = new Date(fecha)
	if (tipotraer == 'H') { //  la H es de HORA
		var fechaingresadahora1 = traerHora(fechaingresada.getHours());
		var fechaingresadahora2 = traerHora(fechaingresada.getMinutes());
		return fechaingresadahora1 + ":" + fechaingresadahora2;
	} else if (tipotraer == 'F') { //la F es de FECHA
		var month = (fechaingresada.getMonth() + 1 < 10 ? '0' + (fechaingresada.getMonth() + 1) : fechaingresada.getMonth());
		var day = (fechaingresada.getDate() < 10 ? '0' + fechaingresada.getDate() : fechaingresada.getDate());
		return day + "/" + month + "/" + fechaingresada.getFullYear();
	}

}
function ObtenerTipo(tipo, dataencontrar) {
	let codigo = tipo[0].Codigo;
	let nombre = tipo[0].Nombre;
	for (var i = 0; i < codigo.length; i++) {
		if (codigo[i] === dataencontrar)
			return nombre[i];
	}
}
function traerHora(Hora) {
	var nuevaHora = "";
	if (Hora < 10)
		nuevaHora = "0" + Hora;
	else
		nuevaHora = Hora;
	return nuevaHora;
}

/***************************************************************************************
Fin 
****************************************************************************************/
