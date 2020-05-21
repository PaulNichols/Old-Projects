using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for ButtonTextBox.
	/// </summary>
	public class ButtonTextBox : UserControl
	{
		private class TestTextBox : TextBox
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="keyData"></param>
			/// <returns></returns>
			protected override bool IsInputKey(Keys keyData)
			{
				bool ret = true;

				switch (keyData)
				{
					case Keys.Left:
						break;
					case Keys.Right:
						break;
					case Keys.Up:
						break;
					case Keys.Down:
						break;
					case Keys.Tab:
						break;
					default:
						ret = base.IsInputKey(keyData);
						break;
				}

				return ret;
			}
		}

		#region Locals

		private TestTextBox textBox;
		private Button actionButton;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region Event declaration

		/// <summary>
		/// Delegate for the Button clicked event
		/// </summary>
		public delegate void ButtonClickedHandler(object sender, EventArgs e);

		/// <summary>
		/// Delegate for the text box validating event
		/// </summary>
		public delegate void TextBoxValidatingHandler(object sender, CancelEventArgs e);

		/// <summary>
		/// Event signifying the button has been clicked which is associated with our delegate ButtonClickedHandler()
		/// </summary>
		[Category("Action")]
		[Description("Fire when the button is clicked.")]
		public event ButtonClickedHandler ButtonClicked;

		/// <summary>
		/// Event signifying the text box is validating which is associated with our delegate TextBoxValidatingHandler()
		/// </summary>
		[Category("Action")]
		[Description("Fire when the text box is validating.")]
		public event TextBoxValidatingHandler TextBoxValidating;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public ButtonTextBox()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			actionButton.Cursor = Cursors.Arrow;
			textBox.Cursor = Cursors.Default;
		}

		#endregion

		#region Protected methods

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

		/// <summary>
		/// Add a protected method to use in child classes instead of
		/// adding event handlers.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected virtual void OnButtonClicked(EventArgs e)
		{
			// If an event has no subscribers registered, it will evaluate to null
			// This test checks that the value is not null, ensuring that there 
			// are subscribers before calling the event itself.
			if (ButtonClicked != null)
			{
				ButtonClicked(this, e); // Notify subscribers
			}
		}

		/// <summary>
		/// Add a protected method to use in child classes instead of
		/// adding event handlers.
		/// </summary>
		/// <param name="e">Provides data for a cancelable event</param>
		protected virtual void OnTextBoxValidating(CancelEventArgs e)
		{
			// If an event has no subscribers registered, it will evaluate to null
			// This test checks that the value is not null, ensuring that there 
			// are subscribers before calling the event itself.
			if (TextBoxValidating != null)
			{
				TextBoxValidating(this, e); //Notify subscribers
			}
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox = new TestTextBox();
			this.actionButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.Location = new System.Drawing.Point(0, 0);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(156, 13);
			this.textBox.TabIndex = 5;
			this.textBox.Text = "";
			this.textBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
			this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
			// 
			// actionButton
			// 
			this.actionButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.actionButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.actionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.actionButton.Location = new System.Drawing.Point(160, 0);
			this.actionButton.Name = "actionButton";
			this.actionButton.Size = new System.Drawing.Size(15, 15);
			this.actionButton.TabIndex = 7;
			this.actionButton.Text = "...";
			this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
			// 
			// ButtonTextBox
			// 
			this.Controls.Add(this.actionButton);
			this.Controls.Add(this.textBox);
			this.Name = "ButtonTextBox";
			this.Size = new System.Drawing.Size(176, 23);
			this.ResumeLayout(false);

		}

		#endregion

		#region Event handlers

		// Handler for action button
		private void actionButton_Click(object sender, EventArgs e)
		{
			OnButtonClicked(e);
		}

		// Highlight all text in the text box
		private void textBox_Enter(object sender, EventArgs e)
		{
			this.textBox.SelectAll();
		}

		private void textBox_Validating(object sender, CancelEventArgs e)
		{
			OnTextBoxValidating(e);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read / write property for the text box
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the value in the text box.")]
		public override string Text
		{
			get { return this.textBox.Text; }

			set { this.textBox.Text = value; }
		}

		#endregion
	}
}