/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:11
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
using Bob.DataClasses;
using Params = Bob.DataClasses.Parameters;
using SPs = Bob.DataClasses.StoredProcedures;

namespace Bob.AbstractClasses {

	/// <summary>
	/// This class allows you to very easily retrieve a record from the [Customers] table.
	/// </summary>
	[Serializable()]
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:11", SqlObjectDependancyName="Customers", SqlObjectDependancyRevision=960)]
#endif
	public class Abstract_Customers {

		Params.spS_Customers Param;
		private bool CloseConnectionAfterUse = true;

		/// <summary>
		/// Create a new instance of the Abstract_Customers class.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public Abstract_Customers(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.Param = new Params.spS_Customers(true);
			this.Param.SetUpConnection(connectionString);
		}

		/// <summary>
		/// Create a new instance of the Abstract_Customers class.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection to the database.</param>
		public Abstract_Customers(System.Data.SqlClient.SqlConnection sqlConnection) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_Customers(true);
			this.Param.SetUpConnection(sqlConnection);
			CloseConnectionAfterUse = (this.Param.SqlConnection.State != System.Data.ConnectionState.Open);
		}

		/// <summary>
		/// Create a new instance of the Abstract_Customers class.
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction to the database.</param>
		public Abstract_Customers(System.Data.SqlClient.SqlTransaction sqlTransaction) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_Customers(true);
			this.Param.SetUpConnection(sqlTransaction);
		}

		private System.Data.SqlTypes.SqlInt32 col_CustomerID;
		/// <summary>
		/// Returns the value of the CustomerID column.
		/// More info on this column: Customer unique identifier
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_CustomerID {

			get {

				return (this.col_CustomerID);
			}
		}

		private System.Data.SqlTypes.SqlString col_CompanyName;
		/// <summary>
		/// Returns the value of the CompanyName column.
		/// More info on this column: Company for which the contact represents
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_CompanyName {

			get {

				return (this.col_CompanyName);
			}
		}

		private System.Data.SqlTypes.SqlString col_ContactName;
		/// <summary>
		/// Returns the value of the ContactName column.
		/// More info on this column: Customer or Contact Name (if a business) 
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_ContactName {

			get {

				return (this.col_ContactName);
			}
		}

		private System.Data.SqlTypes.SqlInt32 col_TitleId;
		/// <summary>
		/// Returns the value of the TitleId column.
		/// More info on this column: Foriegn Key for Contact Title
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_TitleId {

			get {

				return (this.col_TitleId);
			}
		}

		private System.Data.SqlTypes.SqlString col_Address;
		/// <summary>
		/// Returns the value of the Address column.
		/// More info on this column: Customer/Contact Address
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Address {

			get {

				return (this.col_Address);
			}
		}

		private System.Data.SqlTypes.SqlString col_City;
		/// <summary>
		/// Returns the value of the City column.
		/// More info on this column: Customer/Contact City
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_City {

			get {

				return (this.col_City);
			}
		}

		private System.Data.SqlTypes.SqlString col_PostalCode;
		/// <summary>
		/// Returns the value of the PostalCode column.
		/// More info on this column: Customer/Contact Post code
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_PostalCode {

			get {

				return (this.col_PostalCode);
			}
		}

		private System.Data.SqlTypes.SqlString col_Phone;
		/// <summary>
		/// Returns the value of the Phone column.
		/// More info on this column: Customer/Contact Phone
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Phone {

			get {

				return (this.col_Phone);
			}
		}

		private System.Data.SqlTypes.SqlString col_Email;
		/// <summary>
		/// Returns the value of the Email column.
		/// More info on this column: Email Address
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Email {

			get {

				return (this.col_Email);
			}
		}

		private System.Data.SqlTypes.SqlString col_WebAddress;
		/// <summary>
		/// Returns the value of the WebAddress column.
		/// More info on this column: Web Address URL
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_WebAddress {

			get {

				return (this.col_WebAddress);
			}
		}

		private System.Data.SqlTypes.SqlString col_Fax;
		/// <summary>
		/// Returns the value of the Fax column.
		/// More info on this column: Customer/Contact Fax
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Fax {

			get {

				return (this.col_Fax);
			}
		}

		private System.Data.SqlTypes.SqlBoolean col_Active;
		/// <summary>
		/// Returns the value of the Active column.
		/// More info on this column: Active flag
		/// </summary>
		public System.Data.SqlTypes.SqlBoolean Col_Active {

			get {

				return (this.col_Active);
			}
		}

		/// <summary>
		/// This method allows to clear all the properties previously loaded by a call to the Refresh method.
		/// </summary>
		public void Reset() {

			this.col_CustomerID = System.Data.SqlTypes.SqlInt32.Null;
			this.col_CompanyName = System.Data.SqlTypes.SqlString.Null;
			this.col_ContactName = System.Data.SqlTypes.SqlString.Null;
			this.col_TitleId = System.Data.SqlTypes.SqlInt32.Null;
			this.col_Address = System.Data.SqlTypes.SqlString.Null;
			this.col_City = System.Data.SqlTypes.SqlString.Null;
			this.col_PostalCode = System.Data.SqlTypes.SqlString.Null;
			this.col_Phone = System.Data.SqlTypes.SqlString.Null;
			this.col_Email = System.Data.SqlTypes.SqlString.Null;
			this.col_WebAddress = System.Data.SqlTypes.SqlString.Null;
			this.col_Fax = System.Data.SqlTypes.SqlString.Null;
			this.col_Active = System.Data.SqlTypes.SqlBoolean.Null;
		}

		/// <summary>
		/// Allows you to load a specific record of the [Customers] table.
		/// </summary>
		/// <param name="col_CustomerID">Customer unique identifier</param>
		public bool Refresh(System.Data.SqlTypes.SqlInt32 col_CustomerID) {

			bool Status;
			Reset();

			if (col_CustomerID.IsNull) {

				throw new ArgumentNullException("col_CustomerID" , "The primary key 'col_CustomerID' can not have a Null value!");
			}


			this.col_CustomerID = col_CustomerID;

			this.Param.Reset();

			this.Param.Param_CustomerID = this.col_CustomerID;

			System.Data.SqlClient.SqlDataReader DR;
			SPs.spS_Customers SP = new SPs.spS_Customers(false);

			if (SP.Execute(ref this.Param, out DR)) {

				Status = false;
				if (DR.Read()) {

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_CompanyName.ColumnIndex)) {

						this.col_CompanyName = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_CompanyName.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_ContactName.ColumnIndex)) {

						this.col_ContactName = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_ContactName.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_TitleId.ColumnIndex)) {

						this.col_TitleId = DR.GetSqlInt32(SPs.spS_Customers.Resultset1.Fields.Column_TitleId.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Address.ColumnIndex)) {

						this.col_Address = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Address.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_City.ColumnIndex)) {

						this.col_City = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_City.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_PostalCode.ColumnIndex)) {

						this.col_PostalCode = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_PostalCode.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Phone.ColumnIndex)) {

						this.col_Phone = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Phone.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Email.ColumnIndex)) {

						this.col_Email = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Email.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_WebAddress.ColumnIndex)) {

						this.col_WebAddress = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_WebAddress.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Fax.ColumnIndex)) {

						this.col_Fax = DR.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Fax.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Active.ColumnIndex)) {

						this.col_Active = DR.GetSqlBoolean(SPs.spS_Customers.Resultset1.Fields.Column_Active.ColumnIndex);
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

				throw new Bob.DataClasses.CustomException(this.Param, "Bob.AbstractClasses.Abstract_Customers", "Refresh");
			}
		}
	}
}
