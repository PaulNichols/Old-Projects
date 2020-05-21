using System;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// enum to indicate whether user pressed yes no or cancel (or not known)
	/// </summary>
	public enum YesNoCancelAction 
	{
				
		/// <summary>
		/// we don't know what the user clicked
		/// </summary>
		unknown, 
		/// <summary>
		/// user clicked Yes
		/// </summary>
		yes, 
		/// <summary>
		/// User clicked No
		/// </summary>
		no, 
		/// <summary>
		/// User clicked cancel
		/// </summary>
		cancel}

	/// <summary>
	/// Event argument to indicate whether the user pressed yes no or cancel
	/// </summary>
	public class YesNoCancelEventArgs : EventArgs
	{

		#region Member Variables

		YesNoCancelAction m_userAction = YesNoCancelAction.unknown;

		#endregion

		#region Properties
		/// <summary>
		/// sets or returns what the user clicked - yes, no or cancel (or unknown)
		/// </summary>
		public YesNoCancelAction UserAction
		{
			get 
			{
				return m_userAction;
			}
			set
			{
				m_userAction = value;
			}
		}
		#endregion

	}
}
