using System;
using System.Data;

namespace HBOS.FS.AMP.Data.Persister.Interface
{
	/// <summary>
	/// IPersistDataSet - used to persist an entire dataset to a data sink.
	/// </summary>
	public interface IPersistDataSet : IPersist
	{
		/// <summary>
		/// Persist the dataset
		/// </summary>
		/// <param name="dataSetToPersist">The Dataset to persist.</param>
		void PersistDataSet( DataSet dataSetToPersist  );
	}
}
