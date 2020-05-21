using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using HBOS.FS.AMP.Windows.Controls;

namespace EnumComboTestHarness
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private HBOS.FS.AMP.Windows.Controls.EnumComboBox enumComboBox1;
		private System.Windows.Forms.Button button1;
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

			enumComboBox1.Enum = typeof(MyColours);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.enumComboBox1 = new HBOS.FS.AMP.Windows.Controls.EnumComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// enumComboBox1
			// 
			this.enumComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.enumComboBox1.Location = new System.Drawing.Point(56, 48);
			this.enumComboBox1.Name = "enumComboBox1";
			this.enumComboBox1.Size = new System.Drawing.Size(136, 21);
			this.enumComboBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(88, 144);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.enumComboBox1);
			this.Name = "Form1";
			this.Text = "Test Harness";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show( enumComboBox1.SelectedItem.ToString() );
		}
	}

	public enum MyColours : int
	{
		[EnumDisplayText("Blaze Red")]
		Red,
		[EnumDisplayText("Shocking Blue")]
		Blue,
		[EnumDisplayText("Grass Green")]
		Green,
		[EnumDisplayText("Storm Purple")]
		Purple
	}
}
