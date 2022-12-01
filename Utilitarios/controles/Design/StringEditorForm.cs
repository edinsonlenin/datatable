/*=====================================================================
  File:     StringEditorForm.cs

  Summary:  The StringEditorForm class is the Form for the StringEditor UITypeEditor.

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

namespace Seriva.Bolsa.Presentacion.Utilitarios.Controles.Reportes.Design {

	public class StringEditorForm : System.Windows.Forms.Form {

		#region WinForm Private Variable
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox urlPathTextBox;
    private System.Windows.Forms.Button cancelButton;
		#endregion

    public StringEditorForm() {
			InitializeComponent();

		}

		/// <summary>
		/// Property value to get/set within Form Textbox
		/// </summary>
    public string Value {
			get
			{
					return urlPathTextBox.Text;
			}
			set
			{
					urlPathTextBox.Text = value;
			}
		}

		#region Windows Form Designer generated code
        private void InitializeComponent() {
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.urlPathTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(184, 64);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(88, 27);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(280, 64);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(89, 27);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Url Path";
			// 
			// urlPathTextBox
			// 
			this.urlPathTextBox.Location = new System.Drawing.Point(8, 24);
			this.urlPathTextBox.Multiline = true;
			this.urlPathTextBox.Name = "urlPathTextBox";
			this.urlPathTextBox.Size = new System.Drawing.Size(368, 32);
			this.urlPathTextBox.TabIndex = 5;
			this.urlPathTextBox.Text = string.Empty;
			// 
			// StringEditorForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(384, 94);
			this.Controls.Add(this.urlPathTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Font = new System.Drawing.Font("Tahoma", 8F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StringEditorForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "String Editor";
			this.Load += new System.EventHandler(this.StringEditorForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void StringEditorForm_Load(object sender, System.EventArgs e)
		{

		}

    }
}
