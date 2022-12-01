using System;
using System.Web.UI.WebControls;
using Infragistics.WebUI.Misc;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.UltraWebTab;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.WebDataInput;
using Infragistics.WebUI.WebSchedule;
using Seriva.Bolsa.Herramientas.Constantes;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
	#region enumeraciones
	/// <summary>
	/// Enumeración que incluye los formatos de los tabulados 
	/// </summary>grillas
	public enum FormatoTabulado
	{
		Estandar = 1,Clasico = 2
	};

	/// <summary>
	/// Enumeración que incluye los formatos de las
	/// </summary>
	public enum FormatoGrilla
	{
		Estandar = 1, EstandarSinFilaSeleccionada = 2,
		CatalogoEstandar = 3, Paginada = 4

	};

	/// <summary>
	/// Enumeración que incluye los formatos de los webcombos 
	/// </summary>grillas
	public enum FormatoWebCombo
	{
		Estandar = 1,Clasico = 2
	};

	public enum FormatoPanel
	{
		Estandar,
		Clasico
	}
	#endregion enumeraciones

	/// <summary>
	/// Clase para darle formato a un control
	/// </summary>
	public class FormatoControl
	{
		string pathImagenes = ".." + ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.Substring(ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.IndexOf("/",2)+1);
		private void obtenerPathImagenes(string path)
		{
			pathImagenes = string.Empty;

			string[] resPath = path.Split('/');

			for(int i = 1; i < resPath.Length - 2; i++)
			{
				pathImagenes = pathImagenes + "../";
			}

			pathImagenes = pathImagenes + ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.Substring(ConstantesPresentacion.RUTA_COMPLETA_IMAGENES.IndexOf("/",2)+1);
		}

		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public FormatoControl()
		{
		}

		/// <summary>
		/// Aplica el formato sobre UltraWebGrid
		/// </summary>
		/// <param name="grilla">UltraWebGrid al que se le aplicará un formato</param>
		/// <param name="formato">El Formato que se aplicará</param>
		public void AplicarFormato(UltraWebGrid grilla, FormatoGrilla formato)
		{
			if(grilla!=null)
			{
				AplicarFormato(grilla, formato, -1);
			}
		}

		/// <summary>
		/// Aplica el formato sobre UltraWebGrid
		/// </summary>
		/// <param name="grilla">UltraWebGrid al que se le aplicará un formato</param>
		/// <param name="formato">El Formato que se aplicará</param>
		/// <param name="banda">Banda que será modificada</param>
		public void AplicarFormato(UltraWebGrid grilla, FormatoGrilla formato,int banda)
		{
			Unit width = grilla.Width;
			Unit height = grilla.Height;

			string grafico= ConstantesPresentacion.RUTA_COMPLETA_IMAGENES   + "HeaderFondo.jpg";

			grilla.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaGrid.xml", false);
			grilla.DisplayLayout.HeaderStyleDefault.BackgroundImage=grafico;
 		    grilla.DisplayLayout.NoDataMessage = string.Empty;
			grilla.BorderStyle = BorderStyle.None;
			grilla.DisplayLayout.AllowColSizingDefault = AllowSizing.Free;
			grilla.DisplayLayout.StationaryMargins = StationaryMargins.HeaderAndFooter;
			grilla.Height = height;
			grilla.Width = width;
			grilla.DisplayLayout.HeaderStyleDefault.CustomRules = "background-repeat:repeat-x";
			switch (formato)
			{
				case FormatoGrilla.Estandar:
					break;
				case FormatoGrilla.EstandarSinFilaSeleccionada:
					//Para que edite la celda seleccionada
					if (banda > 0)
					{
						if (banda <= grilla.Bands.Count)
						{
							grilla.Bands[banda].CellClickAction = CellClickAction.Edit;
							grilla.Bands[banda].AllowUpdate = AllowUpdate.Yes;
							grilla.Bands[banda].AllowAdd = AllowAddNew.Yes;
							grilla.Bands[banda].AllowDelete = AllowDelete.Yes;
						}
					}
					else
					{
						grilla.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
						grilla.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
						grilla.DisplayLayout.AllowAddNewDefault = AllowAddNew.Yes;
						grilla.DisplayLayout.AllowDeleteDefault = AllowDelete.Yes;
					}
					break;
				case FormatoGrilla.Paginada:
					grilla.DisplayLayout.Pager.AllowPaging		= true;
					grilla.DisplayLayout.Pager.PageSize			= ConstantesPresentacion.FILASPORPAGINA_GRILLA;
					break;
				case FormatoGrilla.CatalogoEstandar:
					//Para dar el tamaño del grid
					grilla.Width = Unit.Percentage(100);
					grilla.Height = Unit.Percentage(100);
					break;
			}
		}

		public void AplicarFormato(UltraWebTab tabulado)
		{
			tabulado.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "/Utilitarios/SerivaTab.xml", true);
			tabulado.LoadAllTargetUrls = false;
		}

		/// <summary>
		/// Aplica el formato sobre UltraWebTab
		/// </summary>
		/// <param name="tabulado">UltraWebTab sobre el que se aplicará el formato</param>
		/// <param name="formato">Formato que se desea aplicar</param>
		public void AplicarFormato(UltraWebTab tabulado, int formato)
		{
			switch( formato )
			{
				case 1:
				case 2:
					this.AplicarFormato(tabulado);
					break;
			}
		}

		public void AplicarFormato(UltraWebTab tabulado, int formato, string rutaPagina)
		{
			obtenerPathImagenes(rutaPagina);
			AplicarFormato(tabulado, formato);
		}

		public void AplicarFormato(WebPanel panel, FormatoPanel formato)
		{
			panel.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaPanel.xml", false);

			switch(formato)
			{
				case FormatoPanel.Estandar:
					panel.BorderStyle = BorderStyle.None;
					break;
				case FormatoPanel.Clasico:
					panel.Header.TextAlignment = Infragistics.WebUI.Misc.TextAlignment.Left;
					break;
			}
		}

		/// <summary>
		/// Aplica el formato sobre WebCombo
		/// </summary>
		/// <param name="combo">WebCombo al que se le aplicará un formato</param>
		public void AplicarFormato(WebCombo combo)
		{
			AplicarFormato(combo, new Unit(combo.Width.Value*1.1, UnitType.Percentage), new Unit(100, UnitType.Pixel));
			//AplicarFormato(combo, new Unit(combo.Width.Value*1.1, UnitType.Percentage), new Unit(120, UnitType.Pixel));
		}

		public void AplicarFormato(WebCombo webCombo, int formato)
		{
			switch( formato )
			{
				case 1:
					AplicarFormato(webCombo);
					break;
				case 2:
					int anchoColumnas = 0;
					int nroColumnasVisibles = 0;
					foreach(Infragistics.WebUI.UltraWebGrid.UltraGridColumn columna in webCombo.Columns)
					{
						if(columna.Hidden==false)
						{
							anchoColumnas = anchoColumnas + Convert.ToInt32(columna.Width.Value);
							nroColumnasVisibles++;
						}
					}
					AplicarFormato(webCombo);
					webCombo.DropDownLayout.DropdownWidth = Unit.Pixel(anchoColumnas + Convert.ToInt32(webCombo.DropDownLayout.FrameStyle.BorderWidth.Value*2) +Convert.ToInt32(webCombo.DropDownLayout.CellSpacing*nroColumnasVisibles+1));
					break;
			}
		}

		/// <summary>
		/// Aplica el formato sobre WebCombo
		/// </summary>
		/// <param name="combo">WebCombo al que se le aplicará un formato</param>
		/// <param name="ancho">Ancho del DropDown</param>
		public void AplicarFormato(WebCombo combo, Unit ancho)
		{
			//AplicarFormato(combo, ancho, new Unit(120, UnitType.Pixel));
			AplicarFormato(combo, ancho, new Unit(80, UnitType.Pixel));
		}

		/// <summary>
		/// Aplica el formato sobre WebCombo
		/// </summary>
		/// <param name="combo">WebCombo al que se le aplicará un formato</param>
		/// <param name="ancho">Ancho del DropDown</param>
		/// <param name="alto">Alto del DropDown</param>
		public void AplicarFormato(WebCombo combo, Unit ancho, Unit alto)
		{
			combo.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaCombo.xml", false);
			combo.DropDownLayout.DropdownWidth = ancho;
			combo.DropDownLayout.DropdownHeight = alto;
			combo.DropDownLayout.HeaderStyle.BackgroundImage= ConstantesPresentacion.RUTA_COMPLETA_IMAGENES + "HeaderFondo.jpg";

		}

		/// <summary>
		/// Aplica formato sobre un WebDateChooser
		/// </summary>
		/// <param name="calendario">WebDateChooser al que se le aplicará el formato</param>
		public void AplicarFormato(WebDateChooser calendario)
		{
			if(calendario!=null)
			{
				calendario.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaDateChooser.xml", false);
			}
		}

		public void AplicarFormato(WebNumericEdit editor)
		{
			if (editor != null)
			{
				editor.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaNumericEdit.xml", false);
			}
		}

		public void AplicarFormato(WebPercentEdit editor)
		{
			if (editor != null)
			{
				editor.LoadPreset(AppDomain.CurrentDomain.BaseDirectory + "Utilitarios/SerivaPercentEdit.xml", false);
			}
		}

	}
}
