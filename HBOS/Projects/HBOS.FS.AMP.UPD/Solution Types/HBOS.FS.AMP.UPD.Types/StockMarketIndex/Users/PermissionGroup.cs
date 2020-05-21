using System.Collections;

namespace HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// Summary description for PermissionGroup.
	/// </summary>
	public class PermissionGroup : Permission
	{
		#region Private Members

		private Permission[] permissions;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public override bool IsDirty
		{
			get { return base.IsDirty; }
			set
			{
				foreach (Permission permission in this.permissions)
				{
					permission.IsDirty = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Permission"/> at the specified index.
		/// </summary>
		/// <value></value>
		public override Permission this[int id]
		{
			get { return FindPermissionById(this, id); }
//			set
//			{
//				if (id >= permissions.Length)
//				{
//					throw new IndexOutOfRangeException();
//				}
//				else
//				{
//					permissions[id] = value;
//				}
//			}
		}

		private Permission FindPermissionById(Permission parentPermission, int id)
		{
			Permission returnPermission = null;

			if (parentPermission.PermissionId == id)
			{
				returnPermission = parentPermission;
			}
			else
			{
				if (parentPermission is PermissionGroup)
				{
					foreach (Permission permission in ((PermissionGroup) parentPermission).permissions)
					{
						returnPermission = FindPermissionById(permission, id);
						if (returnPermission != null)
						{
							break;
						}
					}
				}
			}
			return returnPermission;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="PermissionGroup"/> is granted.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if granted; otherwise, <c>false</c>.
		/// </value>
		public override bool Granted
		{
			get
			{
				bool allGranted = true;

				foreach (Permission permission in permissions)
				{
					allGranted = permission.Granted;
					if (!allGranted)
					{
						break;
					}
				}
				return allGranted;
			}
			set
			{
				foreach (Permission permission in permissions)
				{
					permission.Granted = value;
				}
			}
		}

		#endregion

		#region "Public Methods"

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public override IEnumerator GetEnumerator()
		{
			SortedList sortedByDisplayName = new SortedList();
			foreach (Permission p in permissions)
			{
				sortedByDisplayName.Add(p.DisplayName, p);
			}

			return sortedByDisplayName.GetValueList().GetEnumerator();
		}

		/// <summary>
		/// Adds the specified permission.
		/// </summary>
		/// <param name="permission">Permission.</param>
		public override void Add(Permission permission)
		{
			ArrayList tempPermissions = new ArrayList(permissions);
			tempPermissions.Add(permission);
			permissions = (Permission[]) tempPermissions.ToArray(typeof (Permission));
		}

		#endregion

		#region "Constructor(s)"

		/// <summary>
		/// Creates a new <see cref="Permission"/> instance.
		/// </summary>
		/// <param name="id">Permission Id.</param>
		/// <param name="displayName">The display name.</param>
		/// /// <param name="companyCode">Code of Company for which these permissions apply for the specified user</param>
		public PermissionGroup(string companyCode, int id, string displayName) : base(companyCode, id, displayName, false)
		{
			this.permissions = new Permission[0];
		}

		#endregion
	}
}