using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Discovery.Scheduling;

namespace Discovery.Scheduling.TestHarness
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Start the scheduling provider
            DiscoverySchedulingProvider.Instance().Start();
        }
    }
}