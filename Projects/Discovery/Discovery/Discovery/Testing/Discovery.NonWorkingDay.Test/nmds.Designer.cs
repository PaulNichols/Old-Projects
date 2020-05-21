namespace Discovery.NonWorkingDayTest
{
    partial class frmNWDS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.bntSave = new System.Windows.Forms.Button();
            this.txtNumberOfDays = new System.Windows.Forms.TextBox();
            this.txtUpdatedBy = new System.Windows.Forms.TextBox();
            this.lblNumberOfDays = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.lblUpdatedBy = new System.Windows.Forms.Label();
            this.discoveryRegionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.discoveryDataSet = new Discovery.NonWorkingDayTest.DiscoveryDataSet();
            this.discovery_RegionTableAdapter = new Discovery.NonWorkingDayTest.DiscoveryDataSetTableAdapters.Discovery_RegionTableAdapter();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.txtWarehouse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.discoveryRegionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discoveryDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(51, 49);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date";
            // 
            // txtStartDate
            // 
            this.txtStartDate.Location = new System.Drawing.Point(160, 45);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 20);
            this.txtStartDate.TabIndex = 1;
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(242, 269);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(127, 23);
            this.bntSave.TabIndex = 10;
            this.bntSave.Text = "Save";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // txtNumberOfDays
            // 
            this.txtNumberOfDays.Location = new System.Drawing.Point(160, 79);
            this.txtNumberOfDays.Name = "txtNumberOfDays";
            this.txtNumberOfDays.Size = new System.Drawing.Size(45, 20);
            this.txtNumberOfDays.TabIndex = 3;
            // 
            // txtUpdatedBy
            // 
            this.txtUpdatedBy.Location = new System.Drawing.Point(160, 193);
            this.txtUpdatedBy.Name = "txtUpdatedBy";
            this.txtUpdatedBy.Size = new System.Drawing.Size(121, 20);
            this.txtUpdatedBy.TabIndex = 9;
            // 
            // lblNumberOfDays
            // 
            this.lblNumberOfDays.AutoSize = true;
            this.lblNumberOfDays.Location = new System.Drawing.Point(51, 82);
            this.lblNumberOfDays.Name = "lblNumberOfDays";
            this.lblNumberOfDays.Size = new System.Drawing.Size(83, 13);
            this.lblNumberOfDays.TabIndex = 2;
            this.lblNumberOfDays.Text = "Number of Days";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(51, 129);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(52, 13);
            this.lblRegion.TabIndex = 4;
            this.lblRegion.Text = "OptrakRegion(s)";
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(51, 162);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(73, 13);
            this.lblWarehouse.TabIndex = 6;
            this.lblWarehouse.Text = "Warehouse(s)";
            // 
            // lblUpdatedBy
            // 
            this.lblUpdatedBy.AutoSize = true;
            this.lblUpdatedBy.Location = new System.Drawing.Point(51, 193);
            this.lblUpdatedBy.Name = "lblUpdatedBy";
            this.lblUpdatedBy.Size = new System.Drawing.Size(63, 13);
            this.lblUpdatedBy.TabIndex = 8;
            this.lblUpdatedBy.Text = "Updated By";
            // 
            // discoveryRegionBindingSource
            // 
            this.discoveryRegionBindingSource.DataMember = "Discovery_OptrakRegion";
            this.discoveryRegionBindingSource.DataSource = this.discoveryDataSet;
            // 
            // discoveryDataSet
            // 
            this.discoveryDataSet.DataSetName = "DiscoveryDataSet";
            this.discoveryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // discovery_RegionTableAdapter
            // 
            this.discovery_RegionTableAdapter.ClearBeforeFill = true;
            // 
            // txtRegion
            // 
            this.txtRegion.Location = new System.Drawing.Point(160, 126);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(100, 20);
            this.txtRegion.TabIndex = 5;
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.Location = new System.Drawing.Point(160, 162);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Size = new System.Drawing.Size(100, 20);
            this.txtWarehouse.TabIndex = 7;
            // 
            // frmNWDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 368);
            this.Controls.Add(this.txtWarehouse);
            this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.lblUpdatedBy);
            this.Controls.Add(this.lblWarehouse);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.lblNumberOfDays);
            this.Controls.Add(this.txtUpdatedBy);
            this.Controls.Add(this.txtNumberOfDays);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Name = "frmNWDS";
            this.Text = "nmds";
            this.Load += new System.EventHandler(this.frmNWDS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.discoveryRegionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discoveryDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.TextBox txtNumberOfDays;
        private System.Windows.Forms.TextBox txtUpdatedBy;
        private System.Windows.Forms.Label lblNumberOfDays;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.Label lblUpdatedBy;
        private DiscoveryDataSet discoveryDataSet;
        private System.Windows.Forms.BindingSource discoveryRegionBindingSource;
        private Discovery.NonWorkingDayTest.DiscoveryDataSetTableAdapters.Discovery_RegionTableAdapter discovery_RegionTableAdapter;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.TextBox txtWarehouse;
    }
}