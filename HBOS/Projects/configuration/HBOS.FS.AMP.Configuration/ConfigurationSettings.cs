using System;
using System.Xml;
using System.Collections.Specialized;
using SysCon = System.Configuration;

namespace HBOS.FS.AMP.Configuration
{
	/// <summary>
	/// <p>Provides access to configuration settings in a specified configuration section in a remote .config file.</p>
	/// </summary>
	public sealed class ConfigurationSettings
	{

		#region Constructors

		/// <summary>
		/// Private constructor - no instance methods on this class and we don't need to show
		/// a constructor in the nDoc documentation
		/// </summary>
		private ConfigurationSettings()
		{
		}

		#endregion Constructors

		#region Public Methods

		#region AppSettings

		/// <summary>
		/// Gets configuration settings in the &lt;appSettings&gt; Element configuration 
		/// section of the remote .config file.
		/// </summary>
		/// <value>A <see cref="System.Collections.Specialized.NameValueCollection">NameValueCollection</see> containing name/value pairs of configuration settings.</value>
		/// <example>
		/// Given the following local and remote .config files:
		/// <code>
		/// //
		/// // local .config file which points to the remote .config file via the ConfigLocation setting
		/// // 
		/// &lt;xml version="1.0" encoding="utf-8" ?&gt;
		/// &lt;configuration&gt;
		///    &lt;appSettings&gt;
		///       &lt;add key="ConfigLocation" value="\\someserver\share\remote.config" /&gt;
		///    &lt;/appSettings&gt;
		/// &lt;/configuration&gt;
		///
		/// //
		/// // remote .config file (\\someserver\share\remote.config)
		/// //
		/// &lt;xml version="1.0" encoding="utf-8" ?&gt;
		/// &lt;configuration&gt;
		///    &lt;appSettings&gt;
		///       &lt;add key="connectionString" value="Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MYDB;Data Source=MYSERVER" /&gt;
		///       &lt;add key="anotherKey" value="another value" /&gt;
		///    &lt;/appSettings&gt;
		/// &lt;/configuration&gt;
		/// </code>
		///    <para>
		///    The following code would retrieve the connection string value from the remote .config file:
		///    <code escaped="true" lang="C#">
		///       string connString = HBOS.FS.AMP.ConfigurationSettings.AppSettings[ "connectionString" ];
		///    </code>
		///    </para>
		/// </example>
		public static NameValueCollection AppSettings
		{
			get
			{
				string configLocation = getConfigLocation();
				return getAppSettings( configLocation );
			}
		}

		#endregion AppSettings

		#region GetConfig

		/// <summary>
		/// Returns configuration settings for a user-defined configuration 
		/// section in the remote .config file.
		/// </summary>
		/// <param name="sectionName">The configuration section to read.</param>
		/// <returns>The configuration settings for sectionName.</returns>
		/// <remarks>User-defined configuration sections are accessed via custom configuration section handlers
		/// which must implement <see cref="System.Configuration.IConfigurationSectionHandler">IConfigurationSectionHandler</see>.
		/// </remarks>
		/// <example>
		/// The following is an example remote .config file containing a user-defined configuration section:
		/// <code>
		/// //
		/// // remote .config file 
		/// //
		/// &lt;xml version="1.0" encoding="utf-8" ?&gt;
		/// &lt;configuration&gt;
		///    &lt;configSections&gt;
		///       &lt;section name="messages" type="ConfigExample.MessageSectionHandler,ConfigExample.exe" /&gt;
		///    &lt;/configSections&gt;
		///    
		///    &lt;messages&gt;
		///       &lt;messageEntry&gt;Hello World&lt;/messageEntry&gt;
		///    &lt;/messages&gt;
		/// &lt;/configuration&gt;
		/// </code>
		/// The following console application shows how the user-defined section shown above could be accessed:
		/// <code escaped="true" lang="C#">
		/// using System;
		/// using System.Xml;
		/// using System.Configuration;
		/// using Cnfg = HBOS.FS.AMP.Configuration;
		/// 
		/// namespace ConfigExample
		/// {
		///     public class MessageSectionHandler : IConfigurationSectionHandler
		///     {
		///	        public object Create(object parent, object configContext, System.Xml.XmlNode section)
		///         {
		///             return section;
		///         }
		///     }
        ///
		///     class ConfigExample
		///     {
	    ///         [STAThread]
		///         static void Main(string[] args)
	    ///         {
		///             XmlNode msgNode = (XmlNode)Cnfg.ConfigurationSettings.GetConfig( "messages" );
		///             Console.WriteLine(msgNode.ChildNodes[0].InnerXml);
		///             Console.ReadLine();
		///         }
		///     }
	    /// }
		/// </code>
		/// </example>
		public static object GetConfig(string sectionName)
		{
			string configLocation = getConfigLocation();
			return getConfigSection( sectionName, configLocation );
		}

