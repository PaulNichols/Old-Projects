using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Discovery.BusinessObjects.Controllers;

namespace Discovery.NonWorkingDayTest
{
    public partial class frmNWDS : Form
    {
        public frmNWDS()
        {
            InitializeComponent();
        }

        private void frmNWDS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'discoveryDataSet.Discovery_OptrakRegion' table. You can move, or remove it, as needed.
            this.discovery_RegionTableAdapter.Fill(this.discoveryDataSet.Discovery_Region);

        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            // Return the id from the NonWorkingDayController.SaveNonWorkingDay method, 
            // passing in the object nonWorkingDay and storing the returned id in the nonWorkingDay.Id 
            // field.

            DateTime EndDate = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt32(txtNumberOfDays.Text));

            int RegionId = 0;
            int WarehouseId = 0;
            string Description = "";
            bool WeekendOnly = true;

            int returnValue = NonWorkingDayController.SaveNonWorkingDays(
                                              Convert.ToDateTime(txtStartDate.Text), 
                                              EndDate,
                                              Description,
                                              RegionId,
                                              WarehouseId,
                                              WeekendOnly,
                                              txtUpdatedBy.Text);

            switch (returnValue)
            {
                case 0:
                    MessageBox.Show("No update been performed");
                    break;
                case -1:
                    MessageBox.Show("Error Create non-working date");
                    break;
                default:
                    MessageBox.Show("Updated successfully");
                    break;
            }
        }
    }
}