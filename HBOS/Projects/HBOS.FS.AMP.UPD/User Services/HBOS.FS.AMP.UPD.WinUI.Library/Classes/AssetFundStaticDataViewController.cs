using System;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.WeightedIndices;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// View controller for fund static data maintenance
	/// </summary>
	public class AssetFundStaticDataViewController : StaticDataViewController
	{
		#region Member Variables

		private SimpleStringLookupCollection m_assetFunds ;
		private AssetFundStaticDataEditor m_editor ;
		private FundGroupController m_fundGroupController ;
		private LookupController m_lookupController ;
		private const string assetFundTypeName = "asset fund";
		private string m_companyCode;
		private string m_connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
		private bool m_editingFunds = false;
		private IList m_cachedFunds = null;

		private enum CurrentAction
		{
			unknownAction,
			newAction,
			saveAction,
			deleteAction,
			cancelAction,
			exportAction
		}

		private CurrentAction m_currentAction = CurrentAction.unknownAction;

		#endregion

		/// <summary>
		/// Creates a new <see cref="AssetFundStaticDataViewController"/> instance.
		/// </summary>
		/// <param name="frame">The frame to display in</param>
		internal AssetFundStaticDataViewController(StaticDataFrame frame) : base(frame)
		{
			T.E();
			try
			{
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				m_companyCode = updPrincipal.CompanyCode;

				m_fundGroupController = new FundGroupController(this.m_connectionString);
				m_assetFunds = AssetFundController.LoadAssetFundLookupsByCompany(m_connectionString, m_companyCode);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Handles state change when the entity being viewed in changed
		/// </summary>
		/// <param name="newEntity">The new entity being viewed.</param>
		protected override void EntityChanged(object newEntity)
		{
			T.E();
			try
			{
				m_cachedFunds = null;
				m_editingFunds = false;
				if (!(newEntity is StaticDataFrame.NewEntity))
				{
					//no need to reload it if new  - this is all covered on editor New method

					loadAssetFund(((SimpleStringLookup) newEntity).Key);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Gets the editor for the entity.
		/// </summary>
		/// <returns>A user control to use as the editor</returns>
		protected override UserControl GetEntityEditor()
		{
			T.E();
			try
			{
				m_editor = new AssetFundStaticDataEditor();
				m_editor.Dock = DockStyle.Fill;

				m_editor.AllFundGroupsRequested += new AssetFundStaticDataEditor.RequestListHandler(editor_AllFundGroupsRequested);
				m_editor.AssetFundFundGroupsRequested += new AssetFundStaticDataEditor.RequestListHandler(editor_AssetFundFundGroupsRequested);

				m_editor.AssetFundFundsRequested += new AssetFundStaticDataEditor.RequestListHandler(editor_AssetFundFundsRequested);
				m_editor.FundsEdited += new EventHandler(editor_FundsEdited);

				m_editor.AllMarketIndicesRequested += new AssetFundStaticDataEditor.RequestListHandler(editor_AllMarketIndicesRequested);

				m_editor.MarketIndicesUpdated += new AssetFundStaticDataEditor.RequestListHandler(editor_MarketIndicesUpdated);

				m_editor.SaveExecuted += new StaticDataEventHandler(saveExecuted);

			}
			finally
			{
				T.X();
			}
			return m_editor;
		}

		/// <summary>
		/// Gets the entity collection for display. Each entity's ToString() method is used to determine display.
		/// </summary>
		/// <returns></returns>
		protected override IList GetEntityCollection()
		{
			T.E();
			T.X();
			return m_assetFunds;
		}

		/// <summary>
		/// Provides a custom initialisation point that can be overriden, does nothing in the default implementation
		/// </summary>
		protected override void CustomInitialisation()
		{
			T.E();
			T.X();
			//does nothing
		}

		/// <summary>
		/// Gets an array of allowable actions in the GUI, you should hook to the Executed event to
		/// act on the action from the GUI.
		/// </summary>
		protected override StaticDataAction[] GetActions()
		{
			T.E();
			StaticDataAction[] actions = null;
			try
			{
				actions = new StaticDataAction[5];

				actions[0] = new StaticDataAction();
				actions[0].Text = "&Delete";
				actions[0].Executed += new StaticDataEventHandler(deleteExecuted);

				actions[1] = new StaticDataAction();
				actions[1].Text = "&Export";
				actions[1].Executed += new StaticDataEventHandler(exportExecuted);

				actions[2] = new StaticDataAction();
				actions[2].Text = "&New";
				actions[2].Executed += new StaticDataEventHandler(newExecuted);

				actions[3] = new StaticDataAction();
				actions[3].Text = "&Save";
				actions[3].Executed += new StaticDataEventHandler(saveExecuted);

				actions[4] = new StaticDataAction();
				actions[4].Text = "&Cancel";
				actions[4].Executed += new StaticDataEventHandler(cancelExecuted);
			}
			finally
			{
				T.X();
			}

			return actions;
		}

		/// <summary>
		/// Allows the entity to change.
		/// </summary>
		/// <returns></returns>
		protected override StaticDataFrame.YesNoCancelEventArgs.YesNoCancelAction AllowEntityToChange()
		{
			T.E();
			try
			{
				if (m_currentAction == CurrentAction.cancelAction)
				{
					//meaning 'no the user doesn't wish to save'
					return StaticDataFrame.YesNoCancelEventArgs.YesNoCancelAction.no;
				}
				else
				{
					return m_editor.AllowEntityToChange();
				}
			}
			finally
			{
				T.X();
			}
		}

		#region Event Handlers

		private void deleteExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				m_currentAction = CurrentAction.deleteAction;
				//TODO: replace with resource
				string question = string.Format("Are you sure you want to delete this {0}?", "asset fund");
				if (MessageBox.Show(question, "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (m_editor.Changed)
					{
						//still retains editor newassetfundtype for new items - we only swap over to specific assetfundtype when validated
						m_editor.UpdateAssetFund();
					}
					if (!m_editor.CurrentAssetFund.IsNew)
					{
						m_editor.CurrentAssetFund.IsDeleted = true;
						AssetFundController.UpdateAssetFund(m_connectionString, m_editor.CurrentAssetFund);
					}
					m_editor.Changed = false;
					//reload them from db to remove the deleted item
					m_assetFunds = AssetFundController.LoadAssetFundLookupsByCompany(m_connectionString, m_companyCode);
					refreshList();
				}

			}
			catch (AssetFundAssocDeletionException)
			{
				//TODO: replace with resource
				string msg = string.Format("This asset fund has associated {0}s and cannot be deleted", "fund");
				MessageBox.Show(msg, "Cannot Delete Asset Fund", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("DatabaseError", "DatabaseErrorTitle", ex);
			}
			finally
			{
				m_currentAction = CurrentAction.unknownAction;
				T.X();
			}
		}

		/// <summary>
		/// Gets the data for export
		/// </summary>
		/// <param name="exportColl"></param>
		/// <param name="xsltFiles"></param>
		/// <param name="exportFileNames"></param>
		protected override void getExportData(out IList exportColl, out string[] xsltFiles, out string[] exportFileNames)
		{
			exportColl = AssetFundController.LoadAssetFundsByCompanyIdForStaticDataExport(m_connectionString, m_companyCode);

			xsltFiles = new string[]
				{
					"HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataFundGroups.xslt",
					"HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataMarketSplits.xslt",
					"HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataCompositeSplits.xslt"
				};

			string dateFormatted = String.Format("{0:yyyyMMdd}", DateTime.Now.Date);
			exportFileNames = new string[]
				{
					m_companyCode + "_assetfunds_fundgroupmembership_" + dateFormatted + ".csv",
					m_companyCode + "_assetfunds_marketsplits_" + dateFormatted + ".csv",
					m_companyCode + "_assetfunds_compsplits_" + dateFormatted + ".csv"
				};
		}

		private void exportExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				m_currentAction = CurrentAction.exportAction;
				exportFiles(m_editor);
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("UnableToExportBody", "UnableToExportTitle", ex, "asset funds");
			}
			finally
			{
				m_currentAction = CurrentAction.unknownAction;
				T.X();
			}
		}

		private void newExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				if (frame.SelectList != null & frame.SelectList.Count > 0 && !(frame.SelectList[frame.SelectList.Count - 1] is StaticDataFrame.NewEntity))
				{
//					frame.New();

					//user may have cancelled
					if (frame.SelectList[frame.SelectList.Count - 1] is StaticDataFrame.NewEntity)
					{
						m_editor.New();
					}
				}
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				m_currentAction = CurrentAction.unknownAction;
				T.X();
			}
		}

		private void saveExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				m_currentAction = CurrentAction.saveAction;
				e.Cancel = ! (doSave());
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplaySaveException(assetFundTypeName, ex);
			}
			finally
			{
				m_currentAction = CurrentAction.unknownAction;
				T.X();
			}

		}

