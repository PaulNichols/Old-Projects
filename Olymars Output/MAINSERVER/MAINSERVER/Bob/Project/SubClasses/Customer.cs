using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bob.Windows.Forms.SubClasses 
{
	public class Customer : Bob.Windows.Forms.WinForm_Customers
	{
		private System.ComponentModel.IContainer components = null;

		public Customer()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Customer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.ClientSize = new System.Drawing.Size(482, 663);
			this.ControlBox = true;
			this.MinimizeBox = true;
			this.Name = "Customer";
			this.ShowInTaskbar = false;
			this.Text = "Customers Management";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(64)));
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);

		}
		#endregion

		public void OnClosing(object sender,System.ComponentModel.CancelEventArgs args)
		{
			if (! this.ErrorHasOccured)
			{
		//		((Bob.Windows.Forms.MDI) this.MdiParent).ShowNotification("Saved Successfully!");
			}
		}
	}
}

