using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Threading;
using Discovery.Integration;
using System.Messaging;

namespace Discovery.Integration.Testharness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          IntegrationController.Start();
          
           
        }
    }
}