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
	public class tblCustomer_Record {

		private OlymarsDemo.WebServices.WSTypes.WSInt32 col_Cus_LngID = OlymarsDemo.WebServices.WSTypes.WSInt32.Null;
		private OlymarsDemo.WebServices.WSTypes.WSString customer_Surname = OlymarsDemo.WebServices.WSTypes.WSString.Null;
		private OlymarsDemo.WebServices.WSTypes.WSString col_Cus_StrFirstName = OlymarsDemo.WebServices.WSTypes.WSString.Null;
		private OlymarsDemo.WebServices.WSTypes.WSString col_Cus_StrEmail = OlymarsDemo.WebServices.WSTypes.WSString.Null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblCustomer_Record() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblCustomer_Record(Int32 col_Cus_LngID, String customer_Surname, String col_Cus_StrFirstName, String col_Cus_StrEmail) {

			this.col_Cus_LngID.Value = col_Cus_LngID;
			this.customer_Surname.Value = customer_Surname;
			this.col_Cus_StrFirstName.Value = col_Cus_StrFirstName;
			this.col_Cus_StrEmail.Value = col_Cus_StrEmail;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSInt32 Col_Cus_LngID {

			get {

				return(this.col_Cus_LngID);
			}
			set {

				this.col_Cus_LngID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSString Customer_Surname {

			get {

				return(this.customer_Surname);
			}
			set {

				this.customer_Surname = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSString Col_Cus_StrFirstName {

			get {

				return(this.col_Cus_StrFirstName);
			}
			set {

				this.col_Cus_StrFirstName = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSString Col_Cus_StrEmail {

			get {

				return(this.col_Cus_StrEmail);
			}
			set {

				this.col_Cus_StrEmail = value;
			}
		}

	}
}
