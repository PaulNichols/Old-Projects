using System.Collections;
using System.Configuration;
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
	/// View controller for fund group static data maintenance
	/// </summary>
	public class FundGroupStaticDataViewController : StaticDataViewController
	{
		#region Member Variables

	//	private const string fundGroupTypeName = "fund group";

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new <see cref="FundGroupStaticDataViewController"/> instance.
		/// </summary>
		/// <param name="frame">The frame to display in</param>
		internal FundGroupStaticDataViewController(StaticDataFrame frame) : base(frame)
		{
			T.E();
			UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
			companyCode = updPrincipal.CompanyCode;

			fundGroupController = new FundGroupController(connectionString);
			//distFileController = new DistributionFileController(connectionString);

			loadFundGroupLookups();
			T.X();
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets the export data.
		/// </summary>
		/// <param name="exportColl">Export coll.</param>
		/// <param name="xsltFiles">XSLT files.</param>
		/// <param name="exportFileNames">Export file names.</param>
		protected override void getExportData(out IList exportColl, out string[] xsltFiles, out string[] exportFileNames)
		{
			exportColl = null;
			xsltFiles = null;
			exportFileNames = null;
		}


		/// <summary>
		/// Handles state change when the entity being viewed in changed
		/// </summary>
		/// <param name="newEntity">The new entity being viewed.</param>
		protected override void EntityChanged(object newEntity)
		{
			T.E();
			SimpleLookup newLookup = newEntity as SimpleLookup;
			if (newLookup != null)
			{
				loadFundGroup(newLookup.Key);
			}
			T.X();
		}

		/// <summary>
		/// Gets the editor for the entity.
		/// </summary>
		/// <returns>A user control to use as the editor</returns>
		protected override UserControl GetEntityEditor()
		{
			T.E();
//			editor = new FundGroupStaticDataEditor();
//			editor.Dock = DockStyle.Fill;
//			editor.AllDistributionFilesRequested +=new RequestListHandler(editor_AllDistributionFilesRequested);
//			editor.FundGroupDistributionFileRequested += new RequestListHandler(editor_FundGroupDistributionFileRequested);
//			editor.SaveExecuted += new StaticDataEventHandler(saveExecuted);
			T.X();
			return null;
		}

		/// <summary>
		/// Gets the entity collection for display. Each entity's ToString() method is used to determine display.
		/// </summary>
		/// <returns></returns>
		protected override IList GetEntityCollection()
		{
			return fundGroups;
		}

		/// <summary>
		/// Gets an array of allowable actions in the GUI, you should hook to the Executed event to
		/// act on the action from the GUI.
		/// </summary>
		protected override StaticDataAction[] GetActions()
		{
			T.E();
			StaticDataAction[] result = new StaticDataAction[0];
			T.X();
			return result;
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
				return editor.AllowEntityToChange();
			}
			finally
			{
				T.X();
			}

		}

		#endregion Overrides

		#region Privates

		private SimpleLookupCollection fundGroups;
		internal FundGroupStaticDataEditor editor;
		private FundGroupController fundGroupController;
		//private DistributionFileController distFileController;
		private string companyCode;
		private string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

		private void loadFundGroupLookups()
		{
			T.E();
			fundGroups = fundGroupController.LoadFundGroupLookupsByCompany(companyCode);
			T.X();
		}


/*
		private void doCancel()
		{
			T.E();
			if (isNew())
			{
				fundGroups.RemoveAt(fundGroups.Count - 1);
				refreshList();
			}
			else
			{
				loadFundGroup(editor.CurrentFundGroup.ID);
			}
			T.X();
		}
*/

		private void loadFundGroup(int fundGroupId)
		{
			T.E();
			editor.CurrentFundGroup = fundGroupController.LoadFundGroupStaticData(fundGroupId);
			T.X();
		}

		#endregion Privates
	}
}