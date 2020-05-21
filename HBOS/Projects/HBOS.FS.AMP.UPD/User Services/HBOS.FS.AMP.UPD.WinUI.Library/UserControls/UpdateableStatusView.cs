using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Interfaces;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.Support.Tex;


namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for UpdateableStatusView.
	/// </summary>
	public class UpdateableStatusView : StatusView, IUPDControl
	{
		#region Member Variables

		private bool m_changed = false;
		private int m_lastSelectedFundGroup = -1;
		private bool m_systemGeneratedEvent = false;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Ctor

		/// <summary>
		/// Constructs a new UpdateableStatusView
		/// </summary>
		public UpdateableStatusView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent call

		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Allows the entity to change.
		/// </summary>
		/// <returns></returns>
		protected YesNoCancelAction allowMenuChange()
		{
			T.E();
			//default to 'no we don't want to save'
			YesNoCancelAction userAction  = YesNoCancelAction.no;
			try
			{
				if (m_changed)
				{
					userAction = SaveDialog.Show();
					if (userAction == YesNoCancelAction.yes)
					{
						EventArgs e = new EventArgs();	
						OnSaveExecuted(e); //pass back to the editor to do a save
					}
				}
			}
			finally
			{
				T.X();
			}
			return YesNoCancelAction.no;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets or sets the list of fund groups displayed in the combo box at top
		/// </summary>
		/// <value></value>
		public override SimpleLookupCollection FundGroupLookups
		{
			get 
			{
				return base.FundGroupLookups;
			}

			set	
			{
				base.FundGroupLookups = value;
				m_lastSelectedFundGroup = cmboFundGroup.SelectedIndex;
			}
		}

		#region Event Handlers
				
		/// <summary>
		/// The event that is fired when user selects a new fund group
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void cmboFundGroup_SelectedValueChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				if (!m_systemGeneratedEvent)
				{
					if (m_lastSelectedFundGroup < 0 || AllowMenuChange())
					{
						base.cmboFundGroup_SelectedValueChanged(sender, e);
						m_lastSelectedFundGroup = cmboFundGroup.SelectedIndex;
					}
					else if (m_lastSelectedFundGroup >= 0)
					{
						m_systemGeneratedEvent = true;
						cmboFundGroup.SelectedIndex = m_lastSelectedFundGroup;
					}
				}
			}
			finally
			{
				m_systemGeneratedEvent = false;
				T.X();
			}
		}

		#endregion

		#endregion

		#region IUPDControl

		/// <summary>
		/// implmentation of IUPDControl method. Only allows menu to change if nothing has changed or user saves or cancels. 
		/// </summary>
		public bool AllowMenuChange()
		{
			bool allowChange = true;
			if (m_changed)
			{
				YesNoCancelAction ync = allowMenuChange();
				//allow it if the user hasn't pressed cancel or if the event has been cancelled
				allowChange = (ync != YesNoCancelAction.cancel);
			}

			return allowChange;
		}

		#endregion

		#region Properties

		/// <summary>
		/// gets/sets the changed flag
		/// </summary>
		public bool Changed
		{
			get 
			{
				return m_changed;
			}
			set
			{
				m_changed = value;
			}
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Events

		/// <summary>
		/// Event raised when the GUI needs to save via the UI controller
		/// </summary>
		public event EventHandler SaveExecuted;

		private void OnSaveExecuted(EventArgs e)
		{
			if (this.SaveExecuted != null)
				this.SaveExecuted(this, e);
		}

		#endregion
	}
}
