using System;
using System.Windows.Forms;
using System.Collections;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for FundGroupStaticDataBuilder.
	/// </summary>
	public class FundGroupStaticDataBuilder : IStaticDataBuilder
	{
		/// <summary>
		/// Creates a new <see cref="FundGroupStaticDataBuilder"/> instance.
		/// </summary>
		public FundGroupStaticDataBuilder()
		{
			T.E();
			T.X();
		}

		#region IStaticDataBuilder Members

		/// <summary>
		/// Configures the frame.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public void ConfigureFrame(StaticDataFrame frame)
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Gets the actions for this form.
		/// </summary>
		public StaticDataAction[] GetActions()
		{
			T.E();
			StaticDataAction[] actions = new StaticDataAction[5];

			actions[0] = new StaticDataAction();
			actions[0].Text = "&Delete";
			actions[0].Executed += new EventHandler(editor.deleteExecuted);

			actions[1] = new StaticDataAction();
			actions[1].Text = "&Export";
			actions[1].Executed += new EventHandler(editor.exportExecuted);

			actions[2] = new StaticDataAction();
			actions[2].Text = "&New";
			actions[2].Executed += new EventHandler(editor.newExecuted);

			actions[3] = new StaticDataAction();
			actions[3].Text = "&Save";
			actions[3].Executed += new EventHandler(editor.saveExecuted);

			actions[4] = new StaticDataAction();
			actions[4].Text = "&Cancel";
			actions[4].Executed += new EventHandler(editor.cancelExecuted);

			T.X();

			return actions;
		}

		/// <summary>
		/// Gets the entity editor.
		/// </summary>
		/// <returns></returns>
		public StaticDataEditor GetEntityEditor()
		{
			T.E();
			editor = new FundGroupStaticDataEditor();
			editor.Dock = DockStyle.Fill;
//			editor.AllDistributionFilesRequested +=new RequestListHandler(editor_AllDistributionFilesRequested);
//			editor.FundGroupDistributionFileRequested += new RequestListHandler(editor_FundGroupDistributionFileRequested);
//			editor.SaveExecuted += new EventHandler(saveExecuted);
			T.X();
			return editor;
		}

		/// <summary>
		/// Configure listManager so it contains the neccessary data for the current static data screen
		/// </summary>
		/// <param name="listManager">List manager instance to configure.</param>
		public void ConfigureListManager(StaticDataListManager listManager)
		{
			T.E();

			FundGroupController fundGroupController = new FundGroupController(GlobalRegistry.ConnectionString);
			IList fundGroups = fundGroupController.LoadFundGroupLookupsByCompany(GlobalRegistry.CompanyCode);
			listManager.RefreshContents(fundGroups);
			T.X();
		}

		private FundGroupStaticDataEditor editor;

		#endregion
	}
}