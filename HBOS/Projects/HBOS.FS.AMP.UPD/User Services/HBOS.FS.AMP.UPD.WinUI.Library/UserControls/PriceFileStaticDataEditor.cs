using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for PriceFileStaticDataEditor.
	/// </summary>
	public class PriceFileStaticDataEditor : StaticDataEditor
	{
		#region Member Variables

		private PriceFile m_currentPriceFile = null;

		#endregion

		#region Form Stuff/Constructors/Dispose

		private TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ComboBox ExtensionComboBox;
		private System.Windows.Forms.Label AssetFundsLabel;
		private System.Windows.Forms.ListBox AssetFundList;
		private System.Windows.Forms.Label FileNameLabel;
		private System.Windows.Forms.TextBox FileNameTextBox;
		private System.Windows.Forms.Label FileDescriptionLabel;
		private System.Windows.Forms.TextBox DescriptionTextBox;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="PriceFileStaticDataEditor"/> instance.
		/// </summary>
		public PriceFileStaticDataEditor()
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
		/// Gets or sets the current Price File.
		/// </summary>
		/// <value></value>
		public PriceFile CurrentPriceFile
		{
			get { return m_currentPriceFile; }

			set
			{
				m_currentPriceFile = value;
				clearErrors(this);
				displayPriceFileProperties();
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
				if (CurrentPriceFile == null)
					return authoringModes.Null;
				else if (ListManager.SelectedIsNew)
					return authoringModes.New;
				else
					return authoringModes.Editing;
			}
		}

		/// <summary>
		/// Gets the current entity description for the current PriceFile.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentPriceFile.FileName; }
		}

		#endregion Properties

		#region Overriden methods

		/// <summary>
		/// Does the delete.
		/// </summary>
		protected override void doDelete()
		{
			CurrentPriceFile.IsDeleted = true;
			PriceFileController.UpdatePriceFile(CurrentPriceFile, GlobalRegistry.ConnectionString);
		}

		/// <summary>
		/// Load the selected PriceFile item.
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			try
			{
				StaticDataPriceFileLookupDecorator selectedPriceFile = ((StaticDataPriceFileLookupDecorator) ListManager.SelectedItem);
				CurrentPriceFile = PriceFileController.LoadPriceFile(selectedPriceFile.PriceFile.FileId, GlobalRegistry.ConnectionString);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a new PriceFile object
		/// </summary>
		protected override void doNew()
		{
			Changed = true;
			CurrentPriceFile = new PriceFile();
			this.FileNameTextBox.Focus();
			ExtensionComboBox.SelectedIndex=-1;
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
				isValid = this.updatePriceFile();

				if (isValid)
				{
					try
					{
						// Persist data to database
						PriceFileController.UpdatePriceFile(this.CurrentPriceFile, GlobalRegistry.ConnectionString);
						
						this.Changed = false;

						// Refresh list with modified PriceFile details
						ListManager.ChangeSelected(StaticDataPriceFileLookupDecorator.ToDecoratedObject(CurrentPriceFile));
					}
					catch (ConstraintViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("DuplicatePriceFileBody", "DuplicatePriceFileTitle", ex);
					}
					catch (ConcurrencyViolationException ex)
					{
						GUIExceptionHelper.LogAndDisplayException("PriceFileChangedBody", "PriceFileUnableToSaveTitle", ex);
					}
					catch (Exception ex)
					{
						GUIExceptionHelper.LogAndDisplayException("SystemError", "PriceFileUnableToSaveTitle", ex);
					}
				}
				else
				{
					showErrorDialog(MessageBoxHelper.DialogText("PriceFileUnableToSaveTitle"));
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
		/// Displays the PriceFile properties.
		/// </summary>
		private void displayPriceFileProperties()
		{
			T.E();
			try
			{
				if (authoringMode == authoringModes.Editing || authoringMode == authoringModes.New)
				{
					this.FileNameTextBox.Text = CurrentPriceFile.FileName;
					this.DescriptionTextBox.Text = CurrentPriceFile.FileDescription;
					ExtensionComboBox.SelectedItem=CurrentPriceFile.Extension;
					this.AssetFundList.DataSource=PriceFileController.LoadAssetFunds(CurrentPriceFile.FileId, GlobalRegistry.ConnectionString);
					this.AssetFundList.DisplayMember="DisplayValue";
				}
				else
				{
					this.FileNameTextBox.Text = string.Empty;
					this.DescriptionTextBox.Text = string.Empty;
					this.AssetFundList.DataSource=null;
					this.AssetFundList.Items.Clear();
				}
			}
			finally
			{
				T.X();
			}
		}

		private void enableControls()
		{
			this.FileNameTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.DescriptionTextBox.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.AssetFundList.Enabled=(authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			this.ExtensionComboBox.Enabled=(authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		private bool updatePriceFile()
		{
			T.E();
			clearErrors(this);
			bool isValid = true;
			if (Changed)
			{

				if (FileNameTextBox.Text =="" )
				{
					isValid=false;
					setError(FileNameTextBox,MessageBoxHelper.DialogText("PriceFileNameFieldLengthInvalid",new object[]{FileNameTextBox.MaxLength.ToString()} ));
				}

				if (this.ExtensionComboBox.SelectedIndex< 1)
				{
					isValid=false;
					setError(ExtensionComboBox,MessageBoxHelper.DialogText("PriceFileExtensionMandatory"));
				}

				if (isValid)
				{
					if (CurrentPriceFile.FileName != FileNameTextBox.Text)
					{
						CurrentPriceFile.FileName = FileNameTextBox.Text;
					}

					if (CurrentPriceFile.FileDescription != DescriptionTextBox.Text)
					{
						CurrentPriceFile.FileDescription = DescriptionTextBox.Text;
					}
					
					CurrentPriceFile.Extension=this.ExtensionComboBox.SelectedItem.ToString();
					CurrentPriceFile.CompanyCode=GlobalRegistry.CompanyCode;
					CurrentPriceFile.IsDirty = true;
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
			this.FileDescriptionLabel = new System.Windows.Forms.Label();
			this.FileNameLabel = new System.Windows.Forms.Label();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.FileNameTextBox = new System.Windows.Forms.TextBox();
			this.ExtensionComboBox = new System.Windows.Forms.ComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.AssetFundsLabel = new System.Windows.Forms.Label();
			this.AssetFundList = new System.Windows.Forms.ListBox();
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
			this.tabPage2.Controls.Add(this.FileDescriptionLabel);
			this.tabPage2.Controls.Add(this.FileNameLabel);
			this.tabPage2.Controls.Add(this.DescriptionTextBox);
			this.tabPage2.Controls.Add(this.FileNameTextBox);
			this.tabPage2.Controls.Add(this.ExtensionComboBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(496, 254);
			this.tabPage2.TabIndex = 0;
			this.tabPage2.Text = "Properties";
			// 
			// FileDescriptionLabel
			// 
			this.FileDescriptionLabel.Location = new System.Drawing.Point(8, 40);
			this.FileDescriptionLabel.Name = "FileDescriptionLabel";
			this.FileDescriptionLabel.Size = new System.Drawing.Size(88, 23);
			this.FileDescriptionLabel.TabIndex = 9;
			this.FileDescriptionLabel.Text = "Description:";
			this.FileDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FileNameLabel
			// 
			this.FileNameLabel.Location = new System.Drawing.Point(8, 8);
			this.FileNameLabel.Name = "FileNameLabel";
			this.FileNameLabel.Size = new System.Drawing.Size(88, 23);
			this.FileNameLabel.TabIndex = 6;
			this.FileNameLabel.Text = "File Name:";
			this.FileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DescriptionTextBox
			// 
			this.DescriptionTextBox.Location = new System.Drawing.Point(104, 40);
			this.DescriptionTextBox.MaxLength = 200;
			this.DescriptionTextBox.Multiline = true;
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.Size = new System.Drawing.Size(296, 88);
			this.DescriptionTextBox.TabIndex = 5;
			this.DescriptionTextBox.Text = "";
			this.DescriptionTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// FileNameTextBox
			// 
			this.FileNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.FileNameTextBox.Location = new System.Drawing.Point(104, 8);
			this.FileNameTextBox.MaxLength = 50;
			this.FileNameTextBox.Name = "FileNameTextBox";
			this.FileNameTextBox.Size = new System.Drawing.Size(296, 20);
			this.FileNameTextBox.TabIndex = 4;
			this.FileNameTextBox.Text = "";
			this.FileNameTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// ExtensionComboBox
			// 
			this.ExtensionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ExtensionComboBox.Items.AddRange(new object[] {
																   "",
																   "prc",
																   "pri",
																   "pro"});
			this.ExtensionComboBox.Location = new System.Drawing.Point(424, 8);
			this.ExtensionComboBox.Name = "ExtensionComboBox";
			this.ExtensionComboBox.Size = new System.Drawing.Size(56, 21);
			this.ExtensionComboBox.TabIndex = 0;
			this.ExtensionComboBox.SelectedIndexChanged += new System.EventHandler(this.ExtensionComboBox_SelectedIndexChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.AssetFundsLabel);
			this.tabPage3.Controls.Add(this.AssetFundList);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(496, 254);
			this.tabPage3.TabIndex = 1;
			this.tabPage3.Text = "Asset Funds";
			// 
			// AssetFundsLabel
			// 
			this.AssetFundsLabel.AutoSize = true;
			this.AssetFundsLabel.Location = new System.Drawing.Point(16, 8);
			this.AssetFundsLabel.Name = "AssetFundsLabel";
			this.AssetFundsLabel.Size = new System.Drawing.Size(263, 16);
			this.AssetFundsLabel.TabIndex = 1;
			this.AssetFundsLabel.Text = "Below are the Asset Funds related to this Price File:";
			// 
			// AssetFundList
			// 
			this.AssetFundList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.AssetFundList.Location = new System.Drawing.Point(16, 32);
			this.AssetFundList.Name = "AssetFundList";
			this.AssetFundList.Size = new System.Drawing.Size(464, 199);
			this.AssetFundList.TabIndex = 0;
			// 
			// PriceFileStaticDataEditor
			// 
			this.Controls.Add(this.tabControl1);
			this.Name = "PriceFileStaticDataEditor";
			this.Size = new System.Drawing.Size(504, 280);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void ExtensionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
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
				return "Price File";
			}
		}
	}
}