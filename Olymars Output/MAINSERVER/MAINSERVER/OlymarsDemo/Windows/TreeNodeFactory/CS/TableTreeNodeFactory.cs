using System;
using OlymarsDemo.DataClasses;
using Params=OlymarsDemo.DataClasses.Parameters;
using SPs=OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.Windows {

	/// <summary>
	/// OlymarsDemo database tables enumeration
	/// </summary>
	public enum TableList {

		/// <summary>None</summary>
		None,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;tblCategory&quot; table.</summary>
		Table_tblCategory,
		/// <summary>I edited the customer table description</summary>
		Table_tblCustomer,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;tblOrder&quot; table.</summary>
		Table_tblOrder,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;tblOrderItem&quot; table.</summary>
		Table_tblOrderItem,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;tblProduct&quot; table.</summary>
		Table_tblProduct,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;tblSupplier&quot; table.</summary>
		Table_tblSupplier
	}

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode based class used by the TableTreeNodeFactory class.
	/// </summary>
	public sealed class TableCustomTreeNode : System.Windows.Forms.TreeNode {

		private string contextOfUse = String.Empty;
		private object id = null;
		private string display = String.Empty;
		private TableList table;
		private bool isUpToDate = false;

		/// <summary>
		/// Creates a new instance of the TableCustomTreeNode class.
		/// </summary>
		public TableCustomTreeNode(string contextOfUse, object id, string display, TableList table) {

			this.contextOfUse= contextOfUse;
			this.id = id;
			this.display = display;
			this.table = table;
			this.Text = display;
		}

		/// <summary>
		/// Returns the context of use of this TreeNode.
		/// </summary>
		public string ContextOfUse {

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
		/// Returns the table from which the record was loaded.
		/// </summary>
		public TableList Table {

			get {

				return(this.table);
			}
		}

		/// <summary>
		/// This member overrides Object.ToString.
		/// </summary>
		public override string ToString() {

			return(this.display);
		}

		/// <summary>
		/// Refreshes the display used for this record.
		/// </summary>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the current node sub-nodes collection before.</param>
		public void RefreshDisplay(bool purgeTreeNodeChildren) {

			bool sqlConnectionAlreadyOpened = false;
			System.Data.SqlClient.SqlDataReader sqlDataReader = null;

			if (purgeTreeNodeChildren) {

				base.Nodes.Clear();
			}

			this.Text = "Refreshing...";

			switch (this.table) {

				case TableList.Table_tblCategory:

					Params.spS_tblCategory_Display param_Table_tblCategory = new Params.spS_tblCategory_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblCategory.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblCategory.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblCategory.Param_Cat_LngID = (System.Data.SqlTypes.SqlInt32)this.Id;

					using (SPs.spS_tblCategory_Display sp_Table_tblCategory = new SPs.spS_tblCategory_Display(true)) {

						try {

							sp_Table_tblCategory.Execute(ref param_Table_tblCategory, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblCategory", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblCategory.Connection.Close();
							}
						}
					}

					param_Table_tblCategory.Dispose();
					break;

				case TableList.Table_tblCustomer:

					Params.spS_tblCustomer_Display param_Table_tblCustomer = new Params.spS_tblCustomer_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblCustomer.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblCustomer.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblCustomer.Param_Cus_LngID = (System.Data.SqlTypes.SqlInt32)this.Id;

					using (SPs.spS_tblCustomer_Display sp_Table_tblCustomer = new SPs.spS_tblCustomer_Display(true)) {

						try {

							sp_Table_tblCustomer.Execute(ref param_Table_tblCustomer, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblCustomer", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblCustomer.Connection.Close();
							}
						}
					}

					param_Table_tblCustomer.Dispose();
					break;

				case TableList.Table_tblOrder:

					Params.spS_tblOrder_Display param_Table_tblOrder = new Params.spS_tblOrder_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblOrder.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblOrder.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblOrder.Param_Ord_GuidID = (System.Data.SqlTypes.SqlGuid)this.Id;

					using (SPs.spS_tblOrder_Display sp_Table_tblOrder = new SPs.spS_tblOrder_Display(true)) {

						try {

							sp_Table_tblOrder.Execute(ref param_Table_tblOrder, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblOrder", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblOrder.Connection.Close();
							}
						}
					}

					param_Table_tblOrder.Dispose();
					break;

				case TableList.Table_tblOrderItem:

					Params.spS_tblOrderItem_Display param_Table_tblOrderItem = new Params.spS_tblOrderItem_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblOrderItem.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblOrderItem.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblOrderItem.Param_Oit_GuidID = (System.Data.SqlTypes.SqlGuid)this.Id;

					using (SPs.spS_tblOrderItem_Display sp_Table_tblOrderItem = new SPs.spS_tblOrderItem_Display(true)) {

						try {

							sp_Table_tblOrderItem.Execute(ref param_Table_tblOrderItem, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblOrderItem", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblOrderItem.Connection.Close();
							}
						}
					}

					param_Table_tblOrderItem.Dispose();
					break;

				case TableList.Table_tblProduct:

					Params.spS_tblProduct_Display param_Table_tblProduct = new Params.spS_tblProduct_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblProduct.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblProduct.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblProduct.Param_Pro_GuidID = (System.Data.SqlTypes.SqlGuid)this.Id;

					using (SPs.spS_tblProduct_Display sp_Table_tblProduct = new SPs.spS_tblProduct_Display(true)) {

						try {

							sp_Table_tblProduct.Execute(ref param_Table_tblProduct, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblProduct", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblProduct.Connection.Close();
							}
						}
					}

					param_Table_tblProduct.Dispose();
					break;

				case TableList.Table_tblSupplier:

					Params.spS_tblSupplier_Display param_Table_tblSupplier = new Params.spS_tblSupplier_Display(true);

					switch (TableTreeNodeFactory.lastKnownConnectionType) {

						case ConnectionType.ConnectionString:
							param_Table_tblSupplier.SetUpConnection(TableTreeNodeFactory._connectionString);
							break;

						case ConnectionType.SqlConnection:
							param_Table_tblSupplier.SetUpConnection(TableTreeNodeFactory._sqlConnection);
							sqlConnectionAlreadyOpened = (TableTreeNodeFactory._sqlConnection.State == System.Data.ConnectionState.Open);
							break;
					}

					param_Table_tblSupplier.Param_Sup_GuidID = (System.Data.SqlTypes.SqlGuid)this.Id;

					using (SPs.spS_tblSupplier_Display sp_Table_tblSupplier = new SPs.spS_tblSupplier_Display(true)) {

						try {

							sp_Table_tblSupplier.Execute(ref param_Table_tblSupplier, out sqlDataReader);

							if (sqlDataReader.Read()) {

								this.display = sqlDataReader.GetString(1);
							}
							else {

								throw new InvalidOperationException(String.Format("Table {0}: record {1} does not exist anymore in the database.", "tblSupplier", this.Id));
							}
						}
						catch {

							throw;
						}

						finally {

							if (sqlDataReader != null && !sqlDataReader.IsClosed) {

								sqlDataReader.Close();
							}

							if (!sqlConnectionAlreadyOpened) {

								sp_Table_tblSupplier.Connection.Close();
							}
						}
					}

					param_Table_tblSupplier.Dispose();
					break;

			}

			this.Text = this.Display;
		}
	}

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode factory built for the OlymarsDemo tables.
	/// </summary>
	public class TableTreeNodeFactory {

		internal static string _connectionString = String.Empty;
		internal static System.Data.SqlClient.SqlConnection _sqlConnection = null;
		internal static ConnectionType lastKnownConnectionType = ConnectionType.None;

		/// <summary>
		/// Sets the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public static void SetUpConnection(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			_connectionString = connectionString;
			lastKnownConnectionType = ConnectionType.ConnectionString;
		}

		/// <summary>
		/// Sets the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		public static void SetUpConnection(System.Data.SqlClient.SqlConnection sqlConnection) {

			if (sqlConnection == null) {

				throw new ArgumentNullException("sqlConnection", "sqlConnection can not be null.");
			}

			_sqlConnection = sqlConnection;
			lastKnownConnectionType = ConnectionType.SqlConnection;
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString, SqlConnection
		/// </summary>
		public static ConnectionType ConnectionType {

			get {

				return(lastKnownConnectionType );
			}
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse) {

			Fill_tblCategory(nodes, contextOfUse, true, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex) {

			Fill_tblCategory(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut) {

			Fill_tblCategory(nodes, contextOfUse, true, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblCategory(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut) {

			Fill_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCategory' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblCategory_Display param = new Params.spS_tblCategory_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;

			using (SPs.spS_tblCategory_Display sp = new SPs.spS_tblCategory_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlInt32(0), sqlDataReader.GetString(1), TableList.Table_tblCategory);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse) {

			Fill_tblCustomer(nodes, contextOfUse, true, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex) {

			Fill_tblCustomer(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut) {

			Fill_tblCustomer(nodes, contextOfUse, true, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblCustomer(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut) {

			Fill_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblCustomer' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblCustomer_Display param = new Params.spS_tblCustomer_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;

			using (SPs.spS_tblCustomer_Display sp = new SPs.spS_tblCustomer_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlInt32(0), sqlDataReader.GetString(1), TableList.Table_tblCustomer);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, true, false, -1, -1, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, true, false, -1, -1, commandTimeOut, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			Fill_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut, col_Ord_LngCustomerID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrder' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		public static void Fill_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Ord_LngCustomerID) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblOrder_Display param = new Params.spS_tblOrder_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;
			param.Param_Ord_LngCustomerID = col_Ord_LngCustomerID;

			using (SPs.spS_tblOrder_Display sp = new SPs.spS_tblOrder_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlGuid(0), sqlDataReader.GetString(1), TableList.Table_tblOrder);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, true, false, -1, -1, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, true, false, -1, -1, commandTimeOut, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			Fill_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut, col_Oit_GuidOrderID, col_Oit_GuidProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblOrderItem' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="col_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		public static void Fill_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblOrderItem_Display param = new Params.spS_tblOrderItem_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;
			param.Param_Oit_GuidOrderID = col_Oit_GuidOrderID;
			param.Param_Oit_GuidProductID = col_Oit_GuidProductID;

			using (SPs.spS_tblOrderItem_Display sp = new SPs.spS_tblOrderItem_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlGuid(0), sqlDataReader.GetString(1), TableList.Table_tblOrderItem);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, true, false, -1, -1, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, true, false, -1, -1, commandTimeOut, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			Fill_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut, col_Pro_LngCategoryID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblProduct' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="col_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		public static void Fill_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 col_Pro_LngCategoryID) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblProduct_Display param = new Params.spS_tblProduct_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;
			param.Param_Pro_LngCategoryID = col_Pro_LngCategoryID;

			using (SPs.spS_tblProduct_Display sp = new SPs.spS_tblProduct_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlGuid(0), sqlDataReader.GetString(1), TableList.Table_tblProduct);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse) {

			Fill_tblSupplier(nodes, contextOfUse, true, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex) {

			Fill_tblSupplier(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int commandTimeOut) {

			Fill_tblSupplier(nodes, contextOfUse, true, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblSupplier(nodes, contextOfUse, true, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int commandTimeOut) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, false, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, false, imageIndex, selectedImageIndex, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, imageIndex, selectedImageIndex, 30);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int commandTimeOut) {

			Fill_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, -1, -1, commandTimeOut);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with the 'tblSupplier' table content.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		public static void Fill_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int imageIndex, int selectedImageIndex, int commandTimeOut) {

			if (lastKnownConnectionType == ConnectionType.None) {

				throw new InvalidOperationException("You must specify the type of connection (SetUpConnection) to use before calling this method.");
			}

			bool sqlConnectionAlreadyOpened = false;
			System.Windows.Forms.TreeView treeView = null;

			if (nodes.Count == 0) {

				treeView = nodes[nodes.Add(new System.Windows.Forms.TreeNode())].TreeView;
				nodes.Clear();
			}
			else {

				treeView = nodes[0].TreeView;
			}

			if (purgeTreeNodeChildren) {

				nodes.Clear();
			}

			Params.spS_tblSupplier_Display param = new Params.spS_tblSupplier_Display(true);

			switch (lastKnownConnectionType) {

				case ConnectionType.ConnectionString:
					param.SetUpConnection(_connectionString);
					break;

				case ConnectionType.SqlConnection:
					param.SetUpConnection(_sqlConnection);
					sqlConnectionAlreadyOpened = (_sqlConnection.State == System.Data.ConnectionState.Open);
					break;
			}

			param.CommandTimeOut = commandTimeOut;

			using (SPs.spS_tblSupplier_Display sp = new SPs.spS_tblSupplier_Display(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.TableCustomTreeNode tableCustomTreeNode = new OlymarsDemo.Windows.TableCustomTreeNode(contextOfUse, sqlDataReader.GetSqlGuid(0), sqlDataReader.GetString(1), TableList.Table_tblSupplier);
						if (addSubNode) {

							tableCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							tableCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							tableCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(tableCustomTreeNode);
					}
					if (treeView != null) {

						treeView.EndUpdate();
					}
				}

				catch {

					throw;
				}

				finally {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					if (!sqlConnectionAlreadyOpened) {

						sp.Connection.Close();
					}
				}
			}

			param.Dispose();
		}
	}
}
