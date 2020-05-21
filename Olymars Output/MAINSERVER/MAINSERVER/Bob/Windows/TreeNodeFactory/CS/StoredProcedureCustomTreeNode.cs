/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:24
			Generator name: MAINSERVER\Administrator
			Template last update: 15/01/2005 18:37:20
			Template revision: 417

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

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
using Bob.DataClasses;
using Params=Bob.DataClasses.Parameters;
using SPs=Bob.DataClasses.StoredProcedures;

namespace Bob.Windows.TreeNodeFactory {

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode based class used by the StoredProcedureTreeNodeFactory class.
	/// </summary>
	public sealed class StoredProcedureCustomTreeNode : System.Windows.Forms.TreeNode, ITreeNodeFactoryCustomTreeNode {

		private object contextOfUse = null;
		internal object id = null;
		internal string display = string.Empty;
		private StoredProcedureList storedProcedure;
		private bool isUpToDate = false;
		private object[] columns = null;
		
		/// <summary>
		/// Gets an array representing all the columns
		/// </summary>
		public object[] Columns {
		
			get {
			
				return(this.columns);
			}
		}
		
		internal void InitColumns(int count) {
		
			this.columns = new object[count];
		}

		/// <summary>
		/// Creates a new instance of the StoredProcedureCustomTreeNode class.
		/// </summary>
		public StoredProcedureCustomTreeNode(object contextOfUse, StoredProcedureList storedProcedure) {

			this.contextOfUse = contextOfUse;
			this.storedProcedure = storedProcedure;
		}

		/// <summary>
		/// Returns the context of use of this TreeNode.
		/// </summary>
		public object ContextOfUse {

			get {

				return(this.contextOfUse);
			}
		}

		/// <summary>
		/// Returns True if the children nodes are loaded.
		/// </summary>
		public bool IsUpToDate {

			get {

				return(this.isUpToDate);
			}

			set {

				this.isUpToDate = value;
			}
		}

		/// <summary>
		/// Returns the primary key of the record.
		/// </summary>
		public object Id {

			get {

				return(this.id);
			}
		}

		/// <summary>
		/// Returns the Display of the record.
		/// </summary>
		public string Display {

			get {

				return(this.display);
			}
		}

		/// <summary>
		/// Returns the stored procedure from which the record was loaded.
		/// </summary>
		public StoredProcedureList StoredProcedure {

			get {

				return(this.storedProcedure);
			}
		}

		/// <summary>
		/// This member overrides Object.ToString.
		/// </summary>
		public override string ToString() {

			return(this.display);
		}
	}
}
