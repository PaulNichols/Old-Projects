/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1287.20792

			Generation Date: 28/11/2004 15:05:21
			Generator name: <Developer Name Here>
			Template last update: 24/06/2002 01:53:57
			Template revision: 15083637

			SQL Server version: 08.00.0194
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.
*/

using System;
using OlymarsDemo.DataClasses;
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.AbstractClasses {

	/// <summary>
	/// This class allows you to very easily retrieve a record from the [tblOrder] table.
	/// </summary>
	[Serializable()]
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:21", SqlObjectDependancyName="tblOrder", SqlObjectDependancyRevision=800)]
#endif
	public sealed class Abstract_tblOrder {

		Params.spS_tblOrder Param;
		private bool CloseConnectionAfterUse = true;

		/// <summary>
		/// Create a new instance of the Abstract_tblOrder class.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public Abstract_tblOrder(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrder'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrder", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.Param = new Params.spS_tblOrder(true);
			this.Param.SetUpConnection(connectionString);
		}

		/// <summary>
		/// Create a new instance of the Abstract_tblOrder class.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection to the database.</param>
		public Abstract_tblOrder(System.Data.SqlClient.SqlConnection sqlConnection) {

			if (sqlConnection == null) {

				throw new ArgumentNullException("sqlConnection", "Invalid connection!");
			}

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrder'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrder", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_tblOrder(true);
			this.Param.SetUpConnection(sqlConnection);
			CloseConnectionAfterUse = (this.Param.SqlConnection.State != System.Data.ConnectionState.Open);
		}

		/// <summary>
		/// Create a new instance of the Abstract_tblOrder class.
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction to the database.</param>
		public Abstract_tblOrder(System.Data.SqlClient.SqlTransaction sqlTransaction) {

			if (sqlTransaction == null || sqlTransaction.Connection == null) {
				throw new ArgumentNullException("sqlTransaction", "Invalid transaction!");
			}

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrder'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrder", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_tblOrder(true);
			this.Param.SetUpConnection(sqlTransaction);
		}

		private System.Data.SqlTypes.SqlGuid col_Ord_GuidID;
		/// <summary>
		/// Returns the value of the Ord_GuidID column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Ord_GuidID {

			get {

				return (this.col_Ord_GuidID);
			}
		}

		private System.Data.SqlTypes.SqlDateTime col_Ord_DatOrderedOn;
		/// <summary>
		/// Returns the value of the Ord_DatOrderedOn column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_DatOrderedOn&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlDateTime Col_Ord_DatOrderedOn {

			get {

				return (this.col_Ord_DatOrderedOn);
			}
		}

		private System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID;
		/// <summary>
		/// Returns the value of the Ord_LngCustomerID column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_Ord_LngCustomerID {

			get {

				return (this.col_Ord_LngCustomerID);
			}
		}

		private System.Data.SqlTypes.SqlMoney col_Ord_CurTotal;
		/// <summary>
		/// Returns the value of the Ord_CurTotal column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_CurTotal&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlMoney Col_Ord_CurTotal {

			get {

				return (this.col_Ord_CurTotal);
			}
		}

		/// <summary>
		/// This method allows to clear all the properties previously loaded by a call to the Refresh method.
		/// </summary>
		public void Reset() {

			this.col_Ord_GuidID = System.Data.SqlTypes.SqlGuid.Null;
			this.col_Ord_DatOrderedOn = System.Data.SqlTypes.SqlDateTime.Null;
			this.col_Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
			this.col_Ord_CurTotal = System.Data.SqlTypes.SqlMoney.Null;
		}

		/// <summary>
		/// Allows you to load a specific record of the [tblOrder] table.
		/// </summary>
		/// <param name="col_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		public bool Refresh(System.Data.SqlTypes.SqlGuid col_Ord_GuidID) {

			bool Status;
			Reset();

			if (col_Ord_GuidID.IsNull) {

				throw new ArgumentNullException("col_Ord_GuidID" , "The primary key 'col_Ord_GuidID' can not have a Null value!");
			}


			this.col_Ord_GuidID = col_Ord_GuidID;

			this.Param.Reset();

			this.Param.Param_Ord_GuidID = this.col_Ord_GuidID;

			System.Data.SqlClient.SqlDataReader DR;
			SPs.spS_tblOrder SP = new SPs.spS_tblOrder(false);

			if (SP.Execute(ref this.Param, out DR)) {

				Status = false;
				if (DR.Read()) {

					if (!DR.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_DatOrderedOn.ColumnIndex)) {

						this.col_Ord_DatOrderedOn = DR.GetSqlDateTime(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_DatOrderedOn.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_LngCustomerID.ColumnIndex)) {

						this.col_Ord_LngCustomerID = DR.GetSqlInt32(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_LngCustomerID.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_CurTotal.ColumnIndex)) {

						this.col_Ord_CurTotal = DR.GetSqlMoney(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_CurTotal.ColumnIndex);
					}

					Status = true;
				}

				if (DR != null && !DR.IsClosed) {

					DR.Close();
				}

				if (CloseConnectionAfterUse && SP.Connection != null && SP.Connection.State == System.Data.ConnectionState.Open) {

					SP.Connection.Close();
					SP.Connection.Dispose();
				}

				return(Status);
			}

			else {

				if (DR != null && !DR.IsClosed) {

					DR.Close();
				}

				if (CloseConnectionAfterUse && SP.Connection != null && SP.Connection.State == System.Data.ConnectionState.Open) {

					SP.Connection.Close();
					SP.Connection.Dispose();
				}

				throw new OlymarsDemo.DataClasses.CustomException(this.Param, "OlymarsDemo.AbstractClasses.Abstract_tblOrder", "Refresh");
			}
		}
	}
}
