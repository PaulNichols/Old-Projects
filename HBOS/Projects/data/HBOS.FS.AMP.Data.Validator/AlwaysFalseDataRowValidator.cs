using System;
using System.Data;

using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.Data.Types;

namespace HBOS.FS.AMP.Data.Validator
{
	/// <summary>
	/// AlwaysFalseDataRowValidator - always causes a validation error.
	/// </summary>
	/// <remarks>Mainly used for testing purpses to ensure that Validation Errors are being successfully captured.</remarks>
	/// <example>
	///		<para>Example demonstrates creation of an Array of data validators which can be passed to the Transfer.DataTransporter.</para>
	///		<code lang="C#">
	///		IDataRowValidator validator1 = new AlwaysFalseValidator();
	///		IDataRowValidator[][] validators = new IDataRowValidator[1][];
	///		validators[0] = new IDataRowValidator[] {validator1 };
	///		</code>
	/// </example>
	public class AlwaysFalseValidator : IDataRowValidator
	{
		#region Event 

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		/// <remarks>Raised when the validator encounters an invalid data row.</remarks>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		/// <summary>
		/// Validates the dataRow and always cause an InvalidDataRow event.
		/// </summary>
		/// <param name="dataRow">Data row to validate.</param>
		/// <param name="dataDefinition">Data definition to use to validate the data.</param>
		/// <returns>A "Critical" validation error as it will always cause a validation error.</returns>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			if ( InvalidDataRowEvent != null )
			{
				InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , "Always False Validator always returns an Invalid result" , ValidationErrorSeverity.Critical ) );
			}

			return ValidationErrorSeverity.Critical;
		}
	}
}
