/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:13
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
using SPs = Bob.DataClasses.StoredProcedures;
using Params = Bob.DataClasses.Parameters;

namespace Bob.Windows.CheckedListBoxes {

	/// <summary>
	/// This class is based on a System.Windows.Forms.CheckedListBox class and was built to display
	/// the resultset returned back by the
	/// 'spS_Title' stored procedure.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:13", SqlObjectDependancyName="spS_Title", SqlObjectDependancyRevision=128)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBoxCustom_spS_Title), "WinCheckedListBox.bmp")]
	public class WinCheckedListBoxCustom_spS_Title : System.Windows.Forms.CheckedListBox {

		private string valueMember;
		private string displayMember;
		private string tableName;
		private Params.spS_Title param;
		private System.Data.SqlTypes.SqlInt32 param_TitleId = System.Data.SqlTypes.SqlInt32.Null;
		private bool bindingInProgress = false;
		private bool internalItemCheckStateUpdateInProgress = false;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private Bob.DataClasses.ConnectionType LastKnownConnectionType = Bob.DataClasses.ConnectionType.None;

		/// <summary>
		/// Returns True if binding operation is in progress.
		/// </summary>
		public bool BindingInProgress {

			get {

				return(this.bindingInProgress);
			}
		}

		/// <summary>
		/// Returns True if an internal item CheckState update operation is in progress.
		/// </summary>
		public bool InternalItemCheckStateUpdateInProgress {

			get {

				return(this.internalItemCheckStateUpdateInProgress);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going
		/// or has been used against the Sql Server database.
		/// </summary>
		public Bob.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.LastKnownConnectionType != Bob.DataClasses.ConnectionType.ConnectionString) {

					throw new InvalidOperationException("The connection string was not set in the class constructor.");
				}

				return(this.connectionString);
			}
		}

