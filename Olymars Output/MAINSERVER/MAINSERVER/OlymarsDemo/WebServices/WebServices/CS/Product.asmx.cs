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
	public class Product : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Product() {

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
		public Guid Add_Product(Tables.Product record) {

			Params.spI_tblProduct param = new Params.spI_tblProduct(true);

			param.SetUpConnection(string.Empty);

			if (record.Name == null || record.Name.UseNull) {

				param.Param_Pro_StrName = SqlString.Null;
			}
			else if (!record.Name.UseDefault) {

				param.Param_Pro_StrName = record.Name.Value;
			}

			if (record.Col_Pro_CurPrice == null || record.Col_Pro_CurPrice.UseNull) {

				param.Param_Pro_CurPrice = SqlMoney.Null;
			}
			else if (!record.Col_Pro_CurPrice.UseDefault) {

				param.Param_Pro_CurPrice = record.Col_Pro_CurPrice.Value;
			}

			if (record.CategoryID == null || record.CategoryID.UseNull) {

				param.Param_Pro_LngCategoryID = SqlInt32.Null;
			}
			else if (!record.CategoryID.UseDefault) {

				param.Param_Pro_LngCategoryID = record.CategoryID.Value;
			}


			using (SPs.spI_tblProduct sp = new SPs.spI_tblProduct(true)) {

				sp.Execute(ref param);
				Guid id  = param.Param_Pro_GuidID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_Product(Tables.Product record) {

			Params.spU_tblProduct param = new Params.spU_tblProduct(false);

			param.SetUpConnection(string.Empty);

			param.Param_Pro_GuidID = record.Col_Pro_GuidID.Value;

			if (record.Name == null || record.Name.UseNull) {

				param.Param_ConsiderNull_Pro_StrName = true;
				param.Param_Pro_StrName = SqlString.Null;
			}
			else if (!record.Name.UseDefault) {

				param.Param_Pro_StrName = record.Name.Value;
			}

			if (record.Col_Pro_CurPrice == null || record.Col_Pro_CurPrice.UseNull) {

				param.Param_ConsiderNull_Pro_CurPrice = true;
				param.Param_Pro_CurPrice = SqlMoney.Null;
			}
			else if (!record.Col_Pro_CurPrice.UseDefault) {

				param.Param_Pro_CurPrice = record.Col_Pro_CurPrice.Value;
			}

			if (record.CategoryID == null || record.CategoryID.UseNull) {

				param.Param_ConsiderNull_Pro_LngCategoryID = true;
				param.Param_Pro_LngCategoryID = SqlInt32.Null;
			}
			else if (!record.CategoryID.UseDefault) {

				param.Param_Pro_LngCategoryID = record.CategoryID.Value;
			}


			using (SPs.spU_tblProduct sp = new SPs.spU_tblProduct(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_Product(Guid id) {

			Params.spD_tblProduct param = new Params.spD_tblProduct(true);

			param.SetUpConnection(string.Empty);

			param.Param_Pro_GuidID = id;

			using (SPs.spD_tblProduct sp = new SPs.spD_tblProduct(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_Products_DataSet(WSTypes.WSInt32 categoryID) {

			Params.spS_tblProduct_Display param = new Params.spS_tblProduct_Display(true);

			param.SetUpConnection(string.Empty);

			if (categoryID == null || categoryID.UseNull) {

				param.Param_Pro_LngCategoryID = SqlInt32.Null;
			}
			else if (!categoryID.UseDefault) {

				param.Param_Pro_LngCategoryID = categoryID.Value;
			}


			using (SPs.spS_tblProduct_Display sp = new SPs.spS_tblProduct_Display(true)) {

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
		public Common.Item[] GetAllDisplay_Products_Array(WSTypes.WSInt32 categoryID) {

			Params.spS_tblProduct_Display param = new Params.spS_tblProduct_Display(true);

			param.SetUpConnection(string.Empty);

			if (categoryID == null || categoryID.UseNull) {

				param.Param_Pro_LngCategoryID = SqlInt32.Null;
			}
			else if (!categoryID.UseDefault) {

				param.Param_Pro_LngCategoryID = categoryID.Value;
			}


			using (SPs.spS_tblProduct_Display sp = new SPs.spS_tblProduct_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetGuid(SPs.spS_tblProduct_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblProduct_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
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
		public Tables.Product GetOne_Product(Guid id) {

			Params.spS_tblProduct param = new Params.spS_tblProduct(true);

			param.SetUpConnection(string.Empty);

			param.Param_Pro_GuidID = id;

			using (SPs.spS_tblProduct sp = new SPs.spS_tblProduct(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.Product record = new Tables.Product();

					record.Col_Pro_GuidID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_GuidID.ColumnIndex);
					if (!record.Col_Pro_GuidID.UseNull) record.Col_Pro_GuidID.Value = sqlDataReader.GetSqlGuid(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_GuidID.ColumnIndex).Value;

					record.Name.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_StrName.ColumnIndex);
					if (!record.Name.UseNull) record.Name.Value = sqlDataReader.GetSqlString(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_StrName.ColumnIndex).Value;

					record.Col_Pro_CurPrice.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_CurPrice.ColumnIndex);
					if (!record.Col_Pro_CurPrice.UseNull) record.Col_Pro_CurPrice.Value = sqlDataReader.GetSqlMoney(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_CurPrice.ColumnIndex).Value;

					record.CategoryID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_LngCategoryID.ColumnIndex);
					if (!record.CategoryID.UseNull) record.CategoryID.Value = sqlDataReader.GetSqlInt32(SPs.spS_tblProduct.Resultset1.Fields.Column_Pro_LngCategoryID.ColumnIndex).Value;


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
