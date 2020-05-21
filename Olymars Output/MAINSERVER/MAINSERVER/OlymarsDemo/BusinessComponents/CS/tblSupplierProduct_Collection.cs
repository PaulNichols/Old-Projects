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
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/12/28 11:52:11", SqlObjectDependancyName="tblSupplierProduct", SqlObjectDependancyRevision=576)]
#endif
	public class tblSupplierProduct_Collection : IBusinessComponentCollection, System.Collections.IEnumerable, System.ComponentModel.IListSource {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public enum tblSupplierProduct_CollectionParentType {

			/// <summary>
			/// [To be supplied.]
			/// </summary>
			None,
			/// <summary>
			/// [To be supplied.]
			/// </summary>
			Col_Spr_GuidProductID,
			/// <summary>
			/// [To be supplied.]
			/// </summary>
			Col_Spr_GuidSupplierID,
		}

		private System.Collections.ArrayList internalRecords = null;
		private bool recordsAreLoaded = false;
		private IBusinessComponentRecord parent = null;
		private tblSupplierProduct_CollectionParentType parentType = tblSupplierProduct_CollectionParentType.None;
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
		public tblSupplierProduct_CollectionParentType ParentType {

			get {

				return(this.parentType);
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

		private System.Data.SqlTypes.SqlGuid col_Spr_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Spr_GuidProductID {

			get {

				return(this.col_Spr_GuidProductID);
			}
			set {

				this.col_Spr_GuidProductID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_Spr_GuidProductID">[To be supplied.]</param>
		/// <param name="parent">[To be supplied.]</param>
		internal void LoadFrom_Spr_GuidProductID(System.Data.SqlTypes.SqlGuid col_Spr_GuidProductID, Product parent) {
		
			if (col_Spr_GuidProductID.IsNull) {

				throw new ArgumentException("Parameter can not be null", "col_Spr_GuidProductID");
			}

			if (parent == null) {

				throw new ArgumentException("Parameter can not be null", "parent");
			}

			this.col_Spr_GuidProductID = col_Spr_GuidProductID;
			this.parent = parent;
			this.parentType = tblSupplierProduct_CollectionParentType.Col_Spr_GuidProductID;
		}

		private System.Data.SqlTypes.SqlGuid col_Spr_GuidSupplierID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Spr_GuidSupplierID {

			get {

				return(this.col_Spr_GuidSupplierID);
			}
			set {

				this.col_Spr_GuidSupplierID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_Spr_GuidSupplierID">[To be supplied.]</param>
		/// <param name="parent">[To be supplied.]</param>
		internal void LoadFrom_Spr_GuidSupplierID(System.Data.SqlTypes.SqlGuid col_Spr_GuidSupplierID, Supplier parent) {
		
			if (col_Spr_GuidSupplierID.IsNull) {

				throw new ArgumentException("Parameter can not be null", "col_Spr_GuidSupplierID");
			}

			if (parent == null) {

				throw new ArgumentException("Parameter can not be null", "parent");
			}

			this.col_Spr_GuidSupplierID = col_Spr_GuidSupplierID;
			this.parent = parent;
			this.parentType = tblSupplierProduct_CollectionParentType.Col_Spr_GuidSupplierID;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		public tblSupplierProduct_Collection(string connectionString) {
			
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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplierProduct'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "tblSupplierProduct", CurrentRevision, OriginalRevision));
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
		public tblSupplierProduct_Collection(SqlConnection sqlConnection) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplierProduct'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblSupplierProduct", CurrentRevision, OriginalRevision, System.Environment.NewLine));
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
		public tblSupplierProduct_Collection(SqlTransaction sqlTransaction) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplierProduct'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblSupplierProduct", CurrentRevision, OriginalRevision, System.Environment.NewLine));
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
		
			tblSupplierProduct_Record recordToAdd = record as tblSupplierProduct_Record;

			if (recordToAdd == null) {

				throw new ArgumentException("Invalid record type. Must be 'OlymarsDemo.BusinessComponents.tblSupplierProduct_Record'.", "record");
			}

			switch (this.parentType) {

				case tblSupplierProduct_CollectionParentType.Col_Spr_GuidProductID:
					recordToAdd.Col_Spr_GuidProductID = ((Product)this.parent).Col_Pro_GuidID;
					break;

				case tblSupplierProduct_CollectionParentType.Col_Spr_GuidSupplierID:
					recordToAdd.Col_Spr_GuidSupplierID = ((Supplier)this.parent).Col_Sup_GuidID;
					break;

			}

			bool alreadyOpened = false;

			Params.spI_tblSupplierProduct Param = new Params.spI_tblSupplierProduct(true);
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

			Param.Param_Spr_GuidProductID = recordToAdd.Col_Spr_GuidProductID;
			Param.Param_Spr_GuidSupplierID = recordToAdd.Col_Spr_GuidSupplierID;

			SPs.spI_tblSupplierProduct Sp = new SPs.spI_tblSupplierProduct(false);
			if (Sp.Execute(ref Param)) {

				tblSupplierProduct_Record newRecord = null;

				switch (this.lastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
						newRecord = new tblSupplierProduct_Record(this.connectionString, Param.Param_Spr_GuidProductID, Param.Param_Spr_GuidSupplierID);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
						newRecord = new tblSupplierProduct_Record(this.sqlConnection, Param.Param_Spr_GuidProductID, Param.Param_Spr_GuidSupplierID);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
						newRecord = new tblSupplierProduct_Record(this.sqlTransaction, Param.Param_Spr_GuidProductID, Param.Param_Spr_GuidSupplierID);
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

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblSupplierProduct_Collection", "Add");
			}	
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="Col_Spr_GuidProductID">[To be supplied.]</param>
		/// <param name="Col_Spr_GuidSupplierID">[To be supplied.]</param>
		public void Remove(System.Data.SqlTypes.SqlGuid Col_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid Col_Spr_GuidSupplierID) {

			bool alreadyOpened = false;

			Params.spD_tblSupplierProduct Param = new Params.spD_tblSupplierProduct(true);
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

			Param.Param_Spr_GuidProductID = Col_Spr_GuidProductID;

			Param.Param_Spr_GuidSupplierID = Col_Spr_GuidSupplierID;

			SPs.spD_tblSupplierProduct Sp = new SPs.spD_tblSupplierProduct(true);

			Sp.Execute(ref Param);

			CloseConnection(Sp.Connection, alreadyOpened);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		public void Remove(IBusinessComponentRecord record) {
		
			tblSupplierProduct_Record recordToDelete = record as tblSupplierProduct_Record;

			if (recordToDelete == null) {

				throw new ArgumentException("Invalid record type. Must be 'OlymarsDemo.BusinessComponents.tblSupplierProduct_Record'.", "record");
			}

			bool alreadyOpened = false;

			Params.spD_tblSupplierProduct Param = new Params.spD_tblSupplierProduct(true);
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

			Param.Param_Spr_GuidProductID = recordToDelete.Col_Spr_GuidProductID;

			Param.Param_Spr_GuidSupplierID = recordToDelete.Col_Spr_GuidSupplierID;

			SPs.spD_tblSupplierProduct Sp = new SPs.spD_tblSupplierProduct(true);

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

			Params.spS_tblSupplierProduct_Display Param = new Params.spS_tblSupplierProduct_Display(true);
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

			if (!this.col_Spr_GuidProductID.IsNull) {

				Param.Param_Spr_GuidProductID = this.col_Spr_GuidProductID;
			}

			if (!this.col_Spr_GuidSupplierID.IsNull) {

				Param.Param_Spr_GuidSupplierID = this.col_Spr_GuidSupplierID;
			}

			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_tblSupplierProduct_Display Sp = new SPs.spS_tblSupplierProduct_Display(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				while (sqlDataReader.Read()) {

					tblSupplierProduct_Record record = null;

					switch (this.lastKnownConnectionType) {

						case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							record = new tblSupplierProduct_Record(this.connectionString, sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID2.ColumnIndex));
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							record = new tblSupplierProduct_Record(this.sqlConnection, sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID2.ColumnIndex));
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
							record = new tblSupplierProduct_Record(this.sqlTransaction, sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetSqlGuid(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_ID2.ColumnIndex));
							break;
					}


					record.UpdateCommandTimeOut = this.updateCommandTimeOut;
					record.SelectCommandTimeOut = this.selectCommandTimeOut;

					if (sqlDataReader.GetFieldType(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

						record.displayName = sqlDataReader.GetString(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_Display.ColumnIndex);
					}
					else {

						record.displayName = sqlDataReader.GetValue(SPs.spS_tblSupplierProduct_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
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
				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblSupplierProduct_Collection", "Refresh");
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
		public tblSupplierProduct_Record this[int index] {

			get {
				
				return((tblSupplierProduct_Record)internalRecords[index]);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_Spr_GuidProductID">[To be supplied.]</param>
		/// <param name="col_Spr_GuidSupplierID">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public tblSupplierProduct_Record this[System.Data.SqlTypes.SqlGuid col_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid col_Spr_GuidSupplierID] {

			get {

				foreach(tblSupplierProduct_Record record in internalRecords) {

					bool equality = true;

					equality = equality && (record.Col_Spr_GuidProductID == col_Spr_GuidProductID).Value;
					equality = equality && (record.Col_Spr_GuidSupplierID == col_Spr_GuidSupplierID).Value;

					if (equality) return(record);
				}
				return(null);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new tblSupplierProduct_Collection MemberwiseClone() {

			tblSupplierProduct_Collection newCollection = null;

			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					newCollection = new tblSupplierProduct_Collection(this.connectionString);
					break;
					
				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					newCollection = new tblSupplierProduct_Collection(this.sqlConnection);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					newCollection = new tblSupplierProduct_Collection(this.sqlTransaction);
					break;
			}
	
			newCollection.recordsAreLoaded = this.recordsAreLoaded;
			newCollection.parent = null;

			newCollection.internalRecords = new System.Collections.ArrayList();
			foreach (tblSupplierProduct_Record record in this.internalRecords) {

				newCollection.internalRecords.Add(record);
			}

			return(newCollection);
		}
	}
}
