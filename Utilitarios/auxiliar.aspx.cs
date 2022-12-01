using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;
using Seriva.Bolsa.Herramientas.Constantes;
using Seriva.Bolsa.Negocio.General;
using Seriva.Bolsa.Negocio.RentaVariable;
using Seriva.Bolsa.Negocio.Valores;
using Seriva.Utilitarios;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    /// <summary>
    /// Página auxiliar que sera llamada desde las otras paginas, via xmlhttp
    /// </summary>
    public class auxiliar : Page
	{
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
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		#region CurrentInstanceName

		public string CurrentInstanceName
		{
			get
			{
				return Sesion.CurrentInstance;
			}
		}

		#endregion

		protected internal bs_Sesion Sesion;

		#region Eventos

		private void Page_Load(object sender, EventArgs e)
		{
			Sesion = (Session[bs_Sesion.ID_Sesion] == null)?null:(bs_Sesion)Session[bs_Sesion.ID_Sesion];

			int tipoBusquedaEnviada = int.Parse(Request.QueryString["TipoBusqueda"]);
			string retorno=null;

			switch (tipoBusquedaEnviada)
			{

				case 1:
				{
					#region Fecha de Vencimiento
					//Recálculo de la fecha de vencimiento
					int diasVigencia=int.Parse(Request.QueryString["DiasVigencia"]);
					retorno=CalcularFechaVencimiento(diasVigencia);
					break;
					#endregion
				}
				case 2:
				{
					#region Comisiones
					//Recálculo comisiones

					// ********CODIGOS PARAMETROS REQUEST************
					//  Precio				= A 
					//  Cantidad			= B
					//  CodigoMoneda		= C
					//  TipoClienteBolsa    = D
					//  TipoMercado			= E
					//  TipoOperacion		= F
					//  ModoOperacion		= G
					//  CodigoCliente		= H
					//  PrecioValor         = I
					// ********FIN CODIGOS PARAMETROS REQUEST************
					Decimal precio;
					Decimal cantidad;
					try
					{
						precio = Convert.ToDecimal(Request.QueryString["A"]);
					}
					catch(Exception ex)
					{
						String mensaje = ex.Message;
						precio = Decimal.Zero;
					}

					try
					{
						cantidad = Convert.ToDecimal(Request.QueryString["B"]);
					}
					catch(Exception ex)
					{
						String mensaje = ex.Message;
						cantidad = Decimal.Zero;
					}

					int codigoMoneda = int.Parse(Request.QueryString["C"]);
					int codigoTipoClienteBolsa = int.Parse(Request.QueryString["D"]);
					string tipoMercado = Request.QueryString["E"];
					string tipoOperacion = Request.QueryString["F"];
					string modalidadOperacion = Request.QueryString["G"];
					string codigoCliente = Request.QueryString["H"];
					int codigoValor = Convert.ToInt32(Request.QueryString["I"]);
					///etorno = CalcularComision(precio,cantidad,codigoMoneda,codigoTipoClienteBolsa,tipoMercado,tipoOperacion,modalidadOperacion,codigoCliente,codigoValor);
					break;
					#endregion
				}
				case 3 :
				{
					#region Control Grilla RV
					//Control Grilla
					string codigo = Request.QueryString["A"];
					//retorno = ObtenerOrdenesPorUsuarioRV(codigo);
					break;
					#endregion
				}
				case 4:
				{
					#region Castigos
					//Obtener los castigos de un determinado valor
					int codigoEmisionRV = int.Parse(Request.QueryString["codigoEmisionRV"]);
					//retorno=ObtenerValorReferencialRV(codigoEmisionRV);
					break;
					#endregion
				}
				case 5:
				{
					#region Precio Valor
					//Obtener el precio de un valor registrado en los estructurales
					int codigoEmisionRV = int.Parse(Request.QueryString["codigoEmisionRV"]);
					retorno=ObtenerPrecioValor(codigoEmisionRV);
					break;
					#endregion
				}
				case 6:
				{
					#region CuentaCustodia

					//Obtener la cantidad de un determinado valor que hay en una cuenta en custodia
					string codigoCuentaMDC = Request.QueryString["codigoCuentaMDC"];
					string codigoCliente= Request.QueryString["codigoCliente"];
					int codigoEmisionRV = int.Parse(Request.QueryString["codigoEmisionRV"]);
					retorno=ObtenerCantidadDeValorEnCuenta(codigoCuentaMDC,codigoCliente,codigoEmisionRV);
					break;
					#endregion
				}
				case 9:
				{
					#region Ordenes de Subasta

					int codigoSubasta = int.Parse(Request.QueryString["codigoSubasta"]);
					retorno = ObtenerTotalesOrdenesSubasta(codigoSubasta);
					break;
					#endregion
				}
				case 10:
				{
					#region Bloqueo

					int codigoOrden = int.Parse(Request.QueryString["CodigoOrden"]);
					retorno = BloquearOrden(codigoOrden);
					break;

					#endregion Bloqueo
				}
				case 11:
				{
					#region DesBloqueo

					int codigoOrden = int.Parse(Request.QueryString["CodigoOrden"]);
					retorno = DesbloquearOrden(codigoOrden);
					break;

					#endregion DesBloqueo
				}

			}
			Response.Clear();
			Response.CacheControl="No-cache";
			Response.Write(retorno);
			Response.Flush();
			Response.End();
		}

		#endregion Eventos

		#region Procesos

		public string BloquearOrden(int codigoOrden){
			StringBuilder retornoXML = new StringBuilder();
			try
			{
				retornoXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
				Orden orden = new Orden(CurrentInstanceName);
				if(this.Sesion != null)
				{
					orden.CapturarOrden(codigoOrden,this.Sesion.CodigoPerfilUsuario);
					retornoXML.Append("<Resultado>");
					retornoXML.Append(ConstantesBolsa.SI);
					retornoXML.Append("</Resultado>");
				}
				else{
					retornoXML.Append("<Resultado>");
					retornoXML.Append(ConstantesBolsa.NO);
					retornoXML.Append("</Resultado>");
				}
			}
			catch(Exception)
			{
				retornoXML.Remove(0, retornoXML.Length);
			}
			return retornoXML.ToString();
		}

		public string DesbloquearOrden(int codigoOrden)
		{
			StringBuilder retornoXML = new StringBuilder();
			try
			{
				retornoXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
				Orden orden = new Orden(CurrentInstanceName);
				if(Sesion != null)
				{
					orden.DesbloquearOrden(codigoOrden,this.Sesion.CodigoPerfilUsuario);
					retornoXML.Append("<Resultado>");
					retornoXML.Append(ConstantesBolsa.SI);
					retornoXML.Append("</Resultado>");
				}
				else
				{
					retornoXML.Append("<Resultado>");
					retornoXML.Append(ConstantesBolsa.NO);
					retornoXML.Append("</Resultado>");
				}
			}
			catch(Exception)
			{
				retornoXML.Remove(0, retornoXML.Length);
			}
			return retornoXML.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="codigoCuentaMDC"></param>
		/// <param name="codigoCliente"></param>
		/// <param name="codigoEmisionRV"></param>
		/// <returns></returns>
		public string ObtenerCantidadDeValorEnCuenta(string codigoCuentaMDC,string codigoCliente,int codigoEmisionRV)
		{
			string[] nombres={"CODEMISIONRV","CANTIDAD"};
			string[] valores={codigoEmisionRV.ToString(),"1000"};
			return construirXML("CANTIDADVALOR",nombres,valores);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="codigoEmisionRV"></param>
		/// <returns></returns>
		public string ObtenerPrecioValor(int codigoEmisionRV)
		{
			DataSet valorDataSet=new ValoresBolsa(CurrentInstanceName).ObtenerValorPorCodigo(codigoEmisionRV);
			DataRow valor=valorDataSet.Tables[0].Rows[0];
			string[] nombres={"CODEMISIONRV","PRECIO"};
			string[] valores={codigoEmisionRV.ToString(),valor["PRECIO"].ToString()};
			return construirXML("PRECIOVALOR",nombres,valores);
		}

//		public string ObtenerValorReferencialRV(int codigoEmisionRV)
//		{
//			StringBuilder retornoXML=new StringBuilder();
//
//			ValorReferencialDataSet valorReferencialDataSet=new ValorReferencial(CurrentInstanceName).obtenerPorCodigoEmision(codigoEmisionRV.ToString());
//			string[] nombres={"CODEMISIONRV","CODMONEDA","MNEMONICOVALOR","PORCENTAJEPRINCIPAL","PORCENTAJEGARANTIA"};
//			string[] valores={valorReferencialDataSet.SER_VALORREFERENCIAL[0].CODEMISIONRV.ToString(),
//							valorReferencialDataSet.SER_VALORREFERENCIAL[0].CODMONEDA.ToString(),
//							valorReferencialDataSet.SER_VALORREFERENCIAL[0].MNEMONICOVALOR.ToString(),
//							valorReferencialDataSet.SER_VALORREFERENCIAL[0].PORCENTAJEPRINCIPAL.ToString(),
//							valorReferencialDataSet.SER_VALORREFERENCIAL[0].PORCENTAJEGARANTIA.ToString()};
//			//string[] valores={"1","1","VALD","0.6","0.6"};
//			return construirXML("ValorReferencial",nombres,valores);
//		}

		/// <summary>
		/// Calcula la fecha de vencimiento a partir de la fecha presente 
		/// a un numero de dias habiles determinado por el parametro
		/// </summary>
		/// <param name="diasVigencia">Dias de vigencia</param>
		/// <returns></returns>
		public string CalcularFechaVencimiento(int diasVigencia)
		{

			StringBuilder retornoXML = new StringBuilder();
			try
			{
				CalendarioManager calendarioManager = new CalendarioManager(CurrentInstanceName);
				string fechaCalculada = String.Empty;//calendarioManager.CalcularDiaUtil(diasVigencia).ToString("dd/MM/yyyy");
				retornoXML.Append("<?xml version=\"1.0\" ?>");
				retornoXML.Append("<FechaCalculada>");
				retornoXML.Append(fechaCalculada);
				retornoXML.Append("</FechaCalculada>");
			}
			catch(Exception)
			{
				retornoXML.Remove(0, retornoXML.Length);
			}
			return retornoXML.ToString();
		}

		/// <summary>
		/// Obtiene los Totales de Ordenes Pendientes de una subasta y forma con ellos una cadena en XML.
		/// </summary>
		/// <param name="codigoSubasta">Código de la subasta a buscar.</param>
		/// <returns>Cadena XML conteniendo el total de órdenes pendientes.</returns>
		private string ObtenerTotalesOrdenesSubasta(int codigoSubasta)
		{
			StringBuilder retornoXML = new StringBuilder();
//			Seriva.Bolsa.Negocio.RentaFija.MercadoPrimario.Orden orden = new Seriva.Bolsa.Negocio.RentaFija.MercadoPrimario.Orden();
//			Seriva.Bolsa.Entidad.RentaFija.MercadoPrimario.OrdenTotalDataSet ordenTotalDataSet;
//			Seriva.Bolsa.Entidad.RentaFija.MercadoPrimario.OrdenTotalDataSet.SER_ORDENTOTALPORSUBASTARow totales;
//			try
//			{
//				ordenTotalDataSet = orden.ObtenerTotalesPorCodigoSubasta(codigoSubasta);
//
//				if(ordenTotalDataSet.SER_ORDENTOTALPORSUBASTA.Rows.Count == 0)	return String.Empty;
//
//				totales = ordenTotalDataSet.SER_ORDENTOTALPORSUBASTA[0];
//
//				retornoXML.Append("<?xml version=\"1.0\" ?>");
//				retornoXML.Append("<TotalOrdenesPendientes>");
//				retornoXML.Append("<PendienteActivacion>");
//				retornoXML.Append(totales.TOTALPENDIENTEACTIVACION);
//				retornoXML.Append("</PendienteActivacion>");
//				retornoXML.Append("<PendienteAnulacion>");
//				retornoXML.Append(totales.TOTALPENDIENTEANULACION);
//				retornoXML.Append("</PendienteAnulacion>");
//				retornoXML.Append("<PendienteIngresoAnulacion>");
//				retornoXML.Append(totales.TOTALPENDIENTEINGRESOANULACION);
//				retornoXML.Append("</PendienteIngresoAnulacion>");
//				retornoXML.Append("</TotalOrdenesPendientes>");
//
//			}
//			catch(Exception)
//			{
//				retornoXML.Remove(0, retornoXML.Length);
//			}

			return retornoXML.ToString();
		}

		#endregion Procesos

		#region Auxiliares

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nombreXML"></param>
		/// <param name="nombre"></param>
		/// <param name="valor"></param>
		/// <returns></returns>
		private string construirXML(string nombreXML,string[] nombre,string[] valor)
		{
			StringBuilder retornoXML=new StringBuilder();
			MemoryStream stream = new MemoryStream();
			XmlDocument xmlDocument=new XmlDocument();
			XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0","utf-8",null);

			XmlElement rootNode  = xmlDocument.CreateElement(nombreXML);
			xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
			xmlDocument.AppendChild(rootNode);

			XmlElement[] xmlElement=new XmlElement[nombre.Length];
			for (int i=0;i<nombre.Length;i++)
			{
				xmlElement[i]=xmlDocument.CreateElement(nombre[i]);
			}
			XmlText[] xmlText=new XmlText[nombre.Length];
			for (int i=0;i<nombre.Length;i++)
			{
				xmlText[i]=xmlDocument.CreateTextNode(valor[i]);
			}
			for (int i=0;i<nombre.Length;i++)
			{
				rootNode.AppendChild(xmlElement[i]);
			}
			for (int i=0;i<nombre.Length;i++)
			{
				xmlElement[i].AppendChild(xmlText[i]);
			}
			return xmlDocument.OuterXml;
		}

		/// <summary>
		/// Función que convierte un dataset a un string xml
		/// </summary>
		/// <param name="xmlDS"></param>
		/// <returns></returns>
		private string ConvertirDataSetAXML(DataSet xmlDS)
		{
			MemoryStream stream = null;
			XmlTextWriter writer = null;
			try
			{
				stream = new MemoryStream();
				writer = new XmlTextWriter(stream, Encoding.Unicode);
				xmlDS.WriteXml(writer,XmlWriteMode.IgnoreSchema);
				int count = (int) stream.Length;
				byte[] arr = new byte[count];
				stream.Seek(0, SeekOrigin.Begin);
				stream.Read(arr, 0, count);
				UnicodeEncoding utf = new UnicodeEncoding();
				return utf.GetString(arr).Trim();
			}
			catch
			{
				return String.Empty;
			}
			finally
			{
				if(writer != null) writer.Close();
			}
		}

		#endregion Auxiliares

	}
}
