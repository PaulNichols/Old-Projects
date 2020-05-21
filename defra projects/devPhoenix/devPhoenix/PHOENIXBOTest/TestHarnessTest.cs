using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using uk.gov.defra.Phoenix.BOTest;
using uk.gov.defra.Phoenix.BO.Application.Bird.Registration;

namespace uk.gov.defra.Phoenix.BOTest
{
	/// <summary>
	/// Summary description for TestHarnessTest.
	/// </summary>
	public class TestHarnessTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		[STAThread]
		static void Main() 
		{
			System.Windows.Forms.Application.Run(new TestHarnessTest());
		}

		public TestHarnessTest()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(64, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(200, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Test Cites Tests";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(64, 72);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(200, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Test Bird Tests";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(64, 112);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(200, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Test Taxonomy Tests";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// TestHarnessTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 213);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "TestHarnessTest";
			this.Text = "TestHarnessTest";
			this.ResumeLayout(false);

		}
		#endregion

		

		private void button1_Click(object sender, System.EventArgs e)
		{
			uk.gov.defra.Phoenix.BOTest.Application.CITES.CitesTestsTest TestClass=new uk.gov.defra.Phoenix.BOTest.Application.CITES.CitesTestsTest();
			TestClass.RunTest();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			BirdRegistration NewApp = new BirdRegistration(1, BirdRegLoadMode.LoadByParty, 1275, "");

//			BirdRegistration birds = CreateApp(RegistrationApplicationType.Imported);
//			int OwnerId = birds.OwnerId;
//			birds.Submit();
//
//			birds = new BO.Application.Bird.Registration.BirdRegistration(birds.ApplicationId);
//			int Owner2 = birds.OwnerId;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			uk.gov.defra.Phoenix.BOTest.TaxonomySearch  TestClass=new uk.gov.defra.Phoenix.BOTest.TaxonomySearch();
			
		}
	}
}
