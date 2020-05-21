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
    public partial class frmNWD2 : Form
    {
        private NonWorkingDay nonWorkingDay;

        public frmNWD2()
        {
            InitializeComponent();

            // Create an instance of the NonWorkingDay class

            nonWorkingDay = new NonWorkingDay();

            // Binding the form fields to the object nonWorkingDay

            nwd2bindingSource.DataSource = nonWorkingDay;
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtWarehouseCode.Text))
            {
                MessageBox.Show("Enter a Warehouse Code");
                bool setcursorto = txtWarehouseCode.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtNonWorkingDate.Text))
            {
                MessageBox.Show("Enter a Non-Working Date");
                bool setcursorto = txtNonWorkingDate.Focus();
                return;
            }

            // Return the id from the NonWorkingDayController.SaveNonWorkingDay method, 
            // passing in the object nonWorkingDay and storing the returned id in the nonWorkingDay.Id 
            // field.
            nonWorkingDay = NonWorkingDayController.GetNonWorkingDay(txtWarehouseCode.Text, Convert.ToDateTime(txtNonWorkingDate.Text));

            if (nonWorkingDay == null)
            {
                MessageBox.Show("non-Working Date doesn't exist");
                return;
            }
            else
            {
                nonWorkingDay.WarehouseCode = txtWarehouseCode.Text;
                nwd2bindingSource.DataSource = nonWorkingDay;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtWarehouseId.Text))
            {
                MessageBox.Show("Use Get Details Button to retrieve the non-working date");
                bool setcursorto = txtWarehouseCode.Focus();
                return;
            }
            // Return the id from the NonWorkingDayController.SaveNonWorkingDay method, 
            // passing in the object nonWorkingDay and storing the returned id in the nonWorkingDay.Id 
            // field.
            bool returnValue = NonWorkingDayController.DeleteNonWorkingDay(nonWorkingDay);

            if (!returnValue)
            {
                MessageBox.Show("Error to delete the non-working date");
                return;
            }
            else
            {
                MessageBox.Show("The non-working date is removed");
                return;
            }
        }
    }
}