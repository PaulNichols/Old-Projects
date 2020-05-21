using System;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

using HBOS.FS.Support.Tex;
using HBOS.FS.AMP.UPD.Exceptions;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Loads xslt files from the file system
	/// </summary>
	public class FileXsltLoader: IXsltLoader
	{
		/// <summary>
		/// Creates a new <see cref="FileXsltLoader"/> instance.
		/// </summary>
		public FileXsltLoader(string filename)
		{
			this.filename = filename;
		}

		private string filename;

		#region IXsltLoader Members

		/// <summary>
		/// Loads xslt from the file
		/// </summary>
		/// <returns></returns>
		public System.Xml.Xsl.XslTransform Load()
		{
			T.E();
			XslTransform result = new XslTransform();
		
			if (!File.Exists(this.filename))
				throw new ExportException("XSLT file does not exist",this.filename);

			XmlTextReader reader = new XmlTextReader(new StreamReader(this.filename));
			try
			{
				result.Load(this.filename);
			}
			catch (XsltCompileException ex)
			{
				throw new ExportException("XSLT does not compile",filename,ex);
			}
			catch (System.Security.SecurityException ex)
			{
				throw new ExportException("User has insufficient .NET permissions to do XSLT transformation",filename,ex);		
			}

			T.X();
			return result;
		}

		#endregion

	}
}
