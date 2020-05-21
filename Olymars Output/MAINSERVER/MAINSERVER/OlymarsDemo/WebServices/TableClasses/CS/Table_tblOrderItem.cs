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
	public class tblOrderItem_Record {

		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Oit_GuidID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Oit_GuidOrderID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSGuid col_Oit_GuidProductID = OlymarsDemo.WebServices.WSTypes.WSGuid.Null;
		private OlymarsDemo.WebServices.WSTypes.WSInt32 col_Oit_LngAmount = OlymarsDemo.WebServices.WSTypes.WSInt32.Null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrderItem_Record() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrderItem_Record(Guid col_Oit_GuidID, Guid col_Oit_GuidOrderID, Guid col_Oit_GuidProductID, Int32 col_Oit_LngAmount) {

			this.col_Oit_GuidID.Value = col_Oit_GuidID;
			this.col_Oit_GuidOrderID.Value = col_Oit_GuidOrderID;
			this.col_Oit_GuidProductID.Value = col_Oit_GuidProductID;
			this.col_Oit_LngAmount.Value = col_Oit_LngAmount;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Oit_GuidID {

			get {

				return(this.col_Oit_GuidID);
			}
			set {

				this.col_Oit_GuidID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Oit_GuidOrderID {

			get {

				return(this.col_Oit_GuidOrderID);
			}
			set {

				this.col_Oit_GuidOrderID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSGuid Col_Oit_GuidProductID {

			get {

				return(this.col_Oit_GuidProductID);
			}
			set {

				this.col_Oit_GuidProductID = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public OlymarsDemo.WebServices.WSTypes.WSInt32 Col_Oit_LngAmount {

			get {

				return(this.col_Oit_LngAmount);
			}
			set {

				this.col_Oit_LngAmount = value;
			}
		}

	}
}
