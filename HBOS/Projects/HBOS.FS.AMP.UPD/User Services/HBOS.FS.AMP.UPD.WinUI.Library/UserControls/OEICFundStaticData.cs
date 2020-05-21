using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for OEICFundStaticData.
	/// </summary>
	public class OEICFundStaticData : System.Windows.Forms.UserControl
	{
        private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new <see cref="OEICFundStaticData"/> instance.
		/// </summary>
		public OEICFundStaticData()
		{
			// This call is required by the Windows.Forms Form Designer.
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "I\'m an OEIC, what about it?";
            // 
            // OEICFundStaticData
            // 
            this.Controls.Add(this.label1);
            this.Name = "OEICFundStaticData";
            this.Size = new System.Drawing.Size(272, 150);
            this.ResumeLayout(false);

        }
		#endregion
	}
}
