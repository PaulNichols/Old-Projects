/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:10
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace Bob.DataClasses {

	internal enum CurrentExecution {
	
		None,
		SqlDataReader,
		XmlReader
	}

	/// <summary>
	/// Constants that describe at which level the exception has occured if one has occured.
	/// </summary>
	public enum ErrorSource {

		/// <summary>
		/// Error level is not available yet
		/// </summary>
		NotAvailable,
		/// <summary>
		/// No error has occured during the call
		/// </summary>
		NoError,
		/// <summary>
		/// An error has occured while initializing a SQLConnection object.
		/// </summary>
		ConnectionInitialization,
		/// <summary>
		/// An error has occured while setting some parameters to the SQLCommand object.
		/// </summary>
		ParametersSetting,
		/// <summary>
		/// An error has occured while opening the SQLConnection object.
		/// </summary>
		ConnectionOpening,
		/// <summary>
		/// An error has occured while executing the SQLCommand object.
		/// </summary>
		QueryExecution,
		/// <summary>
		/// An error has occured while retrieving some parameters from the SQLCommand object.
		/// </summary>
		ParametersRetrieval,
		/// <summary>
		/// An unhandled error has occured during the call.
		/// </summary>
		Other
	};

	/// <summary>
	/// Describes the various way to connect to a Sql Server database.
	/// </summary>
	public enum ConnectionType {
		/// <summary>
		/// The connection type was not defined yet.
		/// </summary>
		None = 0,
		/// <summary>
		/// Connection to the Sql Server database is using a supplied connection string.
		/// </summary>
		ConnectionString = 1,
		/// <summary>
		/// Connection to the Sql Server database is using a supplied System.Data.SqlClient.SqlConnection.
		/// </summary>
		SqlConnection = 2,
		/// <summary>
		/// Connection to the Sql Server database is using a supplied System.Data.SqlClient.SqlTransaction.
		/// </summary>
		SqlTransaction = 3
	};

	/// <summary>
	/// This interface is implemented by all the "Parameters" classes.
	/// </summary>
	public interface IParameter {

		/// <summary>
		/// Sets the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		void SetUpConnection(string connectionString);
		/// <summary>
		/// Returns the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		System.String ConnectionString { get; }
		/// <summary>
		/// Sets the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		void SetUpConnection(System.Data.SqlClient.SqlConnection sqlConnection);
		/// <summary>
		/// Returns the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		System.Data.SqlClient.SqlConnection SqlConnection { get; }
		/// <summary>
		/// Sets the System.Data.SqlClient.SqlTransaction to be used
		/// against the SQL Server database.
		/// </summary>
		void SetUpConnection(System.Data.SqlClient.SqlTransaction sqlTransaction);
		/// <summary>
		/// Returns the System.Data.SqlClient.SqlTransaction to be used
		/// against the SQL Server database.
		/// </summary>
		System.Data.SqlClient.SqlTransaction SqlTransaction { get; }
		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString, SqlConnection
		/// or SqlTransaction
		/// </summary>
		ConnectionType ConnectionType { get; }
		/// <summary>
		/// In case of a System exception, returns the standard exception
		/// (System.Exception) that has occured.
		/// </summary>
		System.Exception OtherException { get; }
		/// <summary>
		/// In case of an ADO exception, returns the SqlException exception
		/// (System.Data.SqlClient.SqlException) that has occured.
		/// </summary>
		System.Data.SqlClient.SqlException SqlException { get; }
		/// <summary>
		/// Allows you to know at which step did the error occured if one occured.
		/// See <see cref="ErrorSource" /> for more information.
		/// </summary>
		ErrorSource ErrorSource { get; }
		/// <summary>
		/// Returns the stored procedure name for which this class was built
		/// </summary>
		string StoredProcedureName { get; }
		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand).
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		int CommandTimeOut { get; set; }
		/// <summary>
		/// Returns the value returned back by the stored procedure execution.
		/// </summary>
		System.Data.SqlTypes.SqlInt32 Param_RETURN_VALUE { get; }
		/// <summary>
		/// This method allows you to reset the parameter object. Please note that this
		/// method resets all the parameters members except the connection information and
		/// the command time-out which are left in their current state.
		/// </summary>
		void Reset();
	}

