using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for CountryStaticDataEditor.
	/// </summary>
	public class CountryStaticDataEditor : StaticDataEditor
	{
		#region Member Variables

		private Country m_currentCountry = null;

		#endregion

		#region Form Stuff/Constructors/Dispose

		private TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ComboBox CurrencyComboBox;
		private System.Windows.Forms.Label CountryNameLabel;
		private System.Windows.Forms.Label CountryCodeLabel;
		private System.Windows.Forms.TextBox CountryNameTextBox;
		private System.Windows.Forms.TextBox CountryCodeTextBox;
		private System.Windows.Forms.Label CurrencyLabel;
		private System.Windows.Forms.Label StockMarketLabel;
		private System.Windows.Forms.ListBox StockMarketList;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="CountryStaticDataEditor"/> instance.
		/// </summary>
		public CountryStaticDataEditor()
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
		/// Gets or sets the current Country.
		/// </summary>
		/// <value></value>
		public Country CurrentCountry
		{
			get { return m_currentCountry; }

			set
			{
				m_currentCountry = value;
				clearErrors(this);
				displayCountryProperties();
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
				if (CurrentCountry == null)
					return authoringModes.Null;
				else if (ListManager.SelectedIsNew)
					return authoringModes.New;
				else
					return authoringModes.Editing;
			}
		}

		/// <summary>
		/// Gets the current entity description for the current Country.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentCountry.CountryName; }
		}

		#endregion Properties

		#region Overriden methods

		/// <summary>
		/// Does the delete.
		/// </summary>
		protected override void doDelete()
		{
			CurrentCountry.IsDeleted = true;
			CountryController.UpdateCountry(CurrentCountry, GlobalRegistry.ConnectionString);
		}

		/// <summary>
		/// Load the selected Country item.
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			try
			{
				StaticDataCountryLookupDecorator selectedCountry = ((StaticDataCountryLookupDecorator) ListManager.SelectedItem);
				CurrentCountry = CountryController.LoadCountry(selectedCountry.CountryCode, GlobalRegistry.ConnectionString);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a new Country object
		/// </summary>
		protected override void doNew()
		{
			Changed = true;
			CurrentCountry = new Country();
			this.CountryCodeTextBox.Focus();
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
				isValid = this.updateCountry();

				if (isValid)
				{
					try
					{
						// Persist data to database
						CountryController.UpdateCountry(this.CurrentCountry, GlobalRegistry.ConnectionString);
						
						// Refresh local Country object
						CurrentCountry.CountryCode = CurrentCountry.CountryCode;
						CurrentCountry.CurrencyCode=CurrentCountry.CurrencyCode;
						CurrentCountry.IsNew = false;
						CurrentCountry.IsDeleted = false;
						CurrentCountry.IsDirty = false;
						this.Changed = false;

						// Refresh list with modified Country details
						ListManager.ChangeSelected(StaticDataCountryLookupDecorator.ToDecoratedObject(CurrentCountry));
					}
					catch (ConstraintViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("DuplicateCountryBody", "DuplicateCountryTitle", ex);
						isValid=false;
					}
					catch (ConcurrencyViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("CountryChangedBody", "CountryUnableToSaveTitle", ex);
						isValid=false;
					}
					catch (Exception ex)
					{
						GUIExceptionHelper.LogAndDisplayException("SystemError", "CountryUnableToSaveTitle", ex);
						isValid=false;
					}
				}
				else
				{
					showErrorDialog(MessageBoxHelper.DialogText("CountryUnableToSaveTitle"));
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
		/// Displays the Country properties.
		/// </summary>
		private void displayCountryProperties()
		{
			T.E();
			try
			{
				if (authoringMode == authoringModes.Editing || authoringMode == authoringModes.New)
				{
					this.CountryCodeTextBox.Text = CurrentCountry.CountryCode;
					this.CountryNameTextBox.Text = CurrentCountry.CountryName;
					this.StockMarketList.DataSource=CountryController.LoadStockMarkets(CurrentCountry.CountryCode, GlobalRegistry.ConnectionString);
					this.StockMarketList.DisplayMember="IndexName";
					populateCurrencyDropDown();
					CurrencyComboBox.DisplayMember="DisplayText";
					CurrencyComboBox.ValueMember="CurrencyCode";
					CurrencyComboBox.SelectedValue=CurrentCountry.CurrencyCode;
					if (CurrencyComboBox.SelectedValue==null) CurrencyComboBox.SelectedIndex=0;
				}
				else
				{
					this.CountryCodeTextBox.Text = string.Empty;
					this.CountryNameTextBox.Text = string.Empty;
					this.StockMarketList.DataSource=null;
					this.CurrencyComboBox.DataSource=null;
					CurrencyComboBox.Items.Clear();
					this.StockMarketList.Items.Clear();
					
				}
			}
			finally
			{
				T.X();
			}
		}

		private void enableControls()
		{
			this.CountryCodeTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.CountryNameTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.StockMarketList.Enabled=(authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.CurrencyComboBox.Enabled=(authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		private bool updateCountry()
		{
			T.E();
			clearErrors(this);
			bool isValid = true;
			if (Changed)
			{

				if (CountryCodeTextBox.Text.Length != CountryCodeTextBox.MaxLength)
				{
					isValid=false;
					setError(CountryCodeTextBox,MessageBoxHelper.DialogText("CountryNameFieldLengthInvalid",new object[]{CountryCodeTextBox.MaxLength.ToString()} ));
				}

				if (this.CountryNameTextBox.Text == string.Empty)
				{
					isValid=false;
					setError(CountryNameTextBox,MessageBoxHelper.DialogText("CountryNameMandatory"));
				}
					
				if (this.CurrencyComboBox.SelectedIndex<1)
				{
					isValid=false;
					setError(CurrencyComboBox,MessageBoxHelper.DialogText("CountryCurrencyMandatory"));
				}

				if (isValid)
				{
					if (CurrentCountry.CountryCode != CountryCodeTextBox.Text)
					{
						CurrentCountry.CountryCode = CountryCodeTextBox.Text;
					}

					if (CurrentCountry.CountryName != CountryNameTextBox.Text)
					{
						CurrentCountry.CountryName = CountryNameTextBox.Text;
					}
					
					CurrentCountry.CurrencyCode=this.CurrencyComboBox.SelectedValue.ToString();
					CurrentCountry.IsDirty = true;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.CurrencyComboBox = new System.Windows.Forms.ComboBox();
			this.CountryNameLabel = new System.Windows.Forms.Label();
			this.CountryCodeLabel = new System.Windows.Forms.Label();
			this.CountryNameTextBox = new System.Windows.Forms.TextBox();
			this.CountryCodeTextBox = new System.Windows.Forms.TextBox();
			this.CurrencyLabel = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.StockMarketLabel = new System.Windows.Forms.Label();
			this.StockMarketList = new System.Windows.Forms.ListBox();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(0, 0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(504, 280);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.CurrencyComboBox);
			this.tabPage2.Controls.Add(this.CountryNameLabel);
			this.tabPage2.Controls.Add(this.CountryCodeLabel);
			this.tabPage2.Controls.Add(this.CountryNameTextBox);
			this.tabPage2.Controls.Add(this.CountryCodeTextBox);
			this.tabPage2.Controls.Add(this.CurrencyLabel);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(496, 254);
			this.tabPage2.TabIndex = 0;
			this.tabPage2.Text = "Properties";
			// 
			// CurrencyComboBox
			// 
			this.CurrencyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurrencyComboBox.Location = new System.Drawing.Point(120, 72);
			this.CurrencyComboBox.Name = "CurrencyComboBox";
			this.CurrencyComboBox.Size = new System.Drawing.Size(296, 21);
			this.CurrencyComboBox.TabIndex = 7;
			this.CurrencyComboBox.SelectedIndexChanged += new System.EventHandler(this.CurrencyComboBox_SelectedIndexChanged);
			// 
			// CountryNameLabel
			// 
			this.CountryNameLabel.Location = new System.Drawing.Point(8, 40);
			this.CountryNameLabel.Name = "CountryNameLabel";
			this.CountryNameLabel.Size = new System.Drawing.Size(104, 23);
			this.CountryNameLabel.TabIndex = 9;
			this.CountryNameLabel.Text = "Country Name:";
			this.CountryNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CountryCodeLabel
			// 
			this.CountryCodeLabel.Location = new System.Drawing.Point(8, 8);
			this.CountryCodeLabel.Name = "CountryCodeLabel";
			this.CountryCodeLabel.Size = new System.Drawing.Size(104, 23);
			this.CountryCodeLabel.TabIndex = 6;
			this.CountryCodeLabel.Text = "Country Code:";
			this.CountryCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CountryNameTextBox
			// 
			this.CountryNameTextBox.Location = new System.Drawing.Point(120, 40);
			this.CountryNameTextBox.MaxLength = 50;
			this.CountryNameTextBox.Name = "CountryNameTextBox";
			this.CountryNameTextBox.Size = new System.Drawing.Size(296, 20);
			this.CountryNameTextBox.TabIndex = 5;
			this.CountryNameTextBox.Text = "";
			this.CountryNameTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// CountryCodeTextBox
			// 
			this.CountryCodeTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.CountryCodeTextBox.Location = new System.Drawing.Point(120, 8);
			this.CountryCodeTextBox.MaxLength = 3;
			this.CountryCodeTextBox.Name = "CountryCodeTextBox";
			this.CountryCodeTextBox.TabIndex = 4;
			this.CountryCodeTextBox.Text = "";
			this.CountryCodeTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// CurrencyLabel
			// 
			this.CurrencyLabel.Location = new System.Drawing.Point(8, 72);
			this.CurrencyLabel.Name = "CurrencyLabel";
			this.CurrencyLabel.Size = new System.Drawing.Size(96, 23);
			this.CurrencyLabel.TabIndex = 8;
			this.CurrencyLabel.Text = "Currency:";
			this.CurrencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.StockMarketLabel);
			this.tabPage3.Controls.Add(this.StockMarketList);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(496, 254);
			this.tabPage3.TabIndex = 1;
			this.tabPage3.Text = "Markets";
			// 
			// StockMarketLabel
			// 
			this.StockMarketLabel.AutoSize = true;
			this.StockMarketLabel.Location = new System.Drawing.Point(16, 8);
			this.StockMarketLabel.Name = "StockMarketLabel";
			this.StockMarketLabel.Size = new System.Drawing.Size(271, 16);
			this.StockMarketLabel.TabIndex = 1;
			this.StockMarketLabel.Text = "Below are the Stock Markets related to this Currency:";
			// 
			// StockMarketList
			// 
			this.StockMarketList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.StockMarketList.Location = new System.Drawing.Point(16, 32);
			this.StockMarketList.Name = "StockMarketList";
			this.StockMarketList.Size = new System.Drawing.Size(464, 199);
			this.StockMarketList.TabIndex = 0;
			// 
			// CountryStaticDataEditor
			// 
			this.Controls.Add(this.tabControl1);
			this.Name = "CountryStaticDataEditor";
			this.Size = new System.Drawing.Size(504, 280);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private IList m_Currencies;

		private void populateCurrencyDropDown()
		{
			if (m_Currencies==null)
			{
				CurrencyComboBox.DataSource=null;
				CurrencyComboBox.Items.Clear();
				LookupController lookupController=new LookupController();
				m_Currencies=lookupController.LoadCurrencies(GlobalRegistry.ConnectionString);
				IList decorectedCurrencies=StaticDataCurrencyLookupDecorator.ToDecoratedList(m_Currencies);
				StaticDataCurrencyLookupDecorator nullItem = new StaticDataCurrencyLookupDecorator( "Please Select","0");
				decorectedCurrencies.Insert(0, nullItem);
				CurrencyComboBox.DataSource= decorectedCurrencies;
			}
		}

		private void CurrencyComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Changed=true;
		}

		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get
			{
				return "Country";
			}
		}


		
	}
}