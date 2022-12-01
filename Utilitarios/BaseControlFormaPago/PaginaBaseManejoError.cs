using System;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Seriva.Bolsa.Herramientas.Constantes;

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
	/// Summary description for PaginaBaseManejoError.
	/// </summary>
	public class PaginaBaseManejoError
	{
		private PaginaBase pagina;

		/// <summary>
		/// Constructor de PaginaBaseManejoError
		/// </summary>
		/// <param name="paginaPadre">Pagina padre de esta página</param>
		public PaginaBaseManejoError(PaginaBase paginaPadre)
		{
			pagina = paginaPadre;
		}

		/// <summary>
		/// Permite Obtener el Mensaje de Error de Presentación
		/// </summary>
		/// <returns>Mensaje de Error de Presentación</returns>
		protected internal string ObtenerMensajeErrorPresentacion()
		{
			string valorRetorno = null;
			try
			{
				valorRetorno = ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION;
			}
			catch (Exception oException)
			{
				bool rethrow = ExceptionPolicy.HandleException(oException, ConstantesConfiguracion.EXCEPCION_POLITICA_PRESENTACION);
				if (rethrow)
				{
					throw;
				}
				else
				{
					valorRetorno = null;
				}
			}
			return valorRetorno;
		}

		/*--------------------------------------------------------------------------------------------------------------------------

			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 14/07/2005
			  REVISADO POR            : Carlos Esquivel
			  FECHA DE REVISION       : 14/07/2005

		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite Mostar un Mensaje y Cerrar la Página Actual
		/// </summary>
		/// <param name="mensaje">Mensaje a Mostrar</param>
		protected internal void LimpiarPantalla(string mensaje)
		{
			if (mensaje != null)
			{
				StringBuilder script = new StringBuilder();
				char comilla = '"';
				script.Append("<SCRIPT>");
				script.Append("EncerarPagina('");
				script.Append(pagina.Request.Url.ToString());
				script.Append("',");
				script.Append(comilla);
				script.Append(mensaje);
				script.Append(comilla);
				script.Append(");");
				script.Append("</SCRIPT>");

				pagina.RegisterStartupScript("Aviso", script.ToString());
			}
			else
			{
				StringBuilder script = new StringBuilder();
				script.Append("<SCRIPT>");
				script.Append("EncerarPaginaBlanca('");
				script.Append(pagina.Request.Url.ToString());
				script.Append("');");
				script.Append("</SCRIPT>");

				pagina.RegisterStartupScript("Aviso", script.ToString());
			}
		}

		/// <summary>
		/// Permite Mostar un Mensaje y Cerrar la Página Actual
		/// </summary>
		/// <param name="mensaje">Mensaje a Mostrar</param>
		protected internal void CerrarPantalla(string mensaje)
		{
			StringBuilder script = new StringBuilder();
			script.Append("<SCRIPT>");
			if (mensaje != null)
			{
				script.Append("alert('");
				script.Append(mensaje);
				script.Append("');");
			}
			script.Append("window.close();");
			script.Append("</SCRIPT>");
			pagina.RegisterStartupScript("Aviso", script.ToString());
		}
	}
}
