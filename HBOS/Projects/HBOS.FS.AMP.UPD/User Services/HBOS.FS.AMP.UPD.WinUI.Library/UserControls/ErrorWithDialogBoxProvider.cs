using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for ErrorWithDialogBoxProvider.
	/// </summary>
	public class ErrorWithDialogBoxProvider : ErrorProvider
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components ;

		/// <summary>
		/// Creates a new <see cref="ErrorWithDialogBoxProvider"/> instance.
		/// </summary>
		/// <param name="container">Container.</param>
		public ErrorWithDialogBoxProvider(IContainer container)
		{
			//
			// Required for Windows.Forms Class Composition Designer support
			//
			T.E();
			container.Add(this);
			InitializeComponent();
			T.E();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Creates a new <see cref="ErrorWithDialogBoxProvider"/> instance.
		/// </summary>
		public ErrorWithDialogBoxProvider()
		{
			T.E();
			InitializeComponent();
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

		/// <summary>
		/// Sets the error message for the specifed control.
		/// </summary>
		/// <param name="control">Control to show error for.</param>
		/// <param name="value">Message to display.</param>
		new public void SetError(Control control, string value)
		{
			T.E();

			string oldError = GetError(control);
			messages.Remove(oldError);

			messages.Add(value);
			base.SetError(control, value);
			T.X();
		}

		private StringCollection messages = new StringCollection();

		/// <summary>
		/// Shows a dialog box displaying all the validation errors
		/// </summary>
		/// <param name="messagePrefix">A prefix message shown in the body before the main message.</param>
		/// <param name="caption">Caption for the dialog box</param>
		public void ShowDialog(string messagePrefix, string caption)
		{
			T.E();
			StringBuilder builder = new StringBuilder();

			if (messagePrefix != string.Empty)
			{
				builder.Append(messagePrefix);
				builder.Append("\n\n");
			}

			foreach (string s in messages)
			{
				builder.Append("     - ");
				builder.Append(s);
				builder.Append("\n");
			}

			MessageBox.Show(builder.ToString(), caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			T.X();
		}

		/// <summary>
		/// Shows a dialog box displaying all the validation errors
		/// </summary>
		public void ShowDialog()
		{
			T.E();
			ShowDialog(string.Empty);
			T.X();
		}


		/// <summary>
		/// Shows a dialog box displaying all the validation errors
		/// </summary>
		/// <param name="messagePrefix">A prefix message shown in the body before the main message.</param>
		public void ShowDialog(string messagePrefix)
		{
			T.E();
			ShowDialog(messagePrefix, "Validation Errors Occurred");
			T.X();
		}

		/// <summary>
		/// Clear messages from the cache
		/// </summary>
		public void Clear()
		{
			messages.Clear();	
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		#endregion
	}
}