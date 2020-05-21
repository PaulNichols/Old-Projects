using System;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Event args used to determine command visibility
	/// </summary>
	public class RequestVisibilityArgs : EventArgs
	{
		private bool m_isEnabled = false;
		private bool m_isVisible = false;

		/// <summary>
		/// Is the button enabled or disabled?
		/// </summary>
		public bool IsEnabled
		{
			get {return m_isEnabled;}
			set {m_isEnabled = value;}
		}

		/// <summary>
		/// Is the button visible?
		/// </summary>
		public bool IsVisible
		{
			get {return m_isVisible;}
			set {m_isVisible = value;}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public RequestVisibilityArgs ()
		{
		}

	}

	/// <summary>
	/// The event signature for requesting command visibility
	/// </summary>
	public delegate void RequestVisibilityHandler (object sender, RequestVisibilityArgs e);

	/// <summary>
	/// The base UPDAction, must be subclassed
	/// </summary>
	public abstract class UPDAction
	{

		private string m_text;
	
		/// <summary>
		/// Gets or sets the text that is displayed on controls associated with the action.
		/// </summary>
		/// <value></value>
		public string Text
		{
			get {return m_text;}
			set {m_text = value;}
		}

		/// <summary>
		/// Default ctor
		/// </summary>
		public UPDAction()
		{
		}
	}
}
