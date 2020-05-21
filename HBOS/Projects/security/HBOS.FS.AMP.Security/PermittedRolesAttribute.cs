using System;
using System.ComponentModel;
using System.Collections;

namespace HBOS.FS.AMP.Security
{
	/// <summary>
	/// Specifies roles that can access the specified control.
	/// </summary>
	/// <remarks>
	/// The PermittedRoles attribute supports the runtime checking of the thread's <see cref="System.Security.Principal"/>
	/// against the list of specified roles. Multiple permitted roles can be provided in a comma delimited list.
	/// By using <see cref="RoleCheckedForm"/> as the base class for your form classes any fields with the
	/// attribute applied will be checked and hidden if the current thread principal is not in any of the specified 
	/// roles.
	/// </remarks>
	/// <example>
	/// <code lang="c#">
    /// [PermittedRoles("Administrator")]
    /// private System.Windows.Forms.CheckBox isAdministrator;        
    /// [PermittedRoles("Administrator,Users")]
    /// private System.Windows.Forms.CheckBox isUser;
	/// </code>
	/// </example>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class PermittedRolesAttribute: System.Attribute, IEnumerable 
    {
        private string[] allowedRoles;
		
        /// <summary>
        /// Initializes an instance of the <see cref="PermittedRolesAttribute"/> class with a 
        /// string value containing a comma seperated list of allow roles..
        /// </summary>
        /// <param name="AllowedRoles"></param>
        public PermittedRolesAttribute(string AllowedRoles)
        {
            this.allowedRoles = AllowedRoles.Split(',');
        }

        /// <summary>
        /// Gets the roles that this attribute will allow.
        /// </summary>
        /// <returns>The roles that this attribute will allow.</returns>
        public string[] GetRoles()
        {
            return this.allowedRoles;
        }

        /// <summary>
        /// Gets a copy of the roles that this attribute will allow.
        /// </summary>
        /// <returns>A copy roles that this attribute will allow.</returns>
        /// <remarks>As this property returns a copy, use <see cref="GetRoles"/> or enumerate over 
        /// the attribute in perference to using this property.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string[] AllowedRoles
        {
            get 
            {
                return this.allowedRoles;
            }
        }

        #region IEnumerable Members
        /// <summary>
        /// Returns an IEnumerator for the rermitted roles.
        /// </summary>
        /// <returns>an IEnumerator for the rermitted roles</returns>
        public IEnumerator GetEnumerator()
        {
            return new RolesEnumerator(this);
        }
        #endregion

        /// <summary>
        /// Enumerator class for the roles array in <see cref="PermittedRolesAttribute"/>
        /// </summary>
        private class RolesEnumerator: IEnumerator
        {
            private PermittedRolesAttribute roles;
            private int positionIndex;

            public RolesEnumerator(PermittedRolesAttribute target)
            {
                this.roles = target;
                positionIndex = -1;
            }

            #region IEnumerator Members

            public void Reset()
            {
                positionIndex = -1;
            }

            public object Current
            {
                get
                {
                    return roles.allowedRoles[positionIndex];
                }
            }

            public bool MoveNext()
            {
                ++positionIndex;
                return (positionIndex < roles.allowedRoles.Length)?true:false;
            }

            #endregion
        }


	}
}
