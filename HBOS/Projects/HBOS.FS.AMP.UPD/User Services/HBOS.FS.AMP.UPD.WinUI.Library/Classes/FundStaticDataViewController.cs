using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// View controller for fund static data maintenance
	/// </summary>
	public class FundStaticDataViewController : StaticDataViewController
	{
		#region Construction

		/// <summary>
		/// Creates a new <see cref="FundStaticDataViewController"/> instance.
		/// </summary>
		/// <param name="frame">The frame to display in</param>
		internal FundStaticDataViewController(StaticDataFrame frame) : base(frame)
		{
			UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
			companyCode = updPrincipal.CompanyCode;

			//assetFundController = new AssetFundController();
			//fundGroupController = new FundGroupController(connectionString);

			fundController = new FundController(connectionString);
			loadFundLookups();
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets the data for export
		/// </summary>
		/// <param name="exportColl"></param>
		/// <param name="xsltFiles"></param>
		/// <param name="exportFileNames"></param>
		protected override void getExportData(out IList exportColl, out string[] xsltFiles, out string[] exportFileNames)
		{
			exportColl = fundController.LoadFundsByCompany(companyCode);

			xsltFiles = new string[]
				{
					"HBOS.FS.AMP.UPD.WinUI.Classes.FundStaticDataFundGroups.xslt",
					"HBOS.FS.AMP.UPD.WinUI.Classes.FundStaticDataSystems.xslt"
				};

			string dateFormatted = String.Format("{0:yyyyMMdd}", DateTime.Now.Date);
			exportFileNames = new string[]
				{
					companyCode + "_funds_fundgroupmembership_" + dateFormatted + ".csv",
					companyCode + "_funds_systems_" + dateFormatted + ".csv"
				};
		}


		/// <summary>
		/// Handles state change when the entity being viewed in changed
		/// </summary>
		/// <param name="newEntity">The new entity being viewed.</param>
		protected override void EntityChanged(object newEntity)
		{
			if (!this.isNew())
			{
				editor.CurrentFund = fundController.LoadStaticData(((SimpleStringLookup) newEntity).Key);
			}
		}

		/// <summary>
		/// Gets the editor for the entity.
		/// </summary>
		/// <returns>A user control to use as the editor</returns>
		protected override UserControl GetEntityEditor()
		{
			editor = new FundStaticDataEditor();
			editor.Dock = DockStyle.Fill;
			//editor.SaveExecuted += new StaticDataEventHandler (saveExecuted);
			editor.Initialise();
			return editor;
		}

		/// <summary>
		/// Gets the entity collection for display. Each entity's ToString() method is used to determine display.
		/// </summary>
		/// <returns></returns>
		protected override IList GetEntityCollection()
		{
			return funds;
		}

		/// <summary>
		/// Provides a custom initialisation point that can be overriden, does nothing in the default implementation
		/// </summary>
		protected override void CustomInitialisation()
		{
			//does nothing
		}

		/// <summary>
		/// Gets an array of allowable actions in the GUI, you should hook to the Executed event to
		/// act on the action from the GUI.
		/// </summary>
		protected override StaticDataAction[] GetActions()
		{
			StaticDataAction[] actions = new StaticDataAction[0];

//			actions[0] = new StaticDataAction();
//			actions[0].Text = "&Delete";
//			actions[0].Executed +=new StaticDataEventHandler(deleteExecuted);
//
//			actions[1] = new StaticDataAction();
//			actions[1].Text = "&Export";
//			actions[1].Executed +=new StaticDataEventHandler(exportExecuted);
//			
//			actions[2] = new StaticDataAction();
//			actions[2].Text = "&New";
//			actions[2].Executed +=new StaticDataEventHandler(newExecuted);
//
//			actions[3] = new StaticDataAction();
//			actions[3].Text = "&Save";
//			actions[3].Executed +=new StaticDataEventHandler(saveExecuted);
//
//			actions[4] = new StaticDataAction();
//			actions[4].Text = "&Cancel";
//			actions[4].Executed +=new StaticDataEventHandler(cancelExecuted);

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
					return editor.AllowEntityToChange();
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Privates

		internal FundStaticDataEditor editor;
		private string companyCode;
		private string connectionString = GlobalRegistry.ConnectionString;
		private FundController fundController;
		//private AssetFundController assetFundController;
	//	private FundGroupController fundGroupController;
		private SimpleStringLookupCollection funds = new SimpleStringLookupCollection();

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

		private void loadFundLookups()
		{
			funds = fundController.LoadFundLookupsByCompany(companyCode);
			frame.SelectList = funds;
		}

		#endregion 
	}
}