using System;
using System.Collections;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for CurrencyStaticDataBuilder.
	/// </summary>
	public class CurrencyStaticDataBuilder : IStaticDataBuilder
	{
		private CurrencyStaticDataEditor m_editor = null;

		#region Constructor
		/// <summary>
		/// Creates a new <see cref="CurrencyStaticDataBuilder"/> instance.
		/// </summary>
		public CurrencyStaticDataBuilder()
		{
		}

		#endregion

		#region IStaticDataBuilder Members

		/// <summary>
		/// Configures the frame.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public void ConfigureFrame(StaticDataFrame frame)
		{
			// CurrencyStaticDataBuilder.ConfigureFrame implementation
			frame.DisplayMember = "DisplayText";
		}

		/// <summary>
		/// Gets the actions.
		/// </summary>
		public StaticDataAction[] GetActions()
		{
			// TODO:  Add CurrencyStaticDataBuilder.GetActions implementation
			T.E();
			StaticDataAction[] actions = new StaticDataAction[3];

//			actions[0] = new StaticDataAction();
//			actions[0].Text = "&Delete";
//			actions[0].Executed += new EventHandler(m_editor.deleteExecuted);

//			actions[1] = new StaticDataAction();
//			actions[1].Text = "&Export";
//			actions[1].Executed += new EventHandler(m_editor.exportExecuted);

			actions[0] = new StaticDataAction();
			actions[0].Text = "&New";
			actions[0].Executed += new EventHandler(m_editor.newExecuted);

			actions[1] = new StaticDataAction();
			actions[1].Text = "&Save";
			actions[1].Executed += new EventHandler(m_editor.saveExecuted);

			actions[2] = new StaticDataAction();
			actions[2].Text = "&Cancel";
			actions[2].Executed += new EventHandler(m_editor.cancelExecuted);

			T.X();

			return actions;
		}

		/// <summary>
		/// Gets the entity editor.
		/// </summary>
		/// <returns></returns>
		public StaticDataEditor GetEntityEditor()
		{
			// TODO:  Add CurrencyStaticDataBuilder.GetEntityEditor implementation
			// User control
			T.E();
			try
			{
				m_editor = new CurrencyStaticDataEditor();
				m_editor.Dock = DockStyle.Fill;
			}
			finally
			{
				T.X();
			}
			return m_editor;
		}

		/// <summary>
		/// Configures the list manager.
		/// </summary>
		/// <param name="listManager">List manager.</param>
		public void ConfigureListManager(StaticDataListManager listManager)
		{
			T.E();

			try
			{
				// Add CurrencyStaticDataBuilder.ConfigureListManager implementation
				LookupController lookupController=new LookupController();
				IList currencies = StaticDataCurrencyLookupDecorator.ToDecoratedList( lookupController.LoadCurrencies(GlobalRegistry.ConnectionString));
				ArrayList currenciesArrayList = new ArrayList(currencies);
				listManager.RefreshContents(currenciesArrayList);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}