using System;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Users;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for UserStaticDataViewController.
	/// </summary>
	public class UserStaticDataViewController : StaticDataViewController
	{
		private UserStaticDataEditor m_editor;
		private StaticDataFrame m_frame;
		//UserCollection m_userList;
		private IList m_userList;
		private int m_selectedUserIndex;

		private string m_currentCompanyID;
		private string m_connectionString;

		/// <summary>
		/// Creates a new <see cref="UserStaticDataViewController"/> instance.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public UserStaticDataViewController(StaticDataFrame frame) : base(frame)
		{
			m_frame = frame;
			m_editor = new UserStaticDataEditor();
			m_frame.SelectListDisplayMember = "LoginID";

			// Retrieve the connection string
			m_connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

			// Get the currently selected company from the GUI principal thread
			UPDPrincipal updPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
			m_currentCompanyID = updPrincipal.CompanyCode;
		}

		#region overrides


		/// <summary>
		/// Handles state change when the entity being viewed in changed
		/// </summary>
		/// <param name="newEntity">The new entity being viewed.</param>
		protected override void EntityChanged(object newEntity)
		{
			if (newEntity is StaticDataFrame.NewEntity)
			{
				m_selectedUserIndex = m_userList.Count;
				//note - EntityChanged will get called again once New processing complete & m_userlist contains a pucker entity
			}
			else
			{
				// Cast the user and determine their position in the list
				m_selectedUserIndex = m_userList.IndexOf((User) newEntity);

				// Load the user into the editor
				m_editor.EditUser = (User) newEntity;
			}
		}

		/// <summary>
		/// Gets the editor for the entity.
		/// </summary>
		/// <returns>A user control to use as the editor</returns>
		protected override UserControl GetEntityEditor()
		{
			m_editor.SaveExecuted += new StaticDataEventHandler(UserStaticDataViewController_SaveUser);
			return m_editor;
		}

		/// <summary>
		/// Gets the entity collection for display. Each entity's ToString() method is used to determine display.
		/// </summary>
		/// <returns></returns>
		protected override IList GetEntityCollection()
		{
			// Load all users for the current company ID
			UserCollection users = UserController.LoadUsers(m_connectionString, m_currentCompanyID);
			//we need an array IList implementation, so we can add a new item

			ArrayList userArray = new ArrayList();
			for (int i = 0; i < users.Count; i++)
			{
				userArray.Add(users[i]);
			}
			m_userList = userArray;

			m_frame.SelectListDisplayMember = "LoginID";
			return m_userList;
		}

		/// <summary>
		/// Provides a custom initialisation point that can be overriden, does nothing in the default implementation
		/// </summary>
		protected override void CustomInitialisation()
		{
		}

		/// <summary>
		/// Gets an array of allowable actions in the GUI, you should hook to the Executed event to
		/// act on the action from the GUI.
		/// </summary>
		protected override StaticDataAction[] GetActions()
		{
			StaticDataAction[] actions = new StaticDataAction[5];

			// Export to CSV button
			actions[1] = new StaticDataAction();
			actions[1].Text = "Export";
			actions[1].Executed += new StaticDataEventHandler(UserStaticDataViewController_ExportUser);

			// Save button
			actions[3] = new StaticDataAction();
			actions[3].Text = "Save";
			actions[3].Executed += new StaticDataEventHandler(UserStaticDataViewController_SaveUser);

			// Cancel button
			actions[4] = new StaticDataAction();
			actions[4].Text = "Cancel";
			actions[4].Executed += new StaticDataEventHandler(UserStaticDataViewController_CancelChanges);

			// New button
			actions[2] = new StaticDataAction();
			actions[2].Text = "New";
			actions[2].Executed += new StaticDataEventHandler(UserStaticDataViewController_NewUser);

			// Delete button
			actions[0] = new StaticDataAction();
			actions[0].Text = "Delete";
			actions[0].Executed += new StaticDataEventHandler(UserStaticDataViewController_DeleteUser);

			return actions;

		}

		#endregion

		private void UserStaticDataViewController_DeleteUser(object sender, StaticDataEventArgs e)
		{
			//Request confirmation
			User deleteUser = m_editor.EditUser;
			DialogResult result = MessageBox.Show("Are you sure you want to delete user " + deleteUser.LogOnID + "?", "Delete User Confirmation", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				try
				{
					deleteUser.Permissions.IsDeleted = true;
					deleteUser.IsDeleted = true;
					saveUser(deleteUser);
					//MessageBox.Show("User successfully deleted");

					deleteUser.IsDirty = false;
					deleteUser.IsNew = false;

					deleteUser.Permissions.IsNew = false;
					deleteUser.Permissions.IsDeleted = false;
					deleteUser.Permissions.IsDirty = false;

					this.refreshList();
				}
				finally
				{
				}
			}
		}

		private void UserStaticDataViewController_ExportUser(object sender, StaticDataEventArgs e)
		{
			T.E();
			try
			{
				exportFiles(m_editor);

			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("UnableToExportBody", "UnableToExportTitle", ex, "asset funds");
			}
			finally
			{
				T.X();
			}

			/*
            // TODO: Use ObjectXPathNavigator to transform the full set of permissions to CSV

            SaveFileDialog selectFileDialog = new SaveFileDialog();

            if(selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportCSVGenerator csvCreator = new ExportCSVGenerator(m_userList, "HBOS.FS.AMP.UPD.WinUI.Classes.UserStaticData.xslt", selectFileDialog.FileName);
                    csvCreator.XsltCustomCollectionToFile();
                    MessageBox.Show("CSV File Successfully Created");
                }
                catch(Exception ex)
                {
                    ExceptionManager.Publish( ex );
                    T.Log("ExportCSVException");
                    T.DumpException( ex );
                    MessageBoxHelper.ShowError( "SystemError", "UserUnableToExportCSV", ex );
                }
            }
			*/

		}

		private void UserStaticDataViewController_NewUser(object sender, StaticDataEventArgs e)
		{
			if (frame.SelectList != null & frame.SelectList.Count > 0 && !(frame.SelectList[frame.SelectList.Count - 1] is StaticDataFrame.NewEntity))
			{
//				frame.New();

				//user may have cancelled
				if (frame.SelectList[frame.SelectList.Count - 1] is StaticDataFrame.NewEntity)
				{
					// Create a new user, add it to the list and call entity changed
					User newUser = new User(m_currentCompanyID);
					//static data frame adds a NewEntity type to m_userlist	so this not needed -> m_userList.Add(newUser);
					newUser.LogOnID = "New User Login";

					//set the User entity in the editor, which is the only reference to it
					EntityChanged(newUser);
				}
			}
		}

		private void UserStaticDataViewController_SaveUser(object sender, StaticDataEventArgs e)
		{
			// Get the user from the editor
			User editedUser = m_editor.EditUser;

			if (validateUser(editedUser))
			{
				try
				{
					saveUser(editedUser);
					editedUser.IsDirty = false;
					editedUser.IsNew = false;
					editedUser.IsDeleted = false;

					editedUser.Permissions.IsNew = false;
					editedUser.Permissions.IsDeleted = false;
					editedUser.Permissions.IsDirty = false;
					m_editor.EditUser = m_editor.EditUser; //reset user in order to re-display
					m_editor.Changed = false;


				}
				finally
				{
				}
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void UserStaticDataViewController_CancelChanges(object sender, StaticDataEventArgs e)
		{
			// Check that the modified version is not dirty...
			if (userNeedsSaving())
			{
				if (m_editor.EditUser.IsNew)
				{
					frame.SelectFirst();
				}
				else
				{
					m_userList = UserController.LoadUsers(m_connectionString, m_currentCompanyID);
					m_editor.EditUser = (User) m_userList[m_selectedUserIndex];
				}

			}

		}

		/// <summary>
		/// Allows the entity to change.
		/// </summary>
		/// <returns></returns>
		protected override StaticDataFrame.YesNoCancelEventArgs.YesNoCancelAction AllowEntityToChange()
		{
			return m_editor.AllowEntityToChange();
		}

		private bool userNeedsSaving()
		{
			if (m_editor.EditUser == null)
			{
				return false;
			}
			else
			{
				return m_editor.Changed;
			}
		}

		/// <summary>
		/// Validate the edited data
		/// </summary>
		/// <param name="editedUser">The edited user data</param>
		private bool validateUser(User editedUser)
		{
			// If the user is a new user
//            if (editedUser.IsNew)
//            {
//                // Check that the user's login and username are unique
//             //   if (m_userList.Contains(editedUser))
//
//            }

			return true;
		}



	}
}