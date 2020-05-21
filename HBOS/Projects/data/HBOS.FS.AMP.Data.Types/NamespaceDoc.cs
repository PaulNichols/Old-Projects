using System;

namespace HBOS.FS.AMP.Data.Types
{
#if (DEBUG)

	/// <summary>
	/// <see cref="HBOS.FS.AMP.Data.Types"/> is a "holding" area for enums, delegates and events which are required throughout AMP.Data.
	/// These could possible be defined as part of other assemblies. However by breaking into a seperate assembly, it means that things which
	/// need to listen for a particular event etc need only reference this component.
	/// </summary>
	/// <remarks>
	/// Contains definitions for the following:
	/// <list type="bullet">
	///		<item><see cref="ValidationErrorSeverity"/></item>
	///		<item><see cref="InvalidDataRowDelegate"/></item>
	///		<item><see cref="InvalidDataRowEventArgs"/></item>
	/// </list>
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
