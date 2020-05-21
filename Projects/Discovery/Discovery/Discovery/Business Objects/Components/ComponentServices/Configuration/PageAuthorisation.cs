using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using Discovery.Utility.DataAccess;

namespace Discovery.ComponentServices.Configuration
{
    /*************************************************************************************************
	** CLASS:	PageAuthorisation
	**
	** OVERVIEW:
	** Reads PageAuthorisation configuration setting from web.config 
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 13/7/06		1.0			PJN		Initial Version
	************************************************************************************************/
    /// <summary>
    /// A class to perform the page authorisation configuration setting from web.config
    /// with name space Discovery.ComponentServices.Configuration
    /// </summary>
    public class PageAuthorisationConfiguration
    {
        private const string pageAuthorisationBase = "";

        private Hashtable m_hashPageAuthorisations = new Hashtable();
        private string defaultPageAuthorisation = "";


        /// <summary>
        /// Gets the page authorisation configuration.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns></returns>
        public static PageAuthorisationConfiguration GetPageAuthorisationConfiguration(string pageName)
        {
            try
            {
                // Load specified web.config section ("Discovery/?????")
                string cacheKey = pageAuthorisationBase + pageName;
                // See if the PageAuthorisation config is in the cache
                PageAuthorisationConfiguration pageAuthorisationConfig =
                    (PageAuthorisationConfiguration) DataCache.GetCache(cacheKey);
                // Check if PageAuthorisation config was in cache
                if (null == pageAuthorisationConfig)
                {
                    // This uses PageAuthorisationConfigurationHandler to read custon settings
                    pageAuthorisationConfig =
                        (PageAuthorisationConfiguration)
                        ConfigurationManager.GetSection(pageAuthorisationBase + pageName);
                    // Insert the type into the cache
                    DataCache.SetCache(cacheKey, pageAuthorisationConfig);
                }
                // return the PageAuthorisation configuration
                return pageAuthorisationConfig;
            }
            catch
            {
                // No PageAuthorisation
                return null;
            }
        }

        /*************************************************************************************************
		** METHOD:	LoadValuesFromConfigurationXml
		** IN:		XmlNode xmlNode, XML node to read values from
		** OUT:		void
		**
		** OVERVIEW:
		** Reads all PageAuthorisations from the specified config node.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Loads the values from configuration XML.
        /// </summary>
        /// <param name="nodeXml">The node XML.</param>
        public void LoadValuesFromConfigurationXml(XmlNode nodeXml)
        {
            XmlAttributeCollection xmlAttribColl = nodeXml.Attributes;
            // Get the default PageAuthorisation ("defaultPageAuthorisation")
            defaultPageAuthorisation = xmlAttribColl["defaultPageAuthorisation"].Value;
            // Read child nodes ("pages")
            foreach (XmlNode nodeChild in nodeXml)
            {
                if (nodeChild.Name == "pages")
                {
                    GetPageAuthorisations(nodeChild);
                }
            }
        }

        /*************************************************************************************************
		** METHOD:	GetPageAuthorisations
		** IN:		void
		** OUT:		void
		**
		** OVERVIEW:
		** Reads all the PageAuthorisations for the specified PageAuthorisation node.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Gets the page authorisations.
        /// </summary>
        /// <param name="nodePageAuthorisations">The node page authorisations.</param>
        public void GetPageAuthorisations(XmlNode nodePageAuthorisations)
        {
            foreach (XmlNode nodePageAuthorisation in nodePageAuthorisations)
            {
                switch (nodePageAuthorisation.Name)
                {
                    case "add":
                        {
                            m_hashPageAuthorisations.Add(nodePageAuthorisation.Attributes["name"].Value,
                                                         new PageAuthorisation(nodePageAuthorisation.Attributes));
                            break;
                        }
                    case "remove":
                        {
                            m_hashPageAuthorisations.Remove(nodePageAuthorisation.Attributes["name"].Value);
                            break;
                        }
                    case "clear":
                        {
                            m_hashPageAuthorisations.Clear();
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Gets the default page authorisation.
        /// </summary>
        /// <value>The default page authorisation.</value>
        public string DefaultPageAuthorisation
        {
            get { return defaultPageAuthorisation; }
        }

        /// <summary>
        /// Gets the page authorisations.
        /// </summary>
        /// <value>The page authorisations.</value>
        public Hashtable PageAuthorisations
        {
            get { return m_hashPageAuthorisations; }
        }
    }

    /*************************************************************************************************
	** CLASS:	PageAuthorisation
	**
	** OVERVIEW:
	** Represents a PageAuthorisation read from web.config.
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 1/8/04		1.0			LAS		Initial Version
	************************************************************************************************/

    /// <summary>
    /// A class to read a page authorisation from web.config
    /// </summary>
    public class PageAuthorisation
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
        private string pageName = "";
        private string pageRules = "";
        private NameValueCollection pageAuthorisationAttributes = new NameValueCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageAuthorisation"/> class.
        /// </summary>
        public PageAuthorisation()
        {
        }

        /*************************************************************************************************
		** METHOD:	PageAuthorisation
		** IN:		XmlAttributeCollection attribCollection, collection of attributes for PageAuthorisation
		** OUT:		void
		**
		** OVERVIEW:
		** Constructs a PageAuthorisation based on the specified XML PageAuthorisation node.  All properties of of the
		** PageAuthorisation are specified as attributes.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PageAuthorisation"/> class.
        /// </summary>
        /// <param name="attribCollection">The attrib collection.</param>
        public PageAuthorisation(XmlAttributeCollection attribCollection)
        {
            // Store name of PageAuthorisation
            pageName = attribCollection["name"].Value;
            // Store type of PageAuthorisation
            pageRules = attribCollection["rules"].Value;
            // Store all the PageAuthorisation attributes (all PageAuthorisations are specified via attributes)
            foreach (XmlAttribute attribItem in attribCollection)
            {
                // Add attribute to NameValueCollection 
                pageAuthorisationAttributes.Add(attribItem.Name, attribItem.Value);
            }
        }

        /// <summary>
        /// Gets the name of the page.
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName
        {
            get { return pageName; }
        }

        /// <summary>
        /// Gets the page rules.
        /// </summary>
        /// <value>The page rules.</value>
        public string PageRules
        {
            get { return pageRules; }
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public NameValueCollection Attributes
        {
            get { return pageAuthorisationAttributes; }
        }
    }

    /*************************************************************************************************
	** CLASS:	PageAuthorisationConfigurationHandler
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

    /// <summary>
    /// A class to register the handler the page authorisation configuration
    /// </summary>
    public class PageAuthorisationConfigurationHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section"></param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            // Create an instance of our PageAuthorisationConfiguration class
            PageAuthorisationConfiguration pageAuthorisationConfig = new PageAuthorisationConfiguration();
            // Load settings from web.config "Discovery/pageAuthorisation", etc
            pageAuthorisationConfig.LoadValuesFromConfigurationXml(section);
            // Return the PageAuthorisation config
            return pageAuthorisationConfig;
        }
    }
}