/*
		private void entityLoadExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				doEntityLoad();
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
*/

		private void cancelExecuted(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				m_currentAction = CurrentAction.cancelAction;
				doCancel();
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				m_currentAction = CurrentAction.unknownAction;
				T.X();
			}

		}

		private void editor_AllFundGroupsRequested(object sender, AssetFundStaticDataEditor.RequestListArgs e)
		{
			T.E();
			//though this is an event, it is only called from an event caught by the editor,
			//so the editor is responsible for catching the exception & displaying error msg
			try
			{
				/*
				 * TODO list to list can't currently cope with simple lookup on one side and full objects on the other.
				 * What we could do is have two list to lists then translate back to full objects.
				 * For now - is non-optimised - loads up the full collection
				//returns SimpleStringLookup IList
				e.RequestedList = m_fundGroupController.LoadFundGroupLookupsByCompany(m_companyCode);
				*/

				e.RequestedList = m_fundGroupController.LoadFundGroupsByCompanyAndType(m_companyCode, "A");

			}
			finally
			{
				T.X();
			}
		}

		private void editor_AllMarketIndicesRequested(object sender, AssetFundStaticDataEditor.RequestListArgs e)
		{
			T.E();
			//though this is an event, it is only called from an event caught by the editor,
			//so the editor is responsible for catching the exception & displaying error msg
			try
			{
				if (m_lookupController == null)
				{
					m_lookupController = new LookupController();
				}
				e.RequestedList = LookupController.LoadStockMarketIndicesForAssetFundStaticMaintenance(m_connectionString);

			}
			finally
			{
				T.X();
			}
		}

		private void editor_MarketIndicesUpdated(object sender, AssetFundStaticDataEditor.RequestListArgs e)
		{
			T.E();
			//though this is an event, it is only called from an event caught by the editor,
			//so the editor is responsible for catching the exception & displaying error msg
			try
			{
				//create a WeightedMovement Collection for type safety purposes
				IList selectedIndices = e.RequestedList;
				WeightedMovementCollection movements = new WeightedMovementCollection();

			//	string assetFundCode = m_editor.CurrentAssetFund.AssetFundCode;
				for (int i = 0; i < selectedIndices.Count; i++)
				{
					movements.Add((IWeightedMovement) selectedIndices[i]);
				}

				//now pass the collection to the assetFund as this is business logic
				//so best kept there for re-use
				m_editor.CurrentAssetFund.UpdateWeightedMovements(movements);
			}
			finally
			{
				T.X();
			}
		}

		private void editor_AssetFundFundGroupsRequested(object sender, AssetFundStaticDataEditor.RequestListArgs e)
		{
			//though this is an event, it is only called from an event caught by the editor,
			//so the editor is responsible for catching the exception & displaying error msg
			T.E();
			try
			{
				//returns FundGroupCollection IList
				e.RequestedList = m_editor.CurrentAssetFund.FundGroups;
			}
			finally
			{
				T.X();
			}
		}

		private void editor_AssetFundFundsRequested(object sender, AssetFundStaticDataEditor.RequestListArgs e)
		{
			T.E();
			//though this is an event, it is only called from an event caught by the editor,
			//so the editor is responsible for catching the exception & displaying error msg
			try
			{
				/*
				 * TODO list to list can't currently cope with simple lookup on one side and full objects on the other.
				 * What we could do is have two list to lists then translate back to full objects.
				 * For now - is non-optimised - loads up the full collection
				 */

				//we load each time in order to always have as up to date as possible, 
				//unless the user has started to edit any fund data, in which we can't and must hold onto the funds
				//until they hit save
				if (m_cachedFunds == null || !m_editingFunds)
				{
					//refresh the list
					//FundController fundCtrllr = new FundController(m_connectionString);
					m_cachedFunds = FundController.LoadFundsByAssetFund(m_connectionString, m_editor.CurrentAssetFund.AssetFundCode);

					if (m_cachedFunds != null)
					{
						//done this way to avoid circular references between asset funds and funds
						for (int i = 0; i < m_cachedFunds.Count; i++)
						{
							Fund fund = (Fund) m_cachedFunds[i];
							fund.ParentAssetFund = m_editor.CurrentAssetFund;
						}
					}
				}
				e.RequestedList = m_cachedFunds;

			}
			finally
			{
				T.X();
			}
		}

		private void editor_FundsEdited(object sender, EventArgs e)
		{
			m_editingFunds = true;
			//reset back to false on Save or on index change
		}

		#endregion

		private void updateListAfterSave(bool forSaveNew)
		{
			SimpleStringLookup listItem = null;
			if (m_indexChanging)
			{
				if (this.frame.PreviouslySelectedItem != null)
				{
					listItem = (SimpleStringLookup) this.frame.PreviouslySelectedItem;
				}
			}
			else
			{
				if (forSaveNew)
				{
					this.frame.SelectList.Add(new SimpleStringLookup(m_editor.CurrentAssetFund.AssetFundCode, m_editor.CurrentAssetFund.ShortName));
				}
				listItem = (SimpleStringLookup) this.frame.SelectedItem;
			}

			if (listItem != null)
			{
				listItem.DisplayValue = m_editor.CurrentAssetFund.ShortName;
			}
			this.refreshList();

		}

