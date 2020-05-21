using HBOS.FS.AMP.Security;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// the form with an exposed property to indicate company changed
	/// </summary>
	public class UPDRoleCheckedForm : RoleCheckedForm
	{
		#region Fields

		/// <summary>
		/// indicates the UI needs to be refreshed
		/// </summary>
		protected bool changeCompany ;

		#endregion

		#region Public Properties

		/// <summary>
		/// Is the form close due to a changing company event.
		/// </summary>
		public bool ChangedCompany
		{
			get { return this.changeCompany; }
			set { this.changeCompany = value; }
		}

		#endregion
	}

}