using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle("HBOS.FS.AMP.UPD.Security")]
[assembly: AssemblyDescription("UPD Authorisation and Permissions provider")]
#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("")]
#endif
[assembly: AssemblyCompany("HBOS Financial Services")]
[assembly: AssemblyProduct("HBOS Asset Management Project, UPD")]
[assembly: AssemblyCopyright("HBOS Financial Services 2005")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: AssemblyDelaySign(false)]
#if (!DEBUG)
//[assembly: AssemblyKeyFile("..\\..\\..\\HBOSFS.snk")]
#endif
[assembly: AssemblyKeyName("")]

// Reject every permission the class does not need
[assembly:FileDialogPermission(SecurityAction.RequestRefuse, Unrestricted=true)]
[assembly:RegistryPermission(SecurityAction.RequestRefuse, Unrestricted=true)]
[assembly:FileIOPermission(SecurityAction.RequestRefuse, Unrestricted=true)]

