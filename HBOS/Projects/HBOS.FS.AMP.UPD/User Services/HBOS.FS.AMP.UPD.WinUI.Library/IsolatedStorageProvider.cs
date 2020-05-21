	using System.IO;
	using System.IO.IsolatedStorage;
	using System.Security.Permissions;
	using System.Xml.Serialization;
	using Microsoft.Practices.EnterpriseLibrary.Configuration;
	using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

	namespace HBOS.FS.AMP.UPD.WinUI.Classes
	{
		/// <summary>
		/// Summary description for IsolatedStorageProvider.
		/// </summary>
		[System.Security.Permissions.FileIOPermission(SecurityAction.Demand)]
		public class IsolatedStorageProvider:Microsoft.Practices.EnterpriseLibrary.Configuration.IStorageProviderReader ,IStorageProviderWriter ,IConfigurationProvider
		{	
			private string m_CurrentSectionName;
			private string m_ConfigurationName;

			/// <summary>
			/// Creates a new <see cref="IsolatedStorageProvider"/> instance.
			/// </summary>
			public IsolatedStorageProvider()
			{
		
			}

			/// <summary>
			/// Initializes the specified configuration view.
			/// </summary>
			/// <param name="configurationView">Configuration view.</param>
			public void Initialize(ConfigurationView configurationView)
			{
			
			}

			/// <summary>
			/// Writes the specified value.
			/// </summary>
			/// <param name="value">Value.</param>
			public void Write(object value)
			{
				//				if (value is UserSettings)
				//				{
				//					XmlSerializer serializer = new XmlSerializer(typeof (UserSettings));
				//					StringBuilder stringBuilder = new StringBuilder();
				//					StringWriter stringWriter = new StringWriter(stringBuilder);
				//
				//					serializer.Serialize(stringWriter, value);

				IsolatedStorageFileStream configFile=new IsolatedStorageFileStream(this.ConfigurationName,FileMode.Create);
				StreamWriter writer=new StreamWriter(configFile);

				writer.Write(((System.Xml.XmlElement)(value)).InnerXml);
				writer.Close();
				configFile.Close();
				//}
			}

			/// <summary>
			/// Reads this instance.
			/// </summary>
			/// <returns></returns>
			public object Read()
			{
				try
				{
					IsolatedStorageFileStream configFile=new IsolatedStorageFileStream(this.ConfigurationName,FileMode.Open);
					StreamReader streamReader=new StreamReader(configFile);
					string serializedSettings= streamReader.ReadToEnd();
					configFile.Close();
					XmlSerializer serializer = new XmlSerializer(typeof (UserSettings));
					StringReader stringReader = new StringReader(serializedSettings);

					//UserSettings settings = (UserSettings) serializer.Deserialize(stringReader);
					return serializer.Deserialize(stringReader);
				}
				catch (System.SystemException ex)
				{
					System.Console.WriteLine((ex.Message));
				}
				return null;
			}

			/// <summary>
			/// <para>Gets the name of the configuration section.</para>
			/// </summary>
			/// <value>
			/// <para>The name of the configuration section.</para>
			/// </value>
			public string CurrentSectionName
			{
				get { return m_CurrentSectionName; }
				set { m_CurrentSectionName = value; }
			}



			/// <summary>
			/// <para>Gets or sets the name of the provider.</para>
			/// </summary>
			/// <value><para>The name of the provider.</para></value>
			public string ConfigurationName
			{
				get { return m_ConfigurationName ; }
				set { m_ConfigurationName = value; }
			}


			/// <summary>
			/// Creates the configuration change watcher.
			/// </summary>
			/// <returns></returns>
			public IConfigurationChangeWatcher CreateConfigurationChangeWatcher()
			{
				return new ConfigurationChangeFileWatcher("","");
			}
		}
	}
