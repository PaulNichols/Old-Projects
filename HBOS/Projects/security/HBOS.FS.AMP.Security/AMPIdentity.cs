using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Security.Principal;

namespace HBOS.FS.AMP.Security
{
	/// <summary>
	/// Represents a user context for the AMP programs.
	/// </summary>
	/// <remarks>
	/// An identity object represents the user on whose behalf the code is running.
    /// </remarks>
	public sealed class AMPIdentity : IIdentity
	{
        private string username;
        private bool authenticated = false;
        private DateTime authenticationTimeStamp = new DateTime();
        private string connectionString;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AMPIdentity"/> class.
        /// </summary>
        /// <param name="SqlConnectionString">A SQL connection string to the authentication database.</param>
        [SecurityPermission(SecurityAction.Demand, ControlPrincipal=true)]
        public AMPIdentity(string SqlConnectionString)
		{
            // Take the windows identity as a starting point, so we can get the current logged in name
            IIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity.IsAuthenticated)
            {
                // Check if the current user is in the SQL authenication database
                try
                {
                    bool isValidUser = false;
                    using (SqlConnection connection = new SqlConnection(SqlConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "usp_IsUserIdValid";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@sUserId", SqlDbType.VarChar, 50).Value = windowsIdentity.Name.ToString();
                            connection.Open();                
                            isValidUser = Convert.ToBoolean(command.ExecuteScalar(),
                                System.Globalization.CultureInfo.InvariantCulture);
                            connection.Close();
                        }
                    }
                
                    if (isValidUser)
                    {                
                        this.username = windowsIdentity.Name.ToString();
                        this.authenticated = true;
                        this.connectionString = SqlConnectionString;
                    }
                }
                catch (SqlException)
                {
                }                
            }
        }

        /// <summary>
        /// Gets the SQL Connection String used to authenicate a user against the security table.
        /// </summary>
        /// <value>
        /// The SQL Connection String used to authenicate a user against the security table.
        /// </value>
        internal string SqlConnectionString
        {
            get
            {
                return (this.connectionString);
            }
        }


        #region IIdentity Members

        /// <summary>
        /// Gets a value indicating whether the user has been authenticated.
        /// </summary>
        /// <value>true if the user was has been authenticated; otherwise, false.</value>
        public bool IsAuthenticated
        {
            get
            {
                return (this.authenticated);
            }
        }

        /// <summary>
        /// Gets the user's name
        /// </summary>
        /// <value>The name of the user on whose behalf the code is being run.</value>
        public string Name
        {
            get
            {
                if (this.IsAuthenticated)
                    return (this.username);
                else
                    return (string.Empty);
            }
        }

        /// <summary>
        /// Gets the type of authentication used to identify the user.
        /// </summary>
        /// <value>The type of authentication used to identify the user.</value>
        /// <remarks>For the AMP authorisation classes the AuthenicationType will be "AMP Auth".</remarks>
        public string AuthenticationType
        {
            get
            {                
                return "AMP Auth";
            }
        }

        #endregion
    }
}
