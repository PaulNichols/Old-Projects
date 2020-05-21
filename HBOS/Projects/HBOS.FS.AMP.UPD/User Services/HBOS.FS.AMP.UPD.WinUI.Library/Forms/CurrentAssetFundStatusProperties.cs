using System;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// CurrentFundStatusProperties - Popup for Current Fund Status
	/// </summary>
	public class CurrentAssetFundStatusProperties : Form
	{
		#region Controls

		private TabControl tabControl;
		private TabPage toleranceTabPage;
		private Label assetMovementLabel;
		private Label assetMovementValueLabel;
		private Button cancelButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		private TabPage tabAFMovement;
		private PropertiesAssetFundMovement propertiesAssetFundMovement;

		#region Variables and Constants

		// Really 5 decimal places as this is a percentage
		private const string formatAsPercentage = "p4";

		#endregion

		#region Constructor

		/// <summary>
		/// Defualt Constructor
		/// </summary>
		public CurrentAssetFundStatusProperties()
		{
			T.E();

			try
			{
				InitializeComponent();

				//
				// TODO: Add any constructor code after InitializeComponent call
				//
				this.Text = "Asset Fund Propreties";

				this.assetMovementValueLabel.Text = string.Empty;

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Overloaded constructor.  
		/// </summary>
		/// <param name="currentAssetFund">Asset Fund object</param>
		public CurrentAssetFundStatusProperties(AssetFundDecorator currentAssetFund) : this()
		{
			T.E();

			try
			{
				this.Text = currentAssetFund.FullName.Trim() + " Properties";

				// Bind tolerance controls to fund object
				this.assetMovementValueLabel.Text = currentAssetFund.AssetMovementToleranceDisplay;

				//Fill the grid with the weighted indexes
				propertiesAssetFundMovement.Populate(currentAssetFund.AssetFund);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toleranceTabPage = new System.Windows.Forms.TabPage();
            this.assetMovementValueLabel = new System.Windows.Forms.Label();
            this.assetMovementLabel = new System.Windows.Forms.Label();
            this.tabAFMovement = new System.Windows.Forms.TabPage();
            this.propertiesAssetFundMovement = new HBOS.FS.AMP.UPD.WinUI.Forms.PropertiesAssetFundMovement();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.toleranceTabPage.SuspendLayout();
            this.tabAFMovement.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.toleranceTabPage);
            this.tabControl.Controls.Add(this.tabAFMovement);
            this.tabControl.Location = new System.Drawing.Point(8, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(602, 352);
            this.tabControl.TabIndex = 0;
            // 
            // toleranceTabPage
            // 
            this.toleranceTabPage.Controls.Add(this.assetMovementValueLabel);
            this.toleranceTabPage.Controls.Add(this.assetMovementLabel);
            this.toleranceTabPage.Location = new System.Drawing.Point(4, 22);
            this.toleranceTabPage.Name = "toleranceTabPage";
            this.toleranceTabPage.Size = new System.Drawing.Size(594, 326);
            this.toleranceTabPage.TabIndex = 0;
            this.toleranceTabPage.Text = "Tolerance";
            // 
            // assetMovementValueLabel
            // 
            this.assetMovementValueLabel.Location = new System.Drawing.Point(176, 16);
            this.assetMovementValueLabel.Name = "assetMovementValueLabel";
            this.assetMovementValueLabel.Size = new System.Drawing.Size(224, 16);
            this.assetMovementValueLabel.TabIndex = 1;
            this.assetMovementValueLabel.Text = "asset movement value";
            this.assetMovementValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // assetMovementLabel
            // 
            this.assetMovementLabel.Location = new System.Drawing.Point(16, 16);
            this.assetMovementLabel.Name = "assetMovementLabel";
            this.assetMovementLabel.Size = new System.Drawing.Size(168, 32);
            this.assetMovementLabel.TabIndex = 0;
            this.assetMovementLabel.Text = "Asset Movement Tolerance :";
            // 
            // tabAFMovement
            // 
            this.tabAFMovement.Controls.Add(this.propertiesAssetFundMovement);
            this.tabAFMovement.Location = new System.Drawing.Point(4, 22);
            this.tabAFMovement.Name = "tabAFMovement";
            this.tabAFMovement.Size = new System.Drawing.Size(594, 326);
            this.tabAFMovement.TabIndex = 1;
            this.tabAFMovement.Text = "Asset Fund Movement";
            // 
            // propertiesAssetFundMovement
            // 
            this.propertiesAssetFundMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesAssetFundMovement.Location = new System.Drawing.Point(0, 0);
            this.propertiesAssetFundMovement.Name = "propertiesAssetFundMovement";
            this.propertiesAssetFundMovement.Size = new System.Drawing.Size(594, 326);
            this.propertiesAssetFundMovement.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(530, 368);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CurrentAssetFundStatusProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(618, 400);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "CurrentAssetFundStatusProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asset Fund Properties";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.toleranceTabPage.ResumeLayout(false);
            this.tabAFMovement.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Close the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			T.E();

			try
			{
				this.Close();
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}