using System.Security.Permissions;
using System.Security.Principal;
using HBOS.FS.AMP.UPD.Controllers;

namespace HBOS.FS.AMP.UPD.Security
{
	
	/// <summary>
	/// Represents a user context for the AMP UPD application.
	/// </summary>
	/// <remarks>
	/// An identity class which represents the user on whose behalf the code is running.
	/// The identity class only checks the database for a user's ability to access the UPD application,
	/// it does not check if a user can access information for a particular company, this role is filled
	/// by the <see cref="UPDPrincipal"/> class.
	/// </remarks>
	public sealed class UPDIdentity : IIdentity
	{
		private string username;
		private bool authenticated;
		private string connectionString;

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="UPDIdentity"/> class.
		/// </summary>
		/// <param name="connectionString">A SQL connection string to the authentication database.</param>
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal=true)]
		public UPDIdentity(string connectionString)
		{
			// Take the windows identity as a starting point, so we can get the current logged in name
			IIdentity windowsIdentity = WindowsIdentity.GetCurrent();
			if (windowsIdentity.IsAuthenticated)
			{
				// Check if the current user is in the SQL authenication database
				bool isValidUser = false;
				isValidUser = UserController.UserIsValid(connectionString);

				if (isValidUser)
				{
					this.username = windowsIdentity.Name.ToString();
					this.authenticated = true;
					this.connectionString = connectionString;
				}
			}
		}

		#endregion

		#region Internal methods
		/// <summary>
		/// Gets the SQL Connection String used to authenicate a user against the security table.
		/// </summary>
		/// <value>
		/// The SQL Connection String used to authenicate a user against the security table.
		/// </value>
		internal string SqlConnectionString
		{
			get { return (this.connectionString); }
		}

		#endregion

		#region IIdentity Members

		/// <summary>
		/// Gets a value indicating whether the user has been authenticated.
		/// </summary>
		/// <value>true if the user was has been authenticated; otherwise, false.</value>
		public bool IsAuthenticated
		{
			get { return (this.authenticated); }
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
			get { return "AMP Auth"; }
		}

		#endregion
	}
}