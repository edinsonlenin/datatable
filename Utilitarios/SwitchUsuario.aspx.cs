using System;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
	/// <summary>
	/// Summary description for SwitchUsuario.
	/// </summary>
	public class SwitchUsuario : PaginaBase
	{
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button5;
		protected System.Web.UI.WebControls.Button btnTraderReporte;
		protected System.Web.UI.WebControls.Button BtnVentas;
		protected System.Web.UI.WebControls.Button btnAdministrador;
		protected System.Web.UI.WebControls.Button btnBackOffice;
		protected System.Web.UI.WebControls.Button btnTraderAdministrador;

		private new void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.btnAdministrador.Click += new System.EventHandler(this.btnAdministrador_Click);
			this.btnTraderReporte.Click += new System.EventHandler(this.btnTraderReporte_Click);
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			this.BtnVentas.Click += new System.EventHandler(this.BtnVentas_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.btnBackOffice.Click += new System.EventHandler(this.btnBackOffice_Click);
			this.btnTraderAdministrador.Click += new System.EventHandler(this.btnTraderAdministrador_Click);

		}
		#endregion

		private void btnAdministrador_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 3;
			Sesion.CodigoUsuario = 0;
		}

		private void BtnVentas_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 9;

		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 14;
			Sesion.CodigoUsuario = 2;
		}

		private void btnBackOffice_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 4;
		}

		private void btnTraderAdministrador_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 8;
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 12;
			Sesion.CodigoUsuario = 3;
		}

		private void btnTraderReporte_Click(object sender, System.EventArgs e)
		{
			Sesion.CodigoPerfilUsuario = 52;
			//Sesion.CodigoUsuario       =
		}
	}
}
