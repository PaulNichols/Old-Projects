using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Interfaces;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Provides the generic GUI for editing static data
	/// </summary>
	public class StaticDataFrame : UserControl, IUPDControl
	{
		#region Member Variables

		private StaticDataAction[] actions;

		#endregion

		#region Controls

		private Panel bottomPanel;

		private ListBox itemList;
		private Splitter splitter;
		private Panel bodyPanel;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public StaticDataFrame()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion

		#region Dispose

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

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.itemList = new System.Windows.Forms.ListBox();
			this.splitter = new System.Windows.Forms.Splitter();
			this.bodyPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// bottomPanel
			// 
			this.bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 432);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(736, 32);
			this.bottomPanel.TabIndex = 50;
			// 
			// itemList
			// 
			this.itemList.Dock = System.Windows.Forms.DockStyle.Left;
			this.itemList.Location = new System.Drawing.Point(0, 0);
			this.itemList.Name = "itemList";
			this.itemList.Size = new System.Drawing.Size(144, 420);
			this.itemList.TabIndex = 1;
			this.itemList.SelectedIndexChanged += new System.EventHandler(this.itemList_SelectedIndexChanged);
			// 
			// splitter
			// 
			this.splitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter.Location = new System.Drawing.Point(144, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(8, 432);
			this.splitter.TabIndex = 54;
			this.splitter.TabStop = false;
			// 
			// bodyPanel
			// 
			this.bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bodyPanel.Location = new System.Drawing.Point(152, 0);
			this.bodyPanel.Name = "bodyPanel";
			this.bodyPanel.Size = new System.Drawing.Size(584, 432);
			this.bodyPanel.TabIndex = 55;
			// 
			// StaticDataFrame
			// 
			this.Controls.Add(this.bodyPanel);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.itemList);
			this.Controls.Add(this.bottomPanel);
			this.Name = "StaticDataFrame";
			this.Size = new System.Drawing.Size(736, 464);
			this.ResumeLayout(false);

		}

		#endregion

		#region Properties

		private StaticDataListManager listManager;
		
		/// <summary>
		/// Gets or sets the list manager.
		/// </summary>
		/// <value></value>
		public StaticDataListManager ListManager
		{
			get { return listManager; }
			set 
			{ 
				listManager = value;
				if (listManager != null)
				{
					listManager.ContentsRefreshed +=new EventHandler(listManager_ContentsRefreshed);
					listManager.SelectedIndexChanged += new EventHandler(listManager_SelectedIndexChanged);
				}
			}
		}

		/// <summary>
		/// Sets the list of allowable actions in the frame. Buttons are created sequentially from the array
		/// from left to right.
		/// </summary>
		/// <value></value>
		public StaticDataAction[] Actions
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

					int totalBtnLeft = bottomPanel.ClientSize.Width - ((btnWidth + btnSpacing)*actions.Length);

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

						bottomPanel.Controls.Add(btn);
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
				StaticDataAction action = (StaticDataAction) btn.Tag;
				action.Execute(EventArgs.Empty);
			}
			finally
			{
				this.Cursor = oldCursor;
			}
		}


		private void refreshList()
		{
			itemList.SuspendLayout();
			try
			{
				itemList.Items.Clear();

				foreach (object o in ListManager.Items)
				{
					string dispValue = "*** ERROR ***";
					if (this.DisplayMember == string.Empty)
					{
						dispValue = o.ToString();
					}
					else
					{
						PropertyInfo pi = o.GetType().GetProperty(this.DisplayMember);
						if (pi != null)
						{
							object propValue = pi.GetValue(o, null);
							if (propValue == null)
							{
								dispValue = "NULL";
							}
							else
							{
								dispValue = propValue.ToString();
							}
						}
						else
						{
							dispValue = o.ToString();
						}
					}
					itemList.Items.Add(dispValue);
				}

			}
			finally
			{
				itemList.ResumeLayout();
			}
		}

		private void autosizeListWidthToContents()
		{
			int width = itemList.Width;

			using (Graphics grph = itemList.CreateGraphics())
			{
				foreach (object item in itemList.Items)
				{
					string itemText = itemList.GetItemText(item);
					width = Math.Max(width, (int) grph.MeasureString(itemText, itemList.Font).Width);
				}
			}
			itemList.Width = width;
		}

		/// <summary>
		/// Gets or sets the property name to display in the lookup list on the left-hand side of
		/// the screen. If no value is specified, the object's ToString() method is used.
		/// </summary>
		/// <value></value>
		public string DisplayMember
		{
			get { return itemList.DisplayMember; }
			set { itemList.DisplayMember = value; }
		}

		private UserControl body;

		/// <summary>
		/// Gets or sets the user control displayed in the central area of the form.
		/// </summary>
		/// <value></value>
		public UserControl Body
		{
			get { return body; }
			set
			{
				clearBody();
				body = value;
				showBody();
			}
		}

		private void showBody()
		{
			if (Body != null)
			{
				this.Controls.Add(Body);
				Body.Parent = this.bodyPanel;
				Body.Show();
			}
		}

		private void clearBody()
		{
			if (Body != null)
			{
				Body.Hide();
				Body.Parent = null;
				this.Controls.Remove(Body);
			}
		}

		#endregion Properties

		#region IUPDControl

		/// <summary>
		/// implmentation of IUPDControl method. Only allows menu to change if nothing has changed or user saves or cancels. 
		/// </summary>
		public bool AllowMenuChange()
		{
			//forward on to the entity itself
			IUPDControl updControl = this.body as IUPDControl;
			if (updControl != null)
			{
				return updControl.AllowMenuChange();
			}
			return true;
		}

		#endregion

		#region NewEntity class

		/// <summary>
		/// "Special Case" class for new entity insertions
		/// </summary>
		public class NewEntity
		{
			/// <summary>
			/// Creates a new <see cref="NewEntity"/> instance.
			/// </summary>
			public NewEntity()
			{
			}

			/// <summary>
			/// ToString override
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return "<<New Item>>";
			}

			/// <summary>
			/// Overridden so that all cases are equal for this class
			/// </summary>
			/// <param name="obj">Obj.</param>
			/// <returns></returns>
			public override bool Equals(object obj)
			{
				return (obj is NewEntity);
			}


			/// <summary>
			/// Just call the base
			/// </summary>
			/// <returns></returns>
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

		}

		#endregion

		#region Event Handlers

		private void listManager_ContentsRefreshed(object sender, EventArgs e)
		{
			itemList.SuspendLayout();
			try
			{
				refreshList();
				autosizeListWidthToContents();
				itemList.SelectedIndex = ListManager.SelectedIndex;
			}
			finally
			{
				itemList.ResumeLayout();
			}
		}

		private void listManager_SelectedIndexChanged(object sender, EventArgs e)
		{
			T.E();
			itemList.SelectedIndex = listManager.SelectedIndex;
			T.X();
		}

		private void itemList_SelectedIndexChanged(object sender, EventArgs e)
		{
			T.E();
			listManager.SelectedIndex = itemList.SelectedIndex;

			// change of selected index in list manager may be cancelled, so we resynchronise the 
			// list to the list manager	

			itemList.SelectedIndex = ListManager.SelectedIndex;
			T.X();
		}

		#endregion Event Handlers

	}
}