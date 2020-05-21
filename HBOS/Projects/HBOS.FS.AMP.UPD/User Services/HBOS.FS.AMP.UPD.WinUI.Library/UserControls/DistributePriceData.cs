using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.Interfaces;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for DistributePriceData.
	/// </summary>
	public class DistributePriceData : UserControl, IUPDControl, ICustomInit
	{
		#region Local variables

		private DistributionFileController m_distributeController = null;
		private DistributionFileCollection m_distributeCollection = null;
		private SerializeHelper m_serialzeHelper = new SerializeHelper();

		private ArrayList m_viewDistributeCollection = null;
		private DataGridTextBoxColumn m_filePathColumn = null;

		private string m_connectionString = GlobalRegistry.ConnectionString;
		private bool m_readSettingsFromDB = false;
		private bool m_useHashtableLocations = false;

		// Create and initialise a new hash table to hold file settings
		private Hashtable m_fileHastable = new Hashtable();

		// Get the serialise file location from the App Config
		private string m_serialiseFile = GlobalRegistry.AppSettings.ExportSerialiseFileStream;

		#region GUI Controls

		private DataGrid distributePricesDataGrid;
		private Button saveButton;
		private Button resetButton;
		private Button cancelButton;

		#endregion

		#endregion

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#region private methods

		/// <summary>
		/// Allows the entity to change.
		/// </summary>
		/// <returns></returns>
		protected YesNoCancelAction allowMenuChange()
		{
			T.E();
			//default to 'no we don't want to save'
			YesNoCancelAction userAction = YesNoCancelAction.no;
			try
			{
				if (distributePricesDataGrid != null)
				{
					IList updatedItems = distributePricesDataGrid.RetrieveUpdatedCustomCollection();
					if (updatedItems != null & updatedItems.Count > 0)
					{
						userAction = SaveDialog.Show();
						if (userAction == YesNoCancelAction.yes)
						{
							distributeFiles();
						}
					}
				}
			}
			finally
			{
				T.X();
			}
			return YesNoCancelAction.no;
		}

		#endregion

		#region IUPDControl

		/// <summary>
		/// implmentation of IUPDControl method. Only allows menu to change if nothing has changed or user saves or cancels. 
		/// </summary>
		public bool AllowMenuChange()
		{
			bool allowChange = true;
			YesNoCancelAction ync = allowMenuChange();
			//allow it if the user hasn't pressed cancel or if the event has been cancelled
			allowChange = (ync != YesNoCancelAction.cancel);

			return allowChange;
		}

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public DistributePriceData()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion

		#region Protected methods

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.distributePricesDataGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.saveButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize) (this.distributePricesDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// distributePricesDataGrid
			// 
			this.distributePricesDataGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.distributePricesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				| System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.distributePricesDataGrid.BackColor = System.Drawing.SystemColors.Window;
			this.distributePricesDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.distributePricesDataGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.distributePricesDataGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.distributePricesDataGrid.DataMember = "";
			this.distributePricesDataGrid.FlatMode = false;
			this.distributePricesDataGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.distributePricesDataGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.distributePricesDataGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.distributePricesDataGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.distributePricesDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.distributePricesDataGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.distributePricesDataGrid.Location = new System.Drawing.Point(8, 8);
			this.distributePricesDataGrid.Name = "distributePricesDataGrid";
			this.distributePricesDataGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.distributePricesDataGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.distributePricesDataGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.distributePricesDataGrid.PrintColumnSettings = null;
			this.distributePricesDataGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.distributePricesDataGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.distributePricesDataGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.distributePricesDataGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.distributePricesDataGrid.Size = new System.Drawing.Size(488, 232);
			this.distributePricesDataGrid.TabIndex = 0;
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(240, 248);
			this.saveButton.Name = "saveButton";
			this.saveButton.TabIndex = 3;
			this.saveButton.Text = "Distribute";
			this.saveButton.Click += new System.EventHandler(this.distributeButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.resetButton.Location = new System.Drawing.Point(328, 248);
			this.resetButton.Name = "resetButton";
			this.resetButton.TabIndex = 2;
			this.resetButton.Text = "Reset";
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(416, 248);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// DistributePriceData
			// 
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.distributePricesDataGrid);
			this.Name = "DistributePriceData";
			this.Size = new System.Drawing.Size(504, 272);
			((System.ComponentModel.ISupportInitialize) (this.distributePricesDataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region Load methods

		/// <summary>
		/// Load all the data for the screen.
		/// </summary>
		public void CustomInitialization()
		{
			LoadControl();
			RefreshGrid();
		}

		/// <summary>
		/// This will populate the control
		/// </summary>
		public void LoadControl()
		{
			T.E();

			try
			{
				// Get the current company code - now in var decalaratoin
				T.Log("Get the current company code : " + GlobalRegistry.CompanyCode);

				// Get any saved file settings
				this.loadFileSettings();
				T.Log("file settings loaded from serialised file");
				T.DumpHashTable(m_fileHastable);

				// Now build the data object from the distribution controller
				populateDataObject();

				T.Log("View distrubution file collection populated");
				T.DumpObjectDeep(m_viewDistributeCollection);
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayLoadException("Distribute", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void populateDataObject()
		{
			try
			{
				T.Log("Populate data object");

				if (null == m_distributeController)
				{
					m_distributeController = new DistributionFileController(m_connectionString);
					this.configureGrid();
				}
				m_distributeCollection = m_distributeController.LoadFilesForDistribution(GlobalRegistry.CompanyCode);

				// Populate our decorator object
				m_viewDistributeCollection = new ArrayList(m_distributeCollection.Count);
				for (int i = 0; i < m_distributeCollection.Count; i++)
				{
					// Assign item from base collection 
					ViewDistributionFile fileItem = new ViewDistributionFile(m_distributeCollection[i]);

					if (m_useHashtableLocations && m_fileHastable.ContainsKey(fileItem.FileID))
					{
						fileItem.FilePath = m_fileHastable[fileItem.FileID].ToString();
						fileItem.IsDirty = false;
					}

					// Add item into arrayList
					m_viewDistributeCollection.Add(fileItem);
				}

			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayLoadException("Distribute", ex);
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Refresh the data grid with the most recent data
		/// </summary>
		public void RefreshGrid()
		{
			T.E();
			try
			{
				// Make sure we have a valid distribution file object
				if (null == m_viewDistributeCollection)
				{
					this.LoadControl();
				}

				// Now populate the grid
				T.Log("Populate grid with distribution collection");
				this.distributePricesDataGrid.BindToCustomCollection(m_viewDistributeCollection);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Event handlers

		private void cancelButton_Click(object sender, EventArgs e)
		{
			T.E();
			try
			{
				T.Log("Cancel current actions and refresh the grid");
				m_viewDistributeCollection = null;
				this.RefreshGrid();
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayLoadException("Distribute", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			m_viewDistributeCollection = null;
			m_useHashtableLocations = false;
			populateDataObject();
			RefreshGrid();
		}

		/// <summary>
		/// Save the selected distribution data to the specified files
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void distributeButton_Click(object sender, EventArgs e)
		{
			T.E();
			try
			{
				// Note. In its own method to reduce event size
				distributeFiles();
			}
			finally
			{
				T.X();
			}
		}

		private void filePathColumn_Validating(object sender, CancelEventArgs e)
		{
			T.E();
			Cursor oldCursor = this.Cursor;
			int rowNumber = this.distributePricesDataGrid.CurrentRowIndex;
			try
			{
				string filePath = ((ButtonTextBox) m_filePathColumn.TextBox.Controls[0]).Text;

				// Reference selected distribution file item form grid
				DistributionFile fileItem = this.fileAtRow(rowNumber);

				// Has the destinatoin path changed?
				if (fileItem.FilePath != filePath)
				{
					this.Cursor = Cursors.WaitCursor;

					// Validate the entered file path
					if (!Directory.Exists(filePath))
					{
						this.distributePricesDataGrid.RejectChanges(rowNumber);
						MessageBoxHelper.Show("PathDoesNotExistsBody", "PathDoesNotExistsTitle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						e.Cancel = true;
					}
					else
					{
						// Update row item
						distributePricesDataGrid.SetValue(rowNumber, "FilePath", filePath);
					}
				}
			}
			catch (Exception)
			{
				MessageBoxHelper.Show("UnableToValidateBody", "UnableToValidateTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		private void distribute_CheckedChanged(object sender, CheckBoxEventArgs e)
		{
			T.E();
			try
			{
				DistributionFile fileItem = this.fileAtRow(e.RowNumber);

				// Test to see if the value has been changed
				if (e.NewValue == true)
				{
					// Does the distribution status allow the check box to be ticked?
					if (fileItem.Status == DistributionFileStatuses.Unavailable)
					{
						distributePricesDataGrid.RejectChanges(e.RowNumber);
						MessageBoxHelper.Show("CannotDistributeUnavailableFundsBody", "CannotDistributeUnavailableFundsTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		private void filePathColumn_ButtonClicked(object sender, EventArgs e)
		{
			T.E();

			try
			{
				// Display the folder dialog box and extract the chosen path
				string filePath = ((ButtonTextBox) m_filePathColumn.TextBox.Controls[0]).Text;
				filePath = FileDialogHelper.DisplayFolderPicker(filePath, true);

				// Save path back to grid when it has changed
				if (filePath != ((ButtonTextBox) m_filePathColumn.TextBox.Controls[0]).Text)
				{
					((ButtonTextBox) m_filePathColumn.TextBox.Controls[0]).Text = filePath;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void filePathColumn_GotFocus(object sender, EventArgs e)
		{
			T.E();
			int rowNumber = distributePricesDataGrid.CurrentRowIndex;

			try
			{
				// Reference selected distribution file item form grid
				DistributionFile fileItem = this.fileAtRow(rowNumber);

				// Add control into the column
				m_filePathColumn.TextBox.Controls[0].Text = fileItem.FilePath; //.Add(btnTextBox);
				m_filePathColumn.TextBox.Controls[0].Focus();
			}
			finally
			{
				T.X();
			}
		}

/*
		private void filePathColumn_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Tab)
			{
				this.filePathColumn_Validating(sender, new CancelEventArgs());
			}
		}
*/

		#endregion

		#region Private methods

		/// <summary>
		/// 
		/// </summary>
		private void configureGrid()
		{
			T.E();
			HBOSGrid grid = distributePricesDataGrid;
			DataGridTableStyle style = new DataGridTableStyle();

			DataGridBool1ClickColumn distributeCheckboxColumn = null;

			try
			{
				grid.TableStyles.Clear();
				grid.TableStyles.Add(style);

				style.HeaderFont = grid.Font;
				style.AlternatingBackColor = Color.WhiteSmoke;
				style.DataGrid = grid;
				style.HeaderForeColor = SystemColors.ControlText;
				style.MappingName = "";

				// Create our distribution columns
				T.Log("Create our distribution columns");
				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "FileName", "Distribution File", 125, HorizontalAlignment.Left, "The name of the distribution file");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "FileDescription", "Description", 200, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "TotalFundCount", "Total\nFunds", 50, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "DistributableCount", "Distributable\nFunds", 80, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "DistributedCount", "Distributed\nFunds", 70, HorizontalAlignment.Left, "");

				GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid,
				                                                         "Status", "Status", 75, HorizontalAlignment.Left, "The current status of the file");

				distributeCheckboxColumn = GridColumnFormattingHelper.AddBooleanColumnStyle(grid,
				                                                                            "Distribute", "Distribute", 75, HorizontalAlignment.Left);

				// Prevent the three-way state
				distributeCheckboxColumn.AllowNull = false;

				m_filePathColumn = GridColumnFormattingHelper.AddButtonTextBoxColumnStyle(grid,
				                                                                          "FilePath", "File Location", 300, HorizontalAlignment.Left, "The location of the distribution file");

				T.Log("Assign event handlers");
				// Assign events to check box column
				distributeCheckboxColumn.CheckedChanged += new DataGridBool1ClickColumn.CheckedChangedDelegate(distribute_CheckedChanged);

				// Assign event handlers text box columns
				m_filePathColumn.TextBox.Validating += new CancelEventHandler(filePathColumn_Validating);
				m_filePathColumn.TextBox.GotFocus += new EventHandler(filePathColumn_GotFocus);

				((ButtonTextBox) m_filePathColumn.TextBox.Controls[0]).ButtonClicked += new ButtonTextBox.ButtonClickedHandler(filePathColumn_ButtonClicked);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Populate the hastable with file settings from the serialised file
		/// </summary>
		/// <exception cref="ExportException">It has not been possible to load the local distribution file settings</exception>
		private void loadFileSettings()
		{
			T.E();
			try
			{
				m_fileHastable = m_serialzeHelper.LoadHashtableFromStream(m_serialiseFile);

				// Can we populate the hastable with file settings from the serialised file?
				if (null != m_fileHastable && m_fileHastable.Count > 0 && !m_readSettingsFromDB)
				{
					this.m_useHashtableLocations = true;
				}
			}
			catch (Exception ex)
			{
				m_useHashtableLocations = false;
				m_fileHastable = null;

				throw new ExportException("It has not been possible to load the local distribution file settings.", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Write the file information back to the hashtable to hold the file settings
		/// </summary>
		/// <param name="distributionFileItem"></param>
		private void updateFileHashtable(DistributionFile distributionFileItem)
		{
			try
			{
				// Update the hashtable value
				if (m_fileHastable.ContainsKey(distributionFileItem.FileID))
				{
					m_fileHastable[distributionFileItem.FileID] = distributionFileItem.FilePath;
				}
				else
				{
					m_fileHastable.Add(distributionFileItem.FileID, distributionFileItem.FilePath);
				}
			}
			catch (ArgumentNullException ex)
			{
				T.DumpException(ex);
				T.DumpHashTable(m_fileHastable);
				throw new ExportException("The local distribution file item settings resource is empty.", ex);
			}
			catch (ArgumentException ex)
			{
				T.DumpException(ex);
				T.DumpHashTable(m_fileHastable);
				throw new ExportException("Distribution file item with the same key already exists in the local file settings resource.", ex);
			}
			catch (NotSupportedException ex)
			{
				T.DumpException(ex);
				T.DumpHashTable(m_fileHastable);
				throw new ExportException("The local distribution file settings resource is read-only. OR, the resource has a fixed size.", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Retrieve the dustribution file object from the collection bound to the data grid that 
		/// corresponds with the given row number.
		/// </summary>
		/// <param name="rowNumber">The row number for which the distribution file object is to be retrieved.</param>
		/// <returns>The distribution file object associated with the row number.</returns>
		private DistributionFile fileAtRow(int rowNumber)
		{
			object obj = this.distributePricesDataGrid.RetrieveObject(rowNumber);
			return obj as DistributionFile;
		}

		/// <summary>
		/// Distribute the selected files
		/// </summary>
		private void distributeFiles()
		{
			T.E();
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;

			try
			{
				string locationSettingsMessageString = "";
				StringBuilder messageString = new StringBuilder();

				// First, capture modified items from the grid
				ArrayList distributeCollection = new ArrayList(distributePricesDataGrid.RetrieveUpdatedCustomCollection());

				// Step through the collection and distribute each file checked
				for (int i = 0; i < distributeCollection.Count; i++)
				{
					// Write the file information back to the hashtable to hold the file settings
					this.updateFileHashtable((DistributionFile) distributeCollection[i]);

					if (((ViewDistributionFile) distributeCollection[i]).Distribute
						&& ((ViewDistributionFile) distributeCollection[i]).Status != DistributionFileStatuses.Unavailable)
					{
						// Create the correct object type
						DistributionFile fileItem = (DistributionFile) distributeCollection[i];

						try
						{
							DataSet dataToDistribute = m_distributeController.RetrieveAndValidateData(fileItem);
							if (!((ViewDistributionFile) fileItem).DisplayDataManipulationPopup(this, dataToDistribute))
							{
								//the popup was cancelled so skip distributing this file
								break;
							}
							m_distributeController.Distribute(fileItem, dataToDistribute);

							// On a successful file distribution, update the file status
							m_distributeController.SaveFileAfterDistribution(fileItem);

							// Populate outer stringBuilder with success message
							messageString.AppendFormat("Successfully generated: {0}.\n", fileItem.FileDescription.Trim());
						}
						catch (ExportException ex)
						{
							messageString.Append(ex.Message + "\n");
						}
					}
				}

				// Now serialise the file path information so any modified 
				// paths are preserved for the next distribution
				try
				{
					m_serialzeHelper.SaveHashtableToStream(m_fileHastable, m_serialiseFile);
				}
				catch
				{
					messageString.AppendFormat("\nYour file location settings were not saved locally.");
				}

				// Inform user of any warnings or errors
				if (messageString.Length > 0)
				{
					MessageBox.Show(messageString.ToString() + locationSettingsMessageString, "Distribute Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

				m_viewDistributeCollection = null;
				this.RefreshGrid();
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				ExceptionManager.Publish(ex);

				//This is a top level UI event, so catch it here & show exception
				MessageBoxHelper.ShowError("UnableToDistributeDataTitle", "UnableToDistributeDataBody", ex, "", ex.Message.ToString());
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		#endregion

		#region Internal classes

		/// <summary>
		/// Object holding the base DistributionFile object and the 'Distribute' flag.
		/// This is used as a helper class within the User Control, because it will not be used anywhere else in the application.
		/// </summary>
		/// <remarks>
		/// This class untilises a basic Decorator pattern.
		/// </remarks>
		/// <remarks>
		/// Ideally we would decalre the class and all its methods and properties as internal, 
		/// unfortunately the grid does not reside in the UserControl namespace which prevents this.
		/// </remarks>
		public class ViewDistributionFile : DistributionFile
		{
			private bool m_distribute;

			/// <summary>
			/// Constructor
			/// </summary>
			public ViewDistributionFile() : base()
			{
			}

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="file">Distribution file object</param>
			public ViewDistributionFile(DistributionFile file) :
				base(file.FileID, file.FileDescription, file.FileName, file.FilePath,
				     file.ArchiveFolder, file.CompanyCode,
				     file.ManipulationClassToInvoke, file.Status, file.DistributedCount, file.DistributableCount, file.TotalFundCount,
				     null, file.TimeStamp)
			{
				this.xsltLoader = file.xsltLoader;

			}

			/// <summary>
			/// Flag indicating if the user wants to distribute the selected item
			/// </summary>
			public bool Distribute
			{
				get { return m_distribute; }

				set { m_distribute = value; }
			}

			/// <summary>
			/// Displays the data manipulation popup if one has been assigned, this allows users a chance 
			/// to alter data which will appear on the distributed files.
			/// </summary>
			/// <returns>bool True=OK button, False=Cancel button</returns>
			public bool DisplayDataManipulationPopup(IWin32Window parent, DataSet dataToDistribute)
			{
				bool result = true;

				if (ManipulationClassToInvoke != null)
				{
					result = false;
					ObjectHandle popupFormHandle = Activator.CreateInstance
						(
						this.GetType().Assembly.FullName,
						ManipulationClassToInvoke,
						false,
						BindingFlags.CreateInstance,
						null,
						new object[] {dataToDistribute,this},
						null,
						null,
						null
						);
					if (popupFormHandle != null)
					{
						DistributionDataManipulationUPAS popupForm = popupFormHandle.Unwrap() as DistributionDataManipulationUPAS;
						if (popupForm != null)
						{
							result = (popupForm.ShowDialog() == DialogResult.OK);
						}
					}
				}
				
				return result;

			}
		}

		#endregion
	}
}