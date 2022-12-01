/*=====================================================================
  File:     FormatListboxTypeEditor.cs

  Summary:  This class is an implementation of the ListboxTypeEditor abstract class.

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
	public class FormatListboxTypeEditor : ListboxTypeEditor
	{
		/// <summary>
		/// Fill a ListBox with format types.
		/// </summary>
		/// <param name="pContext"></param>
		/// <param name="pProvider"></param>
		/// <param name="pListBox"></param>
		protected override void FillInList(ITypeDescriptorContext context, IServiceProvider provider, ListBox listBox)
		{
			listBox.Items.Add("HTML3.2");
			listBox.Items.Add("PDF");
			listBox.Items.Add("HTMLOWC");
			listBox.Items.Add("CSV");
			listBox.Items.Add("HTML4.0");
			listBox.Items.Add("MHTML");
			listBox.Items.Add("IMAGE");
			listBox.Items.Add("XML");
			listBox.BorderStyle = BorderStyle.None;

		}
	}
}
