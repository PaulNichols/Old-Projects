using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Lookups
{
	/// <summary>
	/// Collection of SimpleLookup items
	/// </summary>
	public class SimpleLookupCollection: CollectionBase
	{
		/// <summary>
		/// Creates a new <see cref="SimpleLookupCollection"/> instance.
		/// </summary>
		public SimpleLookupCollection()
		{
		}

		/// <summary>
		/// Adds the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add(SimpleLookup item)
		{
			this.List.Add(item);
		}

		/// <summary>
		/// Gets the <see cref="SimpleLookup"/> at the specified index. Default indexer in C#
		/// </summary>
		/// <value></value>
		public SimpleLookup this[int index]
		{
			get {return (SimpleLookup)this.List[index];}
		}
	}
}
