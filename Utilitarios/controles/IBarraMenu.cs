namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{
	/// <summary>
	/// Define el comportamiento que las p�ginas ASP.NET que implementan
	/// esta voiderfaz deben realizar con la barra de herramientas.
	/// </summary>
	public interface IBarraMenu
	{

		/// <summary>
		/// Define el comportamiento de almacenamiento del DataSet relacionado
		/// con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de dos
		/// valores posibles:
		/// <T>BarraOpcion.GRABAR_NO</T> en el caso de no grabar cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.GRABAR_SI</T> en el caso contrario.
		/// </returns>
		void Grabar();

		/// <summary>
		/// Define el comportamiento de exportaci�n de datos en la p�gina
		/// ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.EXPORTAR_NO</T> en el caso de no grabar cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.EXPORTAR_EXCEL</T> en el caso de exportar como
		/// archivo de Microsoft Excel
		/// </returns>
		void Exportar();

		/// <summary>
		/// Define el comportamiento de inserci�n de registros del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.INSERTAR_NO</T> en el caso de no insertar cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.INSERTAR_ANTES</T> en el caso de insertar un
		/// registro antes del registro seleccionado
		/// <T>BarraOpcion.INSERTAR_DESPUES</T> en el caso de insertar un
		/// registro despu�s del registro seleccionado
		/// </returns>
		void Insertar();

		/// <summary>
		/// Define el comportamiento de ordenamiento de registros del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// </returns>
		void Ordenar();

		/// <summary>
		/// Define el comportamiento de ordenamiento de registros del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// </returns>
		void Desordenar();

		/// <summary>
		/// Define el comportamiento de agregaci�n de registros del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.AGREGAR_NO</T> en el caso de no agregar cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.AGREGAR_SI</T> en el caso de agregar un registro
		/// al final
		/// </returns>
		void Agregar();

		/// <summary>
		/// Define el comportamiento de eliminaci�n de registros del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.BORRAR_NO</T> en el caso de no borrar cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.BORRAR_SI</T> en el caso de borrar el o los registros
		/// seleccionados
		/// </returns>
		void Borrar();

		/// <summary>
		/// Define el comportamiento de navegaci�n al primer registro del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.PRIMERO_NO</T> en el caso de no navegar al primer
		/// registro cuando se presione este bot�n
		/// <T>BarraOpcion.PRIMERO_SI</T> en el caso contrario
		/// </returns>
		void Primero();

		/// <summary>
		/// Define el comportamiento de navegaci�n al �ltimo registro del DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.ULTIMO_NO</T> en el caso de no navegar al �ltimo
		/// registro cuando se presione este bot�n
		/// <T>BarraOpcion.ULTIMO_SI</T> en el caso contrario
		/// </returns>
		void Ultimo();

		/// <summary>
		/// Define el comportamiento de b�squeda de registros en el DataSet
		/// relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.BUSCAR_NO</T> en el caso de no ejecutar la b�squeda
		/// cuando se presione este bot�n
		/// <T>BarraOpcion.BUSCAR_SI</T> en el caso contrario
		/// </returns>
		void Buscar();

		/// <summary>
		/// Define el comportamiento de filtrado de datos del DataSet relacionado
		/// con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.FILTRO_NO</T> en el caso de no realizar filtrado cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.FILTRO_EXTENDIDO</T> en el caso de desplegar la pantalla
		/// de filtrado extendido
		/// <T>BarraOpcion.FILTRO_SIMPLE</T> en el caso de desplegar la pantalla de
		/// filtrado simple
		/// </returns>
		void Filtrar();

		/// <summary>
		/// Define el comportamiento de la presentaci�n de la ayuda en la p�gina
		/// ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.AYUDA_NO</T> en el caso de no desplegar ayuda cuando
		/// se presione este bot�n
		/// <T>BarraOpcion.AYUDA_COMPLETA</T> en el caso de desplegar todo la
		/// estructura de ayuda de la aplicaci�n
		/// <T>BarraOpcion.AYUDA_ITEM</T> en el caso de desplegar �nicamente el
		/// item actual de ayuda
		/// </returns>
		void Ayuda();

		/// <summary>
		/// Define el comportamiento de la navegaci�n al registro anterior del
		/// DataSet relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.ANTERIOR_NO</T> en el caso de no regresar al registro
		/// anterior cuando se presione este bot�n
		/// <T>BarraOpcion.ANTERIOR_SI</T> en el caso contrario
		/// </returns>
		void Anterior();

		/// <summary>
		/// Define el comportamiento de la navegaci�n al registro siguiente del
		/// DataSet relacionado con la p�gina ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este bot�n
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void Siguiente();

		/// <summary>
		/// Permite ocultar la fila seleccionada del grid
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este bot�n
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void OcultarFil();

		/// <summary>
		/// Permite ocultar las columna selecciona del grid
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este bot�n
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void OcultarCol();

		/// <summary>
		/// Muestra las filas que fueron ocultadas
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este bot�n
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void MostrarFil();

		/// <summary>
		/// Muestra las columnas que fueron ocultadas
		/// </summary>
		/// <returns>
		/// La implementaci�n de este m�todo puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este bot�n
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void MostrarCol();

		/// <summary>
		/// Permite que el grid tenga la opcion de agrupamiento
		/// </summary>
		void Agrupar();

		/// <summary>
		/// Hace que el grid vuelva a su forma original
		/// </summary>
		void Desagrupar();

	}

}
