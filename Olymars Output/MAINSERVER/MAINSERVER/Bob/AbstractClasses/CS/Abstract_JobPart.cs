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
	/// This class allows you to very easily retrieve a record from the [JobPart] table.
	/// </summary>
	[Serializable()]
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:11", SqlObjectDependancyName="JobPart", SqlObjectDependancyRevision=656)]
#endif
	public class Abstract_JobPart {

		Params.spS_JobPart Param;
		private bool CloseConnectionAfterUse = true;

		/// <summary>
		/// Create a new instance of the Abstract_JobPart class.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public Abstract_JobPart(string connectionString) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.Param = new Params.spS_JobPart(true);
			this.Param.SetUpConnection(connectionString);
		}

		/// <summary>
		/// Create a new instance of the Abstract_JobPart class.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection to the database.</param>
		public Abstract_JobPart(System.Data.SqlClient.SqlConnection sqlConnection) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_JobPart(true);
			this.Param.SetUpConnection(sqlConnection);
			CloseConnectionAfterUse = (this.Param.SqlConnection.State != System.Data.ConnectionState.Open);
		}

		/// <summary>
		/// Create a new instance of the Abstract_JobPart class.
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction to the database.</param>
		public Abstract_JobPart(System.Data.SqlClient.SqlTransaction sqlTransaction) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.Param = new Params.spS_JobPart(true);
			this.Param.SetUpConnection(sqlTransaction);
		}

		private System.Data.SqlTypes.SqlInt32 col_JobPartId;
		/// <summary>
		/// Returns the value of the JobPartId column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;JobPartId&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobPartId {

			get {

				return (this.col_JobPartId);
			}
		}

		private System.Data.SqlTypes.SqlInt32 col_JobId;
		/// <summary>
		/// Returns the value of the JobId column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;JobId&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobId {

			get {

				return (this.col_JobId);
			}
		}

		private System.Data.SqlTypes.SqlString col_Description;
		/// <summary>
		/// Returns the value of the Description column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Description&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Description {

			get {

				return (this.col_Description);
			}
		}

		private System.Data.SqlTypes.SqlInt32 col_JobPartTypeId;
		/// <summary>
		/// Returns the value of the JobPartTypeId column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;JobPartTypeId&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId {

			get {

				return (this.col_JobPartTypeId);
			}
		}

		private System.Data.SqlTypes.SqlDecimal col_Units;
		/// <summary>
		/// Returns the value of the Units column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Units&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlDecimal Col_Units {

			get {

				return (this.col_Units);
			}
		}

		private System.Data.SqlTypes.SqlMoney col_PricePerUnit;
		/// <summary>
		/// Returns the value of the PricePerUnit column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;PricePerUnit&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlMoney Col_PricePerUnit {

			get {

				return (this.col_PricePerUnit);
			}
		}

		private System.Data.SqlTypes.SqlMoney col_TotalPrice;
		/// <summary>
		/// Returns the value of the TotalPrice column.
		/// More info on this column: Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;TotalPrice&quot; column.
		/// </summary>
		public System.Data.SqlTypes.SqlMoney Col_TotalPrice {

			get {

				return (this.col_TotalPrice);
			}
		}

		/// <summary>
		/// This method allows to clear all the properties previously loaded by a call to the Refresh method.
		/// </summary>
		public void Reset() {

			this.col_JobPartId = System.Data.SqlTypes.SqlInt32.Null;
			this.col_JobId = System.Data.SqlTypes.SqlInt32.Null;
			this.col_Description = System.Data.SqlTypes.SqlString.Null;
			this.col_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
			this.col_Units = System.Data.SqlTypes.SqlDecimal.Null;
			this.col_PricePerUnit = System.Data.SqlTypes.SqlMoney.Null;
			this.col_TotalPrice = System.Data.SqlTypes.SqlMoney.Null;
		}

		/// <summary>
		/// Allows you to load a specific record of the [JobPart] table.
		/// </summary>
		/// <param name="col_JobPartId">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;JobPartId&quot; column.</param>
		public bool Refresh(System.Data.SqlTypes.SqlInt32 col_JobPartId) {

			bool Status;
			Reset();

			if (col_JobPartId.IsNull) {

				throw new ArgumentNullException("col_JobPartId" , "The primary key 'col_JobPartId' can not have a Null value!");
			}


			this.col_JobPartId = col_JobPartId;

			this.Param.Reset();

			this.Param.Param_JobPartId = this.col_JobPartId;

			System.Data.SqlClient.SqlDataReader DR;
			SPs.spS_JobPart SP = new SPs.spS_JobPart(false);

			if (SP.Execute(ref this.Param, out DR)) {

				Status = false;
				if (DR.Read()) {

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_JobId.ColumnIndex)) {

						this.col_JobId = DR.GetSqlInt32(SPs.spS_JobPart.Resultset1.Fields.Column_JobId.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_Description.ColumnIndex)) {

						this.col_Description = DR.GetSqlString(SPs.spS_JobPart.Resultset1.Fields.Column_Description.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_JobPartTypeId.ColumnIndex)) {

						this.col_JobPartTypeId = DR.GetSqlInt32(SPs.spS_JobPart.Resultset1.Fields.Column_JobPartTypeId.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_Units.ColumnIndex)) {

						this.col_Units = DR.GetSqlDecimal(SPs.spS_JobPart.Resultset1.Fields.Column_Units.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_PricePerUnit.ColumnIndex)) {

						this.col_PricePerUnit = DR.GetSqlMoney(SPs.spS_JobPart.Resultset1.Fields.Column_PricePerUnit.ColumnIndex);
					}

					if (!DR.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_TotalPrice.ColumnIndex)) {

						this.col_TotalPrice = DR.GetSqlMoney(SPs.spS_JobPart.Resultset1.Fields.Column_TotalPrice.ColumnIndex);
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

				throw new Bob.DataClasses.CustomException(this.Param, "Bob.AbstractClasses.Abstract_JobPart", "Refresh");
			}
		}
	}
}
