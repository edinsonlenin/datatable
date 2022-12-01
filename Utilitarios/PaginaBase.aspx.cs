using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebTab;
using Infragistics.WebUI.WebCombo;
using Seriva.Bolsa.Entidad.Seguridad;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Bolsa.Herramientas.Utilitarios;
using Seriva.Bolsa.Negocio.General;
using Seriva.Bolsa.Negocio.Seguridad;
using Seriva.Utilitarios;
using Seriva.UtilitariosWebII.Persistence;
using MensajeAplicacion = Seriva.Bolsa.Herramientas.Lenguaje.MensajeAplicacion;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Summary description for BaseWebForm.
    /// </summary>
    public class PaginaBase : Page
    {
        protected MensajeAplicacion MensajeAplicacion;

        #region CurrentInstanceName

        public string CurrentInstanceName
        {
            get
            {

                return Sesion.CurrentInstance;
            }
        }

        #endregion

        #region Formatos

        #region Propiedades

        public Hashtable ObtieneFormatosSession()
        {
            //Hashtable formatosHash = (Hashtable)Session[ConstantesSession.FORMATOS_BOLSA];
            Hashtable formatosHash = null;//Conversores.convertirDataTableaHashtable((DataTable)Session[ConstantesSession.FORMATOS_BOLSA]);
            if (formatosHash == null)
            {
                formatosHash = new Helper(CurrentInstanceName).ObtenerFormatosBolsa();
                //Session.Add(ConstantesSession.FORMATOS_BOLSA,Conversores.convertirHashtableaDataTable(formatosHash));
            }

            return formatosHash;
        }

        protected string ObtieneFormatoSession(e_Formatos eFormato)
        {
            Hashtable formatosHash = (Hashtable)Session[ConstantesSession.FORMATOS_BOLSA];
            if (formatosHash == null)
            {
                formatosHash = new Helper(CurrentInstanceName).ObtenerFormatosBolsa();
                Session.Add(ConstantesSession.FORMATOS_BOLSA, formatosHash);
            }
            return Convert.ToString(formatosHash[eFormato].ToString());
        }

        public string FormatoFechaAplicacion
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoFechaAplicacion); }
        }

        public string FormatoFechaUsuario
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoFechaUsuario); }
        }

        public string FormatoHora
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoHora); }
        }

        public string FormatoTasa
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoTasa); }
        }

        public string FormatoTasaSubasta
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoTasaSubasta); }
        }

        public string FormatoMonto
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoMonto); }
        }
        public string FormatoMontoPoliza
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoMontoPoliza); }
        }

        public string FormatoPrecio
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecio); }
        }
        public string FormatoPrecioRV
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioRV); }
        }
        public string FormatoPrecioEXRV
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioEXRV); }
        }
        public string FormatoPrecioRF
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioRF); }
        }
        public string FormatoPrecioEXRF
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioEXRF); }
        }
        public string FormatoPrecioPorcentaje
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioPorcentaje); }
        }

        public string FormatoPorcentaje
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPorcentaje); }
        }

        public string FormatoCantidad
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoCantidad); }
        }

        public string FormatoMontoOPR
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoMontoOPR); }
        }
        public string FormatoPrecioOPR
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioOPR); }
        }
        public string FormatoTasaOPR
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoTasaOPR); }
        }
        public string FormatoPorcentajeOPR
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPorcentajeOPR); }
        }
        public string FormatoTipoCambio
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoTipoCambio); }
        }
        public string FormatoIndicadorVAC
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoIndicadorVAC); }
        }
        public string FormatoFactorVAC
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoFactorVAC); }
        }

        public string FormatoDuracionBono
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoDuracionBono); }
        }

        //CZS|ST000044782|Atributos: Nro Decimales y Tipo de Redondeo para Precio de Operación
        // de Operaciones de Reporte
        public string NroDecimalPrecioOperacion
        {
            get { return ObtieneFormatoSession(e_Formatos.NroDecimalPrecioOperacion); }
        }

        public string FlagRedondeoPrecioOperacion
        {
            get { return ObtieneFormatoSession(e_Formatos.FlagRedondeoPrecioOperacion); }
        }

        public string FormatoPrecioOperacionOPR
        {
            get { return ObtieneFormatoSession(e_Formatos.FormatoPrecioOperacionOPR); }
        }

        #endregion

        #endregion

        private Page pagina;

        protected internal bs_Sesion Sesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
            if (IsPostBack)
            {
                string target = Request["__EVENTTARGET"];
                string parameter = Request["__EVENTARGUMENT"];

                if (target != "" && target != null)
                {
                    if (parameter != "undefined")
                    {
                        var parameters = new Object[] { parameter };
                        var methodInfo = this.GetType().GetMethod(target, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
                        if (methodInfo != null)
                            methodInfo.Invoke(this, parameters);
                    }
                    else
                    {
                        var methodInfo = this.GetType().GetMethod(target, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { }, null);
                        if (methodInfo != null)
                            methodInfo.Invoke(this, null);

                    }
                }

            }

            if (sender != null)
            {
                pagina = (Page)sender;
            }

            if (Session[bs_Sesion.ID_Sesion] == null)
            {
                Response.Redirect("~/Framework/Caducidad.aspx");
            }

            Sesion = (bs_Sesion)Session[bs_Sesion.ID_Sesion];
            FuncionesBolsa.CurrentInstanceName = CurrentInstanceName;
        }
        protected void SeteaFuncionesBolsa()
        {
            FuncionesBolsa.CurrentInstanceName = CurrentInstanceName;
        }

        /// <summary>
        /// Registra las funciones javascript a emplear el grid
        /// </summary>
        private void registrarEventosUltraWebGridCatalogo()
        {
            StringBuilder MyScript = new StringBuilder();
            MyScript.Append("<script language=\"javascript\">");
            MyScript.Append("function cambiarMayusculaGrid(gridName, cellId)");
            MyScript.Append("{");
            MyScript.Append("var cell = igtbl_getCellById(cellId);");
            MyScript.Append("var contenido = cell.getValue();");
            MyScript.Append("cell.setValue(contenido.toUpperCase());}");
            MyScript.Append(";");
            MyScript.Append("</script>");
            Page.RegisterStartupScript("funcion", MyScript.ToString());
        }

        /// <summary>
        /// Asigna los ClientSideEvents al grid 
        /// </summary>
        /// <param name="ultraWebGrid"></param>
        protected void AplicarEventosUltraWebGridCatalogo(UltraWebGrid ultraWebGrid)
        {
            this.registrarEventosUltraWebGridCatalogo();
            ultraWebGrid.DisplayLayout.ClientSideEvents.AfterCellUpdateHandler = "cambiarMayusculaGrid";
        }

        /// <summary>
        /// Llena una grilla pasando la fuente de datos 
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid</param>
        /// <param name="fuenteDatos">fuente de datos para llenar la grilla</param>
        public void llenarGrilla(UltraWebGrid ultraWebGrid, DataTable fuenteDatos)
        {
            ultraWebGrid.DataSource = fuenteDatos;
            ultraWebGrid.DataBind();
        }

        /// <summary>
        /// Llena una grilla pasando la fuente de datos 
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid</param>
        /// <param name="fuenteDatos">fuente de datos para llenar la grilla</param>
        public void llenarGrilla(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string dataMember)
        {
            ultraWebGrid.DataSource = fuenteDatos;
            ultraWebGrid.DataMember = dataMember;
            ultraWebGrid.DataBind();
        }

        /// <summary>
        /// Llena una grilla pasando la fuente de datos como vista
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid</param>
        /// <param name="fuenteDatos">fuente de datos para llenar la grilla</param>
        public void llenarGrilla(UltraWebGrid ultraWebGrid, DataView fuenteDatos, string dataMember)
        {
            ultraWebGrid.DataSource = fuenteDatos;
            ultraWebGrid.DataMember = dataMember;
            ultraWebGrid.DataBind();
        }

        /// <summary>
        /// Formatea la columna de una grilla con lo siguiente:
        /// - Se habilita la "visibilidad" de las columnas del arreglo insertado y el "orden".
        /// - Si se llena de ceros se asume el tamaño necesario como para que entre el titulo de lo contrario se
        ///	  puede especificar un tamaño.
        ///	</summary>
        /// <param name="nombreBanda">Nombre de la Banda que se va a formatear</param>
        /// <param name="ultraWebGrid">grilla que se va a formatear</param>
        /// <param name="columnas">columnas que se van a mostrar</param>
        /// <param name="tamanio">tamaños que se van a mostrar</param>
        public void HabilitarColumnasVisibles(string nombreBanda, UltraWebGrid ultraWebGrid, string[] columnas, int[] tamanio)
        {
            for (int i = 0; i < ultraWebGrid.Bands.FromKey(nombreBanda).Columns.Count; i++)
            {
                ultraWebGrid.Bands.FromKey(nombreBanda).Columns[i].Hidden = true;
            }
            int j = 0;
            foreach (string cadena in columnas)
            {
                ultraWebGrid.Bands.FromKey(nombreBanda).Columns.FromKey(cadena).Hidden = false;
                ultraWebGrid.Bands.FromKey(nombreBanda).Columns.FromKey(cadena).Move(j);
                if (tamanio[j] == 0)
                    ultraWebGrid.Bands.FromKey(nombreBanda).Columns.FromKey(cadena).Width = ultraWebGrid.Bands.FromKey(nombreBanda).Columns.FromKey(cadena).Header.Caption.Length * ConstantesPresentacion.PXXCARACTER;
                else
                    ultraWebGrid.Bands.FromKey(nombreBanda).Columns.FromKey(cadena).Width = tamanio[j];
                j++;
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

            //			//TODO: así deberia quedar
            if (Session[bs_Sesion.ID_Sesion] == null)
                Response.Redirect("~/Framework/Caducidad.aspx", true);

            Sesion = (bs_Sesion)Session[bs_Sesion.ID_Sesion];
            MensajeAplicacion = new MensajeAplicacion();

            if (Session["GARGO_PARAMETROS_GLOBALES"] == null)
            {
                //CargarParametrosGlobales();
                new ParametroBolsa(CurrentInstanceName).CargarParametrosGlobales(false);
                Session.Add("GARGO_PARAMETROS_GLOBALES", true);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.PaginaBase_PreRender);
        }
        #endregion

        /// <summary>
        /// Insertar fila a un ultrawebGrid
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
        /// <param name="band">Band donde insertara</param>
        protected void insertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, int band, Hashtable campos)
        {
            //object valor;
            UltraGridRow filaGrilla;
            filaGrilla = ultraWebGrid.Bands[band].AddNew();

            //ultraWebGrid.Rows[ultraWebGrid.Rows.Count-1].Activate();
            if (campos != null)
            {
                foreach (string key in campos.Keys)
                {
                    if (filaGrilla.Cells.FromKey(key) != null)
                    {
                        filaGrilla.Cells.FromKey(key).Value = campos[key];
                    }

                }
            }
        }

        /// <summary>
        /// Insertar fila a un ultrawebGrid
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
        protected void insertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid)
        {
            ultraWebGrid.Rows.Add();
            ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Activate();
        }

        /// <summary>
        /// Insertar fila a un ultrawebGrid e inicializar algunos valores 
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid a insertar fila</param>
        /// <param name="campos">Hashtable donde las llaves corresponden al nombre del campo y el value es el valor a asignar</param>
        protected void insertarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, Hashtable campos)
        {
            ultraWebGrid.Rows.Add();
            ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Activate();

            foreach (string key in campos.Keys)
                ultraWebGrid.Rows[ultraWebGrid.Rows.Count - 1].Cells.FromKey(key).Value = campos[key].ToString();
        }

        /// <summary>
        /// Recuperar el indice de la columna identity
        /// </summary>
        /// <param name="dt">Tabla</param>
        private int ObtenerColumnaIdentity(DataTable tabla)
        {
            int indice = -1;
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                if (tabla.Columns[i].AutoIncrement)
                    indice = i;
            }
            return indice;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ugr"></param>
        /// <param name="dataSet"></param>
        /// <param name="nombreTabla"></param>
        internal void eliminarFilaUltraWebGrid(UltraGridRow ugr, DataSet dataSet, string nombreTabla)
        {

            int clave = this.ObtenerColumnaIdentity(dataSet.Tables[nombreTabla]);
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
                    dataSet.Tables[nombreTabla].Rows.Find(cod).Delete();
                ugr.Delete();
            }
        }

        protected void eliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla)
        {
            UltraGridRow ugr = ultraWebGrid.DisplayLayout.SelectedRows[0];

            int clave = this.ObtenerColumnaIdentity(dataSet.Tables[nombreTabla]);
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
                    dataSet.Tables[nombreTabla].Rows.Find(cod).Delete();
                ugr.Delete();
            }
        }

        protected void eliminarFilaDataTableporCodigo(string Codigo, DataSet dataSet, string nombreTabla)
        {
            //UltraGridRow ugr = ultraWebGrid.DisplayLayout.SelectedRows[0];

            int clave = this.ObtenerColumnaIdentity(dataSet.Tables[nombreTabla]);
            string llave = String.Empty;
           

            if (clave == -1 && dataSet.Tables[nombreTabla].PrimaryKey != null)
            {
                llave = dataSet.Tables[nombreTabla].PrimaryKey[0].ColumnName;
            }
            else
            {
                llave = dataSet.Tables[nombreTabla].Columns[clave].ColumnName;
            }
            if (Codigo != null)
            {
                //string cod = ugr.Cells.FromKey(llave).Text;
                string cod = Codigo;
                int key = Convert.ToInt16(Codigo);
                //if (cod != null && !cod.Equals(""))
                if (key > 0)
                    dataSet.Tables[nombreTabla].Rows.Find(cod).Delete();
                //ugr.Delete();
            }
        }


        /// <summary>
        /// Elimina una fila de un UltraWebGrid
        /// </summary>
        /// <param name="ultraWebGrid">UltraWebGrid a eliminar fila</param>
        /// <param name="dataSet">Fuente de datos asociados a la grilla</param>
        /// <param name="nombreTabla">Nombre de la tabla</param>
        /// <param name="camposClavePrimaria">Campos que son llave primaria</param>
        protected void eliminarFilaUltraWebGrid(UltraWebGrid ultraWebGrid, DataSet dataSet, string nombreTabla, string[] camposClavePrimaria)
        {
            foreach (UltraGridRow ugr in ultraWebGrid.DisplayLayout.SelectedRows)
            {
                StringBuilder filtro = new StringBuilder();
                foreach (string campo in camposClavePrimaria)
                {
                    if (ugr.Cells.FromKey(campo).Value == null)
                    {
                        filtro.Remove(0, filtro.Length);
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
        /// Permite actualizar el dataset con los cambios efectuados en la grilla.
        /// </summary>
        /// <param name="ultraWebGrid">Grilla de la que va a comparar información</param>
        /// <param name="dataSet">Fuente de datos a actualizar</param>
        /// <param name="nombreTabla">arreglo de nombres de las tablas a actualizar, 0 tabla padre 1 tabla hija</param>
        /// <param name="llaves">arreglo de nombres de los campos PK</param>
        protected void PreparaDatosAGrabarMaestroDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string[] nombreTablas, string[] llaves)
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
                else// if(ugr.DataChanged == DataChanged.Modified)
                {
                    for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        try
                        {
                            fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells[i].Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                        }
                        catch
                        {
                            if (ugr.Cells[i] == null)
                                fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
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
                            else//  if(ugrHijo.DataChanged == DataChanged.Modified)
                            {
                                for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                                    try
                                    {
                                        fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugrHijo.Cells[i].Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                    }
                                    catch
                                    {
                                        if (ugrHijo.Cells[i] == null)
                                            fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                                    }

                            }
                        }

                    }

                }
            }

        }

        protected void PrepararDatosAGrabar(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla)
        {

            foreach (UltraGridRow ugr in ultraWebGrid.Rows)
            {
                int clave = this.ObtenerColumnaIdentity(fuenteDatos.Tables[nombreTabla]);
                string llave = fuenteDatos.Tables[nombreTabla].PrimaryKey[0].ColumnName;

                string cod = ugr.Cells.FromKey(llave).Text;

                if (cod == null || cod.Equals(""))
                {
                    DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                    for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                            if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                            else
                                dr[i] = DBNull.Value;

                    fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                }
                else //if(ugr.DataChanged == DataChanged.Modified)
                {
                    for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                    {
                        try
                        {
                            fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                        }
                        catch
                        {
                            if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                        }
                    }
                }
            }
        }

        protected void PrepararDatosAGrabarPorTable(DataTable table, DataSet fuenteDatos, string nombreTabla)
        {

            //foreach (UltraGridRow ugr in ultraWebGrid.Rows)
            for (int j=0;j<table.Rows.Count;j++)                      
            {
                int clave = this.ObtenerColumnaIdentity(fuenteDatos.Tables[nombreTabla]);
                string llave = fuenteDatos.Tables[nombreTabla].PrimaryKey[0].ColumnName;
                string tipodato = fuenteDatos.Tables[nombreTabla].PrimaryKey[0].DataType.ToString();

                //string cod = ugr.Cells.FromKey(llave).Text;
                //string cod = table.Rows[j][llave].ToString();

                if (tipodato != "System.String")
                {
                    int cod = Convert.ToInt16(table.Rows[j][llave]);

                    //if (cod == null || cod.Equals("") || cod == 0)
                    if (cod <= 0)
                    {
                        DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                            if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] != null)
                                    dr[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);

                                //dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                else
                                    dr[i] = DBNull.Value;

                        fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                    }
                    else //if(ugr.DataChanged == DataChanged.Modified)
                    {
                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        {
                            try
                            {
                                //fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                if (fuenteDatos.Tables[nombreTabla].Columns[i].ReadOnly == false)
                                {
                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                }
                            }
                            catch
                            {
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] == null)

                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                            }
                        }
                    }



                }
                else 
                {

                    string cod = table.Rows[j][llave].ToString();

                    //if (cod == null || cod.Equals("") || cod == 0)
                    //if (cod <= 0)
                    if (cod == null || cod.Equals("") || cod == "0")
                    {
                        DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                            if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] != null)
                                    dr[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);

                                //dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                else
                                    dr[i] = DBNull.Value;

                        fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                    }
                    else //if(ugr.DataChanged == DataChanged.Modified)
                    {
                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        {
                            try
                            {
                                //fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                if (fuenteDatos.Tables[nombreTabla].Columns[i].ReadOnly == false)
                                {
                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                }
                            }
                            catch
                            {
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] == null)

                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                            }
                        }
                    }




                }





            }
        }

        protected void PrepararDatosAGrabarPorTableTemp(DataTable table, DataSet fuenteDatos, string nombreTabla)
        {

            //foreach (UltraGridRow ugr in ultraWebGrid.Rows)
            for (int j = 0; j < table.Rows.Count; j++)
            {
                int clave = this.ObtenerColumnaIdentity(fuenteDatos.Tables[nombreTabla]);
                string llave = fuenteDatos.Tables[nombreTabla].PrimaryKey[0].ColumnName;
                string tipodato = fuenteDatos.Tables[nombreTabla].PrimaryKey[0].DataType.ToString();
                if (tipodato != "System.String")
                {
                    int cod = Convert.ToInt16(table.Rows[j][llave]);

                    //if (cod == null || cod.Equals("") || cod == 0)
                    if (cod <= 0)
                    {
                        DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                            if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] != null)
                                    dr[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);

                                //dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                else
                                    dr[i] = DBNull.Value;
                        if (fuenteDatos.Tables[nombreTabla].Rows.Count == 0)
                            dr[llave] = 1;
                        fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                    }
                    else //if(ugr.DataChanged == DataChanged.Modified)
                    {
                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        {
                            try
                            {
                                //fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                if (fuenteDatos.Tables[nombreTabla].Columns[i].ReadOnly == false)
                                {
                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                }
                            }
                            catch
                            {
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] == null)
                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                            }
                        }
                    }
                }
                else
                {
                    string cod = table.Rows[j][llave].ToString();
                    if (cod == null || cod.Equals("") || cod == "0")
                    {
                        DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                            if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] != null)
                                    dr[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);

                                //dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                else
                                    dr[i] = DBNull.Value;

                        fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                    }
                    else //if(ugr.DataChanged == DataChanged.Modified)
                    {
                        for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        {
                            try
                            {
                                //fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                if (fuenteDatos.Tables[nombreTabla].Columns[i].ReadOnly == false)
                                {
                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName], fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                                }
                            }
                            catch
                            {
                                //if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                if (table.Rows[j][fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName] == null)

                                    fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }


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
                            if ((!fuenteDatos.Tables[nombreTabla].Columns[str_Nombre].AutoIncrement) && ultraGridCell != null && ultraGridCell.Value != null)
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

        protected void PrepararDatosAGrabarDetalle(UltraWebGrid ultraWebGrid, DataSet fuenteDatos, string nombreTabla, DataRow filaPadre)
        {
            foreach (UltraGridRow ugr in ultraWebGrid.Rows)
            {
                int clave = this.ObtenerColumnaIdentity(fuenteDatos.Tables[nombreTabla]);
                string llave = fuenteDatos.Tables[nombreTabla].Columns[clave].ColumnName;
                string cod = ugr.Cells.FromKey(llave).Text;

                if (cod == null || cod.Equals(""))
                {
                    DataRow dr = fuenteDatos.Tables[nombreTabla].NewRow();

                    for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                        if (!fuenteDatos.Tables[nombreTabla].Columns[i].AutoIncrement)
                            if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value != null)
                                dr[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                            else
                                dr[i] = DBNull.Value;

                    dr.SetParentRow(filaPadre);
                    fuenteDatos.Tables[nombreTabla].Rows.Add(dr);
                }
                else //if(ugr.DataChanged == DataChanged.Modified)
                {
                    for (int i = 0; i < fuenteDatos.Tables[nombreTabla].Columns.Count; i++)
                    {
                        try
                        {
                            fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = Convert.ChangeType(ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value, fuenteDatos.Tables[nombreTabla].Columns[i].DataType);
                        }
                        catch
                        {
                            if (ugr.Cells.FromKey(fuenteDatos.Tables[nombreTabla].Columns[i].ColumnName).Value == null)
                                fuenteDatos.Tables[nombreTabla].Rows.Find(cod)[i] = DBNull.Value;
                        }
                    }
                }
            }
        }

        protected string SerializeDatatableForDrowpDownList(DataTable data, string idField, string valueField)
        {
            return DatatableHelper.SerializeDatatableForDrowpDownList(data, idField, valueField);
        }

        protected string SanitizeResponseString(string jsonStringSerialized) 
        {
            return DatatableHelper.SanitizeResponseString(jsonStringSerialized);
        }

        /// <summary>
        /// Llena un DropDown en una Grilla
        /// </summary>
        /// <param name="ultraWebGrid">Grilla a utilizar</param>
        /// <param name="fuenteDatos">Fuente de datos a utilizar</param>
        /// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
        /// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
        /// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
        public void CargarDropDown(UltraWebGrid ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor)
        {
            ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            ValueList TipoDdwlb = ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).ValueList;
            TipoDdwlb.DataSource = fuenteDatos;
            TipoDdwlb.DisplayMember = columnaVisualizar;
            TipoDdwlb.ValueMember = columnaValor;
            TipoDdwlb.DataBind();
        }


        public void CargarDropDown02(DataTable ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor,bool esString=false)
        {
            foreach (DataRow row in ultraWebGrid.Rows)
            {
                try
                {                
                    var dv = new DataView(fuenteDatos);
                    if (esString == false)
                    {                       
                        dv.RowFilter = columnaValor + " = " + row[columnaDropDown].ToString().Trim();
                    }
                    else
                    {
                        dv.RowFilter = columnaValor + " = '" + row[columnaDropDown].ToString().Trim()+"'";
                    }
                    var str = String.Empty;
                    if (dv.ToTable().Rows.Count > 0)
                    {
                        str = dv.ToTable().Rows[0][columnaVisualizar].ToString();
                    }
                    else
                    {
                        str = row[columnaDropDown].ToString();
                    }                    
                    row[columnaDropDown] = str;
                }
                catch (Exception ex)
                {
                    throw new Exception("columnaDropDown: " + columnaDropDown + "; " + ex.Message);
                }
            }



            ////(0_0)
            //ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            //ValueList TipoDdwlb = ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).ValueList;
            //TipoDdwlb.DataSource = fuenteDatos;
            //TipoDdwlb.DisplayMember = columnaVisualizar;
            //TipoDdwlb.ValueMember = columnaValor;
            //TipoDdwlb.DataBind();
        }

       

        public DataTable CloneTable(DataTable source)
        {
            var dt = new DataTable();
            foreach (DataColumn col in source.Columns)
            {
                dt.Columns.Add(col.ColumnName, typeof(string));
            }
            foreach (DataRow row in source.Rows)
            {
                var newRow = dt.NewRow();
                foreach (DataColumn col in source.Columns)
                {
                    newRow[col.ColumnName] = row[col.ColumnName];
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }

        public string CreateJsDataTableTitles(DataTable table, string aspxFileName, string controlName)
        {

            var str = new StringBuilder();
            str.AppendLine("{");
            foreach (DataColumn column in table.Columns)
            {
                str.Append($"{column.ColumnName}:'{MensajeAplicacion.ObtenerEtiqueta(aspxFileName + ".aspx." + controlName + "." + column.ColumnName)}',");
            }
            str.AppendLine("}");
            return str.ToString();
        }

        public string ObtenerListaEnumeradaColumnasExportar(DataTable table)
        {
            string str = string.Empty;
            str = "[";
            for(int i = 0; i < table.Columns.Count; i++)   
            {
                str += "" + i + ",";
            }
            str = str.Substring(0, str.ToString().Length-1);
            str += "]";

            return str.ToString();
        }

        /// <summary>
        /// Llena un DropDown en una Grilla indicando el band
        /// </summary>
        /// <param name="ultraWebGrid">Grilla a utilizar</param>
        /// <param name="fuenteDatos">Fuente de datos a utilizar</param>
        /// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
        /// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
        /// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
        /// /// <param name="banda">Posicion del campo band</param>
        public void CargarDropDown(UltraWebGrid ultraWebGrid, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor, int banda)
        {
            ultraWebGrid.Bands[banda].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            ValueList TipoDdwlb = ultraWebGrid.Bands[banda].Columns.FromKey(columnaDropDown).ValueList;
            TipoDdwlb.DataSource = fuenteDatos;
            TipoDdwlb.DisplayMember = columnaVisualizar;
            TipoDdwlb.ValueMember = columnaValor;
            TipoDdwlb.DataBind();
        }
        /// <summary>
        /// Llena un DropDown en una Grilla con una vista
        /// </summary>
        /// <param name="ultraWebGrid">Grilla a utilizar</param>
        /// <param name="fuenteDatos">Fuente de datos a utilizar</param>
        /// <param name="columnaDropDown">Columna de la grilla a utilizar DropDown</param>
        /// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
        /// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
        public void CargarDropDown(UltraWebGrid ultraWebGrid, DataView fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor)
        {
            ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            ValueList TipoDdwlb = ultraWebGrid.Bands[0].Columns.FromKey(columnaDropDown).ValueList;
            TipoDdwlb.DataSource = fuenteDatos;
            TipoDdwlb.DisplayMember = columnaVisualizar;
            TipoDdwlb.ValueMember = columnaValor;
            TipoDdwlb.DataBind();
        }

        /// <summary>
        /// Llena un DropDown en un WebCombo
        /// </summary>
        /// <param name="webCombo">WebCombo a utilizar</param>
        /// <param name="fuenteDatos">Fuente de datos a utilizar</param>
        /// <param name="columnaDropDown">Columna del webcombo a utilizar DropDown</param>
        /// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
        /// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
        public void CargarDropDown(WebCombo webCombo, DataTable fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor)
        {
            webCombo.Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            ValueList TipoDdwlb = webCombo.Columns.FromKey(columnaDropDown).ValueList;
            TipoDdwlb.DataSource = fuenteDatos;
            TipoDdwlb.DisplayMember = columnaVisualizar;
            TipoDdwlb.ValueMember = columnaValor;
            TipoDdwlb.DataBind();
        }

        /// <summary>
        /// Llena un DropDown en un WebCombo
        /// </summary>
        /// <param name="webCombo">WebCombo a utilizar</param>
        /// <param name="fuenteDatos">Fuente de datos a utilizar</param>
        /// <param name="columnaDropDown">Columna del webcombo a utilizar DropDown</param>
        /// <param name="columnaVisualizar">Columna de la fuente de datos a visualizar</param>
        /// <param name="columnaValor">Columna de la fuente de datos a utilizar como valor del DropDown</param>
        public void CargarDropDown(WebCombo webCombo, DataView fuenteDatos, string columnaDropDown, string columnaVisualizar, string columnaValor)
        {
            webCombo.Columns.FromKey(columnaDropDown).Type = ColumnType.DropDownList;
            ValueList TipoDdwlb = webCombo.Columns.FromKey(columnaDropDown).ValueList;
            TipoDdwlb.DataSource = fuenteDatos;
            TipoDdwlb.DisplayMember = columnaVisualizar;
            TipoDdwlb.ValueMember = columnaValor;
            TipoDdwlb.DataBind();
        }

        /// <summary>
        /// Método que registra crea una variable javascript y le setea un valor.
        /// </summary>
        /// <param name="nombre">Nombre de la variable a crear.</param>
        /// <param name="valor">Valor que tendrá la variable.</param>
        protected void RegistrarVariableJScript(string nombre, string valor)
        {
            StringBuilder MyScript = new StringBuilder();
            MyScript.Append("<script language=\"javascript\">");
            MyScript.Append("var ");
            MyScript.Append(nombre);
            MyScript.Append("='");
            MyScript.Append(valor);
            MyScript.Append("';");
            MyScript.Append("</script>");
            Page.RegisterStartupScript(nombre, MyScript.ToString());
        }

        #region Manejo de Idioma

        /// <summary>
        /// Valida los tipos de controles que van a manejar el idioma
        /// </summary>
        /// <param name="tipoControl"></param>
        /// <returns>Tipo de Control</returns>
        private bool validarTipoControl(string tipoControl)
        {
            //Tipos de Controles habilitados
            string[] Tiposcontrol = { "Label", "Button", "HtmlInputButton", "LinkButton", "HyperLink", "CheckBox", "RadioButton", "CompareValidator", "RequiredFieldValidator", "RangeValidator", "RegularExpressionValidator", "CustomValidator" };

            if (Array.IndexOf(Tiposcontrol, tipoControl) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Setea el mensaje a visualizar en una propiedad determinada del control dependiendo el tipo de control
        /// </summary>
        /// <param name="tipoControl">Tipo de Control</param>
        /// <param name="control">Control</param>
        /// <param name="mensaje">Mensaje a visualizar</param>
        private void colocarMensajeControl(string tipoControl, Control control, string mensaje)
        {
            switch (tipoControl)
            {
                case "Label":
                    ((Label)control).Text = mensaje;
                    break;
                case "Button":
                    ((Button)control).Text = mensaje;
                    break;
                case "LinkButton":
                    ((LinkButton)control).Text = mensaje;
                    break;
                case "HyperLink":
                    ((HyperLink)control).Text = mensaje;
                    break;
                case "CheckBox":
                    ((CheckBox)control).Text = mensaje;
                    break;
                case "RadioButton":
                    ((RadioButton)control).Text = mensaje;
                    break;
                case "HtmlInputButton":
                    ((HtmlInputButton)control).Value = mensaje;
                    break;
                case "RequiredFieldValidator":
                    ((RequiredFieldValidator)control).Text = "*";
                    ((RequiredFieldValidator)control).ToolTip = mensaje;
                    ((RequiredFieldValidator)control).ErrorMessage = mensaje;
                    break;
                case "RangeValidator":
                    ((RangeValidator)control).Text = "*";
                    ((RangeValidator)control).ToolTip = mensaje;
                    ((RangeValidator)control).ErrorMessage = mensaje;
                    break;
                case "CompareValidator":
                    ((CompareValidator)control).Text = "*";
                    ((CompareValidator)control).ToolTip = mensaje;
                    ((CompareValidator)control).ErrorMessage = mensaje;
                    break;
                case "RegularExpressionValidator":
                    ((RegularExpressionValidator)control).Text = "*";
                    ((RegularExpressionValidator)control).ToolTip = mensaje;
                    ((RegularExpressionValidator)control).ErrorMessage = mensaje;
                    break;
                case "CustomValidator":
                    ((CustomValidator)control).Text = "*";
                    ((CustomValidator)control).ToolTip = mensaje;
                    ((CustomValidator)control).ErrorMessage = mensaje;
                    break;
            }
        }

        /// <summary>
        /// Recorre los controles de la pagina y setea los mensajes en base a un archivo de recurso
        /// </summary>
        /// <param name="nombrePagina">Nombre de la Pagina</param>
        /// <param name="control">Control</param>
        private void cargaMensajeControles(string nombrePagina, Control control)
        {
            if (control.HasControls())
            {
                foreach (Control control1 in control.Controls)
                {
                    if (control1.HasControls())
                    {
                        this.cargaMensajeControles(nombrePagina, control1);
                    }
                    else
                    {
                        string tipoControl = control1.GetType().Name;
                        string nombreControl2 = control1.ID;

                        if (this.validarTipoControl(tipoControl))
                        {
                            string nombreControl = control1.ID;
                            this.colocarMensajeControl(tipoControl, control1, MensajeAplicacion.ObtenerEtiqueta(nombrePagina + "." + nombreControl));

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el nombre exacto de la pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <returns></returns>
        public string obtenerNombrePagina(string pagina)
        {
            string[] res = pagina.Split('/');
            int i = res.GetUpperBound(0);
            string valor = res[i];
            return valor;
        }

        /// <summary>
        /// 
        /// Configura las etiquetas del Header del grid en base a un archivo de recurso
        /// </summary>
        /// <param name="grid"></param>
        protected void configurarEtiquetasUltraWebGrid(UltraWebGrid grid)
        {
            for (int i = 0; i < grid.Bands.Count; i++)
            {
                foreach (UltraGridColumn columna in grid.Bands[i].Columns)
                {
                    columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(this.obtenerNombrePagina(Page.Request.Url.AbsolutePath) + "." + grid.ID + "." + columna.Header.Key);
                }
            }

        }


        /// <summary>
        /// Configura las etiquetas del Header del grid en base a un archivo de recurso
        /// </summary>
        /// <param name="webCombo"></param>
        protected void configurarEtiquetasWebCombo(WebCombo webCombo)
        {
            foreach (UltraGridColumn columna in webCombo.Columns)
            {
                columna.Header.Caption = MensajeAplicacion.ObtenerEtiqueta(this.obtenerNombrePagina(Page.Request.Url.AbsolutePath) + "." + webCombo.ID + "." + columna.Header.Key);
            }

        }

        /// <summary>
        /// Configura las etiquetas del Header del grid en base a un archivo de recurso
        /// </summary>
        protected void configurarEtiquetasGenerales()
        {
            MensajeAplicacion = new MensajeAplicacion();
            this.cargaMensajeControles(this.obtenerNombrePagina(Page.Request.Url.AbsolutePath), this);

        }

        #endregion

        #region Tabs
        /// <summary>
        /// Llena el ultrawebtab con las páginas cuya madre es la que se envía.
        /// </summary>
        /// <param name="tabulado"></param>
        /// <param name="pagina"></param>
        protected void mostrarTabulado(UltraWebTab tabulado, Page pagina)
        {
            tabulado.Tabs.Clear();
            FormatoControl formato = new FormatoControl();
            Permisos permisos = new Permisos(CurrentInstanceName);
            formato.AplicarFormato(tabulado);
            OpcionUsuarioDataSet opciones = new OpcionUsuarioDataSet();

            string url = this.Page.Request.CurrentExecutionFilePath.ToLower();
            int positionstart = url.IndexOf("/", 2) + 1;
            url = url.Substring(positionstart);
            permisos.ObtenerCodigoOpcion(url, opciones);

            if (opciones.SER_OPCION.Rows.Count <= 0)
                return;

            int opcion = (int)opciones.SER_OPCION.Rows[0]["CODOPCION"];

            CrearDisenio(tabulado);
            GenerarTabuladoDinamico(tabulado, opcion);
            tabulado.LoadAllTargetUrls = true;
        }

        /// <summary>
        /// Llena el ultrawebtab con las páginas cuya madre es la que se envía.
        /// </summary>
        /// <param name="tabulado"></param>
        /// <param name="pagina"></param>
        protected void mostrarTabulado(UltraWebTab tabulado, Page pagina, bool cargarTotalTabulado)
        {
            tabulado.Tabs.Clear();
            FormatoControl formato = new FormatoControl();
            Permisos permisos = new Permisos(CurrentInstanceName);
            formato.AplicarFormato(tabulado);
            OpcionUsuarioDataSet opciones = new OpcionUsuarioDataSet();
            string url = this.Page.Request.CurrentExecutionFilePath.ToLower();

            int positionstart = url.IndexOf("/", 2) + 1;
            url = url.Substring(positionstart);
            permisos.ObtenerCodigoOpcion(url, opciones);

            if (opciones.SER_OPCION.Rows.Count <= 0)
            {
                return;
            }

            int opcion = (int)opciones.SER_OPCION.Rows[0]["CODOPCION"];
            CrearDisenio(tabulado);
            GenerarTabuladoDinamico(tabulado, opcion);
            tabulado.LoadAllTargetUrls = !cargarTotalTabulado;
            tabulado.AutoPostBack = cargarTotalTabulado;
        }

        private void CrearDisenio(UltraWebTab ut_Tab)
        {
            ut_Tab.Width = Unit.Percentage(100);
            ut_Tab.Height = Unit.Percentage(100);
            ut_Tab.LoadAllTargetUrls = false;
        }

        #region GenerarTabsDinaminco_Sin_Infragistics
        //GenerarTabsDinaminco_Sin_Infragistics
        protected void MostrarTabs(ref List<Tabs> listTabs)
        {
            Permisos permisos = new Permisos(CurrentInstanceName);
            OpcionUsuarioDataSet opciones = new OpcionUsuarioDataSet();
            string url = this.Page.Request.CurrentExecutionFilePath.ToLower();

            int positionstart = url.IndexOf("/", 2) + 1;
            url = url.Substring(positionstart);
            permisos.ObtenerCodigoOpcion(url, opciones);

            if (opciones.SER_OPCION.Rows.Count <= 0)
            {
                return;
            }

            int opcion = (int)opciones.SER_OPCION.Rows[0]["CODOPCION"];
            listTabs = GenerarTabsDinamicos(opcion);
        }
        private List<Utilitarios.Tabs> GenerarTabsDinamicos(int opcion)
        {
            string titulo, url;
            List<Utilitarios.Tabs> listTabs = new List<Utilitarios.Tabs>();

            DataSet opciones = new Negocio.Seguridad.Seguridad(CurrentInstanceName).ObtenerMenuPorOpcion(Sesion.CodigoTipoProducto, Sesion.CodigoPerfilUsuario, opcion);

            if (opciones != null && opciones.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < opciones.Tables[0].Rows.Count; i++)
                {
                    if (opciones.Tables[0].Rows[i]["EstiloTab"].ToString() == "T")
                        continue;

                    if (opcion >= 0)
                    {
                        if (opciones.Tables[0].Rows[i]["Codopcionpadre"] == DBNull.Value || Convert.ToInt32(opciones.Tables[0].Rows[i]["Codopcionpadre"]) != opcion)
                            continue;
                    }
                    url = opciones.Tables[0].Rows[i]["URL"].ToString();
                    url = url.Substring(url.LastIndexOf("/") + 1);
                    url += "?aumentado=" + opciones.Tables[0].Rows[i]["CODOPCION"].ToString();
                    if (opciones.Tables[0].Rows[i]["PARAMETRO"] != null && opciones.Tables[0].Rows[i]["PARAMETRO"].ToString() != String.Empty)
                    {
                        url = url + "&" + opciones.Tables[0].Rows[i]["PARAMETRO"].ToString();

                    }

                    MensajeAplicacion mensajeAplicacion = new MensajeAplicacion();

                    //titulo = opciones.Tables[0].Rows[i]["TITULO"].ToString();
                    titulo = mensajeAplicacion.ObtenerEtiquetaMenu(opciones.Tables[0].Rows[i]["TITULO"].ToString());

                    if (titulo == null)
                        titulo = url;

                    listTabs.Add(new Tabs(i + 1, titulo, url));
                }
            }
            return listTabs;
        }
        #endregion

        #region Generar Accordion Dinamico Sin_Infragistics

        #endregion
        private void GenerarTabuladoDinamico(UltraWebTab tabulado, int opcion)
        {
            string titulo, url;

            //DataSet  opciones = new Negocio.Seguridad.Seguridad(CurrentInstanceName).ObtenerMenu(Sesion.CodigoTipoProducto, Sesion.CodigoPerfilUsuario);

            DataSet opciones = new Negocio.Seguridad.Seguridad(CurrentInstanceName).ObtenerMenuPorOpcion(Sesion.CodigoTipoProducto, Sesion.CodigoPerfilUsuario, opcion);

            if (opciones != null && opciones.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < opciones.Tables[0].Rows.Count; i++)
                {
                    if (opciones.Tables[0].Rows[i]["EstiloTab"].ToString() == "T")
                        continue;

                    if (opcion >= 0)
                    {
                        if (opciones.Tables[0].Rows[i]["Codopcionpadre"] == DBNull.Value || Convert.ToInt32(opciones.Tables[0].Rows[i]["Codopcionpadre"]) != opcion)
                            continue;
                    }
                    url = opciones.Tables[0].Rows[i]["URL"].ToString();
                    url = url.Substring(url.LastIndexOf("/") + 1);
                    url += "?aumentado=" + opciones.Tables[0].Rows[i]["CODOPCION"].ToString();
                    if (opciones.Tables[0].Rows[i]["PARAMETRO"] != null && opciones.Tables[0].Rows[i]["PARAMETRO"].ToString() != String.Empty)
                    {
                        url = url + "&" + opciones.Tables[0].Rows[i]["PARAMETRO"].ToString();

                    }

                    MensajeAplicacion mensajeAplicacion = new MensajeAplicacion();

                    //titulo = opciones.Tables[0].Rows[i]["TITULO"].ToString();
                    titulo = mensajeAplicacion.ObtenerEtiquetaMenu(opciones.Tables[0].Rows[i]["TITULO"].ToString());
                    Tab _tabulado = new Tab();
                    if (titulo == null)
                        titulo = url;

                    _tabulado.Text = titulo;
                    _tabulado.Key = opciones.Tables[0].Rows[i]["NOMBRETITULO"].ToString();
                    _tabulado.ContentPane.TargetUrl = url;
                    tabulado.Tabs.Add(_tabulado);
                    tabulado.DummyTargetUrl = FuncionesGenerales.ObtenerRutaAplicacion(this.Page) + "/Utilitarios/Cargando.aspx";
                }
            }
            else
                tabulado.Visible = false;
        }

        #endregion

        #region Modelo

        /// <summary>
        /// Metodo a ser sobrescrito en las paginas especificas
        /// </summary>
        protected virtual void inicializarModelo()
        {

        }

        public bool estaCustodiaActivado()
        {
            //TODO Esta comentado hasta que se instale
            //return new ArrayList(Instance.ApplicationsList()).Contains(ConfigurationSettings.AppSettings.Get("CustodiaID"));
            string strCustodiaActivo;
            strCustodiaActivo = new ParametroBolsa(CurrentInstanceName).ObtenerValorParametro(ConstantesBolsa.GEN_CUSTODIA_HABILITADO);
            if (strCustodiaActivo.Equals(ConstantesBolsa.F))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Validaciones UltraWebGrid

        /// <summary>
        /// Busca dentro del array si la columna es un campo requerido
        /// </summary>
        /// <param name="nombreColumna"></param>
        /// <param name="camposObligatorios"></param>
        /// <returns></returns>
        private int validarColumnaRequerida(string nombreColumna, string[] camposObligatorios)
        {
            return Array.IndexOf(camposObligatorios, nombreColumna);
        }

        /// <summary>
        /// Valida las columnas requeridas en el UltraWebGrid 
        /// </summary>
        /// <param name="ultraWebGrid"></param>
        /// <param name="camposObligatorios"></param>
        /// <param name="mensajesValidacion"></param>
        /// <returns></returns>
        protected string ValidarDatosRequeridosUltraWebGrid(UltraWebGrid ultraWebGrid, string[] camposObligatorios, string[] mensajesValidacion)
        {
            int indice = 0;

            foreach (UltraGridRow fila in ultraWebGrid.Rows)
            {
                if (!fila.HasChildRows)
                {
                    foreach (UltraGridCell celda in fila.Cells)
                    {
                        indice = this.validarColumnaRequerida(celda.Key, camposObligatorios);

                        if (indice >= 0)
                        {
                            if (celda.Value == null)
                            {
                                return mensajesValidacion[indice];
                            }
                        }
                    }
                }
                else
                {
                    int bandaHija = fila.BandIndex + 1;
                    UltraGridRow filaHija;
                    UltraGridRowsEnumerator uge = ultraWebGrid.Bands[bandaHija].GetRowsEnumerator();

                    while (uge.MoveNext())
                    {
                        filaHija = uge.Current;

                        foreach (UltraGridCell celda in filaHija.Cells)
                        {
                            indice = this.validarColumnaRequerida(celda.Key, camposObligatorios);

                            if (indice >= 0)
                            {
                                if (celda.Value == null)
                                {
                                    return mensajesValidacion[indice];
                                }
                            }
                        }
                    }
                }

            }
            return String.Empty;
        }

        #endregion

        public string obtenerRutaImagenes()
        {
            string ruta = Page.Request.Url.AbsolutePath;
            string rutaImagenes = string.Empty;
            string[] arregloRuta = ruta.Split('/');
            for (int i = 1; i < arregloRuta.Length - 2; i++)
            {
                rutaImagenes = rutaImagenes + "../";
            }

            rutaImagenes = rutaImagenes + "/Utilitarios/imagenes/";

            return rutaImagenes;

        }

        public int ObtenerCodigoPaginaPorUrl(string URL)
        {
            Negocio.Seguridad.Seguridad seguridad = new Negocio.Seguridad.Seguridad(CurrentInstanceName);
            return seguridad.ObtenerCodigoPaginaPorUrl(URL);
        }

        #region Manejo de Estado

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            new Manager().SavePageState(pagina);

        }

        protected void CargarEstadoPagina(Page paginaDestino)
        {
            new Manager().LoadPageState(paginaDestino);
        }

        #endregion

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

        private void PaginaBase_PreRender(object sender, System.EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
            Response.Buffer = true;
            Response.Expires = 0;
        }

    }

    [Serializable]
    public class Tabs
    {
        public Int32 Index { get; set; }
        public String Text { get; set; }
        public String TargetUrl { get; set; }
        public Tabs() { }
        public Tabs(Int32 Index, String Text, String TargetUrl)
        {
            this.Index = Index;
            this.Text = Text;
            this.TargetUrl = TargetUrl;
        }
    }

    [Serializable]
    public class Accordion
    {
        public Int32 Index { get; set; }
        public String Tittle { get; set; }
        public string Comentario { get; set; }
        public string Contenido01 { get; set; }
        public string Contenido02 { get; set; }
        public string Contenido03 { get; set; }
        public Accordion() { }

        public Accordion(int index, string tittle, string comentario, string contenido01, string contenido02, string contenido03)
        {
            this.Index = index;
            this.Tittle = tittle;
            this.Comentario = comentario;
            this.Contenido01 = contenido01;
            this.Contenido02 = contenido02;
            this.Contenido03 = contenido03;
        }
    }
}
