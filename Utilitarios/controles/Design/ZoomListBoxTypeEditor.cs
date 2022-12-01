/*=====================================================================
  File:     ZoomListboxTypeEditor.cs

  Summary:  This class is an implementation of the ListboxTypeEditor abstract class
						for the zoom listbox.

=====================================================================*/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using System.Collections;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design
{
	public class ZoomListboxTypeEditor : ListboxTypeEditor
	{
		/// <summary>
		/// Fill a ListBox with zoom values.
		/// </summary>
		/// <param name="pContext"></param>
		/// <param name="pProvider"></param>
		/// <param name="pListBox"></param>
		protected override void FillInList(ITypeDescriptorContext pContext, IServiceProvider pProvider, ListBox pListBox)
		{
			pListBox.Items.Add("10%");
			pListBox.Items.Add("25%");
			pListBox.Items.Add("50%");
			pListBox.Items.Add("75%");
			pListBox.Items.Add("100%");
			pListBox.Items.Add("150%");
			pListBox.Items.Add("200%");
			pListBox.Items.Add("500%");
			pListBox.BorderStyle = BorderStyle.None;
		}
	}
}
