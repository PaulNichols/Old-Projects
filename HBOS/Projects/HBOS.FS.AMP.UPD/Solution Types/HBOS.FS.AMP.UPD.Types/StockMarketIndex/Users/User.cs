using System;

using HBOS.FS.AMP.UPD.Types;

namespace HBOS.FS.AMP.UPD.Types.Users
{

	/// <summary>
	/// This is the user object used for static data export
	/// </summary>
	public class ExportStaticDataUserDetails : User
	{
		private readonly string displayName;
		private readonly bool granted;

		/// <summary>
		/// Gets a value indicating whether this <see cref="ExportStaticDataUserDetails"/> is granted.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if granted; otherwise, <c>false</c>.
		/// </value>
		public bool Granted
		{
			get { return granted; }
		}

		/// <summary>
		/// Gets the name of the display.
		/// </summary>
		/// <value></value>
		public string DisplayName
		{
			get { return displayName; }
		}

		/// <summary>
		/// Creates a new <see cref="ExportStaticDataUserDetails"/> instance.
		/// </summary>
		/// <param name="loginId">Login id.</param>
		/// <param name="userName">Name of the user.</param>
		/// <param name="displayName">Permission display name.</param>
		/// <param name="granted">Is the permission Granted to the User.</param>
		public ExportStaticDataUserDetails(string loginId,string userName,string displayName,bool granted):base(loginId,userName)
		{
			this.displayName = displayName;
			this.granted = granted;
		}
	}

    /// <summary>
    /// This is the user object
    /// </summary>
    public class User : EntityBase//, ICloneable
    {
        #region Object Specific Private Variables


        private string m_userName = string.Empty; 
        private string m_logOnID = string.Empty; 
        private bool m_isDeletedinDB = false;
        private string m_lastChangedBy = string.Empty;
        private DateTime m_lastChangedDate = DateTime.Now;

        private Permission m_permission;

    	#endregion
        
        #region Constructors

        /// <summary>
        /// This constructor is to be used when the object is populated fromm the database
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="UserName"></param>
        /// <param name="DeletedInDB"></param>
        /// <param name="LastChangedBy"></param>
        /// <param name="LastChangedDate"></param>
        /// <param name="TimeStamp"></param>
        public User(string LoginID, string UserName, bool DeletedInDB, string LastChangedBy, DateTime LastChangedDate, byte[] TimeStamp)
        {
            this.m_userName = UserName;
            this.m_logOnID = LoginID;
            this.m_isDeletedinDB = DeletedInDB;
            this.m_lastChangedBy = LastChangedBy;
            this.m_lastChangedDate = LastChangedDate;
            this.m_timestamp = TimeStamp;
			this.IsNew = false;
        }
        /// <summary>
        /// This constructor is to be used when the client creates new users
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="UserName"></param>
        public User(string LoginID, string UserName)
        {
			ConstructorForNewUser(null);
			this.m_userName = UserName;
            this.m_logOnID = LoginID;
        }     

        /// <summary>
        /// This constructor is to be used when the client creates new users
        /// </summary>
        public User()
        {
        	ConstructorForNewUser(null);
        }

		/// <summary>
		/// This constructor is to be used when the client creates new users
		/// </summary>
		public User(string companyCode)
		{
			ConstructorForNewUser(companyCode);
		}

		private void ConstructorForNewUser(string companyCode)
		{
			this.m_timestamp = new byte [1];
			this.m_isNew = true;
			//this.m_permission = new Permission();
		}

		 #endregion

        #region Object Specific Properties 
       
        /// <summary>
        /// The users name
        /// </summary>
        public string UserName
        {
            get { return this.m_userName;  }
            set 
            {
                if (this.m_userName != value)
                {
                    this.m_isDirty = true; 
                    this.m_userName = value; 
                }
            }
        }

        /// <summary>
        /// The login ID
        /// </summary>
        public string LogOnID
        {
            get { return this.m_logOnID;  }
            set 
            {
                if (this.m_logOnID != value)
                {
                    this.m_isDirty = true; 
                    this.m_logOnID = value; 
                }
            }
        }

        /// <summary>
        /// The deleted flag from the database (read only)
        /// </summary>
        public bool IsDeletedInDB
        {
            get { return this.m_isDeletedinDB; }
        }

        /// <summary>
        /// The login id of the person who last changed this record (read only)
        /// </summary>
        public string LastChangedBy
        {
            get { return this.m_lastChangedBy; }
        }

        /// <summary>
        /// The date this record was last changed (read only)
        /// </summary>
        public DateTime LastChangedDate
        {
            get { return this.m_lastChangedDate; }
        }

        /// <summary>
        /// This object holds the users actual permissions
        /// </summary>
        public Permission Permissions
        {
            get { return this.m_permission; }
            set { m_permission = value; }
        }

//        /// <summary>
//        /// Returns the isdirty flag from the permissions object
//        /// </summary>
//        public bool PermissionsDirty
//        {
//            get { return this.m_permissions.IsDirty ; }
//        }
//
//        /// <summary>
//        /// Returns the isdeleted flag from the permissions object
//        /// </summary>
//        public bool PermissionsDeleted
//        {
//            get { return this.m_permissions.IsDeleted ; }
//        }
//
//        /// <summary>
//        /// Returns the isnew flag from the permissions object
//        /// </summary>
//        public bool PermissionsNew
//        {
//            get { return this.m_permissions.IsNew ; }
//        }

        #endregion

		/// <summary>
		/// Gets the all permissions including those not granted for this user.
		/// </summary>
		/// <returns></returns>
		public Permission GetAllPermissions()
		{
			return null;
		}


        #region ICloneable Members
//
//        /// <summary>
//        /// Clones this instance.
//        /// </summary>
//        /// <returns>Deep copy of the user permissions data</returns>
//        public object Clone()
//        {
//            User clonedUser = new User();
//
//            clonedUser.m_isDeleted = this.m_isDeleted;
//            clonedUser.m_isDeletedinDB = this.m_isDeletedinDB;
//            clonedUser.m_isDirty = this.m_isDirty;
//            clonedUser.m_isNew = this.m_isNew;
//            clonedUser.m_lastChangedBy = this.m_lastChangedBy;
//            clonedUser.m_lastChangedDate = this.m_lastChangedDate;
//            clonedUser.m_logOnID = this.m_logOnID;
//            clonedUser.m_timestamp = this.m_timestamp;
//            clonedUser.m_userName = this.m_userName;
//            
//            UserPermissions clonedPermissions = (UserPermissions)this.Permissions.Clone();
//            clonedUser.Permissions = clonedPermissions;
//
//            return clonedUser;
//        }

        #endregion
    }

}
