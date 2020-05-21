using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bob.Windows.Forms.SubClasses 
{
	/// <summary>
	/// Summary description for SearchCustomers.
	/// </summary>
	public class SearchCustomers : System.Windows.Forms.Form
	{
		private String ConnectionString;
		internal System.Windows.Forms.Button cmdSearch;
		private Bob.Windows.ListBoxes.WinListBoxCustom_spS_xSearchCustomer winListBoxCustom_spS_xSearchCustomer1;
		internal System.Windows.Forms.Button cmdAdd;
		internal System.Windows.Forms.TextBox txtCompanyName;
		internal System.Windows.Forms.Label lblCompanyName;
		internal System.Windows.Forms.Label lblContactName;
		internal System.Windows.Forms.TextBox txtContactName;
		internal System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuNew;
		private System.Windows.Forms.MenuItem mnuViewJobs;
		private System.Windows.Forms.MenuItem mnuEmail;
		private System.Windows.Forms.MenuItem mnuInactive;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SearchCustomers(string connectionString )
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.ConnectionString=connectionString;
			
		}


		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtCompanyName = new System.Windows.Forms.TextBox();
			this.lblCompanyName = new System.Windows.Forms.Label();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.winListBoxCustom_spS_xSearchCustomer1 = new Bob.Windows.ListBoxes.WinListBoxCustom_spS_xSearchCustomer();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuViewJobs = new System.Windows.Forms.MenuItem();
			this.mnuNew = new System.Windows.Forms.MenuItem();
			this.mnuEmail = new System.Windows.Forms.MenuItem();
			this.mnuInactive = new System.Windows.Forms.MenuItem();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.lblContactName = new System.Windows.Forms.Label();
			this.txtContactName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtCompanyName
			// 
			this.txtCompanyName.Location = new System.Drawing.Point(312, 32);
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.Size = new System.Drawing.Size(256, 20);
			this.txtCompanyName.TabIndex = 1;
			this.txtCompanyName.Text = "";
			this.txtCompanyName.TextChanged += new System.EventHandler(this.SearchTextChanged);
			// 
			// lblCompanyName
			// 
			this.lblCompanyName.AutoSize = true;
			this.lblCompanyName.Location = new System.Drawing.Point(312, 16);
			this.lblCompanyName.Name = "lblCompanyName";
			this.lblCompanyName.Size = new System.Drawing.Size(110, 16);
			this.lblCompanyName.TabIndex = 5;
			this.lblCompanyName.Text = "Company Name:";
			// 
			// cmdSearch
			// 
			this.cmdSearch.Location = new System.Drawing.Point(576, 32);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(104, 23);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "Search... ";
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// winListBoxCustom_spS_xSearchCustomer1
			// 
			this.winListBoxCustom_spS_xSearchCustomer1.CommandTimeOut = 30;
			this.winListBoxCustom_spS_xSearchCustomer1.ContextMenu = this.contextMenu1;
			this.winListBoxCustom_spS_xSearchCustomer1.Location = new System.Drawing.Point(16, 112);
			this.winListBoxCustom_spS_xSearchCustomer1.Name = "winListBoxCustom_spS_xSearchCustomer1";
			this.winListBoxCustom_spS_xSearchCustomer1.Size = new System.Drawing.Size(664, 290);
			this.winListBoxCustom_spS_xSearchCustomer1.TabIndex = 3;
			this.winListBoxCustom_spS_xSearchCustomer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.winListBoxCustom_spS_xSearchCustomer1_MouseDown);
			this.winListBoxCustom_spS_xSearchCustomer1.DoubleClick += new System.EventHandler(this.winListBoxCustom_spS_xSearchCustomer1_DoubleClick);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuEdit,
																						 this.mnuViewJobs,
																						 this.mnuNew,
																						 this.mnuEmail,
																						 this.mnuInactive});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 0;
			this.mnuEdit.Text = "Edit";
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			// 
			// mnuViewJobs
			// 
			this.mnuViewJobs.Index = 1;
			this.mnuViewJobs.Text = "View Jobs";
			// 
			// mnuNew
			// 
			this.mnuNew.Index = 2;
			this.mnuNew.Text = "Add New";
			this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
			// 
			// mnuEmail
			// 
			this.mnuEmail.Index = 3;
			this.mnuEmail.Text = "Email";
			// 
			// mnuInactive
			// 
			this.mnuInactive.Index = 4;
			this.mnuInactive.Text = "Make Inactive";
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(576, 32);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(104, 23);
			this.cmdAdd.TabIndex = 4;
			this.cmdAdd.Text = "Add New";
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// lblContactName
			// 
			this.lblContactName.AutoSize = true;
			this.lblContactName.BackColor = System.Drawing.Color.Transparent;
			this.lblContactName.Location = new System.Drawing.Point(16, 16);
			this.lblContactName.Name = "lblContactName";
			this.lblContactName.Size = new System.Drawing.Size(182, 16);
			this.lblContactName.TabIndex = 4;
			this.lblContactName.Text = "Customer/Contact Name:";
			// 
			// txtContactName
			// 
			this.txtContactName.Location = new System.Drawing.Point(16, 32);
			this.txtContactName.Name = "txtContactName";
			this.txtContactName.Size = new System.Drawing.Size(256, 20);
			this.txtContactName.TabIndex = 0;
			this.txtContactName.Text = "";
			this.txtContactName.TextChanged += new System.EventHandler(this.SearchTextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Search Results:";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(312, 56);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(168, 24);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Include Inactive";
			// 
			// SearchCustomers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.ClientSize = new System.Drawing.Size(704, 469);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.winListBoxCustom_spS_xSearchCustomer1);
			this.Controls.Add(this.cmdSearch);
			this.Controls.Add(this.txtCompanyName);
			this.Controls.Add(this.lblCompanyName);
			this.Controls.Add(this.lblContactName);
			this.Controls.Add(this.txtContactName);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F);
			this.MaximizeBox = false;
			this.Name = "SearchCustomers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Search Customers";
			this.Load += new System.EventHandler(this.SearchCustomers_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void PerformSearch()
		{
//			if (this.txtContactName.Text.Trim().Length == 0 && this.txtCompanyName.Text.Trim().Length == 0)
//			{
//				MessageBox.Show(this, "Please Enter Search Criteria", "Error");
//				this.txtContactName.Select();
//				return;
//			}
  
			this.Cursor = Cursors.WaitCursor;

			System.Data.SqlTypes.SqlString CompanyName=new System.Data.SqlTypes.SqlString("%");
			System.Data.SqlTypes.SqlString ContactName=new System.Data.SqlTypes.SqlString("%");

			if (this.txtCompanyName.Text.Trim().Length !=0)
			{
				CompanyName=new System.Data.SqlTypes.SqlString(this.txtCompanyName.Text+"%");
			}

			if (this.txtContactName.Text.Trim().Length !=0)
			{
				ContactName=new System.Data.SqlTypes.SqlString(this.txtContactName.Text+"%");
			}

			this.winListBoxCustom_spS_xSearchCustomer1.Initialize(this.ConnectionString ,"ID1", "Display", CompanyName,ContactName);
			this.winListBoxCustom_spS_xSearchCustomer1.RefreshData();	
			this.Cursor=Cursors.Default;	

//			if (this.winListBoxCustom_spS_xSearchCustomer1.Items.Count==0)
//			{
//				((Bob.Windows.Forms.MDI) this.MdiParent).ShowNotification("No Customers found with the current Search criteria!");
//			}
		}

		private void cmdSearch_Click(object sender, System.EventArgs e)
		{
			PerformSearch();		 
		}

		private void EditCustomer()
		{
			Bob.Windows.Forms.MDI.EditCustomer(this.MdiParent,this.ConnectionString,this.winListBoxCustom_spS_xSearchCustomer1.SelectedRecordID);
			this.Cursor = Cursors.WaitCursor;
			this.winListBoxCustom_spS_xSearchCustomer1.RefreshData();
			this.Cursor=Cursors.Default;	
		}

		private void AddCustomer()
		{
			Bob.Windows.Forms.MDI.AddNewCustomer(this.MdiParent,this.ConnectionString);
			this.Cursor = Cursors.WaitCursor;
			this.winListBoxCustom_spS_xSearchCustomer1.RefreshData();
			this.Cursor=Cursors.Default;	
		}

		private void winListBoxCustom_spS_xSearchCustomer1_DoubleClick(object sender, System.EventArgs e)
		{
			EditCustomer();
		}

		
		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			AddCustomer();
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			EditCustomer();
		}

		private System.Drawing.Point RightClickPoint;

		private void winListBoxCustom_spS_xSearchCustomer1_MouseDown( object sender,MouseEventArgs e)
		{
			RightClickPoint=new System.Drawing.Point(e.X,e.Y);
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Control Control =this.winListBoxCustom_spS_xSearchCustomer1.GetChildAtPoint(RightClickPoint);
			
		}

		private void SearchTextChanged(object sender, System.EventArgs e)
		{
			PerformSearch();
		}

		private void SearchCustomers_Load(object sender, System.EventArgs e)
		{
			
			PerformSearch();	
		}

		private void mnuNew_Click(object sender, System.EventArgs e)
		{
			AddCustomer();
		}

		
	
	}
}
