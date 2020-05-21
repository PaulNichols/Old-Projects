using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using HBOS.FS.AMP.UPD.Security;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using ConfigurationSettings = System.Configuration.ConfigurationSettings;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Static class for holding and retrieving global ui settings.
	/// </summary>
	public abstract /*static*/ class GlobalRegistry
	{
		private static ApplicationSettings appSettings;
		private static ConnectionStringHandler connectionStringHandler;
		private static UserSettings m_UserSettings=new UserSettings();
		private static string m_ActiveConnection="";

		/// <summary>
		/// Event to say that the user settings have been changed and a refresh might be required
		/// </summary>
		public static event EventHandler UserSettingsChanged;

		static GlobalRegistry()
		{

			if (System.IO.Directory.Exists(Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData) + @"\IsolatedStorage") 
					|| System.IO.Directory.Exists(Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData) + @"\IsolatedStorage"))
			{
				try
				{
					m_UserSettings=(UserSettings) ConfigurationManager.GetConfiguration("UPDUserSettings");
					if (m_UserSettings!=null)
					{
						m_ActiveConnection=m_UserSettings.ActiveConnection;
					}
				}
				catch 
				{
				
				}
			}
			
			appSettings=new ApplicationSettings(false);
			AppSettings.GetSettings();

			if (ActiveConnection=="")
			{
				m_ActiveConnection=appSettings.DatabaseSettings.ActiveConnectionString;
			}
			//change the boolean to true to populate the applicationsetting.config file with default values
			connectionStringHandler=new ConnectionStringHandler(AppSettings,ActiveConnection);	
		}
		
		/// <summary>
		/// Persists the settings via the application block to the specified store.
		/// </summary>
		/// <param name="settings">Settings.</param>
		public static void PersistApplicationSettings(ApplicationSettings settings)
		{
			connectionStringHandler=new ConnectionStringHandler(settings);
			ConfigurationManager.WriteConfiguration("ApplicationSettings", settings);
			
		}
		
		/// <summary>
		/// Persists the settings via the application block to the specified store.
		/// </summary>
		public static void PersistUserSettings()
		{
			PersistUserSettings( m_UserSettings);
		}

		/// <summary>
		/// Persists the settings via the application block to the specified store.
		/// </summary>
		/// <param name="settings">Settings.</param>
		public static void PersistUserSettings(UserSettings settings)
		{
			ConfigurationManager.WriteConfiguration("UPDUserSettings", settings);
			OnUserSettingsChanged();
		}

		private static void OnUserSettingsChanged()
		{
			if (UserSettingsChanged!=null)
			{
				UserSettingsChanged(null,null);
			}
		}

		/// <summary>
		/// Gets the connection string used to connect to the database.
		/// </summary>
		/// <value></value>
		public static string ConnectionString
		{
			get{return connectionStringHandler.ToString();}
		}

		/// <summary>
		/// Gets the Database Name the application will currently connect to .
		/// </summary>
		/// <value></value>
		public static string SQLDatabaseName
		{
			get{return connectionStringHandler.SQLDatabaseName;}
		}

		/// <summary>
		/// Gets the User Name and Password details the application will currently use .
		/// </summary>
		/// <value></value>
		public static string Credentials
		{
			get{return connectionStringHandler.Credentials;}
		}

		/// <summary>
		/// Gets the SQL Server Name the application will currently connect to .
		/// </summary>
		/// <value></value>
		public static string SQLServerName
		{
			get{return connectionStringHandler.SQLServerName;}
		}

		
		/// <summary>
		/// Gets the company valuation point and day for the current company.
		/// </summary>
		/// <value></value>
		public static DateTime CurrentCompanyValuationDateAndTime
		{
			get
			{
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				return updPrincipal.CurrentCompanyValuationDateAndTime;
			}
		}

		/// <summary>
		/// Gets the valuation point and day if the current was progress on.
		/// </summary>
		/// <value></value>
		public static DateTime NextCompanyValuationDateAndTime
		{
			get
			{
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				return updPrincipal.NextCompanyValuationDateAndTime;
			}
		}

		/// <summary>
		/// Gets the company code for the current user.
		/// </summary>
		/// <value></value>
		public static string CompanyCode
		{
			get
			{
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				return updPrincipal.CompanyCode;
			}
		}

		/// <summary>
		/// Gets the previous valuation point and day if the current was progress on.
		/// </summary>
		/// <value></value>
		public static DateTime PreviousCompanyValuationDateAndTime
		{
			get
			{
				UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				return updPrincipal.PreviousCompanyValuationDateAndTime;
			}
		}

		/// <summary>
		/// Gets the client version for the running instance.
		/// </summary>
		/// <value></value>
		public static Version ClientVersion
		{
			get
			{
				return Assembly.GetEntryAssembly().GetName().Version;
			}
		}

		/// <summary>
		/// Gets the app settings class which gives access to all settings.
		/// </summary>
		/// <value></value>
		public static ApplicationSettings AppSettings
		{
			get { return appSettings; }
			set
			{
				appSettings=value;
				connectionStringHandler=new ConnectionStringHandler(value);
			}
		}

		/// <summary>
		/// Gets or sets the active connection.
		/// </summary>
		/// <value></value>
		public static string ActiveConnection
		{
			get { return m_ActiveConnection; }
			set
			{
				m_UserSettings.ActiveConnection = value;
				if (m_UserSettings!=null)
				{
					m_UserSettings.ActiveConnection=value;
				}
				else
				{
					appSettings.DatabaseSettings.ActiveConnectionString=value;
				}

				connectionStringHandler=new ConnectionStringHandler(appSettings,value);
			}
		}

	}

	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public sealed class UserSettings
	{
		private string m_ActiveConnection;
		private string m_Test;

		/// <summary>
		/// Gets or sets the active connection.
		/// </summary>
		/// <value></value>
		public string ActiveConnection
		{
			get { return m_ActiveConnection; }
			set { m_ActiveConnection = value; }
		}

		/// <summary>
		/// Gets or sets the active connection.
		/// </summary>
		/// <value></value>
		public string Test
		{
			get { return m_Test; }
			set { m_Test = value; }
		}


		/// <summary>
		/// Creates a new <see cref="UserSettings"/> instance.
		/// </summary>
		public UserSettings()
		{
			
		}
	}

