using System;
using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// Summary description for Permission.
	/// </summary>
	public abstract class Permission : EntityBase, IEnumerable
	{
		#region "Private Members"

		private string m_displayName;
		private int m_PermissionId;
		private string m_CompanyCode;
		private bool m_granted;

		#endregion

		#region "Public Properties"

		/// <summary>
		/// Gets the company code.
		/// </summary>
		/// <value></value>
		public string CompanyCode
		{
			get { return m_CompanyCode; }
		}

		/// <summary>
		/// Gets the name of this <see cref="Permission"/> .
		/// </summary>
		/// <value></value>
		public string DisplayName
		{
			get { return m_displayName; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="UserPermission"/> is granted.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if granted; otherwise, <c>false</c>.
		/// </value>
		public virtual bool Granted
		{
			get { return m_granted; }
			set
			{
				m_isDirty = true;
				m_granted = value;
			}
		}

		
		/// <summary>
		/// Gets the attribute name that refers to this <see cref="Permission"/>.
		/// </summary>
		/// <value></value>
		public virtual string UniqueName
		{
			get { throw new NotImplementedException(); }
		}

		/// <summary>
		/// Gets the unique Id of this <see cref="Permission"/> .
		/// </summary>
		/// <value></value>
		public int PermissionId
		{
			get { return m_PermissionId; }
		}

		/// <summary>
		/// Gets or sets the <see cref="Permission"/> at the specified index.
		/// </summary>
		/// <value></value>
		public virtual Permission this[int index]
		{
			get { throw new NotImplementedException(); }
			//set { throw new NotImplementedException(); }
		}

		#endregion

		#region "Constructor(s)"
		/// <summary>
		/// Creates a new <see cref="Permission"/> instance.
		/// </summary>
		/// <param name="id">Permission Id.</param>
		/// <param name="displayName">The display name.</param>
		/// <param name="granted">Whether or not the permission is granted.</param>
/// <param name="companyCode">Code of Company for which these permissions apply for the specified user</param>
		/// 
		public Permission(string companyCode,int id, string displayName, bool granted)
		{
			this.m_displayName = displayName;
			this.m_granted = granted;
			this.m_PermissionId = id;
			this.m_CompanyCode=companyCode;
		}

		#endregion

		#region "Public Methods"

		/// <summary>
		/// Shows the DisplayName.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.DisplayName;
		}


		/// <summary>
		/// Adds the specified permission.
		/// </summary>
		/// <param name="permission">Permission.</param>
		public virtual void Add(Permission permission)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerator GetEnumerator()
		{
			return new Permission[0].GetEnumerator();
		}

		#endregion

		
	}
}