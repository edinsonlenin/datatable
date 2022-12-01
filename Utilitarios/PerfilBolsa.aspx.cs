using System;
using Seriva.Bolsa.Negocio.General;
using Seriva.Bolsa.Entidad.General;
using Seriva.Bolsa.Presentacion.Utilitarios;
using Seriva.Bolsa.Herramientas.Constantes;

namespace Seriva.Bolsa.Presentacion.Seguridad
{
	/// <summary>
	/// Pagina para mostrar el perfil del usuario de Bolsa que se ha logueado
	/// </summary>
	public class perfilBolsa : PaginaBase
	{
		protected System.Web.UI.WebControls.Label lblEtiquetaPerfil;
		protected System.Web.UI.WebControls.Label lblEtiquetaFechaSistema;
		protected System.Web.UI.WebControls.Label lblFechaSistema;
		protected System.Web.UI.WebControls.Label lblNombrePerfil;

		private new void  Page_Load(object sender, EventArgs e)
		{
			base.Page_Load(null,null);
			PerfilesBolsaDataSet datosPerfil= new PerfilBolsa(CurrentInstanceName).ObtenerPerfilesBolsa(Sesion.CodigoPerfilUsuario);

			lblNombrePerfil.Text= datosPerfil.SER_PERFILUSUARIO[0].NOMBRE;
			lblFechaSistema.Text = Sesion.FechaEmpresa.ToString(ConstantesBolsa.FORMATO_FECHA_DD_MM_YYYY);
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
	}
}
