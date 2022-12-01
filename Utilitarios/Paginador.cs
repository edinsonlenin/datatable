using System;
using System.Data;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
	/// <summary>
	/// Paginador de DataSet.
	/// </summary>
	public class Paginador
	{
		/// <summary>
		/// Setea la clase con la información necesaria para funcionar
		/// </summary>
		/// <param name="InfoOriginal">Información original que se desea paginar</param>
		/// <param name="NumeroRegistroPagina">Indica el número de registros por página, no se soportan valores menores a 1</param>
		public Paginador(DataSet InfoOriginal, int NumeroRegistroPagina)
		{
			of_SetInfoOriginal(InfoOriginal);
			NumeroRegistroXPagina = NumeroRegistroPagina;
		}

		/// <summary>
		/// Indica el número de registros por página
		/// </summary>
		private int NumeroRegistroPagina = 1;

		/// <summary>
		/// Ultima página pedida
		/// </summary>
		private int UltimaPagina = 0;

		/// <summary>
		/// Almacena la información original
		/// </summary>
		private DataSet InfoOriginal;

		/// <summary>
		/// Indica el número de registros por página, no se soportan valores menores a 1
		/// </summary>
		public int NumeroRegistroXPagina
		{
			set
			{
				if ( value <= 0 )
				{
					NumeroRegistroPagina = 1;
				}
				else
					NumeroRegistroPagina = value;
			}
			get
			{
				return NumeroRegistroPagina;
			}
		}

		/// <summary>
		/// Ultima página que se pidio, o indica que no se ha pedido ninguna página (solo lectura)
		/// </summary>
		public int UltimaPaginaPedida
		{
			get
			{
				return UltimaPagina;
			}
		}

		/// <summary>
		/// Carga la información que se desea paginar
		/// </summary>
		/// <param name="InfoOriginal">Información original que se desea paginar</param>
		public void of_SetInfoOriginal(DataSet InfoOriginal)
		{
			this.InfoOriginal = InfoOriginal;
		}

		/// <summary>
		/// Obtiene la información original que se pagina
		/// </summary>
		/// <returns>Información original</returns>
		public DataSet of_GetInfoOriginal()
		{
			return this.InfoOriginal;
		}

		/// <summary>
		/// Lee el número total de páginas, las páginas inician en 0
		/// </summary>
		public int Count
		{
			get
			{
				if ( InfoOriginal == null || InfoOriginal.Tables.Count == 0 || InfoOriginal.Tables[0].Rows.Count == 0 )
					return 0;

				int residuo = 0;
				if ( InfoOriginal.Tables[0].Rows.Count % NumeroRegistroPagina > 0 )
					residuo = 1;

				return (int)Math.Floor((Decimal)InfoOriginal.Tables[0].Rows.Count / NumeroRegistroPagina) + residuo;
			}
		}

		/// <summary>
		/// Obtiene una página
		/// </summary>
		/// <param name="NumeroPagina">Número de página pedida, si es mayor al máximo devuelve null</param>
		/// <returns>Dataset que contiene la información de la página pedida o null</returns>
		public DataSet of_GetPagina(int NumeroPagina)
		{
			if ( InfoOriginal == null )
				return null;

			DataSet ds = InfoOriginal.Clone();

			if ( Count == 0 )
				return ds;

			if ( NumeroPagina >= Count )
				return InfoOriginal;

			int i_Inicio = (NumeroPagina - 1)*NumeroRegistroPagina;
			int i_Final = (NumeroPagina)*NumeroRegistroPagina;

			if ( i_Final > InfoOriginal.Tables[0].Rows.Count )
				i_Final = InfoOriginal.Tables[0].Rows.Count - 1;

			for (int i=i_Inicio; i<i_Final ; i++)
				ds.Tables[0].Rows.Add(InfoOriginal.Tables[0].Rows[i].ItemArray);

			UltimaPagina = NumeroPagina;
			return ds;
		}

		/// <summary>
		/// Obtiene una página
		/// </summary>
		/// <param name="NumeroPagina">Número de página pedida, si es mayor al máximo devuelve null</param>
		/// <param name="Relacion">Nombre de las relaciones de la Tabla[0] para obtener las filas relacionadas</param>
		/// <returns>Dataset que contiene la información de la página pedida o null</returns>
		public DataSet of_GetPagina(int NumeroPagina, string[] Relacion)
		{
			if ( InfoOriginal == null )
				return null;

			DataSet ds = InfoOriginal.Clone();
			if ( Count == 0 )
				return ds;

			if ( NumeroPagina > Count )
				return ds;

			int i_Inicio = (NumeroPagina - 1)*NumeroRegistroPagina;
			int i_Final = (NumeroPagina)*NumeroRegistroPagina;

			if ( i_Final > InfoOriginal.Tables[0].Rows.Count )
				i_Final = InfoOriginal.Tables[0].Rows.Count;

			for (int i=i_Inicio; i<i_Final ; i++)
			{
				ds.Tables[0].Rows.Add(InfoOriginal.Tables[0].Rows[i].ItemArray);
				for (int j=0; j<Relacion.Length; j++)
				{
					DataRow[] dr_Hijas = InfoOriginal.Tables[0].Rows[i].GetChildRows(Relacion[j]);
					if ( dr_Hijas.Length > 0 )
					{
						string s_Tabla = dr_Hijas[0].Table.TableName;
						foreach(DataRow dr in dr_Hijas)
							ds.Tables[s_Tabla].Rows.Add(dr.ItemArray);
					}
				}
			}

			UltimaPagina = NumeroPagina;
			return ds;
		}

	}
}
