using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using HBOS.FS.AMP.Data.Types;

namespace HBOS.FS.AMP.Data.Transfer
{
	/// <summary>
	/// Validation Errors Report Form - allows display of the validation errors to the user.
	/// </summary>
	/// <remarks>Uses <see cref="HBOS.FS.AMP.Windows.Controls"/> for display of the data. As such, the following are supported:
	///		<list type="bullet">
	///			<item>Sorting</item>
	///			<item>Filtering</item>
	///			<item>Printing</item>
	///			<item>Print Previewing</item>
	///			<item>Writing/Extracting to file.</item>
	///		</list>
	/// </remarks>
	/// <example>
	///		<code lang="C#">
	///			FormValidationErrors myValidationErrors = new FormValidationErrors();
	///			myValidationErrors.Display( m_validationErrors );
	///			myValidationErrors.ShowDialog( );
	///		</code>
	/// </example>
	public class FormValidationErrors : System.Windows.Forms.Form
	{
		#region Variables

		private DataTable m_validationErrorsDataTable = null;
		private HBOS.FS.AMP.Windows.Controls.DataGrid dataGridValidationErrors;
		private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle hbosTableStyle1;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn dataGridTextBoxReadOnlyColumnMessage;
		private HBOS.FS.AMP.Windows.Controls.DataGridImageColumn dataGridImageColumnSeverity;
		private System.Windows.Forms.ImageList imageList1;
		private DataView m_validationErrorsDataView = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor to initialise the form
		/// </summary>
		public FormValidationErrors()
		{
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormValidationErrors));
			this.buttonPrint = new System.Windows.Forms.Button();
			this.buttonPrintPreview = new System.Windows.Forms.Button();
			this.buttonExtract = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.dataGridValidationErrors = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.hbosTableStyle1 = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
			this.dataGridTextBoxReadOnlyColumnMessage = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
			this.dataGridImageColumnSeverity = new HBOS.FS.AMP.Windows.Controls.DataGridImageColumn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridValidationErrors)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(672, 16);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(88, 23);
			this.buttonPrint.TabIndex = 1;
			this.buttonPrint.Text = "Print";
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// buttonPrintPreview
			// 
			this.buttonPrintPreview.Location = new System.Drawing.Point(672, 48);
			this.buttonPrintPreview.Name = "buttonPrintPreview";
			this.buttonPrintPreview.Size = new System.Drawing.Size(88, 23);
			this.buttonPrintPreview.TabIndex = 2;
			this.buttonPrintPreview.Text = "Print Preview";
			this.buttonPrintPreview.Click += new System.EventHandler(this.buttonPrintPreview_Click);
			// 
			// buttonExtract
			// 
			this.buttonExtract.Location = new System.Drawing.Point(672, 80);
			this.buttonExtract.Name = "buttonExtract";
			this.buttonExtract.Size = new System.Drawing.Size(88, 23);
			this.buttonExtract.TabIndex = 3;
			this.buttonExtract.Text = "Extract/Write";
			this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(672, 336);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(88, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// dataGridValidationErrors
			// 
			this.dataGridValidationErrors.DataMember = "";
			this.dataGridValidationErrors.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGridValidationErrors.GridLineColor = System.Drawing.Color.Black;
			this.dataGridValidationErrors.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGridValidationErrors.Location = new System.Drawing.Point(24, 16);
			this.dataGridValidationErrors.Name = "dataGridValidationErrors";
			this.dataGridValidationErrors.ReadOnly = true;
			this.dataGridValidationErrors.Size = new System.Drawing.Size(632, 344);
			this.dataGridValidationErrors.TabIndex = 5;
			this.dataGridValidationErrors.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																												 this.hbosTableStyle1});
			// 
			// hbosTableStyle1
			// 
			this.hbosTableStyle1.DataGrid = this.dataGridValidationErrors;
			this.hbosTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																											  this.dataGridTextBoxReadOnlyColumnMessage,
																											  this.dataGridImageColumnSeverity});
			this.hbosTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.hbosTableStyle1.MappingName = "ValidationErrors";
			this.hbosTableStyle1.ReadOnly = true;
			// 
			// dataGridTextBoxReadOnlyColumnMessage
			// 
			this.dataGridTextBoxReadOnlyColumnMessage.Format = "";
			this.dataGridTextBoxReadOnlyColumnMessage.FormatInfo = null;
			this.dataGridTextBoxReadOnlyColumnMessage.HeaderText = "Error Message";
			this.dataGridTextBoxReadOnlyColumnMessage.MappingName = "Message";
			this.dataGridTextBoxReadOnlyColumnMessage.Width = 150;
			// 
			// dataGridImageColumnSeverity
			// 
			this.dataGridImageColumnSeverity.Format = "";
			this.dataGridImageColumnSeverity.FormatInfo = null;
			this.dataGridImageColumnSeverity.HeaderText = "Severity";
			this.dataGridImageColumnSeverity.ImageList = this.imageList1;
			this.dataGridImageColumnSeverity.MappingName = "Severity";
			this.dataGridImageColumnSeverity.Width = 75;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FormValidationErrors
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(776, 382);
			this.Controls.Add(this.dataGridValidationErrors);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonExtract);
			this.Controls.Add(this.buttonPrintPreview);
			this.Controls.Add(this.buttonPrint);
			this.Name = "FormValidationErrors";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Validation Errors";
			((System.ComponentModel.ISupportInitialize)(this.dataGridValidationErrors)).EndInit();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.Button buttonPrintPreview;
		private System.Windows.Forms.Button buttonExtract;
		private System.Windows.Forms.Button buttonClose;

		private System.ComponentModel.IContainer components;

		#endregion

		#region Public Methods

		/// <summary>
		/// Set up the Grid with the validation errors.
		/// </summary>
		/// <param name="validationErrors"></param>
		public void Display( DataTable validationErrors)
		{
			m_validationErrorsDataTable = validationErrors;
			m_validationErrorsDataView = m_validationErrorsDataTable.DefaultView;

			// Build the dynamic table style columns
			this.dataGridImageColumnSeverity.EnumType = typeof( ValidationErrorSeverity );
			this.buildTableStyle();

			this.dataGridValidationErrors.CopyDefaultTableStyle( this.hbosTableStyle1 );

			this.dataGridValidationErrors.DataSource = m_validationErrorsDataView;
			
			// Select the first row
			if ( this.m_validationErrorsDataView.Count > 0 )
			{
				this.dataGridValidationErrors.Select(0);
			}
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Close the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// we need to add the data columns onto the table style
		/// </summary>
		private void buildTableStyle()
		{
			// Add each data column
			for( int i = 2 ; i < m_validationErrorsDataTable.Columns.Count ; i ++ )
			{
				AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn columnToAdd = new AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
				columnToAdd.MappingName = m_validationErrorsDataTable.Columns[ i ].ColumnName;
				columnToAdd.HeaderText = m_validationErrorsDataTable.Columns[ i ].ColumnName;

				hbosTableStyle1.GridColumnStyles.Add( columnToAdd );
			}
		}

		/// <summary>
		/// Print the Grid Output
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrint_Click(object sender, System.EventArgs e)
		{
			this.dataGridValidationErrors.Print( m_validationErrorsDataView , "Validation Errors" );
		
		}

		/// <summary>
		/// Perform a print preview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrintPreview_Click(object sender, System.EventArgs e)
		{
			this.dataGridValidationErrors.PrintPreview( m_validationErrorsDataView , "Validation Errors" );
		}

		/// <summary>
		/// Extract the data from the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExtract_Click(object sender, System.EventArgs e)
		{
			this.dataGridValidationErrors.Write( m_validationErrorsDataView , @"C:\ValidationErrors.csv" , "HBOS.FS.AMP.Data.Transfer.ValidationErrors.xslt" );
			MessageBox.Show( @"Generated C:\ValidationErrors.csv" );
		}

		#endregion
	}
}
