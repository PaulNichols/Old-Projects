using System;
using System.Xml;
using System.Xml.Xsl;

using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Summary description for DatabaseXsltLoader.
	/// </summary>
	public class DatabaseXsltLoader: IXsltLoader
	{
		/// <summary>
		/// Creates a new <see cref="DatabaseXsltLoader"/> instance.
		/// </summary>
		/// <param name="xsltSource">XSLT source.</param>
		public DatabaseXsltLoader(string xsltSource)
		{
			T.E();
			this.xsltSource = xsltSource;
			T.X();
		}

		private string xsltSource;
 
		#region IXsltLoader Members

		/// <summary>
		/// Loads the xslt from the database into a XslTransform object
		/// </summary>
		/// <returns>A loaded XslTransform object</returns>
		/// <exception cref="ExportException">
		/// If the xml/xslt is invalid or the user has insufficient rights to load the transformation
		/// </exception>
		public XslTransform Load()
		{
			T.E();
			XslTransform result = new XslTransform();

			XmlDocument doc = new XmlDocument();		
			try
			{
				doc.LoadXml(this.xsltSource);
			}
			catch (XmlException ex)
			{
				throw new ExportException("XSLT is not a valid XML document",xsltSource,ex);
			}

			XmlNodeReader reader = new XmlNodeReader(doc);		
			try
			{
				result.Load(reader,null,this.GetType().Assembly.Evidence);
			}
			catch (XsltCompileException ex)
			{
				throw new ExportException("XSLT does not compile",xsltSource,ex);
			}
			catch (System.Security.SecurityException ex)
			{
				throw new ExportException("User has insufficient .NET permissions to do XSLT transformation",xsltSource,ex);		
			}
			
			T.X();
			return result;
		}

		#endregion
	}
}
