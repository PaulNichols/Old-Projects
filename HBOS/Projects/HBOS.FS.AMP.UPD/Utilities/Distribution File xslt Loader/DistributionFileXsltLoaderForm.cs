using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace Distribution_File_xslt_Loader
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DistributionFileXsltLoaderForm : System.Windows.Forms.Form
	{
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private System.Data.SqlClient.SqlDataAdapter daDistributionFile;
		private System.Data.DataSet dataSet;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Data.SqlClient.SqlCommand cmdUpdate;
		private System.Windows.Forms.DataGrid fileGrid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TextBox txtCurrentXslt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmboConnectionString;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DistributionFileXsltLoaderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection = new System.Data.SqlClient.SqlConnection();
			this.daDistributionFile = new System.Data.SqlClient.SqlDataAdapter();
			this.dataSet = new System.Data.DataSet();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.cmdUpdate = new System.Data.SqlClient.SqlCommand();
			this.fileGrid = new System.Windows.Forms.DataGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.txtCurrentXslt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmboConnectionString = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "rrn_DistributionFilesGetForMaintenance";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.sqlConnection;
			// 
			// daDistributionFile
			// 
			this.daDistributionFile.SelectCommand = this.sqlSelectCommand1;
			this.daDistributionFile.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										 new System.Data.Common.DataTableMapping("Table", "DistributionFiles", new System.Data.Common.DataColumnMapping[] {
																																																							  new System.Data.Common.DataColumnMapping("FileID", "FileID"),
																																																							  new System.Data.Common.DataColumnMapping("FileDesc", "FileDesc"),
																																																							  new System.Data.Common.DataColumnMapping("CompanyCode", "CompanyCode"),
																																																							  new System.Data.Common.DataColumnMapping("Filename", "Filename"),
																																																							  new System.Data.Common.DataColumnMapping("Filepath", "Filepath"),
																																																							  new System.Data.Common.DataColumnMapping("XsltLocation", "XsltLocation"),
																																																							  new System.Data.Common.DataColumnMapping("XsltSource", "XsltSource"),
																																																							  new System.Data.Common.DataColumnMapping("SprocName", "SprocName"),
																																																							  new System.Data.Common.DataColumnMapping("LastDistributed", "LastDistributed"),
																																																							  new System.Data.Common.DataColumnMapping("SystemId", "SystemId"),
																																																							  new System.Data.Common.DataColumnMapping("lastChangedBy", "lastChangedBy"),
																																																							  new System.Data.Common.DataColumnMapping("lastChangedDate", "lastChangedDate"),
																																																							  new System.Data.Common.DataColumnMapping("ts", "ts"),
																																																							  new System.Data.Common.DataColumnMapping("deleted", "deleted")})});
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "NewDataSet";
			this.dataSet.Locale = new System.Globalization.CultureInfo("en-GB");
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "xslt";
			this.openFileDialog.Filter = "xsl files (*.xsl,*.xslt)|*.xsl*";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cmboConnectionString);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 454);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(896, 40);
			this.panel1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(800, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "&Upload";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.CommandText = "rrn_DistributionFilesUpdateForMaintenance";
			this.cmdUpdate.CommandType = System.Data.CommandType.StoredProcedure;
			this.cmdUpdate.Connection = this.sqlConnection;
			this.cmdUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@XsltSource", System.Data.SqlDbType.Text, 2147483647, "XsltSource"));
			this.cmdUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FileId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FileID", System.Data.DataRowVersion.Original, null));
			// 
			// fileGrid
			// 
			this.fileGrid.CaptionVisible = false;
			this.fileGrid.DataMember = "";
			this.fileGrid.DataSource = this.dataSet;
			this.fileGrid.Dock = System.Windows.Forms.DockStyle.Left;
			this.fileGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.fileGrid.Location = new System.Drawing.Point(0, 0);
			this.fileGrid.Name = "fileGrid";
			this.fileGrid.ReadOnly = true;
			this.fileGrid.Size = new System.Drawing.Size(544, 454);
			this.fileGrid.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(544, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 454);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// txtCurrentXslt
			// 
			this.txtCurrentXslt.Cursor = System.Windows.Forms.Cursors.Default;
			this.txtCurrentXslt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtCurrentXslt.Location = new System.Drawing.Point(547, 0);
			this.txtCurrentXslt.Multiline = true;
			this.txtCurrentXslt.Name = "txtCurrentXslt";
			this.txtCurrentXslt.ReadOnly = true;
			this.txtCurrentXslt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtCurrentXslt.Size = new System.Drawing.Size(349, 454);
			this.txtCurrentXslt.TabIndex = 4;
			this.txtCurrentXslt.Text = "txtCurrentXslt";
			this.txtCurrentXslt.WordWrap = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Database";
			// 
			// cmboConnectionString
			// 
			this.cmboConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmboConnectionString.Location = new System.Drawing.Point(72, 8);
			this.cmboConnectionString.Name = "cmboConnectionString";
			this.cmboConnectionString.Size = new System.Drawing.Size(704, 21);
			this.cmboConnectionString.TabIndex = 2;
			this.cmboConnectionString.Text = "<<Please select a database connection >>";
			this.cmboConnectionString.SelectedIndexChanged += new System.EventHandler(this.cmboConnectionString_SelectedIndexChanged);
			// 
			// DistributionFileXsltLoaderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(896, 494);
			this.Controls.Add(this.txtCurrentXslt);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.fileGrid);
			this.Controls.Add(this.panel1);
			this.Name = "DistributionFileXsltLoaderForm";
			this.Text = "Distribution File Xslt Loader";
			this.Load += new System.EventHandler(this.DistributionFileXsltLoaderForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fileGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DistributionFileXsltLoaderForm());
		}

		private void DistributionFileXsltLoaderForm_Load(object sender, System.EventArgs e)
		{
			getConnectionStrings();
		}

		private const string tableName = "DistributionFiles";

		private void loadData()
		{
			dataSet.Clear();
			try
			{
				daDistributionFile.Fill(dataSet,tableName);
				fileGrid.DataMember = tableName;
				txtCurrentXslt.Text = selectedRow["XsltSource"].ToString();
				((CurrencyManager)this.BindingContext[dataSet,tableName]).PositionChanged +=new EventHandler(DistributionFileXsltLoaderForm_PositionChanged);
			}
			catch(SqlException ex)
			{
				MessageBox.Show("Failed to connect to database, please check config file\n\n"+ex.Message,"Database Connection Failure",MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			openFileDialog.Title = string.Format("Select xslt for FileID {0}: {1}",selectedRow["fileId"],selectedRow["fileDesc"]);
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				uploadXslt();
			}
		}

		private DataRow selectedRow
		{
			get
			{
				if (dataSet.Tables[tableName] != null && fileGrid.CurrentRowIndex >= 0)
				{
					return dataSet.Tables[0].Rows[fileGrid.CurrentRowIndex];
				}
				else
				{
					return null;
				}
			}
		}

		private void uploadXslt()
		{
			string xslt;
			StreamReader reader = new StreamReader(openFileDialog.FileName);
			try
			{
				xslt = reader.ReadToEnd();
			}
			finally
			{
				reader.Close();
			}		
			
			cmdUpdate.Parameters["@XsltSource"].Value = xslt;
			cmdUpdate.Parameters["@FileId"].Value = (int)selectedRow["fileId"];

			sqlConnection.Open();
			try
			{
				cmdUpdate.ExecuteNonQuery();
				loadData();
			}
			finally
			{
				sqlConnection.Close();
			}		
		}

		private void DistributionFileXsltLoaderForm_PositionChanged(object sender, EventArgs e)
		{
			if (selectedRow != null)
			{
				txtCurrentXslt.Text = selectedRow["XsltSource"].ToString();
			}
			else
			{
				txtCurrentXslt.Text = string.Empty;
			}
		}

		private NameValueCollection connections; 
		
		private void getConnectionStrings()
		{
			connections = (NameValueCollection)ConfigurationSettings.GetConfig("connectionStrings");
			foreach(string name in connections.AllKeys)
			{
				cmboConnectionString.Items.Add(name);
			}
		}

		private void cmboConnectionString_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			sqlConnection.ConnectionString = connections[cmboConnectionString.SelectedIndex];
			loadData();
		}
	}
}
