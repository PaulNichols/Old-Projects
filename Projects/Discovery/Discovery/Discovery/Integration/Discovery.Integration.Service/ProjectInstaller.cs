using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Discovery.Integration.Service
{
    /// <summary>
    /// A project installation class which is in Discovery.Integration.Service
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}