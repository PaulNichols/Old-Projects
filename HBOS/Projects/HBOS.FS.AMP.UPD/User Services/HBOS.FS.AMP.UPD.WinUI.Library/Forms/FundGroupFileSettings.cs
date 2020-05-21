using System;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Summary description for FundGroupFileSettings.
	/// </summary>
	public class FundGroupFileSettings : Form
	{
		private TextBox textBoxNumberOfSigDPs;
		private Label labelDP;
		private TextBox textBoxNumberOfDPs;
		private Label labelFundGroupNumber;
		private TextBox textBoxFundGroupNumber;
		private CheckBox checkBoxMajorDenomination;
		private Label labelMajorDenomination;
		private Button buttonSave;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private Label labelSigDP;
		private Label labelInstructions;
		private ErrorProvider errorProvider1;

		private FundGroupDistributionFile m_file;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="file"></param>
		public FundGroupFileSettings(ref FundGroupDistributionFile file)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			setFilePropertiesEnabledState(file);
			populateFilePropertyControls(file);

			//set the caption of this form
			labelInstructions.Text = String.Format(labelInstructions.Text, file.FileDescription);

			m_file = file;

			buttonSave.Enabled=file.FundGroupNumberRequired || file.MajorDenimonationRequired ||
								file.DecimalPlacesRequired;
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelSigDP = new System.Windows.Forms.Label();
			this.textBoxNumberOfSigDPs = new System.Windows.Forms.TextBox();
			this.labelDP = new System.Windows.Forms.Label();
			this.textBoxNumberOfDPs = new System.Windows.Forms.TextBox();
			this.labelFundGroupNumber = new System.Windows.Forms.Label();
			this.textBoxFundGroupNumber = new System.Windows.Forms.TextBox();
			this.checkBoxMajorDenomination = new System.Windows.Forms.CheckBox();
			this.labelMajorDenomination = new System.Windows.Forms.Label();
			this.labelInstructions = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// labelSigDP
			// 
			this.labelSigDP.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelSigDP.Location = new System.Drawing.Point(24, 200);
			this.labelSigDP.Name = "labelSigDP";
			this.labelSigDP.Size = new System.Drawing.Size(152, 41);
			this.labelSigDP.TabIndex = 24;
			this.labelSigDP.Text = "Number Of Significant Decimal Places:";
			this.labelSigDP.Visible = false;
			// 
			// textBoxNumberOfSigDPs
			// 
			this.textBoxNumberOfSigDPs.Enabled = false;
			this.textBoxNumberOfSigDPs.Location = new System.Drawing.Point(192, 200);
			this.textBoxNumberOfSigDPs.MaxLength = 10;
			this.textBoxNumberOfSigDPs.Name = "textBoxNumberOfSigDPs";
			this.textBoxNumberOfSigDPs.Size = new System.Drawing.Size(40, 22);
			this.textBoxNumberOfSigDPs.TabIndex = 23;
			this.textBoxNumberOfSigDPs.Text = "";
			this.textBoxNumberOfSigDPs.Visible = false;
			// 
			// labelDP
			// 
			this.labelDP.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelDP.Location = new System.Drawing.Point(24, 144);
			this.labelDP.Name = "labelDP";
			this.labelDP.Size = new System.Drawing.Size(152, 38);
			this.labelDP.TabIndex = 22;
			this.labelDP.Text = "Number of Decimal Places:";
			// 
			// textBoxNumberOfDPs
			// 
			this.textBoxNumberOfDPs.Enabled = false;
			this.textBoxNumberOfDPs.Location = new System.Drawing.Point(192, 144);
			this.textBoxNumberOfDPs.MaxLength = 10;
			this.textBoxNumberOfDPs.Name = "textBoxNumberOfDPs";
			this.textBoxNumberOfDPs.Size = new System.Drawing.Size(40, 22);
			this.textBoxNumberOfDPs.TabIndex = 21;
			this.textBoxNumberOfDPs.Text = "";
			// 
			// labelFundGroupNumber
			// 
			this.labelFundGroupNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelFundGroupNumber.Location = new System.Drawing.Point(24, 72);
			this.labelFundGroupNumber.Name = "labelFundGroupNumber";
			this.labelFundGroupNumber.Size = new System.Drawing.Size(131, 18);
			this.labelFundGroupNumber.TabIndex = 19;
			this.labelFundGroupNumber.Text = "Fund Group Number:";
			// 
			// textBoxFundGroupNumber
			// 
			this.textBoxFundGroupNumber.Enabled = false;
			this.textBoxFundGroupNumber.Location = new System.Drawing.Point(192, 72);
			this.textBoxFundGroupNumber.MaxLength = 10;
			this.textBoxFundGroupNumber.Name = "textBoxFundGroupNumber";
			this.textBoxFundGroupNumber.Size = new System.Drawing.Size(40, 22);
			this.textBoxFundGroupNumber.TabIndex = 17;
			this.textBoxFundGroupNumber.Text = "";
			// 
			// checkBoxMajorDenomination
			// 
			this.checkBoxMajorDenomination.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBoxMajorDenomination.Enabled = false;
			this.checkBoxMajorDenomination.ForeColor = System.Drawing.SystemColors.ControlText;
			this.checkBoxMajorDenomination.Location = new System.Drawing.Point(200, 104);
			this.checkBoxMajorDenomination.Name = "checkBoxMajorDenomination";
			this.checkBoxMajorDenomination.Size = new System.Drawing.Size(32, 32);
			this.checkBoxMajorDenomination.TabIndex = 18;
			// 
			// labelMajorDenomination
			// 
			this.labelMajorDenomination.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelMajorDenomination.Location = new System.Drawing.Point(24, 104);
			this.labelMajorDenomination.Name = "labelMajorDenomination";
			this.labelMajorDenomination.Size = new System.Drawing.Size(156, 18);
			this.labelMajorDenomination.TabIndex = 20;
			this.labelMajorDenomination.Text = "Use Major Denomination:";
			// 
			// labelInstructions
			// 
			this.labelInstructions.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelInstructions.Location = new System.Drawing.Point(16, 4);
			this.labelInstructions.Name = "labelInstructions";
			this.labelInstructions.Size = new System.Drawing.Size(320, 48);
			this.labelInstructions.TabIndex = 25;
			this.labelInstructions.Text = "Please specify the following details for the \'{0}\' File relating to the current F" +
				"und Group:";
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(128, 200);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.TabIndex = 26;
			this.buttonSave.Text = "Save";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// FundGroupFileSettings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(352, 233);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.labelInstructions);
			this.Controls.Add(this.labelSigDP);
			this.Controls.Add(this.textBoxNumberOfSigDPs);
			this.Controls.Add(this.labelDP);
			this.Controls.Add(this.textBoxNumberOfDPs);
			this.Controls.Add(this.labelFundGroupNumber);
			this.Controls.Add(this.textBoxFundGroupNumber);
			this.Controls.Add(this.checkBoxMajorDenomination);
			this.Controls.Add(this.labelMajorDenomination);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FundGroupFileSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Distribution File Settings";
			this.ResumeLayout(false);

		}

		#endregion

		private void populateFilePropertyControls(FundGroupDistributionFile file)
		{
			textBoxFundGroupNumber.Text = file.FundGroupNumberRequired && file.FundGroupNumber != null
				? ((int) file.FundGroupNumber).ToString("0#") : "";
			textBoxNumberOfDPs.Text = file.NumberOfDecimalPlaces.ToString() ;
			textBoxNumberOfSigDPs.Text =  file.NumberOfSignificantDecimalPlaces.ToString() ;
			checkBoxMajorDenomination.Checked = file.MajorDenomination ;
		}

		private void setFilePropertiesEnabledState(FundGroupDistributionFile file)
		{
			textBoxFundGroupNumber.Enabled = file.FundGroupNumberRequired;
			textBoxNumberOfDPs.Enabled = file.DecimalPlacesRequired;
			textBoxNumberOfSigDPs.Enabled = file.SignificantDecimalPlacesRequired;
			checkBoxMajorDenomination.Enabled = file.MajorDenimonationRequired;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			bool valid = true;
			valid = validate();

			m_file.IsDirty = valid;

			if (valid)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private bool validate()
		{
			bool valid=true;
			try
			{
				if (textBoxNumberOfDPs.Enabled) m_file.NumberOfDecimalPlaces = int.Parse(textBoxNumberOfDPs.Text);
				errorProvider1.SetError(textBoxNumberOfDPs, string.Empty);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
				{
					this.errorProvider1.SetError(textBoxNumberOfDPs, MessageBoxHelper.DialogText("FundGroupFileNumberOfSigDPFormatError"));
				}
				valid = false;
			}
//			try
//			{
//				if (textBoxNumberOfSigDPs.Enabled) m_file.NumberOfSignificantDecimalPlaces = int.Parse(textBoxNumberOfSigDPs.Text);
//				errorProvider1.SetError(textBoxNumberOfSigDPs, string.Empty);
//			}
//			catch (Exception ex)
//			{
//				if (ex is FormatException)
//				{
//					this.errorProvider1.SetError(textBoxNumberOfSigDPs, MessageBoxHelper.DialogText("FundGroupFileNumberOfDPFormatError"));
//				}
//				valid = false;
//			}
	
			try
			{
				if (textBoxFundGroupNumber.Enabled) m_file.FundGroupNumber = int.Parse(textBoxFundGroupNumber.Text);
				errorProvider1.SetError(textBoxFundGroupNumber, string.Empty);
			}
			catch (Exception ex)
			{
				if (ex is FormatException)
				{
					this.errorProvider1.SetError(textBoxFundGroupNumber, MessageBoxHelper.DialogText("FundGroupFileGroupNumberFormatError"));
				}
				else if (ex is Exceptions.InvalidFundGroupNumber)
				{
					this.errorProvider1.SetError(textBoxFundGroupNumber, MessageBoxHelper.DialogText("FundGroupFileInvalidFundGroupNumber"));
				}

				valid = false;
			}
			if (checkBoxMajorDenomination.Enabled) m_file.MajorDenomination = checkBoxMajorDenomination.Checked;
			return valid;
		}
	}
}