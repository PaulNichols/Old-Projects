using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Reports;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for HistoricPredictedPrices.
	/// </summary>
	public class HistoricPredictedPricesReportControl : UserControl
	{
		private FundGroupCollection m_fundGroups;
		private SimpleStringLookupCollection m_funds;
		private string m_connectionString;
		private DateTimePicker startdateTimePicker;
		private DateTimePicker endDateTimePicker;
		private Label startDateLabel;
		private Label fundGroupLabel;
		private Label endDateLabel;
		private ComboBox fundsComboBox;
		private Button refreshButton;
		private RadioButton fundGroupRadioButton;
		private RadioButton fundRadioButton;
		private CrystalReportViewer reportcrystalReportViewer;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="HistoricPredictedPricesReportControl"/> instance.
		/// </summary>
		public HistoricPredictedPricesReportControl()
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
			this.startdateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.startDateLabel = new System.Windows.Forms.Label();
			this.fundGroupLabel = new System.Windows.Forms.Label();
			this.endDateLabel = new System.Windows.Forms.Label();
			this.fundsComboBox = new System.Windows.Forms.ComboBox();
			this.fundGroupRadioButton = new System.Windows.Forms.RadioButton();
			this.fundRadioButton = new System.Windows.Forms.RadioButton();
			this.refreshButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// reportcrystalReportViewer
			// 
			this.reportcrystalReportViewer.ActiveViewIndex = -1;
			this.reportcrystalReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				| System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.reportcrystalReportViewer.DisplayGroupTree = false;
			this.reportcrystalReportViewer.Location = new System.Drawing.Point(0, 136);
			this.reportcrystalReportViewer.Name = "reportcrystalReportViewer";
			this.reportcrystalReportViewer.ReportSource = null;
			this.reportcrystalReportViewer.ShowCloseButton = false;
			this.reportcrystalReportViewer.ShowGroupTreeButton = false;
			this.reportcrystalReportViewer.ShowRefreshButton = false;
			this.reportcrystalReportViewer.Size = new System.Drawing.Size(672, 296);
			this.reportcrystalReportViewer.TabIndex = 7;
			// 
			// startdateTimePicker
			// 
			this.startdateTimePicker.CustomFormat = "dd\'/\'MM\'/\'yyyy";
			this.startdateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.startdateTimePicker.Location = new System.Drawing.Point(344, 16);
			this.startdateTimePicker.Name = "startdateTimePicker";
			this.startdateTimePicker.Size = new System.Drawing.Size(120, 20);
			this.startdateTimePicker.TabIndex = 3;
			this.startdateTimePicker.ValueChanged += new System.EventHandler(this.startdateTimePicker_ValueChanged);
			// 
			// endDateTimePicker
			// 
			this.endDateTimePicker.CustomFormat = "dd\'/\'MM\'/\'yyyy";
			this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.endDateTimePicker.Location = new System.Drawing.Point(344, 48);
			this.endDateTimePicker.Name = "endDateTimePicker";
			this.endDateTimePicker.Size = new System.Drawing.Size(120, 20);
			this.endDateTimePicker.TabIndex = 4;
			this.endDateTimePicker.ValueChanged += new System.EventHandler(this.endDateTimePicker_ValueChanged);
			// 
			// startDateLabel
			// 
			this.startDateLabel.AutoSize = true;
			this.startDateLabel.Location = new System.Drawing.Point(272, 18);
			this.startDateLabel.Name = "startDateLabel";
			this.startDateLabel.Size = new System.Drawing.Size(55, 16);
			this.startDateLabel.TabIndex = 3;
			this.startDateLabel.Text = "&Start Date";
			// 
			// fundGroupLabel
			// 
			this.fundGroupLabel.AutoSize = true;
			this.fundGroupLabel.Location = new System.Drawing.Point(16, 82);
			this.fundGroupLabel.Name = "fundGroupLabel";
			this.fundGroupLabel.Size = new System.Drawing.Size(93, 16);
			this.fundGroupLabel.TabIndex = 4;
			this.fundGroupLabel.Text = "Fund Group/F&und";
			// 
			// endDateLabel
			// 
			this.endDateLabel.AutoSize = true;
			this.endDateLabel.Location = new System.Drawing.Point(272, 50);
			this.endDateLabel.Name = "endDateLabel";
			this.endDateLabel.Size = new System.Drawing.Size(51, 16);
			this.endDateLabel.TabIndex = 5;
			this.endDateLabel.Text = "&End Date";
			this.endDateLabel.Click += new System.EventHandler(this.endDateLabel_Click);
			// 
			// fundsComboBox
			// 
			this.fundsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fundsComboBox.Location = new System.Drawing.Point(136, 79);
			this.fundsComboBox.Name = "fundsComboBox";
			this.fundsComboBox.Size = new System.Drawing.Size(328, 21);
			this.fundsComboBox.TabIndex = 5;
			// 
			// fundGroupRadioButton
			// 
			this.fundGroupRadioButton.Checked = true;
			this.fundGroupRadioButton.Location = new System.Drawing.Point(16, 15);
			this.fundGroupRadioButton.Name = "fundGroupRadioButton";
			this.fundGroupRadioButton.TabIndex = 1;
			this.fundGroupRadioButton.TabStop = true;
			this.fundGroupRadioButton.Text = "Fund &Group";
			this.fundGroupRadioButton.CheckedChanged += new System.EventHandler(this.fundGroupRadioButton_CheckedChanged);
			// 
			// fundRadioButton
			// 
			this.fundRadioButton.Location = new System.Drawing.Point(16, 47);
			this.fundRadioButton.Name = "fundRadioButton";
			this.fundRadioButton.TabIndex = 2;
			this.fundRadioButton.Text = "&Fund";
			this.fundRadioButton.CheckedChanged += new System.EventHandler(this.fundRadioButton_CheckedChanged);
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(480, 80);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.TabIndex = 6;
			this.refreshButton.Text = "&Refresh";
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// HistoricPredictedPricesReportControl
			// 
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.fundRadioButton);
			this.Controls.Add(this.fundGroupRadioButton);
			this.Controls.Add(this.fundsComboBox);
			this.Controls.Add(this.endDateLabel);
			this.Controls.Add(this.fundGroupLabel);
			this.Controls.Add(this.startDateLabel);
			this.Controls.Add(this.endDateTimePicker);
			this.Controls.Add(this.startdateTimePicker);
			this.Controls.Add(this.reportcrystalReportViewer);
			this.Name = "HistoricPredictedPricesReportControl";
			this.Size = new System.Drawing.Size(672, 432);
			this.Load += new System.EventHandler(this.HistoricPredictedPricesReportControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private void endDateLabel_Click(object sender, EventArgs e)
		{
		}

		private void InitialiseForm()
		{
			// SJR - Added tracing
			T.E();

			try
			{
				// Get the currently selected company from the GUI principal thread
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;

				// This method will initialise the form
				this.m_connectionString = GlobalRegistry.ConnectionString;

				FundGroupController fundGroupList = new FundGroupController(m_connectionString);

				m_fundGroups = fundGroupList.LoadFundGroupsByCompany(updPrincipal.CompanyCode);

				this.fundsComboBox.DataSource = m_fundGroups;
				this.fundsComboBox.DisplayMember = "ShortName";
				this.fundsComboBox.ValueMember = "ID";

				// Get the funds for later
				FundController fundsList = new FundController(m_connectionString);
				m_funds = fundsList.LoadFundLookupsByCompany(updPrincipal.CompanyCode);

				this.startdateTimePicker.MaxDate = updPrincipal.CurrentCompanyValuationDateAndTime;
				this.endDateTimePicker.MaxDate = updPrincipal.CurrentCompanyValuationDateAndTime;
			}
			finally
			{
				T.X();
			}
		}

		private void fundGroupRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			// Change the data source to the fund group list
			this.fundsComboBox.DataSource = m_fundGroups;
			this.fundsComboBox.DisplayMember = "ShortName";
			this.fundsComboBox.ValueMember = "ID";
		}

		private void fundRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			// Change the data source to the funds list
			this.fundsComboBox.DataSource = m_funds;
			this.fundsComboBox.DisplayMember = "DisplayValue";
			this.fundsComboBox.ValueMember = "Key";
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			// SJR - Added tracing and displaying the wait cursor
			T.E();
			Cursor oldCursor = this.Cursor;

			try
			{
				this.Cursor = Cursors.WaitCursor;

				DateTime startDate = this.startdateTimePicker.Value.Date;
				DateTime endDate = this.endDateTimePicker.Value.Date;

				// The report needs to include entries for the enddate, so add 1 to it
				endDate.AddDays(1);

				string extraFundGroupText;

				DataSet reportData = new DataSet();

				//ReportController myReportController = new ReportController();

				if (this.fundGroupRadioButton.Checked)
				{
					reportData = ReportController.LoadPredictedPricesReportByFundGroup(m_connectionString, (int) this.fundsComboBox.SelectedValue, startDate, endDate);
					extraFundGroupText = "group " + this.fundsComboBox.SelectedItem.ToString();
				}
				else
				{
					reportData = ReportController.LoadPredictedPricesReportByFund(m_connectionString, this.fundsComboBox.SelectedValue.ToString(), startDate, endDate);
					extraFundGroupText = " " + this.fundsComboBox.SelectedValue;
				}

				HistoricPredictedPricesReport newHistoricReport = new HistoricPredictedPricesReport();

				// Must use Datatable and not dataset otherwise errors occur!
				newHistoricReport.SetDataSource(reportData.Tables[0]);

				newHistoricReport.SetParameterValue("StartDate", startDate);
				newHistoricReport.SetParameterValue("EndDate", endDate);
				newHistoricReport.SetParameterValue("FundGroup", extraFundGroupText);

				this.reportcrystalReportViewer.ReportSource = newHistoricReport;
				this.reportcrystalReportViewer.Show();
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		private void HistoricPredictedPricesReportControl_Load(object sender, EventArgs e)
		{
		}

		private void startdateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (this.startdateTimePicker.Value > this.endDateTimePicker.Value)
			{
				this.refreshButton.Enabled = false;
			}
			else
			{
				this.refreshButton.Enabled = true;
			}
		}

		private void endDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			if (this.startdateTimePicker.Value > this.endDateTimePicker.Value)
			{
				this.refreshButton.Enabled = false;
			}
			else
			{
				this.refreshButton.Enabled = true;
			}
		}
	}
}