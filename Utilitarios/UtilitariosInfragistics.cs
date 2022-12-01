using System.Drawing;
using System.Web.UI.WebControls;

using Infragistics.WebUI.UltraWebListbar;
using Infragistics.WebUI.UltraWebToolbar;
using Infragistics.WebUI.UltraWebNavigator;

namespace Seriva.Bolsa.Presentacion.Utilitarios
{
    public enum StyleControl
		{
			Ofice2003Blue		= 0,
			Ofice2003Gray		= 1,
			MSDNBlue			= 2,
		}

		/// <summary>
		/// Summary description for UtilesInfragistics.
		/// </summary>
		public class UtilesInfragistics
		{

			/// <summary>
			/// 
			/// </summary>
			/// <param name="grupo"></param>
			public static void AsignarEstiloControlGrupo(Group controlGrupo, StyleControl styleControl)
			{
				switch( styleControl )
				{
					case	StyleControl.Ofice2003Blue:

						#region Item Syle
						//controlGrupo.DefaultItemStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-blu.gif";
						controlGrupo.DefaultItemStyle.BorderStyle = BorderStyle.None;
						controlGrupo.DefaultItemStyle.BorderWidth = Unit.Pixel(0);
						controlGrupo.DefaultItemStyle.Font.Bold = false;
						controlGrupo.DefaultItemStyle.Font.Name = "Verdana";
						controlGrupo.DefaultItemStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.DefaultItemStyle.Font.Size = new FontUnit("7pt");

						controlGrupo.DefaultItemStyle.Height = Unit.Pixel(16);
						controlGrupo.DefaultItemStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.DefaultItemStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.DefaultItemStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.DefaultItemStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Item Hover Style
						controlGrupo.DefaultItemHoverStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						controlGrupo.DefaultItemHoverStyle.BorderStyle = BorderStyle.None;
						controlGrupo.DefaultItemHoverStyle.BorderWidth = Unit.Pixel(0);
						controlGrupo.DefaultItemHoverStyle.Font.Bold = false;
						controlGrupo.DefaultItemHoverStyle.Font.Name = "Verdana";
						controlGrupo.DefaultItemHoverStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.DefaultItemHoverStyle.Font.Size = new FontUnit("7pt");

						controlGrupo.DefaultItemHoverStyle.Height = Unit.Pixel(16);
						controlGrupo.DefaultItemHoverStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.DefaultItemHoverStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.DefaultItemHoverStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.DefaultItemHoverStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Syle
						controlGrupo.ButtonStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-blu.gif";
						controlGrupo.ButtonStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonStyle.BorderColor = Color.Black;
						controlGrupo.ButtonStyle.Font.Bold = true;
						controlGrupo.ButtonStyle.Font.Name = "Verdana";
						controlGrupo.ButtonStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Hover Syle
						controlGrupo.ButtonHoverStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						controlGrupo.ButtonHoverStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonHoverStyle.BorderColor = Color.Black;
						controlGrupo.ButtonHoverStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonHoverStyle.Font.Bold = true;
						controlGrupo.ButtonHoverStyle.Font.Name = "Verdana";
						controlGrupo.ButtonHoverStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonHoverStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonHoverStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonHoverStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonHoverStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonHoverStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonHoverStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Selected Syle
						controlGrupo.ButtonSelectedStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel2-bkgd.gif";
						controlGrupo.ButtonSelectedStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonSelectedStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonSelectedStyle.BorderColor = Color.Black;
						controlGrupo.ButtonSelectedStyle.Font.Bold = true;
						controlGrupo.ButtonSelectedStyle.Font.Name = "Verdana";
						controlGrupo.ButtonSelectedStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonSelectedStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonSelectedStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonSelectedStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonSelectedStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonSelectedStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonSelectedStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						controlGrupo.ItemIconStyle = ItemIconStyle.Small;
						controlGrupo.ImageTextAlign = ImageTextAlign.AbsMiddle;
						controlGrupo.TextAlign = "Left";
						controlGrupo.ItemSelectionStyle = ItemSelectionStyle.FullRowSelection;

						break;
					case	StyleControl.Ofice2003Gray:

						#region Item Syle
						controlGrupo.DefaultItemStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-gris.gif";
						controlGrupo.DefaultItemStyle.BorderStyle = BorderStyle.None;
						controlGrupo.DefaultItemStyle.BorderWidth = Unit.Pixel(0);
						controlGrupo.DefaultItemStyle.Font.Bold = true;
						controlGrupo.DefaultItemStyle.Font.Name = "Verdana";
						controlGrupo.DefaultItemStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.DefaultItemStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.DefaultItemStyle.Height = Unit.Pixel(25);
						controlGrupo.DefaultItemStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.DefaultItemStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.DefaultItemStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.DefaultItemStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Item Hover Style
						controlGrupo.DefaultItemHoverStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						controlGrupo.DefaultItemHoverStyle.BorderStyle = BorderStyle.None;
						controlGrupo.DefaultItemHoverStyle.BorderWidth = Unit.Pixel(0);
						controlGrupo.DefaultItemHoverStyle.Font.Bold = true;
						controlGrupo.DefaultItemHoverStyle.Font.Name = "Verdana";
						controlGrupo.DefaultItemHoverStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.DefaultItemHoverStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.DefaultItemHoverStyle.Height = Unit.Pixel(25);
						controlGrupo.DefaultItemHoverStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.DefaultItemHoverStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.DefaultItemHoverStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.DefaultItemHoverStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Syle
						controlGrupo.ButtonStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-gris.gif";
						controlGrupo.ButtonStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonStyle.BorderColor = Color.Black;
						controlGrupo.ButtonStyle.Font.Bold = true;
						controlGrupo.ButtonStyle.Font.Name = "Verdana";
						controlGrupo.ButtonStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Hover Syle
						controlGrupo.ButtonHoverStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						controlGrupo.ButtonHoverStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonHoverStyle.BorderColor = Color.Black;
						controlGrupo.ButtonHoverStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonHoverStyle.Font.Bold = true;
						controlGrupo.ButtonHoverStyle.Font.Name = "Verdana";
						controlGrupo.ButtonHoverStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonHoverStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonHoverStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonHoverStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonHoverStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonHoverStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonHoverStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						#region Button Selected Syle
						controlGrupo.ButtonSelectedStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel2-bkgd.gif";
						controlGrupo.ButtonSelectedStyle.BorderStyle = BorderStyle.Outset;
						controlGrupo.ButtonSelectedStyle.BorderWidth = Unit.Pixel(1);
						controlGrupo.ButtonSelectedStyle.BorderColor = Color.Black;
						controlGrupo.ButtonSelectedStyle.Font.Bold = true;
						controlGrupo.ButtonSelectedStyle.Font.Name = "Verdana";
						controlGrupo.ButtonSelectedStyle.Font.Names = new string[]{"Verdana"};
						controlGrupo.ButtonSelectedStyle.Font.Size = new FontUnit("8pt");

						controlGrupo.ButtonSelectedStyle.Height = Unit.Pixel(25);
						controlGrupo.ButtonSelectedStyle.Padding.Top = Unit.Pixel(1);
						controlGrupo.ButtonSelectedStyle.Padding.Left = Unit.Pixel(4);
						controlGrupo.ButtonSelectedStyle.Padding.Right = Unit.Pixel(4);
						controlGrupo.ButtonSelectedStyle.Padding.Bottom = Unit.Pixel(1);
						#endregion

						controlGrupo.ItemIconStyle = ItemIconStyle.Small;
						controlGrupo.ImageTextAlign = ImageTextAlign.AbsMiddle;
						controlGrupo.TextAlign = "Left";
						controlGrupo.ItemSelectionStyle = ItemSelectionStyle.FullRowSelection;

						break;
				}

			}

