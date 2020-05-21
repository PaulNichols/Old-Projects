using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// The textual representation of an item in an enumerated list.
	/// </summary>
	/// <example>The following sample shows a decorated enum
	/// <code>
	/// public enum AuditEventSource
	///	{
	///		Null = 0,
	///	
	///		[EnumDisplayTextAttribute( "Exception Management" )]
	///		ExceptionManagement = 1,
	///	
	///		[EnumDisplayTextAttribute( "Agency Earnings" )]
	///		AgencyEarnings = 2,
	///	
	///		[EnumDisplayTextAttribute( "Agency Management" )]
	///		AgencyManagment = 3,
	///	
	///		[EnumDisplayTextAttribute( "Common" )]
	///		HBOS_FS_Common = 4
	///	}
	///	</code>
	/// </example>
	[AttributeUsage( AttributeTargets.Field )]
	public class EnumDisplayTextAttribute: Attribute
	{
		private string m_displayText;

		/// <summary>
		/// Construct a new display text attribute
		/// </summary>
		/// <param name="displayText"></param>
		public EnumDisplayTextAttribute( string displayText )
		{
			m_displayText = displayText;
		}

		/// <summary>
		/// Retrieve the display text
		/// </summary>
		public string DisplayText
		{
			get
			{
				return m_displayText;
			}
		}
	}
}
