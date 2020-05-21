using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Lookups
{
	/// <summary>
	/// Summary description for SimpleStringLookupCollection.
	/// </summary>
	public class SimpleStringLookupCollection : CollectionBase
	{
        /// <summary>
        /// Creates a new <see cref="SimpleStringLookupCollection"/> instance.
        /// </summary>
        public SimpleStringLookupCollection()
        {
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">Item.</param>
        public void Add(SimpleStringLookup item)
        {
            this.List.Add(item);
        }

        /// <summary>
        /// Gets the <see cref="SimpleStringLookup"/> at the specified index. Default indexer in C#
        /// </summary>
        /// <value></value>
        public SimpleStringLookup this[int index]
        {
            get {return (SimpleStringLookup)this.List[index];}
        }	
    }
}
