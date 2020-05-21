using System;
using System.Collections;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for PriceFileStaticDataBuilder.
	/// </summary>
	public class PriceFileStaticDataBuilder : IStaticDataBuilder
	{
		private PriceFileStaticDataEditor m_editor = null;

		#region Constructor
		/// <summary>
		/// Creates a new <see cref="PriceFileStaticDataBuilder"/> instance.
		/// </summary>
		public PriceFileStaticDataBuilder()
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
			// PriceFileStaticDataBuilder.ConfigureFrame implementation
			frame.DisplayMember = "DisplayValue";
		}

		/// <summary>
		/// Gets the actions.
		/// </summary>
		public StaticDataAction[] GetActions()
		{
			// TODO:  Add PriceFileStaticDataBuilder.GetActions implementation
			T.E();
			StaticDataAction[] actions = new StaticDataAction[4];

			actions[0] = new StaticDataAction();
			actions[0].Text = "&Delete";
			actions[0].Executed += new EventHandler(m_editor.deleteExecuted);

//			actions[1] = new StaticDataAction();
//			actions[1].Text = "&Export";
//			actions[1].Executed += new EventHandler(m_editor.exportExecuted);

			actions[1] = new StaticDataAction();
			actions[1].Text = "&New";
			actions[1].Executed += new EventHandler(m_editor.newExecuted);

			actions[2] = new StaticDataAction();
			actions[2].Text = "&Save";
			actions[2].Executed += new EventHandler(m_editor.saveExecuted);

			actions[3] = new StaticDataAction();
			actions[3].Text = "&Cancel";
			actions[3].Executed += new EventHandler(m_editor.cancelExecuted);

			T.X();

			return actions;
		}

		/// <summary>
		/// Gets the entity editor.
		/// </summary>
		/// <returns></returns>
		public StaticDataEditor GetEntityEditor()
		{
			// TODO:  Add PriceFileStaticDataBuilder.GetEntityEditor implementation
			// User control
			T.E();
			try
			{
				m_editor = new PriceFileStaticDataEditor();
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
				// Add PriceFileStaticDataBuilder.ConfigureListManager implementation
				IList priceFiles = StaticDataPriceFileLookupDecorator.ToDecoratedList( LookupController.LoadPriceFiles(GlobalRegistry.ConnectionString,GlobalRegistry.CompanyCode));
				ArrayList priceFilesArrayList = new ArrayList(priceFiles);
				listManager.RefreshContents(priceFilesArrayList);

				if (priceFilesArrayList.Count==0)
				{
					m_editor.newExecuted(null,null);
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}