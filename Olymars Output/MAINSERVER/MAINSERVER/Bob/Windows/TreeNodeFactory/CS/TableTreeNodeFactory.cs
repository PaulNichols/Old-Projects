/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:24
			Generator name: MAINSERVER\Administrator
			Template last update: 15/01/2005 18:37:20
			Template revision: 417

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
using Bob.DataClasses;
using Params=Bob.DataClasses.Parameters;
using SPs=Bob.DataClasses.StoredProcedures;

namespace Bob.Windows.TreeNodeFactory {

	/// <summary>
	/// Bob database tables enumeration
	/// </summary>
	public enum TableList {

		/// <summary>No table is currently available for this enumeration.</summary>
		NoTableAvailable
	}

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode factory built for the Bob tables.
	/// </summary>
	public class TableTreeNodeFactory {

		internal static string _connectionString = String.Empty;
		internal static System.Data.SqlClient.SqlConnection _sqlConnection = null;
		internal static ConnectionType lastKnownConnectionType = ConnectionType.None;

		/// <summary>
		/// Sets the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public static void SetUpConnection(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			_connectionString = connectionString;
			lastKnownConnectionType = ConnectionType.ConnectionString;
		}

		/// <summary>
		/// Sets the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		public static void SetUpConnection(System.Data.SqlClient.SqlConnection sqlConnection) {

			if (sqlConnection == null) {

				throw new ArgumentNullException("sqlConnection", "sqlConnection can not be null.");
			}

			_sqlConnection = sqlConnection;
			lastKnownConnectionType = ConnectionType.SqlConnection;
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString, SqlConnection
		/// </summary>
		public static ConnectionType ConnectionType {

			get {

				return(lastKnownConnectionType );
			}
		}
	}
}
