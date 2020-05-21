/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 27/12/2004 16:08:33
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

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
using System.Windows.Forms;
using OlymarsDemo.DataClasses;
using OlymarsDemo.Windows.Forms;
using OlymarsDemo.Windows.ListBoxes;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using Params = OlymarsDemo.DataClasses.Parameters;

namespace OlymarsDemo.Exe {

	/// <summary>
	/// Summary description for WinForm_Main.
	/// </summary>
	public class WinForm_Main : System.Windows.Forms.Form {

		private System.Windows.Forms.ComboBox comDatabaseTables;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabtblCategory;
		private System.Windows.Forms.Button cmdNewtblCategory;
		private System.Windows.Forms.Button cmdEdittblCategory;
		private System.Windows.Forms.Button cmdDeletetblCategory;
		private System.Windows.Forms.Button cmdRefreshtblCategory;
		private WinListBox_tblCategory lstWinListBox_tblCategory;
		private System.Windows.Forms.TabPage tabtblCustomer;
		private System.Windows.Forms.Button cmdNewtblCustomer;
		private System.Windows.Forms.Button cmdEdittblCustomer;
		private System.Windows.Forms.Button cmdDeletetblCustomer;
		private System.Windows.Forms.Button cmdRefreshtblCustomer;
		private WinListBox_tblCustomer lstWinListBox_tblCustomer;
		private System.Windows.Forms.TabPage tabtblOrder;
		private System.Windows.Forms.Button cmdNewtblOrder;
		private System.Windows.Forms.Button cmdEdittblOrder;
		private System.Windows.Forms.Button cmdDeletetblOrder;
		private System.Windows.Forms.Button cmdRefreshtblOrder;
		private WinListBox_tblOrder lstWinListBox_tblOrder;
		private System.Windows.Forms.TabPage tabtblOrderItem;
		private System.Windows.Forms.Button cmdNewtblOrderItem;
		private System.Windows.Forms.Button cmdEdittblOrderItem;
		private System.Windows.Forms.Button cmdDeletetblOrderItem;
		private System.Windows.Forms.Button cmdRefreshtblOrderItem;
		private WinListBox_tblOrderItem lstWinListBox_tblOrderItem;
		private System.Windows.Forms.TabPage tabtblProduct;
		private System.Windows.Forms.Button cmdNewtblProduct;
		private System.Windows.Forms.Button cmdEdittblProduct;
		private System.Windows.Forms.Button cmdDeletetblProduct;
		private System.Windows.Forms.Button cmdRefreshtblProduct;
		private WinListBox_tblProduct lstWinListBox_tblProduct;
		private System.Windows.Forms.TabPage tabtblSupplier;
		private System.Windows.Forms.Button cmdNewtblSupplier;
		private System.Windows.Forms.Button cmdEdittblSupplier;
		private System.Windows.Forms.Button cmdDeletetblSupplier;
		private System.Windows.Forms.Button cmdRefreshtblSupplier;
		private WinListBox_tblSupplier lstWinListBox_tblSupplier;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WinForm_Main(string ConnectionString) {

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ConnectionString = ConnectionString;

			this.comDatabaseTables.Items.Add("tblCategory");
			this.comDatabaseTables.Items.Add("tblCustomer");
			this.comDatabaseTables.Items.Add("tblOrder");
			this.comDatabaseTables.Items.Add("tblOrderItem");
			this.comDatabaseTables.Items.Add("tblProduct");
			this.comDatabaseTables.Items.Add("tblSupplier");
			this.comDatabaseTables.SelectedIndex = 0;

			try {

				lstWinListBox_tblCategory.Initialize(ConnectionString);
				lstWinListBox_tblCustomer.Initialize(ConnectionString);
				lstWinListBox_tblOrder.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				lstWinListBox_tblOrderItem.Initialize(ConnectionString, System.Data.SqlTypes.SqlGuid.Null, System.Data.SqlTypes.SqlGuid.Null);
				lstWinListBox_tblProduct.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				lstWinListBox_tblSupplier.Initialize(ConnectionString);
			}

			catch (Exception exception) {

				Clipboard.SetDataObject(exception.Message);
				MessageBox.Show(String.Format("The following exception has occured:{0}{0}{1}", Environment.NewLine, exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Enabled = false;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {

			if (disposing) {

				if (components != null) {

					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabtblCategory = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblCategory = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblCategory();
			this.cmdNewtblCategory = new System.Windows.Forms.Button();
			this.cmdEdittblCategory = new System.Windows.Forms.Button();
			this.cmdDeletetblCategory = new System.Windows.Forms.Button();
			this.cmdRefreshtblCategory = new System.Windows.Forms.Button();
			this.tabtblCategory.SuspendLayout();
			this.tabtblCustomer = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblCustomer = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblCustomer();
			this.cmdNewtblCustomer = new System.Windows.Forms.Button();
			this.cmdEdittblCustomer = new System.Windows.Forms.Button();
			this.cmdDeletetblCustomer = new System.Windows.Forms.Button();
			this.cmdRefreshtblCustomer = new System.Windows.Forms.Button();
			this.tabtblCustomer.SuspendLayout();
			this.tabtblOrder = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblOrder = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrder();
			this.cmdNewtblOrder = new System.Windows.Forms.Button();
			this.cmdEdittblOrder = new System.Windows.Forms.Button();
			this.cmdDeletetblOrder = new System.Windows.Forms.Button();
			this.cmdRefreshtblOrder = new System.Windows.Forms.Button();
			this.tabtblOrder.SuspendLayout();
			this.tabtblOrderItem = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblOrderItem = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblOrderItem();
			this.cmdNewtblOrderItem = new System.Windows.Forms.Button();
			this.cmdEdittblOrderItem = new System.Windows.Forms.Button();
			this.cmdDeletetblOrderItem = new System.Windows.Forms.Button();
			this.cmdRefreshtblOrderItem = new System.Windows.Forms.Button();
			this.tabtblOrderItem.SuspendLayout();
			this.tabtblProduct = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblProduct = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblProduct();
			this.cmdNewtblProduct = new System.Windows.Forms.Button();
			this.cmdEdittblProduct = new System.Windows.Forms.Button();
			this.cmdDeletetblProduct = new System.Windows.Forms.Button();
			this.cmdRefreshtblProduct = new System.Windows.Forms.Button();
			this.tabtblProduct.SuspendLayout();
			this.tabtblSupplier = new System.Windows.Forms.TabPage();
			this.lstWinListBox_tblSupplier = new OlymarsDemo.Windows.ListBoxes.WinListBox_tblSupplier();
			this.cmdNewtblSupplier = new System.Windows.Forms.Button();
			this.cmdEdittblSupplier = new System.Windows.Forms.Button();
			this.cmdDeletetblSupplier = new System.Windows.Forms.Button();
			this.cmdRefreshtblSupplier = new System.Windows.Forms.Button();
			this.tabtblSupplier.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstWinListBox_tblCategory
			// 
			this.lstWinListBox_tblCategory.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblCategory.Name = "lstWinListBox_tblCategory";
			this.lstWinListBox_tblCategory.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblCategory.TabIndex = 2;
			this.lstWinListBox_tblCategory.DoubleClick += new System.EventHandler(this.lstWinListBox_tblCategory_DoubleClick);
			// 
			// cmdNewtblCategory
			// 
			this.cmdNewtblCategory.Enabled = false;
			this.cmdNewtblCategory.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblCategory.Name = "cmdNewtblCategory";
			this.cmdNewtblCategory.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblCategory.TabIndex = 3;
			this.cmdNewtblCategory.Text = "New";
			this.cmdNewtblCategory.Click += new System.EventHandler(this.cmdNewtblCategory_Click);
			// 
			// cmdEdittblCategory
			// 
			this.cmdEdittblCategory.Enabled = false;
			this.cmdEdittblCategory.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblCategory.Name = "cmdEdittblCategory";
			this.cmdEdittblCategory.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblCategory.TabIndex = 4;
			this.cmdEdittblCategory.Text = "Edit";
			this.cmdEdittblCategory.Click += new System.EventHandler(this.cmdEdittblCategory_Click);
			// 
			// cmdDeletetblCategory
			// 
			this.cmdDeletetblCategory.Enabled = false;
			this.cmdDeletetblCategory.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblCategory.Name = "cmdDeletetblCategory";
			this.cmdDeletetblCategory.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblCategory.TabIndex = 5;
			this.cmdDeletetblCategory.Text = "Delete";
			this.cmdDeletetblCategory.Click += new System.EventHandler(this.cmdDeletetblCategory_Click);
			// 
			// cmdRefreshtblCategory
			// 
			this.cmdRefreshtblCategory.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblCategory.Name = "cmdRefreshtblCategory";
			this.cmdRefreshtblCategory.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblCategory.TabIndex = 6;
			this.cmdRefreshtblCategory.Text = "Refresh";
			this.cmdRefreshtblCategory.Click += new System.EventHandler(this.cmdRefreshtblCategory_Click);
			// 
			// tabtblCategory
			// 
			this.tabtblCategory.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblCategory,
															 this.cmdNewtblCategory,
															 this.cmdEdittblCategory,
															 this.cmdDeletetblCategory,
															 this.cmdRefreshtblCategory});
			this.tabtblCategory.Location = new System.Drawing.Point(4, 22);
			this.tabtblCategory.Name = "tabtblCategory";
			this.tabtblCategory.Size = new System.Drawing.Size(648, 390);
			this.tabtblCategory.TabIndex = 1;
			this.tabtblCategory.Text = "tblCategory";
			// 
			// lstWinListBox_tblCustomer
			// 
			this.lstWinListBox_tblCustomer.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblCustomer.Name = "lstWinListBox_tblCustomer";
			this.lstWinListBox_tblCustomer.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblCustomer.TabIndex = 2;
			this.lstWinListBox_tblCustomer.DoubleClick += new System.EventHandler(this.lstWinListBox_tblCustomer_DoubleClick);
			// 
			// cmdNewtblCustomer
			// 
			this.cmdNewtblCustomer.Enabled = false;
			this.cmdNewtblCustomer.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblCustomer.Name = "cmdNewtblCustomer";
			this.cmdNewtblCustomer.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblCustomer.TabIndex = 3;
			this.cmdNewtblCustomer.Text = "New";
			this.cmdNewtblCustomer.Click += new System.EventHandler(this.cmdNewtblCustomer_Click);
			// 
			// cmdEdittblCustomer
			// 
			this.cmdEdittblCustomer.Enabled = false;
			this.cmdEdittblCustomer.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblCustomer.Name = "cmdEdittblCustomer";
			this.cmdEdittblCustomer.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblCustomer.TabIndex = 4;
			this.cmdEdittblCustomer.Text = "Edit";
			this.cmdEdittblCustomer.Click += new System.EventHandler(this.cmdEdittblCustomer_Click);
			// 
			// cmdDeletetblCustomer
			// 
			this.cmdDeletetblCustomer.Enabled = false;
			this.cmdDeletetblCustomer.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblCustomer.Name = "cmdDeletetblCustomer";
			this.cmdDeletetblCustomer.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblCustomer.TabIndex = 5;
			this.cmdDeletetblCustomer.Text = "Delete";
			this.cmdDeletetblCustomer.Click += new System.EventHandler(this.cmdDeletetblCustomer_Click);
			// 
			// cmdRefreshtblCustomer
			// 
			this.cmdRefreshtblCustomer.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblCustomer.Name = "cmdRefreshtblCustomer";
			this.cmdRefreshtblCustomer.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblCustomer.TabIndex = 6;
			this.cmdRefreshtblCustomer.Text = "Refresh";
			this.cmdRefreshtblCustomer.Click += new System.EventHandler(this.cmdRefreshtblCustomer_Click);
			// 
			// tabtblCustomer
			// 
			this.tabtblCustomer.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblCustomer,
															 this.cmdNewtblCustomer,
															 this.cmdEdittblCustomer,
															 this.cmdDeletetblCustomer,
															 this.cmdRefreshtblCustomer});
			this.tabtblCustomer.Location = new System.Drawing.Point(4, 22);
			this.tabtblCustomer.Name = "tabtblCustomer";
			this.tabtblCustomer.Size = new System.Drawing.Size(648, 390);
			this.tabtblCustomer.TabIndex = 1;
			this.tabtblCustomer.Text = "tblCustomer";
			// 
			// lstWinListBox_tblOrder
			// 
			this.lstWinListBox_tblOrder.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblOrder.Name = "lstWinListBox_tblOrder";
			this.lstWinListBox_tblOrder.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblOrder.TabIndex = 2;
			this.lstWinListBox_tblOrder.DoubleClick += new System.EventHandler(this.lstWinListBox_tblOrder_DoubleClick);
			// 
			// cmdNewtblOrder
			// 
			this.cmdNewtblOrder.Enabled = false;
			this.cmdNewtblOrder.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblOrder.Name = "cmdNewtblOrder";
			this.cmdNewtblOrder.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblOrder.TabIndex = 3;
			this.cmdNewtblOrder.Text = "New";
			this.cmdNewtblOrder.Click += new System.EventHandler(this.cmdNewtblOrder_Click);
			// 
			// cmdEdittblOrder
			// 
			this.cmdEdittblOrder.Enabled = false;
			this.cmdEdittblOrder.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblOrder.Name = "cmdEdittblOrder";
			this.cmdEdittblOrder.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblOrder.TabIndex = 4;
			this.cmdEdittblOrder.Text = "Edit";
			this.cmdEdittblOrder.Click += new System.EventHandler(this.cmdEdittblOrder_Click);
			// 
			// cmdDeletetblOrder
			// 
			this.cmdDeletetblOrder.Enabled = false;
			this.cmdDeletetblOrder.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblOrder.Name = "cmdDeletetblOrder";
			this.cmdDeletetblOrder.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblOrder.TabIndex = 5;
			this.cmdDeletetblOrder.Text = "Delete";
			this.cmdDeletetblOrder.Click += new System.EventHandler(this.cmdDeletetblOrder_Click);
			// 
			// cmdRefreshtblOrder
			// 
			this.cmdRefreshtblOrder.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblOrder.Name = "cmdRefreshtblOrder";
			this.cmdRefreshtblOrder.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblOrder.TabIndex = 6;
			this.cmdRefreshtblOrder.Text = "Refresh";
			this.cmdRefreshtblOrder.Click += new System.EventHandler(this.cmdRefreshtblOrder_Click);
			// 
			// tabtblOrder
			// 
			this.tabtblOrder.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblOrder,
															 this.cmdNewtblOrder,
															 this.cmdEdittblOrder,
															 this.cmdDeletetblOrder,
															 this.cmdRefreshtblOrder});
			this.tabtblOrder.Location = new System.Drawing.Point(4, 22);
			this.tabtblOrder.Name = "tabtblOrder";
			this.tabtblOrder.Size = new System.Drawing.Size(648, 390);
			this.tabtblOrder.TabIndex = 1;
			this.tabtblOrder.Text = "tblOrder";
			// 
			// lstWinListBox_tblOrderItem
			// 
			this.lstWinListBox_tblOrderItem.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblOrderItem.Name = "lstWinListBox_tblOrderItem";
			this.lstWinListBox_tblOrderItem.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblOrderItem.TabIndex = 2;
			this.lstWinListBox_tblOrderItem.DoubleClick += new System.EventHandler(this.lstWinListBox_tblOrderItem_DoubleClick);
			// 
			// cmdNewtblOrderItem
			// 
			this.cmdNewtblOrderItem.Enabled = false;
			this.cmdNewtblOrderItem.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblOrderItem.Name = "cmdNewtblOrderItem";
			this.cmdNewtblOrderItem.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblOrderItem.TabIndex = 3;
			this.cmdNewtblOrderItem.Text = "New";
			this.cmdNewtblOrderItem.Click += new System.EventHandler(this.cmdNewtblOrderItem_Click);
			// 
			// cmdEdittblOrderItem
			// 
			this.cmdEdittblOrderItem.Enabled = false;
			this.cmdEdittblOrderItem.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblOrderItem.Name = "cmdEdittblOrderItem";
			this.cmdEdittblOrderItem.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblOrderItem.TabIndex = 4;
			this.cmdEdittblOrderItem.Text = "Edit";
			this.cmdEdittblOrderItem.Click += new System.EventHandler(this.cmdEdittblOrderItem_Click);
			// 
			// cmdDeletetblOrderItem
			// 
			this.cmdDeletetblOrderItem.Enabled = false;
			this.cmdDeletetblOrderItem.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblOrderItem.Name = "cmdDeletetblOrderItem";
			this.cmdDeletetblOrderItem.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblOrderItem.TabIndex = 5;
			this.cmdDeletetblOrderItem.Text = "Delete";
			this.cmdDeletetblOrderItem.Click += new System.EventHandler(this.cmdDeletetblOrderItem_Click);
			// 
			// cmdRefreshtblOrderItem
			// 
			this.cmdRefreshtblOrderItem.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblOrderItem.Name = "cmdRefreshtblOrderItem";
			this.cmdRefreshtblOrderItem.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblOrderItem.TabIndex = 6;
			this.cmdRefreshtblOrderItem.Text = "Refresh";
			this.cmdRefreshtblOrderItem.Click += new System.EventHandler(this.cmdRefreshtblOrderItem_Click);
			// 
			// tabtblOrderItem
			// 
			this.tabtblOrderItem.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblOrderItem,
															 this.cmdNewtblOrderItem,
															 this.cmdEdittblOrderItem,
															 this.cmdDeletetblOrderItem,
															 this.cmdRefreshtblOrderItem});
			this.tabtblOrderItem.Location = new System.Drawing.Point(4, 22);
			this.tabtblOrderItem.Name = "tabtblOrderItem";
			this.tabtblOrderItem.Size = new System.Drawing.Size(648, 390);
			this.tabtblOrderItem.TabIndex = 1;
			this.tabtblOrderItem.Text = "tblOrderItem";
			// 
			// lstWinListBox_tblProduct
			// 
			this.lstWinListBox_tblProduct.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblProduct.Name = "lstWinListBox_tblProduct";
			this.lstWinListBox_tblProduct.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblProduct.TabIndex = 2;
			this.lstWinListBox_tblProduct.DoubleClick += new System.EventHandler(this.lstWinListBox_tblProduct_DoubleClick);
			// 
			// cmdNewtblProduct
			// 
			this.cmdNewtblProduct.Enabled = false;
			this.cmdNewtblProduct.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblProduct.Name = "cmdNewtblProduct";
			this.cmdNewtblProduct.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblProduct.TabIndex = 3;
			this.cmdNewtblProduct.Text = "New";
			this.cmdNewtblProduct.Click += new System.EventHandler(this.cmdNewtblProduct_Click);
			// 
			// cmdEdittblProduct
			// 
			this.cmdEdittblProduct.Enabled = false;
			this.cmdEdittblProduct.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblProduct.Name = "cmdEdittblProduct";
			this.cmdEdittblProduct.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblProduct.TabIndex = 4;
			this.cmdEdittblProduct.Text = "Edit";
			this.cmdEdittblProduct.Click += new System.EventHandler(this.cmdEdittblProduct_Click);
			// 
			// cmdDeletetblProduct
			// 
			this.cmdDeletetblProduct.Enabled = false;
			this.cmdDeletetblProduct.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblProduct.Name = "cmdDeletetblProduct";
			this.cmdDeletetblProduct.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblProduct.TabIndex = 5;
			this.cmdDeletetblProduct.Text = "Delete";
			this.cmdDeletetblProduct.Click += new System.EventHandler(this.cmdDeletetblProduct_Click);
			// 
			// cmdRefreshtblProduct
			// 
			this.cmdRefreshtblProduct.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblProduct.Name = "cmdRefreshtblProduct";
			this.cmdRefreshtblProduct.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblProduct.TabIndex = 6;
			this.cmdRefreshtblProduct.Text = "Refresh";
			this.cmdRefreshtblProduct.Click += new System.EventHandler(this.cmdRefreshtblProduct_Click);
			// 
			// tabtblProduct
			// 
			this.tabtblProduct.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblProduct,
															 this.cmdNewtblProduct,
															 this.cmdEdittblProduct,
															 this.cmdDeletetblProduct,
															 this.cmdRefreshtblProduct});
			this.tabtblProduct.Location = new System.Drawing.Point(4, 22);
			this.tabtblProduct.Name = "tabtblProduct";
			this.tabtblProduct.Size = new System.Drawing.Size(648, 390);
			this.tabtblProduct.TabIndex = 1;
			this.tabtblProduct.Text = "tblProduct";
			// 
			// lstWinListBox_tblSupplier
			// 
			this.lstWinListBox_tblSupplier.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_tblSupplier.Name = "lstWinListBox_tblSupplier";
			this.lstWinListBox_tblSupplier.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_tblSupplier.TabIndex = 2;
			this.lstWinListBox_tblSupplier.DoubleClick += new System.EventHandler(this.lstWinListBox_tblSupplier_DoubleClick);
			// 
			// cmdNewtblSupplier
			// 
			this.cmdNewtblSupplier.Enabled = false;
			this.cmdNewtblSupplier.Location = new System.Drawing.Point(528, 16);
			this.cmdNewtblSupplier.Name = "cmdNewtblSupplier";
			this.cmdNewtblSupplier.Size = new System.Drawing.Size(104, 23);
			this.cmdNewtblSupplier.TabIndex = 3;
			this.cmdNewtblSupplier.Text = "New";
			this.cmdNewtblSupplier.Click += new System.EventHandler(this.cmdNewtblSupplier_Click);
			// 
			// cmdEdittblSupplier
			// 
			this.cmdEdittblSupplier.Enabled = false;
			this.cmdEdittblSupplier.Location = new System.Drawing.Point(528, 56);
			this.cmdEdittblSupplier.Name = "cmdEdittblSupplier";
			this.cmdEdittblSupplier.Size = new System.Drawing.Size(104, 23);
			this.cmdEdittblSupplier.TabIndex = 4;
			this.cmdEdittblSupplier.Text = "Edit";
			this.cmdEdittblSupplier.Click += new System.EventHandler(this.cmdEdittblSupplier_Click);
			// 
			// cmdDeletetblSupplier
			// 
			this.cmdDeletetblSupplier.Enabled = false;
			this.cmdDeletetblSupplier.Location = new System.Drawing.Point(528, 96);
			this.cmdDeletetblSupplier.Name = "cmdDeletetblSupplier";
			this.cmdDeletetblSupplier.Size = new System.Drawing.Size(104, 23);
			this.cmdDeletetblSupplier.TabIndex = 5;
			this.cmdDeletetblSupplier.Text = "Delete";
			this.cmdDeletetblSupplier.Click += new System.EventHandler(this.cmdDeletetblSupplier_Click);
			// 
			// cmdRefreshtblSupplier
			// 
			this.cmdRefreshtblSupplier.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshtblSupplier.Name = "cmdRefreshtblSupplier";
			this.cmdRefreshtblSupplier.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshtblSupplier.TabIndex = 6;
			this.cmdRefreshtblSupplier.Text = "Refresh";
			this.cmdRefreshtblSupplier.Click += new System.EventHandler(this.cmdRefreshtblSupplier_Click);
			// 
			// tabtblSupplier
			// 
			this.tabtblSupplier.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_tblSupplier,
															 this.cmdNewtblSupplier,
															 this.cmdEdittblSupplier,
															 this.cmdDeletetblSupplier,
															 this.cmdRefreshtblSupplier});
			this.tabtblSupplier.Location = new System.Drawing.Point(4, 22);
			this.tabtblSupplier.Name = "tabtblSupplier";
			this.tabtblSupplier.Size = new System.Drawing.Size(648, 390);
			this.tabtblSupplier.TabIndex = 1;
			this.tabtblSupplier.Text = "tblSupplier";
			//
			// comDatabaseTables
			//
			this.comDatabaseTables = new System.Windows.Forms.ComboBox();
			this.comDatabaseTables.Location = new System.Drawing.Point(16, 16);
			this.comDatabaseTables.Name = "comDatabaseTables ";
			this.comDatabaseTables.Size = new System.Drawing.Size(656, 24);
			this.comDatabaseTables.TabIndex = 0;
			this.comDatabaseTables.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comDatabaseTables.SelectedIndexChanged += new System.EventHandler(comDatabaseTables_SelectedIndexChanged);
			// 
			// tabControl
			// 
			this.tabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
															  this.tabtblCategory
															 ,this.tabtblCustomer
															 ,this.tabtblOrder
															 ,this.tabtblOrderItem
															 ,this.tabtblProduct
															 ,this.tabtblSupplier
															});
			this.tabControl.Location = new System.Drawing.Point(16, 45);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(656, 416);
			this.tabControl.TabIndex = 1;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// WinForm_Main
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.ClientSize = new System.Drawing.Size(692, 470);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
												  this.tabControl, this.comDatabaseTables});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "WinForm_Main";
			this.Text = "\'OlymarsDemo\' Management";
			this.tabtblCategory.ResumeLayout(false);
			this.tabtblCustomer.ResumeLayout(false);
			this.tabtblOrder.ResumeLayout(false);
			this.tabtblOrderItem.ResumeLayout(false);
			this.tabtblProduct.ResumeLayout(false);
			this.tabtblSupplier.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {

			string connectionString = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDemo ConnectionString"];

			if (connectionString == null || connectionString == String.Empty) {

				OlymarsDemo.Windows.Forms.WinForm_DBConnection oWinForm_DBConnection = new OlymarsDemo.Windows.Forms.WinForm_DBConnection();
				oWinForm_DBConnection.InitializeData("OlymarsDemo connection", @"MAINSERVER\MAINSERVER", "OlymarsDemo");
				oWinForm_DBConnection.ShowDialog(null);
				if (oWinForm_DBConnection.DialogResult == DialogResult.OK) {

					connectionString = oWinForm_DBConnection.ConnectionString;
					oWinForm_DBConnection.Dispose();
					Application.Run(new WinForm_Main(connectionString));
				}
			}
			else {

				Application.Run(new WinForm_Main(connectionString));
			}
		}

		private string ConnectionString = String.Empty;

		private void comDatabaseTables_SelectedIndexChanged(object sender, EventArgs e) {

			switch (comDatabaseTables.SelectedItem.ToString()) {

				case "tblCategory":
				this.tabControl.SelectedTab = tabtblCategory;
				break;

				case "tblCustomer":
				this.tabControl.SelectedTab = tabtblCustomer;
				break;

				case "tblOrder":
				this.tabControl.SelectedTab = tabtblOrder;
				break;

				case "tblOrderItem":
				this.tabControl.SelectedTab = tabtblOrderItem;
				break;

				case "tblProduct":
				this.tabControl.SelectedTab = tabtblProduct;
				break;

				case "tblSupplier":
				this.tabControl.SelectedTab = tabtblSupplier;
				break;

			}
		}
		private void lstWinListBox_tblCategory_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblCategory();

			this.Cursor = Cursors.Default;
		}

		private void EdittblCategory() {

			System.Data.SqlTypes.SqlInt32 _Cat_LngID = (System.Int32)lstWinListBox_tblCategory.SelectedValue;

			if (_Cat_LngID.IsNull) {

				return;
			}

			WinForm_tblCategory oWinForm_tblCategory = new WinForm_tblCategory();

			try {

				if (oWinForm_tblCategory.EditExistingRecord(this, "Edit an existing tblCategory", ConnectionString, _Cat_LngID)) {

					if (oWinForm_tblCategory.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblCategory.ErrorHasOccured) {

							RefreshtblCategory(_Cat_LngID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCategory' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCategory' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblCategory_Click(object sender, System.EventArgs e) {

			WinForm_tblCategory oWinForm_tblCategory = new WinForm_tblCategory();

			try {

				oWinForm_tblCategory.AddNewRecord(this, "Add a new tblCategory", ConnectionString);
				if (oWinForm_tblCategory.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblCategory.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_Cat_LngID = ((Params.spI_tblCategory)oWinForm_tblCategory.Parameter).Param_Cat_LngID;
						RefreshtblCategory(Col_Cat_LngID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCategory' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCategory' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblCategory_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblCategory();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblCategory_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblCategory.SelectedIndex != -1) {

				RefreshtblCategory((System.Int32)lstWinListBox_tblCategory.SelectedValue);
			}
			else {

				RefreshtblCategory(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblCategory(System.Data.SqlTypes.SqlInt32 Col_Cat_LngID) {

			try {

				lstWinListBox_tblCategory.RefreshData(Col_Cat_LngID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblCategory' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblCategory' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblCategory.Enabled = true;
			bool Status = (lstWinListBox_tblCategory.Items.Count != 0);
			cmdEdittblCategory.Enabled = Status;
			cmdDeletetblCategory.Enabled = Status;
		}

		private void cmdDeletetblCategory_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_Cat_LngID = (System.Int32)lstWinListBox_tblCategory.SelectedValue;
			if (Col_Cat_LngID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblCategory Param = new Params.spD_tblCategory();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Cat_LngID = Col_Cat_LngID;

				SPs.spD_tblCategory SP = new SPs.spD_tblCategory();
				if (SP.Execute(ref Param)) {

					RefreshtblCategory(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblCategory_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_tblCustomer_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblCustomer();

			this.Cursor = Cursors.Default;
		}

		private void EdittblCustomer() {

			System.Data.SqlTypes.SqlInt32 _Cus_LngID = (System.Int32)lstWinListBox_tblCustomer.SelectedValue;

			if (_Cus_LngID.IsNull) {

				return;
			}

			WinForm_tblCustomer oWinForm_tblCustomer = new WinForm_tblCustomer();

			try {

				if (oWinForm_tblCustomer.EditExistingRecord(this, "Edit an existing tblCustomer", ConnectionString, _Cus_LngID)) {

					if (oWinForm_tblCustomer.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblCustomer.ErrorHasOccured) {

							RefreshtblCustomer(_Cus_LngID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCustomer' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCustomer' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblCustomer_Click(object sender, System.EventArgs e) {

			WinForm_tblCustomer oWinForm_tblCustomer = new WinForm_tblCustomer();

			try {

				oWinForm_tblCustomer.AddNewRecord(this, "Add a new tblCustomer", ConnectionString);
				if (oWinForm_tblCustomer.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblCustomer.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_Cus_LngID = ((Params.spI_tblCustomer)oWinForm_tblCustomer.Parameter).Param_Cus_LngID;
						RefreshtblCustomer(Col_Cus_LngID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCustomer' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblCustomer' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblCustomer_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblCustomer();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblCustomer_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblCustomer.SelectedIndex != -1) {

				RefreshtblCustomer((System.Int32)lstWinListBox_tblCustomer.SelectedValue);
			}
			else {

				RefreshtblCustomer(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblCustomer(System.Data.SqlTypes.SqlInt32 Col_Cus_LngID) {

			try {

				lstWinListBox_tblCustomer.RefreshData(Col_Cus_LngID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblCustomer' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblCustomer' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblCustomer.Enabled = true;
			bool Status = (lstWinListBox_tblCustomer.Items.Count != 0);
			cmdEdittblCustomer.Enabled = Status;
			cmdDeletetblCustomer.Enabled = Status;
		}

		private void cmdDeletetblCustomer_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_Cus_LngID = (System.Int32)lstWinListBox_tblCustomer.SelectedValue;
			if (Col_Cus_LngID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblCustomer Param = new Params.spD_tblCustomer();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Cus_LngID = Col_Cus_LngID;

				SPs.spD_tblCustomer SP = new SPs.spD_tblCustomer();
				if (SP.Execute(ref Param)) {

					RefreshtblCustomer(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblCustomer_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_tblOrder_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblOrder();

			this.Cursor = Cursors.Default;
		}

		private void EdittblOrder() {

			System.Data.SqlTypes.SqlGuid _Ord_GuidID = (System.Guid)lstWinListBox_tblOrder.SelectedValue;

			if (_Ord_GuidID.IsNull) {

				return;
			}

			WinForm_tblOrder oWinForm_tblOrder = new WinForm_tblOrder();

			try {

				if (oWinForm_tblOrder.EditExistingRecord(this, "Edit an existing tblOrder", ConnectionString, _Ord_GuidID)) {

					if (oWinForm_tblOrder.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblOrder.ErrorHasOccured) {

							RefreshtblOrder(_Ord_GuidID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrder' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrder' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblOrder_Click(object sender, System.EventArgs e) {

			WinForm_tblOrder oWinForm_tblOrder = new WinForm_tblOrder();

			try {

				oWinForm_tblOrder.AddNewRecord(this, "Add a new tblOrder", ConnectionString);
				if (oWinForm_tblOrder.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblOrder.ErrorHasOccured) {

						System.Data.SqlTypes.SqlGuid Col_Ord_GuidID = ((Params.spI_tblOrder)oWinForm_tblOrder.Parameter).Param_Ord_GuidID;
						RefreshtblOrder(Col_Ord_GuidID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrder' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrder' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblOrder_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblOrder();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblOrder_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblOrder.SelectedIndex != -1) {

				RefreshtblOrder((System.Guid)lstWinListBox_tblOrder.SelectedValue);
			}
			else {

				RefreshtblOrder(System.Data.SqlTypes.SqlGuid.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblOrder(System.Data.SqlTypes.SqlGuid Col_Ord_GuidID) {

			try {

				lstWinListBox_tblOrder.RefreshData(Col_Ord_GuidID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblOrder' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblOrder' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblOrder.Enabled = true;
			bool Status = (lstWinListBox_tblOrder.Items.Count != 0);
			cmdEdittblOrder.Enabled = Status;
			cmdDeletetblOrder.Enabled = Status;
		}

		private void cmdDeletetblOrder_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlGuid Col_Ord_GuidID = (System.Guid)lstWinListBox_tblOrder.SelectedValue;
			if (Col_Ord_GuidID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblOrder Param = new Params.spD_tblOrder();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Ord_GuidID = Col_Ord_GuidID;

				SPs.spD_tblOrder SP = new SPs.spD_tblOrder();
				if (SP.Execute(ref Param)) {

					RefreshtblOrder(System.Data.SqlTypes.SqlGuid.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblOrder_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_tblOrderItem_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblOrderItem();

			this.Cursor = Cursors.Default;
		}

		private void EdittblOrderItem() {

			System.Data.SqlTypes.SqlGuid _Oit_GuidID = (System.Guid)lstWinListBox_tblOrderItem.SelectedValue;

			if (_Oit_GuidID.IsNull) {

				return;
			}

			WinForm_tblOrderItem oWinForm_tblOrderItem = new WinForm_tblOrderItem();

			try {

				if (oWinForm_tblOrderItem.EditExistingRecord(this, "Edit an existing tblOrderItem", ConnectionString, _Oit_GuidID)) {

					if (oWinForm_tblOrderItem.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblOrderItem.ErrorHasOccured) {

							RefreshtblOrderItem(_Oit_GuidID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrderItem' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrderItem' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblOrderItem_Click(object sender, System.EventArgs e) {

			WinForm_tblOrderItem oWinForm_tblOrderItem = new WinForm_tblOrderItem();

			try {

				oWinForm_tblOrderItem.AddNewRecord(this, "Add a new tblOrderItem", ConnectionString);
				if (oWinForm_tblOrderItem.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblOrderItem.ErrorHasOccured) {

						System.Data.SqlTypes.SqlGuid Col_Oit_GuidID = ((Params.spI_tblOrderItem)oWinForm_tblOrderItem.Parameter).Param_Oit_GuidID;
						RefreshtblOrderItem(Col_Oit_GuidID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrderItem' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblOrderItem' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblOrderItem_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblOrderItem();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblOrderItem_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblOrderItem.SelectedIndex != -1) {

				RefreshtblOrderItem((System.Guid)lstWinListBox_tblOrderItem.SelectedValue);
			}
			else {

				RefreshtblOrderItem(System.Data.SqlTypes.SqlGuid.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblOrderItem(System.Data.SqlTypes.SqlGuid Col_Oit_GuidID) {

			try {

				lstWinListBox_tblOrderItem.RefreshData(Col_Oit_GuidID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblOrderItem' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblOrderItem' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblOrderItem.Enabled = true;
			bool Status = (lstWinListBox_tblOrderItem.Items.Count != 0);
			cmdEdittblOrderItem.Enabled = Status;
			cmdDeletetblOrderItem.Enabled = Status;
		}

		private void cmdDeletetblOrderItem_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlGuid Col_Oit_GuidID = (System.Guid)lstWinListBox_tblOrderItem.SelectedValue;
			if (Col_Oit_GuidID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblOrderItem Param = new Params.spD_tblOrderItem();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Oit_GuidID = Col_Oit_GuidID;

				SPs.spD_tblOrderItem SP = new SPs.spD_tblOrderItem();
				if (SP.Execute(ref Param)) {

					RefreshtblOrderItem(System.Data.SqlTypes.SqlGuid.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblOrderItem_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_tblProduct_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblProduct();

			this.Cursor = Cursors.Default;
		}

		private void EdittblProduct() {

			System.Data.SqlTypes.SqlGuid _Pro_GuidID = (System.Guid)lstWinListBox_tblProduct.SelectedValue;

			if (_Pro_GuidID.IsNull) {

				return;
			}

			WinForm_tblProduct oWinForm_tblProduct = new WinForm_tblProduct();

			try {

				if (oWinForm_tblProduct.EditExistingRecord(this, "Edit an existing tblProduct", ConnectionString, _Pro_GuidID)) {

					if (oWinForm_tblProduct.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblProduct.ErrorHasOccured) {

							RefreshtblProduct(_Pro_GuidID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblProduct' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblProduct' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblProduct_Click(object sender, System.EventArgs e) {

			WinForm_tblProduct oWinForm_tblProduct = new WinForm_tblProduct();

			try {

				oWinForm_tblProduct.AddNewRecord(this, "Add a new tblProduct", ConnectionString);
				if (oWinForm_tblProduct.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblProduct.ErrorHasOccured) {

						System.Data.SqlTypes.SqlGuid Col_Pro_GuidID = ((Params.spI_tblProduct)oWinForm_tblProduct.Parameter).Param_Pro_GuidID;
						RefreshtblProduct(Col_Pro_GuidID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblProduct' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblProduct' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblProduct_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblProduct();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblProduct_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblProduct.SelectedIndex != -1) {

				RefreshtblProduct((System.Guid)lstWinListBox_tblProduct.SelectedValue);
			}
			else {

				RefreshtblProduct(System.Data.SqlTypes.SqlGuid.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblProduct(System.Data.SqlTypes.SqlGuid Col_Pro_GuidID) {

			try {

				lstWinListBox_tblProduct.RefreshData(Col_Pro_GuidID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblProduct' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblProduct' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblProduct.Enabled = true;
			bool Status = (lstWinListBox_tblProduct.Items.Count != 0);
			cmdEdittblProduct.Enabled = Status;
			cmdDeletetblProduct.Enabled = Status;
		}

		private void cmdDeletetblProduct_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlGuid Col_Pro_GuidID = (System.Guid)lstWinListBox_tblProduct.SelectedValue;
			if (Col_Pro_GuidID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblProduct Param = new Params.spD_tblProduct();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Pro_GuidID = Col_Pro_GuidID;

				SPs.spD_tblProduct SP = new SPs.spD_tblProduct();
				if (SP.Execute(ref Param)) {

					RefreshtblProduct(System.Data.SqlTypes.SqlGuid.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblProduct_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_tblSupplier_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblSupplier();

			this.Cursor = Cursors.Default;
		}

		private void EdittblSupplier() {

			System.Data.SqlTypes.SqlGuid _Sup_GuidID = (System.Guid)lstWinListBox_tblSupplier.SelectedValue;

			if (_Sup_GuidID.IsNull) {

				return;
			}

			WinForm_tblSupplier oWinForm_tblSupplier = new WinForm_tblSupplier();

			try {

				if (oWinForm_tblSupplier.EditExistingRecord(this, "Edit an existing tblSupplier", ConnectionString, _Sup_GuidID)) {

					if (oWinForm_tblSupplier.DialogResult == DialogResult.OK) {

						if (!oWinForm_tblSupplier.ErrorHasOccured) {

							RefreshtblSupplier(_Sup_GuidID);
						}
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblSupplier' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblSupplier' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewtblSupplier_Click(object sender, System.EventArgs e) {

			WinForm_tblSupplier oWinForm_tblSupplier = new WinForm_tblSupplier();

			try {

				oWinForm_tblSupplier.AddNewRecord(this, "Add a new tblSupplier", ConnectionString);
				if (oWinForm_tblSupplier.DialogResult == DialogResult.OK) {

					if (!oWinForm_tblSupplier.ErrorHasOccured) {

						System.Data.SqlTypes.SqlGuid Col_Sup_GuidID = ((Params.spI_tblSupplier)oWinForm_tblSupplier.Parameter).Param_Sup_GuidID;
						RefreshtblSupplier(Col_Sup_GuidID);
					}
				}
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblSupplier' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_tblSupplier' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEdittblSupplier_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EdittblSupplier();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshtblSupplier_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_tblSupplier.SelectedIndex != -1) {

				RefreshtblSupplier((System.Guid)lstWinListBox_tblSupplier.SelectedValue);
			}
			else {

				RefreshtblSupplier(System.Data.SqlTypes.SqlGuid.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshtblSupplier(System.Data.SqlTypes.SqlGuid Col_Sup_GuidID) {

			try {

				lstWinListBox_tblSupplier.RefreshData(Col_Sup_GuidID);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblSupplier' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_tblSupplier' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewtblSupplier.Enabled = true;
			bool Status = (lstWinListBox_tblSupplier.Items.Count != 0);
			cmdEdittblSupplier.Enabled = Status;
			cmdDeletetblSupplier.Enabled = Status;
		}

		private void cmdDeletetblSupplier_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlGuid Col_Sup_GuidID = (System.Guid)lstWinListBox_tblSupplier.SelectedValue;
			if (Col_Sup_GuidID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_tblSupplier Param = new Params.spD_tblSupplier();

				Param.SetUpConnection(ConnectionString);
				Param.Param_Sup_GuidID = Col_Sup_GuidID;

				SPs.spD_tblSupplier SP = new SPs.spD_tblSupplier();
				if (SP.Execute(ref Param)) {

					RefreshtblSupplier(System.Data.SqlTypes.SqlGuid.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeletetblSupplier_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e) {

			comDatabaseTables.SelectedIndex = comDatabaseTables.FindString(tabControl.SelectedTab.Text);
		}
	}
}
