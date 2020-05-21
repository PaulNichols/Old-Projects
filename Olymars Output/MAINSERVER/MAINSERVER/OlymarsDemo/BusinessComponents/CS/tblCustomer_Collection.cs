/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:52:11
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:52:27
			Template revision: 1247

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

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
using System.Data.SqlClient;
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.BusinessComponents {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/12/28 11:52:11", SqlObjectDependancyName="tblCustomer", SqlObjectDependancyRevision=784)]
#endif
	public class tblCustomer_Collection : IBusinessComponentCollection, System.Collections.IEnumerable, System.ComponentModel.IListSource {


		private System.Collections.ArrayList internalRecords = null;
		private bool recordsAreLoaded = false;
		private IBusinessComponentRecord parent = null;
		internal string connectionString = String.Empty;
		internal SqlConnection sqlConnection = null;
		internal SqlTransaction sqlTransaction = null;
		private OlymarsDemo.DataClasses.ConnectionType lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
		private int insertCommandTimeOut = 30;
		private int deleteCommandTimeOut = 30;
		private int selectCollectionCommandTimeOut = 30;
		private int updateCommandTimeOut = 30;
		private int selectCommandTimeOut = 30;
		private System.Collections.ArrayList addedRecords = null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Collections.ArrayList AddedRecords {

			get {

				if (this.addedRecords == null) this.addedRecords = new System.Collections.ArrayList();
				return(this.addedRecords);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public IBusinessComponentRecord Parent {

			get {

				return(parent);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool ContainsListCollection {

			get {

				return(true);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public System.Collections.IList GetList() {

			return(internalRecords);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public int Count {

			get {

				if (!this.recordsAreLoaded) {

					Refresh();
				}

				return(internalRecords.Count);
			}
		}

		/// <summary>
		/// Returns the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		public System.String ConnectionString {

			get {

				return(this.connectionString);
			}
		}
		/// <summary>
		/// Returns the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection {

			get {

				return(this.sqlConnection);
			}
		}

		/// <summary>
		/// Returns the System.Data.SqlClient.SqlTransaction to be used
		/// against the SQL Server database.
		/// </summary>
		public System.Data.SqlClient.SqlTransaction SqlTransaction {

			get {

				return(this.sqlTransaction);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString, SqlConnection
		/// or SqlTransaction
		/// </summary>
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.lastKnownConnectionType );
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Insert operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int InsertCommandTimeOut {

			get {

				return(this.insertCommandTimeOut);
			}

			set {

				this.insertCommandTimeOut = value;
				if (this.insertCommandTimeOut <= 0) {

					this.insertCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Delete operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int DeleteCommandTimeOut {

			get {

				return(this.deleteCommandTimeOut);
			}

			set {

				this.deleteCommandTimeOut = value;
				if (this.deleteCommandTimeOut <= 0) {

					this.deleteCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Select operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int SelectCollectionCommandTimeOut {

			get {

				return(this.selectCollectionCommandTimeOut);
			}

			set {

				this.selectCollectionCommandTimeOut = value;
				if (this.selectCollectionCommandTimeOut <= 0) {

					this.selectCollectionCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Update operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int UpdateCommandTimeOut {

			get {

				return(this.updateCommandTimeOut);
			}

			set {

				this.updateCommandTimeOut = value;
				if (this.updateCommandTimeOut <= 0) {

					this.updateCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Select operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int SelectCommandTimeOut {

			get {

				return(this.selectCommandTimeOut);
			}

			set {

				this.selectCommandTimeOut = value;
				if (this.selectCommandTimeOut <= 0) {

					this.selectCommandTimeOut = 30;
				}
			}
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		public tblCustomer_Collection(string connectionString) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString != String.Empty) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblCustomer'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "tblCustomer", CurrentRevision, OriginalRevision));
					}
				}
			}
#endif

			this.connectionString = connectionString;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		public tblCustomer_Collection(SqlConnection sqlConnection) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlConnection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlConnection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblCustomer'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		public tblCustomer_Collection(SqlTransaction sqlTransaction) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlTransaction.Connection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlTransaction.Connection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlTransaction.Connection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblCustomer'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlTransaction;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public IBusinessComponentRecord Add(IBusinessComponentRecord record) {
		
			tblCustomer_Record recordToAdd = record as tblCustomer_Record;

			if (recordToAdd == null) {

				throw new ArgumentException("Invalid record type. Must be 'OlymarsDemo.BusinessComponents.tblCustomer_Record'.", "record");
			}

			bool alreadyOpened = false;

			Params.spI_tblCustomer Param = new Params.spI_tblCustomer(true);
			Param.CommandTimeOut = this.insertCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_Cus_LngID = recordToAdd.Col_Cus_LngID;
			Param.Param_Cus_StrLastName = recordToAdd.Customer_Surname;
			Param.Param_Cus_StrFirstName = recordToAdd.Col_Cus_StrFirstName;
			Param.Param_Cus_StrEmail = recordToAdd.Col_Cus_StrEmail;

			SPs.spI_tblCustomer Sp = new SPs.spI_tblCustomer(false);
			if (Sp.Execute(ref Param)) {

				tblCustomer_Record newRecord = null;

				switch (this.lastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
						newRecord = new tblCustomer_Record(this.connectionString, Param.Param_Cus_LngID);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
						newRecord = new tblCustomer_Record(this.sqlConnection, Param.Param_Cus_LngID);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
						newRecord = new tblCustomer_Record(this.sqlTransaction, Param.Param_Cus_LngID);
						break;
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				if (internalRecords != null) {

					internalRecords.Add(newRecord);
				}

				if (this.addedRecords == null) this.addedRecords = new System.Collections.ArrayList();
				this.addedRecords.Add(newRecord);

				return(newRecord);
			}
			else {

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblCustomer_Collection", "Add");
			}	
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="Col_Cus_LngID">[To be supplied.]</param>
		public void Remove(System.Data.SqlTypes.SqlInt32 Col_Cus_LngID) {

			bool alreadyOpened = false;

			Params.spD_tblCustomer Param = new Params.spD_tblCustomer(true);
			Param.CommandTimeOut = this.deleteCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_Cus_LngID = Col_Cus_LngID;

			SPs.spD_tblCustomer Sp = new SPs.spD_tblCustomer(true);

			Sp.Execute(ref Param);

			CloseConnection(Sp.Connection, alreadyOpened);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		public void Remove(IBusinessComponentRecord record) {
		
			tblCustomer_Record recordToDelete = record as tblCustomer_Record;

			if (recordToDelete == null) {

				throw new ArgumentException("Invalid record type. Must be 'OlymarsDemo.BusinessComponents.tblCustomer_Record'.", "record");
			}

			bool alreadyOpened = false;

			Params.spD_tblCustomer Param = new Params.spD_tblCustomer(true);
			Param.CommandTimeOut = this.deleteCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_Cus_LngID = recordToDelete.Col_Cus_LngID;

			SPs.spD_tblCustomer Sp = new SPs.spD_tblCustomer(true);

			Sp.Execute(ref Param);
			CloseConnection(Sp.Connection, alreadyOpened);

			if (internalRecords != null) {

				internalRecords.Remove(recordToDelete);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public void Refresh() {

			internalRecords = new System.Collections.ArrayList();

			bool alreadyOpened = false;

			Params.spS_tblCustomer_Display Param = new Params.spS_tblCustomer_Display(true);
			Param.CommandTimeOut = this.selectCollectionCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_tblCustomer_Display Sp = new SPs.spS_tblCustomer_Display(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				while (sqlDataReader.Read()) {

					tblCustomer_Record record = null;

					switch (this.lastKnownConnectionType) {

						case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							record = new tblCustomer_Record(this.connectionString, sqlDataReader.GetSqlInt32(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							record = new tblCustomer_Record(this.sqlConnection, sqlDataReader.GetSqlInt32(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
							record = new tblCustomer_Record(this.sqlTransaction, sqlDataReader.GetSqlInt32(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;
					}


					record.UpdateCommandTimeOut = this.updateCommandTimeOut;
					record.SelectCommandTimeOut = this.selectCommandTimeOut;

					if (sqlDataReader.GetFieldType(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

						record.displayName = sqlDataReader.GetString(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_Display.ColumnIndex);
					}
					else {

						record.displayName = sqlDataReader.GetValue(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
					}

					internalRecords.Add(record);
				}

				if (sqlDataReader != null && !sqlDataReader.IsClosed) {

					sqlDataReader.Close();
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				this.recordsAreLoaded = true;
			}
			else {

				if (sqlDataReader != null && !sqlDataReader.IsClosed) {

					sqlDataReader.Close();
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				this.recordsAreLoaded = false;
				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblCustomer_Collection", "Refresh");
			}
		}

		private void CloseConnection(SqlConnection connection, bool alreadyOpened) {

			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					if (connection != null && connection.State == System.Data.ConnectionState.Open) {

						connection.Close();
						connection.Dispose();
					}
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					if (!alreadyOpened && this.sqlConnection.State == System.Data.ConnectionState.Open) this.sqlConnection.Close();
					break;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public System.Collections.IEnumerator GetEnumerator() {

			if (!this.recordsAreLoaded) {

				Refresh();
			}

			return(internalRecords.GetEnumerator());
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="index">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public tblCustomer_Record this[int index] {

			get {
				
				return((tblCustomer_Record)internalRecords[index]);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_Cus_LngID">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public tblCustomer_Record this[System.Data.SqlTypes.SqlInt32 col_Cus_LngID] {

			get {

				foreach(tblCustomer_Record record in internalRecords) {

					bool equality = true;

					equality = equality && (record.Col_Cus_LngID == col_Cus_LngID).Value;

					if (equality) return(record);
				}
				return(null);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new tblCustomer_Collection MemberwiseClone() {

			tblCustomer_Collection newCollection = null;

			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					newCollection = new tblCustomer_Collection(this.connectionString);
					break;
					
				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					newCollection = new tblCustomer_Collection(this.sqlConnection);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					newCollection = new tblCustomer_Collection(this.sqlTransaction);
					break;
			}
	
			newCollection.recordsAreLoaded = this.recordsAreLoaded;
			newCollection.parent = null;

			newCollection.internalRecords = new System.Collections.ArrayList();
			foreach (tblCustomer_Record record in this.internalRecords) {

				newCollection.internalRecords.Add(record);
			}

			return(newCollection);
		}
	}
}
