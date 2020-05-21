using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Common.ExceptionManagement;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
    /// <summary>
    /// Summary description for AssetFundDetailViewControl.
    /// </summary>
    public class AssetFundDetailViewControl : System.Windows.Forms.UserControl
    {
        private DataTable m_FundsTable = null ;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox newToleranceTextBox;
        private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
        private System.Windows.Forms.Label newToleranceLabel;
        private System.Windows.Forms.Button updateFundsButton;
        private System.Windows.Forms.ToolTip allToolTips;
        private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle mainTableStyle;
        private System.Windows.Forms.DataGridTextBoxColumn fullNameColumn;
        private System.Windows.Forms.DataGridTextBoxColumn toleranceColumn;
        private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new <see cref="AssetFundDetailViewControl"/> instance.
		/// </summary>
		/// <param name="assetFundName">Name of the asset fund.</param>
        public AssetFundDetailViewControl(string assetFundName)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // Add the tooltips to the control
            AddToolTips();

			// Remove call to temporary data source - VM 23/02/2005
//            FundController controller = new FundController();
//            m_Funds = controller.GimmeSomeFunds(0, assetFundName);
//            m_FundsTable = m_Funds.ConvertToDataTable();
//            m_FundsTable.TableName = "Main";
//            fundsGrid.DataSource = m_FundsTable.DefaultView;
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
            this.components = new System.ComponentModel.Container();
            this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
            this.mainTableStyle = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
            this.fullNameColumn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.toleranceColumn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.exportButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.newToleranceTextBox = new System.Windows.Forms.TextBox();
            this.newToleranceLabel = new System.Windows.Forms.Label();
            this.updateFundsButton = new System.Windows.Forms.Button();
            this.allToolTips = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // fundsGrid
            // 
            this.fundsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.fundsGrid.DataMember = "";
            this.fundsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fundsGrid.GridLineColor = System.Drawing.Color.Black;
            this.fundsGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fundsGrid.Location = new System.Drawing.Point(8, 40);
            this.fundsGrid.Name = "fundsGrid";
            //this.fundsGrid.PrintColumnWidths = null;
            this.fundsGrid.Size = new System.Drawing.Size(496, 192);
            this.fundsGrid.TabIndex = 0;
            this.fundsGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
                                                                                                  this.mainTableStyle});
            // 
            // mainTableStyle
            // 
            this.mainTableStyle.DataGrid = this.fundsGrid;
            this.mainTableStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                             this.fullNameColumn,
                                                                                                             this.toleranceColumn});
            this.mainTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.mainTableStyle.MappingName = "Main";
            // 
            // fullNameColumn
            // 
            this.fullNameColumn.Format = "";
            this.fullNameColumn.FormatInfo = null;
            this.fullNameColumn.HeaderText = "Fund Name";
            this.fullNameColumn.MappingName = "FullName";
            this.fullNameColumn.Width = 75;
            // 
            // toleranceColumn
            // 
            this.toleranceColumn.Format = "";
            this.toleranceColumn.FormatInfo = null;
            this.toleranceColumn.HeaderText = "Tolerance";
            this.toleranceColumn.MappingName = "Tolerance";
            this.toleranceColumn.Width = 75;
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(432, 256);
            this.exportButton.Name = "exportButton";
            this.exportButton.TabIndex = 9;
            this.exportButton.Text = "Export";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(344, 256);
            this.printButton.Name = "printButton";
            this.printButton.TabIndex = 8;
            this.printButton.Text = "Print";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(256, 256);
            this.saveButton.Name = "saveButton";
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // newToleranceTextBox
            // 
            this.newToleranceTextBox.Location = new System.Drawing.Point(344, 8);
            this.newToleranceTextBox.Name = "newToleranceTextBox";
            this.newToleranceTextBox.Size = new System.Drawing.Size(56, 20);
            this.newToleranceTextBox.TabIndex = 11;
            this.newToleranceTextBox.Text = "";
            // 
            // newToleranceLabel
            // 
            this.newToleranceLabel.Location = new System.Drawing.Point(256, 8);
            this.newToleranceLabel.Name = "newToleranceLabel";
            this.newToleranceLabel.Size = new System.Drawing.Size(80, 23);
            this.newToleranceLabel.TabIndex = 12;
            this.newToleranceLabel.Text = "New tolerance";
            // 
            // updateFundsButton
            // 
            this.updateFundsButton.Location = new System.Drawing.Point(432, 8);
            this.updateFundsButton.Name = "updateFundsButton";
            this.updateFundsButton.TabIndex = 13;
            this.updateFundsButton.Text = "Update";
            this.updateFundsButton.Click += new System.EventHandler(this.updateFundsButton_Click);
            // 
            // AssetFundDetailViewControl
            // 
            this.Controls.Add(this.updateFundsButton);
            this.Controls.Add(this.newToleranceLabel);
            this.Controls.Add(this.newToleranceTextBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.fundsGrid);
            this.Name = "AssetFundDetailViewControl";
            this.Size = new System.Drawing.Size(520, 296);
            ((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Save the updated data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, System.EventArgs e)
        {
			// Remove call to temporary data source - VM 23/02/2005
//            FundController saveFunds = new FundController();
//
//            saveFunds.SaveSomeFunds(m_Funds);
        }

        /// <summary>
        /// Sets the tooltips for the control
        /// </summary>
        private void AddToolTips ()
        {
            this.allToolTips.SetToolTip (this.updateFundsButton,"Update all funds with this tolerance");
        }

        /// <summary>
        /// User has chosen to update all funds with the new tolerance factor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateFundsButton_Click(object sender, System.EventArgs e)
        {
            decimal newTolerance;
            try
            {
                newTolerance = decimal.Parse( this.newToleranceTextBox.Text);
                
                int totalRows = m_FundsTable.Rows.Count;

                for (int counter = 0; counter < totalRows; counter++)
                {
                        DataRow currentRow = m_FundsTable.Rows[counter];
                        currentRow["Tolerance"] = newTolerance;
                        currentRow.AcceptChanges();
                }                
            }
            catch (System.FormatException formatException)
            {
                ExceptionManager.Publish (formatException);
                MessageBox.Show ("You have not entered a valid tolerance", "Invalid tolerance");
            }
        }

        private void printButton_Click(object sender, System.EventArgs e)
        {
            fundsGrid.PrintPreview( m_FundsTable.DefaultView, "Test Output" ); //, HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.Normal );
        }

        private void exportButton_Click(object sender, System.EventArgs e)
        {
            fundsGrid.Write( m_FundsTable.DefaultView , @"C:\SampleOutput.csv" , "HBOS.FS.AMP.UPD.WinUI.UserControls.AssetFundDetailViewControl.xslt" );
            MessageBox.Show( @"Created C:\SampleOutput.csv" );

        }
    }
}
