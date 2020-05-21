using System;
using System.Windows.Forms;
using System.Collections;

using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Abstract class to control the view for Static Data
	/// </summary>
	public abstract class StaticDataViewController
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="StaticDataViewController"/> instance.
		/// </summary>
		/// <param name="frame">The mFrame under control.</param>
		protected internal StaticDataViewController(StaticDataFrame frame)
		{
			if (frame==null) throw new ArgumentNullException("frame","Cannot construct a StaticDataViewController without a frame");
			this.mFrame = frame;
		}

		#endregion

		#region Abstract & Virtual Methods

		/// <summary>
		/// Handles state change when the entity being viewed in changed
		/// </summary>
		/// <param name="newEntity">The new entity being viewed.</param>
		protected abstract void EntityChanged(object newEntity);

		/// <summary>
		/// Gets the editor for the entity.
		/// </summary>
		/// <returns>A user control to use as the editor</returns>
		protected abstract UserControl GetEntityEditor();
		
		/// <summary>
		/// Gets the entity collection for display. Each entity's ToString() method is used to determine display.
		/// </summary>
		/// <returns></returns>
		protected abstract IList GetEntityCollection();
		
		/// <summary>
		/// Provides a custom initialisation point that can be overriden, does nothing in the default implementation
		/// </summary>
		protected virtual void CustomInitialisation()
		{
			//does nothing
		}

		/// <summary>
		/// Gets an array of allowable actions in the GUI, you should hook to the Executed event to
		/// act on the action from the GUI.
		/// </summary>
		protected abstract StaticDataAction[] GetActions();

		/// <summary>
		/// Allows the selected entity to change.
		/// </summary>
		/// <returns>True to allow the change or false to forbid. Default to true.</returns>
		protected virtual StaticDataFrame.YesNoCancelEventArgs.YesNoCancelAction AllowEntityToChange()
		{
			return StaticDataFrame.YesNoCancelEventArgs.YesNoCancelAction.unknown;
		}

		#endregion

		#region Member Variables

		/// <summary>
		/// a flag to indicate that code is currently executing due to a user having clicked on a 
		/// different item in the list
		/// </summary>
		protected bool m_indexChanging ;

		#endregion

		#region Privates

		private void HookEventsUp()
		{
//			frame.SelectedItemChanged +=new StaticDataEventHandler(frame_SelectedItemChanged);
//			frame.SelectedIndexChanging +=new StaticDataFrame.YesNoCancelEventHandler(frame_SelectedIndexChanging);
		}


		#endregion

		#region Methods

		/// <summary>
		/// Initialises this instance after creation.
		/// </summary>
		public void Initialise()
		{
			HookEventsUp();
			//frame.Body = GetEntityEditor();
			//frame.Actions = GetActions();
			refreshList();
			CustomInitialisation();
			frame.SelectFirst();
		}

		/// <summary>
		/// Refreshes the list, you need to call this whenever you make changes to the underlying list.
		/// </summary>
		protected void refreshList()
		{
			frame.SelectList = GetEntityCollection();
		}

		/// <summary>
		/// Determines whether the controller is dealing with a new entity
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </returns>
		protected bool isNew()
		{
			return (frame.SelectedItem != null && frame.SelectedItem is StaticDataFrame.NewEntity);
		}

		/// <summary>
		/// Gets the data for export
		/// </summary>
		/// <param name="exportColl"></param>
		/// <param name="xsltFiles"></param>
		/// <param name="exportFileNames"></param>
		protected abstract void getExportData(out IList exportColl, out string[] xsltFiles, out string[] exportFileNames);

		/// <summary>
		/// performs the export generically 
		/// </summary>
		/// <param name="editor"></param>
		protected void exportFiles (UserControl editor)
		{

			Cursor oldCursor = editor.Cursor;
			try
			{
				string folder = chooseExportLocation();
				if (folder != null)
				{
					
					if (oldCursor != Cursors.WaitCursor)
					{
						editor.Cursor = Cursors.WaitCursor;
					}
					IList exportColl;
					string[] xsltFiles;
					string[] exportFileNames;
					getExportData (out exportColl, out xsltFiles, out exportFileNames);
					
					if (exportColl == null || xsltFiles == null || exportFileNames == null || xsltFiles.Length != exportFileNames.Length || xsltFiles.Length == 0)
					{
						throw new ArgumentException ("Invalid export data"); //code assertion only
					}

					for (int i = 0; i < xsltFiles.Length; i++)
					{									 
						ExportCSVGenerator csvCreator = new ExportCSVGenerator(exportColl, xsltFiles[i], folder + "\\" + exportFileNames[i]);
						csvCreator.XsltCustomCollectionToFile();
					}
					if (oldCursor != Cursors.WaitCursor)
					{
						editor.Cursor = oldCursor;
					}
					if (xsltFiles.Length == 1)
					{
						MessageBox.Show("CSV file successfully created.");
					}
					else
					{
						MessageBox.Show("CSV files successfully created.");
					}
				}

			}
			finally
			{
				if (oldCursor != Cursors.WaitCursor)
				{
					editor.Cursor = oldCursor;
				}
			}

		}

		/// <summary>
		/// Brings up a dialog for the user to select a folder for export files.
		/// returns null if none selected
		/// </summary>
		/// <returns></returns>
		private string chooseExportLocation ()
		{
			try
			{
				FolderBrowserDialog selectFolderDialog = new FolderBrowserDialog();

				if(selectFolderDialog.ShowDialog() == DialogResult.OK)
				{
					return selectFolderDialog.SelectedPath;
				}
				else
				{
					return null;
				}
			}
			finally
			{
			}

		}

		#endregion

		#region Event Handlers

		private StaticDataFrame mFrame;


		private void frame_SelectedItemChanged(object sender, StaticDataEventArgs e)
		{
			EntityChanged(e.SelectedItem);
		}

		private void frame_SelectedIndexChanging(object sender, StaticDataFrame.YesNoCancelEventArgs e)
		{
			m_indexChanging = true;
			//all single threaded so ok to set member variable then back again when done
			e.UserAction = AllowEntityToChange();

			m_indexChanging = false;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Provides access to the underlying Static Data Frame
		/// </summary>
		/// <value></value>
		protected StaticDataFrame frame
		{
			get {return mFrame;}
		}

		#endregion
	}
}
