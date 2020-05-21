using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Namespaces needed for permissions
using System.Security;
using System.Security.Permissions;
using System.Data.SqlClient;
using System.Net;
using System.IO;



#region Assembly Attribures
[assembly: AssemblyTitle("Unit Pricing and Distribution Administration - GUI Controls and Supporting Classes")]
[assembly: AssemblyDescription("Unit Pricing and Distribution Administration - GUI Controls and Supporting Classes")]
#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("")]
#endif
[assembly: AssemblyCompany("HBOS Financial Services")]
[assembly: AssemblyProduct("HBOS Asset Management Project, UPD")]
[assembly: AssemblyCopyright("(c) 2005 HBOS Financial Services")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

#endregion


#region Security Permissions
// Minimum security demands the application needs.

// Needed to attach to SQL
[assembly:SqlClientPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
// Needed to open the common file open and file save dialog
[assembly:FileDialogPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
// Needed to open files
[assembly:FileIOPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
// Needed to have a UI
[assembly:UIPermission(SecurityAction.RequestMinimum, Unrestricted=true)]
// Needed to make declarative and assertive permission requests
[assembly:SecurityPermission(SecurityAction.RequestMinimum, Unrestricted=true)] 
// Needed to make SOAP calls (for distribution)
[assembly:WebPermission(SecurityAction.RequestMinimum, Unrestricted=true)] 
// Needed for serialisation
[assembly:ReflectionPermission(SecurityAction.RequestMinimum, Unrestricted=true)] 
// Needed for XSLT transforms
[assembly:ZoneIdentityPermission(SecurityAction.RequestMinimum, Unrestricted=false)]
// Needed for event logging
[assembly:RegistryPermission(SecurityAction.RequestMinimum, Unrestricted=true)]

#endregion

#region Code Signing
//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
#endregion
