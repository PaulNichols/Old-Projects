using System;
using System.Collections;
using System.ComponentModel;

using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Manages the list of items available during static data maintenance. This is the
	/// list of items on the left-hand side of the screen
	/// </summary>
	public class StaticDataListManager
	{
		/// <summary>
		/// Creates a new <see cref="StaticDataListManager"/> instance.
		/// </summary>
		public StaticDataListManager()
		{
			T.E();
			T.X();
		}

		
		#region Properties
		
		private IList list;
		/// <summary>
		/// Gets the items (as an ICollection) in the list.
		/// </summary>
		/// <value></value>
		public ICollection Items
		{
			get {return (ICollection)list;}
		}

		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <value></value>
		public object this[int index]
		{
			get 
			{
				if (list != null)
				{
					return list[index];
				}
				else
				{
					return null;
				}
			}
		}

		private int selectedIndex = -1;

		/// <summary>
		/// Gets the index of the item selected in the list.
		/// </summary>
		/// <value></value>
		public int SelectedIndex
		{
			get {return selectedIndex;}
			set 
			{	
				if (value != selectedIndex)
				{
					if (list != null && value < list.Count)
					{
						CancelEventArgs e = new CancelEventArgs(false);
						OnSelectedIndexChanging(e);
						if (!e.Cancel)
						{
							if (SelectedIsNew)
							{
								list.RemoveAt(SelectedIndex);
								selectedIndex = value;
								OnContentsRefreshed(EventArgs.Empty);
							}
							else
							{
								selectedIndex = value;
							}
							
							OnSelectedIndexChanged(EventArgs.Empty);
						}
					}
					else
					{
						throw new IndexOutOfRangeException("SelectedIndex is out of range of list");
					}
				}
			}
		}

		/// <summary>
		/// Gets the currently selected item.
		/// </summary>
		/// <value></value>
		public object SelectedItem
		{
			get 
			{
				if (SelectedIndex < 0)
				{
					return null;
				}
				else
				{
					return this[SelectedIndex];
				}
			}

			set
			{
				if (list != null)
				{
					int index = list.IndexOf(value);
					if (index >= 0)
					{
						SelectedIndex = index;
					}
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the selected item is a new item
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [selected is new]; otherwise, <c>false</c>.
		/// </value>
		public bool SelectedIsNew
		{
			get
			{
				return (SelectedItem is newObject);
			}
		}

		#endregion


		#region Methods

		/// <summary>
		/// Refreshes the contents of the list.
		/// </summary>
		/// <param name="newContents">New contents.</param>
		public void RefreshContents(IList newContents)
		{
			T.E();
			list = newContents;
			OnContentsRefreshed(EventArgs.Empty);
			T.X();
		}

		/// <summary>
		/// Adds a new blank item to the list.
		/// </summary>
		public void AddNew()
		{
			T.E();
			if (list != null)
			{
				CancelEventArgs e = new CancelEventArgs(false);
				OnSelectedIndexChanging(e);
				if (!e.Cancel)
				{
					this.selectedIndex = list.Add(new newObject());
					OnContentsRefreshed(EventArgs.Empty);
					OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
			T.X();
		}

		/// <summary>
		/// Deletes the selected item from the list.
		/// </summary>
		public void DeleteSelected()
		{
			T.E();
			if (list != null && SelectedIndex >= 0)
			{
				list.RemoveAt(SelectedIndex);
				if (SelectedIndex >= list.Count)
				{
					selectedIndex = list.Count - 1;					
				}
				OnContentsRefreshed(EventArgs.Empty);
				OnSelectedIndexChanged(EventArgs.Empty);
			}
			T.X();
		}

		/// <summary>
		/// Changes the selected object.
		/// </summary>
		public void ChangeSelected(object newValue)
		{
			T.E();
			if (list != null && SelectedIndex >= 0)
			{
				list[SelectedIndex] = newValue;
				OnContentsRefreshed(EventArgs.Empty);
			}
			T.X();
		}

		#endregion Methods

		#region Events

		/// <summary>
		/// Event raised when the contents of the list are refreshed
		/// </summary>
		public event EventHandler ContentsRefreshed;

		/// <summary>
		/// Raises the ContentsRefreshed event.
		/// </summary>
		/// <param name="e">The event args</param>
		protected virtual void OnContentsRefreshed(EventArgs e)
		{
			if (ContentsRefreshed != null)
			{
				ContentsRefreshed(this,e);
			}
		}

		/// <summary>
		/// Event raised after the SeletedIndex has changed
		/// </summary>
		public event EventHandler SelectedIndexChanged;

		/// <summary>
		/// Raises the SelectedIndexChanged event
		/// </summary>
		/// <param name="e">E.</param>
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this,e);
			}
		}

		/// <summary>
		/// Event raised after the SeletedIndex has changing
		/// </summary>
		public event CancelEventHandler SelectedIndexChanging;

		/// <summary>
		/// Raises the SelectedIndexChanging event
		/// </summary>
		/// <param name="e">E.</param>
		protected virtual void OnSelectedIndexChanging(CancelEventArgs e)
		{
			if (SelectedIndexChanging != null)
			{
				SelectedIndexChanging(this,e);
			}
		}


		#endregion Events

		#region New Special Case

		private class newObject
		{
			/// <summary>
			/// Override to display default text in menu
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return "<<New Item>>";
			}
		}

		#endregion New Special Case
	}
}
