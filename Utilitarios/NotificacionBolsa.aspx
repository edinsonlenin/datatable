<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v5.1, Version=5.1.20051.37, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="NotificacionBolsa.aspx.cs" AutoEventWireup="false" Inherits="Seriva.Bolsa.Presentacion.Seguridad.NotificacionBolsa" %>
<%@ Register TagPrefix="cc1" Namespace="Seriva.Bolsa.Herramientas.Controles" Assembly="Seriva.Bolsa.Herramientas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Perfil Usuario</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../Utilitarios/seriva-Stiles.css" rel="stylesheet">
		<script>	
								
			//Funcion que inicializa el contador
			function iniciarTimer()
			{
				if(esTrader==si)
				{
					setInterval("mostrarAlertas()",tiempoProceso);
				}
			}	
						
			//Funcion que utiliza AJAX, para obtener del ModeloSeguridad los link y mensajes a mostrar en el PopUp de mensajes
			function mostrarAlertas()
			{								
				try
				{
					var mensaje = null;
					var retorno = null;										
					retorno = ReporteAjaxMetodos.ObtenerCantidadOrdenesPorDescoberturarse();
					
					if(retorno.value!=null)
					{
						var strretorno = String(retorno.value);				
						if(retorno.value>0)
						{
							if(retorno.value>1)
							{
								mensaje= mensajeOrdenesPorIncumplirMargenGarantiaParte1 + strretorno + mensajeOrdenesPorIncumplirMargenGarantiaParte2;
							}
							else
							{
								mensaje= mensajeOrdenPorIncumplirMargenGarantiaParte1 + strretorno + mensajeOrdenPorIncumplirMargenGarantiaParte2;
							}
						}
						
					}
									
					if(mensaje != null)
					{
						mensaje = "<br>"+mensaje;						
						document.all.item("pwAlerta_content").CssClass= "TablasTitulos";
						document.all.item("pwAlerta_content").innerHTML = "<a href=../RentaFija/Reporte/ReposicionGarantias.aspx?Estado=" + si + " target=" + FramePrincipal + ">" + mensaje + "</a>";
						//<a href="url">Link text</a>
						pwAlertaespopup_ShowPopup(null);
						
						document.getElementById("pwAlerta_header").style.fontWeight='bold';
						//document.getElementById("pwAlerta_header").style.color="#B40404";
						
						//document.getElementById("pwAlerta_content").style.fontWeight='bold';
						document.getElementById("pwAlerta_content").style.font="13px arial,serif";
						document.getElementById("pwAlerta_content").style.color="#B40404";	
						
						document.getElementById("pwAlerta_content").style.verticalAlign="middle";
						document.getElementById("pwAlerta").style.verticalAlign="middle";
						//document.getElementById("pwAlerta").style.bottom="50px";
						
					}					
				}				
				catch(e)
				{					
					alert(mensajeCierreSesion);
					window.parent.location.href = '../Framework/Caducidad.aspx';
					return;
				}			
			}
		</script>
	</HEAD>
	<body background="..\Framework\Imagenes\imagenfondo.GIF" MS_POSITIONING="GridLayout" onload="iniciarTimer();">
		<form id="Form1" method="post" runat="server">	
			<table height="90%" width="90%">
				<tr>
					<td align="center" height="90%" width="90%">
						<cc1:popupwin id=pwAlerta title=Mensajes runat="server" CssClass="TablasTitulos" AutoShow="False" ActionType="RaiseEvents" HideAfter="4000" Visible="True" Width="180px" Height="80px" ColorStyle="Blue" Font-Bold="True"></cc1:popupwin>			
					</td>
				</tr>
			</table>		
		</form>
	</body>
</HTML>