			public static void AsignarEstilo(UltraWebToolbar ultraWebToolBar, StyleControl styleControl)
			{
				ultraWebToolBar.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-blu.gif";
				ultraWebToolBar.HoverStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
				ultraWebToolBar.ItemWidthDefault = Unit.Pixel(25);
			}

			public static void AsignarEstilo(UltraWebMenu ultraWebMenu, StyleControl styleControl)
			{
				switch( styleControl )
				{
					case	StyleControl.Ofice2003Blue:
						ultraWebMenu.BackImageUrl = "../Utilitarios/Imagenes/bkgd-v-blu.gif";
						ultraWebMenu.ItemStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-blu.gif";
						ultraWebMenu.ItemStyle.Height = Unit.Pixel(25);
						ultraWebMenu.HoverItemStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						ultraWebMenu.HoverItemStyle.Height = Unit.Pixel(25);
						break;
					case	StyleControl.Ofice2003Gray:
						ultraWebMenu.ItemStyle.BackgroundImage = "../Utilitarios/Imagenes/bkgd-v-gris.gif";
						ultraWebMenu.ItemStyle.Height = Unit.Pixel(25);
						ultraWebMenu.HoverItemStyle.BackgroundImage = "../Utilitarios/Imagenes/nb-sel-bkgd.gif";
						ultraWebMenu.HoverItemStyle.Height = Unit.Pixel(25);
						break;
				}
			}

			public static void AsignarEstilo(UltraWebListbar ultraWebListBar, StyleControl styleControl)
			{
				Group grupoLista = new Group();
				UtilesInfragistics.AsignarEstiloControlGrupo(grupoLista,styleControl);
				ultraWebListBar.Groups.Add(grupoLista);
			}
		}
	}
