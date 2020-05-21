using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
    /// <summary>
    /// Maintain Scale Factor.
    /// </summary>
    public class MaintainScaleFactor : System.Windows.Forms.UserControl
    {
		#region Controls

		private System.Windows.Forms.GroupBox assetFundGroupBox;
		private System.Windows.Forms.Button updateFundsButton;
		private System.Windows.Forms.ComboBox assetFundComboBox;
		private System.Windows.Forms.Label priceIncreaseOnlyLabel;
		private System.Windows.Forms.Label lowerToleranceLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.TextBox revaluationTextBox;
		private System.Windows.Forms.TextBox taxProvisionTextBox;
		private System.Windows.Forms.Label taxProvisionLabel;
		private System.Windows.Forms.TextBox scalingTextBox;
		private System.Windows.Forms.Label revaluationLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;		
		private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;	
		
		/// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;	

		#endregion

		#region private variables

		private string m_selectedAssetFundCode = string.Empty;
		private AssetFundController m_assetFundController;
		private FundController m_fundController;
        private string m_cs = string.Empty;
		private AssetFundCollection m_currentAssetFunds;
		private string m_title = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductName;
		private FundCollection m_funds_clone;
		private HBOS.FS.AMP.Windows.Controls.NumericTextBox XFactorNumericTextBox;
		private int m_CurrentRow = -1;

		private const decimal m_maxFactorValue = .05M;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public MaintainScaleFactor()
        {
			T.E();

			try
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();

				// Anchor the User Control.
				this.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);

				// Set the properties of controls.
				this.fundsGrid.AllowNavigation = false;
				updateFundsButton.Enabled = false;
				cancelButton.Enabled = false;
				saveButton.Enabled = false;
				XFactorNumericTextBox.Enabled = false;
			}
			finally
			{
				T.X();
			}
        }
		#endregion

		#region Public Methods

		/// <summary>
		/// Perform Custom Initialisation for the form
		/// </summary>
		public void CustomInitialisation()
		{
			T.E();

			try
			{
				m_cs = ConfigurationSettings.AppSettings["ConnectionString"];

				this.loadControl();
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private methods
		
		/// <summary> 
		/// Load the user control on the screen.
		/// </summary>
		private void loadControl()
		{
			T.E();

			try
			{
				// Get the currently selected company from the GUI principal thread
				UPDPrincipal updPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;

				// Build collection objects...
				m_assetFundController = new AssetFundController();			// Get the asset funds 
				m_fundController = new FundController();					// Get the funds 

				this.loadAssetFundCombo(updPrincipal.CompanyCode);				// Load the asset funds into the list

				this.addDataGridStyles();        // Set the grid styles

				this.refreshFundGrid();

				// Size the Grid
				this.resizeGrid();
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Load the asset fund combo
		/// </summary>
		/// <param name="companyCode"></param>
		private void loadAssetFundCombo(string companyCode)
		{
			T.E();

			try
			{
				// Load the asset funds for the selected company
				m_currentAssetFunds = m_assetFundController.LoadAssetFunds(m_cs, companyCode,true);

				// Get the asset funds and bind to the combo Box
				this.assetFundComboBox.Enabled = true;
				this.assetFundComboBox.DataSource = m_currentAssetFunds;
				this.assetFundComboBox.DisplayMember = "FullName";
				this.assetFundComboBox.ValueMember = "AssetFundCode";
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add the combox and textbox columns to the grid.
		/// </summary>
		/// <remarks><b>CAUTION</b> Always create DataGridColumnStyle objects and add them to the  
		/// GridColumnStylesCollection before adding DataGridTableStyle objects to the GridTableStylesCollection. 
		/// When you add an empty DataGridTableStyle to the collection, DataGridColumnStyle objects 
		/// are automatically generated for you. Consequently, an exception will be thrown if you 
		/// try to add new DataGridColumnStyle objects with duplicate MappingName values to the 
		/// GridColumnStylesCollection.
		///</remarks>
		private void addDataGridStyles()
		{
			T.E();

			try
			{
				// Create a new DataGridTableStyle and set MappingName.
				DataGridTableStyle factorsGridStyle = new DataGridTableStyle();
				factorsGridStyle.MappingName = "";
				factorsGridStyle.AlternatingBackColor = Color.WhiteSmoke;

				// Fund Code
				DataGridTextBoxColumn fundCodeColStyle = new DataGridTextBoxColumn();
				fundCodeColStyle.MappingName = "HiPortfoliocode";
				fundCodeColStyle.HeaderText = "Fund Code";
				fundCodeColStyle.Width = 100;
				fundCodeColStyle.ReadOnly = true;

				// Short Name
				DataGridTextBoxColumn shortNameColStyle = new DataGridTextBoxColumn();
				shortNameColStyle.MappingName = "ShortName";
				shortNameColStyle.HeaderText = "Short Name";
				shortNameColStyle.ReadOnly = true;

				// X Factor
				DataGridPercentageColumn xFactorColStyle = new DataGridPercentageColumn();
				xFactorColStyle.MappingName = "XFactor";
				xFactorColStyle.HeaderText = "X Factor";
				xFactorColStyle.Alignment = HorizontalAlignment.Right;
				xFactorColStyle.Width = fundCodeColStyle.Width;
				xFactorColStyle.DecimalPlaces = 5;
				xFactorColStyle.AllowNegative = true;

				// Remove any other table styles
				this.fundsGrid.TableStyles.Clear();
                
				// Add column styles to table style.
				factorsGridStyle.GridColumnStyles.Add(fundCodeColStyle);
				factorsGridStyle.GridColumnStyles.Add(shortNameColStyle);   
				factorsGridStyle.GridColumnStyles.Add(xFactorColStyle);   

				// Add the grid style to the GridStylesCollection.
				this.fundsGrid.TableStyles.Add(factorsGridStyle);
			}
			finally
			{
				T.X();
			}
		}	
	
		/// <summary>
		/// This routine resizes the columns in the grid for the current size of the user control.
		/// </summary>
		private void resizeGrid()
		{
			T.E();

			try
			{
				int fixedColumnsWidth;
				GridColumnStylesCollection fundsGridColumns = this.fundsGrid.TableStyles[0].GridColumnStyles;

				fixedColumnsWidth = fundsGridColumns["HiPortfoliocode"].Width +
					fundsGridColumns["XFactor"].Width;

				fundsGridColumns["ShortName"].Width = this.fundsGrid.Width - fixedColumnsWidth - 40;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine fills the grid with fund information for the selected Asset Fund.
		/// </summary>
		private void refreshFundGrid()
		{
			T.E();

			try
			{
				string selectedAssetFundCode = ((AssetFund)assetFundComboBox.SelectedItem).AssetFundCode;


				FundCollection funds = m_fundController.LoadFundsByAssetFund( m_cs , selectedAssetFundCode );
				this.fundsGrid.BindToCustomCollection (funds ); // , typeof(OEICFund));

				// Set control availability.
				if (fundsGrid.Count == 0)
				{
					updateFundsButton.Enabled = false;
					cancelButton.Enabled = false;
					saveButton.Enabled = false;
					XFactorNumericTextBox.Enabled = false;
				}
				else
				{
					updateFundsButton.Enabled = true;
					cancelButton.Enabled = true;
					saveButton.Enabled = true;
					XFactorNumericTextBox.Enabled = true;
				}

				// do not allow an appendrow...
				fundsGrid.AllowNew = false;
				fundsGrid.AllowDelete = false;

				// Clone the Funds so that they can be reverted to their non-committed stated if required.
				m_funds_clone = funds.Clone();

				// Set the initial grid values
				if (m_CurrentRow == -1)
				{
					m_CurrentRow = fundsGrid.CurrentRowIndex;
				}
				else
				{
					fundsGrid.CurrentRowIndex = m_CurrentRow;
				}
				cancelButton.Enabled = false;
				saveButton.Enabled = false;
			}
			catch( System.InvalidOperationException Ex )
			{
				// Error if you try and convert a Fund to an OEIC fund
				ExceptionManager.Publish( Ex );
				T.DumpException( Ex );
				ErrorDialog.Show( Ex  );
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine is fired when the user clicks the update button.
		/// It will assign the specified scale factor and or Xfactor across all specified funds
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateFundsButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				if ( this.validEntry() )
				{
					try
					{
						for (int index = 0; index < this.fundsGrid.Count; index++)
						{
							this.fundsGrid.SetValue(index, "XFactor", XFactorNumericTextBox.Value);
						}
						this.fundsGrid.CommitChanges();					
					}
					catch (System.FormatException formatException)
					{
						ExceptionManager.Publish (formatException);
						MessageBox.Show ("You have not entered a valid factor", "Invalid tolerance");
					}
					cancelButton.Enabled = true;
					saveButton.Enabled = true;
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine validates the screen entry.
		/// </summary>
		private bool validEntry()
		{
			T.E();

			try
			{
				bool returnValue = true;
				bool maxFactorWarning = true;

				//	X Factor
				if ( XFactorNumericTextBox.Text != String.Empty )
				{
					if (!this.validateFactor( XFactorNumericTextBox.Value,"X", ref maxFactorWarning) )
					{
						XFactorNumericTextBox.Focus();
						returnValue = false;
					}
				}
				else
				{
					MessageBox.Show( "Please enter a value for XFactor" , "Invalid XFactor" , MessageBoxButtons.OK , MessageBoxIcon.Error );
					returnValue = false;
				}

				return returnValue;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Validate the factor
		/// </summary>
		/// <param name="factorValue"></param>
		/// <param name="factorText"></param>
		/// <param name="maxFactorWarning"></param>
		/// <returns></returns>
		private bool validateFactor(decimal factorValue, string factorText, ref bool maxFactorWarning)
		{
			T.E();

			try
			{
/*				int factorDecimals = 5;
				Double factorDouble;
				bool numericFactor= true;
				string factorString; */
			
				if (maxFactorWarning && ( factorValue > m_maxFactorValue ) )
				{
					if (MessageBox.Show(factorValue.ToString("p") + " factor is greater than the default maximum factor value of " + m_maxFactorValue.ToString( "p" ) + ". Do you wish to continue?",m_title,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No)
					{
						return false;
					}
					else
					{
						maxFactorWarning = false;
					}
				}
				return true;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine will extract the fund collection from the grid and if it is dirty it will update the
		/// funds collection attached to the asset fund.
		/// </summary>
		private void saveFundCollection(FundCollection updatedCollection)
		{
			T.E();

			// Persist all items back to the DB when one or more existing items have 
			// changed or appended
			try
			{
				// Are there any funds to save.
				if (updatedCollection.Count != 0)
				{
					bool validFlag = m_fundController.Update (m_cs, updatedCollection);

					if ( validFlag )
					{
						this.refreshFundGrid();
					}
					else
					{
						MessageBox.Show("Unable to Save Funds", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			finally
			{
				T.X();
			}
		}
		#endregion

		#region Properties

		/// <summary>
		/// Test for modified rows in the grid
		/// </summary>
		/// <returns>Flag indicating if rows have been modified</returns>
		private bool gridIsDirty()
		{
			T.E();

			try
			{
				FundCollection modifiedFunds = (FundCollection)this.fundsGrid.RetrieveUpdatedCustomCollection(); //typeof(OEICFund));
            
				return modifiedFunds.Count != 0;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region System methods
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
		#endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.assetFundGroupBox = new System.Windows.Forms.GroupBox();
			this.XFactorNumericTextBox = new HBOS.FS.AMP.Windows.Controls.NumericTextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.revaluationTextBox = new System.Windows.Forms.TextBox();
			this.updateFundsButton = new System.Windows.Forms.Button();
			this.assetFundComboBox = new System.Windows.Forms.ComboBox();
			this.taxProvisionTextBox = new System.Windows.Forms.TextBox();
			this.priceIncreaseOnlyLabel = new System.Windows.Forms.Label();
			this.taxProvisionLabel = new System.Windows.Forms.Label();
			this.lowerToleranceLabel = new System.Windows.Forms.Label();
			this.scalingTextBox = new System.Windows.Forms.TextBox();
			this.revaluationLabel = new System.Windows.Forms.Label();
			this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.cancelButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.assetFundGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// assetFundGroupBox
			// 
			this.assetFundGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.assetFundGroupBox.Controls.Add(this.XFactorNumericTextBox);
			this.assetFundGroupBox.Controls.Add(this.comboBox1);
			this.assetFundGroupBox.Controls.Add(this.label1);
			this.assetFundGroupBox.Controls.Add(this.revaluationTextBox);
			this.assetFundGroupBox.Controls.Add(this.updateFundsButton);
			this.assetFundGroupBox.Controls.Add(this.assetFundComboBox);
			this.assetFundGroupBox.Controls.Add(this.taxProvisionTextBox);
			this.assetFundGroupBox.Controls.Add(this.priceIncreaseOnlyLabel);
			this.assetFundGroupBox.Controls.Add(this.taxProvisionLabel);
			this.assetFundGroupBox.Controls.Add(this.lowerToleranceLabel);
			this.assetFundGroupBox.Controls.Add(this.scalingTextBox);
			this.assetFundGroupBox.Controls.Add(this.revaluationLabel);
			this.assetFundGroupBox.Location = new System.Drawing.Point(8, 8);
			this.assetFundGroupBox.Name = "assetFundGroupBox";
			this.assetFundGroupBox.Size = new System.Drawing.Size(688, 80);
			this.assetFundGroupBox.TabIndex = 0;
			this.assetFundGroupBox.TabStop = false;
			this.assetFundGroupBox.Text = "Asset Fund";
			// 
			// XFactorNumericTextBox
			// 
			this.XFactorNumericTextBox.DecimalPlaces = 5;
			this.XFactorNumericTextBox.Location = new System.Drawing.Point(408, 32);
			this.XFactorNumericTextBox.MaxLength = 10;
			this.XFactorNumericTextBox.MaxValue = new System.Decimal(new int[] {
																				   100,
																				   0,
																				   0,
																				   0});
			this.XFactorNumericTextBox.MinValue = new System.Decimal(new int[] {
																				   100,
																				   0,
																				   0,
																				   -2147483648});
			this.XFactorNumericTextBox.Name = "XFactorNumericTextBox";
			this.XFactorNumericTextBox.TabIndex = 12;
			this.XFactorNumericTextBox.Text = "";
			this.XFactorNumericTextBox.ValueMultiplier = new System.Decimal(new int[] {
																						  100,
																						  0,
																						  0,
																						  0});
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(320, 120);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(80, 21);
			this.comboBox1.TabIndex = 8;
			this.comboBox1.Visible = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(416, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "X Factor";
			// 
			// revaluationTextBox
			// 
			this.revaluationTextBox.Location = new System.Drawing.Point(120, 120);
			this.revaluationTextBox.Name = "revaluationTextBox";
			this.revaluationTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.revaluationTextBox.Size = new System.Drawing.Size(80, 20);
			this.revaluationTextBox.TabIndex = 4;
			this.revaluationTextBox.Text = "";
			this.revaluationTextBox.Visible = false;
			// 
			// updateFundsButton
			// 
			this.updateFundsButton.Location = new System.Drawing.Point(544, 32);
			this.updateFundsButton.Name = "updateFundsButton";
			this.updateFundsButton.Size = new System.Drawing.Size(104, 23);
			this.updateFundsButton.TabIndex = 11;
			this.updateFundsButton.Text = "&Update All Funds";
			this.updateFundsButton.Click += new System.EventHandler(this.updateFundsButton_Click);
			// 
			// assetFundComboBox
			// 
			this.assetFundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.assetFundComboBox.Location = new System.Drawing.Point(16, 32);
			this.assetFundComboBox.Name = "assetFundComboBox";
			this.assetFundComboBox.Size = new System.Drawing.Size(224, 21);
			this.assetFundComboBox.TabIndex = 0;
			this.assetFundComboBox.SelectionChangeCommitted += new System.EventHandler(this.assetFundComboBox_SelectionChangeCommitted);
			// 
			// taxProvisionTextBox
			// 
			this.taxProvisionTextBox.Location = new System.Drawing.Point(16, 120);
			this.taxProvisionTextBox.Name = "taxProvisionTextBox";
			this.taxProvisionTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.taxProvisionTextBox.Size = new System.Drawing.Size(80, 20);
			this.taxProvisionTextBox.TabIndex = 2;
			this.taxProvisionTextBox.Text = "";
			this.taxProvisionTextBox.Visible = false;
			// 
			// priceIncreaseOnlyLabel
			// 
			this.priceIncreaseOnlyLabel.Location = new System.Drawing.Point(320, 104);
			this.priceIncreaseOnlyLabel.Name = "priceIncreaseOnlyLabel";
			this.priceIncreaseOnlyLabel.Size = new System.Drawing.Size(68, 16);
			this.priceIncreaseOnlyLabel.TabIndex = 7;
			this.priceIncreaseOnlyLabel.Text = "Valuation";
			this.priceIncreaseOnlyLabel.Visible = false;
			// 
			// taxProvisionLabel
			// 
			this.taxProvisionLabel.Location = new System.Drawing.Point(16, 104);
			this.taxProvisionLabel.Name = "taxProvisionLabel";
			this.taxProvisionLabel.Size = new System.Drawing.Size(88, 16);
			this.taxProvisionLabel.TabIndex = 1;
			this.taxProvisionLabel.Text = "Tax Provision";
			this.taxProvisionLabel.Visible = false;
			// 
			// lowerToleranceLabel
			// 
			this.lowerToleranceLabel.Location = new System.Drawing.Point(216, 104);
			this.lowerToleranceLabel.Name = "lowerToleranceLabel";
			this.lowerToleranceLabel.Size = new System.Drawing.Size(88, 16);
			this.lowerToleranceLabel.TabIndex = 5;
			this.lowerToleranceLabel.Text = "Scaling";
			this.lowerToleranceLabel.Visible = false;
			// 
			// scalingTextBox
			// 
			this.scalingTextBox.Location = new System.Drawing.Point(216, 120);
			this.scalingTextBox.Name = "scalingTextBox";
			this.scalingTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.scalingTextBox.Size = new System.Drawing.Size(80, 20);
			this.scalingTextBox.TabIndex = 6;
			this.scalingTextBox.Text = "";
			this.scalingTextBox.Visible = false;
			// 
			// revaluationLabel
			// 
			this.revaluationLabel.Location = new System.Drawing.Point(120, 104);
			this.revaluationLabel.Name = "revaluationLabel";
			this.revaluationLabel.Size = new System.Drawing.Size(85, 16);
			this.revaluationLabel.TabIndex = 3;
			this.revaluationLabel.Text = "Revaluation";
			this.revaluationLabel.Visible = false;
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
			this.fundsGrid.Location = new System.Drawing.Point(8, 96);
			this.fundsGrid.Name = "fundsGrid";
			//this.fundsGrid.PrintColumnWidths = null;
			this.fundsGrid.Size = new System.Drawing.Size(688, 328);
			this.fundsGrid.TabIndex = 1;
			this.fundsGrid.RowChanged += new HBOS.FS.AMP.Windows.Controls.DataGrid.RowChangedDelegate(this.fundsGrid_RowChanged);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(624, 432);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(544, 432);
			this.saveButton.Name = "saveButton";
			this.saveButton.TabIndex = 2;
			this.saveButton.Text = "&Save";
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// MaintainScaleFactor
			// 
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.fundsGrid);
			this.Controls.Add(this.assetFundGroupBox);
			this.Name = "MaintainScaleFactor";
			this.Size = new System.Drawing.Size(704, 464);
			this.Resize += new System.EventHandler(this.MaintainScaleFactor_Resize);
			this.assetFundGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
			this.ResumeLayout(false);

		}
        #endregion

		#region Control events

		/// <summary>
		/// User has changed Asset Fund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void assetFundComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				m_CurrentRow = -1;
				this.refreshFundGrid();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Control has been resized
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaintainScaleFactor_Resize(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.resizeGrid();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User pressed the cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				if (MessageBox.Show("This will cancel all non-saved Fund changes. Are you sure you wish to continue?",m_title,MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes )
				{
					this.refreshFundGrid();
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User presed the save button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				int index = 0;
				bool maxFactorWarning = true;

				// Firstly, retrieve the modified funds collection from the grid.
				FundCollection updatedCollection = (FundCollection)this.fundsGrid.RetrieveUpdatedCustomCollection(); //typeof(OEICFund));

				foreach(Fund myFund in updatedCollection)
				{
					if (!this.validateFactor(myFund.XFactor, "X", ref maxFactorWarning))
					{
						this.fundsGrid.UnSelect(index);
						this.fundsGrid.Select();
						fundsGrid.CurrentCell = new DataGridCell(index, 2);
						return;
					}
					index++;
				}

				this.saveFundCollection(updatedCollection);
			}
			catch (System.FormatException formatException)
			{
				ExceptionManager.Publish (formatException);
				T.DumpException( formatException );
				ErrorDialog.Show( formatException );
			}
		}

		
		/// <summary>
		/// Enable the buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fundsGrid_RowChanged(object sender, HBOS.FS.AMP.Windows.Controls.CellEventArgs e)
		{
			T.E();

			try
			{
				saveButton.Enabled = true;
				cancelButton.Enabled = true;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		/// <summary>
		/// Sets up the form on loading.
		/// </summary>
		/// <param name="e">Event arguments.</param>
        protected override void OnLoad(EventArgs e)
        {
            // Resize the filter box column width; Normal scale width = 5.32.
            float scaleWidth = Form.GetAutoScaleSize(this.Font).Width;
            float adjustment = scaleWidth / 5.32F;
            
            this.updateFundsButton.Width = (int)((float)this.updateFundsButton.Width * adjustment);
            this.XFactorNumericTextBox.Left = label1.Left;
            this.XFactorNumericTextBox.Top = assetFundComboBox.Top;

            base.OnLoad (e);
        }

	}
}
