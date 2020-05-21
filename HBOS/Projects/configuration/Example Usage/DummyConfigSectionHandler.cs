using System;
using System.Configuration;

namespace ConfigTestHarness
{
	/// <summary>
	/// DummyConfigSectionHandler.
	/// </summary>
	public class DummyConfigSectionHandler : IConfigurationSectionHandler
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public DummyConfigSectionHandler() : base()
		{
		}

		/// <summary>
		/// Necessary part of the Interface
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="configContext"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			// Just return the Xml
			return section;
		}
	}
}
