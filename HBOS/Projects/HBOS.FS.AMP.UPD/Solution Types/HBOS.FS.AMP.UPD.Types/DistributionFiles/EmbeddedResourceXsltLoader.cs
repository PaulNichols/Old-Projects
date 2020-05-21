using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;

using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Types.DistributionFiles
{
	/// <summary>
	/// Loads xslt from embedded resources
	/// </summary>
	public class EmbeddedResourceXsltLoader: IXsltLoader
	{
		/// <summary>
		/// Creates a new <see cref="EmbeddedResourceXsltLoader"/> instance.
		/// </summary>
		public EmbeddedResourceXsltLoader(string resourceName)
		{
			T.E();
			this.resourceName = resourceName;
			T.X();
		}

		private string resourceName;

		#region IXsltLoader Members

		/// <summary>
		/// Loads the xslt from the embedded resource
		/// </summary>
		/// <returns></returns>
		public XslTransform Load()
		{
			T.E();

			XslTransform result = new XslTransform();
			XmlTextReader xmlReader;

			if (resourceName == null || resourceName == string.Empty)
				throw new ExportException("Resouce name not specified");

			Stream xsltStream = retrieveXslt();
			if (xsltStream == null)
				throw new ExportException("Could not retrieve the embedded xslt resource",resourceName);

			
			try
			{
				xmlReader = new XmlTextReader( xsltStream , XmlNodeType.Document , null );
				xmlReader.MoveToContent();
			}
			catch(XmlException ex)
			{
				throw new ExportException("XSLT is not a valid XML document",resourceName,ex);
			}		
			
			try
			{
				result.Load( xmlReader, null , retrieveResourceAssembly().Evidence);
			}
			catch (XsltCompileException ex)
			{
				throw new ExportException("XSLT does not compile",resourceName,ex);
			}
			catch (System.Security.SecurityException ex)
			{
				throw new ExportException("User has insufficient .NET permissions to do XSLT transformation",resourceName,ex);		
			}

			T.X();
			return result;
		}

		#endregion

		private Stream retrieveXslt()
		{
			T.E();

			Stream result = null;
			Assembly assembly = retrieveResourceAssembly();
			if (assembly != null)
			{
				try
				{
					result = assembly.GetManifestResourceStream(resourceName);	
				}
				catch (System.Security.SecurityException ex)
				{
					throw new ExportException("User has insufficient .NET permissions to load resource",resourceName,ex);
				}			
				
				result.Position = 0;
			}

			T.X();
			return result;
		}

		private Assembly retrieveResourceAssembly()
		{
			T.E();

			const string assemblyName = "HBOS.FS.AMP.UPD.WinUI.Library";

			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly result = null;
			foreach(Assembly assembly in assemblies)
			{
				if (assembly.FullName == assemblyName)
				{
					result = assembly;
					break;
				}
			}

			if (result == null)
			{
				try
				{
					result = AppDomain.CurrentDomain.Load(assemblyName);
				}
				catch(FileNotFoundException ex)
				{
					throw new ExportException(string.Format("Cannot load assembly {0} to get resources",assemblyName),resourceName,ex);
				}
				catch(System.Security.SecurityException ex)
				{
					throw new ExportException(string.Format("User has insufficient .NET permissions to load assembly {0} to get resources",assemblyName),resourceName,ex);
				}
			}

			T.X();
			return result;
		}
	}
}
