/*=====================================================================
  File:     StringEditor.cs

  Summary:  The StringEditor class is a UITypeEditor for the ReportPath parameter.

=====================================================================*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design {

    public class StringEditor : UITypeEditor {

			/// <summary>
			/// Show StringEditorForm when user clicks the property editor button.
			/// </summary>
			/// <param name="context"></param>
			/// <param name="serviceProvider"></param>
			/// <param name="value"></param>
			/// <returns></returns>
      public override object EditValue(ITypeDescriptorContext context,
          IServiceProvider serviceProvider, object value) {

          if ((context != null) &&
              (serviceProvider != null)) {
							//Set the editor service
              IWindowsFormsEditorService edSvc =
                  (IWindowsFormsEditorService)serviceProvider.GetService(
                  typeof(IWindowsFormsEditorService));

              if (edSvc != null) {
									//Create new StringEditorForm
                  StringEditorForm form = new StringEditorForm();
                  form.Value = (string)value;
									//Show form as a dialog
                  DialogResult result = edSvc.ShowDialog(form);
                  if (result == DialogResult.OK) {
                      value = form.Value;
                  }
              }
          }
          return value;
      }
			/// <summary>
			/// The editor will be a modal window.
			/// </summary>
			/// <param name="context"></param>
			/// <returns></returns>
      public override UITypeEditorEditStyle GetEditStyle(
          ITypeDescriptorContext context) {
          if (context != null) {
              return UITypeEditorEditStyle.Modal;
          }
          return base.GetEditStyle(context);
      }
    }
}
