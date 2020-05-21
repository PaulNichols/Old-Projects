using System;

namespace HBOS.FS.AMP.Data.Types
{
	/// <summary>
	/// Delegate for raising invalid data row events
	/// </summary>
	/// <remarks>Raised an event args of type <see cref="InvalidDataRowEventArgs"/> which provides detailed information about the validation error.</remarks>
	public delegate void InvalidDataRowDelegate(object sender, InvalidDataRowEventArgs e);
}
