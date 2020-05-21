/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:15
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
	public class spD_tblOrderItem : System.Web.Services.WebService {


		public spD_tblOrderItem() {

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
		public void Execute(WSGuid Oit_GuidID, WSGuid Oit_GuidOrderID, WSGuid Oit_GuidProductID) {
		
			Params.spD_tblOrderItem param = new Params.spD_tblOrderItem(true);
			param.SetUpConnection(string.Empty);

			if (Oit_GuidID == null || Oit_GuidID.UseNull) {

				param.Param_Oit_GuidID = SqlGuid.Null;
			}
			else if (!Oit_GuidID.UseDefault) {

				param.Param_Oit_GuidID = Oit_GuidID.Value;
			}

			if (Oit_GuidOrderID == null || Oit_GuidOrderID.UseNull) {

				param.Param_Oit_GuidOrderID = SqlGuid.Null;
			}
			else if (!Oit_GuidOrderID.UseDefault) {

				param.Param_Oit_GuidOrderID = Oit_GuidOrderID.Value;
			}

			if (Oit_GuidProductID == null || Oit_GuidProductID.UseNull) {

				param.Param_Oit_GuidProductID = SqlGuid.Null;
			}
			else if (!Oit_GuidProductID.UseDefault) {

				param.Param_Oit_GuidProductID = Oit_GuidProductID.Value;
			}

			using (SPs.spD_tblOrderItem sp = new SPs.spD_tblOrderItem(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

	}
}
