using System;
using System.Collections;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Specialised Collection of IStatusFilters
	/// </summary>
	public class StatusFilterCollection: CollectionBase
	{
		/// <summary>
		/// Creates a new <see cref="StatusFilterCollection"/> instance.
		/// </summary>
		public StatusFilterCollection(): base()
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="StatusFilterCollection"/> instance.
		/// </summary>
		/// <param name="items">Items to add to the list.</param>
		public StatusFilterCollection(IStatusFilter[] items): this()
		{
			T.E();
			this.AddRange(items);
			T.X();
		}

		/// <summary>
		/// Gets or sets the <see cref="IStatusFilter"/> at the specified index.
		/// </summary>
		/// <value></value>
		public IStatusFilter this[int index]
		{
			get {return (IStatusFilter)this.List[index];}
			set {this.List[index] = value;}
		}

		/// <summary>
		/// Adds the specified IStatusFilter
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add(IStatusFilter item)
		{
			T.E();
			this.List.Add(item);
			onContentsChanged(EventArgs.Empty);
			T.X();
		}

		/// <summary>
		/// Adds the range of status filters.
		/// </summary>
		/// <param name="items">Items.</param>
		public void AddRange(IStatusFilter[] items)
		{
			T.E();
			if (items != null && items.Length>0)
			{
				foreach(IStatusFilter item in items)
				{
					this.List.Add(item);
				}
				
				onContentsChanged(EventArgs.Empty);
			}
			T.X();
		}

		/// <summary>
		/// Applies the filters in the list to the subject.
		/// </summary>
		/// <param name="subject">Subject.</param>
		public void Apply(IList subject)
		{
			T.E();

			bool keep;
			bool hasAFilterBeenApplied=false;

			for(int idx=0; idx<subject.Count; idx++)
			{
				object obj=subject[idx];
				keep=false;
				foreach(IStatusFilter filter in this)
				{
					if (filter.Applied)
					{
						hasAFilterBeenApplied=true;
						if (! filter.FilterOut(obj))
						{
							keep=true;
						}
					}
				}
				if (!keep && hasAFilterBeenApplied)
				{
					subject.Remove(obj);
					idx--;
				}
			}

			T.X();
		}

		/// <summary>
		/// Event raised when the contents of the list changes
		/// </summary>
		public event EventHandler ContentsChanged;

		private void onContentsChanged(EventArgs e)
		{
			T.E();
			if (ContentsChanged != null)
				ContentsChanged(this,e);
			T.X();
		}

		/// <summary>
		/// Overridden to raise the ContentsChanged event
		/// </summary>
		protected override void OnClearComplete()
		{
			T.E();
			base.OnClearComplete ();
			onContentsChanged(EventArgs.Empty);
			T.X();
		}

		/// <summary>
		/// Overridden to raise the ContentsChanged event
		/// </summary>
		protected override void OnInsertComplete(int index, object value)
		{
			T.E();
			base.OnInsertComplete (index, value);
			onContentsChanged(EventArgs.Empty);
			T.X();
		}

		/// <summary>
		/// Overridden to raise the ContentsChanged event
		/// </summary>
		protected override void OnRemoveComplete(int index, object value)
		{
			T.E();
			base.OnRemoveComplete (index, value);
			onContentsChanged(EventArgs.Empty);
			T.X();
		}

		/// <summary>
		/// Overridden to raise the ContentsChanged event
		/// </summary>
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			T.E();
			base.OnSetComplete (index, oldValue, newValue);
			onContentsChanged(EventArgs.Empty);
			T.X();
		}
	}
}
