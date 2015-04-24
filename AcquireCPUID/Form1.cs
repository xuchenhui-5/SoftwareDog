using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace AcquireCPUID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btAcquire.FlatAppearance.BorderSize = 0;
        }

        private void btAcquire_Click(object sender, EventArgs e)
        {
            string cupID = this.GetCUPID();
            tbCUPID.Text = cupID;
        }
        public string GetCUPID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                string strCPUID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCPUID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCPUID;
            }
            catch
            {
                return "";
            }
        }
    }
}
