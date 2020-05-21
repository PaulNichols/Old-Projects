using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Samples.Windows.Forms.Navigation;
using HBOS.FS.AMP.UPD.WinUI.UserControls.Managers;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// GUI Manager for fund groups
	/// </summary>
	public class FundGroupManager : Explorer
	{
		private System.ComponentModel.IContainer components = null;

		#region Constructors
		
		/// <summary>
		/// Creates a new <see cref="FundGroupManager"/> instance.
		/// </summary>
		public FundGroupManager() : base(new FundGroupHomePage())
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			//Show explorer style dialog
			this.ShowDialog();			
		}
    	
		#endregion Constructors

		#region Dispose

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

		#endregion Dispose

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// FundGroupManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 433);
			this.Name = "FundGroupManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;

		}
		#endregion
	}
}

