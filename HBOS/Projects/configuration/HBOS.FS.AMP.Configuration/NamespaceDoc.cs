using System;
using System.Configuration;

namespace HBOS.FS.AMP.Configuration
{
#if (DEBUG)
	/// <summary>
	/// <p>
	/// The HBOS.FS.AMP.Configuration namespace allows you to programmatically access .NET Framework configuration
	/// settings stored in a remote configuration file.
	/// </p>
	/// <p>
	/// The .NET Framework <see cref="System.Configuration">System.Configuration</see> namespace provides classes and interfaces to 
	/// access configuration settings held in a local configuration file (.config file).  For Windows Forms applications, local .config 
	/// files must be deployed with the application on a user's desktop.  Where a number of instances of a Windows Forms application share common
	/// settings, such as database connection details, managing the configuration settings for each installation can become onerous.
	/// </p>
	/// <p>
	/// To overcome this, the HBOS.FS.AMP.Configuration namespace contains a <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings">ConfigurationSettings</see> class
	/// which works in conjunction with a local .config file to provide access to configuration settings held in a remote .config file
	/// using the same semantics as the <see cref="System.Configuration.ConfigurationSettings">System.Configuration.ConfigurationSettings</see> class.  
	/// Using a common remote .config file, accessible by all instances of a given Windows Forms application, makes it possible to centralise 
	/// administration of the configuration settings.
	/// </p>
	/// <p>
	/// An application that uses the <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings">ConfigurationSettings</see> class 
	/// must have a local .config file with an &lt;appSettings&gt; key/value pair that identifies the remote .config file.  The key 
	/// for this entry must be ConfigLocation and the value must be a fully qualied file name.  The following code shows an example local 
	/// .config file that points to a remote .config file at \\someserver\share\remote.config:
	/// </p>
	/// <code>
	/// &lt;xml version="1.0" encoding="utf-8" ?&gt;
	/// &lt;configuration&gt;
	///    &lt;appSettings&gt;
	///       &lt;add key="ConfigLocation" value="\\someserver\share\remote.config" /&gt;
	///    &lt;/appSettings&gt;
	/// &lt;/configuration&gt;
	/// </code>
	/// <p></p>
	/// <p>
	/// The remote .config file should be structured in the same way as a local .config file. 
	/// </p>  <p>
	/// &lt;appSettings&gt; sections in the remote .config file are accessible using 
	/// the <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings.AppSettings">AppSettings</see>
	/// property of the <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings">ConfigurationSettings</see> class. 
	/// </p>
	/// <p>
	/// User-defined configuration sections in the remote .config file are accessible using 
	/// the <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings.GetConfig">GetConfig</see>
	///  method of the <see cref="HBOS.FS.AMP.Configuration.ConfigurationSettings">ConfigurationSettings</see> class.
	/// </p>
	/// <seealso cref="System.Configuration"/>
	/// </summary>
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
