using System;
using System.Data;

using HBOS.FS.AMP.Data.Types;

namespace HBOS.FS.AMP.Data.Validator.Interface
{
	/// <summary>
	/// Validator Generic Interface - Each Validator must implement IDataRowValidator.
	/// </summary>
	/// <remarks>
	/// Each validator must implement <see cref="Validate"/> and <see cref="InvalidDataRowEvent"/> so they have been broken into this interface definition which every validator must implement.
	/// </remarks>
	public interface IDataRowValidator
	{
		/// <summary>
		/// Performs a validation on each data row
		/// </summary>
		/// <param name="dataRow">Data Row to validate</param>
		/// <param name="dataDefinition">Data Definition to use to validate the row.</param>
		/// <returns>Measure of the severity of the validation error</returns>
		ValidationErrorSeverity  Validate(string[] dataRow, DataTable dataDefinition);

		/// <summary>
		/// Invalid Data Row Event which identifies a validaiton error.
		/// </summary>
		/// <remarks>
		/// <para>An Invalidate DataRow event identifies a validation error with a data row, and the Transfer.DataTransporter captures there events for display in a Validation error report.</para>
		/// <para>Other subscribers to the Error event can be configured when the Validators are established.</para>
		/// </remarks>
		event InvalidDataRowDelegate InvalidDataRowEvent;
	}
}
