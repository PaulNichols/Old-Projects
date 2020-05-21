using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using HBOS.FS.Common.ExceptionManagement;

namespace HBOS.FS.AMP.ExceptionManagement
{
	/// <summary>
	/// ErrorDialogForm - The form to display an exception
	/// </summary>
	internal class ErrorDialogForm : System.Windows.Forms.Form
	{
		#region Controls

		private System.Windows.Forms.Button moreLessButton;
		private System.Windows.Forms.ImageList errorDialogImageList;
		private System.Windows.Forms.Button dismissButton;
		private System.Windows.Forms.Label errorDialogMessageLabel;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.TabControl errorDialogTabControl;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TextBox helpLinkTextBox;
		private System.Windows.Forms.Label helpLinkLabel;
		private System.Windows.Forms.TextBox exceptionSourceValueTextBox;
		private System.Windows.Forms.TextBox exceptionTargetMethodValueTextBox;
		private System.Windows.Forms.TextBox exceptionMessageValueTextBox;
		private System.Windows.Forms.Label exceptionTargetMethodLabel;
		private System.Windows.Forms.Label exceptionSourceLabel;
		private System.Windows.Forms.Label exceptionMessageLabel;
		private System.Windows.Forms.TabPage stackTab;
		private System.Windows.Forms.ListView stackTraceListView;
		private System.Windows.Forms.TabPage innerTab;
		private System.Windows.Forms.TreeView innerExceptionsTreeView;
		private System.Windows.Forms.TabPage specificTab;
		private System.Windows.Forms.ListView otherInfoListView;
		private System.Windows.Forms.ColumnHeader methodColumnHeader;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader descriptionColumnHeader;

		#endregion

		#region Private member variables

		int m_showBasicHeight = 128;
		int m_showDetailsHeight = 320;
		private bool m_isShowingDetails = false;
		private BaseApplicationException m_baseException = null;
		private Exception m_exception = null;
		private bool m_hasGeneralBeenCalled = false;
		private bool m_hasInnerExceptionBeenCalled = false;
		private bool m_hasStackTraceBeenCalled = false;
		private System.Windows.Forms.Button buttonCopyErrorMessage;
		private bool m_hasOtherInformationBeenCalled = false;
		private const string TEXT_SEPARATOR = "*********************************************";
		
		private readonly static string EXCEPTIONMANAGER_NAME = typeof(ExceptionManager).Name;

		#endregion

		#region Enum

		/// <summary>
		/// Which tab to display
		/// </summary>
		private enum ErrorDialogTab : int
		{
			/// <summary>
			/// General
			/// </summary>
			General = 0,
			/// <summary>
			/// Stack
			/// </summary>
			Stack = 1,
			/// <summary>
			/// Inner Exception
			/// </summary>
			InnerException = 2,
			/// <summary>
			/// Other
			/// </summary>
			Other = 3
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ErrorDialogForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Constructor with base exception argument
		/// </summary>
		/// <param name="baseException">HBOS.FS.Common.BaseException or derived</param>
		public ErrorDialogForm(BaseApplicationException baseException)
		{
			InitializeComponent();
			this.m_baseException = baseException;
			this.m_exception = baseException;
		}

