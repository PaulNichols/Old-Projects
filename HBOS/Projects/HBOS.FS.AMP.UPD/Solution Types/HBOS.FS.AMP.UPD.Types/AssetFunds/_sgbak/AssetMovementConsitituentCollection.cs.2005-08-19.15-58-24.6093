using System;
using System.Collections;
using System.ComponentModel;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	// create the template
	using T = AssetMovementConstituent;

	/// <summary>
	/// Summary description for AssetMovementConsitituentCollection.
	/// </summary>
	public class AssetMovementConstituentCollection : ICollection, IList, ITypedList, IEnumerable, ICloneable
	{
		#region Private variables

		private const int DefaultMinimumCapacity = 16;

		private T[] m_array = new T[DefaultMinimumCapacity];
		private int m_count = 0; // permits thread safe operations
		[NonSerialized] private int m_version = 0;

		#endregion Private variables

		#region AssetMovementConsitituentCollection

		/// <summary>
		/// Asset Fund collection constructor
		/// </summary>
		public AssetMovementConstituentCollection()
		{
		}

		/// <summary>
		/// Asset Fun collection overloaded constructor
		/// </summary>
		public AssetMovementConstituentCollection(AssetMovementConstituentCollection collection)
		{
			AddRange(collection);
		}

		/// <summary>
		/// Asset Fund collection overloaded constructor
		/// </summary>
		public AssetMovementConstituentCollection(T[] array)
		{
			AddRange(array);
		}

		#endregion AssetMovementConsitituentCollection

		#region ITypedList

		/// <summary>
		/// Get the List name for the colleciton
		/// </summary>
		/// <param name="listAccessors">Can be null</param>
		/// <returns></returns>
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			if (null == listAccessors)
			{
				return typeof (T).ToString();
			}
			else
			{
				PropertyDescriptor p = listAccessors[0];
				return p.DisplayName;
			}
		}


		/// <summary>
		/// Access to the Item Properties.
		/// </summary>
		/// <param name="listAccessors"></param>
		/// <returns></returns>
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return TypeDescriptor.GetProperties(typeof (T));
		}

		#endregion

		#region Operations (type-safe ICollection)

		/// <summary>
		/// Returns the number of items in the collection
		/// </summary>
		public int Count
		{
			get { return m_count; }
		}

		/// <summary>
		/// Copies all the Consitituents in the collection to the specified target object
		/// </summary>
		/// <param name="array"></param>
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>
		/// Extract a range of items from the collection starting at the specified start index  
		/// </summary>
		/// <param name="array"></param>
		/// <param name="start"></param>
		public void CopyTo(T[] array, int start)
		{
			if (m_count > array.GetUpperBound(0) + 1 - start)
				throw new ArgumentException("Destination array was not long enough.");

			// for (int i=0; i < m_count; ++i) array[start+i] = m_array[i];

			//
			// Copies a range of elements from an Array starting at the specified 
			// source index and pastes them to another Array starting at the 
			// specified destination index. The length and the indexes are specified 
			// as 64-bit integers
			//
			Array.Copy(m_array, 0, array, start, m_count);
		}

		#endregion Operations (type-safe ICollection)

		#region Operations (type-safe IList)

		/// <summary>
		/// Assign or extract a specific item value from the collection
		/// </summary>
		public T this[int index]
		{
			get
			{
				ValidateIndex(index); // throws
				return m_array[index];
			}
			set
			{
				ValidateIndex(index); // throws

				++m_version;
				m_array[index] = value;
			}
		}

		/// <summary>
		/// Add a new fund to the collection
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int Add(T item)
		{
			if (NeedsGrowth())
				Grow();

			++m_version;
			m_array[m_count] = item;

			return m_count++;
		}

		/// <summary>
		/// Empty the collection of items
		/// </summary>
		public void Clear()
		{
			++m_version;
			m_array = new T[DefaultMinimumCapacity];
			m_count = 0;
		}

		/// <summary>
		/// Does the collection contain the specified item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			return ((IndexOf(item) == -1) ? false : true);
		}

		/// <summary>
		/// Returns the index of the specified item if it exists in the collection
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int IndexOf(T item)
		{
			for (int i = 0; i < m_count; ++i)
				if (m_array[i].Equals(item))
					return i;
			return -1;
		}

		/// <summary>
		/// Insert an item at the specified position
		/// </summary>
		/// <param name="position"></param>
		/// <param name="item"></param>
		public void Insert(int position, T item)
		{
			ValidateIndex(position, true); // throws

			if (NeedsGrowth())
				Grow();

			++m_version;
			// for (int i=m_count; i > position; --i) m_array[i] = m_array[i-1];
			Array.Copy(m_array, position, m_array, position + 1, m_count - position);

			m_array[position] = item;
			m_count++;
		}

		/// <summary>
		/// Remove the specified item from the collection
		/// </summary>
		/// <param name="item"></param>
		public void Remove(T item)
		{
			int index = IndexOf(item);
			if (index < 0)
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");

			RemoveAt(index);
		}

		/// <summary>
		/// Remove an item from the specified position of the collection
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index)
		{
			ValidateIndex(index); // throws

			++m_version;
			m_count--;
			// for (int i=index; i < m_count; ++i) m_array[i] = m_array[i+1];
			Array.Copy(m_array, index + 1, m_array, index, m_count - index);

			if (NeedsTrimming())
				Trim();
		}

		#endregion Operations (type-safe IList)

		#region Operations (type-safe IEnumerable)

		/// <summary>
		/// Allows ForEach functionality
		/// </summary>
		/// <returns></returns>
		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		#endregion Operations (type-safe IEnumerable)

		#region Operations (type-safe ICloneable)

		/// <summary>
		/// Create a new copy of the collection
		/// </summary>
		/// <returns></returns>
		public AssetMovementConstituentCollection Clone()
		{
			AssetMovementConstituentCollection tc = new AssetMovementConstituentCollection();
			tc.AddRange(this);
			tc.Capacity = this.m_array.Length;
			tc.m_version = this.m_version;
			return tc;
		}

		#endregion Operations (type-safe ICloneable)

		#region Public helpers 

		//
		// Public helpers (just to mimic some nice features of ArrayList)
		//

		/// <summary>
		/// Set or return the size of collection
		/// </summary>
		public int Capacity
		{
			get { return m_array.Length; }

			set
			{
				if (value < m_count) value = m_count;
				if (value < DefaultMinimumCapacity) value = DefaultMinimumCapacity;

				if (m_array.Length == value) return;

				++m_version;

				T[] temp = new T[value];
				// for (int i=0; i < m_count; ++i) temp[i] = m_array[i];
				Array.Copy(m_array, 0, temp, 0, m_count);
				m_array = temp;
			}
		}

		/// <summary>
		/// Add a new range of AssetMovementConsitituentCollection from a AssetMovementConsitituentCollection collection
		/// </summary>
		/// <param name="collection"></param>
		public void AddRange(AssetMovementConstituentCollection collection)
		{
			// for (int i=0; i < collection.Count; ++i) Add(collection[i]);

			++m_version;

			Capacity += collection.Count;
			Array.Copy(collection.m_array, 0, this.m_array, m_count, collection.m_count);
			m_count += collection.Count;
		}

		/// <summary>
		/// Add a new range of AssetMovementConsitituentCollection from an array
		/// AddRange Overload
		/// </summary>
		/// <param name="array"></param>
		public void AddRange(T[] array)
		{
			// for (int i=0; i < array.Length; ++i) Add(array[i]);

			++m_version;

			Capacity += array.Length;
			Array.Copy(array, 0, this.m_array, m_count, array.Length);
			m_count += array.Length;
		}

		// IB Convert to DataTable is no longer supported as the collection can now directly databind to the datagrid ComboBox column

		//        /// <summary>
		//        /// Return the collection as a data table to be used for data binding.
		//        /// </summary>
		//        /// <returns>The collection as a DataTable.</returns>
		//        public DataTable ConvertToDataTable()
		//        {
		//            // Create the data table.
		//            DataTable table = new DataTable();
		//
		//            // Create a data column for each public property.
		//            PropertyInfo[] properties = typeof(T).GetProperties();
		//
		//            if (properties != null)
		//            {
		//                for (int count = 0; count < properties.Length; count++)
		//                {
		//                    PropertyInfo currentProperty = (PropertyInfo)properties[count];
		//                    DataColumn column = new DataColumn(currentProperty.Name, currentProperty.PropertyType);
		//                    table.Columns.Add(column);
		//                }
		//
		//                // Create a row for each object in the collection.
		//                for (int rowCount = 0; rowCount < this.Count; rowCount++)
		//                {
		//                    DataRow row = table.NewRow();
		//
		//                    for (int columnCount = 0; columnCount < properties.Length; columnCount++)
		//                    {
		//                        PropertyInfo currentProperty = (PropertyInfo)properties[columnCount];
		//                        row[columnCount] = currentProperty.GetValue(this[rowCount], BindingFlags.GetProperty,
		//                            null, null, null);
		//                    }
		//
		//                    table.Rows.Add(row);
		//                }
		//            }
		//
		//            return table;
		//        }

		#endregion Public Helpers

		#region Implementation (helpers)

		/// <summary>
		/// Is the specified Item index valid
		/// </summary>
		/// <param name="index"></param>
		private void ValidateIndex(int index)
		{
			ValidateIndex(index, false);
		}

		/// <summary>
		/// Is the specified item index valid
		/// ValidateIndex overload
		/// </summary>
		/// <param name="index"></param>
		/// <param name="allowEqualEnd"></param>
		private void ValidateIndex(int index, bool allowEqualEnd)
		{
			// if allow equal end then use the number of items else number of items less one
			int max = (allowEqualEnd) ? (m_count) : (m_count - 1);
			if (index < 0 || index > max)
				throw new ArgumentOutOfRangeException("Index was out of range.  Must be non-negative and less than the size of the collection.", index, "Specified argument was out of the range of valid values.");
		}

		/// <summary>
		/// Does the collection need to increase in size?
		/// </summary>
		/// <returns></returns>
		private bool NeedsGrowth()
		{
			return (m_count >= Capacity);
		}

		/// <summary>
		/// Increase the size of the collection
		/// </summary>
		private void Grow()
		{
			if (NeedsGrowth())
				Capacity = m_count*2;
		}

		/// <summary>
		/// Does the collection needs reducing in size?
		/// </summary>
		/// <returns></returns>
		private bool NeedsTrimming()
		{
			return (m_count <= Capacity/2);
		}

		/// <summary>
		/// Reduce the size of the collection to the number of valid items
		/// </summary>
		private void Trim()
		{
			if (NeedsTrimming())
				Capacity = m_count;
		}

		#endregion Implementation (helpers)

		#region Implementation (ICollection)

		/* redundant w/ type-safe method
		int ICollection.Count
		{
			get
			{ return m_count; }
		}
		*/

		bool ICollection.IsSynchronized
		{
			get { return m_array.IsSynchronized; }
		}

		object ICollection.SyncRoot
		{
			get { return m_array.SyncRoot; }
		}

		void ICollection.CopyTo(Array array, int start)
		{
			if (m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}

			Array.Copy(m_array, 0, array, start, m_count);
		}

		#endregion Implementation (ICollection)

		#region Implementation (IList)

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (T) value; }
		}

		int IList.Add(object item)
		{
			return this.Add((T) item);
		}

		/* redundant w/ type-safe method
		void IList.Clear()
		{
			this.Clear();
		}
		*/

		bool IList.Contains(object item)
		{
			return this.Contains((T) item);
		}

		int IList.IndexOf(object item)
		{
			return this.IndexOf((T) item);
		}

		void IList.Insert(int position, object item)
		{
			this.Insert(position, (T) item);
		}

		void IList.Remove(object item)
		{
			this.Remove((T) item);
		}

		/* redundant w/ type-safe method
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}
		*/

		#endregion Implementation (IList)

		#region Implementation (IEnumerable)

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion Implementation (IEnumerable)

		#region Implementation (ICloneable)

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		#endregion Implementation (ICloneable)

		#region public class Enumerator

		/// <summary>
		/// Nested enumerator class
		/// </summary>
		public class Enumerator : IEnumerator
		{
			private AssetMovementConstituentCollection m_collection;
			private int m_index;
			private int m_version;

			// Construction

			internal Enumerator(AssetMovementConstituentCollection tc)
			{
				m_collection = tc;
				m_index = -1;
				m_version = tc.m_version;
			}

			// Operations (type-safe IEnumerator)
			/// <summary>
			/// Get the currently selected item from the collection
			/// </summary>
			public T Current
			{
				get { return m_collection[m_index]; }
			}

			/// <summary>
			/// Move to the next item
			/// </summary>
			/// <returns></returns>
			public bool MoveNext()
			{
				if (m_version != m_collection.m_version)
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");

				++m_index;
				return (m_index < m_collection.Count) ? true : false;
			}

			/// <summary>
			/// Reset the current item index to the previous item index
			/// </summary>
			public void Reset()
			{
				if (m_version != m_collection.m_version)
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");

				m_index = -1;
			}

			// Implementation (IEnumerator)

			object IEnumerator.Current
			{
				get { return this.Current; }
			}

			/* redundant w/ type-safe method
			bool IEnumerator.MoveNext()
			{
				return this.MoveNext();
			}
			*/

			/* redundant w/ type-safe method
			void IEnumerator.Reset()
			{
				this.Reset();
			}
			*/
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get
			{
				return isDirty;
			}
			set
			{
				isDirty=value;
			}

		}
		private bool isDirty;
		private bool m_DoAnyItemsHaveAProportionOfZero;

		#endregion public class Enumerator

		/// <summary>
		/// Totals the movement of each of the constitute.
		/// </summary>
		/// <returns></returns>
		public decimal TotalMovement()
		{
			decimal totalMovement=0;
			foreach (AssetMovementConstituent constitute in m_array)
			{
				if (constitute != null )
				{
					totalMovement +=constitute.CalculateEffect();
				}
			}
			return totalMovement;
			
		}

		/// <summary>
		/// Gets a value indicating whether [if any items have A proportion of zero].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [do any items have A proportion of zero]; otherwise, <c>false</c>.
		/// </value>
		public bool DoAnyItemsHaveAProportionOfZero
		{
			get
			{
				return m_DoAnyItemsHaveAProportionOfZero;
			}
		}

		/// <summary>
		/// Totals the proportion of all the benchmarks, to 4 dp.
		/// </summary>
		/// <returns></returns>
		public decimal TotalProportion()
		{
			decimal totalProportion=0;
			foreach (AssetMovementConstituent constitute in m_array)
			{
				if (constitute!=null)
				{
					if (constitute.Proportion==0)
					{
						m_DoAnyItemsHaveAProportionOfZero=true;
					}
					totalProportion +=constitute.Proportion;
				}
			}
			return decimal.Round(totalProportion,4);
		}
	}
}