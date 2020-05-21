using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;

namespace Discovery.NonWorkingDayTest
{
    public partial class frmGetNWDS : Form
    {
        public frmGetNWDS()
        {
            InitializeComponent();
        }

        private void bntGetDetails_Click(object sender, EventArgs e)
        {
            nonWorkingDayBindingSource.DataSource = NonWorkingDayController.GetNonWorkingDays();
        }
    }
}