		/// <summary>
		/// Constructor with general system exception argument
		/// </summary>
		/// <param name="exception">System.Exception</param>
		public ErrorDialogForm(Exception exception)
		{
			InitializeComponent();
			this.m_exception = exception;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ErrorDialogForm));
			this.moreLessButton = new System.Windows.Forms.Button();
			this.errorDialogImageList = new System.Windows.Forms.ImageList(this.components);
			this.dismissButton = new System.Windows.Forms.Button();
			this.errorDialogMessageLabel = new System.Windows.Forms.Label();
			this.errorDialogTabControl = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.helpLinkTextBox = new System.Windows.Forms.TextBox();
			this.helpLinkLabel = new System.Windows.Forms.Label();
			this.exceptionSourceValueTextBox = new System.Windows.Forms.TextBox();
			this.exceptionTargetMethodValueTextBox = new System.Windows.Forms.TextBox();
			this.exceptionMessageValueTextBox = new System.Windows.Forms.TextBox();
			this.exceptionTargetMethodLabel = new System.Windows.Forms.Label();
			this.exceptionSourceLabel = new System.Windows.Forms.Label();
			this.exceptionMessageLabel = new System.Windows.Forms.Label();
			this.stackTab = new System.Windows.Forms.TabPage();
			this.stackTraceListView = new System.Windows.Forms.ListView();
			this.methodColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.innerTab = new System.Windows.Forms.TabPage();
			this.innerExceptionsTreeView = new System.Windows.Forms.TreeView();
			this.specificTab = new System.Windows.Forms.TabPage();
			this.otherInfoListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.descriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.buttonCopyErrorMessage = new System.Windows.Forms.Button();
			this.errorDialogTabControl.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.stackTab.SuspendLayout();
			this.innerTab.SuspendLayout();
			this.specificTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// moreLessButton
			// 
			this.moreLessButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.moreLessButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.moreLessButton.ImageIndex = 0;
			this.moreLessButton.ImageList = this.errorDialogImageList;
			this.moreLessButton.Location = new System.Drawing.Point(296, 64);
			this.moreLessButton.Name = "moreLessButton";
			this.moreLessButton.Size = new System.Drawing.Size(72, 23);
			this.moreLessButton.TabIndex = 0;
			this.moreLessButton.Text = "&More";
			this.moreLessButton.Click += new System.EventHandler(this.moreLessButton_Click);
			// 
			// errorDialogImageList
			// 
			this.errorDialogImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth4Bit;
			this.errorDialogImageList.ImageSize = new System.Drawing.Size(10, 6);
			this.errorDialogImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("errorDialogImageList.ImageStream")));
			this.errorDialogImageList.TransparentColor = System.Drawing.Color.Cyan;
			// 
			// dismissButton
			// 
			this.dismissButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.dismissButton.Location = new System.Drawing.Point(376, 64);
			this.dismissButton.Name = "dismissButton";
			this.dismissButton.Size = new System.Drawing.Size(72, 23);
			this.dismissButton.TabIndex = 1;
			this.dismissButton.Text = "&Close";
			this.dismissButton.Click += new System.EventHandler(this.dismissButton_Click);
			// 
			// errorDialogMessageLabel
			// 
			this.errorDialogMessageLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.errorDialogMessageLabel.Location = new System.Drawing.Point(8, 8);
			this.errorDialogMessageLabel.Name = "errorDialogMessageLabel";
			this.errorDialogMessageLabel.Size = new System.Drawing.Size(440, 48);
			this.errorDialogMessageLabel.TabIndex = 2;
			// 
			// errorDialogTabControl
			// 
			this.errorDialogTabControl.Controls.Add(this.generalTab);
			this.errorDialogTabControl.Controls.Add(this.stackTab);
			this.errorDialogTabControl.Controls.Add(this.innerTab);
			this.errorDialogTabControl.Controls.Add(this.specificTab);
			this.errorDialogTabControl.Location = new System.Drawing.Point(8, 104);
			this.errorDialogTabControl.Name = "errorDialogTabControl";
			this.errorDialogTabControl.SelectedIndex = 0;
			this.errorDialogTabControl.Size = new System.Drawing.Size(440, 176);
			this.errorDialogTabControl.TabIndex = 13;
			this.errorDialogTabControl.SelectedIndexChanged += new System.EventHandler(this.tabInfo_SelectedIndexChanged);
			// 
			// generalTab
			// 
			this.generalTab.Controls.Add(this.helpLinkTextBox);
			this.generalTab.Controls.Add(this.helpLinkLabel);
			this.generalTab.Controls.Add(this.exceptionSourceValueTextBox);
			this.generalTab.Controls.Add(this.exceptionTargetMethodValueTextBox);
			this.generalTab.Controls.Add(this.exceptionMessageValueTextBox);
			this.generalTab.Controls.Add(this.exceptionTargetMethodLabel);
			this.generalTab.Controls.Add(this.exceptionSourceLabel);
			this.generalTab.Controls.Add(this.exceptionMessageLabel);
			this.generalTab.Location = new System.Drawing.Point(4, 22);
			this.generalTab.Name = "generalTab";
			this.generalTab.Size = new System.Drawing.Size(432, 150);
			this.generalTab.TabIndex = 0;
			this.generalTab.Text = "General Information";
			// 
			// helpLinkTextBox
			// 
			this.helpLinkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.helpLinkTextBox.Location = new System.Drawing.Point(96, 120);
			this.helpLinkTextBox.Name = "helpLinkTextBox";
			this.helpLinkTextBox.ReadOnly = true;
			this.helpLinkTextBox.Size = new System.Drawing.Size(328, 20);
			this.helpLinkTextBox.TabIndex = 23;
			this.helpLinkTextBox.TabStop = false;
			this.helpLinkTextBox.Text = "";
			// 
			// helpLinkLabel
			// 
			this.helpLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.helpLinkLabel.Location = new System.Drawing.Point(8, 120);
			this.helpLinkLabel.Name = "helpLinkLabel";
			this.helpLinkLabel.Size = new System.Drawing.Size(88, 16);
			this.helpLinkLabel.TabIndex = 22;
			this.helpLinkLabel.Text = "Help Link:";
			this.helpLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionSourceValueTextBox
			// 
			this.exceptionSourceValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionSourceValueTextBox.Location = new System.Drawing.Point(96, 72);
			this.exceptionSourceValueTextBox.Name = "exceptionSourceValueTextBox";
			this.exceptionSourceValueTextBox.ReadOnly = true;
			this.exceptionSourceValueTextBox.Size = new System.Drawing.Size(328, 20);
			this.exceptionSourceValueTextBox.TabIndex = 21;
			this.exceptionSourceValueTextBox.TabStop = false;
			this.exceptionSourceValueTextBox.Text = "";
			// 
			// exceptionTargetMethodValueTextBox
			// 
			this.exceptionTargetMethodValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionTargetMethodValueTextBox.Location = new System.Drawing.Point(96, 96);
			this.exceptionTargetMethodValueTextBox.Name = "exceptionTargetMethodValueTextBox";
			this.exceptionTargetMethodValueTextBox.ReadOnly = true;
			this.exceptionTargetMethodValueTextBox.Size = new System.Drawing.Size(328, 20);
			this.exceptionTargetMethodValueTextBox.TabIndex = 19;
			this.exceptionTargetMethodValueTextBox.TabStop = false;
			this.exceptionTargetMethodValueTextBox.Text = "";
			// 
			// exceptionMessageValueTextBox
			// 
			this.exceptionMessageValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.exceptionMessageValueTextBox.Location = new System.Drawing.Point(96, 8);
			this.exceptionMessageValueTextBox.Multiline = true;
			this.exceptionMessageValueTextBox.Name = "exceptionMessageValueTextBox";
			this.exceptionMessageValueTextBox.ReadOnly = true;
			this.exceptionMessageValueTextBox.Size = new System.Drawing.Size(328, 56);
			this.exceptionMessageValueTextBox.TabIndex = 18;
			this.exceptionMessageValueTextBox.TabStop = false;
			this.exceptionMessageValueTextBox.Text = "";
			// 
			// exceptionTargetMethodLabel
			// 
			this.exceptionTargetMethodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exceptionTargetMethodLabel.Location = new System.Drawing.Point(8, 96);
			this.exceptionTargetMethodLabel.Name = "exceptionTargetMethodLabel";
			this.exceptionTargetMethodLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionTargetMethodLabel.TabIndex = 16;
			this.exceptionTargetMethodLabel.Text = "Target Method:";
			this.exceptionTargetMethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionSourceLabel
			// 
			this.exceptionSourceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exceptionSourceLabel.Location = new System.Drawing.Point(8, 72);
			this.exceptionSourceLabel.Name = "exceptionSourceLabel";
			this.exceptionSourceLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionSourceLabel.TabIndex = 14;
			this.exceptionSourceLabel.Text = "Source:";
			this.exceptionSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// exceptionMessageLabel
			// 
			this.exceptionMessageLabel.Location = new System.Drawing.Point(8, 8);
			this.exceptionMessageLabel.Name = "exceptionMessageLabel";
			this.exceptionMessageLabel.Size = new System.Drawing.Size(88, 16);
			this.exceptionMessageLabel.TabIndex = 12;
			this.exceptionMessageLabel.Text = "Message:";
			this.exceptionMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// stackTab
			// 
			this.stackTab.Controls.Add(this.stackTraceListView);
			this.stackTab.Location = new System.Drawing.Point(4, 22);
			this.stackTab.Name = "stackTab";
			this.stackTab.Size = new System.Drawing.Size(432, 150);
			this.stackTab.TabIndex = 2;
			this.stackTab.Text = "Stack Trace";
			this.stackTab.Visible = false;
			// 
			// stackTraceListView
			// 
			this.stackTraceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.stackTraceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.methodColumnHeader});
			this.stackTraceListView.Location = new System.Drawing.Point(8, 7);
			this.stackTraceListView.Name = "stackTraceListView";
			this.stackTraceListView.Size = new System.Drawing.Size(416, 136);
			this.stackTraceListView.TabIndex = 1;
			this.stackTraceListView.View = System.Windows.Forms.View.Details;
			// 
			// methodColumnHeader
			// 
			this.methodColumnHeader.Text = "Method";
			this.methodColumnHeader.Width = 400;
			// 
			// innerTab
			// 
			this.innerTab.Controls.Add(this.innerExceptionsTreeView);
			this.innerTab.Location = new System.Drawing.Point(4, 22);
			this.innerTab.Name = "innerTab";
			this.innerTab.Size = new System.Drawing.Size(432, 150);
			this.innerTab.TabIndex = 1;
			this.innerTab.Text = "Inner Exception Trace";
			this.innerTab.Visible = false;
			// 
			// innerExceptionsTreeView
			// 
			this.innerExceptionsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.innerExceptionsTreeView.ImageIndex = -1;
			this.innerExceptionsTreeView.Location = new System.Drawing.Point(8, 8);
			this.innerExceptionsTreeView.Name = "innerExceptionsTreeView";
			this.innerExceptionsTreeView.SelectedImageIndex = -1;
			this.innerExceptionsTreeView.Size = new System.Drawing.Size(416, 136);
			this.innerExceptionsTreeView.TabIndex = 0;
			// 
			// specificTab
			// 
			this.specificTab.Controls.Add(this.otherInfoListView);
			this.specificTab.Location = new System.Drawing.Point(4, 22);
			this.specificTab.Name = "specificTab";
			this.specificTab.Size = new System.Drawing.Size(432, 150);
			this.specificTab.TabIndex = 3;
			this.specificTab.Text = "Other Information";
			this.specificTab.Visible = false;
			// 
			// otherInfoListView
			// 
			this.otherInfoListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.otherInfoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								this.nameColumnHeader,
																								this.descriptionColumnHeader});
			this.otherInfoListView.Location = new System.Drawing.Point(8, 8);
			this.otherInfoListView.Name = "otherInfoListView";
			this.otherInfoListView.Size = new System.Drawing.Size(416, 136);
			this.otherInfoListView.TabIndex = 0;
			this.otherInfoListView.View = System.Windows.Forms.View.Details;
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 120;
			// 
			// descriptionColumnHeader
			// 
			this.descriptionColumnHeader.Text = "Description";
			this.descriptionColumnHeader.Width = 220;
			// 
			// buttonCopyErrorMessage
			// 
			this.buttonCopyErrorMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCopyErrorMessage.Location = new System.Drawing.Point(160, 64);
			this.buttonCopyErrorMessage.Name = "buttonCopyErrorMessage";
			this.buttonCopyErrorMessage.Size = new System.Drawing.Size(128, 23);
			this.buttonCopyErrorMessage.TabIndex = 14;
			this.buttonCopyErrorMessage.Text = "C&opy error message";
			this.buttonCopyErrorMessage.Click += new System.EventHandler(this.buttonCopyErrorMessage_Click);
			// 
			// ErrorDialogForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(458, 288);
			this.Controls.Add(this.buttonCopyErrorMessage);
			this.Controls.Add(this.errorDialogTabControl);
			this.Controls.Add(this.errorDialogMessageLabel);
			this.Controls.Add(this.dismissButton);
			this.Controls.Add(this.moreLessButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorDialogForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Exception";
			this.Load += new System.EventHandler(this.errorDialogForm_Load);
			this.errorDialogTabControl.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.stackTab.ResumeLayout(false);
			this.innerTab.ResumeLayout(false);
			this.specificTab.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
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

		#endregion

		#region Event Handlers

		/// <summary>
		/// Toggle the details tabs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void moreLessButton_Click(object sender, System.EventArgs e)
		{
			if (m_isShowingDetails)
			{
				this.Height = m_showBasicHeight;
				moreLessButton.Text = "&More";
				moreLessButton.ImageIndex = 0;
			}
			else
			{
				this.Height = m_showDetailsHeight;
				moreLessButton.Text = "&Less";
				moreLessButton.ImageIndex = 1;
			}

			m_isShowingDetails = !m_isShowingDetails;

		}

		/// <summary>
		/// Close the error dialog form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dismissButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Set the form's initial (no details) state and title
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void errorDialogForm_Load(object sender, System.EventArgs e)
		{
			// Sort out sizes and values to account for use of Large fonts in SE
			// build machines
			this.m_showBasicHeight = this.Height * 128/320;
			this.m_showDetailsHeight = this.Height;
			this.Height = m_showBasicHeight;
			exceptionMessageValueTextBox.Width = generalTab.Right - exceptionMessageValueTextBox.Left - 10;
			exceptionMessageValueTextBox.Height = generalTab.Height / 3 + 6;
			exceptionSourceValueTextBox.Width = generalTab.Right - exceptionSourceValueTextBox.Left -10;
			exceptionSourceValueTextBox.Top = exceptionMessageValueTextBox.Bottom + 8;
			exceptionSourceLabel.Top = exceptionSourceValueTextBox.Top;
			exceptionTargetMethodValueTextBox.Width = generalTab.Right - exceptionTargetMethodValueTextBox.Left -10;
			exceptionTargetMethodValueTextBox.Top = exceptionSourceValueTextBox.Bottom + 8;
			exceptionTargetMethodLabel.Top = exceptionTargetMethodValueTextBox.Top;
			helpLinkTextBox.Width = generalTab.Right - helpLinkTextBox.Left -10;
			helpLinkTextBox.Top = exceptionTargetMethodValueTextBox.Bottom + 8;
			helpLinkLabel.Top = helpLinkTextBox.Top;

			if (m_exception != null)
			{
				this.errorDialogMessageLabel.Text = this.m_exception.Message;
				this.Text = this.m_exception.GetType().Name;
				displayGeneralInformation();
			}
			else
			{
				this.errorDialogMessageLabel.Text = "Unknown";
			}
		}

		/// <summary>
		/// Update the details displayed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabInfo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			displayTabInfo();
		}

		/// <summary>
		/// Compile a summary of the exception information
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCopyErrorMessage_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(BuildExceptionInfo(), true);
		}


		#endregion

		/// <summary>
		/// Hides the inherited show to force a modal ShowDialog
		/// </summary>
		public new void Show()
		{
			ShowDialog();
		}

		#region Private Methods

		/// <summary>
		/// Show the first (general) tab information
		/// </summary>
		private void displayGeneralInformation()
		{
			if(false == this.m_hasGeneralBeenCalled)
			{
				this.exceptionMessageValueTextBox.Text = this.m_exception.Message;
				this.exceptionSourceValueTextBox.Text = this.m_exception.Source;
				this.exceptionTargetMethodValueTextBox.Text = getTargetMethodFormat(this.m_exception);
				this.helpLinkTextBox.Text = this.m_exception.HelpLink;
				this.m_hasGeneralBeenCalled = true;
			}
		}

		/// <summary>
		/// Retrieve the information for the method that threw the exception, if any
		/// </summary>
		/// <param name="targetException"></param>
		/// <returns></returns>
		private string getTargetMethodFormat(Exception targetException)
		{
			if (targetException.TargetSite != null)
			{
				return "[" + 
					targetException.TargetSite.DeclaringType.Assembly.GetName().Name + "]" +
					targetException.TargetSite.DeclaringType + 
					"." + targetException.TargetSite.Name + "()";
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Retrieve any custom information added to the exception
		/// </summary>
		/// <param name="targetException">Exception to examine</param>
		/// <returns>Hashtable name/value collection containing custom info</returns>
		private Hashtable getCustomExceptionInfo(Exception targetException)
		{
			Hashtable customInfoHashTable = new Hashtable();
			
			foreach(PropertyInfo targetPropertyInfo in targetException.GetType().GetProperties())
			{
				Type baseException = typeof(System.Exception);

				if(null == baseException.GetProperty(targetPropertyInfo.Name))
				{
					customInfoHashTable.Add(targetPropertyInfo.Name, targetPropertyInfo.GetValue(targetException, null));
				}
			}

			return customInfoHashTable;
		}

		/// <summary>
		/// Show the inner exception details tree
		/// </summary>
		private void displayInnerExceptionTrace()
		{
			if(false == this.m_hasInnerExceptionBeenCalled)
			{
				Exception innerTargetException = this.m_exception;
				TreeNode parentNode = null, 
					childNode = null, childMessage = null,
					childTarget = null;

				this.innerExceptionsTreeView.BeginUpdate();

				while(null != innerTargetException)
				{
					childNode = new TreeNode(innerTargetException.GetType().FullName);
					childMessage = new TreeNode(innerTargetException.Message);
					childTarget = new TreeNode(getTargetMethodFormat(innerTargetException));
					
					childNode.Nodes.Add(childMessage);
					childNode.Nodes.Add(childTarget);

					if(null != parentNode)
					{
						parentNode.Nodes.Add(childNode);
					}
					else
					{
						this.innerExceptionsTreeView.Nodes.Add(childNode);	
					}

					parentNode = childNode;
					innerTargetException = innerTargetException.InnerException;
				}

				this.innerExceptionsTreeView.EndUpdate();
				this.m_hasInnerExceptionBeenCalled = true;
			}
		}

		/// <summary>
		/// Dump the stack
		/// </summary>
		private void displayStackTrace()
		{
			if(false == this.m_hasStackTraceBeenCalled)
			{
				if (this.m_exception.StackTrace != null)
				{
					string[] stackTraceArray = this.m_exception.StackTrace.Split(new char[] {'\n'});

					foreach(string stackTraceItem in stackTraceArray)
					{
						this.stackTraceListView.Items.Add(new ListViewItem(stackTraceItem));
					}
				}
				this.m_hasStackTraceBeenCalled = true;
			}
		}

		/// <summary>
		/// Show the custom info
		/// </summary>
		private void displayOtherInformation()
		{
			if(false == this.m_hasOtherInformationBeenCalled)
			{
				Hashtable customInfoHashTable = this.getCustomExceptionInfo(this.m_exception);
				IDictionaryEnumerator dictionaryEnumeratorInterface = customInfoHashTable.GetEnumerator();
				
				this.otherInfoListView.Items.Clear();
				this.otherInfoListView.BeginUpdate();

				ListViewItem newListViewItem;

				while(dictionaryEnumeratorInterface.MoveNext())
				{
					newListViewItem = new ListViewItem(dictionaryEnumeratorInterface.Key.ToString());
					if(null != dictionaryEnumeratorInterface.Value)
					{
						newListViewItem.SubItems.Add(dictionaryEnumeratorInterface.Value.ToString());
					}
					this.otherInfoListView.Items.Add(newListViewItem);
				}

				this.otherInfoListView.EndUpdate();
				this.m_hasOtherInformationBeenCalled = true;
			}
		}

		/// <summary>
		/// Select info tab to display
		/// </summary>
		private void displayTabInfo()
		{
			switch(this.errorDialogTabControl.SelectedIndex)
			{
				case (int)ErrorDialogTab.General:
					displayGeneralInformation();
					break;
				case (int)ErrorDialogTab.Stack:
					displayStackTrace();
					break;
				case (int)ErrorDialogTab.InnerException:
					displayInnerExceptionTrace();
					break;
				case (int)ErrorDialogTab.Other:
					displayOtherInformation();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Compile a summary of the exception information
		/// </summary>
		/// <returns>String containing the exception information</returns>
		/// <remarks>This code is cribbed from the ExceptionManagement application block which unfortunatly doesn't expose
		/// a method to generate this information.</remarks>
		private string BuildExceptionInfo()
		{
			#region Load the AdditionalInformation Collection with environment data.
			// Create the Additional Information collection if it does not exist.
			NameValueCollection additionalInfo = new NameValueCollection();

			// Add environment information to the information collection.
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", Environment.MachineName);
			}
			catch(SecurityException)
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", "Permission Denied");
			}
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".MachineName", "Exception Accessing Information");
			}
					
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp", DateTime.Now.ToString());
			}
			catch(SecurityException)
			{					
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp","Permission Denied");
			}
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".TimeStamp", "Exception Accessing Information");
			}					
									
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", Assembly.GetExecutingAssembly().FullName);
			}
			catch(SecurityException)
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", "Permission Denied");
			}	
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".FullName", "Exception Accessing Information");
			}
					
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", AppDomain.CurrentDomain.FriendlyName);
			}
			catch(SecurityException)
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", "Permission Denied");
			}
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".AppDomainName", "Exception Accessing Information");
			}
						
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", Thread.CurrentPrincipal.Identity.Name);
			}
			catch(SecurityException)
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", "Permission Denied");
			}
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".ThreadIdentity", "Exception Accessing Information");
			}
				
			try
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", WindowsIdentity.GetCurrent().Name);
			}
			catch(SecurityException)
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", "Permission Denied");
			}
			catch
			{
				additionalInfo.Add(EXCEPTIONMANAGER_NAME + ".WindowsIdentity", "Exception Accessing Information");
			}											
			#endregion

			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();

			#region Record the contents of the AdditionalInfo collection
			// Record General information.
			strInfo.AppendFormat("{0}General Information {0}{1}{0}Additional Info:", Environment.NewLine, TEXT_SEPARATOR);

			foreach (string i in additionalInfo)
			{
				strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, additionalInfo.Get(i));
			}
			#endregion

			#region Loop through each exception class in the chain of exception objects
			// Loop through each exception class in the chain of exception objects.
			Exception currentException = this.m_exception;	// Temp variable to hold InnerException object during the loop.
			int intExceptionCount = 1;				// Count variable to track the number of exceptions in the chain.
			do
			{
				// Write title information for the exception object.
				strInfo.AppendFormat("{0}{0}{1}) Exception Information{0}{2}", Environment.NewLine, intExceptionCount.ToString(), TEXT_SEPARATOR);
				strInfo.AppendFormat("{0}Exception Type: {1}", Environment.NewLine, currentException.GetType().FullName);
				
				#region Loop through the public properties of the exception object and record their value
				// Loop through the public properties of the exception object and record their value.
				PropertyInfo[] aryPublicProperties = currentException.GetType().GetProperties();
				NameValueCollection currentAdditionalInfo;
				foreach (PropertyInfo p in aryPublicProperties)
				{
					// Do not log information for the InnerException or StackTrace. This information is 
					// captured later in the process.
					if (p.Name != "InnerException" && p.Name != "StackTrace")
					{
						if (p.GetValue(currentException,null) == null)
						{
							strInfo.AppendFormat("{0}{1}: NULL", Environment.NewLine, p.Name);
						}
						else
						{
							// Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
							if (p.Name == "AdditionalInformation" && currentException is BaseApplicationException)
							{
								// Verify the collection is not null.
								if (p.GetValue(currentException,null) != null)
								{
									// Cast the collection into a local variable.
									currentAdditionalInfo = (NameValueCollection)p.GetValue(currentException,null);

									// Check if the collection contains values.
									if (currentAdditionalInfo.Count > 0)
									{
										strInfo.AppendFormat("{0}AdditionalInformation:", Environment.NewLine);

										// Loop through the collection adding the information to the string builder.
										for (int i = 0; i < currentAdditionalInfo.Count; i++)
										{
											strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, currentAdditionalInfo.GetKey(i), currentAdditionalInfo[i]);
										}
									}
								}
							}
								// Otherwise just write the ToString() value of the property.
							else
							{
								strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, p.Name, p.GetValue(currentException,null));
							}
						}
					}
				}
				#endregion

				#region Record the Exception StackTrace
				// Record the StackTrace with separate label.
				if (currentException.StackTrace != null)
				{
					strInfo.AppendFormat("{0}{0}StackTrace Information{0}{1}", Environment.NewLine, TEXT_SEPARATOR);
					strInfo.AppendFormat("{0}{1}", Environment.NewLine, currentException.StackTrace);
				}
				#endregion

				// Reset the temp exception object and iterate the counter.
				currentException = currentException.InnerException;
				intExceptionCount++;
			} while (currentException != null);
			#endregion

			return strInfo.ToString();
		}

		#endregion
	}
}
