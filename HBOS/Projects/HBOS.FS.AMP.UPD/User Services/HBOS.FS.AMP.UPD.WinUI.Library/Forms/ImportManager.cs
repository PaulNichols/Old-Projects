using System;
using System.ComponentModel;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// ImportManager - GUI for displaying Import Reports
	/// </summary>
	public class ImportManager : Form
	{
		#region Controls

		private Panel authorisedPricesPanel;
		private PictureBox authorisedPricesPicture;
		private Label authorisedPricesExplanation;

		#endregion

		#region Internal Fields

		private bool closedViaCommand;

		private ImportController importController;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		private ImportManager()
		{
			T.E();

			try
			{
				InitializeComponent();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fileImportController"></param>
		public ImportManager(ImportController fileImportController) : this()
		{
			T.E();

			try
			{
				this.importController = fileImportController;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ImportManager));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonConfirm = new System.Windows.Forms.Button();
			this.authorisedPricesPanel = new System.Windows.Forms.Panel();
			this.authorisedPricesExplanation = new System.Windows.Forms.Label();
			this.authorisedPricesPicture = new System.Windows.Forms.PictureBox();
			this.authorisedPricesPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(560, 384);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(96, 23);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "Cancel &Import";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonConfirm
			// 
			this.buttonConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonConfirm.Location = new System.Drawing.Point(448, 384);
			this.buttonConfirm.Name = "buttonConfirm";
			this.buttonConfirm.Size = new System.Drawing.Size(96, 23);
			this.buttonConfirm.TabIndex = 1;
			this.buttonConfirm.Text = "&Save Import";
			this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
			// 
			// authorisedPricesPanel
			// 
			this.authorisedPricesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.authorisedPricesPanel.Controls.Add(this.authorisedPricesExplanation);
			this.authorisedPricesPanel.Controls.Add(this.authorisedPricesPicture);
			this.authorisedPricesPanel.Location = new System.Drawing.Point(16, 368);
			this.authorisedPricesPanel.Name = "authorisedPricesPanel";
			this.authorisedPricesPanel.Size = new System.Drawing.Size(360, 48);
			this.authorisedPricesPanel.TabIndex = 2;
			this.authorisedPricesPanel.Visible = false;
			// 
			// authorisedPricesExplanation
			// 
			this.authorisedPricesExplanation.Location = new System.Drawing.Point(32, 24);
			this.authorisedPricesExplanation.Name = "authorisedPricesExplanation";
			this.authorisedPricesExplanation.Size = new System.Drawing.Size(328, 16);
			this.authorisedPricesExplanation.TabIndex = 1;
			this.authorisedPricesExplanation.Text = "Authorised prices exist. This import will not impact these prices.";
			// 
			// authorisedPricesPicture
			// 
			this.authorisedPricesPicture.Image = ((System.Drawing.Image)(resources.GetObject("authorisedPricesPicture.Image")));
			this.authorisedPricesPicture.Location = new System.Drawing.Point(8, 16);
			this.authorisedPricesPicture.Name = "authorisedPricesPicture";
			this.authorisedPricesPicture.Size = new System.Drawing.Size(16, 24);
			this.authorisedPricesPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.authorisedPricesPicture.TabIndex = 0;
			this.authorisedPricesPicture.TabStop = false;
			// 
			// ImportManager
			// 
			this.AcceptButton = this.buttonConfirm;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(672, 422);
			this.ControlBox = false;
			this.Controls.Add(this.authorisedPricesPanel);
			this.Controls.Add(this.buttonConfirm);
			this.Controls.Add(this.buttonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import Manager - Results";
			this.authorisedPricesPanel.ResumeLayout(false);
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

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonConfirm;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Events

		/// <summary>
		/// Form is loading
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			T.E();

			try
			{
				if (this.importController.HasAuthorisedPrices)
					this.authorisedPricesPanel.Visible = true;
				base.OnLoad(e);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Cancel the import
		/// </summary>
		/// <param name="sender">sending object</param>
		/// <param name="e">event arguements</param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			T.E();

			try
			{
				closedViaCommand = true;

				this.importController.CancelImport();

				this.Close();
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// "Save" the import
		/// </summary>
		/// <param name="sender">sending object</param>
		/// <param name="e">event arguements</param>
		private void buttonConfirm_Click(object sender, EventArgs e)
		{
			T.E();

			try
			{
				closedViaCommand = true;

				this.importController.ActivateImport();

				this.Close();
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Capture the OnClosing() event to prompt the user if they really meant to cancel the import
		/// </summary>
		/// <param name="e">Cancel event agruements</param>
		protected override void OnClosing(CancelEventArgs e)
		{
			T.E();

			try
			{
				if (!closedViaCommand)
				{
					if (MessageBoxHelper.Show("UPDImportManagerBody", "UPDImportManagerTitle",
					                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						e.Cancel = true;
					}
				}
				base.OnClosing(e);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Choose the file to import
		/// </summary>
		/// <param name="fileFilter"></param>
		/// <param name="owner">Caller of procedure</param>
		/// <returns>file name to import</returns>
		public static string ChooseImportFile(string fileFilter, IWin32Window owner)
		{
			T.E();
			try
			{
				OpenFileDialog importFileDialog = null;

				DialogResult importFileDialogResult;
				string importFileName;

				using (importFileDialog = new OpenFileDialog())
				{
					//					if (this.useConfigDirectory == true)
					//					{
					//CompanyController cc = new CompanyController();
					importFileDialog.InitialDirectory = CompanyController.GetImportDirectory(
						GlobalRegistry.ConnectionString,
						((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode);

					//	useConfigDirectory = false;
					//}
					importFileDialog.Title = "Open " + fileFilter.Substring(0, fileFilter.IndexOf("("));
					importFileDialog.RestoreDirectory = true;
					importFileDialog.CheckFileExists = true;
					importFileDialog.CheckPathExists = true;
					importFileDialog.Multiselect = false;
					importFileDialog.Filter = fileFilter;
					importFileDialogResult = importFileDialog.ShowDialog(owner);
					importFileName = importFileDialog.FileName;
				}

				if (importFileDialogResult == DialogResult.OK)
				{
					return importFileName;
				}
				else
				{
					return String.Empty;
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Import the file.
		/// </summary>
		/// <param name="fileType">Type of file to import</param>
		/// <param name="importFileName">File to import</param>
		/// <param name="owner">Caller of method</param>
		/// <param name="verifyNewFile">Specify if a check against the file should be carried out</param>
		public static long ImportFile(ImportController.ImportFileType fileType, string importFileName, bool verifyNewFile, Control owner)
		{
			T.E();

			Cursor oldCursor = owner.Cursor;
			owner.Cursor = Cursors.WaitCursor;

			long importid = 0;
			try
			{
				string companyCode = ((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode;
				string connectionString = GlobalRegistry.ConnectionString;
				bool importedFile = false;

				ImportController fileImportController = new ImportController(connectionString);


				// Is it a new file
				bool returnValue = true;
				if (verifyNewFile)
				{
					returnValue = fileImportController.VerifyNewFile(importFileName, companyCode);
				}

				if (!returnValue)
				{
					DialogResult reimportFile = MessageBoxHelper.Show("ConfirmImportTitle", "ConfirmImportBody", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					//DialogResult reimportFile = MessageBox.Show(this, "This file has already been imported. Do you wish to re-import?", "Confirm Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					returnValue = (reimportFile == DialogResult.Yes);
				}

				string fileNameWithoutExtension="";
				string fileExtension="";
				if (returnValue)
				{
					//if it's a price file import then check the file is valid for the company
					if (fileType==ImportController.ImportFileType.Hi3Prices || fileType==ImportController.ImportFileType.Hi3PricesComposite || fileType==ImportController.ImportFileType.Hi3PricesLinked)
					{
						System.IO.FileInfo fi=new System.IO.FileInfo(importFileName);
						fileNameWithoutExtension=fi.Name.Replace(fi.Extension,"");
						fileExtension=fi.Extension.Replace(".","");
						if (! PriceFileController.IsFileRelatedToCompany(fileNameWithoutExtension,companyCode,fileExtension,GlobalRegistry.ConnectionString))
						{
							DialogResult reimportFile = MessageBoxHelper.Show("ImportInvalidCompanyPriceFileBody", "ImportInvalidCompanyPriceFileTitle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							returnValue = (reimportFile == DialogResult.Yes);
							fileNameWithoutExtension="";
							fileExtension="";
						}
					}
				}

				if (returnValue)
				{
					try
					{
						fileImportController.Import(fileType, companyCode, importFileName,fileExtension ,fileNameWithoutExtension,((UPDPrincipal) Thread.CurrentPrincipal).CurrentCompanyValuationDateAndTime);
						importedFile = true;
					}
					catch (ImportInvalidDateFormatException e)
					{
						MessageBoxHelper.ShowError("ImportInvalidDateExceptionBody","ImportFileSchemaErrorTitle",e);
					}
					catch (ImportIncorrectDateException e)
					{
						MessageBoxHelper.ShowError("ImportIncorrectDateExceptionBody","ImportFileSchemaErrorTitle",e,new String[]{e.FileDate.ToString(),e.ExpectedDate.ToString()});
					}
					catch (Exception e)
					{
						MessageBoxHelper.ShowError("ImportExceptionBody","ImportFileSchemaErrorTitle",e);
					}
				
				}

				// If we imported a file, we need to display some GUI
				if (importedFile)
				{
					ImportManager myImportManagerForm = new ImportManager(fileImportController);

					// Create the user control
					ImportControl myImportControl = new ImportControl();
					myImportControl.DisplayValidationErrors(fileImportController.ValidationErrors,
						fileImportController.ImportedRows);

					// Add the control to the form
					myImportManagerForm.Controls.Add(myImportControl);
					DialogResult result=myImportManagerForm.ShowDialog(owner);

					if (result==DialogResult.OK)
					{
						importid = fileImportController.ImportSnapshot.Id;
					}
				}
			}
			catch (HBOS.FS.AMP.UPD.Exceptions.ImportFileSchemaMismatchException ex)
			{
				//HBOS.FS.Data.FileReaders.RowMismatchException 
				GUIExceptionHelper.LogAndDisplayException("ImportFileSchemaErrorBody", "ImportFileSchemaErrorTitle", ex);
			}
			catch
			{
				throw;
			}
				finally
			{
				owner.Cursor = oldCursor;
				T.X();
			}

			return importid;
		}

		#endregion
	}
}