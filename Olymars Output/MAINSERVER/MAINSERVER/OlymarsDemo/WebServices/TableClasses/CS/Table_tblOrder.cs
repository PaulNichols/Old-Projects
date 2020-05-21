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
	public class tblOrder_Record {

		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Ord_GuidID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSDateTime col_Ord_DatOrderedOn = OlymarsDemo.WebServices.WSTypes.WSDateTime.Null;
		private OlymarsDemo.WebServices.WSTypes.WSInt32 col_Ord_LngCustomerID = OlymarsDemo.WebServices.WSTypes.WSInt32.Null;
		private OlymarsDemo.WebServices.WSTypes.WSMoney col_Ord_CurTotal = OlymarsDemo.WebServices.WSTypes.WSMoney.Null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrder_Record() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrder_Record(Guid col_Ord_GuidID, DateTime col_Ord_DatOrderedOn, Int32 col_Ord_LngCustomerID, Decimal col_Ord_CurTotal) {

			this.col_Ord_GuidID.Value = col_Ord_GuidID;
			this.col_Ord_DatOrderedOn.Value = col_Ord_DatOrderedOn;
			this.col_Ord_LngCustomerID.Value = col_Ord_LngCustomerID;
			this.col_Ord_CurTotal.Value = col_Ord_CurTotal;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Ord_GuidID {

			get {

				return(this.col_Ord_GuidID);
			}
			set {

				this.col_Ord_GuidID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSDateTime Col_Ord_DatOrderedOn {

			get {

				return(this.col_Ord_DatOrderedOn);
			}
			set {

				this.col_Ord_DatOrderedOn = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSInt32 Col_Ord_LngCustomerID {

			get {

				return(this.col_Ord_LngCustomerID);
			}
			set {

				this.col_Ord_LngCustomerID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSMoney Col_Ord_CurTotal {

			get {

				return(this.col_Ord_CurTotal);
			}
			set {

				this.col_Ord_CurTotal = value;
			}
		}

	}
}
