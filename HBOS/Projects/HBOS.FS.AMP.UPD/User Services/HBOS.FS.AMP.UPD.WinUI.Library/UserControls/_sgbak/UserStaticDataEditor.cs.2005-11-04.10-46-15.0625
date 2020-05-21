using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Users;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for UserStaticDataEditor.
	/// </summary>
	public class UserStaticDataEditor : StaticDataEditor
	{
		#region Private Members

		private GroupBox permissionsGroupBox;
		private TreeView permissionsTreeView;
		private TextBox userNameTextBox;
		private Label userNameLabel;
		private TextBox loginIDTextBox;
		private Label loginIDLabel;

		private UPDPrincipal m_loggedOnUser;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Creates a new <see cref="UserStaticDataEditor"/> instance.
		/// </summary>
		public UserStaticDataEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			m_loggedOnUser = (UPDPrincipal) Thread.CurrentPrincipal;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Properties

		private User currentUser;
		private bool m_IgnoreClick;

		/// <summary>
		/// Gets or sets the edit user.
		/// </summary>
		/// <value></value>
		public User CurrentUser
		{
			get { return currentUser; }

			set
			{
				bool indexChanged = currentUser != value;
				if (!indexChanged && m_loggedOnUser.Identity.Name == loginIDTextBox.Text)
				{
					//pick up the new roles
					Thread.CurrentPrincipal = new UPDPrincipal((UPDIdentity) Principal().Identity, Principal().CompanyCode,
						Principal().CurrentCompanyValuationDateAndTime, Principal().NextCompanyValuationDateAndTime,
						Principal().PreviousCompanyValuationDateAndTime);

					UPDRoleCheckedForm frm = (UPDRoleCheckedForm) this.FindForm();
					frm.ChangedCompany = true;
					frm.Close();
				}
				else
				{
					currentUser = value;
					currentUser.Permissions = UserController.GetAllPermissions(currentUser, Principal().CompanyCode, GlobalRegistry.ConnectionString);

					clearErrors(this);
					displayUser();
					if (indexChanged)
					{
						Changed = false;
					}
				}
			}
		}

		private UPDPrincipal Principal()
		{
			return (UPDPrincipal) Thread.CurrentPrincipal;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// display validation errors in the UI
		/// </summary>
		/// <param name="loginIDErrorMessage"></param>
		/// <param name="userNameErrorMessage"></param>
		private void showErrors(string loginIDErrorMessage, string userNameErrorMessage)
		{
			this.clearErrors(this);
			bool isValid = true;
			if (loginIDErrorMessage != null && loginIDErrorMessage.Length > 0)
			{
				loginIDTextBox.Focus();
				isValid = false;
				setError(loginIDTextBox, loginIDErrorMessage);
			}

			if (userNameErrorMessage != null && userNameErrorMessage.Length > 0)
			{
				if (isValid) //if focus not already determined
				{
					userNameTextBox.Focus();
				}
				isValid = false;
				setError(userNameTextBox, userNameErrorMessage);
			}

			if (!isValid) showErrorDialog("This user cannot be saved:");
		}

		#endregion

		#region Private Methods

		private void AddNodes(ref TreeNode node, Permission group)
		{
			foreach (Permission permission in group)
			{
				TreeNode NewNode = node.Nodes.Add(permission.DisplayName);
				NewNode.Tag = permission;
				NewNode.Checked = permission.Granted;
				AddNodes(ref NewNode, permission);
			}
		}

		private void BuildTree()
		{
			Permission permissions = CurrentUser.Permissions;

			//root
			TreeNode RootNode = new TreeNode(permissions.DisplayName);
			RootNode.Tag = permissions;
			m_IgnoreClick = true;
			AddNodes(ref RootNode, permissions);
			m_IgnoreClick = false;
			this.permissionsTreeView.Nodes.Clear();
			this.permissionsTreeView.Nodes.Add(RootNode);
			this.permissionsTreeView.ExpandAll();
		}

		/// <summary>
		/// Moves the data from the the CurrentUser entity to the controls
		/// </summary>
		private void displayUser()
		{
			//lock the textbox unless we are adding a new user 
			//to change the login id for a user the user record must be deleted and recreated 

			loginIDTextBox.ReadOnly = !ListManager.SelectedIsNew;
			loginIDTextBox.Text = CurrentUser.LogOnID;
			userNameTextBox.Text = CurrentUser.UserName;

			if (permissionsTreeView.Nodes.Count == 0)
			{
				BuildTree();
			}
			else
			{
				m_IgnoreClick = true;
				CheckNodes();
				m_IgnoreClick = false;

			}
		}

		private void CheckNodes()
		{
			CheckNodes(permissionsTreeView.Nodes[0]);
		}

		private void CheckNodes(TreeNode node)
		{
			node.Tag = CurrentUser.Permissions[((Permission) node.Tag).PermissionId];
			node.Checked = ((Permission) node.Tag).Granted;

			foreach (TreeNode childnode in node.Nodes)
			{
				CheckNodes(childnode);
			}
		}

		/// <summary>
		/// Moves the data from the controls to the CurrentUser entity
		/// </summary>
		private void updateUser()
		{
			if (CurrentUser.LogOnID != loginIDTextBox.Text)
			{
				CurrentUser.LogOnID = loginIDTextBox.Text;
			}

			if (CurrentUser.UserName != userNameTextBox.Text)
			{
				CurrentUser.UserName = userNameTextBox.Text;
			}
		}

		private bool saveUser()
		{
			T.E();
			bool userSaved = false;

			try
			{
				UserCollection users = new UserCollection();
				users.Add(CurrentUser);

				UserController.SaveUsers(users, GlobalRegistry.CompanyCode, GlobalRegistry.ConnectionString);
				Changed = false;
				userSaved = true;
			}
			catch (ConstraintViolationException)
			{
				showErrors(MessageBoxHelper.DialogText("DuplicateUserBody"), "");
			}
			catch (ConcurrencyViolationException ex)
			{
				GUIExceptionHelper.LogAndDisplayException("UserChangedBody", "UserUnableToSaveTitle", ex);
			}
			catch (Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UserUnableToSaveTitle", ex);
			}
			finally
			{
				T.X();
			}

			return userSaved;
		}

		/// <summary>
		/// Ensures that all of the child nodes are checked if the parent is
		/// </summary>
		/// <param name="sender">The event sender</param>
		/// <param name="e">The event arguments</param>
		private void permissionsTreeView_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (!m_IgnoreClick)
			{
				bool NodeChecked = e.Node.Checked;

				if (!NodeChecked && e.Node.Parent == null && m_loggedOnUser.Identity.Name == loginIDTextBox.Text)
				{
					switch (MessageBoxHelper.Show("UserPermissionsWarningBody", "UserPermissionsWarningTitle", MessageBoxButtons.YesNoCancel))
					{
						case DialogResult.No:
							NodeChecked = true;
							return;
						case DialogResult.Cancel:
							goto case DialogResult.No;
					}
				}

				((Permission) e.Node.Tag).Granted = NodeChecked;
				Changed = true;
				m_IgnoreClick = true;
				CheckNodes();
				m_IgnoreClick = false;
			}
		}

		private void default_TextChanged(object sender, EventArgs e)
		{
			Changed = true;
		}

		private bool validateUser()
		{
			bool valid = true;
			string idErrorMessage = string.Empty;
			string nameErrorMessage = string.Empty;

			if (loginIDTextBox.Text == string.Empty)
			{
				loginIDTextBox.Focus();
				idErrorMessage = "You must specify a login ID";
				valid = false;
			}

			if (userNameTextBox.Text == string.Empty)
			{
				if (valid) userNameTextBox.Focus();
				nameErrorMessage = "You must specify a user name";
				valid = false;
			}

			if (!valid)
			{
				showErrors(idErrorMessage, nameErrorMessage);
			}

			return valid;
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.permissionsGroupBox = new System.Windows.Forms.GroupBox();
			this.permissionsTreeView = new System.Windows.Forms.TreeView();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.userNameLabel = new System.Windows.Forms.Label();
			this.loginIDTextBox = new System.Windows.Forms.TextBox();
			this.loginIDLabel = new System.Windows.Forms.Label();
			this.permissionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// permissionsGroupBox
			// 
			this.permissionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.permissionsGroupBox.Controls.Add(this.permissionsTreeView);
			this.permissionsGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.permissionsGroupBox.Location = new System.Drawing.Point(16, 80);
			this.permissionsGroupBox.Name = "permissionsGroupBox";
			this.permissionsGroupBox.Size = new System.Drawing.Size(408, 368);
			this.permissionsGroupBox.TabIndex = 15;
			this.permissionsGroupBox.TabStop = false;
			this.permissionsGroupBox.Text = "Permissions";
			// 
			// permissionsTreeView
			// 
			this.permissionsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.permissionsTreeView.CheckBoxes = true;
			this.permissionsTreeView.ImageIndex = -1;
			this.permissionsTreeView.Location = new System.Drawing.Point(16, 24);
			this.permissionsTreeView.Name = "permissionsTreeView";
			this.permissionsTreeView.SelectedImageIndex = -1;
			this.permissionsTreeView.Size = new System.Drawing.Size(376, 328);
			this.permissionsTreeView.TabIndex = 11;
			this.permissionsTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.permissionsTreeView_AfterCheck);
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Location = new System.Drawing.Point(128, 48);
			this.userNameTextBox.MaxLength = 50;
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(184, 20);
			this.userNameTextBox.TabIndex = 14;
			this.userNameTextBox.Text = "";
			this.userNameTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// userNameLabel
			// 
			this.userNameLabel.Location = new System.Drawing.Point(16, 48);
			this.userNameLabel.Name = "userNameLabel";
			this.userNameLabel.Size = new System.Drawing.Size(88, 22);
			this.userNameLabel.TabIndex = 13;
			this.userNameLabel.Text = "User Name:";
			// 
			// loginIDTextBox
			// 
			this.loginIDTextBox.Location = new System.Drawing.Point(128, 16);
			this.loginIDTextBox.MaxLength = 50;
			this.loginIDTextBox.Name = "loginIDTextBox";
			this.loginIDTextBox.Size = new System.Drawing.Size(184, 20);
			this.loginIDTextBox.TabIndex = 12;
			this.loginIDTextBox.Text = "";
			this.loginIDTextBox.TextChanged += new System.EventHandler(this.default_TextChanged);
			// 
			// loginIDLabel
			// 
			this.loginIDLabel.Location = new System.Drawing.Point(16, 16);
			this.loginIDLabel.Name = "loginIDLabel";
			this.loginIDLabel.Size = new System.Drawing.Size(88, 22);
			this.loginIDLabel.TabIndex = 11;
			this.loginIDLabel.Text = "Login ID:";
			// 
			// UserStaticDataEditor
			// 
			this.Controls.Add(this.permissionsGroupBox);
			this.Controls.Add(this.userNameTextBox);
			this.Controls.Add(this.userNameLabel);
			this.Controls.Add(this.loginIDTextBox);
			this.Controls.Add(this.loginIDLabel);
			this.Name = "UserStaticDataEditor";
			this.Size = new System.Drawing.Size(440, 464);
			this.permissionsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets the export parameters.
		/// </summary>
		/// <returns></returns>
		protected override void getExportParameters(StaticDataExportParameters parameters)
		{
			T.E();
			
			parameters.CollectionToExport =UserController.LoadExportData(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode);
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.UserStaticData.xslt", "users"));
			T.X();
		}


		/// <summary>
		/// Does the delete of the fund.
		/// </summary>
		protected override void doDelete()
		{
			T.E();
			CurrentUser.Permissions.IsDeleted = true;
			CurrentUser.IsDeleted = true;
			if (saveUser())
			{
				CurrentUser.IsDirty = false;
				CurrentUser.IsNew = false;

				CurrentUser.Permissions.IsNew = false;
				CurrentUser.Permissions.IsDeleted = false;
				CurrentUser.Permissions.IsDirty = false;
			}
			T.X();
		}

		/// <summary>
		/// Loads the selected user into the editor
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			User selectedUser = (User) ListManager.SelectedItem;
			CurrentUser = UserController.LoadUser(GlobalRegistry.ConnectionString, selectedUser.LogOnID, GlobalRegistry.CompanyCode);
			T.X();
		}

		/// <summary>
		/// Actions to take when user requests new User
		/// </summary>
		protected override void doNew()
		{
			T.E();
			CurrentUser = new User();
			CurrentUser.LogOnID = "New User Login";
			loginIDTextBox.Focus();
			T.X();
		}

		/// <summary>
		/// Saves the currently edited user
		/// </summary>
		/// <returns></returns>
		protected override bool doSave()
		{
			T.E();
			bool userSaved = false;
			if (validateUser())
			{
				updateUser();
				userSaved = saveUser();
				if (userSaved)
				{
					CurrentUser.IsDirty = false;
					CurrentUser.IsNew = false;
					CurrentUser.IsDeleted = false;

					CurrentUser.Permissions.IsNew = false;
					CurrentUser.Permissions.IsDeleted = false;
					CurrentUser.Permissions.IsDirty = false;
					CurrentUser = CurrentUser; //reset user in order to re-display
					Changed = false;

					ListManager.ChangeSelected(CurrentUser);
				}
				T.X();
			}

			return userSaved;
		}

		/// <summary>
		/// Gets the description of the current user.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentUser.LogOnID; }
		}

		/// <summary>
		/// Gets description of entity type
		/// </summary>
		protected override string EditType
		{
			get { return "User"; }
		}

		#endregion
	}
}