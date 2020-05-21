using System;
using System.Windows.Forms;

using HBOS.FS.AMP.Security;

namespace Example
{
	/// <summary>
	/// Simple class illustrating usage of the AMPPrinciple class.
	/// </summary>
	class SecurityExample
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            string dbConnection = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AMP_Pricing;Data Source=(local)";


            // For testing without the database, use the generic principal
            AppDomain.CurrentDomain.SetThreadPrincipal(
                new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity("test"), 
                new string[] {"administrator", "user"})
            );

            // Now create forms, threads or whatever and the principal will be inherited     
            Application.Run(new TestForm());
              
		}
	}
}
