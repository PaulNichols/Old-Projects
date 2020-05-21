using System;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.Interfaces;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// This class contains common code for all static data editors,
	/// which mainly involves just the save/cancel dialog
	/// </summary>
	public class StaticDataEditor : UserControl, IUPDControl
	{
		#region Member variables

		/// <summary>
		/// Used to determine what state the view is in
		/// </summary>
		protected enum authoringModes
		{
			/// <summary>
			/// 
			/// </summary>
			Null,
			/// <summary>
			/// 
			/// </summary>
			New,
			/// <summary>
			/// 
			/// </summary>
			Editing
		}

		//these are protected without accessors, as inheriting 
		//control may wish to have their own accessors that 
		//do more than just set the data.
		//TODO: - revist AssetFundStaticData and see if code on the 'set' can be moved

		/// <summary>
		/// indicates whether changed or not
		/// </summary>
		private bool m_changed = false;

		/// <summary>
		/// displays the validation errors
		/// </summary>
		private ErrorWithDialogBoxProvider m_errors = null;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public StaticDataEditor()
		{
			state = StaticDataFormState.Loading;
			InitializeComponent();
		}

		#endregion

		#region IUPDControl

		/// <summary>
		/// implmentation of IUPDControl method. Only allows menu to change if nothing has changed or user saves or cancels. 
		/// </summary>
		public bool AllowMenuChange()
		{
			return allowNavigateAway();
		}

		#endregion

		#region Component Designer generated code

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

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// clears the errors for the entity editor  
		/// </summary>
		/// <param name="tabs"></param>
		protected void clearErrors(Control tabs)
		{
			if (m_errors != null)
			{
				clearControlErrors(tabs);
				m_errors.Clear();
			}
		}

		/// <summary>
		/// Adds the error msg for the control
		/// </summary>
		/// <param name="control"></param>
		/// <param name="errMsg"></param>
		protected void setError(Control control, string errMsg)
		{
			Errors.SetError(control, errMsg);
		}

		/// <summary>
		/// Displays the error dialog
		/// </summary>
		/// <param name="msg"></param>
		protected void showErrorDialog(string msg)
		{
			if (m_errors != null)
			{
				m_errors.ShowDialog(msg);
			}
		}

		private void clearControlErrors(Control target)
		{
			if (m_errors != null)
			{
				foreach (Control control in target.Controls)
				{
					clearControlErrors(control);
					m_errors.SetError(control, string.Empty);
				}
			}
		}

		/// <summary>
		/// Determines from the state whether the user is Allowed to navigate away from 
		/// the current entity. May pop-up a dialog if unsaved changes exist.
		/// </summary>
		/// <returns>True - if navigate is allowed. False - if not.</returns>
		protected bool allowNavigateAway()
		{
			T.E();
			bool result = true;

			switch(state)
			{
				case StaticDataFormState.Editing:
				case StaticDataFormState.New:
					if (Changed)
					{
						
						switch (MessageBoxHelper.Show(MessageBoxHelper.DialogText("UnsavedChangedBody",new object[]{EditType}),"UnsavedChangedTitle" , MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
						{
							case DialogResult.Yes:
								result = doSave();
								break;

							case DialogResult.No:
								result = true;
								Changed = false;
								break;

							case DialogResult.Cancel:
								result = false;
								break;
						}
					}
					break;

				case StaticDataFormState.Loading:
				case StaticDataFormState.Unknown:
					result = true;
					break;

				default:
					result = false;
					break;
			}

			T.X();
			return result;
		}

		/// <summary>
		/// Loads the selected entity into the editor
		/// </summary>
		protected void loadEntity()
		{
			T.E();
			if (!ListManager.SelectedIsNew && ListManager.SelectedItem != null)
			{
				state = StaticDataFormState.Loading;
				try
				{
					doLoadEntity();
					m_changed = false;
					state = StaticDataFormState.Editing;
				}
				catch
				{
					state = StaticDataFormState.Unknown;
					throw;
				}
			}
			T.X();
		}

		/// <summary>
		/// Override to load for specific entity type into the entity
		/// </summary>
		protected virtual void doLoadEntity()
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// Gets the data for export
		/// </summary>
		protected virtual void getExportParameters(StaticDataExportParameters parameters)
		{
			T.E();
			throw new NotImplementedException();
		}

		/// <summary>
		/// performs the export generically 
		/// </summary>
		protected void doExport()
		{
			T.E();
			try
			{
				string folder = chooseExportLocation();
				if (folder != null)
				{
					StaticDataExportParameters parameters = new StaticDataExportParameters();
					getExportParameters(parameters);

					if (parameters == null || parameters.CollectionToExport == null || parameters.Exports.Count==0)
					{
						throw new ArgumentException("Invalid export data"); //code assertion only
					}

					foreach (StaticDataExport export in parameters.Exports)
					{
						string csvFilePath = string.Format("{0}\\{1}",folder,export.CsvFilename);
						ExportCSVGenerator csvCreator = new ExportCSVGenerator(parameters.CollectionToExport, export.XsltResourceName, csvFilePath);
						csvCreator.XsltCustomCollectionToFile();
					}

					if (parameters.Exports.Count == 1)
					{
						MessageBoxHelper.Show("CSVSuccessBody","CSVSuccessTitle");
					}
					else
					{
						MessageBoxHelper.Show("CSVSuccessBody","CSVSuccessTitle");
					}
				}

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Brings up a dialog for the user to select a folder for export files.
		/// returns null if none selected
		/// </summary>
		/// <returns></returns>
		private string chooseExportLocation()
		{
			T.E();
			FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();

			if (selectFolderDialog.ShowDialog() == DialogResult.OK)
			{
				T.X();
				return selectFolderDialog.SelectedPath;
			}
			else
			{
				T.X();
				return null;
			}
		}

		#endregion

		#region Properties

		private StaticDataFormState m_state = StaticDataFormState.Unknown;

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value></value>
		protected virtual StaticDataFormState state
		{
			get { return m_state; }
			set { m_state = value; }
		}

		private StaticDataListManager listManager;
		
		/// <summary>
		/// Gets or sets the list manager.
		/// </summary>
		/// <value></value>
		public StaticDataListManager ListManager
		{
			get { return listManager; }
			set 
			{ 
				listManager = value; 
				if (listManager != null)
				{
					listManager.SelectedIndexChanging +=new CancelEventHandler(listManager_SelectedIndexChanging);
					listManager.SelectedIndexChanged +=new EventHandler(listManager_SelectedIndexChanged);
				}
			}
		}

		/// <summary>
		/// Returns a description of the current entity instance being edited. When overridding don't 
		/// the name of the entity but rather a description of the current instance (e.g. don't return
		/// "Fund", return the name of the fund).
		/// </summary>
		protected virtual string currentEntityDescription
		{
			get {return "this";}
		}

		/// <summary>
		/// indicates whether changed or not
		/// </summary>
		protected virtual bool Changed
		{
			get { return m_changed; }
			set
			{
				m_changed = value;
			}
		}

		/// <summary>
		/// a string representing the type for user messages
		/// </summary>
		protected virtual string EditType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets the error providers
		/// </summary>
		/// <pattern>
		/// Lazy Load - see Fowler, Patterns of Enterprise Application Architechture
		/// </pattern>
		/// <value></value>
		protected ErrorWithDialogBoxProvider Errors
		{
			get
			{
				if (m_errors == null)
				{
					m_errors = new ErrorWithDialogBoxProvider();
					m_errors.ContainerControl = this;
				}
				return m_errors;
			}
		}

		#endregion

		#region Action Event Handlers (buttons)

		internal void deleteExecuted(object sender, EventArgs e)
		{
			T.E();
			try
			{
				if (this.state == StaticDataFormState.New)
				{
					state = StaticDataFormState.Loading;
					try
					{
						if (ListManager.Items.Count > 1)
							ListManager.SelectedIndex = 0;
						else
							ListManager.SelectedIndex = -1;
					}
					catch
					{
						state = StaticDataFormState.New;
						throw;
					}
				}
				else if (this.state == StaticDataFormState.Editing)
				{
					if (deleteValidation())
					{
						state = StaticDataFormState.Deleting;
						try
						{
							doDelete();
							ListManager.DeleteSelected();
						}
						finally
						{
							state = StaticDataFormState.Editing;
							m_changed = false;
						}

					}

				}
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("UnableToDeleteBody", "UnableToDeleteTitle", ex, "asset funds");
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Override to perform the deletion of the entity
		/// </summary>
		protected virtual void doDelete()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns true or false to indicate if a delete can take place. The default implementation
		/// is to display a Yes/No dialog box.
		/// </summary>
		/// <returns>True - delete, false - do not delete</returns>
		protected virtual bool deleteValidation()
		{
			T.E();
			bool result = MessageBoxHelper.ShowQuestion("ConfirmDeleteBody", "ConfirmDeleteTitle", currentEntityDescription);
			T.X();
			return result; 
		}

		internal void exportExecuted(object sender, EventArgs e)
		{
			T.E();
			StaticDataFormState oldState = this.state;
			this.state = StaticDataFormState.Exporting;
			try
			{
				doExport();
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("UnableToExportBody", "UnableToExportTitle", ex);
			}
			finally
			{
				this.state = oldState;
				T.X();
			}
		}

		internal void newExecuted(object sender, EventArgs e)
		{
			T.E();
			try
			{
				if ((state == StaticDataFormState.Loading || state == StaticDataFormState.Editing) && allowNavigateAway())
				{
					state = StaticDataFormState.New;
					ListManager.AddNew();
					doNew();
				}
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Override to handle new entity
		/// </summary>
		protected virtual void doNew()
		{
			throw new NotImplementedException();
		}

		internal void saveExecuted(object sender, EventArgs e)
		{
			T.E();
			if ((this.Changed && this.state == StaticDataFormState.Editing)  || this.state == StaticDataFormState.New)
			{
				StaticDataFormState oldState = this.state;
				this.state = StaticDataFormState.Saving;
				try
				{
					if (doSave())
					{
						m_changed = false;
						this.state = StaticDataFormState.Editing;
						loadEntity();
					}
					else
					{
						this.state = oldState;
					}
					
				}
				catch
				{
					this.state = oldState;
					m_changed = false;
					throw;
				}
			}
			T.X();
		}

		/// <summary>
		/// Override to do save of the entity
		/// </summary>
		/// <returns></returns>
		protected virtual bool doSave()
		{
			throw new NotImplementedException();
		}

		internal void cancelExecuted(object sender, EventArgs e)
		{
			T.E();
			try
			{
				if (state == StaticDataFormState.New)
				{
					state = StaticDataFormState.Cancelling;
					try
					{
						m_changed = false;
						state = StaticDataFormState.Loading;
						ListManager.SelectedIndex = 0;
						state = StaticDataFormState.Editing;
					}
					catch
					{
						state = StaticDataFormState.New;
						m_changed = true;
						throw;
					}
				}
				else if (state == StaticDataFormState.Editing && Changed)
				{
					state = StaticDataFormState.Cancelling;
					try
					{
						loadEntity();
					}
					finally
					{
						state = StaticDataFormState.Editing;
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion Action Event Handlers

		#region Other Event Handlers

		private void listManager_SelectedIndexChanged(object sender, EventArgs e)
		{
			T.E();
			loadEntity();
			T.X();
		}

		private void listManager_SelectedIndexChanging(object sender, CancelEventArgs e)
		{
			T.E();
			e.Cancel = !allowNavigateAway();
			T.X();
		}

		#endregion
	}

	#region Public enumerations

	/// <summary>
	/// Enumeration of different static data form states
	/// </summary>
	/// <value></value>
	public enum StaticDataFormState
	{
		/// <summary>State is unknown</summary>
		Unknown,
		/// <summary>Form is loading or data is loading</summary>
		Loading,
		/// <summary>Data for an existing item is being displayed</summary>
		Editing,
		/// <summary>Item is being deleted</summary>
		Deleting,
		/// <summary>Item is being saved</summary>
		Saving,
		/// <summary>Data is being exported</summary>
		Exporting,
		/// <summary>Changes being cancelled</summary>
		Cancelling,
		/// <summary>New item being created</summary>
		New
	}

	#endregion
}