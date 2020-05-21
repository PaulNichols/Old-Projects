using System;

namespace HBOS.FS.AMP.Data
{
#if (DEBUG)

	/// <summary>
	/// <para><see cref="HBOS.FS.AMP.Data"/> contains all the functionality for generic data validation, import and export for AMP applications.</para>
	/// <para>It consists of the following main classes:</para>
	/// <list type="bullet">
	///		<item>
	///			<term><see cref="HBOS.FS.AMP.Data.Adapter"/></term>
	///			<description>The ability to convert between various types. e.g. Convert .Net types to SQL Server types.</description>
	///		</item>
	///		<item>
	///			<term><see cref="HBOS.FS.AMP.Data.Transfer"/></term>
	///			<description>The ability to transfer some data from a data source to a data sink.</description>
	///		</item>
	///		<item>
	///			<term><see cref="HBOS.FS.AMP.Data.Validator"/></term>
	///			<description>The ability to validate data as it is being transferred from a data source to a data sink.</description>
	///		</item>
	///		<item>
	///			<term><see cref="HBOS.FS.AMP.Data.Persister"/></term>
	///			<description>The ability to persist some data to a variety to data stores. e.g. SQL Server</description>
	///		</item>
	///		<item>
	///			<term><see cref="HBOS.FS.AMP.Data.Types"/></term>
	///			<description>Some generic types which are used by the Data Transfer mechanism. e.g. enums, events, delegates.</description>
	///		</item>
	/// </list>
	/// <note>The main controlling class is <see cref="HBOS.FS.AMP.Data.Transfer"/>. Its constructor links the Data Source to some validators, and controls persisting the data to 1 or more persisters. </note>
	/// </summary>
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
