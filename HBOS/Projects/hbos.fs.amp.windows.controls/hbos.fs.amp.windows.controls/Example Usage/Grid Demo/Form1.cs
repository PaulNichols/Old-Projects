using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Grid_Demo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		#region Enum

		/// <summary>
		/// Severity Level
		/// </summary>
		private enum Severity : int
		{
			None = 0,
			Low = 1,
			Medium = 2,
			High = 3
		}

		#endregion

		#region Variables

		private DataTable m_dataTable = new DataTable( "DataTable" );
		private DataView m_dataView;

		private ArrayList m_arrayList = new ArrayList();

		private DataTable m_loopDataTable = new DataTable( "Lookup" );

		#endregion

		#region Controls

		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonPrintPreview;
		private System.Windows.Forms.Button buttonWrite;
		private HBOS.FS.AMP.Windows.Controls.DataGrid dataGrid1;
		private System.Windows.Forms.ImageList imageList1;
        private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
        private System.Windows.Forms.Button DisplayOriginalButton;
        private System.Windows.Forms.Button DisplayModifiedButton;
        private System.Windows.Forms.Button DisplayEntireButton;
        private System.Windows.Forms.Label label1;
		private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle hbosTableStyle1;
		private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle hbosTableStyle2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.ComboBox comboBoxFilter;

		#endregion

		#region Constructor

		public Form1()
		{
			InitializeComponent();
			this.dataGrid1.CopyDefaultTableStyle( this.hbosTableStyle1 );

			m_dataTable.Columns.Add( "Column1" );
			m_dataTable.Columns.Add( "Column2" );
			m_dataTable.Columns.Add( "Column3" );
			m_dataTable.Columns.Add( "Column4" );

			m_dataTable.Rows.Add( new object[] { "Zero" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero1" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One1" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two1" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three1", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero2" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One2" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two2" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three2", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero3" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One3" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two3" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three3", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero4" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One4" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two4" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three4", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Three2", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero3" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One3" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two3" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three3", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero4" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One4" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two4" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three4", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			m_dataTable.Rows.Add( new object[] { "Zero5" , 0 , "0" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "One5" , 1,  "1" , Severity.Low } );
			m_dataTable.Rows.Add( new object[] { "Two5" , 2 , "2" , Severity.Medium } );
			m_dataTable.Rows.Add( new object[] { "Three5", 3 ,  "3" , Severity.High } );
			
//			dataGridImageColumn1.EnumType = typeof(Severity);

			m_arrayList.Add( "One" );
			m_arrayList.Add( "Two" );
			m_arrayList.Add( "Three" );

			m_dataView = m_dataTable.DefaultView;
			dataGrid1.DataSource = m_dataView;
			//dataGrid1.DataSource = m_arrayList;

			this.comboBoxFilter.Items.Add( "(All)" );
			this.comboBoxFilter.Items.Add( "Column2 <= 2" );
			this.comboBoxFilter.Items.Add( "Column2 > 2" );
			this.comboBoxFilter.SelectedIndex = 0;

			m_loopDataTable.Columns.Add( "Number" );
			m_loopDataTable.Columns.Add( "String" );

			m_loopDataTable.Rows.Add( new string[] { "0" , "Nil" } );
			m_loopDataTable.Rows.Add( new string[] { "1" , "Un" } );
			m_loopDataTable.Rows.Add( new string[] { "2" , "Deux" } );
			m_loopDataTable.Rows.Add( new string[] { "3" , "Trois" } );

//			dataGridComboBoxColumn1.DataSource = m_loopDataTable;
//			dataGridComboBoxColumn1.DisplayMember = "String";
//			dataGridComboBoxColumn1.ValueMember = "Number";

			this.dataGrid1.Select( 0 );

            // set up the second grid:

            FundCollection funds = new FundCollection();
            for (int i=0; i<5; i++)
            {
                Fund newFund = new Fund();
				newFund.FullName = "Fund " + i.ToString();
				newFund.HiPortfolioCode = i.ToString();
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
            
            fundsGrid.BindToCustomCollection(funds);

		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.comboBoxFilter = new System.Windows.Forms.ComboBox();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonPrintPreview = new System.Windows.Forms.Button();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.dataGrid1 = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.hbosTableStyle1 = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.hbosTableStyle2 = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.DisplayOriginalButton = new System.Windows.Forms.Button();
			this.DisplayModifiedButton = new System.Windows.Forms.Button();
			this.DisplayEntireButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBoxFilter
			// 
			this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter.Location = new System.Drawing.Point(640, 32);
			this.comboBoxFilter.Name = "comboBoxFilter";
			this.comboBoxFilter.Size = new System.Drawing.Size(104, 21);
			this.comboBoxFilter.TabIndex = 2;
			this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(640, 64);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(104, 23);
			this.buttonPrint.TabIndex = 3;
			this.buttonPrint.Text = "Print";
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonPrintPreview
			// 
			this.buttonPrintPreview.Location = new System.Drawing.Point(640, 96);
			this.buttonPrintPreview.Name = "buttonPrintPreview";
			this.buttonPrintPreview.Size = new System.Drawing.Size(104, 23);
			this.buttonPrintPreview.TabIndex = 4;
			this.buttonPrintPreview.Text = "Print Preview";
			this.buttonPrintPreview.Click += new System.EventHandler(this.buttonPrintPreview_Click);
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(640, 128);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(104, 23);
			this.buttonWrite.TabIndex = 5;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.GridLineColor = System.Drawing.Color.Black;
			this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.Location = new System.Drawing.Point(16, 24);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.dataGrid1.PrintColumnSettings = null;
			this.dataGrid1.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.PrintStandardFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.Size = new System.Drawing.Size(576, 160);
			this.dataGrid1.TabIndex = 6;
			this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.hbosTableStyle1});
			this.dataGrid1.CellClicked += new HBOS.FS.AMP.Windows.Controls.DataGrid.CellClickedDelegate(this.dataGrid1_CellClicked);
			// 
			// hbosTableStyle1
			// 
			this.hbosTableStyle1.DataGrid = this.dataGrid1;
			this.hbosTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																											  this.dataGridTextBoxColumn1,
																											  this.dataGridTextBoxColumn2,
																											  this.dataGridTextBoxColumn5,
																											  this.dataGridTextBoxColumn6});
			this.hbosTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.hbosTableStyle1.MappingName = "DataTable";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.MappingName = "Column1";
			this.dataGridTextBoxColumn1.Width = 75;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.MappingName = "Column2";
			this.dataGridTextBoxColumn2.Width = 75;
			// 
			// dataGridTextBoxColumn5
			// 
			this.dataGridTextBoxColumn5.Format = "";
			this.dataGridTextBoxColumn5.FormatInfo = null;
			this.dataGridTextBoxColumn5.MappingName = "Column3";
			this.dataGridTextBoxColumn5.Width = 75;
			// 
			// dataGridTextBoxColumn6
			// 
			this.dataGridTextBoxColumn6.Format = "";
			this.dataGridTextBoxColumn6.FormatInfo = null;
			this.dataGridTextBoxColumn6.MappingName = "Column4";
			this.dataGridTextBoxColumn6.Width = 75;
			// 
			// fundsGrid
			// 
			this.fundsGrid.DataMember = "";
			this.fundsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundsGrid.GridLineColor = System.Drawing.Color.Black;
			this.fundsGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundsGrid.Location = new System.Drawing.Point(24, 272);
			this.fundsGrid.Name = "fundsGrid";
			this.fundsGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.fundsGrid.PrintColumnHeadings = false;
			this.fundsGrid.PrintColumnSettings = null;
			this.fundsGrid.PrintPageHeader = false;
			this.fundsGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.fundsGrid.PrintPageNumbers = false;
			this.fundsGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.fundsGrid.Size = new System.Drawing.Size(576, 160);
			this.fundsGrid.TabIndex = 7;
			this.fundsGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.hbosTableStyle2});
			// 
			// hbosTableStyle2
			// 
			this.hbosTableStyle2.DataGrid = this.fundsGrid;
			this.hbosTableStyle2.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																											  this.dataGridTextBoxColumn3,
																											  this.dataGridTextBoxColumn4});
			this.hbosTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.hbosTableStyle2.MappingName = "";
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.MappingName = "HiPortfolioCode";
			this.dataGridTextBoxColumn3.Width = 75;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.MappingName = "FullName";
			this.dataGridTextBoxColumn4.Width = 75;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// DisplayOriginalButton
			// 
			this.DisplayOriginalButton.Location = new System.Drawing.Point(616, 272);
			this.DisplayOriginalButton.Name = "DisplayOriginalButton";
			this.DisplayOriginalButton.Size = new System.Drawing.Size(160, 32);
			this.DisplayOriginalButton.TabIndex = 8;
			this.DisplayOriginalButton.Text = "Display Original Collection";
			this.DisplayOriginalButton.Click += new System.EventHandler(this.DisplayOriginalButton_Click);
			// 
			// DisplayModifiedButton
			// 
			this.DisplayModifiedButton.Location = new System.Drawing.Point(616, 328);
			this.DisplayModifiedButton.Name = "DisplayModifiedButton";
			this.DisplayModifiedButton.Size = new System.Drawing.Size(160, 32);
			this.DisplayModifiedButton.TabIndex = 9;
			this.DisplayModifiedButton.Text = "Display Modified Only Collection";
			this.DisplayModifiedButton.Click += new System.EventHandler(this.DisplayModifiedButton_Click);
			// 
			// DisplayEntireButton
			// 
			this.DisplayEntireButton.Location = new System.Drawing.Point(616, 392);
			this.DisplayEntireButton.Name = "DisplayEntireButton";
			this.DisplayEntireButton.Size = new System.Drawing.Size(160, 32);
			this.DisplayEntireButton.TabIndex = 10;
			this.DisplayEntireButton.Text = "Display Entire Collection";
			this.DisplayEntireButton.Click += new System.EventHandler(this.DisplayEntireButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 216);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(584, 48);
			this.label1.TabIndex = 11;
			this.label1.Text = "This second grid demonstrates binding and retrieving custom collections of object" +
				"s to and from a datagrid. The fund properties shown on clicking any of the 3 but" +
				"tons on the right are the fund FullName and ShortName";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(808, 526);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DisplayEntireButton);
			this.Controls.Add(this.DisplayModifiedButton);
			this.Controls.Add(this.DisplayOriginalButton);
			this.Controls.Add(this.fundsGrid);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.buttonWrite);
			this.Controls.Add(this.buttonPrintPreview);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.comboBoxFilter);
			this.Name = "Form1";
			this.Text = "DataGrid Test Harness";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
			this.ResumeLayout(false);

		}

		private System.ComponentModel.IContainer components;

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

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void dataGrid1_CellDoubleClicked(object sender, HBOS.FS.AMP.Windows.Controls.CellEventArgs e)
		{
			if ( e.Type == DataGrid.HitTestType.Cell )
			{
				MessageBox.Show( "Double Click Row :" + e.RowNumber.ToString() + Environment.NewLine + "Cell Value :" + m_dataView[ e.RowNumber ][ e.ColumnNumber ].ToString() );
			}
		}

		private void comboBoxFilter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch( comboBoxFilter.SelectedIndex )
			{
				case 0:
					m_dataView.RowFilter = "";
					break;
				case 1:
					m_dataView.RowFilter = "Column2 <= 2";
					break;
				case 2:
					m_dataView.RowFilter = "Column2 > 2";
					break;

			}
		}

		private void dataGrid1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.Enter )
			{
				MessageBox.Show( this.dataGrid1.CurrentRowIndex.ToString() );
			}
		}

		private void buttonPrint_Click(object sender, System.EventArgs e)
		{
			HBOS.FS.AMP.Windows.Controls.PrintColumnSettingsCollection mySettings = new HBOS.FS.AMP.Windows.Controls.PrintColumnSettingsCollection();
			mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column1" , 300 , StringAlignment.Near , "Hello Mum" ));
			mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column2" , 300 , StringAlignment.Far , "Column 2" ));
			mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column3" , 300 , StringAlignment.Center , "Column 3" ));
			mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column4" , 500 , StringAlignment.Center , "Column 4" ));

			dataGrid1.PrintColumnSettings = mySettings;
			dataGrid1.Print( this.m_dataView , "Test Output" );
		}

		private void buttonPrintPreview_Click(object sender, System.EventArgs e)
		{
			try
			{
				HBOS.FS.AMP.Windows.Controls.PrintColumnSettingsCollection mySettings = new HBOS.FS.AMP.Windows.Controls.PrintColumnSettingsCollection();
				mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column1" , 300 , StringAlignment.Near , "Hello Mum" ));
				mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column2" , 300 , StringAlignment.Far , "Column 2" ));
				mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column3" , 300 , StringAlignment.Center , "Column 3" ));
				mySettings.Add( new HBOS.FS.AMP.Windows.Controls.PrintColumnSettings( "Column4" , 500 , StringAlignment.Center , "Column 4" ));

				dataGrid1.PrintColumnSettings = mySettings;

				dataGrid1.PrintPreview( this.m_dataView , "Test Output" );
			}
			catch( Exception Ex )
			{
				MessageBox.Show( Ex.Message );
			}
		}

		private void buttonWrite_Click(object sender, System.EventArgs e)
		{
			dataGrid1.Write( this.m_dataView , @"C:\SampleOutput.csv" , "Grid_Demo.DataGrid.xslt" );
			MessageBox.Show( @"Created C:\SampleOutput.csv" );
		}

		private void dataGrid1_CellClicked(object sender, HBOS.FS.AMP.Windows.Controls.CellEventArgs e)
		{
			//			DataRowView myRow = m_dataView[ e.RowNumber ];
			//			string myCalue = myRow[ e.ColumnNumber ];

			//			MessageBox.Show( m_dataView[ e.RowNumber , e.ColumnNumber ].ToString() );

		}

        private void DisplayOriginalButton_Click(object sender, System.EventArgs e)
        {
            FundCollection fundCollection = (FundCollection)fundsGrid.RetrieveOriginalCustomCollection();
            displayFunds(fundCollection);
        
        }

        private void DisplayModifiedButton_Click(object sender, System.EventArgs e)
        {
            FundCollection fundCollection = (FundCollection)fundsGrid.RetrieveUpdatedCustomCollection();
            displayFunds(fundCollection);
        }

        private void DisplayEntireButton_Click(object sender, System.EventArgs e)
        {
            FundCollection fundCollection = (FundCollection)fundsGrid.RetrieveEntireCustomCollection();
            displayFunds(fundCollection);
        }

        private void displayFunds(FundCollection funds)
        {
            string display = "";
            for (int i=0; i<funds.Count; i++)
            {
                Fund fund = (Fund)funds[i];

                if (fund.IsNew)
                {
                    display += "N ";
                }
                else if (fund.IsDirty)
                {
                    display += "D ";
                }
                display += "Fund number " + i + ", Full name = " + fund.FullName + ", Short Name = " + fund.ShortName + Environment.NewLine;
            }
            MessageBox.Show(display);
        }
	}
}
