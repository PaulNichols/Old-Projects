namespace Discovery.NonWorkingDayTest
{
    partial class frmGetNWDSByRegionWithinDateRange
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
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.lblNoOfDays = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtNoOfDays = new System.Windows.Forms.TextBox();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnGetDetails = new System.Windows.Forms.Button();
            this.nonWorkingDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nonWorkingDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WarehouseCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblDateFrom.Location = new System.Drawing.Point(49, 56);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(56, 13);
            this.lblDateFrom.TabIndex = 0;
            this.lblDateFrom.Text = "Date From";
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.Location = new System.Drawing.Point(111, 53);
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Size = new System.Drawing.Size(100, 20);
            this.txtDateFrom.TabIndex = 1;
            // 
            // lblNoOfDays
            // 
            this.lblNoOfDays.AutoSize = true;
            this.lblNoOfDays.Location = new System.Drawing.Point(230, 60);
            this.lblNoOfDays.Name = "lblNoOfDays";
            this.lblNoOfDays.Size = new System.Drawing.Size(65, 13);
            this.lblNoOfDays.TabIndex = 2;
            this.lblNoOfDays.Text = "No. Of Days";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(435, 60);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 3;
            this.lblRegion.Text = "OptrakRegion";
            // 
            // txtNoOfDays
            // 
            this.txtNoOfDays.Location = new System.Drawing.Point(304, 57);
            this.txtNoOfDays.Name = "txtNoOfDays";
            this.txtNoOfDays.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfDays.TabIndex = 4;
            // 
            // txtRegion
            // 
            this.txtRegion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRegion.Location = new System.Drawing.Point(489, 56);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(100, 20);
            this.txtRegion.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nonWorkingDateDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.WarehouseCode,
            this.warehouseIdDataGridViewTextBoxColumn,
            this.updatedDateDataGridViewTextBoxColumn,
            this.updatedByDataGridViewTextBoxColumn,
            this.checkSumDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.nonWorkingDayBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(817, 121);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnGetDetails
            // 
            this.btnGetDetails.Location = new System.Drawing.Point(584, 251);
            this.btnGetDetails.Name = "btnGetDetails";
            this.btnGetDetails.Size = new System.Drawing.Size(75, 23);
            this.btnGetDetails.TabIndex = 7;
            this.btnGetDetails.Text = "Get Details";
            this.btnGetDetails.UseVisualStyleBackColor = true;
            this.btnGetDetails.Click += new System.EventHandler(this.btnGetDetails_Click);
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
            // WarehouseCode
            // 
            this.WarehouseCode.DataPropertyName = "WarehouseCode";
            this.WarehouseCode.HeaderText = "WarehouseCode";
            this.WarehouseCode.Name = "WarehouseCode";
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
            // frmGetNWDSByRegionWithinDateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 312);
            this.Controls.Add(this.btnGetDetails);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.txtNoOfDays);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.lblNoOfDays);
            this.Controls.Add(this.txtDateFrom);
            this.Controls.Add(this.lblDateFrom);
            this.Name = "frmGetNWDSByRegionWithinDateRange";
            this.Text = "GetNWDSByRegionWithinDateRange";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.TextBox txtDateFrom;
        private System.Windows.Forms.Label lblNoOfDays;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox txtNoOfDays;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnGetDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nonWorkingDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WarehouseCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource nonWorkingDayBindingSource;
    }
}