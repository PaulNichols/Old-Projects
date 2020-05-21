using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.ExceptionManagement;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.AMP.Utilities;

using HBOS.FS.Support.Tex;
using HBOS.FS.AMP.UPD.WinUI.Helpers;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{

    // SJR - 24 Mar 2005
    // TODO: Speak with Kev to find out if this is legacy stuff
    // NOTE: The tracing requires 'T'
    //using T = HBOS.FS.AMP.UPD.Controllers;

	/// <summary>
	/// AssetFundControl - allows maintenance of an asset fund.
	/// </summary>
	public class AssetFundControl : System.Windows.Forms.UserControl
	{
        #region Events
        
		// Historial and no longer needed.

//		/// <summary>
//		/// Show asset Fund details delegate
//		/// </summary>
//        public delegate void ShowAssetFundDetails (string assetFundName);
//
//		/// <summary>
//		/// DisplayAssetFundDetails event
//		/// </summary>
//        // public event ShowAssetFundDetails DisplayAssetFundDetails;

		#endregion

		#region Variables

		private string m_connectionString = "";
		/* all reference to asset fund light removed. 
		 * TODO - a complete new UI to replace this one
		 * 
		private AssetFundLight m_selectedAssetFund = null;
		private AssetFundLightCollection m_allCompanyAssetFundsLight = null;
		*/

		private FundGroupCollection m_assetFundGroupCollection = null;

		private int m_currentlySelectedIndex = 0;
		private bool m_userEditing = false;

		#endregion

		#region Controls

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListBox AssetFundListBox;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxFundGroup;
		private System.Windows.Forms.Label labelFundGroup;
		private System.Windows.Forms.TextBox textBoxFullName;
		private System.Windows.Forms.Label labelFullName;
		private System.Windows.Forms.TextBox textBoxShortName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxAssetFundCode;
		private System.Windows.Forms.Label labelAssetFund;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonNew;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ErrorProvider errorProvider1;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public AssetFundControl()
		{
			T.E();
			try
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();

				m_connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				//m_allCompanyAssetFundsLight = Cache.CompanyAssetFundsLight;

				// Copy the Fund Groups and add a None
				m_assetFundGroupCollection = new FundGroupCollection();
				m_assetFundGroupCollection.Add( new AssetFundGroup( 0 , Cache.CompanyCode , "<None>" , "<None>" , null, false, false, 0 ) );

				for ( int i = 0 ; i < Cache.CompanyAssetFundGroups.Count ; i ++ )
				{
					m_assetFundGroupCollection.Add( Cache.CompanyAssetFundGroups[i] );
				}


				// Set the data sources
				this.comboBoxFundGroup.DataSource = m_assetFundGroupCollection;
				AssetFundListBox.DisplayMember = "FullName";
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				//this.AssetFundListBox.DataSource = m_allCompanyAssetFundsLight;
			}
			finally
			{
				T.X();
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
			T.E();
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.comboBoxFundGroup = new System.Windows.Forms.ComboBox();
			this.textBoxFullName = new System.Windows.Forms.TextBox();
			this.textBoxShortName = new System.Windows.Forms.TextBox();
			this.textBoxAssetFundCode = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonNew = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.AssetFundListBox = new System.Windows.Forms.ListBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelFundGroup = new System.Windows.Forms.Label();
			this.labelFullName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelAssetFund = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxFundGroup
			// 
			this.comboBoxFundGroup.DisplayMember = "FullName";
			this.comboBoxFundGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFundGroup.Location = new System.Drawing.Point(128, 120);
			this.comboBoxFundGroup.Name = "comboBoxFundGroup";
			this.comboBoxFundGroup.Size = new System.Drawing.Size(136, 21);
			this.comboBoxFundGroup.TabIndex = 40;
			this.toolTip1.SetToolTip(this.comboBoxFundGroup, "Fund group for Asset fund");
			this.comboBoxFundGroup.ValueMember = "ID";
			this.comboBoxFundGroup.SelectedIndexChanged += new System.EventHandler(this.selectedItemChanged);
			// 
			// textBoxFullName
			// 
			this.textBoxFullName.Location = new System.Drawing.Point(128, 88);
			this.textBoxFullName.MaxLength = 100;
			this.textBoxFullName.Name = "textBoxFullName";
			this.textBoxFullName.Size = new System.Drawing.Size(200, 20);
			this.textBoxFullName.TabIndex = 39;
			this.textBoxFullName.Text = "";
			this.toolTip1.SetToolTip(this.textBoxFullName, "Long name for asset fund");
			this.textBoxFullName.Validated += new System.EventHandler(this.inputBox_Validated);
			this.textBoxFullName.TextChanged += new System.EventHandler(this.selectedItemChanged);
			// 
			// textBoxShortName
			// 
			this.textBoxShortName.Location = new System.Drawing.Point(128, 56);
			this.textBoxShortName.MaxLength = 50;
			this.textBoxShortName.Name = "textBoxShortName";
			this.textBoxShortName.Size = new System.Drawing.Size(136, 20);
			this.textBoxShortName.TabIndex = 38;
			this.textBoxShortName.Text = "";
			this.toolTip1.SetToolTip(this.textBoxShortName, "Short name for asset Fund");
			this.textBoxShortName.Validated += new System.EventHandler(this.inputBox_Validated);
			this.textBoxShortName.TextChanged += new System.EventHandler(this.selectedItemChanged);
			// 
			// textBoxAssetFundCode
			// 
			this.textBoxAssetFundCode.Location = new System.Drawing.Point(128, 24);
			this.textBoxAssetFundCode.MaxLength = 5;
			this.textBoxAssetFundCode.Name = "textBoxAssetFundCode";
			this.textBoxAssetFundCode.ReadOnly = true;
			this.textBoxAssetFundCode.Size = new System.Drawing.Size(72, 20);
			this.textBoxAssetFundCode.TabIndex = 37;
			this.textBoxAssetFundCode.Text = "";
			this.toolTip1.SetToolTip(this.textBoxAssetFundCode, "Asset fund code for Asset fund");
			this.textBoxAssetFundCode.Validated += new System.EventHandler(this.inputBox_Validated);
			this.textBoxAssetFundCode.TextChanged += new System.EventHandler(this.selectedItemChanged);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Enabled = false;
			this.buttonCancel.Location = new System.Drawing.Point(280, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(81, 23);
			this.buttonCancel.TabIndex = 9;
			this.buttonCancel.Text = "&Cancel";
			this.toolTip1.SetToolTip(this.buttonCancel, "Cancel the changes to the current Asset Fund");
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Enabled = false;
			this.buttonSave.Location = new System.Drawing.Point(192, 8);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(81, 23);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "&Save";
			this.toolTip1.SetToolTip(this.buttonSave, "Save the current Asset Fund");
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonNew
			// 
			this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNew.Location = new System.Drawing.Point(104, 8);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(81, 23);
			this.buttonNew.TabIndex = 7;
			this.buttonNew.Text = "&New";
			this.toolTip1.SetToolTip(this.buttonNew, "Create a new Asset Fund");
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(568, 16);
			this.panel3.TabIndex = 22;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.AssetFundListBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 360);
			this.panel1.TabIndex = 23;
			// 
			// AssetFundListBox
			// 
			this.AssetFundListBox.DisplayMember = "AssetFundCode";
			this.AssetFundListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AssetFundListBox.Location = new System.Drawing.Point(0, 0);
			this.AssetFundListBox.Name = "AssetFundListBox";
			this.AssetFundListBox.Size = new System.Drawing.Size(200, 355);
			this.AssetFundListBox.TabIndex = 0;
			this.AssetFundListBox.ValueMember = "AssetFundCode";
			this.AssetFundListBox.SelectedIndexChanged += new System.EventHandler(this.AssetFundListBox_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(200, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(368, 360);
			this.panel2.TabIndex = 27;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.comboBoxFundGroup);
			this.groupBox1.Controls.Add(this.labelFundGroup);
			this.groupBox1.Controls.Add(this.textBoxFullName);
			this.groupBox1.Controls.Add(this.labelFullName);
			this.groupBox1.Controls.Add(this.textBoxShortName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBoxAssetFundCode);
			this.groupBox1.Controls.Add(this.labelAssetFund);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 320);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// labelFundGroup
			// 
			this.labelFundGroup.Location = new System.Drawing.Point(16, 120);
			this.labelFundGroup.Name = "labelFundGroup";
			this.labelFundGroup.TabIndex = 44;
			this.labelFundGroup.Text = "Fund Group";
			// 
			// labelFullName
			// 
			this.labelFullName.Location = new System.Drawing.Point(16, 88);
			this.labelFullName.Name = "labelFullName";
			this.labelFullName.TabIndex = 43;
			this.labelFullName.Text = "Long Name";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.TabIndex = 42;
			this.label1.Text = "Short Name";
			// 
			// labelAssetFund
			// 
			this.labelAssetFund.Location = new System.Drawing.Point(16, 24);
			this.labelAssetFund.Name = "labelAssetFund";
			this.labelAssetFund.Size = new System.Drawing.Size(112, 23);
			this.labelAssetFund.TabIndex = 41;
			this.labelAssetFund.Text = "Asset Fund Code";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.buttonCancel);
			this.panel4.Controls.Add(this.buttonSave);
			this.panel4.Controls.Add(this.buttonNew);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(200, 336);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(368, 40);
			this.panel4.TabIndex = 29;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(200, 16);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(6, 320);
			this.splitter1.TabIndex = 46;
			this.splitter1.TabStop = false;
			// 
			// AssetFundControl
			// 
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Name = "AssetFundControl";
			this.Size = new System.Drawing.Size(568, 376);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);
			T.X();

        }

		private System.ComponentModel.IContainer components;

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

		#region Event Handlers

		/// <summary>
		/// User has chosen a different asset fund code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AssetFundListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			T.E();
			try
			{
				m_userEditing = false;
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/
				/*
				// If the asset fund is dirty and is invalid, go back to the invalid record
				if ( m_selectedAssetFund != null && m_selectedAssetFund.IsDirty && this.validateScreen() == false )
				{
					this.AssetFundListBox.SelectedIndex = m_currentlySelectedIndex;
				}
				else
				{
					// Do we need to save the current select Asset Fund
					this.checkSaveCurrentAssetFund( this.AssetFundListBox.SelectedIndex );

					m_selectedAssetFund = (AssetFundLight)AssetFundListBox.SelectedItem;
					this.displayAssetFund();

					// Valide the new asset fund displayed
					this.validateScreen();
				}

				*/

				// Remember the selected Index
				m_currentlySelectedIndex = this.AssetFundListBox.SelectedIndex;

				m_userEditing = true;
			}
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// The selected item has changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectedItemChanged(object sender, EventArgs e)
		{
			T.E();

            try
            {
                if ( m_userEditing )
                {
                    this.enableButtons( true );
					/* all reference to asset fund light removed. 
					* TODO - a complete new UI to replace this one
					*/

                    //m_selectedAssetFund.IsDirty = true;
                }
            }
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "AssetFundUnableToSaveTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Refresh the old Asset Fund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			T.E();
			try
			{
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				/*
				// If we are working on a new one, we need to remove it from the list
				if ( m_selectedAssetFund.IsNew )
				{
					m_allCompanyAssetFundsLight.Remove( m_selectedAssetFund );
					m_selectedAssetFund = null;
					this.refreshAssetFundList();
					//AssetFundListBox.SelectedIndex = m_allCompanyAssetFundsLight.Count;
				}
				else
				{
					this.displayAssetFund();
					m_selectedAssetFund.IsDirty = false;
				}
				*/
			}
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "AssetFundUnableToSaveTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Create a new Asset Fund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNew_Click(object sender, System.EventArgs e)
		{
			T.E();
			try
			{
				// Do we need to save the current asset fund
				this.checkSaveCurrentAssetFund( this.AssetFundListBox.SelectedIndex );

				int fundGroupId = 0;

				// If there are some Asset Fund Groups, choose the first as the default
				if ( m_assetFundGroupCollection.Count > 0 )
				{
					fundGroupId = m_assetFundGroupCollection[0].ID;
				}

				// Create a new Asset Fund

				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/
				//AssetFundLight newAssetFundLight = new AssetFundLight( "New" , "" , "" , Cache.CompanyCode , fundGroupId , false , null );
				//newAssetFundLight.IsDirty = true;
				//newAssetFundLight.IsNew = true;

				// Add the new asset fund to the collection and force the GUI to update.
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/
				//int indexPosition = m_allCompanyAssetFundsLight.Add( newAssetFundLight );
				this.refreshAssetFundList();
			
				// Select the right entry in the list
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/
				//AssetFundListBox.SelectedIndex = indexPosition;
				textBoxAssetFundCode.Focus();
			}
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "AssetFundUnableToSaveTitle", ex);
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Save the new/updated Asset Fund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			T.E();
			try
			{
				this.saveAssetFund();
				this.refreshAssetFundList();
			}
			catch(ConstraintViolationException ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("DuplicateAssetFundBody", "DuplicateAssetFundTitle", ex);
				textBoxAssetFundCode.Focus();
				textBoxAssetFundCode.SelectAll();
			}
			catch (ConcurrencyViolationException ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("AssetFundChangedBody", "AssetFundUnableToSaveTitle", ex);
				// Reload the asset funds list and select the fund that was due to be saved, but failed.
                
				// This code was taken from the cancel action, but it doesn't actually work.
				// This is because even if the save method fails the underlying data source for the list boxes
				// and the edit fields are not restored.
				this.displayAssetFund();               
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				//m_selectedAssetFund.IsDirty = false;
				// TODO: this is a placeholder that needs to be fixed when the new GUI comes along.
			}
			catch (DatabaseException ex) //covers Invalid sql parameter too
			{
				GUIExceptionHelper.LogAndDisplayException ("DatabaseError", "AssetFundUnableToSaveTitle", ex);
			}
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "AssetFundUnableToSaveTitle", ex);
			}					
			finally
			{
				T.X();
			}
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Enable the buttons appropriately
		/// </summary>
		/// <param name="enabled"></param>
		private void enableButtons( bool enabled )
		{
			buttonSave.Enabled = enabled;
			buttonCancel.Enabled = enabled;

			/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

			/*
			if ( m_selectedAssetFund.IsNew )
			{
				buttonNew.Enabled = false;
			}
			else
			{
				buttonNew.Enabled = true;
			}
			*/

		}

		/// <summary>
		/// Display the asset fund
		/// </summary>
        private void displayAssetFund()
		{
			T.E();
			try
			{
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				/*
				textBoxAssetFundCode.Text = m_selectedAssetFund.AssetFundCode;
				textBoxShortName.Text = m_selectedAssetFund.ShortName;
				textBoxFullName.Text = m_selectedAssetFund.FullName;

				comboBoxFundGroup.SelectedValue = m_selectedAssetFund.FundGroupId;

				if ( m_selectedAssetFund.IsNew )
				{
					textBoxAssetFundCode.ReadOnly = false;
				}
				else
				{
					textBoxAssetFundCode.ReadOnly = true;
				}

				this.enableButtons( false );
				*/
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Do we need to save the current asset fund?
		/// </summary>
		/// <param name="selectedIndex"></param>
		private void checkSaveCurrentAssetFund( int selectedIndex )
		{
			T.E();
			try
			{
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				/*
				if ( m_selectedAssetFund != null )
				{
					if ( m_selectedAssetFund.IsDirty )
					{
						DialogResult result = MessageBox.Show( this,  "Do you wish to save your changes to the current Asset Fund" , "Save your changes" , MessageBoxButtons.YesNo , MessageBoxIcon.Question );

						if ( result == DialogResult.Yes )
						{
							this.saveAssetFund();
							m_selectedAssetFund.IsDirty = false;
						}
						else
						{
							m_selectedAssetFund.IsDirty = false;

							// If its a new one, need to remove it from the list
							if ( m_selectedAssetFund.IsNew )
							{
								m_allCompanyAssetFundsLight.Remove( m_selectedAssetFund );
								m_selectedAssetFund = null;
								this.refreshAssetFundList();
								AssetFundListBox.SelectedIndex = selectedIndex;
							}
						}
					}
				}
				*/
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Refresh the asset fund list
		/// </summary>
		private void refreshAssetFundList()
		{
			T.E();
			try
			{
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				/*
				CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[ m_allCompanyAssetFundsLight ];
				myCurrencyManager.Refresh();
				*/
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Save the current asset fund
		/// </summary>
		private void saveAssetFund()
		{
			T.E();
			try
			{
				/* all reference to asset fund light removed. 
				 * TODO - a complete new UI to replace this one
				*/

				/*
				// Validate the screen
				if ( validateScreen() )
				{
					AssetFundLightCollection originals = m_allCompanyAssetFundsLight;
					AssetFundLight original = m_selectedAssetFund;
					// Refresh the current object
					m_selectedAssetFund.AssetFundCode = textBoxAssetFundCode.Text;
					m_selectedAssetFund.ShortName = textBoxShortName.Text;
					m_selectedAssetFund.FullName = textBoxFullName.Text;

					m_selectedAssetFund.FundGroupId = (int)comboBoxFundGroup.SelectedValue;

					if ( (int)comboBoxFundGroup.SelectedValue == 0)
					{
						m_selectedAssetFund.FundGroupIDSet = false;
					}

					AssetFundController myAssetFundController = new AssetFundController();
					myAssetFundController.UpdateAssetFunds( m_connectionString , m_allCompanyAssetFundsLight );

					this.enableButtons( false );

					// Bug 186 - Remove message box
					// MessageBox.Show( "Saved" );
				}
				*/
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Is the short name valid
		/// </summary>
		/// <returns></returns>
		private bool validateShortName()
		{
			T.E();
            bool returnValue = true;
            try
            {
                errorProvider1.SetError(this.textBoxShortName, "");
                if( this.textBoxShortName.Text.Length == 0 )
                {
                    returnValue = false;
                    // Set the error if the name is not valid.
                    errorProvider1.SetError(this.textBoxShortName, "Short name is required.");
                }
            }
            finally
            {
                T.X();
            }

			return returnValue;
		}

		/// <summary>
		/// Is the long name valid
		/// </summary>
		/// <returns></returns>
		private bool validateFullName()
		{
            T.E();
			bool returnValue = true;
            
            try
            {
                errorProvider1.SetError(this.textBoxFullName, "");

                if( this.textBoxFullName.Text.Length == 0 )
                {
                    returnValue = false;
                    // Set the error if the name is not valid.
                    errorProvider1.SetError(this.textBoxFullName, "Long name is required.");
                }
            }
            finally
            {
                T.X();
            }

			return returnValue;
		}

		/// <summary>
		/// Valid the asset fund
		/// </summary>
		/// <returns></returns>
		private bool validateAssetFund()
		{
            T.E();
			bool returnValue = true;

            try
            {
                errorProvider1.SetError(this.textBoxAssetFundCode, "");

                if( this.textBoxAssetFundCode.Text.Length == 0 )
                {
                    returnValue = false;
                    // Set the error if the name is not valid.
                    errorProvider1.SetError(this.textBoxAssetFundCode, "Asset Fund code is required.");
                }
            }
            finally
            {
                T.X();
            }

			return returnValue;
		}

		/// <summary>
		/// Validate the screen
		/// </summary>
		/// <returns></returns>
		private bool validateScreen()
		{
			bool returnValue = true;

			if ( this.validateAssetFund() == false ||
				this.validateShortName() == false ||
				this.validateFullName() == false )
			{
				returnValue = false;
			}

			return returnValue;
		}

		#endregion

		#region Validation event handlers
		/// <summary>
		/// Validate form input boxes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inputBox_Validated(object sender, System.EventArgs e)
		{
            T.E();
            try
            {
                switch ( ((TextBox)sender).Name )
                {
                    case "textBoxAssetFundCode":
                    {
                        this.validateAssetFund();
                        break;
                    }
                    case "textBoxFullName":
                    {
                        this.validateFullName();
                        break;
                    }
                    case "textBoxShortName":
                    {
                        this.validateShortName();
                        break;
                    }
                }
            }
			catch(Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}					
            finally
            {
                T.X();
            }
		}

		#endregion

	}
}