/// <summary>
/// 
/// </summary>
	[Serializable]
	public sealed class ApplicationSettings:ICloneable
	{
/// <summary>
/// 
/// </summary>
		public event EventHandler ApplicationSettingsChanged;

		/// <summary>
		/// 
		/// </summary>
		[Serializable]
			public enum errorUIStyleEnum
		{
			/// <summary>
			/// 
			/// </summary>
			Dev,
			/// <summary>
			/// 
			/// </summary>
			EndUser,
			/// <summary>
			/// 
			/// </summary>
			All,
		}

		private ImportFileExtensionFilters importFileExtensionFilterDetails;
		private errorUIStyleEnum errorUIStyle;
		private string exportSerialiseFileStream;
		private DatabaseDetails databaseSettings;

		/// <summary>
		/// Gets or sets the import file extension filter details.
		/// </summary>
		/// <value></value>
		public ImportFileExtensionFilters ImportFileExtensionFilterDetails
		{
			get { return importFileExtensionFilterDetails; }
			set { importFileExtensionFilterDetails = value; }
		}

		/// <summary>
		/// Gets or sets the error UI style.
		/// </summary>
		/// <value></value>
		public errorUIStyleEnum ErrorUIStyle
		{
			get { return errorUIStyle; }
			set { errorUIStyle = value; }
		}

		/// <summary>
		/// Gets or sets the export serialise file stream.
		/// </summary>
		/// <value></value>
		public string ExportSerialiseFileStream
		{
			get { return exportSerialiseFileStream; }
			set { exportSerialiseFileStream = value; }
		}

		/// <summary>
		/// Gets or sets the database settings.
		/// </summary>
		/// <value></value>
		public DatabaseDetails DatabaseSettings
		{
			get { return databaseSettings; }
			set { databaseSettings = value; }
		}


		/// <summary>
		/// Creates a new <see cref="ApplicationSettings"/> instance.
		/// </summary>
		public ApplicationSettings()
		{
		}
		
		/// <summary>
		/// Creates a new <see cref="ApplicationSettings"/> instance.
		/// </summary>
		/// <param name="setDefault">Set default.</param>
		public ApplicationSettings(bool setDefault)
		{
			#if (DEBUG)
				if (setDefault)
				{
					ApplicationSettings tempSettings=new ApplicationSettings();
					tempSettings.ImportFileExtensionFilterDetails = new ImportFileExtensionFilters(setDefault);
					tempSettings.ErrorUIStyle = errorUIStyleEnum.EndUser;
					tempSettings.ExportSerialiseFileStream = @"c:\temp\UPDExport.bin";
					tempSettings.DatabaseSettings = new DatabaseDetails(setDefault);
					ConfigurationManager.WriteConfiguration("ApplicationSettings", tempSettings);
				}
			#endif
		}

		/// <summary>
		/// Gets the settings.
		/// </summary>
		public void GetSettings()
		{
			ApplicationSettings newSettings;
			newSettings=(ApplicationSettings)ConfigurationManager.GetConfiguration("ApplicationSettings");
			this.DatabaseSettings=newSettings.DatabaseSettings;
			this.ErrorUIStyle=newSettings.ErrorUIStyle;
			this.ExportSerialiseFileStream=newSettings.ExportSerialiseFileStream;
			this.ImportFileExtensionFilterDetails= newSettings.ImportFileExtensionFilterDetails;
			ConfigurationManager.ConfigurationChanged -= new ConfigurationChangedEventHandler(ApplicationSetting_Changed);
			ConfigurationManager.ConfigurationChanged += new ConfigurationChangedEventHandler(ApplicationSetting_Changed);
		}

		private void OnApplicationSettingsChanged(object sender, ConfigurationChangedEventArgs e)
		{
			if (ApplicationSettingsChanged!=null)
			{
				this.ApplicationSettingsChanged(sender,e);
			}
		}

	
		private void ApplicationSetting_Changed(object sender, ConfigurationChangedEventArgs e)
		{
			if (e.SectionName == "ApplicationSettings")
			{
				GetSettings();
				OnApplicationSettingsChanged( sender,  e);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public class DatabaseDetails
		{
			private string activeConnectionString;

			private Microsoft.Practices.EnterpriseLibrary.Configuration.NameValueItemCollection connectionStrings;

			/// <summary>
			/// Gets or sets the active connection string.
			/// </summary>
			/// <value></value>
			public string ActiveConnectionString
			{
				get { return activeConnectionString; }
				set { activeConnectionString = value; }
			}

			/// <summary>
			/// Gets or sets the connection strings.
			/// </summary>
			/// <value></value>
			public Microsoft.Practices.EnterpriseLibrary.Configuration.NameValueItemCollection ConnectionStrings
			{
				get { return connectionStrings; }
				set { connectionStrings = value; }
			}

			/// <summary>
			/// Gets the connection strings as a hashtable.
			/// </summary>
			/// <value></value>
			public SortedList GetConnectionStrings()
			{
					SortedList returnCollection = new SortedList(connectionStrings.Count);

					foreach (NameValueItem item in connectionStrings)
					{
						returnCollection.Add(item.Name,item.Value);
					}
					return returnCollection;
			}


			/// <summary>
			/// Creates a new <see cref="DatabaseDetails"/> instance.
			/// </summary>
			public DatabaseDetails()
			{
			}

			/// <summary>
			/// Creates a new <see cref="DatabaseDetails"/> instance.
			/// </summary>
			/// <param name="setDefault">Set default.</param>
			public DatabaseDetails(bool setDefault)
			{
				#if (DEBUG)
					if (setDefault)
					{
						ActiveConnectionString = "Dev";
						ConnectionStrings=new NameValueItemCollection();
						ConnectionStrings.Add("Dev","Persist Security Info=False;User ID=upd;Password=upd;Data Source=SQLSRV95;Initial Catalog=UPD;Application Name=UPD Administration;");
						ConnectionStrings.Add("PreProd","Persist Security Info=False;User ID=upd;Password=upd;Data Source=SSF001PPD;Initial Catalog=UPD;Application Name=UPD Administration;");
						ConnectionStrings.Add("UAT94","Persist Security Info=False;User ID=upd;Password=upd;Data Source=SQLSRV94;Initial Catalog=UPD_UAT;Application Name=UPD Administration;");
						ConnectionStrings.Add("Live","Persist Security Info=False;Integrated Security=SSPI;Data Source=SSF001;Initial Catalog=UPD;Application Name=UPD Administration;");
						ConnectionStrings.Add("ENQ","Persist Security Info=False;Integrated Security=SSPI;Data Source=SQLSRV94;Initial Catalog=UPD_ENQ;Application Name=UPD Administration;");
						ConnectionStrings.Add("VAL","Persist Security Info=False;User ID=upd;Password=upd;Data Source=SQLSRV94;Initial Catalog=UPD_VAL;Application Name=UPD Administration;");
						ConnectionStrings.Add("DIST","Persist Security Info=False;Integrated Security=SSPI;Data Source=SQLSRV94;Initial Catalog=UPD_DIST;Application Name=UPD Administration;");
					}
				#endif
			}

		}

		/// <summary>
		/// 
		/// </summary>
		public class ImportFileExtensionFilters
		{
			private NameValueItemCollection filters;

			/// <summary>
			/// Creates a new <see cref="ImportFileExtensionFilters"/> instance.
			/// </summary>
			public ImportFileExtensionFilters()
			{
			}

			/// <summary>
			/// Creates a new <see cref="ImportFileExtensionFilters"/> instance.
			/// </summary>
			/// <param name="setDefault">Set default.</param>
			public ImportFileExtensionFilters(bool setDefault)
			{
				#if (DEBUG)
					if (setDefault)
					{
						Filters=new NameValueItemCollection();
						Filters.Add("ImportMarketIndicesFilter" ,"Market Indices Files (*.ind)|*.ind|All files (*.*)|*.*)");
						Filters.Add("ImportCurrencyExchangeRateFilter" , "Exchange Rate Files (*.exr)|*.exr|All files (*.*)|*.*)");
						Filters.Add("ImportHi3PricesFilter" , "Price Files (*.pro)|*.pro|All files (*.*)|*.*)");
						Filters.Add("ImportHi3AssetFundSplits" , "Asset Market Split Files (*.afs)|*.afs|All files (*.*)|*.*)");
						Filters.Add("ImportHi3CompositeFundSplits" , "Composite Fund Split Files (*.cmp)|*.cmp|All files (*.*)|*.*)");
						Filters.Add("ImportHi3CompositePricesFilter" , "Composite Price Files (*.prc)|*.prc|All files (*.*)|*.*)");
						Filters.Add("ImportHi3LinkedPricesFilter" , "Linked Price Files (*.pri)|*.pri|All files (*.*)|*.*)");
						Filters.Add("PriceComparisionReportFilter" , "Price Files (*.pro,*.pri,*.prc)|*.pro;*.pri;*.prc|All files (*.*)|*.*)");
					}
				#endif
			}

			/// <summary>
			/// Gets or sets the filters.
			/// </summary>
			/// <value></value>
			public NameValueItemCollection Filters
			{
				get
				{
					return filters;
				}
				set 
				{ 
					filters=value;
				}
			}

			/// <summary>
			/// Gets the filters as hashtable.
			/// </summary>
			/// <returns></returns>
			public Hashtable GetFilters()
			{
				Hashtable returnCollection = new Hashtable(Filters.Count);

				foreach (NameValueItem item in Filters)
				{
					returnCollection.Add(item.Name,item.Value);
				}
				return returnCollection;
			}
		}

		#region ICloneable Members

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			ApplicationSettings clonedObject=new ApplicationSettings();
			clonedObject.ErrorUIStyle = this.ErrorUIStyle;
			clonedObject.ExportSerialiseFileStream = this.ExportSerialiseFileStream;
			
			clonedObject.ImportFileExtensionFilterDetails = new ImportFileExtensionFilters();
			clonedObject.ImportFileExtensionFilterDetails.Filters=new NameValueItemCollection();
			foreach  (NameValueItem filter in this.ImportFileExtensionFilterDetails.Filters)
			{
				clonedObject.ImportFileExtensionFilterDetails.Filters.Add(filter);	
			}

			clonedObject.DatabaseSettings = new DatabaseDetails();
			clonedObject.DatabaseSettings.ActiveConnectionString=this.DatabaseSettings.ActiveConnectionString;
			clonedObject.DatabaseSettings.ConnectionStrings=new NameValueItemCollection();
			foreach  (NameValueItem connectionString in this.DatabaseSettings.ConnectionStrings)
			{
				clonedObject.DatabaseSettings.ConnectionStrings.Add(connectionString);	
			}

			return clonedObject;
			//return HelperMethods.Clone(this);
		}

		

		#endregion
	}

	/// <summary>
	/// Class to retrieve parts of a connection string from the app config
	/// </summary>
	public sealed class ConnectionStringHandler
	{
		private static string connectionString;
		private static string[] m_connectionStringParts;

		/// <summary>
		/// Creates a new <see cref="ConnectionStringHandler"/> instance.
		/// </summary>
		public ConnectionStringHandler()
		{
			string activeConnectionString=ConfigurationSettings.AppSettings["ActiveConnectionString"];
			connectionString = ConfigurationSettings.AppSettings[activeConnectionString];
			splitConnectionString();
		}

		/// <summary>
		/// Creates a new <see cref="ConnectionStringHandler"/> instance.
		/// </summary>
		/// <param name="appSettings">App settings.</param>
		public  ConnectionStringHandler(ApplicationSettings appSettings)
		{
			string activeConnectionString=appSettings.DatabaseSettings.ActiveConnectionString;
			connectionString = appSettings.DatabaseSettings.ConnectionStrings[activeConnectionString];
			splitConnectionString();
		}

		/// <summary>
		/// Creates a new <see cref="ConnectionStringHandler"/> instance.
		/// </summary>
		/// <param name="appSettings">App settings.</param>
		/// <param name="activeConnection"></param>
		public  ConnectionStringHandler(ApplicationSettings appSettings,string activeConnection)
		{
			connectionString = appSettings.DatabaseSettings.ConnectionStrings[activeConnection];
			splitConnectionString();
		}


		private  void splitConnectionString()
		{
			char [] delimiter = ";".ToCharArray();
			m_connectionStringParts= connectionString.Split(delimiter);
		}

		private  string[] getConnectionStringElement(string element)
		{
			string[] keyValue=null;
			foreach (string part in m_connectionStringParts)
			{
				char [] delimiter = "=".ToCharArray();
				keyValue=part.Split(delimiter);
				if (keyValue[0]==element)
				{
					break;
				}
				else
				{
					keyValue=null;
				}
			}
			return keyValue;
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public new  string ToString()
		{
			return connectionString;
		}

		/// <summary>
		/// Gets the Database Name the application will currently connect to .
		/// </summary>
		/// <value></value>
		public  string SQLDatabaseName
		{
			get
			{
				String returnString="";
				string[] databaseNameParts=getConnectionStringElement("Initial Catalog");
				if (databaseNameParts.Length==2)
				{
					returnString= databaseNameParts[1];
				}

				return returnString;
			}
		}
		

		/// <summary>
		/// Gets the User Name and Password details the application will currently use .
		/// </summary>
		/// <value></value>
		public string Credentials
		{
			get
			{
				string returnString="n/a";
				string[] integratedSecurity=getConnectionStringElement("Integrated Security");
				if(integratedSecurity==null)
				{
					string[] userId=getConnectionStringElement("User ID");
					string[] password=getConnectionStringElement("Password");
					if (userId!=null && password!=null)
					{
						returnString=String.Concat(userId[0],"=",userId[1],",",password[0],"=",password[1]);
					}
				}
				else
				{
					returnString=integratedSecurity[1];
				}

				return returnString;
			}
		}

		/// <summary>
		/// Gets the SQL Server Name the application will currently connect to .
		/// </summary>
		/// <value></value>
		public  string SQLServerName
		{
			get
			{
				String returnString="";
				string[] databaseNameParts=getConnectionStringElement("Data Source");
				if (databaseNameParts.Length==2)
				{
					returnString= databaseNameParts[1];
				}

				return returnString;
			}
		}

	}
	
	

}