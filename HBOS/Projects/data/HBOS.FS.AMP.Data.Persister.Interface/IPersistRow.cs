using System;
using System.Data;

namespace HBOS.FS.AMP.Data.Persister.Interface
{
	/// <summary>
	/// IPersistRow - the interface definition to persist a row
	/// </summary>
	public interface IPersistRow : IPersist
	{
		/// <summary>
		/// Persist a Data Row.
		/// </summary>
		/// <param name="dataRow">The Data Row to persist.</param>
		/// <param name="schema">The definition of the data being persisted.</param>
		void PersistRow(string[] dataRow, DataTable schema);
	}
}
