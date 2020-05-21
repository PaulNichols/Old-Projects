using System;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for ICustomInit.
	/// An interface used by UI controls which requires custom initialisation after the construction.
	/// </summary>
	public interface ICustomInit
	{
		/// <summary>
		/// Used by UI controls which requires custom initialisation after the construction.
		/// </summary>
		void CustomInitialization();
	}
}
