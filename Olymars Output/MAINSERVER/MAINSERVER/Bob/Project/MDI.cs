using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bob.Windows.Forms {

	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class MDI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuCustomers;
		private System.Windows.Forms.MenuItem mnuCustomerSearch;
		private System.Windows.Forms.MenuItem mnuCustomerAdd;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuTitle;
		private VbPowerPack.NotificationWindow notificationWindow1;
		private VbPowerPack.TaskPane taskPane1;
		private VbPowerPack.UtilityToolBar utilityToolBar1;
		private VbPowerPack.TaskFrame taskFrame2;
		private VbPowerPack.TaskPane taskPane2;
		private VbPowerPack.TaskFrame taskFrame1;
		private VbPowerPack.TaskFrame taskFrame3;
		private VbPowerPack.TaskFrame taskFrame4;

		private string ConnectionString = String.Empty;

		public MDI(string ConnectionString)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

		
			this.ConnectionString= ConnectionString;
		
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{

			string connectionString = System.Configuration.ConfigurationSettings.AppSettings["Bob ConnectionString"];

			if (connectionString == null || connectionString == String.Empty) 
			{

				Bob.Windows.Forms.WinForm_DBConnection oWinForm_DBConnection = new Bob.Windows.Forms.WinForm_DBConnection();
				oWinForm_DBConnection.InitializeData("Bob connection", @"MAINSERVER\MAINSERVER", "Bob");
				oWinForm_DBConnection.ShowDialog(null);
				if (oWinForm_DBConnection.DialogResult == DialogResult.OK) 
				{

					connectionString = oWinForm_DBConnection.ConnectionString;
					oWinForm_DBConnection.Dispose();
					Application.Run(new MDI(connectionString));
				}
			}
			else 
			{

				Application.Run(new MDI(connectionString));
			}
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MDI));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuCustomers = new System.Windows.Forms.MenuItem();
			this.mnuCustomerSearch = new System.Windows.Forms.MenuItem();
			this.mnuCustomerAdd = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuTitle = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.notificationWindow1 = new VbPowerPack.NotificationWindow(this.components);
			this.taskPane1 = new VbPowerPack.TaskPane();
			this.taskFrame2 = new VbPowerPack.TaskFrame();
			this.taskPane2 = new VbPowerPack.TaskPane();
			this.taskFrame1 = new VbPowerPack.TaskFrame();
			this.taskFrame3 = new VbPowerPack.TaskFrame();
			this.taskFrame4 = new VbPowerPack.TaskFrame();
			this.taskPane1.SuspendLayout();
			this.taskPane2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFile,
																					  this.menuItem2,
																					  this.menuItem1});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuCustomers});
			this.mnuFile.Text = "&File";
			// 
			// mnuCustomers
			// 
			this.mnuCustomers.Index = 0;
			this.mnuCustomers.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuCustomerSearch,
																						 this.mnuCustomerAdd});
			this.mnuCustomers.Text = "Customers";
			// 
			// mnuCustomerSearch
			// 
			this.mnuCustomerSearch.Index = 0;
			this.mnuCustomerSearch.Text = "Search";
			this.mnuCustomerSearch.Click += new System.EventHandler(this.mnuCustomerSearch_Click);
			// 
			// mnuCustomerAdd
			// 
			this.mnuCustomerAdd.Index = 1;
			this.mnuCustomerAdd.Text = "Add";
			this.mnuCustomerAdd.Click += new System.EventHandler(this.mnuCustomerAdd_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuTitle});
			this.menuItem2.Text = "Reference Data";
			// 
			// mnuTitle
			// 
			this.mnuTitle.Index = 0;
			this.mnuTitle.Text = "Titles";
			this.mnuTitle.Click += new System.EventHandler(this.mnuTitle_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.MdiList = true;
			this.menuItem1.Text = "&Window";
			// 
			// notificationWindow1
			// 
			this.notificationWindow1.Blend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.Window);
			this.notificationWindow1.DefaultText = null;
			this.notificationWindow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.notificationWindow1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.notificationWindow1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.notificationWindow1.ShowStyle = VbPowerPack.NotificationShowStyle.Slide;
			// 
			// taskPane1
			// 
			this.taskPane1.AutoScroll = true;
			this.taskPane1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.taskPane1.Controls.Add(this.taskFrame2);
			this.taskPane1.CornerStyle = VbPowerPack.TaskFrameCornerStyle.SystemDefault;
			this.taskPane1.Dock = System.Windows.Forms.DockStyle.Left;
			this.taskPane1.Location = new System.Drawing.Point(0, 42);
			this.taskPane1.Name = "taskPane1";
			this.taskPane1.Size = new System.Drawing.Size(120, 409);
			this.taskPane1.TabIndex = 1;
			// 
			// taskFrame2
			// 
			this.taskFrame2.AllowDrop = true;
			this.taskFrame2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.taskFrame2.CaptionBlend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Horizontal, System.Drawing.SystemColors.Window, System.Drawing.Color.FromArgb(((System.Byte)(160)), ((System.Byte)(160)), ((System.Byte)(160))));
			this.taskFrame2.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption;
			this.taskFrame2.Location = new System.Drawing.Point(-12, 154);
			this.taskFrame2.Name = "taskFrame2";
			this.taskFrame2.Size = new System.Drawing.Size(144, 100);
			this.taskFrame2.TabIndex = 4;
			this.taskFrame2.Text = "OutStanding Quotes";
			// 
			// taskPane2
			// 
			this.taskPane2.AutoScroll = true;
			this.taskPane2.BackColor = System.Drawing.Color.LightSteelBlue;
			this.taskPane2.Controls.Add(this.taskFrame1);
			this.taskPane2.Controls.Add(this.taskFrame3);
			this.taskPane2.Controls.Add(this.taskFrame4);
			this.taskPane2.CornerStyle = VbPowerPack.TaskFrameCornerStyle.SystemDefault;
			this.taskPane2.Dock = System.Windows.Forms.DockStyle.Left;
			this.taskPane2.Location = new System.Drawing.Point(0, 0);
			this.taskPane2.Name = "taskPane2";
			this.taskPane2.Size = new System.Drawing.Size(216, 491);
			this.taskPane2.TabIndex = 10;
			// 
			// taskFrame1
			// 
			this.taskFrame1.AllowDrop = true;
			this.taskFrame1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.taskFrame1.CaptionBlend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.BackwardDiagonal, System.Drawing.Color.LightSkyBlue, System.Drawing.SystemColors.HotTrack);
			this.taskFrame1.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption;
			this.taskFrame1.Location = new System.Drawing.Point(12, 33);
			this.taskFrame1.Name = "taskFrame1";
			this.taskFrame1.Size = new System.Drawing.Size(192, 100);
			this.taskFrame1.TabIndex = 1;
			this.taskFrame1.Text = "taskFrame1";
			// 
			// taskFrame3
			// 
			this.taskFrame3.AllowDrop = true;
			this.taskFrame3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.taskFrame3.CaptionBlend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.BackwardDiagonal, System.Drawing.Color.LightSkyBlue, System.Drawing.SystemColors.HotTrack);
			this.taskFrame3.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption;
			this.taskFrame3.Location = new System.Drawing.Point(12, 166);
			this.taskFrame3.Name = "taskFrame3";
			this.taskFrame3.Size = new System.Drawing.Size(192, 100);
			this.taskFrame3.TabIndex = 3;
			this.taskFrame3.Text = "taskFrame3";
			// 
			// taskFrame4
			// 
			this.taskFrame4.AllowDrop = true;
			this.taskFrame4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.taskFrame4.CaptionBlend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.BackwardDiagonal, System.Drawing.Color.LightSkyBlue, System.Drawing.SystemColors.HotTrack);
			this.taskFrame4.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption;
			this.taskFrame4.Location = new System.Drawing.Point(12, 299);
			this.taskFrame4.Name = "taskFrame4";
			this.taskFrame4.Size = new System.Drawing.Size(192, 100);
			this.taskFrame4.TabIndex = 5;
			this.taskFrame4.Text = "taskFrame4";
			// 
			// MDI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.HotTrack;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(698, 491);
			this.Controls.Add(this.taskPane2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "MDI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BOB";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MDI_Load);
			this.taskPane1.ResumeLayout(false);
			this.taskPane2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void mnuCustomerAdd_Click(object sender, System.EventArgs e)
		{
			AddNewCustomer(this,this.ConnectionString);
		}

		public static void AddNewCustomer(System.Windows.Forms.IWin32Window caller,string connectionString)
		{
			Bob.Windows.Forms.SubClasses.Customer frmCustomer=new Bob.Windows.Forms.SubClasses.Customer();
			((Bob.Windows.Forms.MDI) caller).MakeControlsTransparent(frmCustomer);
			((Bob.Windows.Forms.MDI) caller).AddToolBar(frmCustomer);
			frmCustomer.AddNewRecord(caller,"Add a New Customer",connectionString);
		}

		public static void EditCustomer(System.Windows.Forms.IWin32Window caller,string connectionString,object customerIdentifier)
		{
            Bob.Windows.Forms.SubClasses.Customer frmCustomer=new Bob.Windows.Forms.SubClasses.Customer();
			((Bob.Windows.Forms.MDI) caller).MakeControlsTransparent(frmCustomer);
			((Bob.Windows.Forms.MDI) caller).AddToolBar(frmCustomer);
			frmCustomer.EditExistingRecord(caller,"Edit Customer: "+ customerIdentifier,connectionString,new System.Data.SqlTypes.SqlInt32((System.Int32)customerIdentifier));
		}

		private void mnuCustomerSearch_Click(object sender, System.EventArgs e)
		{
			ShowForm(new Bob.Windows.Forms.SubClasses.SearchCustomers(ConnectionString));
		}

		internal void ShowNotification(string text)
		{
			this.notificationWindow1.Notify(text);
		}

		private void MDI_Load(object sender, System.EventArgs e)
		{
		
		}

		private void mnuTitle_Click(object sender, System.EventArgs e)
		{
			this.ShowForm(new Bob.Windows.Forms.SubClasses.Reference_Data.Titles(ConnectionString));
		}

		private void ShowForm(System.Windows.Forms.Form FormToShow)
		{
			this.MakeControlsTransparent(FormToShow);
			this.AddToolBar(FormToShow);
			FormToShow.MdiParent=this;
			FormToShow.Show();
		}

		private void MakeControlsTransparent(System.Windows.Forms.Form FormToManipulate)
		{
			VbPowerPack.BlendPanel BlendPanel=new  VbPowerPack.BlendPanel();
			BlendPanel.Dock=System.Windows.Forms.DockStyle.Fill;
		
			for (int i=0 ; i<FormToManipulate.Controls.Count;i++)
			{
				if (FormToManipulate.Controls[i].GetType().Equals(typeof(Label)) || FormToManipulate.Controls[i].GetType().Equals(typeof(System.Windows.Forms.CheckBox)))
					FormToManipulate.Controls[i].BackColor=System.Drawing.Color.Transparent;
			
			}
				
			while (FormToManipulate.Controls.Count>0)
			{
				BlendPanel.Controls.Add(FormToManipulate.Controls[0]);
			}

			BlendPanel.Blend = new VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.LightSkyBlue, System.Drawing.SystemColors.HotTrack);
			FormToManipulate.Controls.Add(BlendPanel);

		}

		private void AddToolBar(System.Windows.Forms.Form FormToAddToolBarTo)
		{
			VbPowerPack.UtilityToolBar utilityToolBar2=new VbPowerPack.UtilityToolBar();
			VbPowerPack.BackUtilityButton backUtilityButton1=new VbPowerPack.BackUtilityButton() ;
			VbPowerPack.ForwardUtilityButton forwardUtilityButton1=new VbPowerPack.ForwardUtilityButton();
			VbPowerPack.SearchUtilityButton searchUtilityButton1=new VbPowerPack.SearchUtilityButton();
			VbPowerPack.PrintUtilityButton printUtilityButton1=new VbPowerPack.PrintUtilityButton();
			VbPowerPack.DeleteUtilityButton deleteUtilityButton1=new VbPowerPack.DeleteUtilityButton();
			VbPowerPack.MailUtilityButton mailUtilityButton1=new VbPowerPack.MailUtilityButton();


			// 
			// utilityToolBar2
			// 
			utilityToolBar2.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			utilityToolBar2.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  backUtilityButton1,
																						  forwardUtilityButton1,
																						  searchUtilityButton1,
																						  printUtilityButton1,
																						  deleteUtilityButton1,
																						  mailUtilityButton1});
			utilityToolBar2.Cursor = System.Windows.Forms.Cursors.Hand;
			utilityToolBar2.DropDownArrows = true;
			utilityToolBar2.IconOptions = VbPowerPack.UtilityToolBarIconOptions.SmallIcons;
			utilityToolBar2.Location = new System.Drawing.Point(0, 0);
			utilityToolBar2.Name = "utilityToolBar2";
			utilityToolBar2.ShowText = VbPowerPack.UtilityToolBarShowText.ShowTextLabels;
			utilityToolBar2.Size = new System.Drawing.Size(704, 42);
			utilityToolBar2.TabIndex = 8;
			utilityToolBar2.TextAlign = false;
			// 
			// backUtilityButton1
			// 
			backUtilityButton1.ImageIndex = 0;
			backUtilityButton1.Text = "Back";
			// 
			// forwardUtilityButton1
			// 
			forwardUtilityButton1.ImageIndex = 1;
			forwardUtilityButton1.Text = "Forward";
			// 
			// searchUtilityButton1
			// 
			searchUtilityButton1.ImageIndex = 6;
			searchUtilityButton1.Text = "Search";
			// 
			// printUtilityButton1
			// 
			printUtilityButton1.ImageIndex = 19;
			printUtilityButton1.Text = "Print";
			// 
			// deleteUtilityButton1
			// 
			deleteUtilityButton1.ImageIndex = 25;
			deleteUtilityButton1.Text = "Delete";
			// 
			// mailUtilityButton1
			// 
			mailUtilityButton1.ImageIndex = 28;
			mailUtilityButton1.Text = "Mail";

			FormToAddToolBarTo.Controls.Add(utilityToolBar2);

		}

	}
}