/*
		private void updateListAfterDelete(bool forSaveNew)
		{
			if (this.frame.S
		}
		*/

		private bool doSaveNew()
		{
			T.E();
			bool isValid = true;
			try
			{
				isValid = validateNewAssetFund();
				if (isValid)
				{
					//this causes swap from new asset fund item type to the specific item type
					m_editor.SwapNewAssetFundForTrueAssetFund();

					this.frame.SelectList.RemoveAt(this.frame.SelectList.Count - 1); //clear the NewEntity type & replace with a simplestringlookup
					if (m_editingFunds && m_cachedFunds != null)
					{
						AssetFundController.UpdateAssetFundAndChildFunds(m_connectionString, m_editor.CurrentAssetFund, (FundCollection) m_cachedFunds);
					}
					else
					{
						AssetFundController.UpdateAssetFund(m_connectionString, m_editor.CurrentAssetFund);
					}

					m_editor.Changed = false;

					updateListAfterSave(true);
				}
			}
			finally
			{
				T.X();
			}
			return isValid;
		}


		private bool validateUpdatedAssetFund(out bool amToleranceValid, out bool upperToleranceValid, out bool lowerToleranceValid)
		{
			T.E();
			bool isValid = false;
			try
			{
				AssetFundController.AssetFundFieldValidationError fullNameError; 
				AssetFundController.AssetFundFieldValidationError shortNameError;
				AssetFundController.MarketSplitProportionValidationError mvSplitError;

				FundController.ToleranceValidationError upperToleranceError = FundController.ToleranceValidationError.NoError;
				FundController.ToleranceValidationError lowerToleranceError = FundController.ToleranceValidationError.NoError;
				AssetFundController.ToleranceValidationError assetMovementToleranceError = AssetFundController.ToleranceValidationError.NoError;

				isValid = AssetFundController.ValidateUpdatedAssetFund (this.m_connectionString, m_editor.CurrentAssetFund,
					out fullNameError, out shortNameError, out assetMovementToleranceError, out mvSplitError);

				bool upperToleranceLessThanLowerTolerance = false;
				bool xFactorValid = true;
				bool revalFactorValid = true;
				bool tpeValid = true;
				bool scalingFactorValid = true;
				decimal maxTolerance = 0M;

				if (m_cachedFunds != null)
				{
					isValid = validateChildFunds ((FundCollection)m_cachedFunds, out upperToleranceError, out lowerToleranceError, 
						out upperToleranceLessThanLowerTolerance, out xFactorValid, out revalFactorValid, out tpeValid, out scalingFactorValid, out maxTolerance) && isValid;
				}

				if (!isValid)
				{
					showValidationErrors (true, AssetFundController.AssetFundFieldValidationError.NoError, fullNameError, shortNameError, assetMovementToleranceError, mvSplitError,
						upperToleranceError, lowerToleranceError, upperToleranceLessThanLowerTolerance, xFactorValid, revalFactorValid, tpeValid, maxTolerance);
				}
				amToleranceValid = assetMovementToleranceError == AssetFundController.ToleranceValidationError.NoError;
				upperToleranceValid = upperToleranceError == FundController.ToleranceValidationError.NoError && !upperToleranceLessThanLowerTolerance;
				lowerToleranceValid = lowerToleranceError == FundController.ToleranceValidationError.NoError && !upperToleranceLessThanLowerTolerance;

			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		private bool doSave()
		{
			T.E();
			bool isValid = true;
			try
			{
				if (m_editor.Changed)
				{
					try
					{
						//still retains editor newassetfundtype for new items - we only swap over to specific assetfundtype when validated
						m_editor.UpdateAssetFund();

						if (this.frame.SelectList[this.frame.SelectList.Count - 1] is StaticDataFrame.NewEntity)
						{
							isValid = doSaveNew();
						}
						else
						{
							bool amToleranceValid;
							bool upperToleranceValid;
							bool lowerToleranceValid;
							isValid = validateUpdatedAssetFund(out amToleranceValid, out upperToleranceValid, out lowerToleranceValid);
							if (isValid)
							{
								if (m_editingFunds && m_cachedFunds != null)
								{
									AssetFundController.UpdateAssetFundAndChildFunds(m_connectionString, m_editor.CurrentAssetFund, (FundCollection) m_cachedFunds);
								}
								else
								{
									AssetFundController.UpdateAssetFund(m_connectionString, m_editor.CurrentAssetFund);
								}

								m_editor.Changed = false;
								updateListAfterSave(false);
							}
							else if (m_cachedFunds != null && m_cachedFunds.Count > 0 && (!amToleranceValid || !upperToleranceValid || !lowerToleranceValid))
							{
								m_editor.RefreshValidValuesFromFundsGrid (((FundCollection)m_cachedFunds), amToleranceValid, upperToleranceValid, lowerToleranceValid);
							}

						}
					}
					catch (InvalidFactorException ex)
					{
						isValid = false;
						//TODO - rather than do this by catching exception, perform som UI validation?
						string factorType;

						if (ex is InvalidXFactorException)
						{
							factorType = "x factor";
						}
						else if (ex is InvalidTaxProvisionEstimateException)
						{
							factorType = "tax provision estimate";
						}
						else if (ex is InvalidRevaluationFactorException)
						{
							factorType = "revaluation factor";
						}
						else if (ex is InvalidScalingFactorException)
						{
							factorType = "scaling factor";
						}
						else
						{
							throw new ArgumentException("Invalid factor exception type");
						}
						GUIExceptionHelper.LogAndDisplayException("UnableToSaveAssetFundFactor", "GenericUnableToSaveParmlessTitle2", ex, factorType);
					}
				}
			}
			finally
			{
				T.X();
			}
			return isValid;

		}

		/// <summary>
		/// Only validates the fields we can set on the fund via this asset fund UI
		/// </summary>
		/// <param name="funds"></param>
		/// <param name="upperToleranceError"></param>
		/// <param name="lowerToleranceError"></param>
		/// <param name="upperToleranceLessThanLowerTolerance"></param>
		/// <param name="xFactorValid"></param>
		/// <param name="revalFactorValid"></param>
		/// <param name="tpeValid"></param>
		/// <param name="scalingFactorValid"></param>
		/// <param name="maxTolerance"></param>
		/// <returns></returns>
		private bool validateChildFunds(FundCollection funds,
		                                out FundController.ToleranceValidationError upperToleranceError,
		                                out FundController.ToleranceValidationError lowerToleranceError,
		                                out bool upperToleranceLessThanLowerTolerance,
		                                out bool xFactorValid, out bool revalFactorValid, out bool tpeValid, out bool scalingFactorValid,
		                                out decimal maxTolerance)
		{
			T.E();
			bool isValid = true;
			try
			{
				//set the defaults
				upperToleranceError = FundController.ToleranceValidationError.NoError;
				lowerToleranceError = FundController.ToleranceValidationError.NoError;
				upperToleranceLessThanLowerTolerance = false;
				xFactorValid = true;
				revalFactorValid = true;
				tpeValid = true;
				scalingFactorValid = true;
				maxTolerance = 0M;

				if (funds != null & funds.Count > 0)
				{
					FundController.FundFieldValidationError fundIDError;
					FundController.FundFieldValidationError securityCodeError;
					FundController.FundFieldValidationError fullNameError;
					FundController.FundFieldValidationError shortNameError;
					FundController.FundFieldValidationError assetFundIDError;
					FundController.ClassOrPriceSeriesValidationError classOrSeriesCodeError;
					FundController.FundFieldValidationError externalIDError;
					bool fundTypeSelected;

					FundController fndCtrllr = new FundController(m_connectionString);
					maxTolerance = FundController.MaxTolerance;
					for (int i = 0; i < funds.Count; i++)
					{
						fndCtrllr.ValidateFund (funds[i], out fundIDError, out securityCodeError, out fullNameError, out shortNameError, out assetFundIDError,
							out classOrSeriesCodeError, out externalIDError,
							out upperToleranceError, out lowerToleranceError, out upperToleranceLessThanLowerTolerance, out fundTypeSelected,
							out xFactorValid, out revalFactorValid, out tpeValid, out scalingFactorValid);

						if (upperToleranceError != FundController.ToleranceValidationError.NoError ||
							lowerToleranceError != FundController.ToleranceValidationError.NoError ||
							upperToleranceLessThanLowerTolerance ||
							!xFactorValid ||
							!revalFactorValid ||
							!tpeValid ||
							!scalingFactorValid)
						{
							isValid = false;
							break;
						}
					}

				}
			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		private void showValidationErrors(bool afTypeSelected, AssetFundController.AssetFundFieldValidationError assetFundIDError,
		                                  AssetFundController.AssetFundFieldValidationError fullNameError,
		                                  AssetFundController.AssetFundFieldValidationError shortNameError,
		                                  AssetFundController.ToleranceValidationError assetMovementToleranceError,
		                                  AssetFundController.MarketSplitProportionValidationError mvSplitError,
		                                  FundController.ToleranceValidationError upperToleranceError,
		                                  FundController.ToleranceValidationError lowerToleranceError,
		                                  bool upperToleranceLessThanLowerTolerance,
		                                  bool xFactorValid, bool revalFactorValid, bool tpeValid, decimal maxTolerance)

		{
			T.E();
			try
			{
				string assetFundTypeErrorMessage = null;
				string assetFundIDErrorMessage = null;
				string fullNameErrorMessage = null;
				string shortNameErrorMessage = null;
				string assetMovementToleranceErrorMessage = null;
				string mvSplitErrorMessage = null;

				if (!afTypeSelected)
				{
					assetFundTypeErrorMessage = "An asset fund type must be selected.";
				}

				if (assetFundIDError == AssetFundController.AssetFundFieldValidationError.FieldEmpty)
				{
					assetFundIDErrorMessage = "An asset fund code must be provided.";
				}
				else if (assetFundIDError == AssetFundController.AssetFundFieldValidationError.DuplicateField)
				{
					assetFundIDErrorMessage = "Asset fund with code '" + m_editor.CurrentAssetFund.AssetFundCode + "' already exists in the system.";
				}

				if (fullNameError == AssetFundController.AssetFundFieldValidationError.FieldEmpty)
				{
					fullNameErrorMessage = "A full name must be provided.";
				}
				else if (fullNameError == AssetFundController.AssetFundFieldValidationError.DuplicateField)
				{
					fullNameErrorMessage = "Asset fund with full name '" + m_editor.CurrentAssetFund.FullName + "' already exists in the system.";
				}

				if (shortNameError == AssetFundController.AssetFundFieldValidationError.FieldEmpty)
				{
					shortNameErrorMessage = "A short name must be provided.";
				}
				else if (shortNameError == AssetFundController.AssetFundFieldValidationError.DuplicateField)
				{
					shortNameErrorMessage = "Asset fund with short name '" + m_editor.CurrentAssetFund.ShortName + "' already exists in the system.";
				}

				if (assetMovementToleranceError == AssetFundController.ToleranceValidationError.maxToleranceExceeded)
				{
					assetMovementToleranceErrorMessage = "Asset movement tolerance cannot be higher than " + (maxTolerance*100).ToString("####0.00") + "%.";
				}
				else if (assetMovementToleranceError == AssetFundController.ToleranceValidationError.toleranceNegative)
				{
					assetMovementToleranceErrorMessage = "Asset movement tolerance must be a positive value.";
				}
				else if (assetMovementToleranceError == AssetFundController.ToleranceValidationError.invalidNumDecimalPlaces)
				{
					assetMovementToleranceErrorMessage = "Asset movement tolerance must to maximum of 4 decimal places.";
				}


				if (mvSplitError == AssetFundController.MarketSplitProportionValidationError.lessThan100Percent)
				{
					mvSplitErrorMessage = "The sum of the proportion values is less than 100%. Please adjust the values in the market indices grid.";
				}
				else if (mvSplitError == AssetFundController.MarketSplitProportionValidationError.moreThan100Percent)
				{
					mvSplitErrorMessage = "The sum of the proportion values is greater than 100%. Please adjust the values in the market indices grid.";
				}
				else if (mvSplitError == AssetFundController.MarketSplitProportionValidationError.invalidNumDecimalPlaces)
				{
					mvSplitErrorMessage = "Market split proportion must only be to 4 decimal places maximum. Please adjust the values in the market indices grid.";
				}


				//fund related
				string xFactorErrorMsg = null;
				string revalFactorErrorMsg = null;
				string tpeErrorMsg = null;
				string scalingFactorErrorMsg = null;
				string upperTolErrorMsg = null;
				string lowerTolErrorMsg = null;
				string tolerancesErrorMsg = null;

				if (!xFactorValid)
				{
					xFactorErrorMsg = "X-Factor value is invalid. Please enter a value 0% to +100% to 2 decimal places";
				}

				if (!revalFactorValid)
				{
					revalFactorErrorMsg = "Revaluation factor is invalid. Please enter a value 0% to +100% to 2 decimal places and valid revaluation dates";
				}

				if (!tpeValid)
				{
					tpeErrorMsg = "Tax provision estimate is invalid. Please enter a value 0% to +100% to 2 decimal places";
				}

				/*
				if (!scalingFactorValid)
				{
					scalingFactorErrorMsg = "Scaling factor value is invalid. Please enter a value 0% to +100% to 2 decimal places";
				}
				*/

				if (upperToleranceError == FundController.ToleranceValidationError.MaxToleranceExceeded)
				{
					upperTolErrorMsg = "Upper tolerance cannot be higher than " + maxTolerance.ToString("####0.00##") + ".";
				}
				else if (upperToleranceError == FundController.ToleranceValidationError.ToleranceZero)
				{
					upperTolErrorMsg = "You must specify an Upper Tolerance.";
				}
				else if (upperToleranceError == FundController.ToleranceValidationError.toleranceNegative)
				{
					upperTolErrorMsg = "Upper tolerance must be a positive value.";
				}
				else if (upperToleranceError == FundController.ToleranceValidationError.invalidNumDecimalPlaces)
				{
					upperTolErrorMsg = "Upper tolerance must to maximum of 4 decimal places.";
				}


				if (lowerToleranceError == FundController.ToleranceValidationError.MaxToleranceExceeded)
				{
					lowerTolErrorMsg = "Lower tolerance cannot be higher than " + (maxTolerance*100).ToString("####0.00") + ".";
				}
				else if (lowerToleranceError == FundController.ToleranceValidationError.ToleranceZero)
				{
					lowerTolErrorMsg = "You must specify a lower tolerance.";
				}
				else if (lowerToleranceError == FundController.ToleranceValidationError.toleranceNegative)
				{
					lowerTolErrorMsg = "Lower tolerance must be a positive value.";
				}
				else if (lowerToleranceError == FundController.ToleranceValidationError.invalidNumDecimalPlaces)
				{
					lowerTolErrorMsg = "Lower tolerance must to maximum of 4 decimal places.";
				}


				if (upperToleranceLessThanLowerTolerance)
				{
					tolerancesErrorMsg = "Upper tolerance cannot be lower than the lower tolerance.";
				}

				m_editor.ShowErrors(assetFundIDErrorMessage, fullNameErrorMessage, shortNameErrorMessage, assetFundTypeErrorMessage, mvSplitErrorMessage,
				                    xFactorErrorMsg, revalFactorErrorMsg, tpeErrorMsg, scalingFactorErrorMsg,
				                    upperTolErrorMsg, lowerTolErrorMsg, tolerancesErrorMsg, assetMovementToleranceErrorMessage);

			}
			finally
			{
				T.X();
			}
		}

		private bool validateNewAssetFund()
		{
			T.E();
			bool isValid = false;
			try
			{
				AssetFundController.AssetFundFieldValidationError assetFundIDError;
				AssetFundController.AssetFundFieldValidationError fullNameError;
				AssetFundController.AssetFundFieldValidationError shortNameError;


				//bool weightedMovementsOK = false;
				//WeightedMovementCollection movements = null;

				isValid = AssetFundController.ValidateNewAssetFund(this.m_connectionString, m_editor.CurrentAssetFund.AssetFundCode,
				                                                     m_editor.CurrentAssetFund.FullName, m_editor.CurrentAssetFund.ShortName, out assetFundIDError,
				                                                     out fullNameError, out shortNameError);

				bool afTypeSelected = m_editor.FundsTypeComboSelectedIndex > 0;

				if (!afTypeSelected)
				{
					isValid = false;
				}
				FundController.ToleranceValidationError upperToleranceError = FundController.ToleranceValidationError.NoError;
				FundController.ToleranceValidationError lowerToleranceError = FundController.ToleranceValidationError.NoError;
				bool upperToleranceLessThanLowerTolerance = false;
				bool xFactorValid = true;
				bool revalFactorValid = true;
				bool tpeValid = true;
				bool scalingFactorValid = true;

				decimal maxTolerance = 0M; //only applicable if a tolerance error occurs

				if (m_cachedFunds != null)
				{
					isValid = validateChildFunds((FundCollection) m_cachedFunds, out upperToleranceError, out lowerToleranceError,
					                             out upperToleranceLessThanLowerTolerance, out xFactorValid, out revalFactorValid, out tpeValid, out scalingFactorValid, out maxTolerance) && isValid;
				}

				if (!isValid)
				{
					showValidationErrors(afTypeSelected, assetFundIDError, fullNameError, shortNameError, AssetFundController.ToleranceValidationError.NoError,
					                     AssetFundController.MarketSplitProportionValidationError.NoError,
					                     upperToleranceError, lowerToleranceError, upperToleranceLessThanLowerTolerance, xFactorValid, revalFactorValid, tpeValid, maxTolerance);
				}
			}
			finally
			{
				T.X();
			}
			return isValid;

		}

/*
		private void doEntityLoad()
		{
			if (m_editor.CurrentAssetFund.IsNew)
			{
				//TODO - do we blank out data?
			}
			else
			{
				loadAssetFund(m_editor.CurrentAssetFund.AssetFundCode);
			}
		}
*/

		private void doCancel()
		{
			if (m_editor.CurrentAssetFund.IsNew)
			{
				frame.SelectFirst();
			}
			else
			{
				loadAssetFund(m_editor.CurrentAssetFund.AssetFundCode);
			}
		}

		private void loadAssetFund(string assetFundCode)
		{
			m_editor.CurrentAssetFund = AssetFundController.LoadAssetFundLight(this.m_connectionString, assetFundCode);

		}


	}
}