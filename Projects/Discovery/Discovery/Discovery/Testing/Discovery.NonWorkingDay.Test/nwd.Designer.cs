namespace Discovery.NonWorkingDayTest
{
    partial class frmNWD
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
            this.bntSave = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.nonWorkingDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblDescriprion = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblWarehouseId = new System.Windows.Forms.Label();
            this.lblUpdatedBy = new System.Windows.Forms.Label();
            this.txtWarehouseId = new System.Windows.Forms.TextBox();
            this.txtUpdatedBy = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(312, 231);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(75, 23);
            this.bntSave.TabIndex = 10;
            this.bntSave.Text = "Save";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(42, 66);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(30, 13);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // txtDate
            // 
            this.txtDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nonWorkingDayBindingSource, "NonWorkingDate", true));
            this.txtDate.Location = new System.Drawing.Point(104, 59);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 20);
            this.txtDate.TabIndex = 3;
            // 
            // nonWorkingDayBindingSource
            // 
            this.nonWorkingDayBindingSource.DataSource = typeof(Discovery.BusinessObjects.NonWorkingDay);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(54, 29);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(18, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "ID";
            // 
            // txtId
            // 
            this.txtId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nonWorkingDayBindingSource, "Id", true));
            this.txtId.Location = new System.Drawing.Point(104, 22);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblDescriprion
            // 
            this.lblDescriprion.AutoSize = true;
            this.lblDescriprion.Location = new System.Drawing.Point(12, 103);
            this.lblDescriprion.Name = "lblDescriprion";
            this.lblDescriprion.Size = new System.Drawing.Size(60, 13);
            this.lblDescriprion.TabIndex = 4;
            this.lblDescriprion.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nonWorkingDayBindingSource, "Description", true));
            this.txtDescription.Location = new System.Drawing.Point(104, 96);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // lblWarehouseId
            // 
            this.lblWarehouseId.AutoSize = true;
            this.lblWarehouseId.Location = new System.Drawing.Point(15, 139);
            this.lblWarehouseId.Name = "lblWarehouseId";
            this.lblWarehouseId.Size = new System.Drawing.Size(71, 13);
            this.lblWarehouseId.TabIndex = 6;
            this.lblWarehouseId.Text = "WarehouseId";
            // 
            // lblUpdatedBy
            // 
            this.lblUpdatedBy.AutoSize = true;
            this.lblUpdatedBy.Location = new System.Drawing.Point(15, 177);
            this.lblUpdatedBy.Name = "lblUpdatedBy";
            this.lblUpdatedBy.Size = new System.Drawing.Size(63, 13);
            this.lblUpdatedBy.TabIndex = 8;
            this.lblUpdatedBy.Text = "Updated By";
            // 
            // txtWarehouseId
            // 
            this.txtWarehouseId.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nonWorkingDayBindingSource, "WarehouseId", true));
            this.txtWarehouseId.Location = new System.Drawing.Point(104, 132);
            this.txtWarehouseId.Name = "txtWarehouseId";
            this.txtWarehouseId.Size = new System.Drawing.Size(100, 20);
            this.txtWarehouseId.TabIndex = 7;
            // 
            // txtUpdatedBy
            // 
            this.txtUpdatedBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.nonWorkingDayBindingSource, "UpdatedBy", true));
            this.txtUpdatedBy.Location = new System.Drawing.Point(104, 170);
            this.txtUpdatedBy.Name = "txtUpdatedBy";
            this.txtUpdatedBy.Size = new System.Drawing.Size(100, 20);
            this.txtUpdatedBy.TabIndex = 9;
            // 
            // frmNWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 266);
            this.Controls.Add(this.txtUpdatedBy);
            this.Controls.Add(this.txtWarehouseId);
            this.Controls.Add(this.lblUpdatedBy);
            this.Controls.Add(this.lblWarehouseId);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescriprion);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.bntSave);
            this.Name = "frmNWD";
            this.Text = "nwd";
            ((System.ComponentModel.ISupportInitialize)(this.nonWorkingDayBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.BindingSource nonWorkingDayBindingSource;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblDescriprion;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblWarehouseId;
        private System.Windows.Forms.Label lblUpdatedBy;
        private System.Windows.Forms.TextBox txtWarehouseId;
        private System.Windows.Forms.TextBox txtUpdatedBy;
    }
}