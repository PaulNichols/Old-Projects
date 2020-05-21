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
    // This line was created for us when the frmNWD Form was created
    public partial class frmNWD : Form
    {
        // Create a new object called nonWorkingDay for the Class NonWorkingDay

        private NonWorkingDay nonWorkingDay;

        // Form frmNWD Constructor 
        public frmNWD()
        {
            InitializeComponent();

            // Create an instance of the NonWorkingDay class

            nonWorkingDay = new NonWorkingDay();
            
            // Binding the form fields to the object nonWorkingDay
            
            nonWorkingDayBindingSource.DataSource = nonWorkingDay;
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            // Return the id from the NonWorkingDayController.SaveNonWorkingDay method, 
            // passing in the object nonWorkingDay and storing the returned id in the nonWorkingDay.Id 
            // field.
            nonWorkingDay.Id = NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

            if (nonWorkingDay.Id == -1)
            {
                MessageBox.Show("Error to create non-Working Date");
            }

        }
    }
}