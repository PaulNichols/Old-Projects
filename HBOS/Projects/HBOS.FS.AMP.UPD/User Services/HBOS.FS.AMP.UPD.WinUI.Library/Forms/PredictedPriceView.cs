using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Reports;


namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Summary description for PredictedPriceView.
	/// </summary>
	public class PredictedPriceView : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		//private NewFundDriftReport report;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor for PreditcedPriceView
		/// </summary>
		public PredictedPriceView( PredictedPriceCrystalReport CRpt )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			crystalReportViewer1.ReportSource = CRpt;
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
			this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// crystalReportViewer1
			// 
			this.crystalReportViewer1.ActiveViewIndex = -1;
			this.crystalReportViewer1.DisplayGroupTree = false;
			this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
			this.crystalReportViewer1.Name = "crystalReportViewer1";
			this.crystalReportViewer1.ReportSource = null;
			this.crystalReportViewer1.Size = new System.Drawing.Size(920, 542);
			this.crystalReportViewer1.TabIndex = 0;
			// 
			// PredictedPriceView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(920, 542);
			this.Controls.Add(this.crystalReportViewer1);
			this.Name = "PredictedPriceView";
			this.Text = "PredictedPriceView";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