		#endregion GetConfig

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Get the appSettings from the confile file
		/// </summary>
		/// <param name="configLocation">file location of the config file</param>
		/// <returns>NameValue pair of applicaiton settings.</returns>
		private static NameValueCollection getAppSettings ( string configLocation )
		{
			try
			{
				// open the cofiguration file and retrieve the relevant node					
				XmlDocument configDocument = new XmlDocument();
				XmlTextReader myReader = null;

				// Unlikely to be multithreaded, but just in case.
				lock(  typeof(ConfigurationSettings) )
				{
					myReader = new XmlTextReader( configLocation );
					configDocument.Load( myReader);

					myReader.Close();
				}

				// Get the appSettings chunk from the config file
				XmlNode settingsNode = getConfigurationSettings( ref configDocument, "appSettings");

				if (settingsNode == null)
				{
					throw(new Exception( String.Format( "appSettings does not exist in assembly configuration file {0}." , configLocation )));
				}

				// create the handler and return
				SysCon.NameValueSectionHandler handler = new SysCon.NameValueSectionHandler();
				return (NameValueCollection)handler.Create(null, null, settingsNode);
			}
			catch(Exception ex)
			{
				throw( ex );
			}
		}

		/// <summary>
		/// Get a custom config section out of the Config file
		/// </summary>
		/// <param name="sectionName">section name to retrieve from config file</param>
		/// <param name="configLocation">file name to retrieve config settings from</param>
		/// <returns></returns>
		private static object getConfigSection ( string sectionName, string configLocation )
		{
			try
			{
				// open and parse the configuration file
				XmlDocument configDocument = new XmlDocument();
				XmlTextReader myReader = null;

				lock( typeof(ConfigurationSettings) )
				{
					myReader = new XmlTextReader(configLocation);
					configDocument.Load( myReader );
					myReader.Close();
				}
				// retrieve the section definition and store the handler name and containing assembly
				XmlNode sectionDefinition = configDocument.SelectSingleNode("configuration/configSections/section[@name = '" + sectionName + "']");

				if (sectionDefinition == null)
				{
					throw(new Exception( String.Format( "Section definition '{0}' does not exist in assembly configuration file {1}." , sectionName , configLocation )));
				}

				string sectionType = sectionDefinition.Attributes.GetNamedItem("type").Value;
				string[] sectionHandlerDetails = sectionType.Split(','); 

				string sectionHandlerName = sectionHandlerDetails[0].Trim();
				string handlerAssemblyName = sectionHandlerDetails[1].Trim();

				// load the handler's assembly and create an instance of the handler
				System.Reflection.Assembly handlerAssembly = null;
				try
				{
					handlerAssembly = System.Reflection.Assembly.LoadFrom(handlerAssemblyName);
				}
				catch
				{}
				if ( handlerAssembly == null )
				{
					try
					{
						handlerAssembly = System.Reflection.Assembly.LoadWithPartialName(handlerAssemblyName);
					}
					catch
					{}
				}

				object objectHandler = handlerAssembly.CreateInstance(sectionHandlerName, true);
					
				// check that the handler implements the IConfigurationSectionHandler interface
				SysCon.IConfigurationSectionHandler sectionHandler = objectHandler as SysCon.IConfigurationSectionHandler;
					
				if (sectionHandler == null)
				{
					throw(new Exception("Exception creating custom SectionHandler"));
				}
					
				// retrieve the relevant section and call the section handler
				XmlNode settingNode = getConfigurationSettings( ref configDocument, sectionName);

				if (settingNode == null)
				{
					throw(new Exception("Configuration section does not exist in assembly configuration file"));
				}

				return sectionHandler.Create(null, null, settingNode);
			}

			catch(Exception ex)
			{
				throw(ex);
			}
		}

		/// <summary>
		/// Get the appropriate Xml Chunk from the config file
		/// </summary>
		/// <param name="configDocument"></param>
		/// <param name="sectionName"></param>
		/// <returns></returns>
		private static XmlNode getConfigurationSettings(ref XmlDocument configDocument, string sectionName)
		{
			XmlNodeList sectionNodes = configDocument.GetElementsByTagName(sectionName);

			foreach( XmlNode node in sectionNodes )
			{
				if( node.LocalName == sectionName )
				{
					return node;
				}
			}

			return null;
		}
		
		/// <summary>
		/// Get the location of the central config file by looking in App.Config
		/// </summary>
		/// <returns></returns>
		private static string getConfigLocation()
		{
			return SysCon.ConfigurationSettings.AppSettings[ "ConfigLocation" ];
		}

		#endregion

	}
}
