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
	public class tblOrderItem_Record : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrderItem_Record() {

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
		public Guid Add_tblOrderItem_Record(Tables.tblOrderItem_Record record) {

			Params.spI_tblOrderItem param = new Params.spI_tblOrderItem(true);

			param.SetUpConnection(string.Empty);

			if (record.Col_Oit_GuidOrderID == null || record.Col_Oit_GuidOrderID.UseNull) {

				param.Param_Oit_GuidOrderID = SqlGuid.Null;
			}
			else if (!record.Col_Oit_GuidOrderID.UseDefault) {

				param.Param_Oit_GuidOrderID = record.Col_Oit_GuidOrderID.Value;
			}

			if (record.Col_Oit_GuidProductID == null || record.Col_Oit_GuidProductID.UseNull) {

				param.Param_Oit_GuidProductID = SqlGuid.Null;
			}
			else if (!record.Col_Oit_GuidProductID.UseDefault) {

				param.Param_Oit_GuidProductID = record.Col_Oit_GuidProductID.Value;
			}

			if (record.Col_Oit_LngAmount == null || record.Col_Oit_LngAmount.UseNull) {

				param.Param_Oit_LngAmount = SqlInt32.Null;
			}
			else if (!record.Col_Oit_LngAmount.UseDefault) {

				param.Param_Oit_LngAmount = record.Col_Oit_LngAmount.Value;
			}


			using (SPs.spI_tblOrderItem sp = new SPs.spI_tblOrderItem(true)) {

				sp.Execute(ref param);
				Guid id  = param.Param_Oit_GuidID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_tblOrderItem_Record(Tables.tblOrderItem_Record record) {

			Params.spU_tblOrderItem param = new Params.spU_tblOrderItem(false);

			param.SetUpConnection(string.Empty);

			param.Param_Oit_GuidID = record.Col_Oit_GuidID.Value;

			if (record.Col_Oit_GuidOrderID == null || record.Col_Oit_GuidOrderID.UseNull) {

				param.Param_ConsiderNull_Oit_GuidOrderID = true;
				param.Param_Oit_GuidOrderID = SqlGuid.Null;
			}
			else if (!record.Col_Oit_GuidOrderID.UseDefault) {

				param.Param_Oit_GuidOrderID = record.Col_Oit_GuidOrderID.Value;
			}

			if (record.Col_Oit_GuidProductID == null || record.Col_Oit_GuidProductID.UseNull) {

				param.Param_ConsiderNull_Oit_GuidProductID = true;
				param.Param_Oit_GuidProductID = SqlGuid.Null;
			}
			else if (!record.Col_Oit_GuidProductID.UseDefault) {

				param.Param_Oit_GuidProductID = record.Col_Oit_GuidProductID.Value;
			}

			if (record.Col_Oit_LngAmount == null || record.Col_Oit_LngAmount.UseNull) {

				param.Param_ConsiderNull_Oit_LngAmount = true;
				param.Param_Oit_LngAmount = SqlInt32.Null;
			}
			else if (!record.Col_Oit_LngAmount.UseDefault) {

				param.Param_Oit_LngAmount = record.Col_Oit_LngAmount.Value;
			}


			using (SPs.spU_tblOrderItem sp = new SPs.spU_tblOrderItem(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_tblOrderItem_Record(Guid id) {

			Params.spD_tblOrderItem param = new Params.spD_tblOrderItem(true);

			param.SetUpConnection(string.Empty);

			param.Param_Oit_GuidID = id;

			using (SPs.spD_tblOrderItem sp = new SPs.spD_tblOrderItem(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_tblOrderItem_Collection_DataSet(WSTypes.WSGuid col_Oit_GuidOrderID, WSTypes.WSGuid col_Oit_GuidProductID) {

			Params.spS_tblOrderItem_Display param = new Params.spS_tblOrderItem_Display(true);

			param.SetUpConnection(string.Empty);

			if (col_Oit_GuidOrderID == null || col_Oit_GuidOrderID.UseNull) {

				param.Param_Oit_GuidOrderID = SqlGuid.Null;
			}
			else if (!col_Oit_GuidOrderID.UseDefault) {

				param.Param_Oit_GuidOrderID = col_Oit_GuidOrderID.Value;
			}

			if (col_Oit_GuidProductID == null || col_Oit_GuidProductID.UseNull) {

				param.Param_Oit_GuidProductID = SqlGuid.Null;
			}
			else if (!col_Oit_GuidProductID.UseDefault) {

				param.Param_Oit_GuidProductID = col_Oit_GuidProductID.Value;
			}


			using (SPs.spS_tblOrderItem_Display sp = new SPs.spS_tblOrderItem_Display(true)) {

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
		public Common.Item[] GetAllDisplay_tblOrderItem_Collection_Array(WSTypes.WSGuid col_Oit_GuidOrderID, WSTypes.WSGuid col_Oit_GuidProductID) {

			Params.spS_tblOrderItem_Display param = new Params.spS_tblOrderItem_Display(true);

			param.SetUpConnection(string.Empty);

			if (col_Oit_GuidOrderID == null || col_Oit_GuidOrderID.UseNull) {

				param.Param_Oit_GuidOrderID = SqlGuid.Null;
			}
			else if (!col_Oit_GuidOrderID.UseDefault) {

				param.Param_Oit_GuidOrderID = col_Oit_GuidOrderID.Value;
			}

			if (col_Oit_GuidProductID == null || col_Oit_GuidProductID.UseNull) {

				param.Param_Oit_GuidProductID = SqlGuid.Null;
			}
			else if (!col_Oit_GuidProductID.UseDefault) {

				param.Param_Oit_GuidProductID = col_Oit_GuidProductID.Value;
			}


			using (SPs.spS_tblOrderItem_Display sp = new SPs.spS_tblOrderItem_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetGuid(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
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
		public Tables.tblOrderItem_Record GetOne_tblOrderItem_Record(Guid id) {

			Params.spS_tblOrderItem param = new Params.spS_tblOrderItem(true);

			param.SetUpConnection(string.Empty);

			param.Param_Oit_GuidID = id;

			using (SPs.spS_tblOrderItem sp = new SPs.spS_tblOrderItem(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.tblOrderItem_Record record = new Tables.tblOrderItem_Record();

					record.Col_Oit_GuidID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidID.ColumnIndex);
					if (!record.Col_Oit_GuidID.UseNull) record.Col_Oit_GuidID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidID.ColumnIndex).Value;

					record.Col_Oit_GuidOrderID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidOrderID.ColumnIndex);
					if (!record.Col_Oit_GuidOrderID.UseNull) record.Col_Oit_GuidOrderID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidOrderID.ColumnIndex).Value;

					record.Col_Oit_GuidProductID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidProductID.ColumnIndex);
					if (!record.Col_Oit_GuidProductID.UseNull) record.Col_Oit_GuidProductID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidProductID.ColumnIndex).Value;

					record.Col_Oit_LngAmount.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_LngAmount.ColumnIndex);
					if (!record.Col_Oit_LngAmount.UseNull) record.Col_Oit_LngAmount.Value = sqlDataReader.GetSqlInt32(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_LngAmount.ColumnIndex).Value;


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
