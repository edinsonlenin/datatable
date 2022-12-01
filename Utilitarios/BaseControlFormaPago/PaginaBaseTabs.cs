//using Seriva.Fondos.Aplicacion.Formato;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebTab;
using Seriva.AccesoDatos.Adaptador;
using Seriva.Bolsa.Entidad.Seguridad;

namespace Seriva.Bolsa.Presentacion.Utilitarios.BaseControlFormaPago
{
	/*--------------------------------------------------------------------------------------------------------------------------

				  CREADO POR              : Carlos Esquivel
				  FECHA DE CREACION       : 03/08/2005
				  MODIFICADO POR          :
				  FECHA DE MODIFICACION   :
				  REVISADO POR            :
				  FECHA DE REVISION       :

			--------------------------------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// Summary description for PaginaBaseTabs.
	/// </summary>
	public class PaginaBaseTabs : MultibaseBusiness
	{
		private PaginaBase pagina;

		/// <summary>
		/// Constructor de PaginaBaseTabs
		/// </summary>
		/// <param name="paginaPadre">Página padre</param>
		public PaginaBaseTabs(string currentInstance, PaginaBase paginaPadre) : base(currentInstance)
		{
			pagina = paginaPadre;
		}

		#region Crear Diseño del Tab

		/// <summary>
		/// Crea el diseño del Tab
		/// </summary>
		/// <param name="ultraWebTab">Tab que se modificará</param>
		protected internal void CrearTabUI(UltraWebTab ultraWebTab)
		{
			CrearTabUI(ultraWebTab, Unit.Percentage(98), Unit.Percentage(95));
		}

		/// <summary>
		/// Crea el diseño del Tab
		/// </summary>
		/// <param name="ultraWebTab">Tab que se modificará</param>
		/// <param name="width">Ancho del Tab</param>
		/// <param name="height">Alto del Tab</param>
		protected internal void CrearTabUI(UltraWebTab ultraWebTab, Unit width, Unit height)
		{
			new FormatoControl().AplicarFormato(ultraWebTab);

			ultraWebTab.Width = width;
			ultraWebTab.Height = height;
			ultraWebTab.LoadAllTargetUrls = false;
		}

		#endregion

		#region Generar tabs dinámicamente

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 09/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 09/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Genera el contenido del Tab
		/// </summary>
		/// <param name="ultraWebTab">Tab que se modificarán</param>
		/// <param name="opcion">Opción seleccionada</param>
		/// <param name="opciones">Parámetros que se pasarán a la página hija</param>
		protected internal void GenerarTabDinamico(UltraWebTab ultraWebTab, int opcion, StringDictionary opciones)
		{
			string titulo, url;

			MenuDataSet menuDataSet = new Negocio.Seguridad.Seguridad(CurrentInstanceName).ObtenerMenu(pagina.Sesion.CodigoTipoProducto, pagina.Sesion.CodigoPerfilUsuario);

			if (menuDataSet != null && menuDataSet.MENU.Rows.Count > 0)
			{
				for (int i = 0; i < menuDataSet.MENU.Rows.Count; i++)
				{
					if (menuDataSet.MENU.Rows[i]["EstiloTab"].ToString() == "T")
					{
						continue;
					}

					if (opcion >= 0)
					{
						if (menuDataSet.MENU.Rows[i]["Codopcionpadre"] == DBNull.Value || Convert.ToInt32(menuDataSet.MENU.Rows[i]["Codopcionpadre"]) != opcion)
						{
							continue;
						}
					}
					url = new Herramientas.Utilitarios.Helpers.General().ObtenerRutaAplicacion(pagina) + "/" + menuDataSet.MENU.Rows[i]["URL"].ToString() + "?aumentado=" + menuDataSet.MENU.Rows[i]["Codopcion"].ToString();

					titulo = pagina.MensajeAplicacion.ObtenerMensajeOpcion(menuDataSet.MENU.Rows[i]["TITULO"].ToString());
					Tab tab = new Tab();
					if (titulo == null)
					{
						titulo = url;
					}

					StringBuilder queryString = new StringBuilder();
					if (opciones != null)
					{
						if (opciones.Count > 0)
						{
							string[] claves = new string[opciones.Count];
							string[] valores = new string[opciones.Count];
							opciones.Keys.CopyTo(claves, 0);
							opciones.Values.CopyTo(valores, 0);
							queryString.Append("&");
							for (int j = 0; j < opciones.Count; j++)
							{
								queryString.Append(claves[j] + "=" + valores[j] + "&");
							}
							queryString.Remove(queryString.Length - 1, 1);
						}
					}
					tab.Text = titulo;
					tab.ContentPane.TargetUrl = url + queryString.ToString();
					ultraWebTab.Tabs.Add(tab);
					ultraWebTab.DummyTargetUrl = new Herramientas.Utilitarios.Helpers.General().ObtenerRutaAplicacion(pagina) + "/Utilitarios/Cargando.aspx";
				}
			}
			else
			{
				ultraWebTab.Visible = false;
			}
		}

		#endregion
	}
}