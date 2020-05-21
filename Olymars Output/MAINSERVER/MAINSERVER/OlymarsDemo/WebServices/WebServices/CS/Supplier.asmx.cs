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
	public class Supplier : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Supplier() {

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
		public Guid Add_Supplier(Tables.Supplier record) {

			Params.spI_tblSupplier param = new Params.spI_tblSupplier(true);

			param.SetUpConnection(string.Empty);

			if (record.Col_Sup_StrName == null || record.Col_Sup_StrName.UseNull) {

				param.Param_Sup_StrName = SqlString.Null;
			}
			else if (!record.Col_Sup_StrName.UseDefault) {

				param.Param_Sup_StrName = record.Col_Sup_StrName.Value;
			}


			using (SPs.spI_tblSupplier sp = new SPs.spI_tblSupplier(true)) {

				sp.Execute(ref param);
				Guid id  = param.Param_Sup_GuidID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_Supplier(Tables.Supplier record) {

			Params.spU_tblSupplier param = new Params.spU_tblSupplier(false);

			param.SetUpConnection(string.Empty);

			param.Param_Sup_GuidID = record.Col_Sup_GuidID.Value;

			if (record.Col_Sup_StrName == null || record.Col_Sup_StrName.UseNull) {

				param.Param_ConsiderNull_Sup_StrName = true;
				param.Param_Sup_StrName = SqlString.Null;
			}
			else if (!record.Col_Sup_StrName.UseDefault) {

				param.Param_Sup_StrName = record.Col_Sup_StrName.Value;
			}


			using (SPs.spU_tblSupplier sp = new SPs.spU_tblSupplier(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_Supplier(Guid id) {

			Params.spD_tblSupplier param = new Params.spD_tblSupplier(true);

			param.SetUpConnection(string.Empty);

			param.Param_Sup_GuidID = id;

			using (SPs.spD_tblSupplier sp = new SPs.spD_tblSupplier(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_Suppliers_DataSet() {

			Params.spS_tblSupplier_Display param = new Params.spS_tblSupplier_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblSupplier_Display sp = new SPs.spS_tblSupplier_Display(true)) {

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
		public Common.Item[] GetAllDisplay_Suppliers_Array() {

			Params.spS_tblSupplier_Display param = new Params.spS_tblSupplier_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblSupplier_Display sp = new SPs.spS_tblSupplier_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetGuid(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
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
		public Tables.Supplier GetOne_Supplier(Guid id) {

			Params.spS_tblSupplier param = new Params.spS_tblSupplier(true);

			param.SetUpConnection(string.Empty);

			param.Param_Sup_GuidID = id;

			using (SPs.spS_tblSupplier sp = new SPs.spS_tblSupplier(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.Supplier record = new Tables.Supplier();

					record.Col_Sup_GuidID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_GuidID.ColumnIndex);
					if (!record.Col_Sup_GuidID.UseNull) record.Col_Sup_GuidID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_GuidID.ColumnIndex).Value;

					record.Col_Sup_StrName.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_StrName.ColumnIndex);
					if (!record.Col_Sup_StrName.UseNull) record.Col_Sup_StrName.Value = sqlDataReader.GetSqlString(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_StrName.ColumnIndex).Value;


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
