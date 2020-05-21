using System;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace HBOS.FS.AMP.Security
{
	/// <summary>
	/// Represents a role checked window or dialog box that makes up an application's user interface.
	/// </summary>
	/// <remarks>
	/// A <see cref="RoleCheckedForm"/>, in concert with the <see cref="PermittedRolesAttribute"/> builds 
	/// upon the functionality the standard <see cref="System.Windows.Forms.Form"/> provides to allow the
	/// developer to mark fields as limited to particular <see cref="System.Security.Principal"/> roles.
	/// </remarks>
	/// <permission cref="System.Security.Permissions.StrongNameIdentityPermission">
	/// Only assemblies signed with the HBOSFS strong name key can inherit from this class.
	/// </permission>
	/// <example>
	/// <code lang="c#">
    /// public class TestForm : RoleCheckedForm
    /// {
    ///     [PermittedRoles("Administrator")]
    ///     private System.Windows.Forms.CheckBox isAdministrator;        
    ///     [PermittedRoles("Administrator,Users")]
    ///     private System.Windows.Forms.CheckBox isUser;
    ///     
    ///     ...
    ///  }
	/// </code>
	/// </example>
    [UIPermission(SecurityAction.Demand, 
         Window = UIPermissionWindow.SafeTopLevelWindows, 
         Clipboard = UIPermissionClipboard.NoClipboard)]

    // When compiled in release mode this class will only be usable by assemblies signed with the HBOS key
#if (!DEBUG)
    [StrongNameIdentityPermission(SecurityAction.LinkDemand, 
    PublicKey = "0024000004800000940000000602000000240000525341310004000001000100e37bc1f6f237dacca277f763fde37f5d3194b14319283b63780c8ccc398e022287b1b186d5c41416df8b918c95716fec453d249541753eed9e79ba2d600a55fe40fc5b25c826a6d55f2a262bc4cb5715693ac116882efdc817652113aa6899647d08ed8b8215baa058469c75d7eda8c68b1dc4ee380514d04219fafafa8521be")]
#endif

    public class RoleCheckedForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Creates a new instance of <see cref="RoleCheckedForm"/>
		/// </summary>
        public RoleCheckedForm()
		{
		}
        
        /// <summary>
        /// Raises the Load event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// <para>Raising an event invokes the event handler through a delegate. For more information, 
        /// see Raising an Event.</para>
        /// <para>The OnLoad method also allows derived classes to handle the event without attaching a delegate. 
        /// This is the preferred technique for handling the event in a derived class.</para>
        /// <note type="inheritinfo">
        /// When overriding OnLoad in a derived class, be sure to call the base class's 
        /// OnLoad method so that registered delegates receive the event and the role checking occurs in your
        /// derived class's fields.
        /// </note>
        /// </remarks>

        override protected void OnLoad(EventArgs e)
        {
            // Get the type of the form and all the fields within it.
            Type formType = this.GetType();
            FieldInfo[] fields = formType.GetFields(BindingFlags.NonPublic | 
                BindingFlags.Public | 
                BindingFlags.Instance);

            // Get the current security context for this thread.
            IPrincipal currentPrincipal = Thread.CurrentPrincipal;
            
            // Go through each class field
            foreach (FieldInfo field in fields)
            {                                
                // Load up the custom attributes for the field
                object[] customAttributes = field.GetCustomAttributes(true);
                foreach (object customAttribute in customAttributes)
                {                    
                    // And finally check if we have our custom attribute.
                    if (customAttribute is PermittedRolesAttribute)
                    {
                        // Now check to see if we have a writable Visible property
                        // for the field the attribute decorated
                        PropertyInfo visible = field.FieldType.GetProperty("Visible", typeof(bool)) ;
                        if (visible != null && visible.CanWrite)                        
                        {                            
                            // So we have a field decorated with our custom attribute.

                            // Get the list of allowed roles for this field and check the current
                            // security context to see if we are in any of the roles.
                            bool isInRole = false;

                            foreach (string allowedRole in (PermittedRolesAttribute)customAttribute)
                            {
                                if (currentPrincipal.IsInRole(allowedRole))
                                    isInRole = true;
                            }
                            
                            if (!isInRole)
                            {
                                // We didn't match any of the allow roles, so now we must hide the 
                                // field this attribute was applied to, by setting the Visible 
                                // property to false

                                // Get a reference to the field. 
                                // GetValue will return a reference if it's called on an object.
                                object roleCheckedField = field.GetValue(this);
                                // Get the type of the field
                                Type checkedType = roleCheckedField.GetType();
                                // Get the visible property for our copy
                                PropertyInfo instanceVisible = checkedType.GetProperty("Visible", typeof(bool)) ;
                                if (null != instanceVisible)
                                {
                                    // Now set the visible property on the field.
                                    instanceVisible.SetValue(roleCheckedField, false, null);                                    
                                }
                            }
                        }
                        else
                        {
                            // The attribute was applied to a field which did not support the visible property.
                            string detailedException = 
                                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                "PermittedRolesAttribute applied to {0} which does not have a writable Visible property.",
                                field.Name);
                            throw new PermittedRolesAttributeException(detailedException);
                        }
                    }
                }
            }

            base.OnLoad(e);
        }
    }
}
