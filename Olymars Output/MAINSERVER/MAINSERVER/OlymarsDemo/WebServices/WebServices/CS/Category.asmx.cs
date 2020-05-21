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
	public class Category : System.Web.Services.WebService {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Category() {

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
		public Int32 Add_Category(Tables.Category record) {

			Params.spI_tblCategory param = new Params.spI_tblCategory(true);

			param.SetUpConnection(string.Empty);

			if (record.Col_Cat_StrName == null || record.Col_Cat_StrName.UseNull) {

				param.Param_Cat_StrName = SqlString.Null;
			}
			else if (!record.Col_Cat_StrName.UseDefault) {

				param.Param_Cat_StrName = record.Col_Cat_StrName.Value;
			}


			using (SPs.spI_tblCategory sp = new SPs.spI_tblCategory(true)) {

				sp.Execute(ref param);
				Int32 id  = param.Param_Cat_LngID.Value;
				param.Dispose();

				return(id);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Edit_Category(Tables.Category record) {

			Params.spU_tblCategory param = new Params.spU_tblCategory(false);

			param.SetUpConnection(string.Empty);

			param.Param_Cat_LngID = record.Col_Cat_LngID.Value;

			if (record.Col_Cat_StrName == null || record.Col_Cat_StrName.UseNull) {

				param.Param_ConsiderNull_Cat_StrName = true;
				param.Param_Cat_StrName = SqlString.Null;
			}
			else if (!record.Col_Cat_StrName.UseDefault) {

				param.Param_Cat_StrName = record.Col_Cat_StrName.Value;
			}


			using (SPs.spU_tblCategory sp = new SPs.spU_tblCategory(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public void Delete_Category(Int32 id) {

			Params.spD_tblCategory param = new Params.spD_tblCategory(true);

			param.SetUpConnection(string.Empty);

			param.Param_Cat_LngID = id;

			using (SPs.spD_tblCategory sp = new SPs.spD_tblCategory(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		[WebMethod(false)]
		public System.Data.DataSet GetAllDisplay_Categories_DataSet() {

			Params.spS_tblCategory_Display param = new Params.spS_tblCategory_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblCategory_Display sp = new SPs.spS_tblCategory_Display(true)) {

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
		public Common.Item[] GetAllDisplay_Categories_Array() {

			Params.spS_tblCategory_Display param = new Params.spS_tblCategory_Display(true);

			param.SetUpConnection(string.Empty);


			using (SPs.spS_tblCategory_Display sp = new SPs.spS_tblCategory_Display(true)) {

				System.Collections.ArrayList records = new System.Collections.ArrayList();

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);

				while (sqlDataReader.Read()) {

					records.Add(new Common.Item(sqlDataReader.GetInt32(SPs.spS_tblCategory_Display.Resultset1.Fields.Column_ID1.ColumnIndex), sqlDataReader.GetString(SPs.spS_tblCategory_Display.Resultset1.Fields.Column_Display.ColumnIndex)));
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
		public Tables.Category GetOne_Category(Int32 id) {

			Params.spS_tblCategory param = new Params.spS_tblCategory(true);

			param.SetUpConnection(string.Empty);

			param.Param_Cat_LngID = id;

			using (SPs.spS_tblCategory sp = new SPs.spS_tblCategory(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				sp.Execute(ref param, out sqlDataReader);
				param.Dispose();

				if (sqlDataReader.Read()) {

					Tables.Category record = new Tables.Category();

					record.Col_Cat_LngID.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCategory.Resultset1.Fields.Column_Cat_LngID.ColumnIndex);
					if (!record.Col_Cat_LngID.UseNull) record.Col_Cat_LngID.Value = sqlDataReader.GetSqlInt32(SPs.spS_tblCategory.Resultset1.Fields.Column_Cat_LngID.ColumnIndex).Value;

					record.Col_Cat_StrName.UseNull = sqlDataReader.IsDBNull(SPs.spS_tblCategory.Resultset1.Fields.Column_Cat_StrName.ColumnIndex);
					if (!record.Col_Cat_StrName.UseNull) record.Col_Cat_StrName.Value = sqlDataReader.GetSqlString(SPs.spS_tblCategory.Resultset1.Fields.Column_Cat_StrName.ColumnIndex).Value;


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
