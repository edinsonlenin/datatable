<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BarraMenuC.ascx.cs" Inherits="Seriva.Bolsa.Presentacion.Utilitarios.Controles.BarraMenuC" TargetSchema="http:/schemas.microsoft.com/intellisense/ie5" %>
<SCRIPT language="JavaScript">

			function MM_swapImage(CtrImagen,PathImagen)
			{
				document.all.item(CtrImagen.name).src = "<%=rutaImagenes%>" + PathImagen;
			}
</SCRIPT>
<P id="P" runat="server">
	<TABLE id="Table1" cellspacing="1" cellpadding="1" border="0" height="32">
		<TR>
			<TD>
				<ASP:IMAGEBUTTON id="btImprimir" onmouseover="MM_swapImage(this,'bt_imprimir_down.gif')" onmouseout="MM_swapImage(this,'bt_imprimir_up.gif')"
					onmousedown="MM_swapImage(this,'bt_imprimir_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_imprimir_up.gif"
					tooltip="Imprimir"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btInsertar" onmouseover="MM_swapImage(this,'bt_insertar_down.gif')" onmouseout="MM_swapImage(this,'bt_insertar_up.gif')"
					onmousedown="MM_swapImage(this,'bt_insertar_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_insertar_up.gif"
					tooltip="Insertar"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btBorrar" onmouseover="MM_swapImage(this,'bt_borrar_down.gif')" onmouseout="MM_swapImage(this,'bt_borrar_up.gif')"
					onmousedown="MM_swapImage(this,'bt_borrar_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_borrar_up.gif"
					tooltip="Borrar" backcolor="Transparent"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btGuardar" onmouseover="MM_swapImage(this,'bt_guardar_down.gif')" onmouseout="MM_swapImage(this,'bt_guardar_up.gif')"
					onmousedown="MM_swapImage(this,'bt_guardar_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_guardar_up.gif"
					tooltip="Guardar"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnAnterior" onmouseover="MM_swapImage(this,'bt_anterior_down.gif')" onmouseout="MM_swapImage(this,'bt_anterior_up.gif')"
					onmousedown="MM_swapImage(this,'bt_anterior_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_anterior_up.gif"
					tooltip="Anterior"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnSiguiente" onmouseover="MM_swapImage(this,'bt_siguiente_down.gif')" onmouseout="MM_swapImage(this,'bt_siguiente_up.gif')"
					onmousedown="MM_swapImage(this,'bt_siguiente_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_siguiente_up.gif"
					tooltip="Siguiente"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnSalir" onmouseover="MM_swapImage(this,'bt_salir_down.gif')" onmouseout="MM_swapImage(this,'bt_salir_up.gif')"
					onmousedown="MM_swapImage(this,'bt_salir_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_salir_up.gif"
					tooltip="Salir"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnInsertarDetalle" onmouseover="MM_swapImage(this,'bt_insertar_detalle_down.gif')"
					onmouseout="MM_swapImage(this,'bt_insertar_detalle_up.gif')" onmousedown="MM_swapImage(this,'bt_insertar_detalle_down.gif')"
					runat="server" visible="False" imageurl="../imagenes/bt_insertar_detalle_up.gif" tooltip="Insertar detalle"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnAprobar" onmouseover="MM_swapImage(this,'bt_insertar_detalle_down.gif')"
					onmouseout="MM_swapImage(this,'bt_insertar_detalle_up.gif')" onmousedown="MM_swapImage(this,'bt_insertar_detalle_down.gif')"
					runat="server" visible="False" imageurl="../imagenes/bt_insertar_detalle_up.gif" tooltip="Aprobar"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnRechazar" onmouseover="MM_swapImage(this, 'bt_salir_down.gif')" onmouseout="MM_swapImage(this,'bt_salir_up.gif')"
					onmousedown="MM_swapImage(this,'bt_salir_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_salir_up.gif"
					tooltip="Rechazar"></ASP:IMAGEBUTTON>
				<ASP:IMAGEBUTTON id="btnUndo" onmouseover="MM_swapImage(this,'bt_undo_down.gif')" onmouseout="MM_swapImage(this,'bt_undo_up.gif')"
					onmousedown="MM_swapImage(this,'bt_undo_down.gif')" runat="server" visible="False" imageurl="../imagenes/bt_undo_up.gif"
					tooltip="Deshacer"></ASP:IMAGEBUTTON></TD>
		</TR>
	</TABLE>
</P>
