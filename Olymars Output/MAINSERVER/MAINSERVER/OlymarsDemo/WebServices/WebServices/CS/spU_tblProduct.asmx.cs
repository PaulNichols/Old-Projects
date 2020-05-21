/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:17
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

using System.Collections;
using System.ComponentModel;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using OlymarsDemo.WebServices.WSTypes;
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.WebServices {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class spU_tblProduct : System.Web.Services.WebService {


		public spU_tblProduct() {

			//CODEGEN: This call is required by the ASP.NET Web Services Designer
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

		[WebMethod()]
		public void Execute(WSGuid Pro_GuidID, WSString Pro_StrName, WSBoolean ConsiderNull_Pro_StrName, WSDecimal Pro_CurPrice, WSBoolean ConsiderNull_Pro_CurPrice, WSInt32 Pro_LngCategoryID, WSBoolean ConsiderNull_Pro_LngCategoryID) {
		
			Params.spU_tblProduct param = new Params.spU_tblProduct(true);
			param.SetUpConnection(string.Empty);

			if (Pro_GuidID == null || Pro_GuidID.UseNull) {

				param.Param_Pro_GuidID = SqlGuid.Null;
			}
			else if (!Pro_GuidID.UseDefault) {

				param.Param_Pro_GuidID = Pro_GuidID.Value;
			}

			if (Pro_StrName == null || Pro_StrName.UseNull) {

				param.Param_Pro_StrName = SqlString.Null;
			}
			else if (!Pro_StrName.UseDefault) {

				param.Param_Pro_StrName = Pro_StrName.Value;
			}

			if (ConsiderNull_Pro_StrName == null || ConsiderNull_Pro_StrName.UseNull) {

				param.Param_ConsiderNull_Pro_StrName = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Pro_StrName.UseDefault) {

				param.Param_ConsiderNull_Pro_StrName = ConsiderNull_Pro_StrName.Value;
			}

			if (Pro_CurPrice == null || Pro_CurPrice.UseNull) {

				param.Param_Pro_CurPrice = SqlMoney.Null;
			}
			else if (!Pro_CurPrice.UseDefault) {

				param.Param_Pro_CurPrice = Pro_CurPrice.Value;
			}

			if (ConsiderNull_Pro_CurPrice == null || ConsiderNull_Pro_CurPrice.UseNull) {

				param.Param_ConsiderNull_Pro_CurPrice = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Pro_CurPrice.UseDefault) {

				param.Param_ConsiderNull_Pro_CurPrice = ConsiderNull_Pro_CurPrice.Value;
			}

			if (Pro_LngCategoryID == null || Pro_LngCategoryID.UseNull) {

				param.Param_Pro_LngCategoryID = SqlInt32.Null;
			}
			else if (!Pro_LngCategoryID.UseDefault) {

				param.Param_Pro_LngCategoryID = Pro_LngCategoryID.Value;
			}

			if (ConsiderNull_Pro_LngCategoryID == null || ConsiderNull_Pro_LngCategoryID.UseNull) {

				param.Param_ConsiderNull_Pro_LngCategoryID = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Pro_LngCategoryID.UseDefault) {

				param.Param_ConsiderNull_Pro_LngCategoryID = ConsiderNull_Pro_LngCategoryID.Value;
			}

			using (SPs.spU_tblProduct sp = new SPs.spU_tblProduct(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

	}
}