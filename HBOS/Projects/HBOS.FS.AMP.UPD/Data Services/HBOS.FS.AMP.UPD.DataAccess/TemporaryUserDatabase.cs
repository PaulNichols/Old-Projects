using System;
using HBOS.FS.AMP.UPD.Types.Users;

namespace HBOS.FS.AMP.UPD.DataAccess
{
    /// <summary>
    /// Temporary class to generate dummy data
    /// </summary>
    public class TemporaryUserDatabase
    {
        UserCollection m_TemporaryUsers;
        private static TemporaryUserDatabase m_Instance;

        /// <summary>
        /// Allows internal calling whilst not exposing to the client
        /// </summary>
        private TemporaryUserDatabase()
        {
            m_TemporaryUsers = new UserCollection();
            User currentUser;

//            for (int i=0; i<10; i++)
//            {
//                currentUser = new User (false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,false, false,"Temp user " + i,12,2  );
//                m_TemporaryUsers.Add(currentUser);
//            }
        }

        /// <summary>
        /// Make sure only one instance of the object will be instatiated 
        /// </summary>
        /// <returns></returns>
        public static TemporaryUserDatabase GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new TemporaryUserDatabase();
            }
            return m_Instance;
        }

        /// <summary>
        /// Get a collection of users
        /// </summary>
        /// <returns></returns>
        public UserCollection GetUsers()
        {
            return m_TemporaryUsers;
        }

        /// <summary>
        /// Persist the users back to the DB
        /// </summary>
        /// <param name="users"></param>
        public void SaveUsers(UserCollection users)
        {
            m_TemporaryUsers = users;
        }
    }
}
