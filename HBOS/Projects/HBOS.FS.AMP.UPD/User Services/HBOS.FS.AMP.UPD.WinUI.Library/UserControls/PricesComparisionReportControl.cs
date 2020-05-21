using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Schemas;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for HistoricPredictedPrices.
	/// </summary>
	public class PriceComparisionControl : UserControl
	{
	//	private string m_connectionString;
		private DateTimePicker valuationdateTimePicker;
		private Label startDateLabel;
		private CrystalReportViewer reportcrystalReportViewer;
		private Label fundGroupLabel;
		private TextBox PriceFileLocation;
		private Button RefreshButton;
		private Button PriceFileButton;
		long snapShotId =0;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="HistoricPredictedPricesReportControl"/> instance.
		/// </summary>
		public PriceComparisionControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			InitialiseForm();

			// Shuffle according to font size

//            float scaleWidth = Form.GetAutoScaleSize(this.Font).Width;
//            float adjustment = scaleWidth / 5.32F;
//
//            fundGroupRadioButton.Width = (int) ((float)fundGroupRadioButton.Width * adjustment);
//            fundRadioButton.Left = fundGroupRadioButton.Left + fundGroupRadioButton.Width + (int)(12F * adjustment);
//
//            fundsComboBox.Left = (int)((float)fundsComboBox.Left * adjustment);            
//            
//            startdateTimePicker.Left = endDateTimePicker.Left = startDateLabel.Left + startDateLabel.Width + (int)(12F * adjustment);
//            
//            refreshButton.Left = endDateTimePicker.Left + endDateTimePicker.Width - refreshButton.Width;

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.reportcrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.valuationdateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.startDateLabel = new System.Windows.Forms.Label();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.fundGroupLabel = new System.Windows.Forms.Label();
			this.PriceFileLocation = new System.Windows.Forms.TextBox();
			this.PriceFileButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// reportcrystalReportViewer
			// 
			this.reportcrystalReportViewer.ActiveViewIndex = -1;
			this.reportcrystalReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.reportcrystalReportViewer.DisplayGroupTree = false;
			this.reportcrystalReportViewer.Location = new System.Drawing.Point(0, 88);
			this.reportcrystalReportViewer.Name = "reportcrystalReportViewer";
			this.reportcrystalReportViewer.ReportSource = null;
			this.reportcrystalReportViewer.ShowCloseButton = false;
			this.reportcrystalReportViewer.ShowGroupTreeButton = false;
			this.reportcrystalReportViewer.ShowRefreshButton = false;
			this.reportcrystalReportViewer.Size = new System.Drawing.Size(672, 344);
			this.reportcrystalReportViewer.TabIndex = 7;
			// 
			// valuationdateTimePicker
			// 
			this.valuationdateTimePicker.CustomFormat = "dd\'/\'MM\'/\'yyyy";
			this.valuationdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.valuationdateTimePicker.Location = new System.Drawing.Point(197, 48);
			this.valuationdateTimePicker.Name = "valuationdateTimePicker";
			this.valuationdateTimePicker.Size = new System.Drawing.Size(120, 22);
			this.valuationdateTimePicker.TabIndex = 3;
			// 
			// startDateLabel
			// 
			this.startDateLabel.AutoSize = true;
			this.startDateLabel.Location = new System.Drawing.Point(16, 48);
			this.startDateLabel.Name = "startDateLabel";
			this.startDateLabel.Size = new System.Drawing.Size(93, 18);
			this.startDateLabel.TabIndex = 3;
			this.startDateLabel.Text = "&Valuation Date";
			// 
			// RefreshButton
			// 
			this.RefreshButton.Location = new System.Drawing.Point(429, 48);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(80, 23);
			this.RefreshButton.TabIndex = 6;
			this.RefreshButton.Text = "&Refresh";
			this.RefreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// fundGroupLabel
			// 
			this.fundGroupLabel.AutoSize = true;
			this.fundGroupLabel.Location = new System.Drawing.Point(16, 16);
			this.fundGroupLabel.Name = "fundGroupLabel";
			this.fundGroupLabel.Size = new System.Drawing.Size(162, 18);
			this.fundGroupLabel.TabIndex = 8;
			this.fundGroupLabel.Text = "Price File for Comparision:";
			// 
			// PriceFileLocation
			// 
			this.PriceFileLocation.Location = new System.Drawing.Point(197, 16);
			this.PriceFileLocation.Name = "PriceFileLocation";
			this.PriceFileLocation.ReadOnly = true;
			this.PriceFileLocation.Size = new System.Drawing.Size(288, 22);
			this.PriceFileLocation.TabIndex = 9;
			this.PriceFileLocation.Text = "";
			// 
			// PriceFileButton
			// 
			this.PriceFileButton.Location = new System.Drawing.Point(485, 16);
			this.PriceFileButton.Name = "PriceFileButton";
			this.PriceFileButton.Size = new System.Drawing.Size(24, 23);
			this.PriceFileButton.TabIndex = 10;
			this.PriceFileButton.Text = "...";
			this.PriceFileButton.Click += new System.EventHandler(this.PriceFileButton_Click);
			// 
			// PriceComparisionControl
			// 
			this.Controls.Add(this.PriceFileButton);
			this.Controls.Add(this.PriceFileLocation);
			this.Controls.Add(this.fundGroupLabel);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.startDateLabel);
			this.Controls.Add(this.valuationdateTimePicker);
			this.Controls.Add(this.reportcrystalReportViewer);
			this.Name = "PriceComparisionControl";
			this.Size = new System.Drawing.Size(672, 432);
			this.ResumeLayout(false);

		}

		#endregion

		private void InitialiseForm()
		{
			T.E();

			try
			{
				// Get the currently selected company from the GUI principal thread
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				
				this.valuationdateTimePicker.MaxDate = updPrincipal.PreviousCompanyValuationDateAndTime;

			}
			finally
			{
				T.X();
			}
		}


		private void refreshButton_Click(object sender, EventArgs e)
		{
			T.E();

			Cursor oldCursor = this.Cursor;

			try
			{
				if (this.PriceFileLocation.Text != "")
				{
					this.Cursor = Cursors.WaitCursor;

					DateTime valuationDate = this.valuationdateTimePicker.Value.Date;
					string connectionString = GlobalRegistry.ConnectionString;
					PriceComparisionSchema reportData=new PriceComparisionSchema();

					try
					{
						reportData=(PriceComparisionSchema) ReportController.LoadPriceComparisionData(connectionString,valuationDate,snapShotId,reportData);
							
						PriceComparisionReport newReport = new PriceComparisionReport();
				
						// Must use Datatable and not dataset otherwise errors occur!
						newReport.SetDataSource(reportData.Tables["ComparisionDetails"]);
				
						this.reportcrystalReportViewer.ReportSource = newReport;
						this.reportcrystalReportViewer.Show();
					}
					catch
					{
						throw;
					}
				}
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		private void PriceFileButton_Click(object sender, EventArgs e)
		{
			this.PriceFileLocation.Text = "";
			string importFilter =  (string)GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["PriceComparisionReportFilter"];

			string importFile = ImportManager.ChooseImportFile(importFilter, this);

			if (importFile != null && importFile.Length != 0)
			{
				snapShotId = ImportManager.ImportFile(ImportController.ImportFileType.PriceComparisionPrices, importFile, false, this);

			}

			if (snapShotId>0) {this.PriceFileLocation.Text = importFile;}
		}


	}
}