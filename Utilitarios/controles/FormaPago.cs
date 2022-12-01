using System;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{
	/// <summary>
	/// Summary description for FormaPagoEventArgs.
	/// </summary>
	public class FormaPagoEventArgs : EventArgs
	{
		public FormaPagoEventArgs(string mensaje)
		{
			this.mensaje = mensaje;
		}

		#region Atributos
		private readonly string mensaje;
		#endregion Atributos

		#region Propiedades
		/// <summary>
		/// Mensaje de Error 
		/// </summary>
		public string Mensaje
		{
			get { return mensaje; }
		}
		#endregion Propiedades
	}
}
