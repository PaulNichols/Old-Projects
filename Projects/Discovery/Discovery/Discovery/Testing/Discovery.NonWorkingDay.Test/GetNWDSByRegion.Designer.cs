namespace Discovery.NonWorkingDayTest
{
    partial class frmGetNWDSByRegion
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
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bntgetDetails = new System.Windows.Forms.Button();
            this.nonWorkingDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nonWorkingDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(64, 35);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(56, 13);
            this.lblDateFrom.TabIndex = 0;
            this.lblDateFrom.Text = "Date From";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(254, 35);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 1;
            this.lblRegion.Text = "OptrakRegion";
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(126, 31);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Size = new System.Drawing.Size(100, 20);
            this.txtDateFrom.TabIndex = 2;
            // 
            // txtRegion
            // 
            this.txtRegion.Location = new System.Drawing.Point(301, 31);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(100, 20);
            this.txtRegion.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nonWorkingDateDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.warehouseIdDataGridViewTextBoxColumn,
            this.updatedDateDataGridViewTextBoxColumn,
            this.updatedByDataGridViewTextBoxColumn,
            this.checkSumDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.nonWorkingDayBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(745, 136);
            this.dataGridView1.TabIndex = 4;
            // 
            // bntgetDetails
            // 
            this.bntgetDetails.Location = new System.Drawing.Point(534, 249);
            this.bntgetDetails.Name = "bntgetDetails";
            this.bntgetDetails.Size = new System.Drawing.Size(75, 23);
            this.bntgetDetails.TabIndex = 5;
            this.bntgetDetails.Text = "Get Details";
            this.bntgetDetails.UseVisualStyleBackColor = true;
            this.bntgetDetails.Click += new System.EventHandler(this.bntgetDetails_Click);
            // 
            // nonWorkingDayBindingSource
            // 
            this.nonWorkingDayBindingSource.DataSource = typeof(Discovery.BusinessObjects.NonWorkingDay);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nonWorkingDateDataGridViewTextBoxColumn
            // 
            this.nonWorkingDateDataGridViewTextBoxColumn.DataPropertyName = "NonWorkingDate";
            this.nonWorkingDateDataGridViewTextBoxColumn.HeaderText = "NonWorkingDate";
            this.nonWorkingDateDataGridViewTextBoxColumn.Name = "nonWorkingDateDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // warehouseIdDataGridViewTextBoxColumn
            // 
            this.warehouseIdDataGridViewTextBoxColumn.DataPropertyName = "WarehouseId";
            this.warehouseIdDataGridViewTextBoxColumn.HeaderText = "WarehouseId";
            this.warehouseIdDataGridViewTextBoxColumn.Name = "warehouseIdDataGridViewTextBoxColumn";
            // 
            // updatedDateDataGridViewTextBoxColumn
            // 
            this.updatedDateDataGridViewTextBoxColumn.DataPropertyName = "UpdatedDate";
            this.updatedDateDataGridViewTextBoxColumn.HeaderText = "UpdatedDate";
            this.updatedDateDataGridViewTextBoxColumn.Name = "updatedDateDataGridViewTextBoxColumn";
            // 
            // updatedByDataGridViewTextBoxColumn
            // 
            this.updatedByDataGridViewTextBoxColumn.DataPropertyName = "UpdatedBy";
            this.updatedByDataGridViewTextBoxColumn.HeaderText = "UpdatedBy";
            this.updatedByDataGridViewTextBoxColumn.Name = "updatedByDataGridViewTextBoxColumn";
            // 
            // checkSumDataGridViewTextBoxColumn
            // 
            this.checkSumDataGridViewTextBoxColumn.DataPropertyName = "CheckSum";
            this.checkSumDataGridViewTextBoxColumn.HeaderText = "CheckSum";
            this.checkSumDataGridViewTextBoxColumn.Name = "checkSumDataGridViewTextBoxColumn";
            // 
            // frmNWDSByRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 313);
            this.Controls.Add(this.bntgetDetails);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.lblDateFrom);
            this.Name = "frmNWDSByRegion";
            this.Text = "GetNWDSByRegion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox txtDateFrom;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bntgetDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nonWorkingDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource nonWorkingDayBindingSource;
    }
}