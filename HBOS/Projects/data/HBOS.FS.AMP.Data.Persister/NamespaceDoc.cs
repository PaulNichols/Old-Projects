using System;

namespace HBOS.FS.AMP.Data.Persister
{
	#if (DEBUG)

	/// <summary>
	/// <see cref="HBOS.FS.AMP.Data.Persister"/> supports the persisting of AMP data to various data sinks.
	/// </summary>
	/// <remarks>The persisters currently supported include
	/// <list type="bullet">
	///		<item><see cref="SqlPersister"/> - persisting to SQL Server.</item>
	///		<item><see cref="DataSetXSLTFilePersister"/> - persisting a DataSet to a file using an XSLT.</item>
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
