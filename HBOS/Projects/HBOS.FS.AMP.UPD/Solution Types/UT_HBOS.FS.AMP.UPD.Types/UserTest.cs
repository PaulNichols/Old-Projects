using System;

using NUnit.Framework;
using HBOS.FS.AMP.UPD.Types.Users;

namespace UT_HBOS.FS.AMP.UPD.Users
{
    /// <summary>
    /// Summary description for FundTest.
    /// </summary>
    [TestFixture()]
    public class UserTest
    {
        public UserTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [Test]
        public void UserPropertyImportWeightings()
        {
            byte[] arrayOfBytes = new byte[] {1, 2};

            UserPermissions userPerms = new UserPermissions( false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false ,false, false, false, "HIFM");
            User user = new User ("Fred", "Mark", false, "MASDA", DateTime.Now, arrayOfBytes );
            user.Permissions = userPerms;

            Assert.IsFalse (user.IsDirty,"Dirty flag is true after initialisation");

            user.Permissions.ImportMarketIndices = true;
            Assert.IsTrue (user.Permissions.ImportMarketIndices,"Import Market Indices has not been set");
    
            Assert.IsTrue (user.IsDirty,"Dirty flag has been set");
            
        }

        [Test]
        public void UserPropertyUserName()
        {
            byte[] arrayOfBytes = new byte[] {1, 2};

            UserPermissions userPerms = new UserPermissions( false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false ,false, false, false, "HIFM");
            User user = new User ("Fred", "Mark", false, "MASDA", DateTime.Now, arrayOfBytes );
            user.Permissions = userPerms;
            
            Assert.IsFalse (user.IsDirty,"Dirty flag is true!");

            user.UserName = "Big ears" ;
            Assert.AreEqual ("Big ears", user.UserName,"user names has not been set");
    
            Assert.IsTrue (user.IsDirty,"Dirty flag has been set");
        }
    }
}
