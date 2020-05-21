using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Application About Dialog Box.
	/// </summary>
	public class About : Form
	{
		private PictureBox aboutImage;
		private Label productName;
		private Label productVersion;
		private Label frameworkVersion;
		private Button close;
		private DataGrid dgReferencedAssemblies;

		private Cursor originalCursor;
		private Label warning;
		private Label copyright;
		private Label principalDetails;
		private Button copyDetails;
		private ContextMenu versionGridContextMenu;
		private MenuItem contextCopy;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		/// <summary>
		/// Creates a new <see cref="About"/> instance.
		/// </summary>
		public About()
		{
			T.E();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Assembly myself = Assembly.GetExecutingAssembly();
			object[] objAttributes = null;

			originalCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;

			T.Log("Loading About Image");
			//string[] resources = myself.GetManifestResourceNames();

			aboutImage.Image =
				new Bitmap(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Images.about-folders.jpg"));

			T.Log("Binding version numbers and meta data");
			this.productVersion.Text = "Version " + GlobalRegistry.ClientVersion.ToString();

			this.frameworkVersion.Text = ".net Version " + Environment.Version;

			this.principalDetails.Text = ((UPDPrincipal) Thread.CurrentPrincipal).Identity.Name + " [" +
				((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode + "]";


			objAttributes = myself.GetCustomAttributes(typeof (AssemblyDescriptionAttribute), true);
			if (objAttributes.Length != 0)
			{
				string assemblyDescription = ((AssemblyDescriptionAttribute) objAttributes[0]).Description;
				this.Text = "About " + assemblyDescription;
				this.productName.Text = assemblyDescription;
			}


			objAttributes = myself.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), true);
			string copyrightString = (objAttributes.Length != 0) ? ((AssemblyCopyrightAttribute) objAttributes[0]).Copyright : "";
			this.copyright.Text = copyrightString;


			T.Log("Loading dependency meta data");
			AssemblyName[] referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();

			DataTable dtAssemblyVersions = new DataTable("Assemblies");
			dtAssemblyVersions.Locale = CultureInfo.InvariantCulture;
			dtAssemblyVersions.Columns.Add("assembly");
			dtAssemblyVersions.Columns.Add("version");
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				AssemblyName referencedAssembly = referencedAssemblies[i];
				DataRow row = dtAssemblyVersions.NewRow();
				row[0] = referencedAssembly.Name;
				row[1] = referencedAssembly.Version.ToString();
				dtAssemblyVersions.Rows.Add(row);
			}

			dtAssemblyVersions.DefaultView.Sort = "assembly";


			dgReferencedAssemblies.DataSource = dtAssemblyVersions;

			this.SizeColumnsToContent(dgReferencedAssemblies, -1);

			this.Cursor = this.originalCursor;
			T.X();
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			T.E();
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
			T.X();
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (About));
			this.aboutImage = new System.Windows.Forms.PictureBox();
			this.productName = new System.Windows.Forms.Label();
			this.productVersion = new System.Windows.Forms.Label();
			this.frameworkVersion = new System.Windows.Forms.Label();
			this.close = new System.Windows.Forms.Button();
			this.dgReferencedAssemblies = new System.Windows.Forms.DataGrid();
			this.warning = new System.Windows.Forms.Label();
			this.copyright = new System.Windows.Forms.Label();
			this.principalDetails = new System.Windows.Forms.Label();
			this.copyDetails = new System.Windows.Forms.Button();
			this.versionGridContextMenu = new System.Windows.Forms.ContextMenu();
			this.contextCopy = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize) (this.dgReferencedAssemblies)).BeginInit();
			this.SuspendLayout();
			// 
			// aboutImage
			// 
			this.aboutImage.Location = new System.Drawing.Point(0, 0);
			this.aboutImage.Name = "aboutImage";
			this.aboutImage.Size = new System.Drawing.Size(205, 256);
			this.aboutImage.TabIndex = 0;
			this.aboutImage.TabStop = false;
			// 
			// productName
			// 
			this.productName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.productName.Location = new System.Drawing.Point(216, 8);
			this.productName.Name = "productName";
			this.productName.Size = new System.Drawing.Size(232, 23);
			this.productName.TabIndex = 1;
			this.productName.Text = "Unit Pricing and Distribution";
			// 
			// productVersion
			// 
			this.productVersion.Location = new System.Drawing.Point(216, 32);
			this.productVersion.Name = "productVersion";
			this.productVersion.Size = new System.Drawing.Size(232, 23);
			this.productVersion.TabIndex = 2;
			this.productVersion.Text = "Product Version";
			// 
			// frameworkVersion
			// 
			this.frameworkVersion.Location = new System.Drawing.Point(216, 56);
			this.frameworkVersion.Name = "frameworkVersion";
			this.frameworkVersion.Size = new System.Drawing.Size(232, 23);
			this.frameworkVersion.TabIndex = 3;
			this.frameworkVersion.Text = ".net Version";
			// 
			// close
			// 
			this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.close.Location = new System.Drawing.Point(376, 224);
			this.close.Name = "close";
			this.close.TabIndex = 4;
			this.close.Text = "&Close";
			this.close.Click += new System.EventHandler(this.onCloseClicked);
			// 
			// dgReferencedAssemblies
			// 
			this.dgReferencedAssemblies.AllowNavigation = false;
			this.dgReferencedAssemblies.AllowSorting = false;
			this.dgReferencedAssemblies.AlternatingBackColor = System.Drawing.SystemColors.Control;
			this.dgReferencedAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgReferencedAssemblies.BackColor = System.Drawing.SystemColors.Control;
			this.dgReferencedAssemblies.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgReferencedAssemblies.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgReferencedAssemblies.CaptionVisible = false;
			this.dgReferencedAssemblies.CausesValidation = false;
			this.dgReferencedAssemblies.ColumnHeadersVisible = false;
			this.dgReferencedAssemblies.DataMember = "";
			this.dgReferencedAssemblies.FlatMode = true;
			this.dgReferencedAssemblies.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgReferencedAssemblies.Location = new System.Drawing.Point(0, 264);
			this.dgReferencedAssemblies.Name = "dgReferencedAssemblies";
			this.dgReferencedAssemblies.ReadOnly = true;
			this.dgReferencedAssemblies.RowHeadersVisible = false;
			this.dgReferencedAssemblies.Size = new System.Drawing.Size(456, 88);
			this.dgReferencedAssemblies.TabIndex = 5;
			// 
			// warning
			// 
			this.warning.Location = new System.Drawing.Point(216, 120);
			this.warning.Name = "warning";
			this.warning.Size = new System.Drawing.Size(232, 72);
			this.warning.TabIndex = 7;
			this.warning.Text = "Only authorised users are permitted to access this system.  Unauthorised access b" +
				"y employees is a disciplinary offence. Intentional unauthorised access by any pe" +
				"rson may lead to criminal prosecution.";
			// 
			// copyright
			// 
			this.copyright.Location = new System.Drawing.Point(216, 80);
			this.copyright.Name = "copyright";
			this.copyright.Size = new System.Drawing.Size(232, 23);
			this.copyright.TabIndex = 8;
			this.copyright.Text = "(copyright)";
			// 
			// principalDetails
			// 
			this.principalDetails.Location = new System.Drawing.Point(216, 192);
			this.principalDetails.Name = "principalDetails";
			this.principalDetails.Size = new System.Drawing.Size(232, 23);
			this.principalDetails.TabIndex = 9;
			this.principalDetails.Text = "Principal Details";
			// 
			// copyDetails
			// 
			this.copyDetails.Location = new System.Drawing.Point(296, 224);
			this.copyDetails.Name = "copyDetails";
			this.copyDetails.TabIndex = 10;
			this.copyDetails.Text = "C&opy";
			this.copyDetails.Click += new System.EventHandler(this.onCopyClicked);
			// 
			// versionGridContextMenu
			// 
			this.versionGridContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.contextCopy
				});
			// 
			// contextCopy
			// 
			this.contextCopy.Index = 0;
			this.contextCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.contextCopy.Text = "&Copy";
			this.contextCopy.Click += new System.EventHandler(this.onCopyClicked);
			// 
			// About
			// 
			this.AcceptButton = this.close;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.close;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(456, 350);
			this.ContextMenu = this.versionGridContextMenu;
			this.Controls.Add(this.copyDetails);
			this.Controls.Add(this.principalDetails);
			this.Controls.Add(this.copyright);
			this.Controls.Add(this.warning);
			this.Controls.Add(this.dgReferencedAssemblies);
			this.Controls.Add(this.close);
			this.Controls.Add(this.frameworkVersion);
			this.Controls.Add(this.productVersion);
			this.Controls.Add(this.productName);
			this.Controls.Add(this.aboutImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About Unit Pricing and Distribution";
			((System.ComponentModel.ISupportInitialize) (this.dgReferencedAssemblies)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private void onCloseClicked(object sender, EventArgs e)
		{
			T.E();
			this.Close();
			T.X();
		}

		private void SizeColumnsToContent(DataGrid dataGrid, int nRowsToScan)
		{
			T.E();
			// Create graphics object for measuring widths.
			Graphics Graphics = dataGrid.CreateGraphics();

			// Define new table style.
			DataGridTableStyle tableStyle = new DataGridTableStyle();

			try
			{
				DataTable dataTable = (DataTable) dataGrid.DataSource;

				if (-1 == nRowsToScan)
				{
					nRowsToScan = dataTable.Rows.Count;
				}
				else
				{
					// Can only scan rows if they exist.
					nRowsToScan = Math.Min(nRowsToScan,
					                       dataTable.Rows.Count);
				}

				// Clear any existing table styles.
				dataGrid.TableStyles.Clear();

				// Use mapping name that is defined in the data source.
				tableStyle.MappingName = dataTable.TableName;

				// Now create the column styles within the table style.
				DataGridTextBoxColumn columnStyle;
				int iWidth;

				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count;
					iCurrCol++)
				{
					DataColumn dataColumn = dataTable.Columns[iCurrCol];

					columnStyle = new DataGridTextBoxColumn();

					columnStyle.TextBox.Enabled = true;
					columnStyle.HeaderText = dataColumn.ColumnName;
					columnStyle.MappingName = dataColumn.ColumnName;

					// Set width to header text width.
					iWidth = (int) (Graphics.MeasureString(columnStyle.HeaderText,
					                                       dataGrid.Font).Width);

					// Change width, if data width is wider than header text width.
					// Check the width of the data in the first X rows.
					DataRow dataRow;
					for (int iRow = 0; iRow < nRowsToScan; iRow++)
					{
						dataRow = dataTable.Rows[iRow];

						if (null != dataRow[dataColumn.ColumnName])
						{
							int iColWidth = (int) (Graphics.MeasureString(dataRow.
								ItemArray[iCurrCol].ToString(),
							                                              dataGrid.Font).Width);
							iWidth = Math.Max(iWidth, iColWidth);
						}
					}
					columnStyle.Width = iWidth + 4;

					// Add the new column style to the table style.
					tableStyle.GridColumnStyles.Add(columnStyle);


				}
				// Now set it to blend
				tableStyle.AlternatingBackColor =
					tableStyle.BackColor
						= this.BackColor;

				tableStyle.ColumnHeadersVisible =
					tableStyle.AllowSorting =
						tableStyle.ReadOnly =
							tableStyle.RowHeadersVisible =
								false;

				// Add the new table style to the data grid.
				dataGrid.TableStyles.Add(tableStyle);
			}
			catch
			{
				throw;
			}
			finally
			{
				Graphics.Dispose();
			}
			T.X();
		}

		/// <summary>
		/// Copies the version and principal information to the clipboard
		/// </summary>
		/// <param name="sender">Sending object</param>
		/// <param name="e">Sending arguements</param>
		private void onCopyClicked(object sender, EventArgs e)
		{
			T.E();
			StringBuilder versionInformation = new StringBuilder();

			versionInformation.AppendFormat(CultureInfo.CurrentCulture,
			                                "Unit Pricing And Distribution {0:s}{3:s}{1:s}{3:s}Current user {2:s}{3:s}",
			                                productVersion.Text, frameworkVersion.Text, principalDetails.Text, Environment.NewLine);

			for (int i = 0;
				i < ((DataTable) this.dgReferencedAssemblies.DataSource).DefaultView.Count;
				i++)
			{
				versionInformation.AppendFormat(CultureInfo.CurrentCulture,
				                                "{0:s}\t{1:s}{2:s}",
				                                ((DataTable) this.dgReferencedAssemblies.DataSource).DefaultView[i]["assembly"],
				                                ((DataTable) this.dgReferencedAssemblies.DataSource).DefaultView[i]["version"],
				                                Environment.NewLine);
			}


			Clipboard.SetDataObject(versionInformation.ToString(), true);
			T.X();

		}

	}
}