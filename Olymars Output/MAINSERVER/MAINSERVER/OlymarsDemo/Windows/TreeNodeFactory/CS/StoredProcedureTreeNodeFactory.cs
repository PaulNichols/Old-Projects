using System;
using OlymarsDemo.DataClasses;
using Params=OlymarsDemo.DataClasses.Parameters;
using SPs=OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.Windows {

	/// <summary>
	/// OlymarsDemo database tables enumeration
	/// </summary>
	public enum StoredProcedureList {

		/// <summary>None</summary>
		None,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblCategory table.</summary>
		StoredProcedure_spS_tblCategory,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblCustomer table.</summary>
		StoredProcedure_spS_tblCustomer,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrder table.</summary>
		StoredProcedure_spS_tblOrder,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrder table.</summary>
		StoredProcedure_spS_tblOrder_Full,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrder table.</summary>
		StoredProcedure_spS_tblOrder_SelectDisplay,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrderItem table.</summary>
		StoredProcedure_spS_tblOrderItem,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrderItem table.</summary>
		StoredProcedure_spS_tblOrderItem_Full,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblOrderItem table.</summary>
		StoredProcedure_spS_tblOrderItem_SelectDisplay,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblProduct table.</summary>
		StoredProcedure_spS_tblProduct,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblProduct table.</summary>
		StoredProcedure_spS_tblProduct_Full,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblProduct table.</summary>
		StoredProcedure_spS_tblProduct_SelectDisplay,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblSupplier table.</summary>
		StoredProcedure_spS_tblSupplier,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblSupplierProduct table.</summary>
		StoredProcedure_spS_tblSupplierProduct,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblSupplierProduct table.</summary>
		StoredProcedure_spS_tblSupplierProduct_Full,
		/// <summary>This stored procedure was automatically generated to retrieve records from the tblSupplierProduct table.</summary>
		StoredProcedure_spS_tblSupplierProduct_SelectDisplay,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xProductQuantityPerOrder&quot; stored procedure.</summary>
		StoredProcedure_spS_xProductQuantityPerOrder,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xQuantityOrderedPerProduct&quot; stored procedure.</summary>
		StoredProcedure_spS_xQuantityOrderedPerProduct,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xReadOrderDateAmount&quot; stored procedure.</summary>
		StoredProcedure_spS_xReadOrderDateAmount,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xReadOrderItems&quot; stored procedure.</summary>
		StoredProcedure_spS_xReadOrderItems,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xReadOrderLines&quot; stored procedure.</summary>
		StoredProcedure_spS_xReadOrderLines,
		/// <summary>Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;spS_xSearchCustomer&quot; stored procedure.</summary>
		StoredProcedure_spS_xSearchCustomer
	}

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode based class used by the TableTreeNodeFactory class.
	/// </summary>
	public sealed class StoredProcedureCustomTreeNode : System.Windows.Forms.TreeNode {

		private string contextOfUse = String.Empty;
		private object id = null;
		private string display = String.Empty;
		private StoredProcedureList storedProcedure;
		private bool isUpToDate = false;

		/// <summary>
		/// Creates a new instance of the StoredProcedureCustomTreeNode class.
		/// </summary>
		public StoredProcedureCustomTreeNode(string contextOfUse, object id, string display, StoredProcedureList storedProcedure) {

			this.contextOfUse = contextOfUse;
			this.id = id;
			this.display = display;
			this.storedProcedure = storedProcedure;
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

	/// <summary>
	/// This class is a System.Windows.Forms.TreeNode factory built for the OlymarsDemo stored procedures.
	/// </summary>
	public class StoredProcedureTreeNodeFactory {

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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cat_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCategory(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cat_LngID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCategory' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cat_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cat_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCategory(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cat_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblCategory param = new Params.spS_tblCategory(true);

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
			param.Param_Cat_LngID = param_Cat_LngID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblCategory sp = new SPs.spS_tblCategory(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblCategory", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblCategory);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Cus_LngID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Cus_LngID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Cus_LngID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Cus_LngID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Cus_LngID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblCustomer param = new Params.spS_tblCustomer(true);

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
			param.Param_Cus_LngID = param_Cus_LngID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblCustomer sp = new SPs.spS_tblCustomer(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblCustomer", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblCustomer);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrder param = new Params.spS_tblOrder(true);

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
			param.Param_Ord_GuidID = param_Ord_GuidID;
			param.Param_Ord_LngCustomerID = param_Ord_LngCustomerID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrder sp = new SPs.spS_tblOrder(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrder", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrder);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrder_Full param = new Params.spS_tblOrder_Full(true);

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
			param.Param_Ord_GuidID = param_Ord_GuidID;
			param.Param_Ord_LngCustomerID = param_Ord_LngCustomerID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrder_Full sp = new SPs.spS_tblOrder_Full(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrder_Full", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrder_Full);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrder_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_GuidID, param_Ord_LngCustomerID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrder_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_GuidID&quot; column.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Ord_LngCustomerID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrder_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Ord_GuidID, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrder_SelectDisplay param = new Params.spS_tblOrder_SelectDisplay(true);

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
			param.Param_Ord_GuidID = param_Ord_GuidID;
			param.Param_Ord_LngCustomerID = param_Ord_LngCustomerID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrder_SelectDisplay sp = new SPs.spS_tblOrder_SelectDisplay(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrder_SelectDisplay", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrder_SelectDisplay);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrderItem param = new Params.spS_tblOrderItem(true);

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
			param.Param_Oit_GuidID = param_Oit_GuidID;
			param.Param_Oit_GuidOrderID = param_Oit_GuidOrderID;
			param.Param_Oit_GuidProductID = param_Oit_GuidProductID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrderItem sp = new SPs.spS_tblOrderItem(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrderItem", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrderItem);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrderItem_Full param = new Params.spS_tblOrderItem_Full(true);

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
			param.Param_Oit_GuidID = param_Oit_GuidID;
			param.Param_Oit_GuidOrderID = param_Oit_GuidOrderID;
			param.Param_Oit_GuidProductID = param_Oit_GuidProductID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrderItem_Full sp = new SPs.spS_tblOrderItem_Full(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrderItem_Full", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrderItem_Full);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblOrderItem_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Oit_GuidID, param_Oit_GuidOrderID, param_Oit_GuidProductID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblOrderItem_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Oit_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidID&quot; column.</param>
		/// <param name="param_Oit_GuidOrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidOrderID&quot; column.</param>
		/// <param name="param_Oit_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Oit_GuidProductID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblOrderItem_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Oit_GuidID, System.Data.SqlTypes.SqlGuid param_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid param_Oit_GuidProductID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblOrderItem_SelectDisplay param = new Params.spS_tblOrderItem_SelectDisplay(true);

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
			param.Param_Oit_GuidID = param_Oit_GuidID;
			param.Param_Oit_GuidOrderID = param_Oit_GuidOrderID;
			param.Param_Oit_GuidProductID = param_Oit_GuidProductID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblOrderItem_SelectDisplay sp = new SPs.spS_tblOrderItem_SelectDisplay(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblOrderItem_SelectDisplay", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblOrderItem_SelectDisplay);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblProduct param = new Params.spS_tblProduct(true);

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
			param.Param_Pro_GuidID = param_Pro_GuidID;
			param.Param_Pro_LngCategoryID = param_Pro_LngCategoryID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblProduct sp = new SPs.spS_tblProduct(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblProduct", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblProduct);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblProduct_Full param = new Params.spS_tblProduct_Full(true);

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
			param.Param_Pro_GuidID = param_Pro_GuidID;
			param.Param_Pro_LngCategoryID = param_Pro_LngCategoryID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblProduct_Full sp = new SPs.spS_tblProduct_Full(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblProduct_Full", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblProduct_Full);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Pro_GuidID, param_Pro_LngCategoryID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Pro_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_GuidID&quot; column.</param>
		/// <param name="param_Pro_LngCategoryID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Pro_LngCategoryID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Pro_GuidID, System.Data.SqlTypes.SqlInt32 param_Pro_LngCategoryID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblProduct_SelectDisplay param = new Params.spS_tblProduct_SelectDisplay(true);

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
			param.Param_Pro_GuidID = param_Pro_GuidID;
			param.Param_Pro_LngCategoryID = param_Pro_LngCategoryID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblProduct_SelectDisplay sp = new SPs.spS_tblProduct_SelectDisplay(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblProduct_SelectDisplay", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblProduct_SelectDisplay);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Sup_GuidID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplier(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Sup_GuidID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplier' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Sup_GuidID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Sup_GuidID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplier(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Sup_GuidID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblSupplier param = new Params.spS_tblSupplier(true);

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
			param.Param_Sup_GuidID = param_Sup_GuidID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblSupplier sp = new SPs.spS_tblSupplier(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblSupplier", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblSupplier);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblSupplierProduct param = new Params.spS_tblSupplierProduct(true);

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
			param.Param_Spr_GuidProductID = param_Spr_GuidProductID;
			param.Param_Spr_GuidSupplierID = param_Spr_GuidSupplierID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblSupplierProduct sp = new SPs.spS_tblSupplierProduct(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblSupplierProduct", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblSupplierProduct);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_Full(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_Full' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_Full(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblSupplierProduct_Full param = new Params.spS_tblSupplierProduct_Full(true);

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
			param.Param_Spr_GuidProductID = param_Spr_GuidProductID;
			param.Param_Spr_GuidSupplierID = param_Spr_GuidSupplierID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblSupplierProduct_Full sp = new SPs.spS_tblSupplierProduct_Full(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblSupplierProduct_Full", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblSupplierProduct_Full);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_tblSupplierProduct_SelectDisplay(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Spr_GuidProductID, param_Spr_GuidSupplierID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_tblSupplierProduct_SelectDisplay' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Spr_GuidProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidProductID&quot; column.</param>
		/// <param name="param_Spr_GuidSupplierID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;Spr_GuidSupplierID&quot; column.</param>
		/// <param name="param_ReturnXML">False if you want the data back in a resultset. True if you want the data back in an XML stream.</param>
		public static void Fill_spS_tblSupplierProduct_SelectDisplay(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_Spr_GuidProductID, System.Data.SqlTypes.SqlGuid param_Spr_GuidSupplierID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_tblSupplierProduct_SelectDisplay param = new Params.spS_tblSupplierProduct_SelectDisplay(true);

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
			param.Param_Spr_GuidProductID = param_Spr_GuidProductID;
			param.Param_Spr_GuidSupplierID = param_Spr_GuidSupplierID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_tblSupplierProduct_SelectDisplay sp = new SPs.spS_tblSupplierProduct_SelectDisplay(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_tblSupplierProduct_SelectDisplay", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_tblSupplierProduct_SelectDisplay);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xProductQuantityPerOrder(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xProductQuantityPerOrder' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xProductQuantityPerOrder(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

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

			Params.spS_xProductQuantityPerOrder param = new Params.spS_xProductQuantityPerOrder(true);

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
			param.Param_ProductID = param_ProductID;

			using (SPs.spS_xProductQuantityPerOrder sp = new SPs.spS_xProductQuantityPerOrder(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xProductQuantityPerOrder", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xProductQuantityPerOrder);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_ProductID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_ProductID) {

			Fill_spS_xQuantityOrderedPerProduct(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_ProductID);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xQuantityOrderedPerProduct' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_ProductID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ProductID&quot; parameter.</param>
		public static void Fill_spS_xQuantityOrderedPerProduct(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_ProductID) {

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

			Params.spS_xQuantityOrderedPerProduct param = new Params.spS_xQuantityOrderedPerProduct(true);

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
			param.Param_ProductID = param_ProductID;

			using (SPs.spS_xQuantityOrderedPerProduct sp = new SPs.spS_xQuantityOrderedPerProduct(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xQuantityOrderedPerProduct", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xQuantityOrderedPerProduct);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_Ord_LngCustomerID, param_ReturnXML);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

			Fill_spS_xReadOrderDateAmount(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_Ord_LngCustomerID, param_ReturnXML);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderDateAmount' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_Ord_LngCustomerID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@Ord_LngCustomerID&quot; parameter.</param>
		/// <param name="param_ReturnXML">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@ReturnXML&quot; parameter.</param>
		public static void Fill_spS_xReadOrderDateAmount(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlInt32 param_Ord_LngCustomerID, System.Data.SqlTypes.SqlBoolean param_ReturnXML) {

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

			Params.spS_xReadOrderDateAmount param = new Params.spS_xReadOrderDateAmount(true);

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
			param.Param_Ord_LngCustomerID = param_Ord_LngCustomerID;
			param.Param_ReturnXML = param_ReturnXML;

			using (SPs.spS_xReadOrderDateAmount sp = new SPs.spS_xReadOrderDateAmount(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xReadOrderDateAmount", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xReadOrderDateAmount);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderItems(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderItems' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderItems(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

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

			Params.spS_xReadOrderItems param = new Params.spS_xReadOrderItems(true);

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
			param.Param_OrderID = param_OrderID;

			using (SPs.spS_xReadOrderItems sp = new SPs.spS_xReadOrderItems(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xReadOrderItems", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xReadOrderItems);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_OrderID);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlGuid param_OrderID) {

			Fill_spS_xReadOrderLines(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_OrderID);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xReadOrderLines' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_OrderID">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@OrderID&quot; parameter.</param>
		public static void Fill_spS_xReadOrderLines(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlGuid param_OrderID) {

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

			Params.spS_xReadOrderLines param = new Params.spS_xReadOrderLines(true);

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
			param.Param_OrderID = param_OrderID;

			using (SPs.spS_xReadOrderLines sp = new SPs.spS_xReadOrderLines(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xReadOrderLines", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xReadOrderLines);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, imageIndex, selectedImageIndex, 30, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, addSubNode, 0, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, bool purgeTreeNodeChildren, int valueMemberIndex, int displayMemberIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, purgeTreeNodeChildren, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, commandTimeOut, param_LastName, param_FirstName);
		}

		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			Fill_spS_xSearchCustomer(nodes, contextOfUse, true, false, resultsetIndex, valueMemberIndex, displayMemberIndex, -1, -1, 30, param_LastName, param_FirstName);
		}


		/// <summary>
		/// Populates the supplied TreeNodeCollection with one of the 'spS_xSearchCustomer' stored procedure returned resultset.
		/// </summary>
		/// <param name="nodes">System.Windows.Forms.TreeNodeCollection to add the TreeNodes to.</param>
		/// <param name="contextOfUse">Context of your use of the TreeNode objects that are going to be added as children of the supplied TreeNode collection.</param>
		/// <param name="purgeTreeNodeChildren">True if you want to purge the System.Windows.Forms.TreeNodeCollection before.</param>
		/// <param name="addSubNode">True if you want to add a sub treenode to the nodes.</param>
		/// <param name="resultsetIndex">Index of the resultset that has to be use.</param>
		/// <param name="valueMemberIndex">Index of the field that has to be use as a value member.</param>
		/// <param name="displayMemberIndex">Index of the field that has to be use as a display member.</param>
		/// <param name="imageIndex">The image list index value of the image displayed when the tree node is in the unselected state.</param>
		/// <param name="selectedImageIndex">The image list index value of the image that is displayed when the tree node is in the selected state.</param>
		/// <param name="commandTimeOut">The time-out (in seconds) to be use by the ADO command object.</param>
		/// <param name="param_LastName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@LastName&quot; parameter.</param>
		/// <param name="param_FirstName">Update this description in the &quot;Olymars/Description&quot; extended property of the &quot;@FirstName&quot; parameter.</param>
		public static void Fill_spS_xSearchCustomer(System.Windows.Forms.TreeNodeCollection nodes, string contextOfUse, bool purgeTreeNodeChildren, bool addSubNode, short resultsetIndex, int valueMemberIndex, int displayMemberIndex, int imageIndex, int selectedImageIndex, int commandTimeOut, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

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

			Params.spS_xSearchCustomer param = new Params.spS_xSearchCustomer(true);

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
			param.Param_LastName = param_LastName;
			param.Param_FirstName = param_FirstName;

			using (SPs.spS_xSearchCustomer sp = new SPs.spS_xSearchCustomer(true)) {

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;

				try {

					sp.Execute(ref param, out sqlDataReader);

					int resultsetNumber = 0;
					while (resultsetNumber != resultsetIndex) {

						if (!sqlDataReader.NextResult()) {

							throw new InvalidOperationException(String.Format("The stored procedure {0} had returned only {1} resultset(s). Unable to use resultset #{2}", "spS_xSearchCustomer", resultsetNumber + 1, resultsetIndex));
						}
						resultsetNumber ++;
					}

					if (treeView != null) {

						treeView.BeginUpdate();
					}
					while (sqlDataReader.Read()) {

						OlymarsDemo.Windows.StoredProcedureCustomTreeNode storedProcedureCustomTreeNode = new OlymarsDemo.Windows.StoredProcedureCustomTreeNode(contextOfUse, sqlDataReader.GetValue(valueMemberIndex), sqlDataReader.GetString(displayMemberIndex), StoredProcedureList.StoredProcedure_spS_xSearchCustomer);
						if (addSubNode) {

							storedProcedureCustomTreeNode.Nodes.Add("Loading...");
						}

						if (imageIndex > -1) {

							storedProcedureCustomTreeNode.ImageIndex = imageIndex;
						}

						if (selectedImageIndex > -1) {

							storedProcedureCustomTreeNode.SelectedImageIndex = selectedImageIndex;
						}

						nodes.Add(storedProcedureCustomTreeNode);
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
