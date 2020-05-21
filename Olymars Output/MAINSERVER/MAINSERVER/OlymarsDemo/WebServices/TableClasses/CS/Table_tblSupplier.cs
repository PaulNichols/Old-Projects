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

using System;

namespace OlymarsDemo.WebServices.Tables {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class Supplier {

		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Sup_GuidID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSString col_Sup_StrName = OlymarsDemo.WebServices.WSTypes.WSString.Null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Supplier() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Supplier(Guid col_Sup_GuidID, String col_Sup_StrName) {

			this.col_Sup_GuidID.Value = col_Sup_GuidID;
			this.col_Sup_StrName.Value = col_Sup_StrName;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Sup_GuidID {

			get {

				return(this.col_Sup_GuidID);
			}
			set {

				this.col_Sup_GuidID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSString Col_Sup_StrName {

			get {

				return(this.col_Sup_StrName);
			}
			set {

				this.col_Sup_StrName = value;
			}
		}

	}
}
