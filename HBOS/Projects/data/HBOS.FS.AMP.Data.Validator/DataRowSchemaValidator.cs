using System;
using System.Data;

using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;

namespace HBOS.FS.AMP.Data.Validator
{
	/// <summary>
	/// Validates a data row to ensure it matches the data definition.
	/// </summary>
	public class DataRowSchemaValidator : IDataRowValidator
	{
		#region Event

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion


		/// <summary>
		/// Validate the data row against the data definition.
		/// </summary>
		/// <param name="dataRow">The data row to validate.</param>
		/// <param name="dataDefinition">The data definition to use to validate the data.</param>
		/// <returns>Severity Level of High if the data is invalid</returns>
		/// <example>
		///		<para>Example demonstrates creation of an Array of data validators which can be passed to the Transfer.DataTransporter.</para>
		///		<code lang="C#">
		///		IDataRowValidator validator1 = new DataRowSchemaValidator();
		///		IDataRowValidator[][] validators = new IDataRowValidator[1][];
		///		validators[0] = new IDataRowValidator[] {validator1 };
		///		</code>
		/// </example>
		public ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity validationErrorSeverity = ValidationErrorSeverity.None;

			// check we have the right number of columns
			if (dataDefinition.Rows.Count != dataRow.Length)
			{
				validationErrorSeverity = ValidationErrorSeverity.High;

				if ( InvalidDataRowEvent != null )
				{
					InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , "Always False Validator always returns an Invalid result" , ValidationErrorSeverity.High ) );
				}
			}

			return validationErrorSeverity;
		}
	}
}
