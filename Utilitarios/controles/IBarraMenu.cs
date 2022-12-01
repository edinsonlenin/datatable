namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles
{
	/// <summary>
	/// Define el comportamiento que las páginas ASP.NET que implementan
	/// esta voiderfaz deben realizar con la barra de herramientas.
	/// </summary>
	public interface IBarraMenu
	{

		/// <summary>
		/// Define el comportamiento de almacenamiento del DataSet relacionado
		/// con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de dos
		/// valores posibles:
		/// <T>BarraOpcion.GRABAR_NO</T> en el caso de no grabar cuando
		/// se presione este botón
		/// <T>BarraOpcion.GRABAR_SI</T> en el caso contrario.
		/// </returns>
		void Grabar();

		/// <summary>
		/// Define el comportamiento de exportación de datos en la página
		/// ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.EXPORTAR_NO</T> en el caso de no grabar cuando
		/// se presione este botón
		/// <T>BarraOpcion.EXPORTAR_EXCEL</T> en el caso de exportar como
		/// archivo de Microsoft Excel
		/// </returns>
		void Exportar();

		/// <summary>
		/// Define el comportamiento de inserción de registros del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.INSERTAR_NO</T> en el caso de no insertar cuando
		/// se presione este botón
		/// <T>BarraOpcion.INSERTAR_ANTES</T> en el caso de insertar un
		/// registro antes del registro seleccionado
		/// <T>BarraOpcion.INSERTAR_DESPUES</T> en el caso de insertar un
		/// registro después del registro seleccionado
		/// </returns>
		void Insertar();

		/// <summary>
		/// Define el comportamiento de ordenamiento de registros del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// </returns>
		void Ordenar();

		/// <summary>
		/// Define el comportamiento de ordenamiento de registros del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// </returns>
		void Desordenar();

		/// <summary>
		/// Define el comportamiento de agregación de registros del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.AGREGAR_NO</T> en el caso de no agregar cuando
		/// se presione este botón
		/// <T>BarraOpcion.AGREGAR_SI</T> en el caso de agregar un registro
		/// al final
		/// </returns>
		void Agregar();

		/// <summary>
		/// Define el comportamiento de eliminación de registros del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.BORRAR_NO</T> en el caso de no borrar cuando
		/// se presione este botón
		/// <T>BarraOpcion.BORRAR_SI</T> en el caso de borrar el o los registros
		/// seleccionados
		/// </returns>
		void Borrar();

		/// <summary>
		/// Define el comportamiento de navegación al primer registro del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.PRIMERO_NO</T> en el caso de no navegar al primer
		/// registro cuando se presione este botón
		/// <T>BarraOpcion.PRIMERO_SI</T> en el caso contrario
		/// </returns>
		void Primero();

		/// <summary>
		/// Define el comportamiento de navegación al último registro del DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.ULTIMO_NO</T> en el caso de no navegar al último
		/// registro cuando se presione este botón
		/// <T>BarraOpcion.ULTIMO_SI</T> en el caso contrario
		/// </returns>
		void Ultimo();

		/// <summary>
		/// Define el comportamiento de búsqueda de registros en el DataSet
		/// relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.BUSCAR_NO</T> en el caso de no ejecutar la búsqueda
		/// cuando se presione este botón
		/// <T>BarraOpcion.BUSCAR_SI</T> en el caso contrario
		/// </returns>
		void Buscar();

		/// <summary>
		/// Define el comportamiento de filtrado de datos del DataSet relacionado
		/// con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.FILTRO_NO</T> en el caso de no realizar filtrado cuando
		/// se presione este botón
		/// <T>BarraOpcion.FILTRO_EXTENDIDO</T> en el caso de desplegar la pantalla
		/// de filtrado extendido
		/// <T>BarraOpcion.FILTRO_SIMPLE</T> en el caso de desplegar la pantalla de
		/// filtrado simple
		/// </returns>
		void Filtrar();

		/// <summary>
		/// Define el comportamiento de la presentación de la ayuda en la página
		/// ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.AYUDA_NO</T> en el caso de no desplegar ayuda cuando
		/// se presione este botón
		/// <T>BarraOpcion.AYUDA_COMPLETA</T> en el caso de desplegar todo la
		/// estructura de ayuda de la aplicación
		/// <T>BarraOpcion.AYUDA_ITEM</T> en el caso de desplegar únicamente el
		/// item actual de ayuda
		/// </returns>
		void Ayuda();

		/// <summary>
		/// Define el comportamiento de la navegación al registro anterior del
		/// DataSet relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.ANTERIOR_NO</T> en el caso de no regresar al registro
		/// anterior cuando se presione este botón
		/// <T>BarraOpcion.ANTERIOR_SI</T> en el caso contrario
		/// </returns>
		void Anterior();

		/// <summary>
		/// Define el comportamiento de la navegación al registro siguiente del
		/// DataSet relacionado con la página ASP.NET.
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este botón
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void Siguiente();

		/// <summary>
		/// Permite ocultar la fila seleccionada del grid
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este botón
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void OcultarFil();

		/// <summary>
		/// Permite ocultar las columna selecciona del grid
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este botón
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void OcultarCol();

		/// <summary>
		/// Muestra las filas que fueron ocultadas
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este botón
		/// <T>BarraOpcion.SIGUIENTE_SI</T> en el caso contrario
		/// </returns>
		void MostrarFil();

		/// <summary>
		/// Muestra las columnas que fueron ocultadas
		/// </summary>
		/// <returns>
		/// La implementación de este método puede devolver uno de los
		/// valores posibles siguientes:
		/// <T>BarraOpcion.SIGUIENTE_NO</T> en el caso de no avanzar al registro
		/// siguiente cuando se presione este botón
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
