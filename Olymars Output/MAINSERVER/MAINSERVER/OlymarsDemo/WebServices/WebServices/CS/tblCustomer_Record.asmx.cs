/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:16
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:39:29
			Template revision: 324

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
using System.Data.SqlTypes;
using System.Web.Services;
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using OlymarsDemo.WebServices;
using System.ComponentModel;

namespace OlymarsDemo.WebServices {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class tblCustomer_Record : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblCustomer_Record() {

			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {

			if(disposing && components != null) {

				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		private enum Action {

			Add,
			Edit,
			Delete,
			GetAllDisplay_DataSet,
			GetAllDisplay_Array,
			GetOne
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public Int32 Add_tblCustomer_Record(Tables.tblCustomer_Record record) {

			Params.spI_tblCustomer param = new Params.spI_tblCustomer(true);

			param.SetUpConnection(string.Empty);

			if (record.Customer_Surname == null || record.Customer_Surname.UseNull) {

				param.Param_Cus_StrLastName = SqlString.Null;
			}
			else if (!record.Customer_Surname.UseDefault) {

				param.Param_Cus_StrLastName = record.Customer_Surname.Value;
			}

			if (record.Col_Cus_StrFirstName == null || record.Col_Cus_StrFirstName.UseNull) {

				param.Param_Cus_StrFirstName = SqlString.Null;
			}
			else if (!record.Col_Cus_StrFirstName.UseDefault) {

				param.Param_Cus_StrFirstName = record.Col_Cus_StrFirstName.Value;
			}

			if (record.Col_Cus_StrEmail == null || record.Col_Cus_StrEmail.UseNull) {

				param.Param_Cus_StrEmail = SqlString.Null;
			}
			else if (!record.Col_Cus_StrEmail.UseDefault) {

				param.Param_Cus_StrEmail = record.Col_Cus_StrEmail.Value;
			}


			using (SPs.spI_tblCustomer sp = new SPs.spI_tblCustomer(true)) {

				sp.Execute(ref param);
				Int32 id  = param.Param_Cus_LngID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_tblCustomer_Record(Tables.tblCustomer_Record record) {

			Params.spU_tblCustomer param = new Params.spU_tblCustomer(false);

			param.SetUpConnection(string.Empty);

			param.Param_Cus_LngID = record.Col_Cus_LngID.Value;

			if (record.Customer_Surname == null || record.Customer_Surname.UseNull) {

				param.Param_ConsiderNull_Cus_StrLastName = true;
				param.Param_Cus_StrLastName = SqlString.Null;
			}
			else if (!record.Customer_Surname.UseDefault) {

				param.Param_Cus_StrLastName = record.Customer_Surname.Value;
			}

			if (record.Col_Cus_StrFirstName == null || record.Col_Cus_StrFirstName.UseNull) {

				param.Param_ConsiderNull_Cus_StrFirstName = true;
				param.Param_Cus_StrFirstName = SqlString.Null;
			}
			else if (!record.Col_Cus_StrFirstName.UseDefault) {

				param.Param_Cus_StrFirstName = record.Col_Cus_StrFirstName.Value;
			}

			if (record.Col_Cus_StrEmail == null || record.Col_Cus_StrEmail.UseNull) {

				param.Param_ConsiderNull_Cus_StrEmail = true;
				param.Param_Cus_StrEmail = SqlString.Null;
			}
			else if (!record.Col_Cus_StrEmail.UseDefault) {

				param.Param_Cus_StrEmail = record.Col_Cus_StrEmail.Value;
			}


			using (SPs.spU_tblCustomer sp = new SPs.spU_tblCustomer(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_tblCustomer_Record(Int32 id) {

			Params.spD_tblCustomer param = new Params.spD_tblCustomer(true);

			param.SetUpConnection(string.Empty);

			param.Param_Cus_LngID = id;

			using (SPs.spD_tblCustomer sp = new SPs.spD_tblCustomer(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_tblCustomer_Collection_DataSet() {

			Params.spS_tblCustomer_Display param = new Params.spS_tblCustomer_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblCustomer_Display sp = new SPs.spS_tblCustomer_Display(true)) {

				System.Data.DataSet dataSet = null;
				sp.Execute(ref param, ref dataSet);
				param.Dispose();

				return(dataSet);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public Common.Item[] GetAllDisplay_tblCustomer_Collection_Array() {

			Params.spS_tblCustomer_Display param = new Params.spS_tblCustomer_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblCustomer_Display sp = new SPs.spS_tblCustomer_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetInt32(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
				}

				sqlDataReader.Close();

				if (sp.Connection.State == System.Data.ConnectionState.Open) sp.Connection.Close();

				sp.Dispose();
				param.Dispose();

				return((Common.Item[])records.ToArray(typeof(Common.Item)));
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public Tables.tblCustomer_Record GetOne_tblCustomer_Record(Int32 id) {

			Params.spS_tblCustomer param = new Params.spS_tblCustomer(true);

			param.SetUpConnection(string.Empty);

			param.Param_Cus_LngID = id;

			using (SPs.spS_tblCustomer sp = new SPs.spS_tblCustomer(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.tblCustomer_Record record = new Tables.tblCustomer_Record();

					record.Col_Cus_LngID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_LngID.ColumnIndex);
					if (!record.Col_Cus_LngID.UseNull) record.Col_Cus_LngID.Value = sqlDataReader.GetSqlInt32(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_LngID.ColumnIndex).Value;

					record.Customer_Surname.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrLastName.ColumnIndex);
					if (!record.Customer_Surname.UseNull) record.Customer_Surname.Value = sqlDataReader.GetSqlString(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrLastName.ColumnIndex).Value;

					record.Col_Cus_StrFirstName.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrFirstName.ColumnIndex);
					if (!record.Col_Cus_StrFirstName.UseNull) record.Col_Cus_StrFirstName.Value = sqlDataReader.GetSqlString(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrFirstName.ColumnIndex).Value;

					record.Col_Cus_StrEmail.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrEmail.ColumnIndex);
					if (!record.Col_Cus_StrEmail.UseNull) record.Col_Cus_StrEmail.Value = sqlDataReader.GetSqlString(SPs.spS_tblCustomer.Resultset1.Fields.Column_Cus_StrEmail.ColumnIndex).Value;


					sqlDataReader.Close();
					sp.Connection.Close();

					return(record);
				}
				else {

					sqlDataReader.Close();
					sp.Connection.Close();

					return(null);
				}
			}
		}
	}
}
