using System;

namespace HBOS.FS.AMP.Data.Persister.Interface
{
#if (DEBUG)

	/// <summary>
	/// <see cref="HBOS.FS.AMP.Data.Persister.Interface"/> defines the interface to use for persisting data to various data sinks.
	/// </summary>
	/// <remarks>The persister interfaces currently defined are:
	/// <list type="bullet">
	///		<item><see cref="IPersistRow"/> - used when persisting data on a row by row basis.</item>
	///		<item><see cref="IPersistDataSet"/> - used when persisting data on a dataset basis.</item>
	/// </list>
	/// </remarks>
	public class NamespaceDoc
	{
		/// <summary>
		/// This class is a dummy class used by NDoc to provide a namespace summary.
		/// </summary>
		public NamespaceDoc()
		{
		}
	}

#endif
}
