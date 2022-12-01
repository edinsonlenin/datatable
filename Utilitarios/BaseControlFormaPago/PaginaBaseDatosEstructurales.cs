using System.Data;
using Seriva.AccesoDatos.Adaptador;
using Seriva.Bolsa.Negocio.Estructurales;

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
	/// Summary description for PaginaBaseDatosEstructurales.
	/// </summary>
	public class PaginaBaseDatosEstructurales : MultibaseBusiness
	{
		public PaginaBaseDatosEstructurales(string currentInstance): base (currentInstance)
		{
		}

		//TODO evaluar que funcion quedara
//		protected internal DataSet RecuperarDatosEstructurales(string[] nombresTablas)
//		{
//			return new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales(nombresTablas);
//		}

		protected internal DataSet RecuperarDatosEstructurales()
		{
			return new DatosEstructurales(CurrentInstanceName).ObtenerDatosEstructurales();
		}

	}
}
