using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Standard view used for all status views
	/// </summary>
	public class StatusView : UserControl, IRefreshable
	{
		/// <summary>
		/// 
		/// </summary>
		public delegate void RefreshDataDelegate();

		/// <summary>
		/// Used to Notify the lister (possibly the controller) that a refresh of the data is required
		/// </summary>
		public event RefreshDataDelegate RefreshData;

		private Panel pnlTop;
		private Label fundGroupLabel;
		/// <summary>
		/// the fund group combo - made protected for sub class to access
		/// </summary>
		protected ComboBox cmboFundGroup;
		private Panel pnlLeft;
		private CheckedListBox filtersList;
		private Panel pnlBody;
		private Panel pnlBottom;
		private DataGrid theGrid;
		private Button btnShowHideFilters;
		private Splitter splitter;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="StatusView"/> instance.
		/// </summary>
		public StatusView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// 
		/// </summary>
		public void OnRefreshData()
		{
			if (RefreshData != null)
			{
				RefreshData();
			}
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private Button findButton (StatusAction action)
		{
			for (int i = 0; i < pnlBottom.Controls.Count; i++)
			{
				if (pnlBottom.Controls[i] is Button &&  ((Button)pnlBottom.Controls[i]).Text == action.Text)
				{
					return (Button)pnlBottom.Controls[i];
				}
			}
			return null;
		}

		/// <summary>
		/// test each button to see whether it should be enabled/disabled/visible/invisible
		/// </summary>
		public void SetActionButtonVisibility()
		{
			if (actions != null)
			{
				for (int i = 0; i < actions.Length; i++)
				{
					Button actionButton = findButton (actions[i]);
					if (actionButton != null)
					{
						bool isEnabled;
						bool isVisible;
						actions[i].RetrieveCommandVisibility (out isEnabled, out isVisible);
						actionButton.Enabled = isEnabled;
						actionButton.Visible = isVisible;
					}
				}
			}
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlTop = new System.Windows.Forms.Panel();
			this.cmboFundGroup = new System.Windows.Forms.ComboBox();
			this.fundGroupLabel = new System.Windows.Forms.Label();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.filtersList = new System.Windows.Forms.CheckedListBox();
			this.splitter = new System.Windows.Forms.Splitter();
			this.pnlBody = new System.Windows.Forms.Panel();
			this.theGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.btnShowHideFilters = new System.Windows.Forms.Button();
			this.pnlTop.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.pnlBody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.theGrid)).BeginInit();
			this.pnlBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlTop.Controls.Add(this.cmboFundGroup);
			this.pnlTop.Controls.Add(this.fundGroupLabel);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(800, 56);
			this.pnlTop.TabIndex = 0;
			// 
			// cmboFundGroup
			// 
			this.cmboFundGroup.DisplayMember = "DisplayValue";
			this.cmboFundGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmboFundGroup.Location = new System.Drawing.Point(128, 16);
			this.cmboFundGroup.Name = "cmboFundGroup";
			this.cmboFundGroup.Size = new System.Drawing.Size(336, 21);
			this.cmboFundGroup.TabIndex = 4;
			this.cmboFundGroup.ValueMember = "Key";
			this.cmboFundGroup.SelectedValueChanged += new System.EventHandler(this.cmboFundGroup_SelectedValueChanged);
			// 
			// fundGroupLabel
			// 
			this.fundGroupLabel.AutoSize = true;
			this.fundGroupLabel.Location = new System.Drawing.Point(8, 16);
			this.fundGroupLabel.Name = "fundGroupLabel";
			this.fundGroupLabel.Size = new System.Drawing.Size(68, 16);
			this.fundGroupLabel.TabIndex = 3;
			this.fundGroupLabel.Text = "Fund Group:";
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.filtersList);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 56);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(176, 432);
			this.pnlLeft.TabIndex = 3;
			this.pnlLeft.Visible = false;
			// 
			// filtersList
			// 
			this.filtersList.CheckOnClick = true;
			this.filtersList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filtersList.Location = new System.Drawing.Point(0, 0);
			this.filtersList.Name = "filtersList";
			this.filtersList.Size = new System.Drawing.Size(176, 424);
			this.filtersList.TabIndex = 0;
			this.filtersList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.filtersList_ItemCheck);
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(176, 56);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(8, 432);
			this.splitter.TabIndex = 4;
			this.splitter.TabStop = false;
			this.splitter.Visible = false;
			// 
			// pnlBody
			// 
			this.pnlBody.Controls.Add(this.theGrid);
			this.pnlBody.Controls.Add(this.pnlBottom);
			this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBody.Location = new System.Drawing.Point(184, 56);
			this.pnlBody.Name = "pnlBody";
			this.pnlBody.Size = new System.Drawing.Size(616, 432);
			this.pnlBody.TabIndex = 5;
			// 
			// theGrid
			// 
			this.theGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.theGrid.BackColor = System.Drawing.SystemColors.Window;
			this.theGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.theGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.theGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.theGrid.DataMember = "";
			this.theGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.theGrid.FlatMode = false;
			this.theGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.theGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.theGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.theGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.theGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.theGrid.Location = new System.Drawing.Point(0, 0);
			this.theGrid.Name = "theGrid";
			this.theGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.theGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.theGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.theGrid.PrintColumnSettings = null;
			this.theGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.theGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.theGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.theGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.theGrid.Size = new System.Drawing.Size(616, 400);
			this.theGrid.TabIndex = 1;
			// 
			// pnlBottom
			// 
			this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
			this.pnlBottom.Controls.Add(this.btnShowHideFilters);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 400);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(616, 32);
			this.pnlBottom.TabIndex = 0;
			// 
			// btnShowHideFilters
			// 
			this.btnShowHideFilters.Location = new System.Drawing.Point(16, 4);
			this.btnShowHideFilters.Name = "btnShowHideFilters";
			this.btnShowHideFilters.Size = new System.Drawing.Size(130, 23);
			this.btnShowHideFilters.TabIndex = 0;
			this.btnShowHideFilters.Text = "<< &Hide Filters";
			this.btnShowHideFilters.Visible = false;
			this.btnShowHideFilters.Click += new System.EventHandler(this.btnShowHideFilters_Click);
			// 
			// StatusView
			// 
			this.Controls.Add(this.pnlBody);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.pnlLeft);
			this.Controls.Add(this.pnlTop);
			this.Name = "StatusView";
			this.Size = new System.Drawing.Size(800, 488);
			this.pnlTop.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.pnlBody.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.theGrid)).EndInit();
			this.pnlBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the list of fund groups displayed in the combo box at top
		/// </summary>
		/// <value></value>
		public virtual SimpleLookupCollection FundGroupLookups
		{
			get
			{
				if (cmboFundGroup.DataSource != null && cmboFundGroup.DataSource is SimpleLookupCollection)
				{
					return (SimpleLookupCollection) cmboFundGroup.DataSource;
				}
				else
				{
					return null;
				}
			}

			set
			{
				cmboFundGroup.DataSource = value;
				if (cmboFundGroup.Items.Count > 0)
				{
					cmboFundGroup.SelectedIndex = 0;
				}
				else
				{
					cmboFundGroup.SelectedIndex = -1;
				}
			}
		}

		private IList data = null;

		/// <summary>
		/// Gets or sets the data displayed in the grid as an IList.
		/// </summary>
		/// <value></value>
		public IList Data
		{
			get { return data; }
			set
			{
				data = value;
				theGrid.BindToCustomCollection(data);

				theGrid.ColumnHeadersVisible=(data != null && data.Count>0);
			}
		}

		internal DataGrid grid
		{
			get { return this.theGrid; }
		}

		private StatusAction[] actions;

		/// <summary>
		/// Sets the list of allowable actions in the view. Buttons are created sequentially from the array
		/// from left to right.
		/// </summary>
		public StatusAction[] Actions
		{
			set
			{
				removeActionButtons();
				actions = value;
				createActionButtons();
			}
		}

		private Button[] actionButtons = null;

		private void removeActionButtons()
		{
			if (actionButtons != null && actionButtons.Length > 0)
			{
				this.SuspendLayout();
				try
				{
					foreach (Button btn in actionButtons)
					{
						btn.Parent.Controls.Remove(btn);
					}
				}
				finally
				{
					this.ResumeLayout();
				}
			}
		}

		private void createActionButtons()
		{
			const int btnWidth = 100;
			const int btnSpacing = 5;

			if (actions != null && actions.Length > 0)
			{
				this.SuspendLayout();
				try
				{
					actionButtons = new Button[actions.Length];

					int totalBtnLeft = pnlBottom.ClientSize.Width - ((btnWidth + btnSpacing)*actions.Length);

					for (int i = 0; i < actions.Length; i++)
					{
						Button btn = new Button();
						btn.Tag = actions[i];
						btn.Click += new EventHandler(actionButtonClick);

						btn.Text = actions[i].Text;
						btn.Top = 4;
						btn.Width = btnWidth;
						btn.Left = totalBtnLeft + (i*(btnWidth + btnSpacing));
						btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

						pnlBottom.Controls.Add(btn);
						actionButtons[i] = btn;
					}
				}
				finally
				{
					this.ResumeLayout(true);
				}
			}
		}

		private void actionButtonClick(object sender, EventArgs e)
		{
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			try
			{
				Button btn = (Button) sender;
				StatusAction action = (StatusAction) btn.Tag;
				action.Execute(e);
			}
			finally
			{
				this.Cursor = oldCursor;
			}
		}


		private StatusFilterCollection filters;

		/// <summary>
		/// Gets and sets the list of filters to show in the gui.
		/// </summary>
		/// <value></value>
		public StatusFilterCollection Filters
		{
			get { return filters; }
			set
			{
				filters = value;
				displayFilters();
				if (filters != null)
				{
					filters.ContentsChanged += new EventHandler(filters_ContentsChanged);
				}
			}
		}

		private void displayFilters()
		{
			T.E();

			filtersList.SuspendLayout();
			try
			{
				filtersList.Items.Clear();

				if (Filters != null)
				{
					foreach (IStatusFilter filter in Filters)
					{
						if (filter.DisplayInGui)
						{
							filtersList.Items.Add(filter, filter.Applied);
						}
					}
				}

				pnlLeft.Visible = (filtersList.Items.Count > 0);
				splitter.Visible = (filtersList.Items.Count > 0);
				btnShowHideFilters.Visible = (filtersList.Items.Count > 0);
			}
			finally
			{
				filtersList.ResumeLayout();
			}
			T.X();
		}

		#endregion Properties

		#region Events

		/// <summary>
		/// Event raised when the fund group in the gui is changed
		/// </summary>
		public event SimpleLookupHandler FundGroupChanged;

		private void onFundGroupChanged(SimpleLookupEventArgs e)
		{
			T.E();
			if (FundGroupChanged != null)
			{
				FundGroupChanged(this, e);
			}
			T.X();
		}

		#endregion Events

		#region Event Handlers

		/// <summary>
		/// The event that is fired when user selects a new fund group
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmboFundGroup_SelectedValueChanged(object sender, System.EventArgs e)
		{
			T.E();

			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			try
			{
				SimpleLookupEventArgs args;
				if (cmboFundGroup.SelectedItem != null && cmboFundGroup.SelectedItem is SimpleLookup)
				{
					args = new SimpleLookupEventArgs((SimpleLookup) cmboFundGroup.SelectedItem);
				}
				else
				{
					args = new SimpleLookupEventArgs(null);
				}
				onFundGroupChanged(args);
			}
			finally
			{
				this.Cursor = oldCursor;
			}
			T.X();
		}

		private void filters_ContentsChanged(object sender, EventArgs e)
		{
			T.E();
			displayFilters();
			T.X();
		}

		private void btnShowHideFilters_Click(object sender, EventArgs e)
		{
			if (pnlLeft.Visible)
			{
				pnlLeft.Hide();
				splitter.Hide();
				btnShowHideFilters.Text = ">> Show Filters";
			}
			else
			{
				pnlLeft.Show();
				splitter.Show();
				btnShowHideFilters.Text = "<< Hide Filters";
			}
		}

		private void filtersList_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			IStatusFilter filter = (IStatusFilter) filtersList.Items[e.Index];
			filter.Applied = (e.NewValue == CheckState.Checked);
		}

		#endregion
	}
}