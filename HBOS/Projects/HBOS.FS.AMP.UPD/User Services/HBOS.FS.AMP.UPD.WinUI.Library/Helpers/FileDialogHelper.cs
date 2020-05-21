using System;
using System.Windows.Forms;
using System.IO;

using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Helpers
{
	/// <summary>
	/// Summary description for FileDialogHelper.
	/// </summary>
	/// <remarks>Abstract to stop people creating instances of the class</remarks>
	public abstract class FileDialogHelper
	{
		#region Enumerators
		/// <summary>
		/// Type of file picker dialog
		/// </summary>
		[FlagsAttribute]
			public enum FileDialogTypes : int
		{
			/// <summary>
			/// Dialog box for saving files
			/// </summary>
			SaveDialog = 0,

			/// <summary>
			/// Dialog box for opening files
			/// </summary>
			OpenDialog = 1,
		}
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>Done like this to prevent construction of the static class.  It will throw an error</remarks>
		private FileDialogHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region File picker
		/// <summary>
		/// Set up the save dialog box
		/// </summary>
		/// <param name="titleText">Dialog title text</param>
		/// <param name="defaultFolder">Default folder</param>
		/// <param name="filename">Name of the file to be exported</param>
		/// <param name="filter">File type filter</param>
		/// <param name="pickerType">The dialog box type to use</param>
		/// <returns>The filename and path</returns>
		public static string DisplayFilePicker(ref string filename, 
			string titleText, string defaultFolder, string filter, 
			FileDialogTypes pickerType)
		{
			T.E();
			string retVal = string.Empty;
			FileDialog filePicker = null;

			// Initiate the correct type of dialog box
			if ( pickerType == FileDialogTypes.SaveDialog )
			{
				filePicker = new SaveFileDialog();
			}
			else
			{
				filePicker = new OpenFileDialog();
				filePicker.CheckFileExists = true;
			}

			// Build file picker properties
			filePicker.Filter = filter;
			filePicker.FilterIndex = 1 ;
			filePicker.RestoreDirectory = false ;
			filePicker.Title = titleText;
			filePicker.CheckPathExists = true;
			filePicker.FileName = Path.GetFileName( filename );

			if ( Directory.Exists( Path.GetDirectoryName( filename ) ) )
			{
				filePicker.InitialDirectory = Path.GetDirectoryName( filename );
			}
			else
			{
				filePicker.InitialDirectory = defaultFolder;
			}

			// Return selected filename and path when Cancel is NOT selected
			if ( filePicker.ShowDialog() == DialogResult.OK )
			{
				filename = filePicker.FileName;

				if ( filename.Substring(filename.Length -1, 1) == ")" )
				{
					//filename = filename
				}
				retVal = filename;
			}
			else
			{
				retVal = "" ;
			}

			T.X();
			return ( retVal );
		}
		#endregion

		#region Folder picker
		/// <summary>
		/// Display a FolderBrowserDialog to allow users to select a directory from the file server
		/// </summary>
		/// <param name="initialDirectory">The default directory to start browsing from</param>
		/// <param name="showNewFolderButton">Allow new folders to be created</param>
		/// <returns>
		/// The selected folder name when the OK button is selected 
		/// and the inoitialDirectory when the cancel is selected.
		/// </returns>
		public static string DisplayFolderPicker(string initialDirectory, bool showNewFolderButton)
		{
			T.E();
			string folderName = initialDirectory;
			FolderBrowserDialog folderPicker = new FolderBrowserDialog();

			try
			{
				// Set-up the dialog
				folderPicker.Description = "Select the directory that you want to use as the default.";
				folderPicker.ShowNewFolderButton = showNewFolderButton;
				folderPicker.SelectedPath = initialDirectory;

				// Show the FolderBrowserDialog.
				DialogResult result = folderPicker.ShowDialog();
				if( result == DialogResult.OK )
				{
					folderName = folderPicker.SelectedPath;
				}
			}
			finally
			{
				T.X();
			}

			return ( folderName );
		}
		#endregion

	}
}
