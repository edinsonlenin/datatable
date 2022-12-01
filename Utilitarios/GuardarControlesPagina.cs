using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Seriva.Bolsa.Herramientas.Controles;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Almacena los valores de los controles de una página TextBox, DropDownList, HtmlInputHidden
    /// </summary>
    public class GuardarControlesPagina: ISerializable
	{
		/// <summary>
		/// Almacena los valores de los controles TextBox, DropDownList, HtmlInputHidden
		/// </summary>
		Hashtable valoresControles = new Hashtable();

		protected string a;

		#region Definición de campo
		/// <summary>
		/// Clase interna que guarda el nombre del campo y su valor
		/// </summary>
		private class Campo: ISerializable
		{
			/// <summary>
			/// Corresponde a la propiedad del control para 
			/// </summary>
			private string papa = string.Empty;
			/// <summary>
			/// Corresponde al nombre del control
			/// </summary>
			private string nombre = string.Empty;
			/// <summary>
			/// Corresponde al valor contenido en el control
			/// </summary>
			private string valor = string.Empty;
			/// <summary>
			/// Corresponde a la propiedad del control que indica si esta visible o no
			/// </summary>
			private bool visible = true;
			/// <summary>
			/// Corresponde a la propiedad del control que indica si es modificable o no
			/// </summary>
			private bool lectura = false;
			/// <summary>
			/// Corresponde a la propiedad del control que indica el ancho del control
			/// </summary>
			private int   ancho = 0;

			/// <summary>
			/// Constructor de la subclase Campo, que setea los atributos del control en la clase campo y su valor se lo utiliza
			/// en controles textbox, y sus derivados
			/// </summary>
			/// <param name="Papa">Corresponde al identificador del control que contiene al control actual</param>
			/// <param name="Nombre">Corresponde al identificador del control</param>
			/// <param name="Valor">Correspnde al valor seteado en el control</param>
			/// <param name="Visible">Corresponde a la propiedad del control que indica si es visible o no</param>
			/// <param name="Lectura">Corresponde a la propiedad del control que indica si es editable o no</param>
			/// <param name="Ancho">Corresponde a la propiedad que indica el ancho del control</param>
			public Campo(string Papa, string Nombre, string Valor, bool Visible, bool Lectura, int Ancho)
			{
				if ( Papa != null )
					papa = Papa;
				if ( Nombre != null )
					nombre = Nombre;
				if ( Valor != null )
					valor = Valor;

				visible = Visible;
				lectura = Lectura;
				ancho = Ancho;
			}

			/// <summary>
			/// Clase interna que guarda el nombre del campo y su valor se lo utiliza
			/// para controles HtmlInputHidden, Label1, Button
			/// </summary>
			/// <param name="Papa">Corresponde al identificador del control que contiene al control actual</param>
			/// <param name="Nombre">Corresponde al identificador del control</param>
			/// <param name="Valor">Correspnde al valor seteado en el control</param>
			/// <param name="Visible">Corresponde a la propiedad del control que indica si es visible o no</param>
			public Campo(string Papa, string Nombre, string Valor, bool Visible)
			{
				if ( Papa != null )
					papa = Papa;
				if ( Nombre != null )
					nombre = Nombre;
				if ( Valor != null )
					valor = Valor;

				visible = Visible;
			}

			/// <summary>
			/// Método para deserializar un control y setea los atributos privados de la subclase
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public Campo(SerializationInfo info, StreamingContext ctx)
			{
				papa	= info.GetString("papa");
				nombre	= info.GetString("nombre");
				valor	= info.GetString("valor");
				visible = info.GetBoolean("visible");
				lectura = info.GetBoolean("lectura");
				ancho	= info.GetInt32("ancho");
			}

			/// <summary>
			/// Método para serializar un control y almacenar los valores de los atributos en el objeto tipo SerializationInfo
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public void GetObjectData(SerializationInfo info, StreamingContext ctx)
			{
				info.AddValue("papa",papa);
				info.AddValue("nombre",nombre);
				info.AddValue("valor",valor);
				info.AddValue("visible",visible);
				info.AddValue("lectura",lectura);
				info.AddValue("ancho",ancho);
			}

			/// <summary>
			/// Corresponde al identificador del control que contiene al control actual
			/// </summary>
			public string Papa
			{
				get
				{
					return papa;
				}
			}

			/// <summary>
			/// Corresponde al identificador del control
			/// </summary>
			public string Nombre
			{
				get
				{
					return nombre;
				}
			}

			/// <summary>
			/// Correspnde al valor seteado en el control
			/// </summary>
			public string Valor
			{
				get
				{
					return valor;
				}

				set
				{
					valor = value;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad del control que indica si es visible o no
			/// </summary>
			public bool Visible
			{
				get
				{
					return visible;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad del control que indica si es editable o no
			/// </summary>
			public bool Lectura
			{
				get
				{
					return lectura;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad que indica el ancho del control
			/// </summary>
			public int Ancho
			{
				get
				{
					return ancho;
				}
			}

		}
		#endregion Definición de campo

		#region Clase definición DropDown
		/// <summary>
		/// Clase interna que guarda la informacion de dropdown
		/// </summary>
		private class DropDown: ISerializable
		{
			private string papa		= string.Empty;
			private string nombre	= string.Empty;
			private string valor	= string.Empty;
			private bool visible	= true;
			private int ancho		= 0;
			private SortedList lista= new SortedList();

			/// <summary>
			/// Constructor de la subclase DropDown
			/// </summary>
			/// <param name="Papa">Corresponde al identificador del control que contiene al control actual</param>
			/// <param name="Nombre">Corresponde al identificador del control</param>
			/// <param name="Valor">Correspnde al valor seteado en el control</param>
			/// <param name="Visible">Corresponde a la propiedad del control que indica si es visible o no</param>
			/// <param name="Lectura">Corresponde a la propiedad del control que indica si es editable o no</param>
			/// <param name="Ancho">Corresponde a la propiedad que indica el ancho del control</param>
			/// <param name="Lista">Corresponde al objeto tipo ListItemCollection que contiene los items del dropdown</param>
			public DropDown(string Papa, string Nombre, string Valor, bool Visible, int Ancho, ListItemCollection Lista)
			{
				if ( Papa != null )
					papa = Papa;
				if ( Nombre != null )
					nombre = Nombre;
				if ( Valor != null )
					valor = Valor;

				visible = Visible;
				ancho = Ancho;
				foreach(ListItem li in Lista)
					lista.Add(li.Value,li.Text);
			}

			/// <summary>
			/// Método para deserializar un control y setea los atributos privados de la subclase
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public DropDown(SerializationInfo info, StreamingContext ctx)
			{
				papa	= info.GetString("papa");
				nombre	= info.GetString("nombre");
				valor	= info.GetString("valor");
				visible = info.GetBoolean("visible");
				ancho	= (int)info.GetValue("ancho", ancho.GetType());
				lista	= (SortedList)info.GetValue("lista",lista.GetType());
			}

			/// <summary>
			/// Método para serializar un control y almacenar los valores de los atributos en el objeto tipo SerializationInfo
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public void GetObjectData(SerializationInfo info, StreamingContext ctx)
			{
				info.AddValue("papa",papa);
				info.AddValue("nombre",nombre);
				info.AddValue("valor",valor);
				info.AddValue("visible",visible);
				info.AddValue("ancho",ancho);
				info.AddValue("lista",lista);
			}

			/// <summary>
			/// Corresponde al identificador del control que contiene al control actual
			/// </summary>
			public string Papa
			{
				get
				{
					return papa;
				}
			}

			/// <summary>
			/// Corresponde al identificador del control
			/// </summary>
			public string Nombre
			{
				get
				{
					return nombre;
				}
			}

			/// <summary>
			/// Correspnde al valor seteado en el control
			/// </summary>
			public string Valor
			{
				get
				{
					return valor;
				}

				set
				{
					valor = value;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad del control que indica si es visible o no
			/// </summary>
			public bool Visible
			{
				get
				{
					return visible;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad que indica el ancho del control
			/// </summary>
			public int Ancho
			{
				get
				{
					return ancho;
				}
			}

			/// <summary>
			/// Corresponde a la lista de items a ser seteados en el dropdown
			/// </summary>
			public SortedList Lista
			{
				get
				{
					return lista;
				}
			}

		}
		#endregion Clase definición DropDown

		#region Clase del WebCombo
		/// <summary>
		/// Clase interna que guarda la informacion de dropdown
		/// </summary>
		private class claseWebCombo: ISerializable
		{
			private string papa				= string.Empty;
			private string nombre			= string.Empty;
			private string valor			= string.Empty;
			private string nombreCampoValor	= string.Empty;
			private string nombreCampoTexto	= string.Empty;
			private bool visible			= true;
			private int ancho				= 0;
			private DataSet dsListaFilas;

			/// <summary>
			/// Constructor de la subclase DropDown
			/// </summary>
			/// <param name="Papa">Corresponde al identificador del control que contiene al control actual</param>
			/// <param name="Nombre">Corresponde al identificador del control</param>
			/// <param name="Valor">Correspnde al valor seteado en el control</param>
			/// <param name="Visible">Corresponde a la propiedad del control que indica si es visible o no</param>
			/// <param name="Lectura">Corresponde a la propiedad del control que indica si es editable o no</param>
			/// <param name="Ancho">Corresponde a la propiedad que indica el ancho del control</param>
			/// <param name="Lista">Corresponde al objeto tipo ListItemCollection que contiene los items del dropdown</param>
			public claseWebCombo(string Papa, string Nombre, string Valor, bool Visible, int Ancho, Infragistics.WebUI.UltraWebGrid.RowsCollection Rows,string NombreCampoValor,string NombreCampoTexto)
			{
				if ( Papa != null )
					papa = Papa;
				if ( Nombre != null )
					nombre = Nombre;
				if ( Valor != null )
					valor = Valor;

				nombreCampoValor	= NombreCampoValor;
				nombreCampoTexto	= NombreCampoTexto;
				visible				= Visible;
				ancho				= Ancho;
				DataSet ds			= new DataSet();
				ds.Tables.Add("Tabla");
				ds.Tables[0].Columns.Add(NombreCampoValor,typeof(String));
				ds.Tables[0].Columns.Add(NombreCampoTexto,typeof(String));
				foreach(UltraGridRow item in Rows)
				{
					try
					{
						DataRow dr				= ds.Tables[0].NewRow();
						dr[NombreCampoValor]	= item.Cells.FromKey(NombreCampoValor).Value.ToString();
						dr[NombreCampoTexto]	= item.Cells.FromKey(NombreCampoTexto).Value.ToString();
						ds.Tables[0].Rows.Add(dr);
					}
					catch{}
				}
				dsListaFilas=ds;
			}

			/// <summary>
			/// Método para deserializar un control y setea los atributos privados de la subclase
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public claseWebCombo(SerializationInfo info, StreamingContext ctx)
			{
				papa				= info.GetString("papa");
				nombre				= info.GetString("nombre");
				valor				= info.GetString("valor");
				nombreCampoValor	= info.GetString("nombreCampoValor");
				nombreCampoTexto	= info.GetString("nombreCampoTexto");
				visible				= info.GetBoolean("visible");
				ancho				= (int)info.GetValue("ancho", ancho.GetType());
				dsListaFilas		= (DataSet)info.GetValue("lista",dsListaFilas.GetType());
			}

			/// <summary>
			/// Método para serializar un control y almacenar los valores de los atributos en el objeto tipo SerializationInfo
			/// </summary>
			/// <param name="info">Corresponde al objeto SerializationInfo</param>
			/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public void GetObjectData(SerializationInfo info, StreamingContext ctx)
			{
				info.AddValue("papa",papa);
				info.AddValue("nombre",nombre);
				info.AddValue("valor",valor);
				info.AddValue("nombreCampoValor",nombreCampoValor);
				info.AddValue("nombreCampoTexto",nombreCampoTexto);
				info.AddValue("visible",visible);
				info.AddValue("ancho",ancho);
				info.AddValue("lista",dsListaFilas);
			}

			/// <summary>
			/// Corresponde al identificador del control que contiene al control actual
			/// </summary>
			public string Papa
			{
				get
				{
					return papa;
				}
			}

			/// <summary>
			/// Corresponde al identificador del control
			/// </summary>
			public string Nombre
			{
				get
				{
					return nombre;
				}
			}

			/// <summary>
			/// Correspnde al valor seteado en el control
			/// </summary>
			public string Valor
			{
				get
				{
					return valor;
				}

				set
				{
					valor = value;
				}
			}

			/// <summary>
			/// Corresponde al DataValueField del webcombo
			/// </summary>
			public string NombreCampoValor
			{
				get
				{
					return nombreCampoValor;
				}
			}

			/// <summary>
			/// Corresponde al DataTextField del webcombo
			/// </summary>
			public string NombreCampoTexto
			{
				get
				{
					return nombreCampoTexto;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad del control que indica si es visible o no
			/// </summary>
			public bool Visible
			{
				get
				{
					return visible;
				}
			}

			/// <summary>
			/// Corresponde a la propiedad que indica el ancho del control
			/// </summary>
			public int Ancho
			{
				get
				{
					return ancho;
				}
			}

			/// <summary>
			/// Corresponde a la lista de items a ser seteados en el dropdown
			/// </summary>
			public DataSet Fuente
			{
				get
				{
					return dsListaFilas;
				}
			}

		}

		#endregion Clase del WebCombo

		/// <summary>
		/// Método obligatorio para deserializar  la clase
		/// </summary>
		/// <param name="info">Corresponde al objeto SerializationInfo</param>
		/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
		public GuardarControlesPagina(SerializationInfo info, StreamingContext ctx)
		{
			valoresControles = (Hashtable) info.GetValue("ht",typeof(Hashtable));
		}

		/// <summary>
		/// Método obligatorio para serializar la clase
		/// </summary>
		/// <param name="info">Corresponde al objeto SerializationInfo</param>
		/// <param name="ctx">Conrresponde al objeto de tipo StreamingContext</param>
			public void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("ht",valoresControles);
		}

		/// <summary>
		/// Carga los valores de los controles de la página de parametro
		/// </summary>
		/// <param name="pg_Pagina">Página que tiene los controles origen de los valores</param>
		public GuardarControlesPagina(System.Web.UI.Page pg_Pagina)
		{

			CargarControlesPagina(pg_Pagina, null);
		}

		/// <summary>
		/// Recursiva que carga los controles de la página de parametro
		/// </summary>
		/// <param name="pg_Pagina">Página que tiene los controles origen de los valores</param>
		/// <param name="ctrl_Papa">Control actual o null en la primera llamada</param>
		private void CargarControlesPagina(System.Web.UI.Page pg_Pagina, System.Web.UI.Control ctrl_Papa)
		{
			if ( ctrl_Papa == null )
			{
				foreach(System.Web.UI.Control ctrl in pg_Pagina.Controls)
					cargarControles(ctrl,pg_Pagina);
			}
			else
			{
				foreach(System.Web.UI.Control ctrl in ctrl_Papa.Controls)
					cargarControles(ctrl,pg_Pagina);
			}
		}

		/// <summary>
		/// Carga los campos de la clase en los valores de los controles correspondientes de la página de parametro
		/// </summary>
		/// <param name="pg_Pagina">Página que tiene los controles destino de los valores</param>
		public void AsignarControlesPagina(System.Web.UI.Page pg_Pagina)
		{
			Leer(pg_Pagina, null);
		}

		/// <summary>
		/// Asigna un valor en el miembro de la clase correspondiente al Identificador del control
		/// </summary>
		/// <param name="s_ID">Identificador del control</param>
		/// <param name="s_Valor">Valor del control</param>
		/// <returns>0 todo bien, -1 hubo un error</returns>
		public int AsignarValorClase(string controlID, string valor)
		{
			if ( valoresControles[controlID]!= null )
			{
				switch (valoresControles[controlID].GetType().Name)
				{
					case "DropDown":
						((DropDown)valoresControles[controlID]).Valor		= valor;
						return 0;
					case "WebCombo":
						((claseWebCombo)valoresControles[controlID]).Valor = valor;
						return 0;
					case "Campo":
						((Campo)valoresControles[controlID]).Valor			= valor;
						return 0;
					default:
						return -1;
				}
			}
			else
				return -1;
		}

		/// <summary>
		/// Recursiva que carga los controles de la página de parametro
		/// </summary>
		/// <param name="pg_Pagina">Página que tiene los controles origen de los valores</param>
		/// <param name="ctrl_Papa">Control actual o null en la primera llamada</param>
		private void Leer(System.Web.UI.Page pg_Pagina, System.Web.UI.Control ctrl_Papa)
		{
			if ( ctrl_Papa == null )
			{
				foreach(System.Web.UI.Control ctrl in pg_Pagina.Controls)
					llenarControles(ctrl,valoresControles,pg_Pagina);
			}
			else
			{
				foreach(System.Web.UI.Control ctrl in ctrl_Papa.Controls)
					llenarControles(ctrl,valoresControles,pg_Pagina);
			}
		}

		/// <summary>
		/// Setea el valor de un control
		/// </summary>
		/// <param name="ctrl">Control en el que se coloca el valor</param>
		/// <returns>true todo bien, false caso contrario</returns>
		public bool AsignarControl(System.Web.UI.Control control)
		{
			if ( control == null )
				return false;

			string identificadorControl = control.ID;

			if ( valoresControles[identificadorControl] != null )
			{
				llenarControles(control,valoresControles,null);
			}
			else
				return false;
			return true;
		}

		#region Retorna el valor que tenìa el control
		/// <summary>
		/// Devuelve el valor que tenía el control
		/// </summary>
		/// <param name="ctrl">Control que se desea recuperar</param>
		/// <returns>Valor del control o null</returns>
		public string ValorControl(System.Web.UI.Control control)
		{
			if ( control == null )
				return null;

			string identificadorControl = control.ID;
			Campo objetoControl;
			DropDown dropDown;
			claseWebCombo webCombo;

			if ( valoresControles[identificadorControl] != null )
			{
				switch (valoresControles[control.ID].GetType().Name)
				{
					case "DropDown":
						dropDown = (DropDown)valoresControles[control.ID];
						return dropDown.Valor;
					case "claseWebCombo":
						webCombo = (claseWebCombo)valoresControles[control.ID];
						return webCombo.Valor;
					case "Campo":
						objetoControl = (Campo)valoresControles[control.ID];
						return objetoControl.Valor;
					default:
						return null;
				}
			}
			else
				return null;
		}
		#endregion Retorna el valor que tenìa el control

		#region LLenado de controles SP 02/dic/2004

		/// <summary>
		/// Llenar los controles de una pagina con inf. de las sesion
		/// </summary>
		/// <param name="control"></param>
		/// <param name="ht"></param>
		/// <param name="pg_Pagina"></param>
		/// <returns></returns>
		private bool llenarControles(System.Web.UI.Control control,Hashtable contenedorControles,System.Web.UI.Page pg_Pagina)
		{
			Campo			objetoControl;
			DropDown		dropDown;
			claseWebCombo	webCombo;
			UtilitarioFormatos formatos = new UtilitarioFormatos();

			switch(control.GetType().Name)
			{
				case "DropDownList":
					if ( contenedorControles[control.ID] != null )
					{
						((DropDownList)control).ClearSelection();
						dropDown = (DropDown)contenedorControles[control.ID];

						// Cargo los valores
						int i_Total = dropDown.Lista.Count;
						for (int i=0; i<i_Total; i++)
						{
							if ( ((DropDownList)control).Items.FindByValue(dropDown.Lista.GetKey(i).ToString()) == null )
							{
								ListItem li = new ListItem(dropDown.Lista.GetByIndex(i).ToString(),dropDown.Lista.GetKey(i).ToString());
								((DropDownList)control).Items.Add(li);
							}
						}

						// visible
						((DropDownList)control).Visible = dropDown.Visible;
						// ancho
						((DropDownList)control).Width= dropDown.Ancho;

						// Seteo la opción
						if ( dropDown.Valor != "" )
						{
							if ( ((DropDownList)control).Items.FindByValue(dropDown.Valor) != null )
								((DropDownList)control).Items.FindByValue(dropDown.Valor).Selected = true;
							else
							{
								int i_NumItem = ((DropDownList)control).Items.Count;
								((DropDownList)control).Items.Insert(i_NumItem,dropDown.Valor);
								((DropDownList)control).Items[i_NumItem].Selected = true;
							}
						}
					}
					break;
				case "WebCombo":
					if ( contenedorControles[control.ID]!= null )
					{
						webCombo = (claseWebCombo)contenedorControles[control.ID];
						// Cargo los valores
						((WebCombo)control).DataSource=webCombo.Fuente;
						((WebCombo)control).DataTextField= webCombo.NombreCampoTexto;
						((WebCombo)control).DataValueField = webCombo.NombreCampoValor;
						((WebCombo)control).DataBind();
						for(int i=0; i< ((WebCombo)control).Columns.Count ; i++)
						{
							if(((WebCombo)control).Columns[i].BaseColumnName != webCombo.NombreCampoTexto)
								((WebCombo)control).Columns[i].Hidden = true;
						}
						// visible
						((WebCombo)control).Visible = webCombo.Visible;
						// ancho
						((WebCombo)control).Width = webCombo.Ancho;
						// Campo de datos

						// asignar la opción
						if ( webCombo.Valor != "" )
						{
							int index = 0;
							foreach(UltraGridRow item in ((WebCombo)control).Rows)
							{
								string item_value = item.Cells.FromKey(((WebCombo)control).DataValueField).Value.ToString();
								if (item_value.Equals(webCombo.Valor))
								{
									((WebCombo)control).SelectedIndex = index;
									break;
								}
								index = index + 1;
							}
						}
					}
					break;
				case "TextBox":
					if ( contenedorControles[control.ID]!= null )
					{
						if (control.ID == "tb_CodEmision" )
							this.a = "a";
						objetoControl = (Campo)contenedorControles[control.ID];
						((TextBox)control).Text = objetoControl.Valor;
						((TextBox)control).Visible = objetoControl.Visible;
						((TextBox)control).Width = objetoControl.Ancho;
						((TextBox)control).ReadOnly = objetoControl.Lectura;
					}
					break;
				case "HtmlInputHidden":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						((HtmlInputHidden)control).Value = objetoControl.Valor;
						((HtmlInputHidden)control).Visible = objetoControl.Visible;
					}
					break;
				case "ValidaIngresoNumero":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						if (control.ID == "tb_ValorNominal")
							a= "1";
						((ValidaIngresoNumero)control).Text		= formatos.of_StrAStrNumero( objetoControl.Valor );
						((ValidaIngresoNumero)control).Visible	= objetoControl.Visible;
						((ValidaIngresoNumero)control).Width	= objetoControl.Ancho;
						((ValidaIngresoNumero)control).ReadOnly = objetoControl.Lectura;
					}
					break;
				case "TextoSimple":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						((TextoSimple)control).Text = objetoControl.Valor;
						((TextoSimple)control).Visible = objetoControl.Visible;
						((TextoSimple)control).Width = objetoControl.Ancho;
						((TextoSimple)control).ReadOnly = objetoControl.Lectura;
					}
					break;
				case "ValidaIngresoFecha":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						((ValidaIngresoFecha)control).Text = objetoControl.Valor;
						((ValidaIngresoFecha)control).Visible = objetoControl.Visible;
						((ValidaIngresoFecha)control).Width = objetoControl.Ancho;
						((ValidaIngresoFecha)control).ReadOnly = objetoControl.Lectura;
					}
					break;
				case "Label":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						((Label)control).Visible = objetoControl.Visible;
						((Label)control).Text = objetoControl.Valor;
					}
					break;
				case "Button":
					if ( contenedorControles[control.ID]!= null )
					{
						objetoControl = (Campo)contenedorControles[control.ID];
						((Button)control).Visible = objetoControl.Visible;
						((Button)control).Text = objetoControl.Valor;
					}
					break;
				default:
					if(pg_Pagina!=null)
						Leer(pg_Pagina, control);
					else
						return false;
					break;
			}
			return true;
		}
		#endregion Llenado de controles SP 02/dic/2004

		#region Carga de los controles en los objetos de las clases

		/// <summary>
		/// Carga los controles en los objetos de la clase
		/// </summary>
		/// <param name="control">control</param>
		/// <param name="pg_Pagina">pag. a cargar</param>
		private void cargarControles(System.Web.UI.Control control,System.Web.UI.Page pg_Pagina)
		{
			Campo			objetoControl;
			DropDown		dropDown;
			claseWebCombo	webCombo;
			UtilitarioFormatos formatos = new UtilitarioFormatos();

			switch(control.GetType().Name)
				{
					case "DropDownList":
					if ( ((DropDownList)control).SelectedItem != null )
						dropDown = new DropDown(pg_Pagina.ID, control.ID, ((DropDownList)control).SelectedItem.Value,
							((DropDownList)control).Visible, Convert.ToInt32(((DropDownList)control).Width), ((DropDownList)control).Items);
					else
						dropDown = new DropDown(pg_Pagina.ID, control.ID, null,
							((DropDownList)control).Visible, Convert.ToInt32(((DropDownList)control).Width), ((DropDownList)control).Items);
					valoresControles.Add(control.ID, dropDown);
					break;
				case "WebCombo":
					try
							{
						webCombo = new claseWebCombo(pg_Pagina.ID, control.ID, ((WebCombo)control).DataValue.ToString(),
							((WebCombo)control).Visible, Convert.ToInt32(((WebCombo)control).Width), ((WebCombo)control).Rows,((WebCombo)control).DataValueField,((WebCombo)control).DataTextField);
								}
					catch
							{
						webCombo = new claseWebCombo(pg_Pagina.ID, control.ID, null,
							((WebCombo)control).Visible, Convert.ToInt32(((WebCombo)control).Width), ((WebCombo)control).Rows,((WebCombo)control).DataValueField,((WebCombo)control).DataTextField);
						}
					valoresControles.Add(control.ID, webCombo);
					break;
				case "HtmlInputHidden":
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((HtmlInputHidden)control).Value,
						((HtmlInputHidden)control).Visible);
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "TextBox":
						if (control.ID == "tb_CodEmision" )
							this.a = "a";
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((TextBox)control).Text,
						((TextBox)control).Visible, ((TextBox)control).ReadOnly, Convert.ToInt32(((TextBox)control).Width));
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "ValidaIngresoNumero":
							if (control.ID == "tb_ValorNominal")
						a = string.Empty;
					objetoControl = new Campo(pg_Pagina.ID, control.ID, formatos.of_StrAStrNumero(((ValidaIngresoNumero)control).Text),
						((ValidaIngresoNumero)control).Visible, ((ValidaIngresoNumero)control).ReadOnly , Convert.ToInt32(((ValidaIngresoNumero)control).Width));
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "TextoSimple":
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((TextoSimple)control).Text,
						((TextoSimple)control).Visible, ((TextoSimple)control).ReadOnly, Convert.ToInt32(((TextoSimple)control).Width));
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "ValidaIngresoFecha":
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((ValidaIngresoFecha)control).Text,
						((ValidaIngresoFecha)control).Visible, ((ValidaIngresoFecha)control).ReadOnly, Convert.ToInt32(((ValidaIngresoFecha)control).Width));
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "Label":
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((Label)control).Text,
						((Label)control).Visible);
					valoresControles.Add(control.ID, objetoControl);
						break;
					case "Button":
					objetoControl = new Campo(pg_Pagina.ID, control.ID, ((Button)control).Text,
						((Button)control).Visible);
					valoresControles.Add(control.ID, objetoControl);
						break;
					default:
					CargarControlesPagina(pg_Pagina, control);
					break;
			}
		}
		#endregion Carga de los controles en los objetos de las clases

		#region Retorna el valor que tenìa el control por ID
		/// <summary>
		/// Devuelve el valor que tenía el control
		/// </summary>
		/// <param name="s_ID">Identificador del control</param>
		/// <returns>Valor del control o null</returns>
		public string ValorControlPorID(string identificadorControl)
		{
			if ( identificadorControl == null )
				return null;

			Campo			objetoControl;
			DropDown		dropDown;
			claseWebCombo	webCombo;

			if ( valoresControles[identificadorControl] != null )
			{
				switch (valoresControles[identificadorControl].GetType().Name)
				{
					case "DropDown":
						dropDown = (DropDown)valoresControles[identificadorControl];
						return dropDown.Valor;
					case "claseWebCombo":
						webCombo = (claseWebCombo)valoresControles[identificadorControl];
						return webCombo.Valor;
					case "Campo":
						objetoControl = (Campo)valoresControles[identificadorControl];
						return objetoControl.Valor;
					default:
						return null;
				}

			}
			else
				return null;
		}
		#endregion Retorna el valor que tenìa el control por ID
	}
}
