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
	/// Defines the properties implemented by both TableCustomTreeNode and StoredProcedureCustomTreeNode
	/// </summary>
	public interface ITreeNodeFactoryCustomTreeNode {

		/// <summary>
		/// Returns the context of use of this TreeNode.
		/// </summary>
		object ContextOfUse { get; }
		/// <summary>
		/// Returns True if the children nodes are loaded.
		/// </summary>
		bool IsUpToDate { get; set; }
		/// <summary>
		/// Returns the primary key of the record.
		/// </summary>
		object Id { get; }
		/// <summary>
		/// Returns the Display of the record.
		/// </summary>
		string Display { get; }
		/// <summary>
		/// Gets the collection of TreeNode objects assigned to the current tree node.
		/// </summary>
		System.Windows.Forms.TreeNodeCollection Nodes { get; }
	}
}
