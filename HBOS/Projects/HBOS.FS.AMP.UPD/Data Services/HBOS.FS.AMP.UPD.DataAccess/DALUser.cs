using System;

using HBOS.FS.AMP.UPD.Types.Users;

namespace HBOS.FS.AMP.UPD.DataAccess
{
    /// <summary>
    /// Data Access Layer for fund information
    /// </summary>
    public class DALUser
    {
        /// <summary>
        /// Main constructor
        /// </summary>
        public DALUser()
        {
            //
            // No constructor logic
            //
        }

        /// <summary>
        /// Gets a collection of users filtered by the specified compny id
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public  UserCollection GetUsers(int companyID)
        {
            // Get data from our dummy database object until the SQL DB is built
            return  TemporaryUserDatabase.GetInstance().GetUsers();
        }

        /// <summary>
        /// Persist data back to the DB through the user collection
        /// </summary>
        /// <param name="users"></param>
        public void SaveUsers(UserCollection  users)
        {
            // Send collection back to temp DB object
            TemporaryUserDatabase.GetInstance().SaveUsers(users);
        }
    }
}