#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG

	/// <summary>
	/// Stores synchronization information between the database schema and the generated code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class OlymarsInformationAttribute : System.Attribute {
	
		private string developerName;
		/// <summary>
		/// Name of the developer who has generated this code.
		/// </summary>
		public string DeveloperName {
		
			get {
			
				return(this.developerName);
			}

			set {

				this.developerName = value;
			}
		}
		
		private string generatedOn;
		/// <summary>
		/// Date and time when the developer has generated this code (format is: yyyy/mm/dd hh:mm:ss).
		/// </summary>
		public string GeneratedOn {
		
			get {
			
				return(this.generatedOn);
			}

			set {

				this.generatedOn = value;
			}
		}

		private string sqlObjectDependancyName;
		/// <summary>
		/// Name of the SQL object this code relies on.
		/// </summary>
		public string SqlObjectDependancyName {
		
			get {
			
				return(this.sqlObjectDependancyName);
			}

			set {

				this.sqlObjectDependancyName = value;
			}
		}
		
		private int sqlObjectDependancyRevision;
		/// <summary>
		/// Version of the SQL object this code relies on.
		/// </summary>
		public int SqlObjectDependancyRevision{
		
			get {
			
				return(this.sqlObjectDependancyRevision);
			}

			set {

				this.sqlObjectDependancyRevision = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the OlymarsInformationAttribute class.
		/// </summary>
		public OlymarsInformationAttribute() {

		}

		/// <summary>
		/// Initializes a new instance of the OlymarsInformationAttribute class.
		/// </summary>
		/// <param name="developerName">Name of the developer who has generated this code.</param>
		/// <param name="generatedOn">Date and time when the developer has generated this code (format is: yyyy/mm/dd hh:mm:ss).</param>
		/// <param name="sqlObjectDependancyName">Name of the SQL object this code relies on.</param>
		/// <param name="sqlObjectDependancyRevision">Version of the SQL object this code relies on.</param>
		public OlymarsInformationAttribute(string developerName, string generatedOn, string sqlObjectDependancyName, int sqlObjectDependancyRevision) : this() {

			this.developerName = developerName;
			this.generatedOn = generatedOn;
			this.sqlObjectDependancyName = sqlObjectDependancyName;
			this.sqlObjectDependancyRevision  = sqlObjectDependancyRevision;
		}

	}
	
#endif

	/// <summary>
	/// This class contains many useful functions like for example "BuildConnectionString" which is
	/// a helper for building valid Sql Server database connection strings.
	/// </summary>
	[Serializable()]
	public class Information {

		private static string ConnectionString = String.Empty;

		/// <summary>
		/// Returns the connection string that is stored in the configuration file:
		/// &lt;configuration&gt;
		/// &lt;appSettings&gt;
		/// &lt;add key="ConnectionString" value="Your connection string here" /&gt;
		/// &lt;/appSettings&gt;
		/// &lt;/configuration&gt;
		/// </summary>
		public static string GetConnectionStringFromConfigurationFile {

			get {

				if (ConnectionString.Length != 0) {

					return(ConnectionString);
				}

				object ConnectionStringObject = ConfigurationSettings.AppSettings["Bob ConnectionString"];

				if (ConnectionStringObject != null) {

					ConnectionString = (string)ConnectionStringObject;
					return(ConnectionString);
				}
				else {

					return(String.Empty);
				}
			}

		}

		/// <summary>
		/// Returns the connection string that is stored in the following registry location:
		/// Key:HKLM/SOFTWARE/ConnectionStrings/Bob, Entry:ConnectionString
		/// </summary>
		public static string GetConnectionStringFromRegistry {

			get {

				if (ConnectionString.Length != 0) {

					return(ConnectionString);
				}

				Microsoft.Win32.RegistryKey RK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ConnectionStrings\\Bob", false);
				if (RK != null) {

					ConnectionString = RK.GetValue("ConnectionString").ToString();
					RK.Close();
				}

				if (ConnectionString == null) {

					return(String.Empty);
				}
				else {

					return(ConnectionString);
				}
			}
		}

		/// <summary>
		/// Returns the System.Version of this assembly.
		/// </summary>
		static public System.Version Version {

			get {

				return(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
			}
		}

		/// <summary>
		/// Build an connection string using a SQL Server account. The connection time-out is set to 15 seconds. The connections pooling is not set.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="userID">SQL Server user account.</param>
		/// <param name="password">Password for the SQL Server account.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, string userID, string password) {

			return(internal_BuildConnectionString(server, database, false, userID, password, 15, false));
		}

		/// <summary>
		/// Build an connection string using a SQL Server account. The connection time-out is set to 15 seconds.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="userID">SQL Server user account.</param>
		/// <param name="password">Password for the SQL Server account.</param>
		/// <param name="pooling">True if connections pooling is desired.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, string userID, string password, bool pooling) {

			return(internal_BuildConnectionString(server, database, false, userID, password, 15, pooling));
		}

		/// <summary>
		/// Build an connection string using the current Windows logged on user (Integrated Security). The connection time-out is set to 15 seconds. The connections pooling is not set.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database) {

			return(internal_BuildConnectionString(server, database, true, String.Empty, String.Empty, 15, false));
		}

		/// <summary>
		/// Build an connection string using the current Windows logged on user (Integrated Security). The connection time-out is set to 15 seconds.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="pooling">True if connections pooling is desired.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, bool pooling) {

			return(internal_BuildConnectionString(server, database, true, String.Empty, String.Empty, 15, pooling));
		}

		/// <summary>
		/// Build an connection string using a SQL Server account. The connections pooling is not set.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="userID">SQL Server user account.</param>
		/// <param name="password">Password for the SQL Server account.</param>
		/// <param name="connectionTimeOut">The connection time-out in seconds.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, string userID, string password, short connectionTimeOut) {

			return(internal_BuildConnectionString(server, database, false, userID, password, connectionTimeOut, false));
		}

		/// <summary>
		/// Build an connection string using a SQL Server account.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="userID">SQL Server user account.</param>
		/// <param name="password">Password for the SQL Server account.</param>
		/// <param name="connectionTimeOut">The connection time-out in seconds.</param>
		/// <param name="pooling">True if connections pooling is desired.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, string userID, string password, short connectionTimeOut, bool pooling) {

			return(internal_BuildConnectionString(server, database, false, userID, password, connectionTimeOut, pooling));
		}

		/// <summary>
		/// Build an connection string using the current Windows logged on user (Integrated Security). The connections pooling is not set.
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="connectionTimeOut">The connection time-out in seconds.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, short connectionTimeOut) {

			return(internal_BuildConnectionString(server, database, true, String.Empty, String.Empty, connectionTimeOut, false));
		}

		/// <summary>
		/// Build an connection string using the current Windows logged on user (Integrated Security).
		/// </summary>
		/// <param name="server">SQL Server name (can be an named instance, i.e. SERVER\INSTANCE).</param>
		/// <param name="database">SQL Server Database name.</param>
		/// <param name="connectionTimeOut">The connection time-out in seconds.</param>
		/// <param name="pooling">True if connections pooling is desired.</param>
		/// <returns>The connection string.</returns>
		static public string BuildConnectionString(string server, string database, short connectionTimeOut, bool pooling) {

			return(internal_BuildConnectionString(server, database, true, String.Empty, String.Empty, connectionTimeOut, pooling));
		}

		private static string internal_BuildConnectionString(string Server, string Database, bool IntegratedSecurity, string UserID, string Password, short TimeOut, bool Pooling) {

			StringBuilder connectionString = new StringBuilder();

			connectionString.Append("Data Source=");
			connectionString.Append(Server);
			connectionString.Append(";");

			connectionString.Append("Initial Catalog=");
			connectionString.Append(Database);
			connectionString.Append(";");

			if (IntegratedSecurity) {

				connectionString.Append("Integrated Security=SSPI;");
			}
			else {

				connectionString.Append("Integrated Security=false;");

				connectionString.Append("User ID=");
				connectionString.Append(UserID);
				connectionString.Append(";");

				connectionString.Append("Password=");
				connectionString.Append(Password);
				connectionString.Append(";");
			}
			connectionString.Append("Pooling=");
			connectionString.Append(Pooling.ToString());
			connectionString.Append(";");

			connectionString.Append("Connect Timeout=");
			connectionString.Append(TimeOut.ToString());
			connectionString.Append(";");

			return(connectionString.ToString());
		}
	}
}
