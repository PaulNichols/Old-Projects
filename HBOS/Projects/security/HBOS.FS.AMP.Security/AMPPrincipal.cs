using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Permissions;
using System.Security.Principal;

namespace HBOS.FS.AMP.Security
{
	/// <summary>
	/// Allows code to check the group membership of an AMP user.
	/// </summary>
	public sealed class AMPPrincipal : IPrincipal
	{
		private AMPIdentity ampIdentity = null;
        private string[] roles  = null;
        
        /// <summary>
        /// Initializes a new instance of the AMPPrincipal class from an <see cref="AMPIdentity"/> object.
        /// </summary>
        /// <param name="ampIdentity">The <see cref="AMPIdentity"/> object from which to construct the new instance of <see cref="AMPPrincipal"/></param>
        /// <example>
        /// The following example creates a new <see cref="AMPPrincipal"/> object from a new <see cref="AMPIdentity"/> object.
        /// <code lang="c#">
        /// AMPIdentity ampIdentity = new AMPIdentity();
        /// AMPPrincipal ampPrincipal = new AMPPrincipal(ampIdentity);
        /// </code>
        /// The following example can be used to set the principle of the application domain so any resultant
        /// threads will inherit the same principle.
        /// <code lang="c#">
        /// AppDomain.CurrentDomain.SetThreadPrincipal(new AMPPrincipal(new AMPIdentity(dbConnection)));
        /// </code>
        /// Once the principle is set, a permission demand or link demand can evaluate the properties of the 
        /// security principal for the execution context.
        /// <code lang="c#">
        /// [SecurityPermission(SecurityAction.Demand, Role="UDP Administrators")]
        /// </code>
        /// </example>
        public AMPPrincipal(AMPIdentity ampIdentity)
		{
            this.ampIdentity = ampIdentity;
            if (ampIdentity.IsAuthenticated)
            {
                // Load Roles from the database
                try
                {
                    using (SqlConnection connection = new SqlConnection(this.ampIdentity.SqlConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "usp_GetRolesForUser";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@sUserId", SqlDbType.VarChar, 50).Value = this.ampIdentity.Name;                            
                            using (DataSet rolesDS = new DataSet())
                            {                            
                                rolesDS.Locale = System.Globalization.CultureInfo.InvariantCulture;
                                connection.Open();                
                                using (SqlDataAdapter roleDA = new SqlDataAdapter(command))
                                {
                                    // Get all the roles for the current user
                                    roleDA.Fill(rolesDS);
                                }
                            
                                // Check that we have some roles back again
                                if (rolesDS.Tables.Count == 1 &&
                                    rolesDS.Tables[0].Rows.Count > 0)
                                {
                                    // Create a strnig array to hold the roles, then fill it from the dataset.
                                    this.roles = new string[rolesDS.Tables[0].Rows.Count];
                                    int iArrayCount = 0;
                                    foreach (DataRow dr in rolesDS.Tables[0].Rows)
                                    {
                                        this.roles[iArrayCount++] = dr[0].ToString();
                                    }
                                }
                                else
                                {
                                    this.roles = null;
                                }
                            }
                            connection.Close();
                        }
                    }                
                }
                catch (SqlException e)
                {
                    throw (e);
                }                
            }
        }
        #region IPrincipal Members

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <value>The <see cref="AMPIdentity"/> object of the current principal</value>
        public IIdentity Identity
        {
            get
            {
                return (this.ampIdentity);
            }
        }

        /// <summary>
        /// Determines whether the current principal belongs to a specified AMP user group.
        /// </summary>
        /// <param name="role">The name of the AMP program role for which to check membership.</param>
        /// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
        public bool IsInRole(string role)
        {
            if (null != role &&
                null != this.roles)
            {
                for (int i = 0; i < this.roles.Length; i++)
                {
                    if ((this.roles[i] != null) && 
                        (string.Compare(this.roles[i], role, true, CultureInfo.InvariantCulture) == 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
