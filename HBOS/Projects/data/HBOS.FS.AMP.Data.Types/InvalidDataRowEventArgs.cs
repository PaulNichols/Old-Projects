using System;
using System.Data;

namespace HBOS.FS.AMP.Data.Types
{
	/// <summary>
	/// InvalidDataRowEventArgs - detailed information for an invalid row of data.
	/// </summary>
	/// <remarks>Contains detailed information about the validation error.
	/// <list type="number">
	///		<item><see cref="Row"/> - the invalid data row</item>
	///		<item><see cref="DataDefinition"/> - the data definition that may have been used in the validation process.</item>
	///		<item><see cref="Message"/> - the Validation message to display to the user.</item>
	///		<item><see cref="ValidationErrorSeverity"/> - the severity of the validaiton error.</item>
	/// </list>
	/// </remarks>
	public class InvalidDataRowEventArgs
	{
		#region Variables

		private readonly string[] m_InvalidRow;
		private readonly DataTable m_dataDefinition;
		private readonly string m_Message;
		private ValidationErrorSeverity m_validationErrorSeverity = ValidationErrorSeverity.None;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor to create an InvalidDataRowEventArgs
		/// </summary>
		/// <param name="invalidRow">The data row that is invalid</param>
		/// <param name="dataDefinition">The data definition that was used to validate the data.</param>
		/// <param name="message">The message to display to the user.</param>
		/// <param name="validationErrorSeverity">The severity of the Validation Error.</param>
		public InvalidDataRowEventArgs(string[] invalidRow, DataTable dataDefinition, string message , ValidationErrorSeverity validationErrorSeverity )
		{
			m_InvalidRow = invalidRow;
			m_dataDefinition = dataDefinition;
			m_Message = message;
			m_validationErrorSeverity = validationErrorSeverity;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Access to the data row that contains invalid data.
		/// </summary>
		public string[] Row
		{
			get { return m_InvalidRow; }
		}

		/// <summary>
		/// Access to the schema data definition that was used in the data validation process.
		/// </summary>
		public DataTable DataDefinition
		{
			get { return m_dataDefinition; }
		}

		/// <summary>
		/// Access to the Message to display to the user.
		/// </summary>
		public string Message
		{
			get { return m_Message; }
		}

		/// <summary>
		/// Access to the Validation Error Severity
		/// </summary>
		/// <remarks>Can be None, Low, Medium, High, Critical</remarks>
		public ValidationErrorSeverity ValidationErrorSeverity
		{
			get
			{
				return m_validationErrorSeverity;
			}
		}

		#endregion
	}
}
