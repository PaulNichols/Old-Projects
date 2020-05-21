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
    public partial class frmGetNWDSByRegionWithinDateRange : Form
    {
        public frmGetNWDSByRegionWithinDateRange()
        {
            InitializeComponent();
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDateFrom.Text))
            {
                MessageBox.Show("Enter a Date From");
                bool setcursorto = txtDateFrom.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtNoOfDays.Text))
            {
                MessageBox.Show("Enter a value");
                bool setcursorto = txtNoOfDays.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtRegion.Text))
            {
                MessageBox.Show("Enter a OptrakRegion Code");
                bool setcursorto = txtRegion.Focus();
                return;
            }

            DateTime dateFrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dateTo = dateFrom.AddDays(Convert.ToInt32(txtNoOfDays.Text));

            //nonWorkingDayBindingSource.DataSource = NonWorkingDayController.GetNonWorkingDaysByRegion(dateFrom, dateTo, txtRegion.Text);

        }

    }
}