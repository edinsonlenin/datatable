using System;
using System.Globalization;
using Seriva.Bolsa.Herramientas.Constantes;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
	/// <summary>
	/// Summary description for UtilitarioFormatos.
	/// Modificacion: Incremento constante longitud para cantidad entera. A.U.
	/// </summary>
	public class UtilitarioFormatos
	{
		#region  Constantes para formato
		public const int NOMBRECORTO				= 20;
		public const int SIGLAMONEDA				= 3;
		public const int NOMBRELARGO				= 100;
		public const int NOMBREMEDIANO				= 50;
		public const int DESCRIPCION				= 500;
		public const int IDENTIFICADORCORTO			= 20;
		public const int IDENTIFICADORLARGO			= 100;
		public const int SINO						= 1;
		public const int CODIFICACION				= 6;
		public const int LONGITUD_DINERO			= 18;
		public const int LONGITUDCANTIDADENTERACORTA= 5;
		public const int LONGITUDCANTIDADENTERA		= 10;

		/// <summary>
		/// ########.##
		/// </summary>
		public const string CANTIDAD = "########.##";
		/// <summary>
		/// ##########.##
		/// </summary>
		public const string CANTIDAD10 = "##########.##";
		/// <summary>
		/// $###,###.##
		/// </summary>
		public const string CANTIDADVISUAL1 = "$###,###.##";
		/// <summary>
		/// $####.###
		/// </summary>
		public const string CANTIDADVISUAL2 = "$####.###";
		/// <summary>
		/// #########
		/// </summary>
		public const string CANTIDADVISUAL3 = "#########";
		/// <summary>
		/// ###,###.##
		/// </summary>
		public const string CANTIDADVISUAL4 = "###,###.##";
		/// <summary>
		/// ###,###,###,###.##
		/// </summary>
		//public const string DINEROGRID = "###"+separador_dec+"###"+separador_dec+"###"+separador_dec+"###"+separador_dec+"##";
		/// <summary>
		/// ######
		/// </summary>
		public const string CANTIDADENTERA = "#####";
		/// <summary>
		/// ############.##
		/// </summary>
		public const string DINERO	= "###,###,###,###.##";
		/// <summary>
		/// #############.##
		/// </summary>
		public const string DINERO13	= "#,###,###,###,###.##";
		/// <summary>
		/// ############.####
		/// </summary>
		public const string PRECIO	= "##,###.########";

		/// <summary>
		/// ############.####
		/// </summary>
		public const string PRECIOTV	= "###.####";

		/// <summary>
		/// ###.##
		/// </summary>
		public const string TASA = "###.##";

		/// <summary>
		/// ##0.####
		/// </summary>
		public const string TASACERO = "##0.####";

		/// <summary>
		/// ####-###
		/// </summary>
		public const string TELEFONO = "####-###";

		/// <summary>
		/// ##,###.##
		/// </summary>
		public const string PRECIOVENTANILLA="##,###.##";

		#endregion

		/// <summary>
		/// Atributo que indica el formato de fecha definido inicial o el definido por el usuario
		/// </summary>
		private string FormatoFecha;
		/// <summary>
		/// Atributo que indica el string utilizado como separador de las fechas
		/// </summary>
		private string Separador;
		private static char separador_dec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0];//System.Globalization.CultureInfo.InstalledUICulture.NumberFormat.NumberDecimalSeparator.ToCharArray()[0];

		/// <summary>
		/// Retorna el formato de dinero a una grilla 2 decimales
		/// </summary>
		public static string DINEROGRILLA
		{
			get
			{
				string s_Separador = separador_dec.ToString();
				if (s_Separador == ".")
					return "###,###,###,###,##0.00";
				else
					return "###.###.###.###,##0.00";
			}
		}

		/// <summary>
		/// Retorna el formato de precio a una grilla 4 decimales
		/// </summary>
		public static string PRECIOGRID
		{
			get
			{
				string s_Separador = separador_dec.ToString();
				if (s_Separador == ".")
					return "###,###,###,##0.0000";
				else
					return "###.###.###.##0,0000";
			}
		}

		/// <summary>
		/// Retorna el formato de dinero a un control 2 decimales
		/// </summary>
		public static string FORMATO_DINERO
		{
			get
			{
				string s_Separador = separador_dec.ToString();
				if (s_Separador == ".")
					return "###,###,###,###,##0.00";
				else
					return "###.###.###.###,##0.00";
			}
		}

		/// <summary>
		/// Retorna el formato de número entero
		/// </summary>
		public int of_NumEnteros(string Texto)
		{
			if (Texto != null && Texto .Length > 0)
			{
				int i_PosPunto = Texto.IndexOf(separador_dec ,0,Texto.Length-1);
				if (i_PosPunto != -1)
				{
					return i_PosPunto;
				}
				else
					return Texto.Length;
			}
			return -1;
		}

		/// <summary>
		/// Retorna el número de decimales de un texto
		/// </summary>
		/// <param name="Texto"></param>
		/// <returns></returns>
		public int of_NumDecimales(string Texto)
		{
			if (Texto != null && Texto .Length > 0)
			{
				int i_PosPunto = Texto.IndexOf(separador_dec ,0,Texto.Length-1);
				if (i_PosPunto != -1)
				{
					return Texto.Length - i_PosPunto - 1;
				}
				else
					return 0;
			}
			return -1;
		}

		/// <summary>
		/// Transforma de un string con formato a un string número sin  formato
		/// </summary>
		/// <param name="Texto"></param>
		/// <returns></returns>
		public string of_StrAStrNumero(string Texto)
		{
			if(Texto.IndexOf("E")>-1)
				Texto="0";

			string ls_result = string.Empty;
			for(int i = 0; i < Texto.Length;i++)
			{
				if( Char.IsDigit(Texto,i) || Texto[i] ==  separador_dec || Texto[i] == '-')
					ls_result = ls_result + Texto[i];
			}
			if (ls_result == (separador_dec + "00") || ls_result == ("0" + separador_dec + "00"))
				ls_result = "0";

			ls_result = ls_result.Replace(separador_dec, '.');
			return ls_result;
		}

		/// <summary>
		/// Convierte a double un cadena
		/// </summary>
		/// <param name="Texto"></param>
		/// <returns></returns>
		public double ConvertirADouble(string Texto)
		{
			return double.Parse(this.of_StrAStrNumero(Texto));
		}

        /// <summary>
        /// Convierte a entero una cadena
        /// </summary>
        /// <param name="Texto"></param>
        /// <returns></returns>
		public Int32 ConvertirAEntero(string Texto)
		{
			return Int32.Parse(this.of_StrAStrNumero(Texto));
		}

		/// <summary>
		/// Convierte a decimal una cadena
		/// </summary>
		/// <param name="Texto"></param>
		/// <returns></returns>
		public decimal ConvertirADecimal(string Texto)
		{
			return decimal.Parse(this.of_StrAStrNumero(Texto));
		}

		/// <summary>
		/// Transforma de un string con formato a un string número sin  formato.
		/// Exclusivamente para generar Swift. 
		/// </summary>
		/// <param name="Texto"></param>
		/// <returns></returns>
		public string of_StrAStrNumeroComa(string Texto)
		{
			string ls_result = string.Empty;
			for(int i = 0; i < Texto.Length;i++)
			{
				if( Char.IsDigit(Texto,i) || Texto[i] ==  separador_dec)
					ls_result = ls_result + Texto[i];
			}
			if (ls_result == (separador_dec + "00") || ls_result == ("0" + separador_dec + "00"))
				ls_result = "0";
			ls_result = ls_result.Replace(separador_dec, ',');
			if ( ls_result.IndexOf(",") == -1)
				ls_result += ",00";
			return ls_result;
		}

		/// <summary>
		/// Utilizarlo solo en Capa de Persistencia.Constructor que setea el formato de la fecha con los parametros definidos
		/// en el archivo xml inicial.
		/// </summary>
		public UtilitarioFormatos(string s_Formato, string s_Separador)
		{
			this.FormatoFecha	= s_Formato;
			this.Separador 		= s_Separador;
		}

		/// <summary>
		/// Constructor que toma el formato de las fechas seleccionadas por el usuario
		/// Utilizarlo para formatos de interface. El formato del separador es fijo "/"
		/// </summary>
		/// <param name="s_formato">Corresponde al formato de la fecha</param>
		public UtilitarioFormatos(string s_formato)
		{
			this.FormatoFecha	= s_formato;
			this.Separador 		= "/";

		}

		public UtilitarioFormatos(){
		}

		/// <summary>
		/// Retorna el formato de la fecha pudiendo ser: El formato del usuario (Cuando el constructor recibe como parámetro elusuario) y
		/// El formato de la base de datos (Cuando no existen parámetros en el constructor)
		/// </summary>
		/// <returns>string</returns>
		public string of_ObtenerFormatoFecha()
		{
			return this.FormatoFecha;
		}

		/// <summary>
		/// Retorna la posición del primer dígito del día de acuerdo al formato 
		/// definido por el constructor
		/// </summary>
		/// <returns>int</returns>
		private int PosicionDia()
		{
			int i_PosicionDia = 0;
			i_PosicionDia = this.FormatoFecha.IndexOf("d");
			return i_PosicionDia;
		}

		/// <summary>
		/// Retorna la posición del primer dígito del mes de acuerdo al formato 
		/// definido por el constructor
		/// </summary>
		/// <returns>int</returns>
		private int PosicionMes()
		{
			int i_PosicionMes = 0;
			i_PosicionMes = this.FormatoFecha.IndexOf("M");
			return i_PosicionMes;
		}

		/// <summary>
		/// Retorna la posición del primer dígito del año de acuerdo al formato 
		/// definido por el constructor
		/// </summary>
		/// <returns>int</returns>
		private int PosicionAnio()
		{
			int i_PosicionAnio = this.FormatoFecha.IndexOf("y");
			return i_PosicionAnio;
		}

		/// <summary>
		/// Utilizado solo en capa de persistencia. Convierte un DateTime a un String en el formato de fechas
		/// definido para el motor de la DB dento del archivo xml inicial.
		/// </summary>
		/// <param name="dt_Fecha">Corresponde a la fecha a formatearse</param>
		/// <returns>Retorna un string con el formato de la base de datos</returns>
		public string of_FormatearFechaDataBase(DateTime dt_Fecha)
		{
			string s_FechaFormateada = string.Empty;
			string Horas			 = string.Empty;
			if (this.FormatoFecha != "")
			{
				int	posicion1 = FormatoFecha.IndexOf(this.Separador , 0,this.FormatoFecha.Length);
				int	posicion2 = FormatoFecha.IndexOf(this.Separador , posicion1+1, FormatoFecha.Length-(posicion1+1));

				string s_Primer			= FormatoFecha.Substring(0,posicion1);
				string s_Segundo		= FormatoFecha.Substring(posicion1+1,posicion2-posicion1-1);
				string s_Dia			= dt_Fecha.Day.ToString();
				string s_Mes			= dt_Fecha.Month.ToString();
				string s_Anio			= dt_Fecha.Year.ToString();
				string s_Hora			= dt_Fecha.Hour.ToString();
				string s_Minuto			= dt_Fecha.Minute.ToString();
				string s_Segundos		= dt_Fecha.Second.ToString();
				string s_MiliSegundos	= dt_Fecha.Millisecond.ToString();

				if (s_Hora != "0")
					Horas = " " + s_Hora + ":" + s_Minuto + ":" +s_Segundos + "." + s_MiliSegundos;

				switch(s_Primer)
				{
					case "dd":
						if (s_Segundo == "mm")
							s_FechaFormateada = s_Dia + this.Separador  + s_Mes + this.Separador  + s_Anio;
						else
							s_FechaFormateada = s_Dia + this.Separador  + s_Anio + this.Separador  + s_Mes;
						break;
					case "mm":
						if (s_Segundo == "dd")
							s_FechaFormateada = s_Mes + this.Separador  + s_Dia + this.Separador  + s_Anio;
						else
							s_FechaFormateada = s_Mes + this.Separador  + s_Anio +this.Separador  + s_Dia;
						break;
					case "yyyy":
						if (s_Segundo == "dd")
							s_FechaFormateada = s_Anio + this.Separador  + s_Dia + this.Separador  + s_Mes;
						else
							s_FechaFormateada = s_Anio + this.Separador  + s_Mes + this.Separador  + s_Dia;
						break;

				}
			}
			return s_FechaFormateada + Horas;
		}

		/// <summary>
		/// Convierte un objeto DateTime a un objeto String con el formato
		/// completo incluyendo horas, minutos y segundos
		/// </summary>
		/// <param name="dt_Fecha">Corresponde a la fecha a ser formateada</param>
		/// <param name="s_Formato">Corresponde al formato de la fecha, si
		/// el parámetro es null toma el formato definido por el usuario</param>
		/// <returns>Retorna un string con el formato definido del usuario</returns>
		public string of_ConvertirAStringConFormatoLargo(DateTime dt_Fecha, string s_Formato)
		{
			string s_FechaFormateada = string.Empty;
			if (dt_Fecha.CompareTo(null) > 0)
			{
				string s_FormatoFechaCompleto = string.Empty;
				s_FormatoFechaCompleto = this.FormatoFecha + " hh:mm:ss.fff";
				if (s_Formato != null)
					s_FechaFormateada = dt_Fecha.ToString(s_Formato);
				else
					s_FechaFormateada = dt_Fecha.ToString(s_FormatoFechaCompleto);
			}
			return s_FechaFormateada;
		}

		/// <summary>
		/// Convierte un objeto DateTime a un objeto String con el formato corto.
		/// </summary>
		/// <param name="dt_Fecha">Corresponde a la fecha a ser formateada</param>
		/// <param name="s_Formato">Corresponde al formato de la fecha,
		/// si el parametro es null toma el formato del usuario</param>
		/// <returns>Retorna un string con el formato definido del usuario</returns>
		public string of_ConvertirAStringConFormatoCorto(DateTime dt_Fecha, string s_Formato)
		{
			string s_FechaFormateada = string.Empty;
			if (dt_Fecha.CompareTo(null) > 0)
			{

				if (s_Formato != null)
					s_FechaFormateada = dt_Fecha.ToString(s_Formato);
				else
					s_FechaFormateada = dt_Fecha.ToString(this.FormatoFecha);
			}
			return s_FechaFormateada;
		}

		/// <summary>
		/// Convierte un objeto String (con el formato de fecha del usuario)
		/// a un objeto DateTime
		/// </summary>
		/// <param name="Fecha">Corresponde a la fecha tipo string (en el formato de fecha definido por el usuario)</param>
		/// <returns>DateTime</returns>
		public DateTime of_ConvertirADateTimeConFormato(string Fecha)
		{
			string dia		= string.Empty;
			string mes		= string.Empty;
			string anio		= string.Empty;
			string hora		= string.Empty;
			string minuto	= string.Empty;
			string segundo	= string.Empty;
			string milisegundo	= string.Empty;

			DateTime FechaRetorno= new DateTime(100);
			if (Fecha != "")
			{

				try
				{
					int	posicion1 = Fecha.IndexOf(this.Separador);
					int	posicion2 = Fecha.IndexOf(this.Separador ,posicion1+1);
					int posicion3 = Fecha.IndexOf(" ");
					int posicion4 = Fecha.IndexOf(":");
					int posicion5 = Fecha.IndexOf(":", posicion4+1);
					int posicion6 = Fecha.IndexOf(".");
					int tam1 = posicion1;
					int tam2 = posicion2  - (posicion1+1);
					int tam3 = 0;
					if (posicion3 >= 0)
						tam3 = posicion3  -  posicion2-1;
					else
						tam3 = Fecha.Length-1  -  posicion2;
					int tam4 = posicion4  -  posicion3-1;
					int tam5 = posicion5  -  posicion4-1;
					int tam6 = posicion6  -  posicion5-1;
					int tam7 = (Fecha.Length) - posicion6-1;

					if (posicion4 >= 0)
					{
						hora = Fecha.Substring(posicion3+1,tam4);
						minuto = Fecha.Substring(posicion4+1,tam5);
						if (posicion6 > 0)
						{
							segundo = Fecha.Substring(posicion5+1,tam6);
							milisegundo = Fecha.Substring(posicion6+1,tam7);
						}
						else
						{
							segundo = Fecha.Substring(posicion5+1);
							milisegundo = "000";
						}
					}

					int	posformato1 = FormatoFecha.IndexOf(this.Separador);
					string s_Primer			= FormatoFecha.Substring(0,1);
					string s_Segundo		= FormatoFecha.Substring(posformato1+1,1);

					switch(s_Primer)
					{
						case "d":
							if (s_Segundo == "M")
							{
								dia = Fecha.Substring(0 ,tam1);
								mes = Fecha.Substring(posicion1+1 ,tam2);
								anio =Fecha.Substring(posicion2+1 ,tam3);
							}
							else
							{
								dia = Fecha.Substring(0 ,tam1);
								anio =Fecha.Substring(posicion1+1 ,tam2);
								mes = Fecha.Substring(posicion2+1 ,tam3);
							}

							break;
						case "M":
							if (s_Segundo == "d")
							{
								mes = Fecha.Substring(0 ,tam1);
								dia = Fecha.Substring(posicion1+1 ,tam2);
								anio =Fecha.Substring(posicion2+1 ,tam3);
							}
							else
							{
								mes = Fecha.Substring(0 ,tam1);
								anio =Fecha.Substring(posicion2+1 ,tam3);
								dia = Fecha.Substring(posicion2+1 ,tam3);
							}
							break;
						case "y":
							if (s_Segundo == "d")
							{
								anio =Fecha.Substring(0 ,tam1);
								dia = Fecha.Substring(posicion1+1 ,tam2);
								mes = Fecha.Substring( posicion2+1,tam3);
							}
							else
							{
								anio =Fecha.Substring(0 ,tam1);
								mes = Fecha.Substring(posicion1+1,tam2);
								dia = Fecha.Substring(posicion2+1 ,tam3);
							}
							break;
					}

					if (posicion3 >= 0)
						FechaRetorno = new DateTime(Int32.Parse(anio),Int32.Parse(mes),Int32.Parse(dia),Int32.Parse(hora),Int32.Parse(minuto),Int32.Parse(segundo),Int32.Parse(milisegundo));
					else
						FechaRetorno = new DateTime(Int32.Parse(anio),Int32.Parse(mes),Int32.Parse(dia));
					return FechaRetorno;
				}
				catch(Exception e)
				{
					throw new Exception("Error el formato ingresado no corresponde con el definido por el usuario", e);
				}
			}

			return FechaRetorno;
		}

		/// <summary>
		/// Retorna una cadena con formato númerico
		/// </summary>
		/// <param name="d_Valor"></param>
		/// <param name="i_NumDecimal"></param>
		/// <returns></returns>
		public string aplicaFormato(double d_Valor, int i_NumDecimal)
		{
			string decimales = d_Valor.ToString();
			string decimos   = string.Empty;
			int inicio = 0;

			char s_Separador = separador_dec;
			string s_texto = d_Valor.ToString();

			//FORMATEADOR

			if(decimales.IndexOf(separador_dec) != -1)
			{
				if(decimales[0] == '-') inicio = 1;
				decimales = s_texto.Substring(inicio, s_texto.IndexOf(separador_dec)-inicio);
				decimos   = s_texto.Substring(s_texto.IndexOf(separador_dec)+1);
			}

			for(int i= decimos.Length ; i< i_NumDecimal; i++)
				decimos += "0";

			if(i_NumDecimal > 0)
				decimos = separador_dec + decimos.Substring(0,i_NumDecimal);

			s_texto = string.Empty;
			if(inicio == 1) s_texto += '-';
			s_texto += formatoDecimal(decimales) + decimos;

			if(s_texto.IndexOf('-') != -1)
			{
				int indice = s_texto.IndexOf('-');
				s_texto = s_texto.Substring(0, indice) + "(-)" + s_texto.Substring(indice + 1);
			}

			return s_texto;
		}

		/// <summary>
		/// Retorna una cadena con formato decimal
		/// </summary>
		/// <param name="decimales"></param>
		/// <returns></returns>
		private string formatoDecimal(string decimales)
		{
			int seis = Int32.Parse(Math.Floor((Decimal)(decimales.Length - 1) / 6).ToString());
			int tres = Int32.Parse(Math.Floor((Decimal)(decimales.Length + 2) / 6).ToString());
			int i = 0, j = 1;

			string s_SeparadorMiles = ",";
			if (separador_dec == ',')
				s_SeparadorMiles = ".";

			while(i < tres)
			{
				string parteA = decimales.Substring(0, decimales.Length - 3 - (6 * i) - i - (j - 1));
				string parteB = decimales.Substring(decimales.Length - 3 - (6 * i) - i - (j - 1));
				decimales = parteA + s_SeparadorMiles + parteB;
				i++;

				if(j <= seis)
				{
					string _parteA = decimales.Substring(0, decimales.Length - (6 * j) - i - (j - 1));
					string _parteB = decimales.Substring(decimales.Length - (6 * j) - i - (j - 1));
					decimales = _parteA + s_SeparadorMiles + _parteB;
					j++;
				}
			}

			return decimales;
		}

		/// <summary>
		/// Restaura el texto
		/// </summary>
		/// <param name="d_Valor"></param>
		/// <returns></returns>
		public string of_RestauraTexto(double d_Valor)
		{
			char s_Separador = separador_dec;
			char s_separadormil;
			string s_texto = d_Valor.ToString();

			if (s_Separador == '.')
				s_separadormil = ',';
			else
				s_separadormil = '.';

			if(s_texto.IndexOf('\'') != -1 || s_texto.IndexOf(s_separadormil) != -1)
			{
				string resultA = string.Empty;
				for(int i = 0; i < s_texto.Length; i++)
				{
					char car = s_texto[i];
					if(car != s_separadormil && car != '\'')
						resultA = resultA + s_texto[i];
				}

				s_texto = resultA;
			}

			if (s_texto == ".00" || s_texto == ",00")
				s_texto = "0";

			s_texto = s_texto.Replace(s_Separador, '.');
			return s_texto;
		}

		/// <summary>
		/// Método que retorna una fecha en forma de número, el formato de retorno es AAAAMMDD
		/// </summary>
		/// <param name="dt_Fecha">Corresponde a la fecha</param>
		/// <returns></returns>
		public static int of_ObtenerFechaFormatoNumero(DateTime dt_Fecha)
		{
			string s_Anio = string.Empty;
			string s_Mes = string.Empty;
			string s_Dia = string.Empty;
			string s_Fecha = string.Empty;

			s_Anio	= dt_Fecha.Year.ToString();
			if (dt_Fecha.Month.ToString().Length == 1)
				s_Mes	= "0"+dt_Fecha.Month.ToString();
			else
				s_Mes	= dt_Fecha.Month.ToString();
			if (dt_Fecha.Day.ToString().Length == 1)
				s_Dia	= "0"+dt_Fecha.Day.ToString();
			else
				s_Dia	= dt_Fecha.Day.ToString();
			s_Fecha	= s_Anio+s_Mes+s_Dia;

			return Int32.Parse(s_Fecha);

		}

		/// <summary>
		/// Método que limpia de caracteres especiales a un texto que puede ser ingresador por el usuario
		/// </summary>
		/// <param name="s_TextoIngresado">Corresponde al texto que se desea limpiar</param>
		/// <returns>Corresponde al string original sin caracteres especiales</returns>
		public static string of_LimpiaCaracteresEspeciales(string s_TextoIngresado)
		{
			if (s_TextoIngresado.Trim() != "")
			{
				s_TextoIngresado.Replace("'","");
				s_TextoIngresado.Replace("<","");
				s_TextoIngresado.Replace(">","");
			}
			return s_TextoIngresado;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fecha"></param>
		/// <param name="tipoFormato"></param>
		/// <returns></returns>
		public static string of_ObtenerFechaStringConFormato(DateTime fecha,string tipoFormato)
		{
			string fechaString = String.Empty;

			if(tipoFormato==ConstantesBolsa.FORMATO_FECHA_DIA_MES_ANIO)
				fechaString = fecha.Day.ToString().PadLeft(2,'0') + "/" + fecha.Month.ToString().PadLeft(2,'0') + "/" + fecha.Year.ToString();
			else if(tipoFormato==ConstantesBolsa.FORMATO_FECHA_MES_DIA_ANIO)
				fechaString = fecha.Month.ToString().PadLeft(2,'0') + "/" + fecha.Day.ToString().PadLeft(2,'0') + "/" + fecha.Year.ToString();
			else if(tipoFormato==ConstantesBolsa.FORMATO_FECHA_ANIO_MES_DIA)
				fechaString = fecha.Year.ToString().PadLeft(2,'0') + "/" + fecha.Month.ToString().PadLeft(2,'0') + "/" + fecha.Day.ToString();

			return fechaString;

		}

		public static DateTime of_ObtenerFechaDBFromString(string fecha,string Formato)
		{
			int posAnio=0;
			int posMes=0;
			int posDia=0;

			string[] arrFormato = Formato.Split('/');
			string[] arrFecha = Formato.Split('/');

			for(int i=0;i<3;i++)
			{
				if((arrFormato[i].ToUpper()=="YYYY")||(arrFormato[i].ToUpper()=="YY"))
				{
					posAnio=i;
					break;
				}
			}
			for(int i=0;i<3;i++)
			{
				if(arrFormato[i].ToUpper()=="MM")
				{
					posMes=i;
					break;
				}
			}
			for(int i=0;i<3;i++)
			{
				if(arrFormato[i].ToUpper()=="DD")
				{
					posDia=i;
					break;
				}
			}

			return Convert.ToDateTime(arrFecha[posAnio] + "/" + arrFecha[posMes] + "/" + arrFecha[posDia]);
		}

	}
}
