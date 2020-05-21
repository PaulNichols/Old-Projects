using System;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for CurrencyStaticDataEditor.
	/// </summary>
	public class CurrencyStaticDataEditor : StaticDataEditor
	{
		#region Member Variables

		private Currency m_currentCurrency = null;

		#endregion

		#region Form Stuff/Constructors/Dispose

		private TextBox CurrencyCodeTextBox;
		private TextBox CurrencyNameTextBox;
		private Label CurrencyCodeLabel;
		private Label CurrencyNameLabel;
		private TabPage tabPage1;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="CurrencyStaticDataEditor"/> instance.
		/// </summary>
		public CurrencyStaticDataEditor()
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
		/// Gets or sets the current Currency.
		/// </summary>
		/// <value></value>
		public Currency CurrentCurrency
		{
			get { return m_currentCurrency; }

			set
			{
				//	bool indexChanged = m_currentCurrency == null || m_currentCurrency.CurrencyCode != value.CurrencyCode;
				clearErrors(this);
				m_currentCurrency = value;
				displayCurrencyProperties();
				enableControls();

				//				if (indexChanged)
				//				{
				//				//	this.Changed = false;
				//				}
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
				if (CurrentCurrency == null)
					return authoringModes.Null;
				else if (ListManager.SelectedIsNew)
					return authoringModes.New;
				else
					return authoringModes.Editing;
			}
		}

		/// <summary>
		/// Gets the current entity description for the current Currency.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentCurrency.CurrencyName; }
		}

		#endregion Properties

		#region Overriden methods

		/// <summary>
		/// Does the delete.
		/// </summary>
		protected override void doDelete()
		{
			CurrentCurrency.IsDeleted = true;
			CurrencyController.UpdateCurrency(CurrentCurrency, GlobalRegistry.ConnectionString);
		}

		/// <summary>
		/// Load the selected Currency item.
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			try
			{
				StaticDataCurrencyLookupDecorator selectedCurrency = ((StaticDataCurrencyLookupDecorator) ListManager.SelectedItem);
				CurrentCurrency = CurrencyController.LoadCurrency(selectedCurrency.CurrencyCode, GlobalRegistry.ConnectionString);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a new Currency object
		/// </summary>
		protected override void doNew()
		{
			Changed = true;
			CurrentCurrency = new Currency();
			this.CurrencyCodeTextBox.Focus();
		}

		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get
			{
				return "Currency";
			}
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
				isValid = this.updateCurrency();

				if (isValid)
				{
					try
					{
						// Persist data to database
						CurrencyController.UpdateCurrency(this.CurrentCurrency, GlobalRegistry.ConnectionString);
						
						// Refresh local Currency object
						CurrentCurrency.CurrentCurrencyCode = CurrentCurrency.CurrencyCode;
						CurrentCurrency.IsNew = false;
						CurrentCurrency.IsDeleted = false;
						CurrentCurrency.IsDirty = false;
						this.Changed = false;

						// Refresh list with modified Currency details
						ListManager.ChangeSelected(StaticDataCurrencyLookupDecorator.ToDecoratedObject(CurrentCurrency));
					}			
					catch (ConstraintViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("DuplicateCurrencyBody", "DuplicateCurrencyTitle", ex);
						isValid=false;
					}
					catch (ConcurrencyViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("CurrencyChangedBody", "CurrencyUnableToSaveTitle", ex);
						isValid=false;
					}
					catch (Exception ex)
					{
						GUIExceptionHelper.LogAndDisplayException("SystemError", "CurrencyUnableToSaveTitle", ex);
						isValid=false;
					}
				}
				else
				{
					showErrorDialog(MessageBoxHelper.DialogText("CurrencyUnableToSaveTitle"));
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
		/// Displays the Currency properties.
		/// </summary>
		private void displayCurrencyProperties()
		{
			T.E();
			try
			{
				if (authoringMode == authoringModes.Editing || authoringMode == authoringModes.New)
				{
					this.CurrencyCodeTextBox.Text = CurrentCurrency.CurrencyCode;
					this.CurrencyNameTextBox.Text = CurrentCurrency.CurrencyName;
				}
				else
				{
					this.CurrencyCodeTextBox.Text = string.Empty;
					this.CurrencyNameTextBox.Text = string.Empty;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void enableControls()
		{
			this.CurrencyCodeTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.CurrencyNameTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		private bool updateCurrency()
		{
			T.E();
			clearErrors(this);
			bool isValid = true;
			if (Changed)
			{

				if (CurrencyCodeTextBox.Text.Length != CurrencyCodeTextBox.MaxLength)
				{
					isValid=false;
					setError(CurrencyCodeTextBox,MessageBoxHelper.DialogText("CurrencyNameFieldLengthInvalid",new object[]{CurrencyCodeTextBox.MaxLength.ToString()} ));
				}

				if (this.CurrencyNameTextBox.Text == string.Empty)
				{
					isValid=false;
					setError(CurrencyNameTextBox,MessageBoxHelper.DialogText("CurrencyNameMandatory"));
				}

				if (isValid)
				{
					if (CurrentCurrency.CurrencyCode != CurrencyCodeTextBox.Text)
					{
						CurrentCurrency.CurrencyCode = CurrencyCodeTextBox.Text;
					}

					if (CurrentCurrency.CurrencyName != CurrencyNameTextBox.Text)
					{
						CurrentCurrency.CurrencyName = CurrencyNameTextBox.Text;
					}
					
					CurrentCurrency.IsDirty = true;
				}
				else
				{
					//	CurrentCurrency = m_currentCurrency;
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
			this.CurrencyCodeTextBox = new System.Windows.Forms.TextBox();
			this.CurrencyNameTextBox = new System.Windows.Forms.TextBox();
			this.CurrencyCodeLabel = new System.Windows.Forms.Label();
			this.CurrencyNameLabel = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.SuspendLayout();
			// 
			// CurrencyCodeTextBox
			// 
			this.CurrencyCodeTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.CurrencyCodeTextBox.Location = new System.Drawing.Point(128, 8);
			this.CurrencyCodeTextBox.MaxLength = 3;
			this.CurrencyCodeTextBox.Name = "CurrencyCodeTextBox";
			this.CurrencyCodeTextBox.TabIndex = 0;
			this.CurrencyCodeTextBox.Text = "";
			this.CurrencyCodeTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// CurrencyNameTextBox
			// 
			this.CurrencyNameTextBox.Location = new System.Drawing.Point(128, 40);
			this.CurrencyNameTextBox.MaxLength = 150;
			this.CurrencyNameTextBox.Name = "CurrencyNameTextBox";
			this.CurrencyNameTextBox.Size = new System.Drawing.Size(296, 22);
			this.CurrencyNameTextBox.TabIndex = 1;
			this.CurrencyNameTextBox.Text = "";
			this.CurrencyNameTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// CurrencyCodeLabel
			// 
			this.CurrencyCodeLabel.Location = new System.Drawing.Point(16, 7);
			this.CurrencyCodeLabel.Name = "CurrencyCodeLabel";
			this.CurrencyCodeLabel.Size = new System.Drawing.Size(104, 23);
			this.CurrencyCodeLabel.TabIndex = 2;
			this.CurrencyCodeLabel.Text = "Currency Code:";
			this.CurrencyCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CurrencyNameLabel
			// 
			this.CurrencyNameLabel.Location = new System.Drawing.Point(16, 39);
			this.CurrencyNameLabel.Name = "CurrencyNameLabel";
			this.CurrencyNameLabel.Size = new System.Drawing.Size(104, 23);
			this.CurrencyNameLabel.TabIndex = 3;
			this.CurrencyNameLabel.Text = "Currency Name:";
			this.CurrencyNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(0, 0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.TabIndex = 0;
			// 
			// CurrencyStaticDataEditor
			// 
			this.Controls.Add(this.CurrencyNameLabel);
			this.Controls.Add(this.CurrencyCodeLabel);
			this.Controls.Add(this.CurrencyNameTextBox);
			this.Controls.Add(this.CurrencyCodeTextBox);
			this.Name = "CurrencyStaticDataEditor";
			this.Size = new System.Drawing.Size(432, 88);
			this.ResumeLayout(false);

		}

		#endregion
	}
}