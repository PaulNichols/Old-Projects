using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.WinUI.Helpers
{
	/// <summary>
	/// Provides for a complete list of items to be split into an included list and excluded list, 
	/// with all items exclusively in one list or the other. Initially all items are in the excluded
	/// list.
	/// </summary>
	public class ExclusiveListHelper
	{
		/// <summary>
		/// Creates a new <see cref="ExclusiveListHelper"/> instance.
		/// </summary>
		public ExclusiveListHelper(IList completeList)
		{
			if (completeList == null) throw new ArgumentNullException("completeList");
			this.completeList = completeList;
			excluded = new ArrayList(completeList);
		}

		private IList completeList;
		
		private IList included = new ArrayList();

		/// <summary>
		/// Gets the included.
		/// </summary>
		/// <value></value>
		public IList Included
		{
			get {return included;}
		}

		private IList excluded;

		/// <summary>
		/// Gets the excluded.
		/// </summary>
		/// <value></value>
		public IList Excluded
		{
			get {return excluded;}
		}

		/// <summary>
		/// Includes the specified items.
		/// </summary>
		/// <param name="items">Items.</param>
		public void Include(IList items)
		{
			moveItems(items,Excluded,Included);
		}

		/// <summary>
		/// Includes the specified item.
		/// </summary>
		/// <param name="item">Items.</param>
		public void Include(object item)
		{
			moveItem(item,Excluded,Included);
		}

		/// <summary>
		/// Excludes the specified items.
		/// </summary>
		/// <param name="items">Items.</param>
		public void Exclude(IList items)
		{
			moveItems(items,Included,Excluded);
		}


		/// <summary>
		/// Excludes the specified item.
		/// </summary>
		/// <param name="item">Items.</param>
		public void Exclude(object item)
		{
			moveItem(item,Included,Excluded);
		}


		private void moveItems(IList items, IList source, IList target)
		{
			if (items != null)
			{
				foreach(object o in items)
				{
					moveItem(o,source,target);
				}
			}
		}

		private void moveItem(object item,IList source, IList target)
		{
			if (item != null)
			{
				if (!completeList.Contains(item))
				{
					throw new IndexOutOfRangeException(string.Format("Cannot handle object {0} in ExclusiveListHelper as it is not in the completeList set in the constructor",item));
				}
				
				if(!target.Contains(item))
				{
					target.Add(item);
					source.Remove(item);
				}
			}
		}
	}
}
