using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.BenchMark;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// View Controller for the second level authorisation process
	/// </summary>
	public class SecondLevelAuthorisationViewController : UpdateableStatusViewController
	{
		#region Member variables

		//private bool m_eventsAreSystemGenerated = false;

		#endregion

		#region Action text constants

		private const string m_selectAllActionText = "Select &All";
		private const string deselectAllActionText = "&Deselect All";
		private const string saveActionText = "&Save";
		private const string cancelActionText = "&Cancel";
		private const string printActionText = "&Print";
		private const string exportActionText = "&Export";

		#endregion

		/// <summary>
		/// Creates a new <see cref="SecondLevelAuthorisationViewController"/> instance.
		/// </summary>
		public SecondLevelAuthorisationViewController(UpdateableStatusView updateView, StatusDataManager dataManager) :
			base(updateView, dataManager)
		{
			T.E();
			updateableView.SaveExecuted += new EventHandler(saveExecuted);
			T.X();
		}

		#region Overrides

		/// <summary>
		/// Creates the actions.
		/// </summary>
		protected override StatusAction[] createActions()
		{
			T.E();

			StatusAction[] result = new StatusAction[7];

			result[0] = new StatusAction();
			result[0].Text = "&Refresh";
			result[0].Executed += new EventHandler(refreshExecuted);

			result[1] = new StatusAction();
			result[1].Text = m_selectAllActionText;
			result[1].Executed += new EventHandler(selectAllExecuted);
			result[1].RequestVisibility += new RequestVisibilityHandler(visibilityRequested);

			result[2] = new StatusAction();
			result[2].Text = deselectAllActionText;
			result[2].Executed += new EventHandler(deselectAllExecuted);

			result[3] = new StatusAction();
			result[3].Text = saveActionText;
			result[3].Executed += new EventHandler(saveExecuted);

			result[4] = new StatusAction();
			result[4].Text = cancelActionText;
			result[4].Executed += new EventHandler(cancelExecuted);

			result[5] = new StatusAction();
			result[5].Text = printActionText;
			result[5].Executed += new EventHandler(printExecuted);

			result[6] = new StatusAction();
			result[6].Text = exportActionText;
			result[6].Executed += new EventHandler(exportExecuted);


			T.X();
			return result;
		}

		/// <summary>
		/// Configures the grid.
		/// </summary>
		protected override void configureGrid()
		{
			T.E();
			HBOSGrid grid = updateableView.grid;
			DataGridTableStyle style = new DataGridTableStyle();

			DataGridBool1ClickColumn authoriseImportedCheckBox;
			DataGridBool1ClickColumn authorisePredictedCheckBox;

			grid.TableStyles.Clear();
			grid.TableStyles.Add(style);

			style.AlternatingBackColor = Color.WhiteSmoke;
			style.DataGrid = grid;
			style.HeaderForeColor = SystemColors.ControlText;
			style.MappingName = "";

			// Hack to allow for multi-line header rows.
			// This requires a large font (36pt) to be set for grid.HeaderFont through the properties window
			style.HeaderFont = grid.Font;

			const int defaultColWidth = 80;

			//fund columns
			AddTextBoxReadOnlyColumnStyle("FullName", "Fund Name", 300, HorizontalAlignment.Left, "FullName");
			AddTextBoxReadOnlyColumnStyle("PriceDisplay", "Imported\nFund Price", defaultColWidth, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("PriceMovementPercentDisplay", "Imported\nFund Price\nMovement", defaultColWidth, HorizontalAlignment.Right, "FullName");

			authoriseImportedCheckBox =
				AddBooleanColumnStyle("ProgressStatus", "Authorise\nImported\nFund Price?", defaultColWidth, HorizontalAlignment.Center);

			AddTextBoxReadOnlyColumnStyle("PredictedPriceDisplay", "Predicted\nFund Price", defaultColWidth, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("PredictedPriceMovementPercentDisplay", "Predicted\nFund Price\nMovement", defaultColWidth, HorizontalAlignment.Right, "FullName");

			authorisePredictedCheckBox =
				AddBooleanColumnStyle("UsePredictedPrice", "Authorise\nPredicted\nFund Price?", defaultColWidth, HorizontalAlignment.Center);

			AddTextBoxReadOnlyColumnStyle("PriceMovementVarianceDisplay", "Fund Price\nVariance", defaultColWidth, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("PriceMovementRoundedToleranceDisplay", "Within Fund\nPrice Tolerance", 120, HorizontalAlignment.Left, "FullName");

			//asset fund columns
			AddTextBoxReadOnlyColumnStyle("AssetUnitPriceDisplay", "Imported Asset\nFund Price", 100, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("AssetMovementDisplay", "Imported\nAsset Fund\nMovement", 100, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("PredictedAssetMovementDisplay", "Predicted\nAsset Fund\nMovement", 100, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("AssetMovementVarianceDisplay", "Asset Fund\nVariance", 100, HorizontalAlignment.Right, "FullName");
			AddTextBoxReadOnlyColumnStyle("AssetMovementToleranceDisplay", "Within\nAsset Fund\nTolerance", 100, HorizontalAlignment.Center, "FullName");
			AddTextBoxReadOnlyColumnStyle("FundStatusDisplay", "Price\nStatus", defaultColWidth, HorizontalAlignment.Left, "FullName");
			AddTextBoxReadOnlyColumnStyle("StatusChangedTime", "Status\nChanged\nTime", 130, HorizontalAlignment.Left, "FullName");

			//assign events to check box columns
			authorisePredictedCheckBox.CheckedChanged += new DataGridBool1ClickColumn.CheckedChangedDelegate(authPredictedPrice_CheckedChanged);
			authoriseImportedCheckBox.CheckedChanged += new DataGridBool1ClickColumn.CheckedChangedDelegate(authImportedPrice_CheckedChanged);


			authorisePredictedCheckBox.AllowNull = false;
			authoriseImportedCheckBox.AllowNull = false;

			T.X();
		}

		#endregion Overrides	

		/// <summary>
		/// This structure can be used to get Error messages based on the availability of a benchmark
		/// The idea is that various screens may want to show different messages depending on the context
		/// therefore this logic is in the UI not the IBenchMark implementations
		/// </summary>
		public struct BenchMarkAvailabilityInformation
		{
			private StringDictionary m_ErrorMessages;
			private StringDictionary m_WarningMessages;


			/// <summary>
			/// Creates a new <see cref="BenchMarkAvailabilityInformation"/> instance.
			/// </summary>
			/// <param name="benchmark">Benchmark.</param>
			public BenchMarkAvailabilityInformation(IBenchMark benchmark)
			{
				m_ErrorMessages = new StringDictionary();
				m_WarningMessages = new StringDictionary();

				buildAvailabilityMessageStrings(benchmark);
			}

			/// <summary>
			/// Creates a new <see cref="BenchMarkAvailabilityInformation"/> instance.
			/// </summary>
			/// <param name="fund">Fund.</param>
			public BenchMarkAvailabilityInformation(Fund fund)
			{
				m_ErrorMessages = new StringDictionary();
				m_WarningMessages = new StringDictionary();

				if (fund != null && fund.ParentAssetFund != null && fund.ParentAssetFund.AssetMovementConstituents != null)
				{
					foreach (AssetMovementConstituent movementConstituent in fund.ParentAssetFund.AssetMovementConstituents)
					{
						buildAvailabilityMessageStrings(movementConstituent.BenchMark);
					}
				}
			}


			/// <summary>
			/// Builds the availability message strings.
			/// </summary>
			/// <param name="benchMark">Bench mark.</param>
			private void buildAvailabilityMessageStrings(IBenchMark benchMark)
			{
				// Amended by MAW 14/09/05 (Issue: UA104)
				// Qualified availability messages to make them more meaningful and thus reduce confusion about
				// what is wrong when they are returned.

				string availabilityMessage = "";
				switch (benchMark.Availability)
				{
					case BenchMarkAvailabilityState.AvailableWithWarnings:

						if (benchMark.Currency!=null && (benchMark.Currency.CurrentRate == 0m || benchMark.Currency.PreviousRate == 0m))
						{
							availabilityMessage = "No previous or current currency import values.";
							addWarning(availabilityMessage);
						}

						availabilityMessage = "";

						StockMarketIndex stockMarketIndex = benchMark as StockMarketIndex;
						if (stockMarketIndex != null)
						{
							if (stockMarketIndex.CurrentValue == 0m && stockMarketIndex.PreviousValue == 0m)
							{
								availabilityMessage = "No previous or current stock market index values.";
							}
							else if (stockMarketIndex.PreviousValue == 0m)
							{
								availabilityMessage = "No previous stock market index import value.";
							}
							else if (stockMarketIndex.CurrentValue == 0m)
							{
								availabilityMessage = "No current stock market index import value.";
							}
							addWarning(availabilityMessage);
						}
						break;
					case BenchMarkAvailabilityState.Available:
						break;
					case BenchMarkAvailabilityState.Unavailable:
						Fund fund = benchMark as Fund;
						if (fund != null)
						{
							if (! fund.IsBenchMarkable)
							{
								availabilityMessage = string.Format("The Fund cannot be used as a Benchmark ({0}).", fund.ShortName);
							}
							else
							{
								availabilityMessage = String.Format("The Fund is not yet Released ({0}).", fund.ShortName); //currently ", Enum.GetName(typeof (FundStatusType), this.FundStatus), ". It needs to be
							}

							// Amended by SJR 23/09/05 (Issue: UA111)
							// Treat unavailable benchmarks as errors rather than warnings
							addError(availabilityMessage);
						}
						break;
				}
			}

			/// <summary>
			/// Adds the warning to string dictionary.
			/// </summary>
			/// <param name="availabilityMessage">Availability message.</param>
			private void addWarning(string availabilityMessage)
			{
				if (availabilityMessage != null && availabilityMessage != "" && !m_WarningMessages.ContainsKey(availabilityMessage))
				{
					m_WarningMessages.Add(availabilityMessage, availabilityMessage +"\n");
				}
			}

			/// <summary>
			/// Adds the error message to string dictionary.
			/// </summary>
			/// <param name="availabilityMessage">Availability message.</param>
			private void addError(string availabilityMessage)
			{
				if (availabilityMessage != null && availabilityMessage != "" && !m_WarningMessages.ContainsKey(availabilityMessage))
				{
					m_ErrorMessages.Add(availabilityMessage, availabilityMessage +"\n");
				}
			}

			internal bool AllAvailable
			{
				get { return UniqueErrorMessages.Count == 0 && UniqueWarningMessages.Count == 0; }
			}

			private IList UniqueWarningMessages
			{
				get { return ArrayList.ReadOnly(new ArrayList(m_WarningMessages.Values)); }
			}

			/// <summary>
			/// Gets the unique messages, both error and warning.
			/// </summary>
			/// <value></value>
			public IList UniqueMessages
			{
				get
				{
					ArrayList messagesArray = new ArrayList(m_WarningMessages.Values);
					messagesArray.AddRange(m_ErrorMessages.Values);
					return ArrayList.ReadOnly(messagesArray);
				}
			}

			/// <summary>
			/// Gets the unique error messages.
			/// </summary>
			/// <value></value>
			private IList UniqueErrorMessages
			{
				get { return ArrayList.ReadOnly(new ArrayList(m_ErrorMessages.Values)); }
			}

			/// <summary>
			/// Shows the a message box with either errors or warnings and returns result 
			/// dependning on the messagebox button clicked .
			/// </summary>
			/// <returns></returns>
			public bool ShowMessageBoxAndReturnResult()
			{
				bool result = false;
				if (! this.AllAvailable)
				{
					if (hasErrors())
					{
						String messageString = MessageBoxHelper.DialogText("CannotAuthoriseBody", new object[] {"\n"+getErrorString()});
						MessageBox.Show(messageString, MessageBoxHelper.DialogText("CannotAuthoriseTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
						result = false;
					}
					else if (hasWarnings())
					{
						String messageString = MessageBoxHelper.DialogText("CannotAuthoriseWarningBody", new object[] {"\n"+getWarningString()});
						result = MessageBox.Show(messageString, MessageBoxHelper.DialogText("CannotAuthoriseWarningTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
					}
				}
				else
				{
					result = true;
				}
				return result;
			}

			private string getWarningString()
			{
				String returnString = "";
				foreach (string message in this.UniqueWarningMessages)
				{
					returnString += message;
				}
				return returnString;
			}

			private string getErrorString()
			{
				String returnString = "";
				foreach (string message in this.UniqueErrorMessages)
				{
					returnString += message;
				}
				return returnString;
			}

			private bool hasWarnings()
			{
				return this.UniqueWarningMessages.Count > 0;
			}

			private bool hasErrors()
			{
				return UniqueErrorMessages.Count > 0;
			}

			/// <summary>
			/// Gets the unique messages string which is the contents of the UniqueMessages collection concatinated together.
			/// </summary>
			/// <value></value>
			public string UniqueMessagesString
			{
				get { return getErrorString() + getWarningString(); }
			}
		}

		/// <summary>
		/// Checks the benchmarks avalibility.
		/// </summary>
		/// <param name="benchmark">Benchmark.</param>
		/// <returns></returns>
		public BenchMarkAvailabilityInformation CheckBenchMarkAvalibility(IBenchMark benchmark)
		{
			T.E();

			BenchMarkAvailabilityInformation result;
			result = new BenchMarkAvailabilityInformation(benchmark);

			T.X();
			return result;
		}

		#region Privates

		/// <summary>
		/// Checks the bench mark avalibility.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <returns></returns>
		private BenchMarkAvailabilityInformation checkBenchMarkAvalibility(Fund fund)
		{
			T.E();

			BenchMarkAvailabilityInformation result;
			result = new BenchMarkAvailabilityInformation(fund);

			T.X();
			return result;
		}

		private void doSave()
		{
			// Retrieve only the edited rows.
			FundCollection funds = CurrentFundStatusFundDecorator.FromDecoratedList(updateableView.grid.RetrieveUpdatedCustomCollection());

			foreach (Fund fund in funds)
			{
//				if (fund.FundStatus < Fund.FundStatusType.SecondLevelAuthorised)
//				{
					//fund.IsDirty = (fund.ProgressStatus || fund.UsePredictedPrice);
				fund.IsDirty = true;
//				}
//				else
//				{
//					fund.IsDirty = !(fund.ProgressStatus || fund.UsePredictedPrice);
//				}
			}

			// Save the valid data.
			if (funds != null && funds.Count > 0)
			{
				try
				{
					FundController controller = new FundController(GlobalRegistry.ConnectionString);
					controller.ProgressFunds(funds, Fund.FundStatusType.SecondLevelAuthorised);
				}
				catch (SecondLevelAuthorisationException ex)
				{
					MessageBoxHelper.ShowError(String.Format(ex.Message,((Fund)ex.Fund).HiPortfolioCode),"CannotSaveAuthorisationsTitle",ex);
				}
				catch (Exception ex)
				{
					GUIExceptionHelper.LogAndDisplayException("CannotSaveAuthorisationsBody", "CannotSaveAuthorisationsTitle", ex);
				}
			}

			this.refreshData();
		}

		#endregion

		#region Event Handlers

		private void visibilityRequested(object sender, RequestVisibilityArgs e)
		{
			//defaults
			e.IsEnabled = true;
			e.IsVisible = true;

			if (((StatusAction) sender).Text == m_selectAllActionText)
			{
				FundGroup selectedFundGroup = ((FundStatusDataManager) dataManager).RetrieveFundGroup();
				if (selectedFundGroup != null)
				{
					e.IsEnabled = selectedFundGroup.AllowSelectAllAuthorisation;
				}
			}
		}

		private void selectAllExecuted(object sender, EventArgs e)
		{
			T.E();

			try
			{
				//m_eventsAreSystemGenerated = true;
				if (view.Data != null && updateableView.Data.Count > 0)
				{
					FundCollection funds = CurrentFundStatusFundDecorator.FromDecoratedList(updateableView.grid.RetrieveEntireCustomCollection());

					bool atLeastOneFailed = false;
					for (int i = 0; i < funds.Count; i++)
					{
						Fund fund = funds[i];

						if (fund.FundStatus < Fund.FundStatusType.SecondLevelAuthorised)
						{
							if (fund.PriceSet && this.checkBenchMarkAvalibility(fund).AllAvailable)
							{
								if (fund.UsePredictedPrice)
								{
									updateableView.grid.SetValue(i, "UsePredictedPrice", false); //causes rowstate to change
									fund.UsePredictedPrice = false;
								}

								if (!fund.ProgressStatus)
								{
									updateableView.grid.SetValue(i, "ProgressStatus", true); //causes rowstate to change
									fund.ProgressStatus = true;
								}

								//updateableView.grid.
							}
							else
							{
								atLeastOneFailed = true;
							}
						}
					}
					updateableView.grid.Refresh();

					if (atLeastOneFailed)
					{
						MessageBoxHelper.Show("UnableToSelectAllForAuthBody", "UnableToSelectAllTitle", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			finally
			{
				//m_eventsAreSystemGenerated = false;
				T.X();
			}

		}

		private void deselectAllExecuted(object sender, EventArgs e)
		{
			T.E();
			try
			{
				if (view.Data != null && updateableView.Data.Count > 0)
				{
					//					FundCollection funds = (FundCollection) updateableView.grid.RetrieveEntireCustomCollection();
					FundCollection funds = CurrentFundStatusFundDecorator.FromDecoratedList(updateableView.grid.RetrieveEntireCustomCollection());
					bool anyReleasedFunds = false;

					for (int i = 0; i < funds.Count; i++)
					{
						Fund fund = funds[i];
						if (fund.ProgressStatus || fund.UsePredictedPrice)
						{
							if (fund.FundStatus >= Fund.FundStatusType.Released)
							{
								anyReleasedFunds = true;
							}
							else
							{
								updateableView.grid.SetValue(i, "ProgressStatus", false); //causes rowstate to change
								fund.ProgressStatus = false;

								updateableView.grid.SetValue(i, "UsePredictedPrice", false); //causes rowstate to change
								fund.UsePredictedPrice = false;
							}
						}
					}

					if (anyReleasedFunds)
					{
						MessageBoxHelper.Show("UnauthoriseAllSomeReleasedBody", "UnauthoriseAllSomeReleasedTitle", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					MessageBoxHelper.Show("NothingToUnauthoriseBody", "NothingToUnauthoriseTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}

			finally
			{
				T.X();
			}

		}

		private void saveExecuted(object sender, EventArgs e)
		{
			doSave();
		}

		private void cancelExecuted(object sender, EventArgs e)
		{
			loadDataToGrid();
		}

		private void refreshExecuted(object sender, EventArgs e)
		{
			refreshData();
		}

		private void printExecuted(object sender, EventArgs e)
		{
			printGrid("Second Level Authorisation");
		}

		private void exportExecuted(object sender, EventArgs e)
		{
			exportGrid("FundAuthorisation.csv", "HBOS.FS.AMP.UPD.WinUI.UserControls.SecondLevelAuthorisation.xslt");
		}

		private void authPredictedPrice_CheckedChanged(object sender, CheckBoxEventArgs e)
		{
			Fund changedFund = fundAtRow(e.RowNumber);

			if (e.NewValue == true)
			{
				decimal predPrice = changedFund.PredictedPrice;
				if (predPrice <= 0)
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.Show("CannotAuthoriseUnpredictedBody", "CannotAuthoriseUnpredictedTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				/*********************************************************************************************
				// Amended by SJR 23/09/05 (Issue: UA111)
				// When the fund is unavailable there is no need to process any 
				// benchmarkable funds related through the asset fund.
				// 
				// Removed.  TG 30/9/05 --- even if PriceMovementPercentDisplay == "Unavailable"
				// --- It is OK to go ahead to distribute. What is important is the price not movement.				
				else if (fundAtRow(e.RowNumber).PredictedPriceMovementPercentDisplay == "Unavailable")
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.ShowExclamation("CannotAuthoriseUnavailableBody", "CannotAuthoriseUnavailableTitle", "predicted");
				}
				*********************************************************************************************/
				else
				{
					BenchMarkAvailabilityInformation availabilityInformation = this.checkBenchMarkAvalibility(changedFund);
					bool authorisePredictedPrice = availabilityInformation.AllAvailable;

					if (!authorisePredictedPrice)
					{
						authorisePredictedPrice = availabilityInformation.ShowMessageBoxAndReturnResult();
					}

					if (authorisePredictedPrice)
					{
						if (changedFund.ProgressStatus)
						{
							updateableView.grid.SetValue(e.RowNumber, "ProgressStatus", false);
							changedFund.ProgressStatus = false;
						}

						if (!changedFund.ProgressStatus)
						{
							updateableView.grid.SetValue(e.RowNumber, "UsePredictedPrice", true); //causes rowstate to change
							changedFund.UsePredictedPrice = true;
						}
						updateableView.grid.Refresh();

						updateableView.Changed = true;


						
					}
					else
					{
						updateableView.grid.RejectChanges(e.RowNumber);
					}
				}
			}
			else
			{
				if (changedFund.FundStatus >= Fund.FundStatusType.Released)
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.Show("CannotUnauthoriseReleasedBody", "CannotUnauthoriseReleasedTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					changedFund.UsePredictedPrice = false;
					updateableView.Changed = true;
				}
			}
		}

		private void authImportedPrice_CheckedChanged(object sender, CheckBoxEventArgs e)
		{
			Fund changedFund = fundAtRow(e.RowNumber);

			if (e.NewValue == true)
			{
				if (!changedFund.PriceSet)
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.Show("CannotAuthoriseUnimportedBody", "CannotAuthoriseUnimportedTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				/*********************************************************************************************
				// Amended by SJR 23/09/05 (Issue: UA111)
				// When the fund is unavailable there is no need to process any 
				// benchmarkable funds related through the asset fund.
				// 
				// Removed.  TG 30/9/05 --- even if PriceMovementPercentDisplay == "Unavailable"
				// --- It is OK to go ahead to distribute. What is important is the price not movement.
				else if (fundAtRow(e.RowNumber).PriceMovementPercentDisplay == "Unavailable")
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.ShowExclamation("CannotAuthoriseUnavailableBody", "CannotAuthoriseUnavailableTitle", "imported");
				}
				*********************************************************************************************/
				else
				{
					BenchMarkAvailabilityInformation availabilityInformation = this.checkBenchMarkAvalibility(changedFund);
					bool authoriseImportedPrice = availabilityInformation.AllAvailable;

					if (!authoriseImportedPrice)
					{
						authoriseImportedPrice = availabilityInformation.ShowMessageBoxAndReturnResult();
					}
					if (authoriseImportedPrice)
					{
						if (changedFund.UsePredictedPrice)
						{
							updateableView.grid.SetValue(e.RowNumber, "UsePredictedPrice", false);
							changedFund.UsePredictedPrice = false;
						}

						if (!changedFund.ProgressStatus)
						{
							updateableView.grid.SetValue(e.RowNumber, "ProgressStatus", true); //causes rowstate to change
							changedFund.ProgressStatus = true;
						}
						updateableView.grid.Refresh();

						updateableView.Changed = true;
					}
					else
					{
						updateableView.grid.RejectChanges(e.RowNumber);
					}
				}
			}
			else
			{
				if (changedFund.FundStatus >= Fund.FundStatusType.Released)
				{
					updateableView.grid.RejectChanges(e.RowNumber);
					MessageBoxHelper.Show("CannotUnauthoriseReleasedBody", "CannotUnauthoriseReleasedTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					changedFund.ProgressStatus = false;
					updateableView.Changed = true;
				}
			}
		}

		#endregion
	}
}