using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bob.Windows.Forms.Reference_Data.BasesClasses
{
	/// <summary>
	/// Summary description for BaseReference.
	/// </summary>
	public class BaseReference : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdNew;
		private System.Windows.Forms.Button cmdEdit;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button cmdRefresh;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BaseReference()
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
			this.cmdNew = new System.Windows.Forms.Button();
			this.cmdEdit = new System.Windows.Forms.Button();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdRefresh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdNew
			// 
			this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNew.Location = new System.Drawing.Point(224, 232);
			this.cmdNew.Name = "cmdNew";
			this.cmdNew.TabIndex = 0;
			this.cmdNew.Text = "Add New";
			this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
			// 
			// cmdEdit
			// 
			this.cmdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdEdit.Location = new System.Drawing.Point(304, 232);
			this.cmdEdit.Name = "cmdEdit";
			this.cmdEdit.TabIndex = 1;
			this.cmdEdit.Text = "Edit";
			this.cmdEdit.Click += new System.EventHandler(this.button2_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDelete.Location = new System.Drawing.Point(144, 232);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.TabIndex = 1;
			this.cmdDelete.Text = "Delete";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			// 
			// cmdRefresh
			// 
			this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRefresh.Location = new System.Drawing.Point(8, 232);
			this.cmdRefresh.Name = "cmdRefresh";
			this.cmdRefresh.TabIndex = 1;
			this.cmdRefresh.Text = "Refresh";
			this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
			// 
			// BaseReference
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(386, 263);
			this.Controls.Add(this.cmdEdit);
			this.Controls.Add(this.cmdNew);
			this.Controls.Add(this.cmdDelete);
			this.Controls.Add(this.cmdRefresh);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "BaseReference";
			this.ShowInTaskbar = false;
			this.Text = "BaseReference";
			this.ResumeLayout(false);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			OnEdit();
		}

		private void cmdNew_Click(object sender, System.EventArgs e)
		{
			OnNew();
		}

		protected virtual void   OnNew()
		{

		}

		protected virtual void   OnDelete()
		{

		}

		protected virtual void   OnEdit()
		{

		}

		protected virtual void   OnRefresh()
		{

		}

		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			OnDelete();
		}

		private void cmdRefresh_Click(object sender, System.EventArgs e)
		{
			OnRefresh();
		}
	}
}
