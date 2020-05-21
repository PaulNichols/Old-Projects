using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.Reports.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Schemas;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for FundDriftReportControl.
	/// </summary>
	public class FundDriftReportControl : UserControl
	{
		#region Controls

		private SimpleLookupCollection m_fundGroups;
		private string m_connectionString;
		private ComboBox fundsComboBox;
		private CrystalReportViewer reportcrystalReportViewer;
		private Button refreshButton;
		private Label fundGroupLabel;
		private Panel panelNotEnoughData;
		private Label lblNoIndexPricingData;
		private PictureBox NotEnoughDataPicture;
		private Label lblNoCurrencyData;
		private Label lblNoIndexOrCurrencyData;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="FundDriftReportControl"/> instance.
		/// </summary>
		public FundDriftReportControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			InitialiseForm();
		}

		#endregion

		#region Private Methods

		private void InitialiseForm()
		{
			try
			{
				// Get the currently selected company from the GUI principal thread
				//	UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;

				// This method will initialise the form
				this.m_connectionString = GlobalRegistry.ConnectionString;

				FundGroupController fundGroupList = new FundGroupController(m_connectionString);

				m_fundGroups = fundGroupList.LoadFundGroupsByCompanyAndTypeLookUp(GlobalRegistry.CompanyCode, FundGroupFactory.FundGroupTypes.Asset);

				this.fundsComboBox.DataSource = m_fundGroups;
				this.fundsComboBox.DisplayMember = "DisplayValue";
				this.fundsComboBox.ValueMember = "Key";

				reportcrystalReportViewer.ShowRefreshButton = false;
				reportcrystalReportViewer.ShowCloseButton = false;
			}
			catch (Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}

		}

		#endregion

		#region Control Events

		private void refreshButton_Click(object sender, EventArgs e)
		{
			try
			{
				//DataSet reportData = new DataSet();

				if (this.fundsComboBox.SelectedIndex > -1)
				{
					int fundGroupID = (int) this.fundsComboBox.SelectedValue;

					FundDriftDataset reportData = getPopulatedFundDriftDataset(fundGroupID, this.fundsComboBox.Text);

					NewFundDriftReport newFundDriftReport = new NewFundDriftReport();

					// Cannot use dataset as it causes an error in Crystal - must use tables
					newFundDriftReport.SetDataSource(reportData.AssetFund);
					//newFundDriftReport.SetParameterValue("FundGroup", this.fundsComboBox.SelectedItem.ToString());

					this.reportcrystalReportViewer.ReportSource = newFundDriftReport;
					this.reportcrystalReportViewer.Show();
				}
			}
			catch (Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayLoadException("Fund Drift Price Report", ex);
			}
		}

		private static FundDriftDataset getPopulatedFundDriftDataset(int fundGroupID, string fundGroupName)
		{
			IList decoratedAssetFunds = loadDecoratedAssetFundCollection(fundGroupID);

			FundDriftDataset reportData = populateFundDriftDataset(decoratedAssetFunds, fundGroupName);

			return reportData;
		}

		private static FundDriftDataset populateFundDriftDataset(IList decoratedAssetFunds, string fundGroupName)
		{
			WinUI.Reports.Schemas.FundDriftDataset reportData = new FundDriftDataset();

			foreach (AssetFundDecorator decoratedAssetFund in decoratedAssetFunds)
			{
				IList constituents = AssetMovementConstituentPropertiesDecorator.FromConstituentListToDecorated(decoratedAssetFund.AssetFund.AssetMovementConstituents);
				foreach (AssetMovementConstituentPropertiesDecorator constituent in constituents)
				{
					reportData.AssetFund.AddAssetFundRow
						(
							decoratedAssetFund.AssetFund.AssetFundCode,
							decoratedAssetFund.FullName,
							decoratedAssetFund.AssetFund.AssetMovementConstituents.TotalMovement().ToString("p4"),
							decoratedAssetFund.AssetFund.AssetMovementConstituents.TotalProportion().ToString("p4"),
							constituent.Effect,
							constituent.CurrencyMovement,
							constituent.CurrentBenchmarkExchangeRate,
							constituent.PreviousBenchmarkExchangeRate,
							constituent.BenchmarkMovement,
							decoratedAssetFund.AssetFund.Currency.CurrencyCode,
							constituent.CurrentAssetExchangeRate,
							constituent.PreviousAssetFundExchangeRate,
							constituent.CurrentBenchmarkValue,
							constituent.PreviousBenchmarkValue,
							constituent.ProportionDisplay,
							constituent.BenchmarkDisplayName,
							String.Concat("(", fundGroupName, ")"),
							GlobalRegistry.CurrentCompanyValuationDateAndTime.ToShortDateString()
						);
				}


			}
			return reportData;
		}

		private static IList loadDecoratedAssetFundCollection(int fundGroupID)
		{
			AssetFundCollection assetFunds = AssetFundController.LoadAssetFunds(GlobalRegistry.ConnectionString, fundGroupID, true);

			return AssetFundDecorator.ToDecoratedList(assetFunds);
		}

		#endregion

		#region Dispose

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

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			try
			{
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (FundDriftReportControl));
				this.fundsComboBox = new System.Windows.Forms.ComboBox();
				this.fundGroupLabel = new System.Windows.Forms.Label();
				this.reportcrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
				this.refreshButton = new System.Windows.Forms.Button();
				this.panelNotEnoughData = new System.Windows.Forms.Panel();
				this.lblNoIndexPricingData = new System.Windows.Forms.Label();
				this.NotEnoughDataPicture = new System.Windows.Forms.PictureBox();
				this.lblNoCurrencyData = new System.Windows.Forms.Label();
				this.lblNoIndexOrCurrencyData = new System.Windows.Forms.Label();
				this.panelNotEnoughData.SuspendLayout();
				this.SuspendLayout();
				// 
				// fundsComboBox
				// 
				this.fundsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.fundsComboBox.Location = new System.Drawing.Point(120, 8);
				this.fundsComboBox.Name = "fundsComboBox";
				this.fundsComboBox.Size = new System.Drawing.Size(336, 24);
				this.fundsComboBox.TabIndex = 12;
				// 
				// fundGroupLabel
				// 
				this.fundGroupLabel.Location = new System.Drawing.Point(8, 8);
				this.fundGroupLabel.Name = "fundGroupLabel";
				this.fundGroupLabel.TabIndex = 11;
				this.fundGroupLabel.Text = "Fund Group";
				// 
				// reportcrystalReportViewer
				// 
				this.reportcrystalReportViewer.ActiveViewIndex = -1;
				this.reportcrystalReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
					| System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
				this.reportcrystalReportViewer.DisplayGroupTree = false;
				this.reportcrystalReportViewer.Location = new System.Drawing.Point(0, 72);
				this.reportcrystalReportViewer.Name = "reportcrystalReportViewer";
				this.reportcrystalReportViewer.ReportSource = null;
				this.reportcrystalReportViewer.Size = new System.Drawing.Size(584, 296);
				this.reportcrystalReportViewer.TabIndex = 10;
				// 
				// refreshButton
				// 
				this.refreshButton.Location = new System.Drawing.Point(471, 8);
				this.refreshButton.Name = "refreshButton";
				this.refreshButton.TabIndex = 14;
				this.refreshButton.Text = "Refresh";
				this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
				// 
				// panelNotEnoughData
				// 
				this.panelNotEnoughData.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
					| System.Windows.Forms.AnchorStyles.Right)));
				this.panelNotEnoughData.Controls.Add(this.lblNoIndexOrCurrencyData);
				this.panelNotEnoughData.Controls.Add(this.lblNoCurrencyData);
				this.panelNotEnoughData.Controls.Add(this.lblNoIndexPricingData);
				this.panelNotEnoughData.Controls.Add(this.NotEnoughDataPicture);
				this.panelNotEnoughData.Location = new System.Drawing.Point(8, 32);
				this.panelNotEnoughData.Name = "panelNotEnoughData";
				this.panelNotEnoughData.Size = new System.Drawing.Size(568, 32);
				this.panelNotEnoughData.TabIndex = 15;
				this.panelNotEnoughData.Visible = false;
				// 
				// lblNoIndexPricingData
				// 
				this.lblNoIndexPricingData.AutoSize = true;
				this.lblNoIndexPricingData.Location = new System.Drawing.Point(32, 8);
				this.lblNoIndexPricingData.Name = "lblNoIndexPricingData";
				this.lblNoIndexPricingData.Size = new System.Drawing.Size(396, 16);
				this.lblNoIndexPricingData.TabIndex = 1;
				this.lblNoIndexPricingData.Text = "No index prices have been imported today. This report may not be meaningful.";
				// 
				// NotEnoughDataPicture
				// 
				this.NotEnoughDataPicture.Image = ((System.Drawing.Image) (resources.GetObject("NotEnoughDataPicture.Image")));
				this.NotEnoughDataPicture.Location = new System.Drawing.Point(8, 8);
				this.NotEnoughDataPicture.Name = "NotEnoughDataPicture";
				this.NotEnoughDataPicture.Size = new System.Drawing.Size(24, 24);
				this.NotEnoughDataPicture.TabIndex = 0;
				this.NotEnoughDataPicture.TabStop = false;
				// 
				// lblNoCurrencyData
				// 
				this.lblNoCurrencyData.AutoSize = true;
				this.lblNoCurrencyData.Location = new System.Drawing.Point(32, 8);
				this.lblNoCurrencyData.Name = "lblNoCurrencyData";
				this.lblNoCurrencyData.Size = new System.Drawing.Size(408, 16);
				this.lblNoCurrencyData.TabIndex = 2;
				this.lblNoCurrencyData.Text = "No currency rates have been imported today. This report may not be meaningful.";
				// 
				// lblNoIndexOrCurrencyData
				// 
				this.lblNoIndexOrCurrencyData.AutoSize = true;
				this.lblNoIndexOrCurrencyData.Location = new System.Drawing.Point(32, 8);
				this.lblNoIndexOrCurrencyData.Name = "lblNoIndexOrCurrencyData";
				this.lblNoIndexOrCurrencyData.Size = new System.Drawing.Size(484, 16);
				this.lblNoIndexOrCurrencyData.TabIndex = 3;
				this.lblNoIndexOrCurrencyData.Text = "No index prices or currency rates have been imported today. This report may not b" +
					"e meaningful.";
				// 
				// FundDriftReportControl
				// 
				this.Controls.Add(this.panelNotEnoughData);
				this.Controls.Add(this.refreshButton);
				this.Controls.Add(this.fundsComboBox);
				this.Controls.Add(this.fundGroupLabel);
				this.Controls.Add(this.reportcrystalReportViewer);
				this.Name = "FundDriftReportControl";
				this.Size = new System.Drawing.Size(584, 368);
				this.panelNotEnoughData.ResumeLayout(false);
				this.ResumeLayout(false);
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}

		}

		#endregion
	}
}