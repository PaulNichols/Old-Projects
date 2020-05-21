using System;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Abstract controller for status views. Override to provide specific functionality
	/// for each view
	/// </summary>
	public abstract class StatusViewController
	{
		/// <summary>
		/// Creates a new <see cref="StatusViewController"/> instance.
		/// </summary>
		protected StatusViewController(StatusView view, StatusDataManager dataManager)
		{
			T.E();
			if (view == null) throw new ArgumentNullException("view", "Cannot create " + this.GetType().ToString() + " without a view");
			if (dataManager == null) throw new ArgumentNullException("dataManager", "Cannot create " + this.GetType().ToString() + " without a data manager");

			this.m_view = view;
			this.m_dataManager = dataManager;
			T.X();
		}

		#region Properties

		private StatusDataManager m_dataManager;

		/// <summary>
		/// Gets the data manager.
		/// </summary>
		/// <value></value>
		protected StatusDataManager dataManager
		{
			get { return m_dataManager; }
		}


		private StatusView m_view;

		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value></value>
		protected StatusView view
		{
			get { return m_view; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Initialises this instance.
		/// </summary>
		public void Initialise()
		{
			T.E();
			view.SuspendLayout();
			try
			{
				dataManager.FiltersChanged += new EventHandler(dataManager_FiltersChanged);
				view.FundGroupChanged += new SimpleLookupHandler(view_FundGroupChanged);
				view.Actions = createActions();
				configureGrid();
				customInitialisation();
			}
			finally
			{
				view.ResumeLayout();
			}
			T.X();
		}

		#endregion Public Methods

		#region Abstract & Virtual Methods

		/// <summary>
		/// Custom initialisation.
		/// </summary>
		protected virtual void customInitialisation()
		{
			// does nothing here
		}

		/// <summary>
		/// Creates the actions.
		/// </summary>
		protected abstract StatusAction[] createActions();

		/// <summary>
		/// Configures the grid.
		/// </summary>
		protected abstract void configureGrid();

		#endregion Abstract & Virtual Methods

		#region Helper methods

		/// <summary>
		/// Loads the data into grid.
		/// </summary>
		protected virtual void loadDataToGrid()
		{
			T.E();
			view.Data = dataManager.GetData();
			T.X();
		}

		/// <summary>
		/// Refreshes the data from the database.
		/// </summary>
		protected virtual void refreshData()
		{
			T.E();
			dataManager.Refresh();
			T.X();
		}

		/// <summary>
		/// Retrieve the fund from the collection bound to the data grid that corresponds with the given
		/// row number.
		/// </summary>
		/// <param name="rowNumber">The row number for which the fund is to be retrieved.</param>
		/// <returns>The fund associated with the row number.</returns>
		protected virtual Fund fundAtRow(int rowNumber)
		{
			object obj = view.grid.RetrieveObject(rowNumber);
			return ((CurrentFundStatusFundDecorator)obj).Fund;
		}


		/// <summary>
		/// Retrieve the asset fund from the collection bound to the data grid that corresponds 
		/// with the given row number.
		/// </summary>
		/// <param name="rowNumber">The row number for which the asset fund is to be retrieved.</param>
		/// <returns>The asset fund associated with the row number.</returns>
		protected AssetFundDecorator assetFundAtRow(int rowNumber)
		{
			object obj = view.grid.RetrieveObject(rowNumber);
			if (obj is AssetFundDecorator)
			{
				return ((AssetFundDecorator) obj);
			}
			else
			{
				return obj as AssetFundDecorator;
			}
		}

		/// <summary>
		/// Prints the contents of the grid.
		/// </summary>
		/// <param name="title">Title.</param>
		protected void printGrid(string title)
		{
			if (view.Data != null && view.Data.Count > 0)
				view.grid.PrintPreview(title);
			else
				MessageBoxHelper.Show("NothingToPrintBody", "NothingToPrintTitle");
		}


		/// <summary>
		/// Prompts the user for a location and then exports the contents of the
		/// grid to a CSV file.
		/// </summary>
		/// <param name="defaultFilename">Default filename.</param>
		/// <param name="xsltResource">XSLT resource.</param>
		protected void exportGrid(string defaultFilename, string xsltResource)
		{
			if (view.Data != null && view.Data.Count > 0)
			{
				string filename = defaultFilename;
				string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string actualFileName = FileDialogHelper.DisplayFilePicker(ref filename, "Export Data", folder, "All Text Files(*.csv)|(*.csv)|All files (*.*)|*.*", FileDialogHelper.FileDialogTypes.SaveDialog);


				if (actualFileName.Length > 0)
				{
					view.grid.Write(actualFileName, xsltResource);

					MessageBoxHelper.Show(MessageBoxHelper.DialogText("SucessfulExportBody",new object[]{ actualFileName}), "SucessfulExportTitle", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
				MessageBoxHelper.Show("NothingToExportBody", "NothingToExportTitle");
		}

		#endregion

		#region Grid Formatting Helper Methods

		/// <summary>
		/// Overloaded Statuc version 
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="ColumnAlignment"></param>
		/// <param name="ToolTipText"></param>
		protected void AddTextBoxReadOnlyColumnStyle(string ColumnMappingName,
		                                             string ColumnHeaderText, int ColumnWidth,
		                                             HorizontalAlignment ColumnAlignment, string ToolTipText)
		{
			AddTextBoxReadOnlyColumnStyle(view.grid, ColumnMappingName, ColumnHeaderText, ColumnWidth, ColumnAlignment, ToolTipText);
		}

		/// <summary>
		/// Add a text box column style to the data grid table style
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="ColumnAlignment"></param>
		/// <param name="ToolTipText"></param>
		/// <param name="grid"></param>
		public static void AddTextBoxReadOnlyColumnStyle(DataGrid grid, string ColumnMappingName,
		                                                 string ColumnHeaderText, int ColumnWidth,
		                                                 HorizontalAlignment ColumnAlignment, string ToolTipText)
		{
			GridColumnFormattingHelper.AddTextBoxReadOnlyColumnStyle(grid, ColumnMappingName, ColumnHeaderText, ColumnWidth, ColumnAlignment, ToolTipText);
		}

		/// <summary>
		/// Add a number column style to the data grid table style
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="DecimalPlaces"></param>
		/// <param name="ColumnAlignment"></param>
		/// <param name="ToolTipText"></param>
		protected void AddNumberColumnStyle(string ColumnMappingName,
		                                    string ColumnHeaderText, int ColumnWidth, int DecimalPlaces,
		                                    HorizontalAlignment ColumnAlignment, string ToolTipText)
		{
			GridColumnFormattingHelper.AddNumberColumnStyle(view.grid, ColumnMappingName, ColumnHeaderText, ColumnWidth, DecimalPlaces, ColumnAlignment, ToolTipText);
		}

		/// <summary>
		/// Add a percent column style to the data grid table style
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="DecimalPlaces"></param>
		/// <param name="ColumnAlignment"></param>
		/// <param name="ToolTipText"></param>
		protected void AddPercentColumnStyle(string ColumnMappingName,
		                                     string ColumnHeaderText, int ColumnWidth, int DecimalPlaces,
		                                     HorizontalAlignment ColumnAlignment, string ToolTipText)
		{
			AddPercentColumnStyle(view.grid, ColumnMappingName,
			                      ColumnHeaderText, ColumnWidth, DecimalPlaces, ColumnAlignment, ToolTipText);
		}

		/// <summary>
		/// Add a percent column style to the data grid table style
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="DecimalPlaces"></param>
		/// <param name="ColumnAlignment"></param>
		/// <param name="ToolTipText"></param>
		/// <param name="grid"></param>
		public static void AddPercentColumnStyle(DataGrid grid,
		                                         string ColumnMappingName,
		                                         string ColumnHeaderText, int ColumnWidth, int DecimalPlaces,
		                                         HorizontalAlignment ColumnAlignment, string ToolTipText)
		{
			GridColumnFormattingHelper.AddPercentColumnStyle(grid, ColumnMappingName, ColumnHeaderText, ColumnWidth, DecimalPlaces, ColumnAlignment, ToolTipText);
		}

		/// <summary>
		/// Add a boolean style column
		/// </summary>
		/// <param name="ColumnMappingName"></param>
		/// <param name="ColumnHeaderText"></param>
		/// <param name="ColumnWidth"></param>
		/// <param name="ColumnAlignment"></param>
		protected DataGridBool1ClickColumn AddBooleanColumnStyle(string ColumnMappingName,
		                                                         string ColumnHeaderText, int ColumnWidth,
		                                                         HorizontalAlignment ColumnAlignment)
		{
			return GridColumnFormattingHelper.AddBooleanColumnStyle(view.grid, ColumnMappingName, ColumnHeaderText, ColumnWidth, ColumnAlignment);
		}

		#endregion

		#region Event Handlers

		private void dataManager_FiltersChanged(object sender, EventArgs e)
		{
			T.E();
			loadDataToGrid();
			T.X();
		}

		private void view_FundGroupChanged(object sender, SimpleLookupEventArgs e)
		{
			T.E();
			dataManager.FundGroupFilter = e.Lookup;
			view.SetActionButtonVisibility();
			T.X();
		}

		#endregion Event Handlers
	}
}