namespace HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// Summary description for UserPermission.
	/// </summary>
	public class UserPermission : Permission
	{
		#region "Private Members"

		private string m_uniqueName;

		#endregion

		#region "Public Properties"
		/// <summary>
		/// Gets the attribute name that refers to this <see cref="Permission"/>.
		/// </summary>
		/// <value></value>
		
		public override string UniqueName
		{
			get
			{
				return m_uniqueName;
			}
		}

		#endregion

		#region "Constructor(s)"

		/// <summary>
		/// Creates a new <see cref="Permission"/> instance.
		/// </summary>
		/// <param name="id">Permission Id.</param>
		/// <param name="displayName">The display name.</param>
		/// <param name="granted">Whether or not the permission is granted.</param>
		/// <param name="uniqueName">Name of the attribute for use in attribute based security.</param>
		/// <param name="companyCode">Code of Company for which these permissions apply for the specified user</param>
		public UserPermission(string companyCode,int id, string displayName, bool granted, string uniqueName) : base(companyCode,id, displayName, granted)
		{
			this.m_uniqueName = uniqueName;
		}

		#endregion
	}
}