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
	public class spU_tblOrder : System.Web.Services.WebService {


		public spU_tblOrder() {

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
		public void Execute(WSGuid Ord_GuidID, WSDateTime Ord_DatOrderedOn, WSBoolean ConsiderNull_Ord_DatOrderedOn, WSInt32 Ord_LngCustomerID, WSBoolean ConsiderNull_Ord_LngCustomerID, WSDecimal Ord_CurTotal, WSBoolean ConsiderNull_Ord_CurTotal) {
		
			Params.spU_tblOrder param = new Params.spU_tblOrder(true);
			param.SetUpConnection(string.Empty);

			if (Ord_GuidID == null || Ord_GuidID.UseNull) {

				param.Param_Ord_GuidID = SqlGuid.Null;
			}
			else if (!Ord_GuidID.UseDefault) {

				param.Param_Ord_GuidID = Ord_GuidID.Value;
			}

			if (Ord_DatOrderedOn == null || Ord_DatOrderedOn.UseNull) {

				param.Param_Ord_DatOrderedOn = SqlDateTime.Null;
			}
			else if (!Ord_DatOrderedOn.UseDefault) {

				param.Param_Ord_DatOrderedOn = Ord_DatOrderedOn.Value;
			}

			if (ConsiderNull_Ord_DatOrderedOn == null || ConsiderNull_Ord_DatOrderedOn.UseNull) {

				param.Param_ConsiderNull_Ord_DatOrderedOn = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Ord_DatOrderedOn.UseDefault) {

				param.Param_ConsiderNull_Ord_DatOrderedOn = ConsiderNull_Ord_DatOrderedOn.Value;
			}

			if (Ord_LngCustomerID == null || Ord_LngCustomerID.UseNull) {

				param.Param_Ord_LngCustomerID = SqlInt32.Null;
			}
			else if (!Ord_LngCustomerID.UseDefault) {

				param.Param_Ord_LngCustomerID = Ord_LngCustomerID.Value;
			}

			if (ConsiderNull_Ord_LngCustomerID == null || ConsiderNull_Ord_LngCustomerID.UseNull) {

				param.Param_ConsiderNull_Ord_LngCustomerID = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Ord_LngCustomerID.UseDefault) {

				param.Param_ConsiderNull_Ord_LngCustomerID = ConsiderNull_Ord_LngCustomerID.Value;
			}

			if (Ord_CurTotal == null || Ord_CurTotal.UseNull) {

				param.Param_Ord_CurTotal = SqlMoney.Null;
			}
			else if (!Ord_CurTotal.UseDefault) {

				param.Param_Ord_CurTotal = Ord_CurTotal.Value;
			}

			if (ConsiderNull_Ord_CurTotal == null || ConsiderNull_Ord_CurTotal.UseNull) {

				param.Param_ConsiderNull_Ord_CurTotal = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Ord_CurTotal.UseDefault) {

				param.Param_ConsiderNull_Ord_CurTotal = ConsiderNull_Ord_CurTotal.Value;
			}

			using (SPs.spU_tblOrder sp = new SPs.spU_tblOrder(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

	}
}
