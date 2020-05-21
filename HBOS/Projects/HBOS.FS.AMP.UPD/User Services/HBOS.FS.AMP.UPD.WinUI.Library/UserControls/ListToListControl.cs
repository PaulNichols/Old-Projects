using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Helpers;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for ListToListControl.
	/// </summary>
	public class ListToListControl : UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private int m_initialWidth;

		//style changed from event driven, as rows were being repeatedly processed
		//each time a column was added
		internal static void SetColsAndListsForListView(ListView lv, ColumnCollection colCollection, Size clientSize)
		{
			lv.SuspendLayout();
			try
			{
				lv.Columns.Clear();
				if (lv.Name == "lstUnselected" || lv.View == System.Windows.Forms.View.List)
				{
					if (lv.Columns.Count > 0)
					{
						ColumnHeader ch = colCollection[0].makeListViewColumn();
						ch.Width = lv.ClientSize.Width;
						lv.Columns.Add(ch);
					}
					else
					{
						lv.Columns.Add(string.Empty, clientSize.Width, HorizontalAlignment.Left);
					}
				}
				else
				{
					foreach (Column col in colCollection)
					{
						lv.Columns.Add(col.makeListViewColumn());
					}
				}
			}
			finally
			{
				lv.ResumeLayout();
			}
		}

		/// <summary>
		/// sets up list view columns from data added with columncollection.add,
		/// then resets the data in the views.
		/// </summary>
		/// <param name="allItems"></param>
		/// <param name="selectedItems"></param>
		public void SetColumnsAndLists(IList allItems, IList selectedItems)
		{
			this.SuspendLayout();

			try
			{
				SetColsAndListsForListView(lstUnselected, unselectedColumns, this.ClientSize);
				SetColsAndListsForListView(lstSelected, selectedColumns, this.ClientSize);

				ResetLists(allItems, selectedItems);

				this.OnResize(new EventArgs());

			}
			finally
			{
				this.ResumeLayout();
			}


		}

		/// <summary>
		/// accepts new allItems and selectedItems list and associates these new lists with the user control
		/// </summary>
		/// <param name="allItems"></param>
		/// <param name="selectedItems"></param>
		public void ResetLists(IList allItems, IList selectedItems)
		{
			configureListHelper(allItems, selectedItems);
			refreshLists();
		}

		/// <summary>
		/// sets the selected list to null to indicate not using the list to list control at the moment
		/// and to indicate it is uninitialised
		/// </summary>
		public void ClearLists()
		{
			//only call this when List to List isn't being displayed and eg new item being selected
			this.SuspendLayout();
			try
			{
				listHelper = null;
				lstUnselected.Items.Clear();
				lstSelected.Items.Clear();
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		/// <summary>
		/// Creates a new <see cref="ListToListControl"/> instance.
		/// </summary>
		public ListToListControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			m_initialWidth = this.Width;

			selectedColumns = new ColumnCollection(this.lstSelected);
			unselectedColumns = new ColumnCollection(this.lstUnselected);

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstUnselected = new System.Windows.Forms.ListView();
			this.lstSelected = new System.Windows.Forms.ListView();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.pnlMiddle = new System.Windows.Forms.Panel();
			this.pnlMiddle.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstUnselected
			// 
			this.lstUnselected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstUnselected.FullRowSelect = true;
			this.lstUnselected.LabelEdit = true;
			this.lstUnselected.Location = new System.Drawing.Point(0, 0);
			this.lstUnselected.Name = "lstUnselected";
			this.lstUnselected.Size = new System.Drawing.Size(264, 624);
			this.lstUnselected.TabIndex = 0;
			this.lstUnselected.View = System.Windows.Forms.View.Details;
			// 
			// lstSelected
			// 
			this.lstSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstSelected.FullRowSelect = true;
			this.lstSelected.GridLines = true;
			this.lstSelected.LabelEdit = true;
			this.lstSelected.Location = new System.Drawing.Point(400, 0);
			this.lstSelected.Name = "lstSelected";
			this.lstSelected.Size = new System.Drawing.Size(272, 624);
			this.lstSelected.TabIndex = 1;
			this.lstSelected.View = System.Windows.Forms.View.List;
			this.lstSelected.DoubleClick += new System.EventHandler(this.lstSelected_DoubleClick);
			this.lstSelected.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstSelected_AfterLabelEdit);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(8, 40);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(112, 23);
			this.btnRemove.TabIndex = 1;
			this.btnRemove.Text = "<< &Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(8, 8);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(112, 23);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "&Add >>";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// pnlMiddle
			// 
			this.pnlMiddle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pnlMiddle.Controls.Add(this.btnAdd);
			this.pnlMiddle.Controls.Add(this.btnRemove);
			this.pnlMiddle.Location = new System.Drawing.Point(264, 0);
			this.pnlMiddle.Name = "pnlMiddle";
			this.pnlMiddle.Size = new System.Drawing.Size(128, 72);
			this.pnlMiddle.TabIndex = 2;
			// 
			// ListToListControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.pnlMiddle);
			this.Controls.Add(this.lstSelected);
			this.Controls.Add(this.lstUnselected);
			this.Name = "ListToListControl";
			this.Size = new System.Drawing.Size(688, 624);
			this.Resize += new System.EventHandler(this.ListToListControl_Resize);
			this.pnlMiddle.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ListView lstUnselected;
		private ListView lstSelected;
		private Button btnRemove;
		private Button btnAdd;
		private Panel pnlMiddle;

		#region Privates

		private ExclusiveListHelper listHelper;

		private void configureListHelper(IList allItems, IList selectedItems)
		{
			listHelper = new ExclusiveListHelper(allItems);
			listHelper.Include(selectedItems);
		}

		private void refreshLists()
		{
			this.SuspendLayout();
			try
			{
				lstUnselected.Items.Clear();
				lstSelected.Items.Clear();
				foreach (object excludedObj in listHelper.Excluded)
				{
					ListViewItem unselectedItem = unselectedColumns.makeListViewItem(excludedObj);
					lstUnselected.Items.Add(unselectedItem);
				}

				foreach (object includedObj in listHelper.Included)
				{
					ListViewItem selectedItem = selectedColumns.makeListViewItem(includedObj);
					lstSelected.Items.Add(selectedItem);
				}
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		/*
		private void columnsChanged(object sender, EventArgs e)
		{
			//this.refreshLists();
		}
		*/

		private void btnAdd_Click(object sender, EventArgs e)
		{
			IList selectedEntities = getEntitiesFromListViewItems(lstUnselected.SelectedItems);

			if (selectedEntities.Count > 0)
			{
				ListToListChangingArgs args = new ListToListChangingArgs(false, selectedEntities);
				onAddingSelectedItems(args);

				if (!args.Cancel)
				{
					listHelper.Include(selectedEntities);
					this.refreshLists();
				}
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			IList selectedEntities = getEntitiesFromListViewItems(lstSelected.SelectedItems);

			if (selectedEntities.Count > 0)
			{
				ListToListChangingArgs args = new ListToListChangingArgs(false, selectedEntities);
				onRemovingSelectedItems(args);

				if (!args.Cancel)
				{
					listHelper.Exclude(selectedEntities);
					this.refreshLists();
				}
			}
		}

		internal static IList getEntitiesFromListViewItems(IList items)
		{
			object[] result = new object[items.Count];
			for (int i = 0; i < result.Length; i++)
			{
				ListViewItem lvi = items[i] as ListViewItem;
				if (lvi != null)
					result[i] = lvi.Tag;
			}
			return result;
		}

		private void ListToListControl_Resize(object sender, EventArgs e)
		{
			this.SuspendLayout();
			try
			{
				int maxWidth = this.ClientSize.Width - 50;
				int maxHeight = this.ClientSize.Height - 60;

				if (this.View == ListViewMode.ListToList)
				{
					Size listSize = new Size((maxWidth - pnlMiddle.Width)/2, maxHeight);
					lstUnselected.Size = listSize;
					lstSelected.Size = listSize;
				}
				else
				{
					lstUnselected.Size = new Size((maxWidth - pnlMiddle.Width)/3, maxHeight);
					lstSelected.Size = new Size(((maxWidth - pnlMiddle.Width)/3)*2, maxHeight);

					//adjust column widths
					if (this.selectedColumns != null && this.selectedColumns.Count > 0)
					{
						decimal proportion = ((decimal) this.Width)/((decimal) m_initialWidth);
						for (int i = 0; i < this.selectedColumns.Count; i++)
						{
							Column col = (this.selectedColumns[i]);
							if (col.ColHeader != null)
							{
								col.ColHeader.Width = (int) (col.InitialWidth*proportion);
							}
						}
					}
				}
				pnlMiddle.Left = lstUnselected.Right + 1;
				lstSelected.Left = pnlMiddle.Right + 1;
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		private void lstSelected_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			//just pass it all back to the client and let them handle the object update
			//this is because if we throw an exception here for an invalid cast 
			//there would be no way to catch it and deal with it.

			if (selectedColumns[0].propertyName != string.Empty)
			{
				ListToListEditedArgs args = new ListToListEditedArgs(lstSelected.Items[e.Item].Tag, selectedColumns[0].propertyName, e.Label);
				onEditingSelectedItem(args);
				e.CancelEdit = args.Cancel;
			}
		}

		#endregion

		#region Properties

		private ListViewMode view = ListViewMode.ListToList;

		/// <summary>
		/// Gets or sets the view layout of the control.
		/// </summary>
		/// <value></value>
		public ListViewMode View
		{
			get { return view; }
			set
			{
				view = value;
				switch (view)
				{
					case ListViewMode.ListToList:
						lstSelected.View = System.Windows.Forms.View.List;
						break;
					case ListViewMode.ListToGrid:
						lstSelected.View = System.Windows.Forms.View.Details;
						break;
				}
				selectedColumns.changed();
				this.OnResize(new EventArgs());
			}
		}


		private ColumnCollection selectedColumns;

		/// <summary>
		/// Gets the columns collection used to display the selected items when using grids. When <see cref="View"/>
		/// is set to ListToList only SelectedColumn[0] is used to format display.
		/// </summary>
		/// <value></value>
		public ColumnCollection SelectedColumns
		{
			get { return this.selectedColumns; }
		}

		private ColumnCollection unselectedColumns;

		/// <summary>
		/// Gets the columns collection used to display the unselected items. As the unselected items can only be
		/// displayed in a list only Column[0] is used.
		/// </summary>
		/// <value></value>
		public ColumnCollection UnselectedColumns
		{
			get { return this.unselectedColumns; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the list of items in the selected (right-hand) list
		/// </summary>
		/// <returns>An IList</returns>
		public IList SelectedItems()
		{
			if (listHelper == null)
			{
				return null;
			}
			{
				object[] result = new object[listHelper.Included.Count];
				listHelper.Included.CopyTo(result, 0);
				return result;
			}

		}

		/// <summary>
		/// Returns the list of items in the unselected (left-hand) list
		/// </summary>
		/// <returns>An IList</returns>
		public IList UnselectedItems()
		{
			object[] result = new object[listHelper.Excluded.Count];
			listHelper.Excluded.CopyTo(result, 0);
			return result;
		}

		#endregion Methods

		#region Events

		/// <summary>
		/// Event raised when one or more items are moved for the unselected list into the selected list
		/// </summary>
		public event ListToListChangingHandler AddingSelectedItems;

		private void onAddingSelectedItems(ListToListChangingArgs e)
		{
			if (this.AddingSelectedItems != null)
				this.AddingSelectedItems(this, e);
		}

		/// <summary>
		/// Event raised when one or more items are removed from the selected list into the unselected list
		/// </summary>
		public event ListToListChangingHandler RemovingSelectedItems;

		private void onRemovingSelectedItems(ListToListChangingArgs e)
		{
			if (this.RemovingSelectedItems != null)
				this.RemovingSelectedItems(this, e);
		}

		/// <summary>
		/// Event raised when the text in the selected list is edited by user
		/// </summary>
		public event ListToListEditedHandler EditingSelectedItem;

		private void onEditingSelectedItem(ListToListEditedArgs e)
		{
			if (this.EditingSelectedItem != null)
				this.EditingSelectedItem(this, e);
		}

		/// <summary>
		/// Event raised when the text in the selected list is edited by user
		/// </summary>
		public event ListToListItemDoubleClickedHandler SelectedItemsDoubleClicked;

		private void onSelectedItemsDoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedItemsDoubleClicked != null)
				this.SelectedItemsDoubleClicked(sender, e);
		}

		/// <summary>
		/// Event raised when the text in the selected list is edited by user
		/// </summary>


		#endregion Events


		private void lstSelected_DoubleClick(object sender, System.EventArgs e)
		{
			onSelectedItemsDoubleClick(sender, e);
		}

		#region Column class

		internal class Column
		{
			internal Column(string propertyName, string title, int width, HorizontalAlignment hzAlign)
			{
				this.propertyName = propertyName;
				this.title = title;
				this.width = width;
				this.initialWidth = width;
				this.hzAlign = hzAlign;
			}

			internal string propertyName;
			private string title;
			private int width;
			private int initialWidth;
			private HorizontalAlignment hzAlign;
			private ColumnHeader colHeader;

			internal ColumnHeader makeListViewColumn()
			{
				colHeader = new ColumnHeader();
				if (title != string.Empty)
				{
					colHeader.Text = title;
				}
				else
				{
					colHeader.Text = propertyName;
				}
				colHeader.TextAlign = hzAlign;
				colHeader.Width = width;
				return colHeader;
			}

			internal string getDisplayValue(object o)
			{
				if (propertyName == string.Empty)
				{
					return o.ToString();
				}
				else
				{
					Type type = o.GetType();
					PropertyInfo pi = type.GetProperty(propertyName);
					if (pi == null)
					{
						return string.Empty;
					}
					else
					{
						return pi.GetValue(o, null).ToString();
					}
				}
			}

			internal int InitialWidth
			{
				get { return initialWidth; }
			}

			internal ColumnHeader ColHeader
			{
				get { return colHeader; }
			}

		}

		#endregion

		#region ColumnCollection class

		/// <summary>
		/// Collection of the columns used in a List-to-Grid configuration
		/// </summary>
		public class ColumnCollection : CollectionBase
		{
			private const int defaultWidth = 100;
			private const HorizontalAlignment defaultAlign = HorizontalAlignment.Left;

			internal ColumnCollection(ListView parent)
			{
				this.parent = parent;
			}

			//			private ListToListControl parent;
			private ListView parent;

			internal Column this[int index]
			{
				get { return (Column) this.List[index]; }
			}

			/// <summary>
			/// Adds the specified property to the column collection.
			/// </summary>
			/// <param name="propertyName">Name of the property.</param>
			public void Add(string propertyName)
			{
				Add(propertyName, string.Empty, defaultWidth, defaultAlign);
			}

			/// <summary>
			/// Adds the specified property to the column collection.
			/// </summary>
			/// <param name="propertyName">Name of the property.</param>
			/// <param name="title">Title to put in the grid.</param>
			public void Add(string propertyName, string title)
			{
				Add(propertyName, title, defaultWidth, defaultAlign);
			}


			/// <summary>
			/// Adds the specified property to the column collection.
			/// </summary>
			/// <param name="propertyName">Name of the property.</param>
			/// <param name="title">Title to put in the grid.</param>
			/// <param name="width">Width of the column.</param>
			public void Add(string propertyName, string title, int width)
			{
				Add(propertyName, title, width, defaultAlign);
			}

			/// <summary>
			/// Adds the specified property to the column collection.
			/// </summary>
			/// <param name="propertyName">Name of the property.</param>
			/// <param name="title">Title to put in the grid.</param>
			/// <param name="width">Width of the column.</param>
			/// <param name="hzAlign">Horizontal alignment of the column.</param>
			public void Add(string propertyName, string title, int width, HorizontalAlignment hzAlign)
			{
				Column col = new Column(propertyName, title, width, hzAlign);
				List.Add(col);
				changed();
			}

			/// <summary>
			/// Adds the specified property to the column collection.
			/// </summary>
			/// <param name="propertyName">Name of the property.</param>
			/// <param name="width">Width of the column.</param>
			/// <param name="hzAlign">Horizontal alignment of the column.</param>
			public void Add(string propertyName, int width, HorizontalAlignment hzAlign)
			{
				Add(propertyName, string.Empty, defaultWidth, hzAlign);
			}

			/// <summary>
			/// Overridden to update the list view on clear
			/// </summary>
			protected override void OnClearComplete()
			{
				base.OnClearComplete();
				changed();
			}

			/// <summary>
			/// Overridden to update the list view on insert
			/// </summary>
			protected override void OnInsertComplete(int index, object value)
			{
				base.OnInsertComplete(index, value);
				changed();
			}

			/// <summary>
			/// Overridden to update the list view on remove
			/// </summary>
			protected override void OnRemoveComplete(int index, object value)
			{
				base.OnRemoveComplete(index, value);
				changed();
			}

			/// <summary>
			/// Overridden to update the list view on set
			/// </summary>
			protected override void OnSetComplete(int index, object oldValue, object newValue)
			{
				base.OnSetComplete(index, oldValue, newValue);
				changed();
			}

			internal void changed()
			{
				/*
				parent.SuspendLayout();
				try
				{
					parent.Columns.Clear();
					if (parent.Name == "lstUnselected" || parent.View == System.Windows.Forms.View.List)
					{
						if (this.Count>0)
						{
							ColumnHeader ch = this[0].makeListViewColumn();
							ch.Width = this.parent.ClientSize.Width;
							parent.Columns.Add(ch);
						}
						else
						{
							parent.Columns.Add(string.Empty,this.parent.ClientSize.Width,HorizontalAlignment.Left);
						}
					}
					else
					{
						foreach(Column col in this)
						{
							parent.Columns.Add(col.makeListViewColumn());
						}
					}
				}
				finally
				{
					parent.ResumeLayout();
				}

				onColumnsChanged(new EventArgs());
				*/
			}


			internal ListViewItem makeListViewItem(object o)
			{
				ListViewItem result = null;

				if (this.Count > 0)
				{
					string[] dispItems = new string[this.Count];
					for (int i = 0; i < this.Count; i++)
					{
						dispItems[i] = this[i].getDisplayValue(o);
					}

					result = new ListViewItem(dispItems);
				}
				else
				{
					result = new ListViewItem(o.ToString());
				}

				result.Tag = o;

				return result;
			}

			internal event EventHandler columnsChanged;

			internal void onColumnsChanged(EventArgs e)
			{
				if (columnsChanged != null)
					columnsChanged(this, e);
			}
		}

		#endregion
	}

	#region enumerations

	/// <summary>
	/// Describes the display format of the ListToList control
	/// </summary>
	public enum ListViewMode
	{
		/// <value>Displays a list (left) going to a list(right)</value>
		ListToList,
		/// <value>Displays a list (left) going to a grid(right)</value>
		ListToGrid
	}

	#endregion

	#region Delegate and Event Args

	/// <summary>
	/// Delegate to handle when the lists are about to change in a ListToList control
	/// </summary>
	public delegate void ListToListItemDoubleClickedHandler(object sender, EventArgs e);

	/// <summary>
	/// Delegate to handle when the selected index changed eevent is raise from the selected list
	/// </summary>
	public delegate void ListToListUnSelectedItemIndexChangedHandler(object sender, EventArgs e);

	/// <summary>
	/// Delegate to handle when the selected index changed eevent is raise from the unselected list
	/// </summary>
	public delegate void ListToListChangingHandler(object sender, ListToListChangingArgs e);

	/// <summary>
	/// Delegate to handle editing in list to list
	/// </summary>
	public delegate void ListToListEditedHandler(object sender, ListToListEditedArgs e);

	/// <summary>
	/// Event arguments to raise when item in ListToList has been edited
	/// </summary>
	public class ListToListEditedArgs : CancelEventArgs
	{
		private object m_editedObject = null;
		private string m_editedValue = null;
		private string m_propertyName = null;


		/// <summary>
		/// Creates a new <see cref="ListToListEditedArgs"/> instance.
		/// </summary>
		/// <param name="editedObject">The object that has been edited</param>
		/// <param name="propertyName">The name of the property being edited</param>
		/// <param name="editedValue">The edited value</param>
		public ListToListEditedArgs(object editedObject, string propertyName, string editedValue)
		{
			m_editedObject = editedObject;
			m_propertyName = propertyName;
			m_editedValue = editedValue;
		}

		/// <summary>
		/// The object from the selected list that is being edited
		/// </summary>
		public object EditedObject
		{
			get { return m_editedObject; }
		}

		/// <summary>
		/// The name of the property that is being edited
		/// </summary>
		public string PropertyName
		{
			get { return m_propertyName; }
		}

		/// <summary>
		/// The new value typed by the user
		/// </summary>
		public string EditedValue
		{
			get { return m_editedValue; }
		}
	}

	/// <summary>
	/// Event arguments to raise when lists in ListToList are changing
	/// </summary>
	public class ListToListChangingArgs : CancelEventArgs
	{
		/// <summary>
		/// Creates a new <see cref="ListToListChangingArgs"/> instance.
		/// </summary>
		/// <param name="cancel">Cancel the change when true.</param>
		/// <param name="changedItems">The changed items.</param>
		public ListToListChangingArgs(bool cancel, IList changedItems) : base(cancel)
		{
			this.changedItems = changedItems;
		}

		private IList changedItems;

		/// <summary>
		/// Gets the changed items.
		/// </summary>
		/// <value></value>
		public IList ChangedItems
		{
			get { return changedItems; }
		}
	}

	#endregion
}