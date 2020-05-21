using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using Discovery.Utility.DataAccess;

namespace Discovery.Utility.Configuration
{
	/*************************************************************************************************
	** CLASS:	ProviderConfiguration
	**
	** OVERVIEW:
	** Reads provider configuration setting from web.config
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 1/8/04		1.0			LAS		Initial Version
	************************************************************************************************/	/// <summary>
	public class ProviderConfiguration
	{
		/*************************************************************************************************
		** CONSTANTS
		************************************************************************************************/
		private const string m_strProviderBase = "";

		/*************************************************************************************************
		** PUBLIC MEMBER VARIABLES
		************************************************************************************************/

		/*************************************************************************************************
		** PRIVATE MEMBER VARIABLES
		************************************************************************************************/
		private Hashtable m_hashProviders = new Hashtable();
		private string m_strDefaultProvider = "";
		
		/*************************************************************************************************
		** METHOD:	Provider
		** IN:		void
		** OUT:		void
		**
		** OVERVIEW:
		** Constructor
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
		public ProviderConfiguration()
		{
		}

		/*************************************************************************************************
		** METHOD:	GetProviderConfiguration
		** IN:		string strProvider, name of provider to retrieve, eg "data"
		** OUT:		void
		**
		** OVERVIEW:
		** Static function used to load configuration settings from web.config within Discovery section.
		** Provider example is "Discovery/data"
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
		public static ProviderConfiguration GetProviderConfiguration(string strProvider)
		{
            try
            {
                // Load specified web.config section ("Discovery/?????")
                string strCacheKey = m_strProviderBase + strProvider;
                // See if the provider config is in the cache
                ProviderConfiguration objProviderConfig = (ProviderConfiguration)DataCache.GetCache(strCacheKey);
                // Check if provider config was in cache
                if (null == objProviderConfig)
                {
                    // This uses ProviderConfigurationHandler to read custon settings
                    objProviderConfig = (ProviderConfiguration)ConfigurationSettings.GetConfig(m_strProviderBase + strProvider);
                    // Insert the type into the cache
                    DataCache.SetCache(strCacheKey, objProviderConfig);
                }
                // return the provider configuration
                return objProviderConfig;
            }
            catch
            {
                // No provider
               throw new Discovery.ComponentServices.ExceptionHandling.DataProviderException("Null Provider Instance");
            }
		}

		/*************************************************************************************************
		** METHOD:	LoadValuesFromConfigurationXml
		** IN:		XmlNode xmlNode, XML node to read values from
		** OUT:		void
		**
		** OVERVIEW:
		** Reads all providers from the specified config node.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
		public void LoadValuesFromConfigurationXml(XmlNode nodeXml)
		{
			XmlAttributeCollection xmlAttribColl = nodeXml.Attributes; 
			// Get the default provider ("defaultProvider")
			m_strDefaultProvider = xmlAttribColl["defaultProvider"].Value;
			// Read child nodes ("providers")
			foreach(XmlNode nodeChild in nodeXml)
			{
				if (nodeChild.Name == "providers")
				{
					GetProviders(nodeChild);
				}
			}
		}

		/*************************************************************************************************
		** METHOD:	GetProviders
		** IN:		void
		** OUT:		void
		**
		** OVERVIEW:
		** Reads all the providers for the specified provider node.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
		public void GetProviders(XmlNode nodeProviders)
		{
			foreach(XmlNode nodeProvider in nodeProviders)
			{
				switch(nodeProvider.Name)
				{
					case "add":
					{
						m_hashProviders.Add(nodeProvider.Attributes["name"].Value, new Provider(nodeProvider.Attributes));
						break;
					}
					case "remove":
					{
						m_hashProviders.Remove(nodeProvider.Attributes["name"].Value);
						break;
					}
					case "clear":
					{
						m_hashProviders.Clear();
						break;
					}
				}
			}
		}

		public string DefaultProvider
		{
			get
			{
				return m_strDefaultProvider;
			}
		}

		public Hashtable Providers
		{
			get
			{
				return m_hashProviders;
			}
		}

	}

	/*************************************************************************************************
	** CLASS:	Provider
	**
	** OVERVIEW:
	** Represents a provider read from web.config.
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 1/8/04		1.0			LAS		Initial Version
	************************************************************************************************/
	public class Provider
	{
		/*************************************************************************************************
		** CONSTANTS
		************************************************************************************************/

		/*************************************************************************************************
		** PUBLIC MEMBER VARIABLES
		************************************************************************************************/

		/*************************************************************************************************
		** PRIVATE MEMBER VARIABLES
		************************************************************************************************/
		private string m_strProviderName = "";
		private string m_strProviderType = "";
		private NameValueCollection m_collProviderAttributes = new NameValueCollection();

		public Provider()
		{
		}

		/*************************************************************************************************
		** METHOD:	Provider
		** IN:		XmlAttributeCollection attribCollection, collection of attributes for provider
		** OUT:		void
		**
		** OVERVIEW:
		** Constructs a provider based on the specified XML provider node.  All properties of of the
		** provider are specified as attributes.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
		public Provider(XmlAttributeCollection attribCollection)
		{
			// Store name of provider
			m_strProviderName = attribCollection["name"].Value;
			// Store type of provider
			m_strProviderType = attribCollection["type"].Value;
			// Store all the provider attributes (all providers are specified via attributes)
			foreach(XmlAttribute attribItem in attribCollection)
			{
				// Make sure the attribute is not name or type
				if ("name" != attribItem.Name && "type" != attribItem.Name)
				{
					// Add attribute to NameValueCollection 
					m_collProviderAttributes.Add(attribItem.Name, attribItem.Value);
				}
			}
		}

		public string Name
		{
			get
			{
				return m_strProviderName;
			}
		}

		public string Type
		{
			get
			{
				return m_strProviderType;
			}
		}

		public NameValueCollection Attributes
		{
			get
			{
				return m_collProviderAttributes;
			}
		}
	}

	/*************************************************************************************************
	** CLASS:	ProviderConfigurationHandler
	**
	** OVERVIEW:
	** Implements IConfigurationSectionHandler used to read custom web.config sections.  This handler
	** is registered via <configSections> entry in web.config.
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 1/8/04		1.0			LAS		Initial Version
	************************************************************************************************/
	public class ProviderConfigurationHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			// Create an instance of our ProviderConfiguration class
			ProviderConfiguration providerConfig = new ProviderConfiguration();
			// Load settings from web.config "Discovery/data", etc
			providerConfig.LoadValuesFromConfigurationXml(section);
			// Return the provider config
			return providerConfig;
		}
	}
}
