using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Discovery.RequestManager.Service
{
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