namespace Discovery.NonWorkingDayTest
{
    partial class frmGetNWDS
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
            this.grdvNonWorkingDay = new System.Windows.Forms.DataGridView();
            this.bntGetDetails = new System.Windows.Forms.Button();
            this.nonWorkingDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nonWorkingDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdvNonWorkingDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grdvNonWorkingDay
            // 
            this.grdvNonWorkingDay.AutoGenerateColumns = false;
            this.grdvNonWorkingDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvNonWorkingDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nonWorkingDateDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.warehouseIdDataGridViewTextBoxColumn,
            this.updatedDateDataGridViewTextBoxColumn,
            this.updatedByDataGridViewTextBoxColumn,
            this.checkSumDataGridViewTextBoxColumn});
            this.grdvNonWorkingDay.DataSource = this.nonWorkingDayBindingSource;
            this.grdvNonWorkingDay.Location = new System.Drawing.Point(30, 39);
            this.grdvNonWorkingDay.Name = "grdvNonWorkingDay";
            this.grdvNonWorkingDay.Size = new System.Drawing.Size(738, 150);
            this.grdvNonWorkingDay.TabIndex = 0;
            // 
            // bntGetDetails
            // 
            this.bntGetDetails.Location = new System.Drawing.Point(189, 242);
            this.bntGetDetails.Name = "bntGetDetails";
            this.bntGetDetails.Size = new System.Drawing.Size(75, 23);
            this.bntGetDetails.TabIndex = 1;
            this.bntGetDetails.Text = "Get Details";
            this.bntGetDetails.UseVisualStyleBackColor = true;
            this.bntGetDetails.Click += new System.EventHandler(this.bntGetDetails_Click);
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
            // frmGetNWDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 298);
            this.Controls.Add(this.bntGetDetails);
            this.Controls.Add(this.grdvNonWorkingDay);
            this.Name = "frmGetNWDS";
            this.Text = "GetNWDS";
            ((System.ComponentModel.ISupportInitialize)(this.grdvNonWorkingDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdvNonWorkingDay;
        private System.Windows.Forms.Button bntGetDetails;
        private System.Windows.Forms.BindingSource nonWorkingDayBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nonWorkingDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkSumDataGridViewTextBoxColumn;
    }
}