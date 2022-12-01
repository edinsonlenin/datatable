

namespace Seriva.Bolsa.Presentacion.Utilitarios.controles
{
    using System;
    using System.Text;

    /// <summary>
    ///		Summary description for CalendarioFuncional.
    /// </summary>
    public class CalendarioFuncional : System.Web.UI.UserControl
	{
		protected Infragistics.WebUI.WebSchedule.WebCalendar SharedCalendar;

		private void Page_Load(object sender, System.EventArgs e)
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
			this.obtenerPathScript();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void obtenerPathScript()
		{
			string path = Page.Request.Url.AbsolutePath;

			string[] resPath = path.Split('/');

			string pathScript = string.Empty;

			for(int i = 1; i < resPath.Length - 2; i++)
			{
				pathScript = pathScript + "../";
			}

			pathScript = pathScript + "/Utilitarios/JScript/";

			StringBuilder MyScript = new StringBuilder();
			MyScript.Append("<script language=\"javascript\">");
			MyScript.Append("var pathScript='");
			MyScript.Append(pathScript);
			MyScript.Append("';");
			MyScript.Append("</script>");
			Page.RegisterStartupScript("var",MyScript.ToString());
		}
	}
}
