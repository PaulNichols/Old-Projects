/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 27/12/2004 16:08:32
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;
using OlymarsDemo.DataClasses;

namespace OlymarsDemo.Windows.Forms {

	/// <summary>
	/// Summary description for WinForm_ErrorForm.
	/// </summary>
	internal class WinForm_ErrorForm : System.Windows.Forms.Form {

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labProcedure;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labStoredProcedure;
		private System.Windows.Forms.Label labErrorSource;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtException;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary> 
		/// [To be supplied.]
		/// </summary>
		public WinForm_ErrorForm() {

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {

			if(disposing) {

				if (components != null) {

					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

			this.labProcedure = new System.Windows.Forms.Label();
			this.txtException = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labErrorSource = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labStoredProcedure = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labProcedure
			// 
			this.labProcedure.AutoSize = true;
			this.labProcedure.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labProcedure.Location = new System.Drawing.Point(176, 64);
			this.labProcedure.Name = "labProcedure";
			this.labProcedure.Size = new System.Drawing.Size(96, 13);
			this.labProcedure.TabIndex = 1;
			this.labProcedure.Text = "<Procedure>";
			// 
			// txtException
			// 
			this.txtException.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtException.Location = new System.Drawing.Point(16, 136);
			this.txtException.Multiline = true;
			this.txtException.Name = "txtException";
			this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtException.Size = new System.Drawing.Size(816, 336);
			this.txtException.TabIndex = 3;
			this.txtException.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(80, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Procedure:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(56, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(110, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Error source:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labErrorSource
			// 
			this.labErrorSource.AutoSize = true;
			this.labErrorSource.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labErrorSource.Location = new System.Drawing.Point(176, 112);
			this.labErrorSource.Name = "labErrorSource";
			this.labErrorSource.Size = new System.Drawing.Size(121, 13);
			this.labErrorSource.TabIndex = 1;
			this.labErrorSource.Text = "<Error source>";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lucida Console", 14.25F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(275, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "An error has occured !";
			// 
			// labStoredProcedure
			// 
			this.labStoredProcedure.AutoSize = true;
			this.labStoredProcedure.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labStoredProcedure.Location = new System.Drawing.Point(176, 88);
			this.labStoredProcedure.Name = "labStoredProcedure";
			this.labStoredProcedure.Size = new System.Drawing.Size(155, 13);
			this.labStoredProcedure.TabIndex = 1;
			this.labStoredProcedure.Text = "<Stored Procedure>";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(142, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Stored Procedure:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// WinForm_ErrorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.ClientSize = new System.Drawing.Size(850, 485);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.txtException,
																		  this.labErrorSource,
																		  this.label6,
																		  this.label3,
																		  this.labStoredProcedure,
																		  this.label2,
																		  this.labProcedure,
																		  this.label1});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_ErrorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Error";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary> 
		/// [To be supplied.]
		/// </summary>
		/// <param name="procedure">[To be supplied.]</param>
		/// <param name="param">[To be supplied.]</param>
		public void DisplayError(string procedure, IParameter param) {

			try {

				labProcedure.Text = procedure;
				labStoredProcedure.Text = param.StoredProcedureName;

				labErrorSource.Text = param.ErrorSource.ToString();
				
				if (param.SqlException != null) {

					txtException.Text = "SqlException number: " + param.SqlException.Number.ToString() + "\r\n\r\n";
					txtException.Text = txtException.Text + param.SqlException.ToString();
					return;
				}

				if (param.OtherException != null) {

					txtException.Text = param.OtherException.ToString();
					return;
				}
			}

			finally {

				this.ShowDialog();
			}
		}
	}
}
