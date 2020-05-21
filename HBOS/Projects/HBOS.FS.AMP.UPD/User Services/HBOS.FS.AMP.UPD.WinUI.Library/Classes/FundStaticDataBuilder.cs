using System;
using System.Collections;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for FundStaticDataBuilder.
	/// </summary>
	public class FundStaticDataBuilder : IStaticDataBuilder
	{
		/// <summary>
		/// Creates a new <see cref="FundStaticDataBuilder"/> instance.
		/// </summary>
		public FundStaticDataBuilder()
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
			editor = new FundStaticDataEditor();
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
			FundController fundController = new FundController(GlobalRegistry.ConnectionString);
			IList funds = fundController.LoadFundLookupsByCompany(GlobalRegistry.CompanyCode);
			listManager.RefreshContents(funds);
			T.X();
		}


		private FundStaticDataEditor editor;

		#endregion
	}
}