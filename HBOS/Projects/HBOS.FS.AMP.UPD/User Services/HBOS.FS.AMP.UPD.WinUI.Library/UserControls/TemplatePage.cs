using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	public class TemplatePage : Microsoft.Samples.Windows.Forms.Navigation.Page
	{
        private System.Windows.Forms.Panel pageTitlePanel;
        private Microsoft.Samples.Windows.Forms.Navigation.LinkLabel helpLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox topPictureBox;
        private System.Windows.Forms.PictureBox footerPictureBox;
        private SteepValley.Windows.Forms.XPCaption pageTitleLabel;
		private System.ComponentModel.IContainer components = null;

		public TemplatePage()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TemplatePage));
			this.pageTitlePanel = new System.Windows.Forms.Panel();
			this.pageTitleLabel = new SteepValley.Windows.Forms.XPCaption();
			this.helpLink = new Microsoft.Samples.Windows.Forms.Navigation.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.footerPictureBox = new System.Windows.Forms.PictureBox();
			this.topPictureBox = new System.Windows.Forms.PictureBox();
			this.pageTitlePanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pageTitlePanel
			// 
			this.pageTitlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pageTitlePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pageTitlePanel.Controls.Add(this.pageTitleLabel);
			this.pageTitlePanel.Controls.Add(this.helpLink);
			this.pageTitlePanel.Location = new System.Drawing.Point(144, 0);
			this.pageTitlePanel.Name = "pageTitlePanel";
			this.pageTitlePanel.Size = new System.Drawing.Size(392, 72);
			this.pageTitlePanel.TabIndex = 1;
			// 
			// pageTitleLabel
			// 
			this.pageTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pageTitleLabel.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
			this.pageTitleLabel.Location = new System.Drawing.Point(0, 0);
			this.pageTitleLabel.Name = "pageTitleLabel";
			this.pageTitleLabel.Size = new System.Drawing.Size(392, 56);
			this.pageTitleLabel.TabIndex = 10;
			this.pageTitleLabel.Tag = "";
			this.pageTitleLabel.Text = "(Page title text goes here)";
			// 
			// helpLink
			// 
			this.helpLink.Location = new System.Drawing.Point(0, 56);
			this.helpLink.Name = "helpLink";
			this.helpLink.Size = new System.Drawing.Size(32, 16);
			this.helpLink.TabIndex = 5;
			this.helpLink.TabStop = true;
			this.helpLink.Text = "Help";
			this.helpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLink_LinkClicked);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.footerPictureBox);
			this.panel1.Controls.Add(this.topPictureBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(144, 312);
			this.panel1.TabIndex = 7;
			// 
			// footerPictureBox
			// 
			this.footerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.footerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("footerPictureBox.Image")));
			this.footerPictureBox.Location = new System.Drawing.Point(0, 57);
			this.footerPictureBox.Name = "footerPictureBox";
			this.footerPictureBox.Size = new System.Drawing.Size(144, 255);
			this.footerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.footerPictureBox.TabIndex = 7;
			this.footerPictureBox.TabStop = false;
			// 
			// topPictureBox
			// 
			this.topPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("topPictureBox.Image")));
			this.topPictureBox.Location = new System.Drawing.Point(0, 0);
			this.topPictureBox.Name = "topPictureBox";
			this.topPictureBox.Size = new System.Drawing.Size(144, 57);
			this.topPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.topPictureBox.TabIndex = 5;
			this.topPictureBox.TabStop = false;
			// 
			// TemplatePage
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pageTitlePanel);
			this.Name = "TemplatePage";
			this.Size = new System.Drawing.Size(536, 312);
			this.Resize += new System.EventHandler(this.TemplatePage_Resize);
			this.pageTitlePanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

        private void helpLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //this.Go(typeof(HelpPage));
        }

        private void TemplatePage_Resize(object sender, System.EventArgs e)
        {
            this.footerPictureBox.Height = (this.Bottom - this.footerPictureBox.Top);
        }

        // lock down controls but allow developers to pass text through for the title.
        public override string Text
        {
            set 
            { 
                base.Text = value;
                if (pageTitleLabel == null)
                {
                    pageTitleLabel.Hide();
                }
                else
                {
                    pageTitleLabel.Text = value;
                    pageTitleLabel.Show();
                }
            }
        }

        public string PageTitle
        {
            set 
            { 
                base.Text = value;
                if (pageTitleLabel == null)
                {
                    pageTitleLabel.Hide();
                }
                else
                {
                    pageTitleLabel.Text = value;
                    pageTitleLabel.Show();
                }
            }
        }
        public bool ShowHelp
        {
            get { return this.helpLink.Visible; }
            set { this.helpLink.Visible = value; }
        }
    }
}

