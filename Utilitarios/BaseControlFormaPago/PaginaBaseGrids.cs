using System;
using System.Collections;
using System.Data;
using System.Text;
using Infragistics.WebUI.UltraWebGrid;

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
	/// Summary description for PaginaBaseGrids.
	/// </summary>
	public sealed class PaginaBaseGrids
	{
		internal PaginaBaseGrids()
		{}

		/// <summary>
		/// Insertar fila a un ultrawebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		public void InsertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid)
		{
			ultraWebGrid.Rows.Add();
			ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Activate();
		}

		/// <summary>
		/// Insertar fila a un ultrawebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		/// <param name="band">Band donde insertara</param>
		/// <param name="campos">Campos que se insertarán</param>
		public void InsertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, int band, Hashtable campos)
		{
			object valor;
			UltraGridRow ugr = ultraWebGrid.Bands[band].AddNew();
			if (campos != null)
			{
				foreach (string key in campos.Keys)
				{
					UltraGridRow ugrPadre;
					valor = campos[key];
					if (campos[key] == null)
					{
						//buscar en el papa 
						ugrPadre = ugr.ParentRow;
						valor = ugrPadre.Cells.FromKey(key).Value;
					}
					if (ugr.Cells.FromKey(key).Value == null)
					{
						ugr.Cells.FromKey(key).Value = valor;
					}
				}
			}
		}

		/// <summary>
		/// Insertar fila a un ultrawebGrid e inicializar algunos valores 
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
		/// <param name="campos">Hashtable donde las llaves corresponden al nombre del campo y el value es el valor a asignar</param>
		public void insertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, Hashtable campos)
		{
			ultraWebGrid.Rows.Add();
			ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Activate();

			foreach (string key in campos.Keys)
				ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Cells.FromKey(key).Value = campos[key];
		}

		/*--------------------------------------------------------------------------------------------------------------------------
			CREADO POR				: Luis Marcilla
			FECHA DE CREACION		: 14/12/2005
			MODIFICADO POR			: Luis Marcilla
			FECHA DE MODIFICACION	: 14/12/2005
			REVISADO POR			: Luis Marcilla
			FECHA DE REVISION		: 14/12/2005
		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Inserta una fila en la UltraWebGrid en la pagina correspondiente
		/// </summary>
		/// <param name="grid">UltraWebGrid con paginación al que se le inserta una fila</param>
		/// <param name="fuenteDataSet">Fuente de datos asociado a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		public void ControlarPaginacionAlInsertar(UltraWebGrid grid, DataSet fuenteDataSet, string nombreTabla )
		{
			if (grid.DisplayLayout.Pager.AllowPaging)
			{
				int nroRegistros = fuenteDataSet.Tables[nombreTabla].Rows.Count + 1;

				if(nroRegistros>1)
				{
					grid.DataSource = fuenteDataSet.Tables[nombreTabla];

					int paginaActual = Convert.ToInt32(Math.Ceiling(nroRegistros/(grid.DisplayLayout.Pager.PageSize *1.0)));

					grid.DisplayLayout.Pager.CurrentPageIndex = paginaActual;
					grid.DataBind();
				}
				else
				{
					grid.DisplayLayout.Pager.CurrentPageIndex = 0;
				}
			}
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		/// <param name="camposClavePrimaria">Campos que son llave primaria</param>
		public void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla, string[] camposClavePrimaria)
		{
			foreach (UltraGridRow ugr in ultraWebGrid.DisplayLayout.SelectedRows)
			{
				StringBuilder filtro = new StringBuilder();
				foreach (string campo in camposClavePrimaria)
				{
					if (ugr.Cells.FromKey(campo).Value == null)
					{
						filtro.Remove(0,filtro.Length);
						break;
					}
					if (filtro.Length > 0)
					{
						filtro.Append(" and ");
					}
					filtro.Append(campo);
					filtro.Append("=");
					filtro.Append(ugr.Cells.FromKey(campo).Value.ToString());
				}

				if (filtro.Length > 0)
				{
					DataRow[] filasEncontradas = dataSet.Tables[nombreTabla].Select(filtro.ToString());
					foreach (DataRow fila in filasEncontradas)
					{
						fila.Delete();
					}
				}
				ugr.Delete();
			}
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		public void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla)
		{
			UltraGridRow ugr = ultraWebGrid.DisplayLayout.SelectedRows[0];

			int clave = ObtenerColumnaIdentity(dataSet.Tables[nombreTabla]);
			string llave = String.Empty;

			if (clave == -1 && dataSet.Tables[nombreTabla].PrimaryKey != null)
			{
				llave = dataSet.Tables[nombreTabla].PrimaryKey[0].ColumnName;
			}
			else
			{
				llave = dataSet.Tables[nombreTabla].Columns[clave].ColumnName;
			}
			if (ugr != null)
			{
				string cod = ugr.Cells.FromKey(llave).Text;
				if (cod != null && !cod.Equals(""))
				{
					dataSet.Tables[nombreTabla].Rows.Find(cod).Delete();
				}
				ugr.Delete();
			}
		}

		/// <summary>
		/// Elimina una fila de un UltraWebGrid
		/// </summary>
		/// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
		/// <param name="dataSet">Fuente de datos asociados a la grilla</param>
		/// <param name="nombreTabla">Nombre de la tabla</param>
		/// <param name="llave">Clave primaria de la tabla</param>
		public void EliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla, string llave)
		{
			UltraGridRow ugr = ultraWebGrid.DisplayLayout.SelectedRows[0];
			if (ugr != null)
			{
				string cod = ugr.Cells.FromKey(llave).Text;
				if (cod != null && !cod.Equals(""))
				{
					dataSet.Tables[nombreTabla].Rows.Find(cod).Delete();
				}
				ugr.Delete();
			}
		}

		/// <summary>
		/// Recuperar el indice de la columna identity
		/// </summary>
		/// <param name="tabla">Del tipo DataTable</param>
		/// 
		private int ObtenerColumnaIdentity(DataTable tabla)
		{
			int indice = -1;
			for (int i = 0; i < tabla.Columns.Count; i++)
			{
				if (tabla.Columns[i].AutoIncrement)
				{
					indice = i;
				}
			}
			return indice;
		}

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTablas">arreglo de nombres de las tablas a actualizar, 0 tabla padre 1 tabla hija</param>
		/// <param name="llaves">arreglo de nombres de los campos PK</param>
		public void PreparaDatosAGrabarMaestroDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string[] nombreTablas, string[] llaves)
		{
			string nombreTabla;
			DataRow filaPadre = null;
			DataRow filaHija = null;
			string llave;

			foreach (UltraGridRow ugr in ultraWebGrid.Rows)
			{
				llave = llaves[0];
				nombreTabla = nombreTablas[0]; //tabla papa
				string cod = ugr.Cells.FromKey(llave).Text;

				if (cod == null || cod.Equals(""))
				{
					filaPadre = fuenteDatos.Tables[nombreTabla].NewRow();

					for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
					{
						if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
						{
							filaPadre[i] = Convert.ChangeType(ugr.Cells[i].Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
						}
					}

					fuenteDatos.Tables[nombreTabla].Rows.Add(filaPadre);
				}
				else // if(ugr.DataChanged == DataChanged.Modified)
				{
					for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
					{
						try
						{
							fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells[i].Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
						}
						catch
						{
							if (ugr.Cells[i] == null)
							{
								fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
							}
						}
					}
				}
				if (ugr.HasChildRows)
				{
					UltraGridRow ugrHijo;
					UltraGridRow ugrPadre;
					UltraGridRowsEnumerator uge = ultraWebGrid.Bands[1].GetRowsEnumerator();
					while (uge.MoveNext())
					{
						ugrHijo = uge.Current;
						ugrPadre = ugrHijo.ParentRow;

						if (ugr.Index == ugrPadre.Index)
						{
							llave = llaves[1];
							cod = ugrHijo.Cells.FromKey(llave).Text;
							nombreTabla = nombreTablas[1];
							if (cod == null || cod.Equals(""))
							{
								if (filaPadre == null)
								{
									filaPadre = fuenteDatos.Tables[nombreTablas[0]].NewRow();

									for (int i = 0; i < fuenteDatos.Tables[nombreTablas[0]].Columns.Count; i++)
									{
										filaPadre[i] = Convert.ChangeType(ugrPadre.Cells[i].Value, fuenteDatos.Tables[nombreTablas[0]].Columns[i].DataType);
									}

								}

								filaHija = fuenteDatos.Tables[nombreTablas[1]].NewRow();
								for (int j = 0; j < fuenteDatos.Tables[nombreTabla].Columns.Count; j++)
								{
									if (!fuenteDatos.Tables[nombreTabla].Columns[j].AutoIncrement)
									{
										if (ugrHijo.Cells[j].Value != null)
										{
											filaHija[j] = Convert.ChangeType(ugrHijo.Cells[j].Value, fuenteDatos.Tables[nombreTabla].Columns[j].DataType);
										}
									}
								}
								filaHija.SetParentRow(filaPadre);
								fuenteDatos.Tables[nombreTablas[1]].Rows.Add(filaHija);
							}
							else //  if(ugrHijo.DataChanged == DataChanged.Modified)
							{
								for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
								{
									try
									{
										fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugrHijo.Cells[i].Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
									}
									catch
									{
										if (ugrHijo.Cells[i] == null)
										{
											fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		/*--------------------------------------------------------------------------------------------------------------------------
			  CREADO POR              :
			  FECHA DE CREACION       :
			  MODIFICADO POR          : Carlos Esquivel
			  FECHA DE MODIFICACION   : 09/09/2005
			  REVISADO POR            :
			  FECHA DE REVISION       :
		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">llave primaria de la tabla a Grabar</param>
		/// <param name="comparaCeldas">Comparar a nivel de celdas</param>
		public void PrepararDatosAGrabar(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, bool comparaCeldas)
		{
			foreach (UltraGridRow ugr in ultraWebGrid.Rows)
			{
				string cod = ugr.Cells.FromKey(llave).Text;

				if (cod == null || cod.Equals(""))
				{
					DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						if (str_Nombre != null && str_Nombre.Length != 0)
						{
							if ((!fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].AutoIncrement) && ultraGridCell != null &&  ultraGridCell.Value != null)
							{
								dr[str_Nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
							}
						}
					}

					fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
				}
				else if (ExisteModificaciones(comparaCeldas, ugr))
				{
					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						try
						{
							fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
						}
						catch
						{
							if (ultraGridCell == null)
							{
								fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = DBNull.Value;
							}
						}
					}
				}
			}
		}

		/*--------------------------------------------------------------------------------------------------------------------------
			  CREADO POR              : Hernan Pereda
			  FECHA DE CREACION       :
			  MODIFICADO POR          :
			  FECHA DE MODIFICACION   :
			  REVISADO POR            :
			  FECHA DE REVISION       :
		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">llave primaria de la tabla a Grabar</param>
		/// <param name="comparaCeldas">Comparar a nivel de celdas</param>
		public void PrepararDatosAGrabarConColumnasExtras(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, bool comparaCeldas)
		{
			foreach (UltraGridRow ugr in ultraWebGrid.Rows)
			{

				if (ugr.Cells.FromKey(llave).Text==null )
				{
					DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						if (str_Nombre != null && str_Nombre.Length != 0)
						{

							if(fuenteDatos.Tables[nombreTabla].Columns.Contains(str_Nombre))
							{
								if ((!fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].AutoIncrement) && ultraGridCell != null && ultraGridCell.IsEditable() && ultraGridCell.Value != null)
								{
									dr[str_Nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
								}
							}
						}
					}

					fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
				}
				else if (ExisteModificaciones(comparaCeldas, ugr))
				{
					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						try
						{
							if(fuenteDatos.Tables[nombreTabla].Columns.Contains(str_Nombre))
							  fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
						}
						catch
						{
							if (ultraGridCell == null)
							{
								fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = DBNull.Value;
							}
						}
					}
				}
			}
		}
		/*--------------------------------------------------------------------------------------------------------------------------
		  CREADO POR              : Luis Marcilla
		  FECHA DE CREACION       : 08/08/2005
		  MODIFICADO POR          :
		  FECHA DE MODIFICACION   :
		  REVISADO POR            :
		  FECHA DE REVISION       :
		--------------------------------------------------------------------------------------------------------------------------*/

		/// <summary>
		/// Permite actualizar el dataset con los cambios efectuados en la grilla
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		/// <param name="llave">Nombre de la clave primaria de la tabla</param>
		/// <param name="comparaCeldas">Si desea comprobar si hay cambios</param>
		/// <param name="filaPadre">Fila padre del cual se requiere su clave</param>
		/// <param name="llavePadre">Nombre de la clave primaria de la fila padre</param>
		public void PrepararDatosAGrabarDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, string llave, bool comparaCeldas, DataRow filaPadre, string llavePadre)
		{
			foreach (UltraGridRow ugr in ultraWebGrid.Rows)
			{
				string cod = ugr.Cells.FromKey(llave).Text;

				if (cod == null || cod.Equals(""))
				{
					DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						if ((!fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].AutoIncrement) && ultraGridCell.Value != null && !str_Nombre.Equals(llavePadre))
						{
							dr[str_Nombre]		= Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
						}
					}

					dr.SetParentRow(filaPadre);
					fuenteDatos.Tables[nombreTabla].Rows.Add(dr);

				}
				else if (ExisteModificaciones(comparaCeldas, ugr))
				{
					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_Nombre = ultraGridCell.Column.BaseColumnName;

						try
						{
							fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].DataType);
						}
						catch
						{
							if (ultraGridCell == null)
							{
								fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_Nombre] = DBNull.Value;
							}
						}
					}

				}
			}

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
		/// Permite actualizar el dataset con los cambios efectuados en la grilla.
		/// </summary>
		/// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
		/// <param name="fuenteDatos">Fuente de datos a actualizar</param>
		/// <param name="nombreTabla">nombre de la tabla a actualizar</param>
		public void PrepararDatosAGrabarSinIdentity(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, bool comparaCeldas) //, string llave, string valorllave)
		{
			foreach (UltraGridRow ugr in ultraWebGrid.Rows)
			{
				if (ugr.Index + 1 > fuenteDatos.Tables[nombreTabla].Rows.Count)
				{
					DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_nombre = ultraGridCell.Column.BaseColumnName;

						if ((fuenteDatos.Tables[nombreTabla].Columns.Contains(str_nombre))&&(!fuenteDatos.Tables[nombreTabla].Columns[str_nombre].AutoIncrement) && ultraGridCell != null &&  ultraGridCell.Value != null)
						{
							dr[str_nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_nombre].DataType);
						}
					}
					fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
				}
				else if (ExisteModificaciones(comparaCeldas, ugr))
				{
					foreach (UltraGridCell ultraGridCell in ugr.Cells)
					{
						string str_nombre = ultraGridCell.Column.BaseColumnName;

						try
						{
							if((fuenteDatos.Tables[nombreTabla].Columns.Contains(str_nombre)))
							fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_nombre] = Convert.ChangeType(ultraGridCell.Value, fuenteDatos.Tables[nombreTabla].Columns[str_nombre].DataType);
						}
						catch
						{
							if (ultraGridCell == null)
							{
								fuenteDatos.Tables[nombreTabla].Rows[ugr.Index][str_nombre] = DBNull.Value;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Verificar si existe modificaciones en la fila de la grilla
		/// </summary>
		/// <param name="comparaCeldas">Comparación a nivel de celdas</param>
		/// <param name="fila">Fila de la grilla</param>
		/// <returns>Existe modificación</returns>
		private bool ExisteModificaciones(bool comparaCeldas, UltraGridRow fila)
		{
			bool existeModificacion = false;
			if (fila.DataChanged == DataChanged.Modified)
			{
				existeModificacion = true;
			}
			else
			{
				if (comparaCeldas)
				{
					foreach (UltraGridCell celda in fila.Cells)
					{
						if (celda.DataChanged)
						{
							existeModificacion = true;
							break;
						}

					}
				}
			}
			return existeModificacion;

		}
	}
}
