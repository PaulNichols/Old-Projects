using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyTitle("HBOS.FS.AMP.Security")]
[assembly: AssemblyDescription("AMP Authorisation and Permissions provider")]
#if (DEBUG)
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("")]
#endif
[assembly: AssemblyCompany("HBOS Financial Services")]
[assembly: AssemblyProduct("HBOS Data File Readers")]
[assembly: AssemblyCopyright("HBOS Financial Services 2005")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("1.0.1.*")]

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("..\\..\\HBOSFS.snk")]
[assembly: AssemblyKeyName("")]

// Reject every permission the class does not need
[assembly:FileDialogPermission(SecurityAction.RequestRefuse, Unrestricted=true)]
[assembly:RegistryPermission(SecurityAction.RequestRefuse, Unrestricted=true)]
[assembly:FileIOPermission(SecurityAction.RequestRefuse, Unrestricted=true)]

