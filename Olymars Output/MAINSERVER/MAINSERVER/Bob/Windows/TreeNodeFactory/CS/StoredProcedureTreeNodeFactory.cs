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
	public enum StoredProcedureList {

		/// <summary>No stored procedure is currently available for this enumeration.</summary>
		NoStoredProcedureAvailable
	}

	/// <summary>
	/// Provides a base class for all the events.
	/// </summary>
	public class StoredProcedureFactoryBaseEventArgs : System.EventArgs {

		private StoredProcedureCustomTreeNode node = null;
		/// <summary>
		/// Gets the node beiing concerned by the event.
		/// </summary>
		public StoredProcedureCustomTreeNode Node {

			get {

				return(this.node);
			}
		}

		private StoredProcedureList caller = StoredProcedureList.NoStoredProcedureAvailable;
		/// <summary>
		/// Gets the method call that has generated the event.
		/// </summary>
		public StoredProcedureList Caller {

			get {

				return(this.caller);
			}
		}

		internal StoredProcedureFactoryBaseEventArgs(StoredProcedureCustomTreeNode node, StoredProcedureList caller) : base() {

			this.node = node;
			this.caller = caller;
		}
	}

	/// <summary>
	/// Represents the method that will handle the BeforeInsert event of StoredProcedureTreeNodeFactory
	/// </summary>
	public delegate void BeforeInsertHandler(object sender, StoredProcedureFactoryBeforeInsertEventArgs e);

	/// <summary>
	/// Provides data for the BeforeInsert event.
	/// </summary>
	public class StoredProcedureFactoryBeforeInsertEventArgs : StoredProcedureFactoryBaseEventArgs {

		private bool cancel = false;
		/// <summary>
		/// Gets or sets a value indicating whether the event should be canceled.
		/// </summary>
		public bool Cancel {

			get {

				return(this.cancel);
			}
			set {

				this.cancel = value;
			}
		}

		internal StoredProcedureFactoryBeforeInsertEventArgs (StoredProcedureCustomTreeNode node, StoredProcedureList caller) : base(node, caller) {

		}
	}

	/// <summary>
	/// Represents the method that will handle the AfterInsert event of StoredProcedureTreeNodeFactory
	/// </summary>
	public delegate void AfterInsertHandler(object sender, StoredProcedureFactoryAfterInsertEventArgs e);

	/// <summary>
	/// Provides data for the AfterInsert event.
	/// </summary>
	public class StoredProcedureFactoryAfterInsertEventArgs : StoredProcedureFactoryBaseEventArgs {

		internal StoredProcedureFactoryAfterInsertEventArgs(StoredProcedureCustomTreeNode node, StoredProcedureList caller) : base(node, caller) {

		}
	}

	/// <summary>
	/// Represents the method that will handle the FormatIdDisplay event of StoredProcedureTreeNodeFactory
	/// </summary>
	public delegate void FormatIdDisplayHandler(object sender, StoredProcedureFactoryFormatIdDisplayEventArgs e);

	/// <summary>
	/// Provides data for the FormatIdDisplay event.
	/// </summary>
	public class StoredProcedureFactoryFormatIdDisplayEventArgs : StoredProcedureFactoryBaseEventArgs {

		private bool idReadOnly = true;
		private object id = null;
		/// <summary>
		/// Gets or sets the value to use for the node id.
		/// </summary>
		public object Id {

			get {

				return(this.id);
			}
			set {

				if (idReadOnly) throw new InvalidOperationException("'Id' can not be changed because it is already bound on a column");
				this.id = value;
			}
		}

		private bool displayReadOnly = true;
		private string display = string.Empty;
		/// <summary>
		/// Gets or sets the value to use for the node display.
		/// </summary>
		public string Display {

			get {

				return(this.display);
			}
			set {

				if (displayReadOnly) throw new InvalidOperationException("'Display' can not be changed because it is already bound on a column");
				this.display = value;
			}
		}

		internal StoredProcedureFactoryFormatIdDisplayEventArgs(StoredProcedureCustomTreeNode node, StoredProcedureList caller, int valueMemberIndex, int displayMemberIndex) : base(node, caller) {

			idReadOnly = (valueMemberIndex != -1);
			displayReadOnly = (displayMemberIndex != -1);
		}
	}

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode factory built for the Bob stored procedures.
	/// </summary>
	public class StoredProcedureTreeNodeFactory {

		internal static string _connectionString = String.Empty;
		internal static System.Data.SqlClient.SqlConnection _sqlConnection = null;
		internal static ConnectionType lastKnownConnectionType = ConnectionType.None;

		/// <summary>
		/// Occurs just before the node is actually inserted.
		/// </summary>
		public static event BeforeInsertHandler BeforeInsert;

		/// <summary>
		/// Raises the BeforeInsert event.
		/// </summary>
		protected static void OnBeforeInsert(StoredProcedureFactoryBeforeInsertEventArgs e) {

			if (BeforeInsert != null) {

				BeforeInsert(null, e);
			}
		}

		/// <summary>
		/// Occurs just after the node has actually been inserted.
		/// </summary>
		public static event AfterInsertHandler AfterInsert;

		/// <summary>
		/// Raises the AfterInsert event.
		/// </summary>
		protected static void OnAfterInsert(StoredProcedureFactoryAfterInsertEventArgs e) {

			if (AfterInsert != null) {

				AfterInsert(null, e);
			}
		}

		/// <summary>
		/// Occurs when the display member of the node is needed.
		/// </summary>
		public static event FormatIdDisplayHandler FormatIdDisplay;

		/// <summary>
		/// Raises the FormatIdDisplay event.
		/// </summary>
		protected static void OnFormatIdDisplay(StoredProcedureFactoryFormatIdDisplayEventArgs e) {

			if (FormatIdDisplay != null) {

				FormatIdDisplay(null, e);
			}
		}

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
