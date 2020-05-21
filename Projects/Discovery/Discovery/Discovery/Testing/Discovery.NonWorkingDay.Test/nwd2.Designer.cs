namespace Discovery.NonWorkingDayTest
{
    partial class frmNWD2
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
            this.lblWarehouseCode = new System.Windows.Forms.Label();
            this.lblNonWorkingDate = new System.Windows.Forms.Label();
            this.txtNonWorkingDate = new System.Windows.Forms.TextBox();
            this.nwd2bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtWarehouseCode = new System.Windows.Forms.TextBox();
            this.lblWarehouseId = new System.Windows.Forms.Label();
            this.txtWarehouseId = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnGetDetails = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nwd2bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWarehouseCode
            // 
            this.lblWarehouseCode.AutoSize = true;
            this.lblWarehouseCode.Location = new System.Drawing.Point(38, 30);
            this.lblWarehouseCode.Name = "lblWarehouseCode";
            this.lblWarehouseCode.Size = new System.Drawing.Size(90, 13);
            this.lblWarehouseCode.TabIndex = 0;
            this.lblWarehouseCode.Text = "Warehouse Code";
            // 
            // lblNonWorkingDate
            // 
            this.lblNonWorkingDate.AutoSize = true;
            this.lblNonWorkingDate.Location = new System.Drawing.Point(38, 67);
            this.lblNonWorkingDate.Name = "lblNonWorkingDate";
            this.lblNonWorkingDate.Size = new System.Drawing.Size(30, 13);
            this.lblNonWorkingDate.TabIndex = 2;
            this.lblNonWorkingDate.Text = "Date";
            // 
            // txtNonWorkingDate
            // 
            this.txtNonWorkingDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nwd2bindingSource, "NonWorkingDate", true));
            this.txtNonWorkingDate.Location = new System.Drawing.Point(134, 64);
            this.txtNonWorkingDate.Name = "txtNonWorkingDate";
            this.txtNonWorkingDate.Size = new System.Drawing.Size(100, 20);
            this.txtNonWorkingDate.TabIndex = 3;
            // 
            // nwd2bindingSource
            // 
            this.nwd2bindingSource.DataSource = typeof(Discovery.BusinessObjects.NonWorkingDay);
            // 
            // txtWarehouseCode
            // 
            this.txtWarehouseCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nwd2bindingSource, "WarehouseCode", true));
            this.txtWarehouseCode.Location = new System.Drawing.Point(134, 27);
            this.txtWarehouseCode.Name = "txtWarehouseCode";
            this.txtWarehouseCode.Size = new System.Drawing.Size(100, 20);
            this.txtWarehouseCode.TabIndex = 1;
            // 
            // lblWarehouseId
            // 
            this.lblWarehouseId.AutoSize = true;
            this.lblWarehouseId.Location = new System.Drawing.Point(38, 112);
            this.lblWarehouseId.Name = "lblWarehouseId";
            this.lblWarehouseId.Size = new System.Drawing.Size(74, 13);
            this.lblWarehouseId.TabIndex = 4;
            this.lblWarehouseId.Text = "Warehouse Id";
            // 
            // txtWarehouseId
            // 
            this.txtWarehouseId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nwd2bindingSource, "WarehouseId", true));
            this.txtWarehouseId.Location = new System.Drawing.Point(134, 109);
            this.txtWarehouseId.Name = "txtWarehouseId";
            this.txtWarehouseId.Size = new System.Drawing.Size(100, 20);
            this.txtWarehouseId.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(38, 156);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nwd2bindingSource, "Description", true));
            this.txtDescription.Location = new System.Drawing.Point(134, 149);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 20);
            this.txtDescription.TabIndex = 7;
            // 
            // btnGetDetails
            // 
            this.btnGetDetails.Location = new System.Drawing.Point(41, 214);
            this.btnGetDetails.Name = "btnGetDetails";
            this.btnGetDetails.Size = new System.Drawing.Size(75, 23);
            this.btnGetDetails.TabIndex = 8;
            this.btnGetDetails.Text = "Get Details";
            this.btnGetDetails.UseVisualStyleBackColor = true;
            this.btnGetDetails.Click += new System.EventHandler(this.btnGetDetails_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(178, 214);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmNWD2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 269);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnGetDetails);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtWarehouseId);
            this.Controls.Add(this.lblWarehouseId);
            this.Controls.Add(this.txtWarehouseCode);
            this.Controls.Add(this.txtNonWorkingDate);
            this.Controls.Add(this.lblNonWorkingDate);
            this.Controls.Add(this.lblWarehouseCode);
            this.Name = "frmNWD2";
            this.Text = "nwd2";
            ((System.ComponentModel.ISupportInitialize)(this.nwd2bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarehouseCode;
        private System.Windows.Forms.Label lblNonWorkingDate;
        private System.Windows.Forms.TextBox txtNonWorkingDate;
        private System.Windows.Forms.TextBox txtWarehouseCode;
        private System.Windows.Forms.Label lblWarehouseId;
        private System.Windows.Forms.TextBox txtWarehouseId;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnGetDetails;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.BindingSource nwd2bindingSource;
    }
}