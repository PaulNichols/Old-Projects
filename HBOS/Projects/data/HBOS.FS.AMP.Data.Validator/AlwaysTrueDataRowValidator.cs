using System;
using System.Data;

using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.Data.Types;

namespace HBOS.FS.AMP.Data.Validator
{
	/// <summary>
	/// AlwaysTrueDataRowValidator - always succeeds
	/// </summary>
	public class AlwaysTrueValidator : IDataRowValidator
	{
		#region Event

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		/// <summary>
		/// Validate the data row and never caues a Validation Error event to be raised.
		/// </summary>
		/// <param name="dataRow">Data Row to validate.</param>
		/// <param name="dataDefinition">Data definition to use to define the data.</param>
		/// <returns>Never returns a validation error.</returns>
		/// <example>
		///		<para>Example demonstrates creation of an Array of data validators which can be passed to the Transfer.DataTransporter.</para>
		///		<code lang="C#">
		///		IDataRowValidator validator1 = new AlwaysTrueValidator();
		///		IDataRowValidator[][] validators = new IDataRowValidator[1][];
		///		validators[0] = new IDataRowValidator[] {validator1 };
		///		</code>
		/// </example>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			bool myFalseValue = false;

			// We never need to raise an invalid row event
			if ( myFalseValue == true )
			{
				if ( InvalidDataRowEvent != null )
				{
					InvalidDataRowEvent( this, new InvalidDataRowEventArgs( dataRow , dataDefinition , "Always True validation error" , ValidationErrorSeverity.None ) );
				}
			}

			return ValidationErrorSeverity.None;
		}
	}
}
