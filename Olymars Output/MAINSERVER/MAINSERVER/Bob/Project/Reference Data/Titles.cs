using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bob.Windows.Forms.SubClasses.Reference_Data
{
	/// <summary>
	/// Summary description for Titles.
	/// </summary>
	public class Titles :Bob.Windows.Forms.Reference_Data.BasesClasses.BaseReference
	{
		private Bob.Windows.ListBoxes.WinListBox_Title winListBox_Title1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
private String ConnectionString;

		public Titles(string connectionString )
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
			this.winListBox_Title1 = new Bob.Windows.ListBoxes.WinListBox_Title();
			this.SuspendLayout();
			// 
			// winListBox_Title1
			// 
			this.winListBox_Title1.CommandTimeOut = 30;
			this.winListBox_Title1.Location = new System.Drawing.Point(16, 24);
			this.winListBox_Title1.Name = "winListBox_Title1";
			this.winListBox_Title1.Size = new System.Drawing.Size(344, 173);
			this.winListBox_Title1.TabIndex = 0;
			// 
			// Titles
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.winListBox_Title1);
			this.Name = "Titles";
			this.Text = "Titles";
			this.Load += new System.EventHandler(this.Titles_Load);
			this.Controls.SetChildIndex(this.winListBox_Title1, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void Titles_Load(object sender, System.EventArgs e)
		{
			this.winListBox_Title1.Initialize(this.ConnectionString );
			this.winListBox_Title1.RefreshData();	
		}

		protected override void OnEdit()
		{
			Bob.Windows.Forms.WinForm_Title frmTitle=new Bob.Windows.Forms.WinForm_Title();
			frmTitle.EditExistingRecord(this,"Editing Item",ConnectionString,new System.Data.SqlTypes.SqlInt32((System.Int32)this.winListBox_Title1.SelectedRecordID));
			winListBox_Title1.RefreshData();
		}

		protected override void OnRefresh()
		{
			winListBox_Title1.RefreshData();
		}

		protected override void OnNew()
		{
			Bob.Windows.Forms.WinForm_Title frmTitle=new Bob.Windows.Forms.WinForm_Title();
			frmTitle.AddNewRecord(this,"Add a New Item",ConnectionString);
			winListBox_Title1.RefreshData();
		}

		protected override void OnDelete()
		{
		
			if (this.winListBox_Title1.SelectedIndex==-1)
			{
				return;
			}

			System.Data.SqlTypes.SqlInt32 TitleID = (System.Int32)this.winListBox_Title1.SelectedValue;
			if (TitleID.IsNull) 
			{

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
			{

				this.Cursor = Cursors.WaitCursor;

				Bob.BusinessComponents.Titles BOTitles=new Bob.BusinessComponents.Titles(ConnectionString);
				System.Int32 ItemsCount=this.winListBox_Title1.Items.Count;
				
				try
				{
					BOTitles.Remove(TitleID);
				}
				catch
				{
					MessageBox.Show("It may possible that this item is linked to other records", "Deletion Error");
					this.Cursor = Cursors.Default;
					return;
				}

				winListBox_Title1.RefreshData();

				if (ItemsCount>this.winListBox_Title1.Items.Count) 
				{					
				}
				else 
				{

					MessageBox.Show("Deletion Failed", "Deletion Error");
					/*Bob.Windows.Forms.WinForm_ErrorForm oWinForm_ErrorForm = new Bob.Windows.Forms.WinForm_ErrorForm ();
					oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteCustomers_Click", Param);
					oWinForm_ErrorForm.Dispose();*/
				}

				this.Cursor = Cursors.Default;
			}
		

			
		}

	}
}
