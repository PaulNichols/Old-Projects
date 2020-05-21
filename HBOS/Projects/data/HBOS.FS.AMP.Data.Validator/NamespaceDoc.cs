using System;

namespace HBOS.FS.AMP.Data.Validator
{
#if (DEBUG)

	/// <summary>
	/// <see cref="HBOS.FS.AMP.Data.Validator"/> contains the logic for performing various data validations of data that we are importing or exporting.
	/// </summary>
	/// <remarks>
	/// <para>All Validators should implement <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator">IDataRowValidator</see>
	///  which defines the <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.Validate">Validate</see> 
	///  method and the <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.InvalidDataRowEvent">InvalidDataRowEvent</see> event.</para>
	/// <para>The <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.Validate">Validate</see> method performs the data validation.</para>
	/// <para>The <see cref="HBOS.FS.AMP.Data.Validator.Interface.IDataRowValidator.InvalidDataRowEvent">InvalidDataRowEvent</see> event is raised 
	/// for each invalid data row. These are captured by the Transfer.DataTransporter but can also be captured at source when the validators are set up.</para>
	/// <para>The validators currently implemented are:
	///		<list type="number">
	///			<item><see cref="HBOS.FS.AMP.Data.Validator.AlwaysTrueValidator">AlwaysTrueDataRowValidator</see></item>
	///			<item><see cref="HBOS.FS.AMP.Data.Validator.AlwaysFalseValidator">AlwaysFalseDataRowValidator</see></item>
	///			<item><see cref="DataRowSchemaValidator"/></item>
	///		</list>
	/// </para>
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
