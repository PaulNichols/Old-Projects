using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for StockMarketStaticDataEditor.
	/// </summary>
	public class StockMarketStaticDataEditor : StaticDataEditor
	{
		#region Member Variables

		private StockMarket m_currentStockMarket = null;

		#endregion

		#region Form Stuff/Constructors/Dispose

		private TabPage tabPage1;
		private System.Windows.Forms.Label StockMarketNameLabel;
		private System.Windows.Forms.TextBox StockMarketNameTextBox;
		private System.Windows.Forms.Label CountryLabel;
		private System.Windows.Forms.CheckBox GlobalCheckBox;
		private System.Windows.Forms.ComboBox CountryComboBox;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="StockMarketStaticDataEditor"/> instance.
		/// </summary>
		public StockMarketStaticDataEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#endregion

		#region Events

		/// <summary>
		/// Default text changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void default_TextChanged(object sender, EventArgs e)
		{
			Changed = true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the current StockMarket.
		/// </summary>
		/// <value></value>
		public StockMarket CurrentStockMarket
		{
			get { return m_currentStockMarket; }

			set
			{
				m_currentStockMarket = value;
				clearErrors(this);
				displayStockMarketProperties();
				enableControls();
			}
		}

		/// <summary>
		/// Gets the authoring mode.
		/// </summary>
		/// <value></value>
		private authoringModes authoringMode
		{
			get
			{
				if (CurrentStockMarket == null)
					return authoringModes.Null;
				else if (ListManager.SelectedIsNew)
					return authoringModes.New;
				else
					return authoringModes.Editing;
			}
		}

		/// <summary>
		/// Gets the current entity description for the current StockMarket.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentStockMarket.IndexName; }
		}

		#endregion Properties

		#region Overriden methods

		/// <summary>
		/// Does the delete.
		/// </summary>
		protected override void doDelete()
		{
			CurrentStockMarket.IsDeleted = true;
			StockMarketController.UpdateStockMarket(CurrentStockMarket, GlobalRegistry.ConnectionString);
		}

		/// <summary>
		/// Load the selected StockMarket item.
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			try
			{
				StaticDataStockMarketLookupDecorator selectedStockMarket = ((StaticDataStockMarketLookupDecorator) ListManager.SelectedItem);
				CurrentStockMarket = StockMarketController.LoadStockMarket(selectedStockMarket.MarketId, GlobalRegistry.ConnectionString);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a new StockMarket object
		/// </summary>
		protected override void doNew()
		{
			Changed = true;
			CurrentStockMarket = new StockMarket();
			this.StockMarketNameTextBox.Focus();
		}

		/// <summary>
		/// Does the save.
		/// </summary>
		/// <returns></returns>
		protected override bool doSave()
		{
			T.E();

			bool isValid = true;
			try
			{
				isValid = this.updateStockMarket();

				if (isValid)
				{
					try
					{
						// Persist data to database
						StockMarketController.UpdateStockMarket(this.CurrentStockMarket, GlobalRegistry.ConnectionString);
						
						// Refresh local StockMarket object
						CurrentStockMarket.MarketIndexID = CurrentStockMarket.MarketIndexID;
						CurrentStockMarket.Global = CurrentStockMarket.Global;
						CurrentStockMarket.CountryCode=CurrentStockMarket.CountryCode;
						CurrentStockMarket.IsNew = false;
						CurrentStockMarket.IsDeleted = false;
						CurrentStockMarket.IsDirty = false;
						this.Changed = false;

						// Refresh list with modified StockMarket details
						ListManager.ChangeSelected(StaticDataStockMarketLookupDecorator.ToDecoratedObject(CurrentStockMarket));
					}
					catch (ConstraintViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("DuplicateStockMarketBody", "DuplicateStockMarketTitle", ex);
						isValid=false;
					}
					catch (ConcurrencyViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("StockMarketChangedBody", "StockMarketUnableToSaveTitle", ex);
						isValid=false;
					}
					catch (Exception ex)
					{
						GUIExceptionHelper.LogAndDisplayException("SystemError", "StockMarketUnableToSaveTitle", ex);
						isValid=false;
					}
				}
				else
				{
					showErrorDialog(MessageBoxHelper.DialogText("StockMarketUnableToSaveTitle"));
				}
			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Displays the StockMarket properties.
		/// </summary>
		private void displayStockMarketProperties()
		{
			T.E();
			try
			{
				if (authoringMode == authoringModes.Editing || authoringMode == authoringModes.New)
				{
					this.StockMarketNameTextBox.Text = CurrentStockMarket.IndexName;
					populateCountryDropDown();
					CountryComboBox.DisplayMember="DisplayText";
					CountryComboBox.ValueMember="CountryCode";
					CountryComboBox.SelectedValue=CurrentStockMarket.CountryCode;
					if (CountryComboBox.SelectedValue==null) CountryComboBox.SelectedIndex=0;

					this.GlobalCheckBox.Checked=CurrentStockMarket.Global;
				}
				else
				{
					this.StockMarketNameTextBox.Text = string.Empty;
					this.GlobalCheckBox.Checked=false;
					this.CountryComboBox.DataSource=null;
					CountryComboBox.Items.Clear();
					
				}
			}
			finally
			{
				T.X();
			}
		}

		private void enableControls()
		{
			this.StockMarketNameTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.GlobalCheckBox.Enabled= (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			CountryComboBox.Enabled=!GlobalCheckBox.Checked && (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);;
		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		private bool updateStockMarket()
		{
			T.E();
			clearErrors(this);
			bool isValid = true;
			if (Changed)
			{
				if (this.StockMarketNameTextBox.Text == string.Empty)
				{
					isValid=false;
					setError(StockMarketNameTextBox,MessageBoxHelper.DialogText("StockMarketNameMandatory"));
				}

				if (this.CountryComboBox.SelectedIndex<1)
				{
					isValid=false;
					setError(CountryComboBox,MessageBoxHelper.DialogText("StockMarketCountryMandatory"));
				}


				if (isValid)
				{
					if (CurrentStockMarket.IndexName != StockMarketNameTextBox.Text)
					{
						CurrentStockMarket.IndexName = StockMarketNameTextBox.Text;
					}
					
					CurrentStockMarket.Global=this.GlobalCheckBox.Checked;
					if(!CurrentStockMarket.Global)
					{
						CurrentStockMarket.CountryCode=this.CountryComboBox.SelectedValue.ToString();
					}
					else
					{
						CurrentStockMarket.CountryCode=null;
					}
					CurrentStockMarket.IsDirty = true;
				}

				Changed = !isValid;
			}
			T.X();
			return isValid;
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.StockMarketNameLabel = new System.Windows.Forms.Label();
			this.StockMarketNameTextBox = new System.Windows.Forms.TextBox();
			this.CountryLabel = new System.Windows.Forms.Label();
			this.GlobalCheckBox = new System.Windows.Forms.CheckBox();
			this.CountryComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(0, 0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.TabIndex = 0;
			// 
			// StockMarketNameLabel
			// 
			this.StockMarketNameLabel.Location = new System.Drawing.Point(8, 8);
			this.StockMarketNameLabel.Name = "StockMarketNameLabel";
			this.StockMarketNameLabel.Size = new System.Drawing.Size(104, 23);
			this.StockMarketNameLabel.TabIndex = 13;
			this.StockMarketNameLabel.Text = "Market Name:";
			this.StockMarketNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StockMarketNameTextBox
			// 
			this.StockMarketNameTextBox.Location = new System.Drawing.Point(120, 8);
			this.StockMarketNameTextBox.MaxLength = 50;
			this.StockMarketNameTextBox.Name = "StockMarketNameTextBox";
			this.StockMarketNameTextBox.Size = new System.Drawing.Size(296, 22);
			this.StockMarketNameTextBox.TabIndex = 10;
			this.StockMarketNameTextBox.Text = "";
			this.StockMarketNameTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// CountryLabel
			// 
			this.CountryLabel.Location = new System.Drawing.Point(8, 40);
			this.CountryLabel.Name = "CountryLabel";
			this.CountryLabel.Size = new System.Drawing.Size(96, 23);
			this.CountryLabel.TabIndex = 12;
			this.CountryLabel.Text = "Country:";
			this.CountryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GlobalCheckBox
			// 
			this.GlobalCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.GlobalCheckBox.Location = new System.Drawing.Point(8, 72);
			this.GlobalCheckBox.Name = "GlobalCheckBox";
			this.GlobalCheckBox.Size = new System.Drawing.Size(126, 24);
			this.GlobalCheckBox.TabIndex = 14;
			this.GlobalCheckBox.Text = "Global:";
			this.GlobalCheckBox.CheckedChanged += new System.EventHandler(this.GlobalCheckBox_CheckedChanged);
			// 
			// CountryComboBox
			// 
			this.CountryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CountryComboBox.Location = new System.Drawing.Point(120, 40);
			this.CountryComboBox.Name = "CountryComboBox";
			this.CountryComboBox.Size = new System.Drawing.Size(296, 24);
			this.CountryComboBox.TabIndex = 15;
			this.CountryComboBox.SelectedIndexChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// StockMarketStaticDataEditor
			// 
			this.Controls.Add(this.CountryComboBox);
			this.Controls.Add(this.GlobalCheckBox);
			this.Controls.Add(this.StockMarketNameLabel);
			this.Controls.Add(this.StockMarketNameTextBox);
			this.Controls.Add(this.CountryLabel);
			this.Name = "StockMarketStaticDataEditor";
			this.Size = new System.Drawing.Size(504, 280);
			this.ResumeLayout(false);

		}

		#endregion

//		private void populateCurrencyDropDown()
//		{
//			if (m_Currencies==null)
//			{
//				CurrencyComboBox.DataSource=null;
//				CurrencyComboBox.Items.Clear();
//				m_Currencies=LookupController.LoadCurrencies(GlobalRegistry.ConnectionString);
//				IList decorectedCurrencies=StaticDataCurrencyLookupDecorator.ToDecoratedList(m_Currencies);
//				StaticDataCurrencyLookupDecorator nullItem = new StaticDataCurrencyLookupDecorator( "Please Select","0");
//				decorectedCurrencies.Insert(0, nullItem);
//				CurrencyComboBox.DataSource= decorectedCurrencies;
//			}
//		}
		private IList m_countries;

		private void populateCountryDropDown()
		{

			if (m_countries==null)
			{
				CountryComboBox.DataSource=null;
				CountryComboBox.Items.Clear();
				LookupController lookupController=new LookupController();
				m_countries=lookupController.LoadCountries(GlobalRegistry.ConnectionString);
				IList decorectedCountries=StaticDataCountryLookupDecorator.ToDecoratedList(m_countries);
				StaticDataCountryLookupDecorator nullItem = new StaticDataCountryLookupDecorator( "Please Select","0");
				decorectedCountries.Insert(0, nullItem);
				CountryComboBox.DataSource= decorectedCountries;
			}
		}

		private void GlobalCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			Changed=true;
			this.CountryComboBox.Enabled=!this.GlobalCheckBox.Checked;
		}


		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get
			{
				return "StockMarket";
			}
		}


		
	}
}