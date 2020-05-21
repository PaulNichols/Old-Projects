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
	public class spU_tblCategory : System.Web.Services.WebService {


		public spU_tblCategory() {

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
		public void Execute(WSInt32 Cat_LngID, WSString Cat_StrName, WSBoolean ConsiderNull_Cat_StrName) {
		
			Params.spU_tblCategory param = new Params.spU_tblCategory(true);
			param.SetUpConnection(string.Empty);

			if (Cat_LngID == null || Cat_LngID.UseNull) {

				param.Param_Cat_LngID = SqlInt32.Null;
			}
			else if (!Cat_LngID.UseDefault) {

				param.Param_Cat_LngID = Cat_LngID.Value;
			}

			if (Cat_StrName == null || Cat_StrName.UseNull) {

				param.Param_Cat_StrName = SqlString.Null;
			}
			else if (!Cat_StrName.UseDefault) {

				param.Param_Cat_StrName = Cat_StrName.Value;
			}

			if (ConsiderNull_Cat_StrName == null || ConsiderNull_Cat_StrName.UseNull) {

				param.Param_ConsiderNull_Cat_StrName = SqlBoolean.Null;
			}
			else if (!ConsiderNull_Cat_StrName.UseDefault) {

				param.Param_ConsiderNull_Cat_StrName = ConsiderNull_Cat_StrName.Value;
			}

			using (SPs.spU_tblCategory sp = new SPs.spU_tblCategory(true)) {

				sp.Execute(ref param);
				param.Dispose();
			}
		}

	}
}
