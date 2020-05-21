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

using System;

namespace OlymarsDemo.WebServices.Common {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
	public class Item {

		private object id;
		private string display = string.Empty;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Item() {

		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Item(object id, string display) {

			this.id = id;
			this.display = display;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public object Id {

			get {

				return(this.id);
			}
			set {

				this.id = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public string Display {

			get {

				return(this.display);
			}
			set {

				this.display = value;
			}
		}
	}
}
