using System;

namespace HBOS.FS.AMP.Data.Types
{
	#region Enum

	/// <summary>
	/// Validation Error Severity - provides an indication for the severity of a validation error.
	/// </summary>
	/// <remarks>Can be displayed in a grid, so the user can filter on a particular severity of validation error.</remarks>
	public enum ValidationErrorSeverity : int
	{
		/// <summary>
		/// No Validation Error (0)
		/// </summary>
		/// <remarks>Should not be rasied as having an exception error with a severity of None doesn't make sense.</remarks>
		None = 0,
		/// <summary>
		/// Low (1)
		/// </summary>
		/// <remarks>Low error severity - can probably be ignored.</remarks>
		Low = 1,
		/// <summary>
		/// Normal (2)
		/// </summary>
		/// <remarks>Medium error severity - can probably not be ignored.</remarks>
		Normal = 2 ,
		/// <summary>
		/// High (3)
		/// </summary>
		/// <remarks>High error severity - should not be ignored.</remarks>
		High = 3,
		/// <summary>
		/// Critical (4)
		/// </summary>
		/// <remarks>Critical error severity - should probably abort processing.</remarks>
		Critical = 4
	}

	#endregion
}
