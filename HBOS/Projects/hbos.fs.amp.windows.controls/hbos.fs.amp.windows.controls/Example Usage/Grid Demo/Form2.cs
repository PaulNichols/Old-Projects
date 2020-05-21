using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grid_Demo
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private HBOS.FS.AMP.Windows.Controls.DataGrid dataGrid1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			FundCollection funds = new FundCollection();
			for (int i=0; i<5; i++)
			{
				Fund newFund = new Fund("HiPo" + i, "Full " + i, "Short " + i);
				funds.Add(newFund);
			}

			//            DataGridTableStyle style = new DataGridTableStyle();
			//            
			//            DataGridColumnStyle col1 = new DataGridTextBoxColumn();
			//            col1.HeaderText = "HiPortfolio 3 code";
			//            col1.MappingName = "HiPortFolioCode";
			//            
			//            style.GridColumnStyles.Add(col1);
			//            dataGrid1.TableStyles.Add(style);
            
			dataGrid1.BindToCustomCollection(funds, System.Type.GetType("Grid_Demo.Fund"));
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
			this.dataGrid1 = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.GridLineColor = System.Drawing.Color.Black;
			this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(32, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.PrintColumnWidths = null;
			this.dataGrid1.Size = new System.Drawing.Size(1376, 296);
			this.dataGrid1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(256, 360);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(1424, 472);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form2";
			this.Text = "Form2";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			FundCollection newFunds = (FundCollection)dataGrid1.RetrieveUpdatedCustomCollection(System.Type.GetType("Grid_Demo.Fund"));
			FundCollection oldFunds = (FundCollection)dataGrid1.RetrieveOriginalCustomCollection();
			for (int i=0; i<newFunds.Count; i++)
			{
				Fund currentNewFund = newFunds[i];
				SomethingCollection things = newFunds[i].TheCustomCollection;
				for (int j=0; j<things.Count; j++)
				{
					Console.WriteLine(things[j].String1 + " " + things[j].String2);

				}
			}
		}
	}
}
