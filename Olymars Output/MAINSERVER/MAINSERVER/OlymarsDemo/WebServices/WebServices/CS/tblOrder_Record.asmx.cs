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
	public class tblOrder_Record : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrder_Record() {

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
		public Guid Add_tblOrder_Record(Tables.tblOrder_Record record) {

			Params.spI_tblOrder param = new Params.spI_tblOrder(true);

			param.SetUpConnection(string.Empty);

			if (record.Col_Ord_DatOrderedOn == null || record.Col_Ord_DatOrderedOn.UseNull) {

				param.Param_Ord_DatOrderedOn = SqlDateTime.Null;
			}
			else if (!record.Col_Ord_DatOrderedOn.UseDefault) {

				param.Param_Ord_DatOrderedOn = record.Col_Ord_DatOrderedOn.Value;
			}

			if (record.Col_Ord_LngCustomerID == null || record.Col_Ord_LngCustomerID.UseNull) {

				param.Param_Ord_LngCustomerID = SqlInt32.Null;
			}
			else if (!record.Col_Ord_LngCustomerID.UseDefault) {

				param.Param_Ord_LngCustomerID = record.Col_Ord_LngCustomerID.Value;
			}

			if (record.Col_Ord_CurTotal == null || record.Col_Ord_CurTotal.UseNull) {

				param.Param_Ord_CurTotal = SqlMoney.Null;
			}
			else if (!record.Col_Ord_CurTotal.UseDefault) {

				param.Param_Ord_CurTotal = record.Col_Ord_CurTotal.Value;
			}


			using (SPs.spI_tblOrder sp = new SPs.spI_tblOrder(true)) {

				sp.Execute(ref param);
				Guid id  = param.Param_Ord_GuidID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_tblOrder_Record(Tables.tblOrder_Record record) {

			Params.spU_tblOrder param = new Params.spU_tblOrder(false);

			param.SetUpConnection(string.Empty);

			param.Param_Ord_GuidID = record.Col_Ord_GuidID.Value;

			if (record.Col_Ord_DatOrderedOn == null || record.Col_Ord_DatOrderedOn.UseNull) {

				param.Param_ConsiderNull_Ord_DatOrderedOn = true;
				param.Param_Ord_DatOrderedOn = SqlDateTime.Null;
			}
			else if (!record.Col_Ord_DatOrderedOn.UseDefault) {

				param.Param_Ord_DatOrderedOn = record.Col_Ord_DatOrderedOn.Value;
			}

			if (record.Col_Ord_LngCustomerID == null || record.Col_Ord_LngCustomerID.UseNull) {

				param.Param_ConsiderNull_Ord_LngCustomerID = true;
				param.Param_Ord_LngCustomerID = SqlInt32.Null;
			}
			else if (!record.Col_Ord_LngCustomerID.UseDefault) {

				param.Param_Ord_LngCustomerID = record.Col_Ord_LngCustomerID.Value;
			}

			if (record.Col_Ord_CurTotal == null || record.Col_Ord_CurTotal.UseNull) {

				param.Param_ConsiderNull_Ord_CurTotal = true;
				param.Param_Ord_CurTotal = SqlMoney.Null;
			}
			else if (!record.Col_Ord_CurTotal.UseDefault) {

				param.Param_Ord_CurTotal = record.Col_Ord_CurTotal.Value;
			}


			using (SPs.spU_tblOrder sp = new SPs.spU_tblOrder(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_tblOrder_Record(Guid id) {

			Params.spD_tblOrder param = new Params.spD_tblOrder(true);

			param.SetUpConnection(string.Empty);

			param.Param_Ord_GuidID = id;

			using (SPs.spD_tblOrder sp = new SPs.spD_tblOrder(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_tblOrder_Collection_DataSet(WSTypes.WSInt32 col_Ord_LngCustomerID) {

			Params.spS_tblOrder_Display param = new Params.spS_tblOrder_Display(true);

			param.SetUpConnection(string.Empty);

			if (col_Ord_LngCustomerID == null || col_Ord_LngCustomerID.UseNull) {

				param.Param_Ord_LngCustomerID = SqlInt32.Null;
			}
			else if (!col_Ord_LngCustomerID.UseDefault) {

				param.Param_Ord_LngCustomerID = col_Ord_LngCustomerID.Value;
			}


			using (SPs.spS_tblOrder_Display sp = new SPs.spS_tblOrder_Display(true)) {

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
		public Common.Item[] GetAllDisplay_tblOrder_Collection_Array(WSTypes.WSInt32 col_Ord_LngCustomerID) {

			Params.spS_tblOrder_Display param = new Params.spS_tblOrder_Display(true);

			param.SetUpConnection(string.Empty);

			if (col_Ord_LngCustomerID == null || col_Ord_LngCustomerID.UseNull) {

				param.Param_Ord_LngCustomerID = SqlInt32.Null;
			}
			else if (!col_Ord_LngCustomerID.UseDefault) {

				param.Param_Ord_LngCustomerID = col_Ord_LngCustomerID.Value;
			}


			using (SPs.spS_tblOrder_Display sp = new SPs.spS_tblOrder_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetGuid(SPs.spS_tblOrder_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblOrder_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
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
		public Tables.tblOrder_Record GetOne_tblOrder_Record(Guid id) {

			Params.spS_tblOrder param = new Params.spS_tblOrder(true);

			param.SetUpConnection(string.Empty);

			param.Param_Ord_GuidID = id;

			using (SPs.spS_tblOrder sp = new SPs.spS_tblOrder(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.tblOrder_Record record = new Tables.tblOrder_Record();

					record.Col_Ord_GuidID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_GuidID.ColumnIndex);
					if (!record.Col_Ord_GuidID.UseNull) record.Col_Ord_GuidID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_GuidID.ColumnIndex).Value;

					record.Col_Ord_DatOrderedOn.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_DatOrderedOn.ColumnIndex);
					if (!record.Col_Ord_DatOrderedOn.UseNull) record.Col_Ord_DatOrderedOn.Value = sqlDataReader.GetSqlDateTime(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_DatOrderedOn.ColumnIndex).Value;

					record.Col_Ord_LngCustomerID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_LngCustomerID.ColumnIndex);
					if (!record.Col_Ord_LngCustomerID.UseNull) record.Col_Ord_LngCustomerID.Value = sqlDataReader.GetSqlInt32(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_LngCustomerID.ColumnIndex).Value;

					record.Col_Ord_CurTotal.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_CurTotal.ColumnIndex);
					if (!record.Col_Ord_CurTotal.UseNull) record.Col_Ord_CurTotal.Value = sqlDataReader.GetSqlMoney(SPs.spS_tblOrder.Resultset1.Fields.Column_Ord_CurTotal.ColumnIndex).Value;


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
