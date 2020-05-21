using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.AMP.UPD.WinUI.Classes;

namespace HBOS.FS.AMP.UPD.WinUI
{
	/// <summary>
	/// Displays a message box that can contain text 
	/// buttons and symbols that inform and instruct the user.
	/// </summary>
	/// <remarks>
	/// All dialog text and captions are retrieved from the DialogText.resx file.
	/// </remarks>
	public class MessageBoxHelper
	{
		#region Constructor
		private MessageBoxHelper()
		{
		}

		#endregion

		#region Show methods
		/// <summary>
		/// Displays a message box using the resource string specified.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property"</see>.</exception>
		/// <remarks>By default, the message box displays an OK button. The message box does not contain a caption in the title.</remarks>
		public static DialogResult Show(string textResourceName)
		{
			return (MessageBox.Show(DialogText(textResourceName)));
		}


		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <remarks>By default, the message box displays an OK button.</remarks>
		public static DialogResult Show(string textResourceName, string captionResourceName)
		{
			return (MessageBox.Show(DialogText(textResourceName), DialogText(captionResourceName)));
		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption, and specified buttons
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The buttons parameter specified is not a member of <see cref="MessageBoxButtons"/>.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		public static DialogResult Show(string textResourceName, string captionResourceName,
			MessageBoxButtons buttons)
		{
			return (MessageBox.Show(
				DialogText(textResourceName),
				DialogText(captionResourceName),
				buttons));
		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption, and specified buttons and icon.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The buttons parameter specified is not a member of <see cref="MessageBoxButtons"/>.
		/// -or-
		/// The icon parameter specified is not a member of <see cref="MessageBoxIcon"/>.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		public static DialogResult Show(string textResourceName, string captionResourceName,
			MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return (MessageBox.Show(
				DialogText(textResourceName),
				DialogText(captionResourceName),
				buttons,
				icon));
		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption, and specified buttons and icon.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The buttons parameter specified is not a member of <see cref="MessageBoxButtons"/>. 
		/// -or- 
		/// The icon parameter specified is not a member of <see cref="MessageBoxIcon"/>. 
		/// -or- 
		/// defaultButton is not a member of <see cref="MessageBoxDefaultButton"/>.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		public static DialogResult Show(string textResourceName, string captionResourceName,
			MessageBoxButtons buttons, MessageBoxIcon icon,
			MessageBoxDefaultButton defaultButton)
		{
			return (MessageBox.Show(
				DialogText(textResourceName),
				DialogText(captionResourceName),
				buttons,
				icon,
				defaultButton));
		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption, and specified buttons and icon.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="buttons">One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for the message box.</param>
		/// <returns>One of the <see cref="DialogResult"/> values.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The buttons parameter specified is not a member of <see cref="MessageBoxButtons"/>. 
		/// -or- 
		/// The icon parameter specified is not a member of <see cref="MessageBoxIcon"/>. 
		/// -or- 
		/// defaultButton is not a member of <see cref="MessageBoxDefaultButton"/>.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <exception cref="ArgumentException">options specified both <see cref="MessageBoxOptions.DefaultDesktopOnly"/> and <see cref="MessageBoxOptions.ServiceNotification"/>. 
		/// -or-
		/// options specified <see cref="MessageBoxOptions.DefaultDesktopOnly"/> or <see cref="MessageBoxOptions.ServiceNotification"/> and specified a value in the owner parameter. These two options should be used only if you invoke the version of this method that does not take an owner parameter.
		/// -or-
		/// buttons specified an invalid combination of MessageBoxButtons.
		/// </exception>
		public static DialogResult Show(string textResourceName, string captionResourceName,
			MessageBoxButtons buttons, MessageBoxIcon icon,
			MessageBoxDefaultButton defaultButton,
			MessageBoxOptions options)
		{
			return (MessageBox.Show(
				DialogText(textResourceName),
				DialogText(captionResourceName),
				buttons,
				icon,
				defaultButton,
				options));
		}


		#endregion

		#region Show error methods
		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="ex">The exception who's full details will be displayed if dev style UI required.</param>
		/// <returns>For an error we do not need to know if they pressed ok or cancel.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <remarks>By default, the message box displays an OK button. 
		/// The exception will also be logged to the client event log</remarks>
		public static void ShowError(string textResourceName, string captionResourceName, Exception ex)
		{
			string errorUIStyle;

			try
			{
				errorUIStyle = GlobalRegistry.AppSettings.ErrorUIStyle.ToString();
			}
			catch
			{
				errorUIStyle = "EndUser";
			}

			if (errorUIStyle == "Dev" || errorUIStyle == "All")
			{
				ErrorDialog.Show(ex);
			}
			//make sure we always display some sort of dialog!
			if (errorUIStyle != "Dev" || errorUIStyle == "All")
			{
				MessageBox.Show(
					DialogText(textResourceName),
					DialogText(captionResourceName),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="ex">The exception who's full details will be displayed if dev style UI required.</param>
		/// <param name="bodyTextArgs">A list of string items to be applied into the resource body text.  This must be in the order of the target parameters of the resource string.</param>
		/// <returns>For an error we do not need to know if they pressed ok or cancel.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <remarks>By default, the message box displays an OK button.</remarks>
		public static void ShowError(string textResourceName, string captionResourceName, Exception ex, params string[] bodyTextArgs)
		{
			string errorUIStyle;

			try
			{
				errorUIStyle = GlobalRegistry.AppSettings.ErrorUIStyle.ToString();
			}
			catch
			{
				errorUIStyle = "EndUser";
			}

			if (errorUIStyle == "Dev" || errorUIStyle == "All")
			{
				ErrorDialog.Show(ex);
			}

			//make sure we always display some sort of dialog!
			if (errorUIStyle != "Dev" || errorUIStyle == "All")
			{
				string bodyText = string.Format(DialogText(textResourceName), bodyTextArgs);
				string captionText = DialogText(captionResourceName);

				MessageBox.Show(bodyText, captionText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="ex">The exception who's full details will be displayed if dev style UI required.</param>
		/// <param name="captionArg">Caption format item to be applied to the title caption from the resource file.</param>
		/// <param name="bodyTextArgs">A list of string format items to be applied into the resource body text.  This must be in the order of the target parameters of the resource string.</param>
		/// <returns>For an error we do not need to know if they pressed ok or cancel.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <remarks>By default, the message box displays an OK button.</remarks>
		public static void ShowError(string textResourceName, string captionResourceName, Exception ex, string captionArg, params string[] bodyTextArgs)
		{
			string errorUIStyle;

			try
			{
				errorUIStyle = GlobalRegistry.AppSettings.ErrorUIStyle.ToString();
			}
			catch
			{
				errorUIStyle = "EndUser";
			}

			if (errorUIStyle == "Dev" || errorUIStyle == "All")
			{
				ErrorDialog.Show(ex);
			}

			//make sure we always display some sort of dialog!
			if (errorUIStyle != "Dev" || errorUIStyle == "All")
			{
				string bodyText = string.Format(DialogText(textResourceName), bodyTextArgs);
				string captionText = string.Format(DialogText(captionResourceName), captionArg);

				MessageBox.Show(bodyText, captionText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Displays a message box using the resource strings specified for text and caption.
		/// </summary>
		/// <param name="textResourceName">The resource name to retrieve and display in the message box.</param>
		/// <param name="captionResourceName">The resource name to retrieve and display in the message box caption.</param>
		/// <param name="ex">The exception who's full details will be displayed if dev style UI required.</param>
		/// <param name="captionArg">Caption format item to be applied to the title caption from the resource file.</param>
		/// <param name="bodyArg">The body text parameter to be passed into the resource item.</param>
		/// <returns>For an error we do not need to know if they pressed ok or cancel.</returns>
		/// <exception cref="MissingManifestResourceException">The resource identifier passed was not found.</exception>
		/// <exception cref="InvalidOperationException">An attempt was made to display the <see cref="MessageBoxHelper"/> in a process that is not running in User Interactive mode. This is specified by the <see cref="SystemInformation.UserInteractive"> property</see>.</exception>
		/// <remarks>By default, the message box displays an OK button.</remarks>
		public static void ShowError(string textResourceName, string captionResourceName, Exception ex, string captionArg, string bodyArg)
		{
			string errorUIStyle;

			try
			{
				errorUIStyle = GlobalRegistry.AppSettings.ErrorUIStyle.ToString();
			}
			catch
			{
				errorUIStyle = "EndUser";
			}

			if (errorUIStyle == "Dev" || errorUIStyle == "All")
			{
				ErrorDialog.Show(ex);
			}

			//make sure we always display some sort of dialog!
			if (errorUIStyle != "Dev" || errorUIStyle == "All")
			{
				string bodyText = string.Format(DialogText(textResourceName), bodyArg);
				string captionText = string.Format(DialogText(captionResourceName), captionArg);

				MessageBox.Show(bodyText, captionText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		#endregion


		/// <summary>
		/// Returns the dialog text for a given resource name
		/// </summary>
		/// <param name="resourceName">Name of the resource.</param>
		/// <returns>The text associated with the resource name</returns>
		public static string DialogText(string resourceName)
		{
			ResourceManager resourceManager = new ResourceManager("HBOS.FS.AMP.UPD.WinUI.DialogText", Assembly.GetExecutingAssembly());
			string result = resourceManager.GetString(resourceName, CultureInfo.CurrentUICulture);

			if (result == null)
			{
				//this has been changed so that it is possible to pass the actual string you'd like to see 
				//to the helper not always a resource name
				result = resourceName;//string.Format("Missing Dialog Resource Text: {0}", resourceName);
			}

			return result;
		}

		/// <summary>
		/// Returns the dialog text for a given resource name and formats it with args
		/// </summary>
		/// <param name="resourceName">Name of the resource.</param>
		/// <param name="args">Arguments to format string with, aka string.Format</param>
		/// <returns>The text associated with the resource name</returns>
		public static string DialogText(string resourceName, params object[] args)
		{
			return string.Format(DialogText(resourceName), args);
		}

		#region Show question message
		/// <summary>
		/// Shows a question in a dialog box
		/// </summary>
		/// <param name="textResourceName">Name of the text resource.</param>
		/// <param name="captionResourceName">Name of the caption resource.</param>
		/// <returns>true when Yes pressed, false when No pressed</returns>
		public static bool ShowQuestion(string textResourceName, string captionResourceName)
		{
			DialogResult buttonPressed = Show(textResourceName,
			                                  captionResourceName,
			                                  MessageBoxButtons.YesNo,
			                                  MessageBoxIcon.Question);

			return (buttonPressed == DialogResult.Yes);

		}

		/// <summary>
		/// Shows a question in a dialog box
		/// </summary>
		/// <param name="textResourceName">Name of the text resource.</param>
		/// <param name="captionResourceName">Name of the caption resource.</param>
		/// <param name="args">Arguments to format textResource with, aka string.Format</param>
		/// <returns>true when Yes pressed, false when No pressed</returns>
		public static bool ShowQuestion(string textResourceName, string captionResourceName, params object[] args)
		{
			string text = DialogText(textResourceName, args);
			string caption = DialogText(captionResourceName);


			DialogResult buttonPressed = MessageBox.Show(
				text,
				caption,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			return (buttonPressed == DialogResult.Yes);
		}
		#endregion

		#region Show exclamation

		/// <summary>
		/// Shows an exclamation in a dialog box
		/// </summary>
		/// <param name="textResourceName">Name of the text/body resource.</param>
		/// <param name="captionResourceName">Name of the caption/title resource.</param>
		/// <param name="args">Arguments to format textResource with, aka string.Format</param>
		public static void ShowExclamation(string textResourceName, string captionResourceName, params object[] args)
		{
			string text = DialogText(textResourceName, args);
			string caption = DialogText(captionResourceName);

			MessageBox.Show(
				text,
				caption,
				MessageBoxButtons.OK,
				MessageBoxIcon.Exclamation);
		}
		#endregion
	}
}