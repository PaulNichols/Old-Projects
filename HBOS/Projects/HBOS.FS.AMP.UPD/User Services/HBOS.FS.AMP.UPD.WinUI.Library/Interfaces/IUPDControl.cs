namespace HBOS.FS.AMP.UPD.WinUI.Interfaces
{
	/// <summary>
	/// An interface to be implemented by controls to be loaded by the main UPD exe
	/// </summary>
	public interface IUPDControl
	{
		/// <summary>
		///This method is invoked when the user has selected a different menu item which will load up a different control.
		///By returning true from this, the menu change is allowed to occur, return false and the menu change will not be allowed.
		/// </summary>
		/// <returns></returns>
		bool AllowMenuChange();
	}
}