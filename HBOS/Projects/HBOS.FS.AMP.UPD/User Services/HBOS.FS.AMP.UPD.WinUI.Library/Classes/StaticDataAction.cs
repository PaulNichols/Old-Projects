using System;
using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{

	/// <summary>
	/// Event args used to determine command visibility
	/// </summary>
	public class RequestStaticDataVisibilityArgs : RequestVisibilityArgs
	{
		private object m_currentlyEditedObject = null;

		/// <summary>
		/// The currently edited object - used for static data screens not status screens, in order
		/// to help determine whether a button should be visible or enables (in most cases by interrogating whether changed or not)
		/// </summary>
		public object CurrentlyEditedObject
		{
			get {return m_currentlyEditedObject;}
			set {m_currentlyEditedObject = value;}
		}

		/// <summary>
		/// Constructor requires currently edited object
		/// </summary>
		/// <param name="currentlyEditedObject"></param>
		public RequestStaticDataVisibilityArgs (object currentlyEditedObject)
		{
			m_currentlyEditedObject = currentlyEditedObject;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public RequestStaticDataVisibilityArgs ()
		{
		}
	}

	/// <summary>
	/// The event signature for requesting command visibility
	/// </summary>
	public delegate void RequestStaticDataVisibilityHandler (object sender, RequestVisibilityArgs e);


	/// <summary>
	/// Provides the description of an allowable action in static data. Provides abstraction of
	/// the actions from the GUI and Controller.
	/// </summary>
	public class StaticDataAction : UPDAction
	{
		/// <summary>
		/// Creates a new <see cref="StaticDataAction"/> instance.
		/// </summary>
		public StaticDataAction()
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
		public event RequestStaticDataVisibilityHandler RequestVisibility;

		/// <summary>
		/// The method to be invoked by the UI following any change in UI, in order to 
		/// determine whether a command (button) is enabled or visible
		/// </summary>
		/// <param name="currentlyEditedObject"></param>
		/// <param name="isEnabled"></param>
		/// <param name="isVisible"></param>
		public void RetrieveCommandVisibility (object currentlyEditedObject, out bool isEnabled, out bool isVisible)
		{
			//set the defaults
			isEnabled = true;
			isVisible = true;
			
			//get the button visibility which may be dependent on currently edited object state (if there is a currently edited object)
			if (RequestVisibility != null)
			{
				RequestStaticDataVisibilityArgs e = new RequestStaticDataVisibilityArgs(currentlyEditedObject);
				RequestVisibility (this, e);
				isEnabled = e.IsEnabled;
				isVisible = e.IsVisible;
			}
		}

	}
}