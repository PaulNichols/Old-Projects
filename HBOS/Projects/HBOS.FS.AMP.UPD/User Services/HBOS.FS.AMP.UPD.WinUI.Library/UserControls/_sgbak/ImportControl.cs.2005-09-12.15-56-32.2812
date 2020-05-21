using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Windows.Controls;
using DataGrid = System.Windows.Forms.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// ImportControl - used to display Import reports
	/// </summary>
	public class ImportControl : UserControl
	{
		#region Controls

		private TabControl tabControlImport;
		private TabPage tabPageValidationErrors;
		private TabPage tabPageImportReport;
		private Windows.Controls.DataGrid dataGridValidationErrors;
		private ImageList imageList1;
		private Button buttonPrint;
		private Button buttonPrintPreview;
		private Button buttonWrite;
		private Panel panelNoErrors;
		private Label labelNoErrors;
		private PictureBox NoErrorsPicture;

		#endregion

		#region Variables

		private DataTable m_validationErrorsDataTable;
		private DataView m_validationErrorsDataView;
		private DataTable m_importedRowsDataTable;
		private HBOSTableStyle hbosTableStyle1;
		private DataGridTextBoxReadOnlyColumn dataGridTextBoxReadOnlyColumn1;
		private Panel panelNoRowsImported;
		private Label label1;
		private PictureBox NoRowsPicture;
		private Windows.Controls.DataGrid dataGridImportedData;
		private DataGridImageColumn dataGridImageColumnSeverity;
		private DataView m_importedRowsDataView;

		#endregion

		#region Contructor

		/// <summary>
		/// Constructor
		/// </summary>
		public ImportControl()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Display the validation errors
		/// </summary>
		/// <param name="validationErrors">List of validation errors as a DataTable</param>
		/// <param name="importedRows">List of imported rows as a DataTable</param>
		public void DisplayValidationErrors(DataTable validationErrors, DataTable importedRows)
		{
			// Always display the Errors tab first
			this.tabControlImport.SelectedIndex = 1;

			if (validationErrors != null)
			{
				m_validationErrorsDataTable = validationErrors;
				m_validationErrorsDataView = m_validationErrorsDataTable.DefaultView;

				// Build the dynamic table style columns
				this.dataGridImageColumnSeverity.EnumType = typeof (ValidationErrorSeverity);
				this.buildTableStyle();

				this.dataGridValidationErrors.CopyDefaultTableStyle(this.hbosTableStyle1);

				this.dataGridValidationErrors.DataSource = m_validationErrorsDataView;

				// Select the first row
				if (this.m_validationErrorsDataView.Count > 0)
				{
					this.dataGridValidationErrors.Select(0);
				}
			}
			else
			{
				this.panelNoErrors.Visible = true;
				this.dataGridValidationErrors.Visible = false;
				this.buttonPrint.Enabled = this.buttonPrintPreview.Enabled = this.buttonWrite.Enabled = false;
			}

			if (importedRows != null)
			{
				this.m_importedRowsDataTable = importedRows;
				this.m_importedRowsDataView = this.m_importedRowsDataTable.DefaultView;

				this.dataGridImportedData.DataSource = this.m_importedRowsDataView;
				SizeColumnsToContent(this.dataGridImportedData, -1);
				this.dataGridImportedData.AllowNavigation = false;
				this.dataGridImportedData.ReadOnly = true;


			}
			else
			{
				this.panelNoRowsImported.Visible = true;
				this.dataGridImportedData.Visible = false;
			}
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Print the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
			SetGridPrinterSettings(dataGridValidationErrors, hbosTableStyle1);
			this.dataGridValidationErrors.Print(m_validationErrorsDataView, "Validation Errors");
		}

		/// <summary>
		/// Print Preview the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrintPreview_Click(object sender, EventArgs e)
		{
			SetGridPrinterSettings(dataGridValidationErrors, hbosTableStyle1);
			this.dataGridValidationErrors.PrintPreview(m_validationErrorsDataView, "Validation Errors");
		}

		/// <summary>
		/// Sets the grid printer settings.
		/// </summary>
		/// <param name="dataGrid">Data grid.</param>
		/// <param name="tableStyle">Table style.</param>
		public static void SetGridPrinterSettings(Windows.Controls.DataGrid dataGrid, DataGridTableStyle tableStyle)
		{
			dataGrid.PrintGridStyle = Windows.Controls.DataGrid.GridPrintStyle.MultipleHorizontalPages;
			dataGrid.PrintPageNumbers = true;
			dataGrid.PrintInPortraitLayout = false;

			dataGrid.PrintColumnSettings = new PrintColumnSettingsCollection();

			foreach (DataGridColumnStyle gridColumnStyle in tableStyle.GridColumnStyles)
			{
				dataGrid.PrintColumnSettings.Add(
					new PrintColumnSettings(
						gridColumnStyle.MappingName, gridColumnStyle.Width,
						StringAlignment.Center, gridColumnStyle.HeaderText));
			}
			//this.dataGridValidationErrors.PrintColumnWidths = new int[] { 300 , 50 , 80 , 80 , 80 , 80 , 80 , 80 , 80 , 80 , 80 , 80, 80 , 80 , 80 , 80 , 80, 80 , 80 , 80 , 80 , 80, 80 , 80 , 80 , 80 , 80, 80 , 80 , 80 , 80 , 80};
		}

		/// <summary>
		/// Write the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonWrite_Click(object sender, EventArgs e)
		{
			string exportFileName;

			try
			{
				SaveFileDialog exportFileDialog = null;

				DialogResult exportFileDialogResult;

				using (exportFileDialog = new SaveFileDialog())
				{
					exportFileDialog.Title = "Save File As";
					exportFileDialog.RestoreDirectory = true;
					exportFileDialog.CheckFileExists = false;
					exportFileDialog.CheckPathExists = true;
					exportFileDialog.Filter = "Comma Seperated Values files (*.csv)|*.csv|All files (*.*)|*.*";
					exportFileDialogResult = exportFileDialog.ShowDialog(this);
					exportFileName = exportFileDialog.FileName;
				}

				if (exportFileDialogResult == DialogResult.OK)
				{
					this.dataGridValidationErrors.Write(m_validationErrorsDataView, exportFileName, "HBOS.FS.AMP.UPD.WinUI.UserControls.ImportControl.xslt");
				}
			}
			catch
			{
				throw;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// we need to add the data columns onto the table style
		/// </summary>
		private void buildTableStyle()
		{
			// Add each data column
			for (int i = 2; i < m_validationErrorsDataTable.Columns.Count; i ++)
			{
				DataGridTextBoxReadOnlyColumn columnToAdd = new DataGridTextBoxReadOnlyColumn();
				columnToAdd.MappingName = m_validationErrorsDataTable.Columns[i].ColumnName;
				columnToAdd.HeaderText = m_validationErrorsDataTable.Columns[i].ColumnName;

				hbosTableStyle1.GridColumnStyles.Add(columnToAdd);
			}
		}

		/// <summary>
		/// Sizes the columns to content.
		/// </summary>
		/// <param name="dataGrid">Data grid.</param>
		/// <param name="nRowsToScan">N rows to scan.</param>
		public static void SizeColumnsToContent(DataGrid dataGrid, int nRowsToScan)
		{
			// Create graphics object for measuring widths.
			Graphics Graphics = dataGrid.CreateGraphics();

			// Define new table style.
			DataGridTableStyle tableStyle = new DataGridTableStyle();

			try
			{
				DataView dataView = (DataView) dataGrid.DataSource;
				DataTable dataTable = dataView.Table;

				if (-1 == nRowsToScan)
				{
					nRowsToScan = dataView.Count;
				}
				else
				{
					// Can only scan rows if they exist.
					nRowsToScan = Math.Min(nRowsToScan,
					                       dataView.Count);
				}

				// Clear any existing table styles.
				dataGrid.TableStyles.Clear();

				// Use mapping name that is defined in the data source.
				tableStyle.MappingName = dataTable.TableName;

				// Now create the column styles within the table style.
				DataGridTextBoxColumn columnStyle;
				int iWidth;

				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count;
					iCurrCol++)
				{
					DataColumn dataColumn = dataTable.Columns[iCurrCol];

					columnStyle = new DataGridTextBoxColumn();

					columnStyle.TextBox.Enabled = true;
					columnStyle.HeaderText = dataColumn.ColumnName;
					columnStyle.MappingName = dataColumn.ColumnName;

					// Set width to header text width.
					iWidth = (int) (Graphics.MeasureString(columnStyle.HeaderText,
					                                       dataGrid.Font).Width);

					// Change width, if data width is wider than header text width.
					// Check the width of the data in the first X rows.
					DataRow dataRow;
					for (int iRow = 0; iRow < nRowsToScan; iRow++)
					{
						dataRow = dataTable.Rows[iRow];

						if (null != dataRow[dataColumn.ColumnName])
						{
							int iColWidth = (int) (Graphics.MeasureString(dataRow.
								ItemArray[iCurrCol].ToString(),
							                                              dataGrid.Font).Width);
							iWidth = Math.Max(iWidth, iColWidth);
						}
					}
					columnStyle.Width = iWidth + 4;

					// Add the new column style to the table style.
					tableStyle.GridColumnStyles.Add(columnStyle);


				}

				// Add the new table style to the data grid.
				dataGrid.TableStyles.Add(tableStyle);
			}
			catch
			{
				throw;
			}
			finally
			{
				Graphics.Dispose();
			}
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (ImportControl));
			this.tabControlImport = new System.Windows.Forms.TabControl();
			this.tabPageImportReport = new System.Windows.Forms.TabPage();
			this.panelNoRowsImported = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.NoRowsPicture = new System.Windows.Forms.PictureBox();
			this.dataGridImportedData = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.hbosTableStyle1 = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
			this.dataGridValidationErrors = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.dataGridTextBoxReadOnlyColumn1 = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
			this.dataGridImageColumnSeverity = new HBOS.FS.AMP.Windows.Controls.DataGridImageColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tabPageValidationErrors = new System.Windows.Forms.TabPage();
			this.panelNoErrors = new System.Windows.Forms.Panel();
			this.labelNoErrors = new System.Windows.Forms.Label();
			this.NoErrorsPicture = new System.Windows.Forms.PictureBox();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.buttonPrintPreview = new System.Windows.Forms.Button();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.tabControlImport.SuspendLayout();
			this.tabPageImportReport.SuspendLayout();
			this.panelNoRowsImported.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.dataGridImportedData)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.dataGridValidationErrors)).BeginInit();
			this.tabPageValidationErrors.SuspendLayout();
			this.panelNoErrors.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlImport
			// 
			this.tabControlImport.Controls.Add(this.tabPageImportReport);
			this.tabControlImport.Controls.Add(this.tabPageValidationErrors);
			this.tabControlImport.Location = new System.Drawing.Point(24, 24);
			this.tabControlImport.Name = "tabControlImport";
			this.tabControlImport.SelectedIndex = 0;
			this.tabControlImport.Size = new System.Drawing.Size(616, 344);
			this.tabControlImport.TabIndex = 0;
			// 
			// tabPageImportReport
			// 
			this.tabPageImportReport.Controls.Add(this.panelNoRowsImported);
			this.tabPageImportReport.Controls.Add(this.dataGridImportedData);
			this.tabPageImportReport.Location = new System.Drawing.Point(4, 22);
			this.tabPageImportReport.Name = "tabPageImportReport";
			this.tabPageImportReport.Size = new System.Drawing.Size(608, 318);
			this.tabPageImportReport.TabIndex = 1;
			this.tabPageImportReport.Text = "Import Report";
			// 
			// panelNoRowsImported
			// 
			this.panelNoRowsImported.Controls.Add(this.label1);
			this.panelNoRowsImported.Controls.Add(this.NoRowsPicture);
			this.panelNoRowsImported.Location = new System.Drawing.Point(8, 8);
			this.panelNoRowsImported.Name = "panelNoRowsImported";
			this.panelNoRowsImported.Size = new System.Drawing.Size(592, 40);
			this.panelNoRowsImported.TabIndex = 6;
			this.panelNoRowsImported.Visible = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "There were no rows imported";
			// 
			// NoRowsPicture
			// 
			this.NoRowsPicture.Image = ((System.Drawing.Image) (resources.GetObject("NoRowsPicture.Image")));
			this.NoRowsPicture.Location = new System.Drawing.Point(8, 8);
			this.NoRowsPicture.Name = "NoRowsPicture";
			this.NoRowsPicture.Size = new System.Drawing.Size(24, 24);
			this.NoRowsPicture.TabIndex = 0;
			this.NoRowsPicture.TabStop = false;
			// 
			// dataGridImportedData
			// 
			this.dataGridImportedData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.dataGridImportedData.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.dataGridImportedData.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.dataGridImportedData.BackColor = System.Drawing.SystemColors.Window;
			this.dataGridImportedData.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridImportedData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridImportedData.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGridImportedData.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridImportedData.DataMember = "";
			this.dataGridImportedData.FlatMode = false;
			this.dataGridImportedData.ForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGridImportedData.GridLineColor = System.Drawing.SystemColors.Control;
			this.dataGridImportedData.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.dataGridImportedData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridImportedData.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.dataGridImportedData.Location = new System.Drawing.Point(8, 8);
			this.dataGridImportedData.Name = "dataGridImportedData";
			this.dataGridImportedData.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.dataGridImportedData.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGridImportedData.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.dataGridImportedData.PrintColumnSettings = null;
			this.dataGridImportedData.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.dataGridImportedData.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.dataGridImportedData.ReadOnly = true;
			this.dataGridImportedData.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dataGridImportedData.RowHeadersVisible = false;
			this.dataGridImportedData.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGridImportedData.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridImportedData.Size = new System.Drawing.Size(592, 304);
			this.dataGridImportedData.TabIndex = 1;
			this.dataGridImportedData.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[]
				{
					this.hbosTableStyle1
				});
			// 
			// hbosTableStyle1
			// 
			this.hbosTableStyle1.DataGrid = this.dataGridImportedData;
			this.hbosTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[]
				{
					this.dataGridTextBoxReadOnlyColumn1,
					this.dataGridImageColumnSeverity
				});
			this.hbosTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.hbosTableStyle1.MappingName = "ValidationErrors";
			// 
			// dataGridValidationErrors
			// 
			this.dataGridValidationErrors.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.dataGridValidationErrors.BackColor = System.Drawing.SystemColors.Window;
			this.dataGridValidationErrors.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridValidationErrors.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGridValidationErrors.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridValidationErrors.DataMember = "";
			this.dataGridValidationErrors.ForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGridValidationErrors.GridLineColor = System.Drawing.SystemColors.Control;
			this.dataGridValidationErrors.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.dataGridValidationErrors.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.dataGridValidationErrors.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.dataGridValidationErrors.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridValidationErrors.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.dataGridValidationErrors.Location = new System.Drawing.Point(8, 16);
			this.dataGridValidationErrors.Name = "dataGridValidationErrors";
			this.dataGridValidationErrors.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.dataGridValidationErrors.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGridValidationErrors.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.dataGridValidationErrors.PrintColumnSettings = null;
			this.dataGridValidationErrors.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.dataGridValidationErrors.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.dataGridValidationErrors.ReadOnly = true;
			this.dataGridValidationErrors.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.dataGridValidationErrors.RowHeadersVisible = false;
			this.dataGridValidationErrors.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGridValidationErrors.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridValidationErrors.Size = new System.Drawing.Size(592, 248);
			this.dataGridValidationErrors.TabIndex = 0;
			this.dataGridValidationErrors.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[]
				{
					this.hbosTableStyle1
				});
			// 
			// dataGridTextBoxReadOnlyColumn1
			// 
			this.dataGridTextBoxReadOnlyColumn1.Format = "";
			this.dataGridTextBoxReadOnlyColumn1.FormatInfo = null;
			this.dataGridTextBoxReadOnlyColumn1.HeaderText = "Error Message";
			this.dataGridTextBoxReadOnlyColumn1.MappingName = "Message";
			this.dataGridTextBoxReadOnlyColumn1.ReadOnly = true;
			this.dataGridTextBoxReadOnlyColumn1.ToolTipProperty = "";
			this.dataGridTextBoxReadOnlyColumn1.Width = 350;
			// 
			// dataGridImageColumnSeverity
			// 
			this.dataGridImageColumnSeverity.Format = "";
			this.dataGridImageColumnSeverity.FormatInfo = null;
			this.dataGridImageColumnSeverity.HeaderText = "Severity";
			this.dataGridImageColumnSeverity.ImageList = this.imageList1;
			this.dataGridImageColumnSeverity.MappingName = "Severity";
			this.dataGridImageColumnSeverity.ToolTipProperty = "";
			this.dataGridImageColumnSeverity.Width = 75;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabPageValidationErrors
			// 
			this.tabPageValidationErrors.Controls.Add(this.panelNoErrors);
			this.tabPageValidationErrors.Controls.Add(this.buttonWrite);
			this.tabPageValidationErrors.Controls.Add(this.buttonPrintPreview);
			this.tabPageValidationErrors.Controls.Add(this.buttonPrint);
			this.tabPageValidationErrors.Controls.Add(this.dataGridValidationErrors);
			this.tabPageValidationErrors.Location = new System.Drawing.Point(4, 22);
			this.tabPageValidationErrors.Name = "tabPageValidationErrors";
			this.tabPageValidationErrors.Size = new System.Drawing.Size(608, 318);
			this.tabPageValidationErrors.TabIndex = 0;
			this.tabPageValidationErrors.Text = "Validation Errors";
			// 
			// panelNoErrors
			// 
			this.panelNoErrors.Controls.Add(this.labelNoErrors);
			this.panelNoErrors.Controls.Add(this.NoErrorsPicture);
			this.panelNoErrors.Location = new System.Drawing.Point(8, 16);
			this.panelNoErrors.Name = "panelNoErrors";
			this.panelNoErrors.Size = new System.Drawing.Size(592, 40);
			this.panelNoErrors.TabIndex = 5;
			this.panelNoErrors.Visible = false;
			// 
			// labelNoErrors
			// 
			this.labelNoErrors.Location = new System.Drawing.Point(32, 8);
			this.labelNoErrors.Name = "labelNoErrors";
			this.labelNoErrors.Size = new System.Drawing.Size(176, 16);
			this.labelNoErrors.TabIndex = 1;
			this.labelNoErrors.Text = "There were no errors in the import";
			// 
			// NoErrorsPicture
			// 
			this.NoErrorsPicture.Image = ((System.Drawing.Image) (resources.GetObject("NoErrorsPicture.Image")));
			this.NoErrorsPicture.Location = new System.Drawing.Point(8, 8);
			this.NoErrorsPicture.Name = "NoErrorsPicture";
			this.NoErrorsPicture.Size = new System.Drawing.Size(24, 24);
			this.NoErrorsPicture.TabIndex = 0;
			this.NoErrorsPicture.TabStop = false;
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(520, 280);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(80, 23);
			this.buttonWrite.TabIndex = 3;
			this.buttonWrite.Text = "&Save Report";
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// buttonPrintPreview
			// 
			this.buttonPrintPreview.Location = new System.Drawing.Point(432, 280);
			this.buttonPrintPreview.Name = "buttonPrintPreview";
			this.buttonPrintPreview.TabIndex = 2;
			this.buttonPrintPreview.Text = "Pre&view";
			this.buttonPrintPreview.Click += new System.EventHandler(this.buttonPrintPreview_Click);
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(344, 280);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.TabIndex = 1;
			this.buttonPrint.Text = "&Print";
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// ImportControl
			// 
			this.Controls.Add(this.tabControlImport);
			this.Name = "ImportControl";
			this.Size = new System.Drawing.Size(664, 392);
			this.tabControlImport.ResumeLayout(false);
			this.tabPageImportReport.ResumeLayout(false);
			this.panelNoRowsImported.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.dataGridImportedData)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.dataGridValidationErrors)).EndInit();
			this.tabPageValidationErrors.ResumeLayout(false);
			this.panelNoErrors.ResumeLayout(false);
			this.ResumeLayout(false);

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

		private System.ComponentModel.IContainer components;

		#endregion
	}
}