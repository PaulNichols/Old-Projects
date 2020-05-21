using System;
using System.IO;
using System.Xml;

namespace HBOS.FS.AMP.Data.Persister
{
	/// <summary>
	/// BaseXSLFilePersister - Base functionality for using an XSLT to persist a file
	/// </summary>
	/// <remarks>
	///		Remembers the XSLT resource name to use in the transform.
	///		<note>THe XSLT needs to be an embedded resource in an Assembly.</note>
	///	</remarks>
	public abstract class BaseXSLFilePersister : BaseFilePersister 
	{
		#region Variables

		private string m_xslFileResourceName = String.Empty;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which gets the file name to write to, and the XSLT resource to use in the writing process.
		/// </summary>
		/// <param name="fileName">File to persist the data to</param>
		/// <param name="xslFileResourceName">XSLT resource to use to transform the data before persisting.</param>
		public BaseXSLFilePersister( string fileName , string xslFileResourceName ) : base( fileName )
		{
			m_xslFileResourceName = xslFileResourceName;
		}
        
		#endregion

		#region Properties

		/// <summary>
		/// Inheritance access to the XSLT Resource Name
		/// </summary>
		protected string XSLTResourceName
		{
			get
			{
				return m_xslFileResourceName;
			}
		}

		#endregion
	}
}
