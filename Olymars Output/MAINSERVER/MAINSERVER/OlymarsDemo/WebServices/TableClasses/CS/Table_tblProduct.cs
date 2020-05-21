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
	public class Product {

		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Pro_GuidID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSString name = OlymarsDemo.WebServices.WSTypes.WSString.Null;
		private OlymarsDemo.WebServices.WSTypes.WSMoney col_Pro_CurPrice = OlymarsDemo.WebServices.WSTypes.WSMoney.Null;
		private OlymarsDemo.WebServices.WSTypes.WSInt32 categoryID = OlymarsDemo.WebServices.WSTypes.WSInt32.Null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Product() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Product(Guid col_Pro_GuidID, String name, Decimal col_Pro_CurPrice, Int32 categoryID) {

			this.col_Pro_GuidID.Value = col_Pro_GuidID;
			this.name.Value = name;
			this.col_Pro_CurPrice.Value = col_Pro_CurPrice;
			this.categoryID.Value = categoryID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Pro_GuidID {

			get {

				return(this.col_Pro_GuidID);
			}
			set {

				this.col_Pro_GuidID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSString Name {

			get {

				return(this.name);
			}
			set {

				this.name = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSMoney Col_Pro_CurPrice {

			get {

				return(this.col_Pro_CurPrice);
			}
			set {

				this.col_Pro_CurPrice = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSInt32 CategoryID {

			get {

				return(this.categoryID);
			}
			set {

				this.categoryID = value;
			}
		}

	}
}
