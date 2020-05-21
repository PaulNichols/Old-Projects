using System;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Provides the description of an allowable action in a status view. Provides abstraction of
	/// the actions from the GUI and Controller. (Dependancy Inversion pattern).
	/// </summary>
	public class StatusAction : UPDAction
	{
		/// <summary>
		/// Creates a new <see cref="StatusAction"/> instance.
		/// </summary>
		public StatusAction()
		{
		}

		/// <summary>
		/// Raised when the action is executed.
		/// Connected to the controller to receive messages from the GUI.
		/// </summary>
		public event EventHandler Executed;

		/// <summary>
		/// Execute the action. Raises the executed event. 
		/// This is called be the GUI to send the event to the controller
		/// </summary>
		/// <param name="e">Event arguments.</param>
		public void Execute(EventArgs e)
		{
			if (Executed != null)
			{
				Executed(this, e);
			}
		}

		/// <summary>
		/// The event to be invoked in order to determine command visibility
		/// </summary>
		public event RequestVisibilityHandler RequestVisibility;

		/// <summary>
		/// Overloaded method to be invoked by the UI following any change in UI, in order to 
		/// determine whether a command (button) is enabled or visible.
		/// This method to be used where this no concept of currently edited object.
		/// </summary>
		/// <param name="isEnabled"></param>
		/// <param name="isVisible"></param>
		public void RetrieveCommandVisibility (out bool isEnabled, out bool isVisible)
		{
			//set the defaults
			isEnabled = true;
			isVisible = true;
			
			//get the button visibility which may be dependent on currently edited object state (if there is a currently edited object)
			if (RequestVisibility != null)
			{
				RequestVisibilityArgs e = new RequestVisibilityArgs();
				RequestVisibility (this, e);
				isEnabled = e.IsEnabled;
				isVisible = e.IsVisible;
			}
		}

	}
}