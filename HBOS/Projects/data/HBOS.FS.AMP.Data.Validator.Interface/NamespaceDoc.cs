using System;

namespace HBOS.FS.AMP.Data.Validator.Interface
{
#if (DEBUG)

	/// <summary>
	/// <see cref="HBOS.FS.AMP.Data.Validator.Interface"/> contains the interface definition for the Data Validators
	/// </summary>
	/// <remarks>
	/// <para>Each validator must implement <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator">IDataRowValidator</see>
	///  which defines the <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.Validate">Validate</see> 
	///  method and the <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.InvalidDataRowEvent">InvalidDataRowEvent</see> event.</para>
	/// <para>The <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.Validate">Validate</see> method performs the data validation.</para>
	/// <para>The <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.InvalidDataRowEvent">InvalidDataRowEvent</see> event is raised 
	/// for each invalid data row. These are captured by the Transfer.DataTransporter but can also be captured at source when the validators are set up.</para>
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
