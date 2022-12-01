<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BarraMenu.ascx.cs" Inherits="Seriva.Bolsa.Presentacion.Utilitarios.Controles.BarraMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="JavaScript">
function MM_swapImage(CtrImagen,PathImagen)
			{
				document.all.item(CtrImagen.name).src = PathImagen;
			}
</script>
<DIV style="LEFT: 0px; WIDTH: 736px; POSITION: absolute; TOP: 0px; HEIGHT: 56px" ms_positioning="GridLayout">
	<P id="Prueba" runat="server">
		<TABLE id="Table1" style="WIDTH: 360px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="360"
			border="0">
			<TR>
				<TD><asp:imagebutton id="btImprimir" onmouseover="MM_swapImage(this,pathImagenes + 'bt_imprimir_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_imprimir_up.gif')" Visible="False" runat="server"
						ImageUrl="../imagenes/bt_imprimir_up.gif"></asp:imagebutton><asp:imagebutton id="btInsertar" onmouseover="MM_swapImage(this,pathImagenes + 'bt_insertar_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_insertar_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_insertar_up.gif"></asp:imagebutton><asp:imagebutton id="btBorrar" onmouseover="MM_swapImage(this,pathImagenes + 'bt_borrar_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_borrar_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_borrar_up.gif"></asp:imagebutton><asp:imagebutton id="btGuardar" onmouseover="MM_swapImage(this,pathImagenes + 'bt_guardar_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_guardar_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_guardar_up.gif" Width="40px"></asp:imagebutton><asp:imagebutton id="btnAnterior" onmouseover="MM_swapImage(this,pathImagenes + 'bt_anterior_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_anterior_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_anterior_up.gif"></asp:imagebutton><asp:imagebutton id="btnSiguiente" onmouseover="MM_swapImage(this,pathImagenes + 'bt_siguiente_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_siguiente_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_siguiente_up.gif"></asp:imagebutton><asp:imagebutton id="btnUndo" onmouseover="MM_swapImage(this,pathImagenes + 'bt_undo_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_undo_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_undo_up.gif"></asp:imagebutton><asp:imagebutton id="btnSalir" onmouseover="MM_swapImage(this,pathImagenes + 'bt_salir_down.gif')"
						onmouseout="MM_swapImage(this,pathImagenes + 'bt_salir_up.gif')" Visible="False" runat="server" ImageUrl="../imagenes/bt_salir_up.gif"></asp:imagebutton></TD>
			</TR>
		</TABLE>
	</P>
</DIV>
