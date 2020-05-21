using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NumericTextBoxTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private HBOS.FS.AMP.Controls.NumericTextBox numericTextBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.numericTextBox1 = new HBOS.FS.AMP.Controls.NumericTextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(112, 152);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// numericTextBox1
			// 
			this.numericTextBox1.Location = new System.Drawing.Point(112, 56);
			this.numericTextBox1.MaxValue = new System.Decimal(new int[] {
																			 100,
																			 0,
																			 0,
																			 0});
			this.numericTextBox1.MinValue = new System.Decimal(new int[] {
																			 0,
																			 0,
																			 0,
																			 0});
			this.numericTextBox1.Name = "numericTextBox1";
			this.numericTextBox1.TabIndex = 2;
			this.numericTextBox1.Text = "";
			this.numericTextBox1.ValueMultiplier = new System.Decimal(new int[] {
																					1,
																					0,
																					0,
																					0});
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.numericTextBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		public static void Main()
		{
			Application.Run( new Form1() );
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show( numericTextBox1.Text );
			MessageBox.Show( numericTextBox1.Value .ToString());
		}
	}
}
