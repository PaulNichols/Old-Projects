using System;
using System.Collections;
using System.Security;
using System.Security.Principal;
using HBOS.FS.AMP.UPD.Controllers;

namespace HBOS.FS.AMP.UPD.Security
{
	/// <summary>
	/// Allows code to check the group membership of an AMP user.
	/// </summary>
	public sealed class UPDPrincipal : IPrincipal
	{
		private UPDIdentity updIdentity;
		private Hashtable roles=new Hashtable();
		private string company;
		private DateTime m_NextCompanyValuationDateAndTime;
		private DateTime m_CurrentCompanyValuationDateAndTime;
		private DateTime m_PreviousCompanyValuationDateAndTime;


		/// <summary>
		/// Initializes a new instance of the UPDPrincipal class from an <see cref="UPDIdentity"/> object.
		/// </summary>
		/// <param name="identity">The <see cref="UPDIdentity"/> object from which to construct the new instance of <see cref="UPDPrincipal"/></param>
		/// <param name="companyId">The company for which permissions should be loaded.</param>
		/// <param name="currentCompanyValuationDateAndTime"></param>
		/// <param name="nextCompanyValuationDateAndTime"></param>
		/// <param name="previousCompanyValuationDateAndTime"></param>
		/// <example>
		/// The following example creates a new <see cref="UPDPrincipal"/> object from a new <see cref="UPDIdentity"/> object.
		/// <code lang="c#">
		/// AMPIdentity ampIdentity = new AMPIdentity();
		/// AMPPrincipal ampPrincipal = new AMPPrincipal(ampIdentity. "Leeds");
		/// </code>
		/// The following example can be used to set the principle of the application domain so any resultant
		/// threads will inherit the same principle.
		/// <code lang="c#">
		/// AppDomain.CurrentDomain.SetThreadPrincipal(new UPDPrincipal(new UPDIdentity(dbConnection)));
		/// </code>
		/// To attach the principle to the current thread use
		/// <code lang="c#">
		/// Thread.CurrentPrincipal = New UPDPrincipal(new UPDIdentity(dbConnection)))
		/// </code>
		/// Once the principle is set, a permission demand or link demand can evaluate the properties of the 
		/// security principal for the execution context.
		/// <code lang="c#">
		/// [SecurityPermission(SecurityAction.Demand, Role="Administrator")]
		/// public MyMethod()
		/// {
		///     ....
		/// }
		/// </code>
		/// </example>
		public UPDPrincipal(UPDIdentity identity, string companyId,
							DateTime currentCompanyValuationDateAndTime,
							DateTime nextCompanyValuationDateAndTime,
							DateTime previousCompanyValuationDateAndTime)
		{
			if (identity != null)
			{
				this.updIdentity = identity;
				if (updIdentity.IsAuthenticated)
				{
					lock (roles.SyncRoot)
					{
						// Load Permissions from the database
						try
						{
							this.company = companyId;
							this.m_CurrentCompanyValuationDateAndTime=currentCompanyValuationDateAndTime;
							this.m_NextCompanyValuationDateAndTime=nextCompanyValuationDateAndTime;
							this.m_PreviousCompanyValuationDateAndTime=previousCompanyValuationDateAndTime;
							this.roles = UserController.GrantedPermissionsAttributeCollection(this.updIdentity.SqlConnectionString, companyId,identity.Name);
						}
						catch
						{
							throw;
						}
					}
				}
				else
				{
					this.roles = null;
					throw new SecurityException("This User has no permissions for the specified Company.");
				}
			}
		}

		/// <summary>
		/// Gets the company code the current principle was retrieved for.
		/// </summary>
		/// <value>The company code the current principle was retrieved for.</value>
		public string CompanyCode
		{
			get { return this.company; }
		}

		#region IPrincipal Members

		/// <summary>
		/// Gets the identity of the current principal.
		/// </summary>
		/// <value>The <see cref="UPDIdentity"/> object of the current principal</value>
		public IIdentity Identity
		{
			get { return (this.updIdentity); }
		}

		/// <summary>
		/// Determines whether the current principal belongs to a specified AMP user group.
		/// </summary>
		/// <param name="role">The name of the AMP program role for which to check membership.</param>
		/// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
		public bool IsInRole(string role)
		{
			bool ReturnValue = false;
			if (null != role &&
				null != this.roles)
			{
				ReturnValue = roles.ContainsKey(role.ToLower());
			}
			return ReturnValue;
		}

		/// <summary>
		/// Gets the current company valuation date and time.
		/// </summary>
		/// <value></value>
		public DateTime CurrentCompanyValuationDateAndTime
		{
			get { return m_CurrentCompanyValuationDateAndTime; }
		}

		/// <summary>
		/// Gets the next company valuation date and time.
		/// </summary>
		/// <value></value>
		public DateTime NextCompanyValuationDateAndTime
		{
			get { return m_NextCompanyValuationDateAndTime; }
		}

		/// <summary>
		/// Gets the previous company valuation date and time.
		/// </summary>
		/// <value></value>
		public DateTime PreviousCompanyValuationDateAndTime
		{
			get { return m_PreviousCompanyValuationDateAndTime; }
		}

		#endregion
	}
}