		/// <summary>
		/// Returns the SqlConnection used to access the Sql Server database.
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection {

			get {

				if (this.LastKnownConnectionType != Bob.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the Parameter class that was used to populate this control.
		/// </summary>
		public Params.spS_Title SP_Parameter {

			get {

				return(this.param);
			}
		}

		private int commandTimeOut = 30;
		/// <summary>
		/// Gets or sets the command timeout for the underlying stored procedure execution (default is 30 seconds)
		/// </summary>
		public int CommandTimeOut {

			get {

				return(this.commandTimeOut);
			}
			set {

				this.commandTimeOut = value;
			}
		}

		/// <summary>
		/// Returns the currently selected record primary key.
		/// </summary>
		public object SelectedRecordID {

			get {

				if (base.SelectedIndex == -1) {

					return(null);
				}

				return(((System.Data.DataRowView)this.SelectedItem).Row[this.valueMember]);
			}
		}

		/// <summary>
		/// Returns the currently checked records primary keys.
		/// </summary>
		public System.Collections.ArrayList CheckedRecordsID {

			get {
						
				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(object currentItem in this.CheckedItems) {
				
					arrayList.Add(((System.Data.DataRowView)currentItem).Row[this.valueMember]);
				}
				
				return(arrayList);
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		/// <param name="disposing">
		/// true to release both managed and unmanaged resources;
		/// false to release only unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing) {

			if (disposing) {

				this.param_TitleId = System.Data.SqlTypes.SqlInt32.Null;
				if (this.param != null) {

					this.param.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the WinCheckedListBoxCustom_spS_Title class.
		/// </summary>
		public WinCheckedListBoxCustom_spS_Title() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the 'spS_Title' stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="valueMember">name of the field to be used as a primary key.</param>
		/// <param name="displayMember">name of the field to be used for the content display.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_TitleId">Value for the parameter @TitleId.</param>
		public void Initialize(string connectionString, string valueMember, string displayMember, string tableName, System.Data.SqlTypes.SqlInt32 param_TitleId) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_Title'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_Title", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.valueMember = valueMember;
			this.displayMember = displayMember;
			this.tableName = tableName;

			this.param_TitleId = param_TitleId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the 'spS_Title' stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="valueMember">name of the field to be used as a primary key.</param>
		/// <param name="displayMember">name of the field to be used for the content display.</param>
		/// <param name="param_TitleId">Value for the parameter @TitleId.</param>
		public void Initialize(string connectionString, string valueMember, string displayMember, System.Data.SqlTypes.SqlInt32 param_TitleId) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_Title'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_Title", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.valueMember = valueMember;
			this.displayMember = displayMember;
			this.tableName = "spS_Title";

			this.param_TitleId = param_TitleId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the 'spS_Title' stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="valueMember">name of the field to be used as a primary key.</param>
		/// <param name="displayMember">name of the field to be used for the content display.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_TitleId">Value for the parameter @TitleId.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, string valueMember, string displayMember, string tableName, System.Data.SqlTypes.SqlInt32 param_TitleId) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_Title'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_Title", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.valueMember = valueMember;
			this.displayMember = displayMember;
			this.tableName = tableName;

			this.param_TitleId = param_TitleId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the 'spS_Title' stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="valueMember">name of the field to be used as a primary key.</param>
		/// <param name="displayMember">name of the field to be used for the content display.</param>
		/// <param name="param_TitleId">Value for the parameter @TitleId.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, string valueMember, string displayMember, System.Data.SqlTypes.SqlInt32 param_TitleId) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_Title'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_Title", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.valueMember = valueMember;
			this.displayMember = displayMember;
			this.tableName = "spS_Title";

			this.param_TitleId = param_TitleId;
		}

		/// <summary>
		/// Load or reloads the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new object[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new object[0], startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PrimaryKeys) {

			this.RefreshData(ArrayOf_PrimaryKeys, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the chosen resultset returned by the stored procedure.
		/// You can specify which record you want to be checked by default.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PrimaryKeys, int startRecord, int maxRecords) {

			if (ArrayOf_PrimaryKeys != null && ArrayOf_PrimaryKeys.Count > 0) {

				object[] typedArray = new object[ArrayOf_PrimaryKeys.Count];
				int index = 0;
				foreach(object item in ArrayOf_PrimaryKeys) {

					typedArray[index] = item;
					index++;
				}
				this.RefreshData(typedArray, startRecord, maxRecords);
			}
			else {

				this.RefreshData(startRecord, maxRecords);
			}
		}

		/// <summary>
		/// Load or reloads the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(object[] ArrayOf_PrimaryKeys) {

			this.RefreshData(ArrayOf_PrimaryKeys, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the chosen resultset returned by the stored procedure.
		/// You can specify which record you want to be checked by default.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(object[] ArrayOf_PrimaryKeys, int startRecord, int maxRecords) {

			this.CreateControl();

			if (this.LastKnownConnectionType == Bob.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}

			this.param = new Params.spS_Title();

			switch (this.LastKnownConnectionType) {

 				case Bob.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case Bob.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;
			}

			this.param.CommandTimeOut = this.commandTimeOut;


			if (!this.param_TitleId.IsNull) {

				this.param.Param_TitleId = this.param_TitleId;
			}


			System.Data.DataSet DS = null;
			
			SPs.spS_Title SP = new SPs.spS_Title();
			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.BeginUpdate();
				this.bindingInProgress = true;
				this.DataSource = DS.Tables[this.tableName].DefaultView;
				this.ValueMember = this.valueMember;
				this.DisplayMember = this.displayMember;
				this.bindingInProgress = false;

				if (ArrayOf_PrimaryKeys != null && ArrayOf_PrimaryKeys.Length > 0) {

					this.SetRecordsCheckState(ArrayOf_PrimaryKeys, System.Windows.Forms.CheckState.Checked);
				}
				else {

					base.OnSelectedIndexChanged(EventArgs.Empty);
				}

				this.EndUpdate();
				SP.Dispose();
			}

			else {

				SP.Dispose();
				throw new Bob.DataClasses.CustomException(this.param, "WinCheckedListBoxCustom_spS_Title", "RefreshData");
			}

		}

		/// <summary>
		/// Sets all records to a given System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetAllRecordsCheckState(System.Windows.Forms.CheckState value) {

			for (int index=0; index < this.Items.Count; index++) {
			
				this.SetItemCheckState(index, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Collections.ArrayList ArrayOf_PrimaryKeys, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PrimaryKeys != null && ArrayOf_PrimaryKeys.Count > 0) {

				object[] typedArray = new object[ArrayOf_PrimaryKeys.Count];
				int index = 0;
				foreach(object item in ArrayOf_PrimaryKeys) {

					typedArray[index] = item;
					index++;
				}
				this.SetRecordsCheckState(typedArray, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PrimaryKeys">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(object[] ArrayOf_PrimaryKeys, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PrimaryKeys != null && ArrayOf_PrimaryKeys.Length > 0) {

				this.internalItemCheckStateUpdateInProgress = true;

				foreach (object PrimaryKey in ArrayOf_PrimaryKeys) {

					for (int index = 0; index < this.Items.Count; index++) {

						object currentPrimaryKey = ((System.Data.DataRowView)this.Items[index]).Row[this.valueMember];
						if (currentPrimaryKey.Equals(PrimaryKey)) {
						
							this.SetItemCheckState(index, value);
							break;
						}
					}
				}

				this.internalItemCheckStateUpdateInProgress = false;
			}
		}
	}
}
