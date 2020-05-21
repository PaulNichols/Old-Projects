using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.ComponentModel;

namespace HBOS.FS.AMP.UPD.CustomInstaller
{
	/// <summary>
	/// Custom install event to create the ExceptionManager event log source, as users will not have
	/// permissions to do this at runtime.
	/// </summary>
    [RunInstaller(true)]
    public class MyEventLogInstaller: Installer
    {
        string[] eventSources = {"ExceptionManagerPublishedException", "ExceptionManagerInternalException"};

        /// <summary>
        /// Performs the custom installation action to create event log sources.
        /// </summary>
        /// <param name="stateSaver">An IDictionary used to save information needed to perform a commit, rollback, or uninstall operation.</param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            try
            {
                base.Install (stateSaver);
                for (int i=0; i < eventSources.Length; i++)
                {
                    if (!EventLog.SourceExists(eventSources[i]))
                      EventLog.CreateEventSource(eventSources[i], "Application");
                }

            }
            catch (Exception e)
            {
                throw new InstallException("Cannot create event sources for Exception Publisher", e);
            }
        }


    }
    
}
