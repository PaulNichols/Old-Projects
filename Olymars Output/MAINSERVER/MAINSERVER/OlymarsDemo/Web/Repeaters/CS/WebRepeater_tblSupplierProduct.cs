/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1287.20792

			Generation Date: 28/11/2004 15:05:23
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
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using Params = OlymarsDemo.DataClasses.Parameters;

namespace OlymarsDemo.Web.Repeaters {

	/// <summary>
	/// This class is based on a System.Web.UI.WebControls.Repeater class and was built to display the 'tblSupplierProduct' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:23", SqlObjectDependancyName="tblSupplierProduct", SqlObjectDependancyRevision=544)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WebRepeater_tblSupplierProduct), "WebRepeater.bmp")]
	public sealed class WebRepeater_tblSupplierProduct : System.Web.UI.WebControls.Repeater {

		private Params.spS_tblSupplierProduct param;
		private System.Data.SqlTypes.SqlGuid FK_Spr_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		private System.Data.SqlTypes.SqlGuid FK_Spr_GuidSupplierID = System.Data.SqlTypes.SqlGuid.Null;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private OlymarsDemo.DataClasses.ConnectionType LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
		private bool doDataBindAfterRefreshData = true;

		/// <summary>
		/// Returns the current type of connection that is going or has been used against the Sql Server database.
		/// </summary>
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.LastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.ConnectionString) {

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

				if (this.LastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the OlymarsDemo.DataClasses.Parameters.spS_tblSupplierProduct class that was used to populate this control.
		/// </summary>
		public Params.spS_tblSupplierProduct SP_Parameter {

			get {

				return(this.param);
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public override void Dispose() {

			this.FK_Spr_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
			this.FK_Spr_GuidSupplierID = System.Data.SqlTypes.SqlGuid.Null;

			if (this.param != null) {

				this.param.Dispose();
			}

			base.Dispose();
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Web.Repeaters.WebRepeater_tblSupplierProduct class.
		/// </summary>
		public WebRepeater_tblSupplierProduct() : base() {

		}

		/// <summary>
		/// Gets or sets the databinding behavior after the RefreshData method is called. True if DataBind has to be called after
		/// RefreshData call, False if not.
		/// </summary>
		public bool DoDataBindAfterRefreshData {

			get {

				return(this.doDataBindAfterRefreshData);
			}

			set {

				this.doDataBindAfterRefreshData = value;
			}
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// or only a subset (based on its foreign keys).
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="FK_Spr_GuidProductID">Value for this foreign key.</param>
		/// <param name="FK_Spr_GuidSupplierID">Value for this foreign key.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlGuid FK_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid FK_Spr_GuidSupplierID) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplierProduct'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblSupplierProduct", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.FK_Spr_GuidProductID = FK_Spr_GuidProductID;
			this.FK_Spr_GuidSupplierID = FK_Spr_GuidSupplierID;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// or only a subset (based on its foreign keys).
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object.
		/// If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="FK_Spr_GuidProductID">Value for this foreign key.</param>
		/// <param name="FK_Spr_GuidSupplierID">Value for this foreign key.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlGuid FK_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid FK_Spr_GuidSupplierID) {

			this.sqlConnection= sqlConnection;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;

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

			this.FK_Spr_GuidProductID = FK_Spr_GuidProductID;
			this.FK_Spr_GuidSupplierID = FK_Spr_GuidSupplierID;
		}

		/// <summary>
		/// Load or reloads all the table content.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(-1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			if (this.LastKnownConnectionType == OlymarsDemo.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}

			this.param = new Params.spS_tblSupplierProduct(true);

			switch (this.LastKnownConnectionType) {

 				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (!this.FK_Spr_GuidProductID.IsNull) {

				this.param.Param_Spr_GuidProductID = this.FK_Spr_GuidProductID;
			}

			if (!this.FK_Spr_GuidSupplierID.IsNull) {

				this.param.Param_Spr_GuidSupplierID = this.FK_Spr_GuidSupplierID;
			}

			System.Data.DataSet DS = null;
				
			SPs.spS_tblSupplierProduct SP = new SPs.spS_tblSupplierProduct(false);
			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.DataSource = DS.Tables["spS_tblSupplierProduct"].DefaultView;
				if (this.doDataBindAfterRefreshData) {

					this.DataBind();
				}

				SP.Dispose();
			}

			else {

				SP.Dispose();
				throw new OlymarsDemo.DataClasses.CustomException(this.param, "WebRepeater_tblSupplierProduct : System.Web.UI.WebControls.Repeater", "RefreshData");
			}
		}
	}
}
