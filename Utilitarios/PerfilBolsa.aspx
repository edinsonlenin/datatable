<%@ Page language="c#" Codebehind="PerfilBolsa.aspx.cs" AutoEventWireup="false" Inherits="Seriva.Bolsa.Presentacion.Seguridad.perfilBolsa" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Perfil Usuario</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../Utilitarios/seriva-Stiles.css" rel="stylesheet">
	</HEAD>
	<body background="..\Framework\Imagenes\backFrame_02.jpg" MS_POSITIONING="GridLayout"
		bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				cellSpacing="0" width="100%" border="0">
				<TR>
					<TD align="center" width="100%" colSpan="2" class="EtiquetasFramework">
						<asp:Label id="lblEtiquetaPerfil" runat="server" ForeColor="White" BackColor="Transparent"
							Font-Size="XX-Small" Font-Names="Verdana,XX-Small" Font-Bold="True">Perfil Usuario : </asp:Label>
						<asp:Label id="lblNombrePerfil" runat="server" ForeColor="Gainsboro" BackColor="Transparent"
							Font-Size="7pt" Font-Names="verdana, xx-small" Font-Bold="True"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD align="center" width="100%" colSpan="2" class="EtiquetasFramework">
						<asp:Label id="lblEtiquetaFechaSistema" runat="server" ForeColor="White" BackColor="Transparent"
							Font-Size="XX-Small" Font-Names="Verdana,XX-Small" Font-Bold="True">Fecha(dd/MM/yyyy): </asp:Label>
						<asp:Label id="lblFechaSistema" runat="server" ForeColor="Gainsboro" BackColor="Transparent"
							Font-Size="7pt" Font-Names="verdana, xx-small" Font-Bold="True"></asp:Label>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
