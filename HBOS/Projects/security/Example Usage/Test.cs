using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Security.Permissions;
using System.Security.Principal;

using HBOS.FS.AMP.Security;

namespace Example
{
	/// <summary>
	/// Summary description for Test.
	/// </summary>
    public class TestForm : HBOS.FS.AMP.Security.RoleCheckedForm
	{
        private System.Windows.Forms.Label userNameDescription;
        private System.Windows.Forms.Button close;
        
        [PermittedRoles("Administrator")]
        private System.Windows.Forms.CheckBox isAdministrator;
        
        [PermittedRoles("Administrator,Users")]
        private System.Windows.Forms.CheckBox isUser;
        
        [PermittedRoles("Superhero")]
        private System.Windows.Forms.CheckBox isSuperHero;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem fileMenu;
        
        [PermittedRoles("Superhero")]
        private System.Windows.Forms.MenuItem openItem;
        private System.Windows.Forms.Button button1;
		
        
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


            IPrincipal currentPrincipal = Thread.CurrentPrincipal;

            this.userNameDescription.Text += currentPrincipal.Identity.Name;
            
            isAdministrator.Checked = currentPrincipal.IsInRole("Administrator");
            isUser.Checked = currentPrincipal.IsInRole("User");
            isSuperHero.Checked = currentPrincipal.IsInRole("SuperHero");


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
            this.userNameDescription = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.isAdministrator = new System.Windows.Forms.CheckBox();
            this.isUser = new System.Windows.Forms.CheckBox();
            this.isSuperHero = new System.Windows.Forms.CheckBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.openItem = new System.Windows.Forms.MenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameDescription
            // 
            this.userNameDescription.Location = new System.Drawing.Point(8, 16);
            this.userNameDescription.Name = "userNameDescription";
            this.userNameDescription.Size = new System.Drawing.Size(368, 23);
            this.userNameDescription.TabIndex = 0;
            this.userNameDescription.Text = "This form is run with a user context of ";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(8, 168);
            this.close.Name = "close";
            this.close.TabIndex = 1;
            this.close.Text = "close";
            this.close.Click += new System.EventHandler(this.Close_OnClick);
            // 
            // isAdministrator
            // 
            this.isAdministrator.Enabled = false;
            this.isAdministrator.Location = new System.Drawing.Point(8, 48);
            this.isAdministrator.Name = "isAdministrator";
            this.isAdministrator.TabIndex = 2;
            this.isAdministrator.Text = "IsAdministrator";
            // 
            // isUser
            // 
            this.isUser.Enabled = false;
            this.isUser.Location = new System.Drawing.Point(8, 80);
            this.isUser.Name = "isUser";
            this.isUser.TabIndex = 3;
            this.isUser.Text = "IsUser";
            // 
            // isSuperHero
            // 
            this.isSuperHero.Enabled = false;
            this.isSuperHero.Location = new System.Drawing.Point(8, 112);
            this.isSuperHero.Name = "isSuperHero";
            this.isSuperHero.TabIndex = 4;
            this.isSuperHero.Text = "IsSuperHero";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.fileMenu});
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.openItem});
            this.fileMenu.Text = "File";
            // 
            // openItem
            // 
            this.openItem.Index = 0;
            this.openItem.Text = "Open";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 48);
            this.button1.Name = "button1";
            this.button1.TabIndex = 5;
            this.button1.Text = "Crash!";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(384, 206);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.isSuperHero);
            this.Controls.Add(this.isUser);
            this.Controls.Add(this.isAdministrator);
            this.Controls.Add(this.close);
            this.Controls.Add(this.userNameDescription);
            this.Menu = this.mainMenu1;
            this.Name = "TestForm";
            this.Text = "Test";
            this.ResumeLayout(false);

        }
		#endregion

        private void Close_OnClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        [PrincipalPermission(SecurityAction.Demand,
             Role="SuperHero")]
        private void button1_Click(object sender, System.EventArgs e)
        {
        
        }
	}
}
