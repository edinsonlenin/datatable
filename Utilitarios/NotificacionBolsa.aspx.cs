using System;
using Seriva.Bolsa.Negocio.General;
using Seriva.Bolsa.Presentacion.Utilitarios;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Bolsa.Herramientas.Utilitarios;
using Ajax;

namespace Seriva.Bolsa.Presentacion.Seguridad
{
    /// <summary>
    /// Pagina para mostrar el perfil del usuario de Bolsa que se ha logueado
    /// </summary>
    public class NotificacionBolsa : PaginaBase
	{
		protected System.Web.UI.WebControls.Label lblEtiquetaPerfil;
		protected Seriva.Bolsa.Herramientas.Controles.PopupWin pwAlerta;
		protected System.Web.UI.WebControls.Label lblNombrePerfil;
//		protected System.Web.UI.WebControls.Label lblEtiquetaPerfil;
//		protected System.Web.UI.WebControls.Label lblNombrePerfil;

		private new void  Page_Load(object sender, EventArgs e)
		{
//			base.Page_Load(null,null);
			RegistrarJScript();
			if(!Page.IsPostBack)
			{
				Utility.RegisterTypeForAjax(typeof(Seriva.Bolsa.Presentacion.RentaFija.Reporte.ReporteAjaxMetodos));
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void RegistrarJScript()
		{
			string esTrader = string.Empty;
			bool esTrdr = new Negocio.Seguridad.Permisos(CurrentInstanceName).EstaEnRol(Sesion.CodigoPerfilUsuario,TipoRolBolsa.TraderOperacionReporte);
			string numeroSegundos = new ParametroBolsa(CurrentInstanceName).ObtenerValorParametro(ConstantesBolsa.GEN_NUMERO_SEGUNDOS_MOSTRAR_AVISO_OPR);
			int tiempo = Int32.Parse(numeroSegundos);
			tiempo = tiempo*1000;
			if(esTrdr)
			{
				esTrader = ConstantesBolsa.SI;
			}
			else
			{
				esTrader = ConstantesBolsa.NO;
			}
			RegistrarVariableJScript("esTrader", esTrader);
			RegistrarVariableJScript("tiempoProceso", tiempo.ToString());
			RegistrarVariableJScript("mensajeOrdenPorIncumplirMargenGarantiaParte1", MensajeAplicacion.ObtenerMensaje("NotificacionBolsa.aspx.mensajeOrdenPorIncumplirMargenGarantiaParte1"));
			RegistrarVariableJScript("mensajeOrdenPorIncumplirMargenGarantiaParte2", MensajeAplicacion.ObtenerMensaje("NotificacionBolsa.aspx.mensajeOrdenPorIncumplirMargenGarantiaParte2"));
			RegistrarVariableJScript("mensajeOrdenesPorIncumplirMargenGarantiaParte1", MensajeAplicacion.ObtenerMensaje("NotificacionBolsa.aspx.mensajeOrdenesPorIncumplirMargenGarantiaParte1"));
			RegistrarVariableJScript("mensajeOrdenesPorIncumplirMargenGarantiaParte2", MensajeAplicacion.ObtenerMensaje("NotificacionBolsa.aspx.mensajeOrdenesPorIncumplirMargenGarantiaParte2"));
			RegistrarVariableJScript("mensajeCierreSesion", MensajeAplicacion.ObtenerMensaje("MensajeCierreSesion"));
			RegistrarVariableJScript("si", ConstantesBolsa.SI);
			RegistrarVariableJScript("FramePrincipal", ConstantesBolsa.FramePrincipal);
		}

	}
}
