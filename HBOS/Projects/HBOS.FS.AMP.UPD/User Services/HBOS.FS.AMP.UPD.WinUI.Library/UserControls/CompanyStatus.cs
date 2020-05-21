using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.Types.Status;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Used by the status bars refresh. Type that implement this interface will be refreshed
	/// </summary>
	public interface IRefreshable
	{
		/// <summary>
		/// Method to refresh data.
		/// </summary>
		void OnRefreshData();
	}

	/// <summary>
	/// Summary description for CompanyStatus.
	/// </summary>
	public class CompanyStatus : UserControl, IRefreshable, ICustomInit
	{
		#region Controls

		private Panel panel1;
		private Panel panel4;
		private GroupBox importsGroupBox;
		private Windows.Controls.DataGrid importDataGrid;
		private Splitter splitter2;
		private Panel panel3;
		private GroupBox groupBox1;
		private DataGrid exportDataGrid;
		private Label companyNameLabel;
		private PictureBox companyLogo;
		private Button ValuationDayButton;
		private IContainer components = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Tells the user the status of imports, exports and fund progress
		/// </summary>
		public CompanyStatus()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			panel4.Height = this.Height / 2;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Load all the data for the Company Status screen.
		/// </summary>
		public void CustomInitialization()
		{
			Assembly myself = Assembly.GetExecutingAssembly();
			//	string[] resources = myself.GetManifestResourceNames();

			//Set company specifics
			switch (GlobalRegistry.CompanyCode.Trim())
			{
				case "HLL":
					companyNameLabel.Text = "Halifax Life Limited";
					companyLogo.Image = new Bitmap(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Images.halifax.gif"));
					break;
				case "HIFM":
					companyNameLabel.Text = "Halifax Investment Fund Managers";
					companyLogo.Image = new Bitmap(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Images.halifax.gif"));
					break;
				case "SALA":
					companyNameLabel.Text = "St Andrews Life";
					companyLogo.Image = new Bitmap(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Images.st_andrews.gif"));
					break;
				case "CMIG":
					companyNameLabel.Text = "Clerical Medical";
					companyLogo.Image = new Bitmap(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Images.clerical_medical_black.gif"));
					break;
				case "EFL":
					companyNameLabel.Text = "EFL OEIC Price for Hi3";
					companyLogo.Image = null;
					break;
			}

			//Set grid styles
			createImportGridStyle();
			createExportGridStyle();

			this.ValuationDayButton.Enabled = Thread.CurrentPrincipal.IsInRole("EndPricingDay");
			OnRefreshData();
		}

		#endregion

		#region Private methods

		private void createImportGridStyle()
		{
			T.E();
			HBOSGrid grid = importDataGrid;
			DataGridTableStyle style = new DataGridTableStyle();

			try
			{
				grid.TableStyles.Clear();
				grid.TableStyles.Add(style);

				style.AlternatingBackColor = Color.WhiteSmoke;
				style.DataGrid = grid;
				style.HeaderForeColor = SystemColors.ControlText;
				style.MappingName = "";

				// Create our import file columns
				T.Log("Create our import file columns");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "ImportFileName", "Import file", 200, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "ImportDateTime", "Last imported/updated", 200, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "ImportedByAccount", "Imported/updated by", 200, HorizontalAlignment.Left, "");
			}
			finally
			{
				T.X();
			}
		}

		private void createExportGridStyle()
		{
			T.E();
			HBOSGrid grid = exportDataGrid;
			DataGridTableStyle style = new DataGridTableStyle();

			try
			{
				grid.TableStyles.Clear();
				grid.TableStyles.Add(style);

				style.AlternatingBackColor = Color.WhiteSmoke;
				style.DataGrid = grid;
				style.HeaderForeColor = SystemColors.ControlText;
				style.MappingName = "";
				style.HeaderFont = grid.Font;

				// Create our export file columns
				T.Log("Create our export file columns");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "FileDescription", "Description", 200, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "Status", "Status", 120, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "TotalFundCount", "Total\nFunds", 100, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "DistributableCount", "Distributable\nFunds", 100, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "DistributedCount", "Distributed\nFunds", 100, HorizontalAlignment.Left, "");
			}
			finally
			{
				T.X();
			}
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.companyNameLabel = new System.Windows.Forms.Label();
			this.companyLogo = new System.Windows.Forms.PictureBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.importsGroupBox = new System.Windows.Forms.GroupBox();
			this.importDataGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.ValuationDayButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.exportDataGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.importsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.importDataGrid)).BeginInit();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.exportDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.companyNameLabel);
			this.panel1.Controls.Add(this.companyLogo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(672, 88);
			this.panel1.TabIndex = 1;
			// 
			// companyNameLabel
			// 
			this.companyNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.companyNameLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.companyNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.companyNameLabel.Location = new System.Drawing.Point(16, 32);
			this.companyNameLabel.Name = "companyNameLabel";
			this.companyNameLabel.Size = new System.Drawing.Size(456, 40);
			this.companyNameLabel.TabIndex = 2;
			// 
			// companyLogo
			// 
			this.companyLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.companyLogo.Location = new System.Drawing.Point(488, 8);
			this.companyLogo.Name = "companyLogo";
			this.companyLogo.Size = new System.Drawing.Size(176, 72);
			this.companyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.companyLogo.TabIndex = 1;
			this.companyLogo.TabStop = false;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.importsGroupBox);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 88);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(672, 184);
			this.panel4.TabIndex = 9;
			// 
			// importsGroupBox
			// 
			this.importsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.importsGroupBox.Controls.Add(this.importDataGrid);
			this.importsGroupBox.ForeColor = System.Drawing.Color.Maroon;
			this.importsGroupBox.Location = new System.Drawing.Point(16, 16);
			this.importsGroupBox.Name = "importsGroupBox";
			this.importsGroupBox.Size = new System.Drawing.Size(643, 152);
			this.importsGroupBox.TabIndex = 3;
			this.importsGroupBox.TabStop = false;
			this.importsGroupBox.Text = "Import Status";
			// 
			// importDataGrid
			// 
			this.importDataGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.importDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.importDataGrid.BackColor = System.Drawing.SystemColors.Window;
			this.importDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.importDataGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.importDataGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.importDataGrid.DataMember = "";
			this.importDataGrid.FlatMode = false;
			this.importDataGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.importDataGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.importDataGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.importDataGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.importDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.importDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.importDataGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.importDataGrid.Location = new System.Drawing.Point(16, 24);
			this.importDataGrid.Name = "importDataGrid";
			this.importDataGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.importDataGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.importDataGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.importDataGrid.PrintColumnSettings = null;
			this.importDataGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.importDataGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.importDataGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.importDataGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.importDataGrid.Size = new System.Drawing.Size(611, 112);
			this.importDataGrid.TabIndex = 0;
			// 
			// splitter2
			// 
			this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 272);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(672, 8);
			this.splitter2.TabIndex = 10;
			this.splitter2.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.ValuationDayButton);
			this.panel3.Controls.Add(this.groupBox1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 280);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(672, 208);
			this.panel3.TabIndex = 11;
			// 
			// ValuationDayButton
			// 
			this.ValuationDayButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.ValuationDayButton.Location = new System.Drawing.Point(256, 168);
			this.ValuationDayButton.Name = "ValuationDayButton";
			this.ValuationDayButton.Size = new System.Drawing.Size(176, 32);
			this.ValuationDayButton.TabIndex = 5;
			this.ValuationDayButton.Text = "Start Next Valuation Day";
			this.ValuationDayButton.Click += new System.EventHandler(this.ValuationDayButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.exportDataGrid);
			this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(643, 144);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Export Status";
			// 
			// exportDataGrid
			// 
			this.exportDataGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.exportDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.exportDataGrid.BackColor = System.Drawing.SystemColors.Window;
			this.exportDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.exportDataGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.exportDataGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.exportDataGrid.DataMember = "";
			this.exportDataGrid.FlatMode = false;
			this.exportDataGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.exportDataGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.exportDataGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.exportDataGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.exportDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.exportDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.exportDataGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.exportDataGrid.Location = new System.Drawing.Point(14, 24);
			this.exportDataGrid.Name = "exportDataGrid";
			this.exportDataGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.exportDataGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.exportDataGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.exportDataGrid.PrintColumnSettings = null;
			this.exportDataGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.exportDataGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.exportDataGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.exportDataGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.exportDataGrid.Size = new System.Drawing.Size(614, 104);
			this.exportDataGrid.TabIndex = 1;
			// 
			// CompanyStatus
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel1);
			this.Name = "CompanyStatus";
			this.Size = new System.Drawing.Size(672, 488);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.importsGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.importDataGrid)).EndInit();
			this.panel3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.exportDataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// Refresh data. This can also be called from the status refresh
		/// </summary>
		public void OnRefreshData()
		{
			T.E();
			try
			{
				//Load all import files
				ImportDetailsCollection importFiles = CurrentStatusController.LoadCompanyImportStatus(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode);
				importDataGrid.BindToCustomCollection(importFiles);

				//Load all export files
				DistributionFileController exportController = new DistributionFileController(GlobalRegistry.ConnectionString);
				DistributionFileCollection exportFiles = exportController.LoadFilesForDistribution(GlobalRegistry.CompanyCode);
				exportDataGrid.BindToCustomCollection(exportFiles);

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Event to say a request to progress the Valuation Date has been made
		/// </summary>
		public event ProgressValuationDayDelegate ProgressValuationDay;

		/// <summary>
		/// 
		/// </summary>
		public delegate void ProgressValuationDayDelegate();


		private void ValuationDayButton_Click(object sender, EventArgs e)
		{
			onProgressValuationDay();
		}


		/// <summary>
		/// Raises the Data changed event
		/// </summary>
		protected virtual void onProgressValuationDay()
		{
			ProgressValuationDay();
		}

	}
}