namespace Discovery.RequestManagerClient.TestHarness
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.txtQueueName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWildCard = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumMessages = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgFolderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Queue Name:";
            // 
            // txtQueueName
            // 
            this.txtQueueName.Location = new System.Drawing.Point(112, 25);
            this.txtQueueName.Name = "txtQueueName";
            this.txtQueueName.Size = new System.Drawing.Size(210, 20);
            this.txtQueueName.TabIndex = 1;
            this.txtQueueName.Text = "lspringslt\\discovery.main.channel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtQueueName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Queue";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtWildCard);
            this.groupBox2.Controls.Add(this.btnOpenFolder);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.btnFileOpen);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNumMessages);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBody);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtSequence);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDestination);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSource);
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 400);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Request Message";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "File Wild Card:";
            // 
            // txtWildCard
            // 
            this.txtWildCard.Location = new System.Drawing.Point(112, 77);
            this.txtWildCard.Name = "txtWildCard";
            this.txtWildCard.Size = new System.Drawing.Size(57, 20);
            this.txtWildCard.TabIndex = 22;
            this.txtWildCard.Text = "*.*";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(187, 283);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(81, 24);
            this.btnOpenFolder.TabIndex = 20;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(111, 338);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(317, 48);
            this.progressBar.TabIndex = 19;
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(111, 284);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(71, 24);
            this.btnFileOpen.TabIndex = 18;
            this.btnFileOpen.Text = "Open File(s)";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "# Of Messages:";
            // 
            // txtNumMessages
            // 
            this.txtNumMessages.Location = new System.Drawing.Point(111, 312);
            this.txtNumMessages.Name = "txtNumMessages";
            this.txtNumMessages.Size = new System.Drawing.Size(57, 20);
            this.txtNumMessages.TabIndex = 17;
            this.txtNumMessages.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Body:";
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(112, 183);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(317, 94);
            this.txtBody.TabIndex = 15;
            this.txtBody.Text = "Message body...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Sequence:";
            // 
            // txtSequence
            // 
            this.txtSequence.Location = new System.Drawing.Point(112, 157);
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.Size = new System.Drawing.Size(210, 20);
            this.txtSequence.TabIndex = 11;
            this.txtSequence.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type:";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(112, 131);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(210, 20);
            this.txtType.TabIndex = 9;
            this.txtType.Text = "Shipment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 105);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 20);
            this.txtName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Destination System:";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(112, 51);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(57, 20);
            this.txtDestination.TabIndex = 5;
            this.txtDestination.Text = "MS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source System:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(112, 25);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(57, 20);
            this.txtSource.TabIndex = 3;
            this.txtSource.Text = "RHG";
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.DefaultExt = "dat";
            this.dlgFileOpen.Filter = "Dat file|*.dat";
            this.dlgFileOpen.Multiselect = true;
            // 
            // dlgFolderSelect
            // 
            this.dlgFolderSelect.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.dlgFolderSelect.ShowNewFolderButton = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 490);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Request Manager Client Test Harness";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQueueName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNumMessages;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderSelect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWildCard;
    }
}

