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

using System.Windows.Forms;

namespace Bob.Windows.TreeNodeFactory {

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode based class.
	/// </summary>
	public sealed class StaticCustomTreeNode : System.Windows.Forms.TreeNode, ITreeNodeFactoryCustomTreeNode {

		private object contextOfUse = null;
		private object id = null;
		private string display = string.Empty;
		private bool isUpToDate = false;
		
		/// <summary>
		/// Creates a new instance of the StoredProcedureCustomTreeNode class.
		/// </summary>
		public StaticCustomTreeNode(object contextOfUse, object id, string display, bool addSubNode) {

			this.contextOfUse = contextOfUse;
			this.id = id;
			this.display = display;
			this.Text = display;

			if (addSubNode){

				this.Nodes.Add("Loading...");
			}
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
		/// This member overrides Object.ToString.
		/// </summary>
		public override string ToString() {

			return(this.display);
		}
	}
}
