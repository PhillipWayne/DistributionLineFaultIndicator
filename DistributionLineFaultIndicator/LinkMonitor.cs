using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistributionLineFaultIndicator
{
    public partial class LinkMonitor : Form
    {
        public LinkMonitor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataCollection.Channel1.ip = textBoxIP.Text;
            DataCollection.Channel1.port = textBoxPort.Text;

            DataCollection.linklen=int.Parse(comboBoxLAddrLen.Text); 
            DataCollection.cotlen=int.Parse(comboBoxCOTLen.Text);
            DataCollection.publen=int.Parse(comboBoxPALen.Text);
            DataCollection.inflen=int.Parse(comboBoxInAddrLen.Text);
            DataCollection.linkAddr = UInt16.Parse(textBoxLA.Text);
            DataCollection.DevAddr = UInt16.Parse(textBoxPA.Text);
        }

        private void LinkMonitor_Load(object sender, EventArgs e)
        {
            //Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
            //comboBoxComID.Items.Clear();
            //foreach (string s in pc.Ports.SerialPortNames)
            //{
            //    this.comboBoxComID.Items.Add(s);
            //}

        }

        private void validating_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9')&&e.KeyChar!=8)
                e.Handled = true;
        }

        private void validating_Ip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9')&&e.KeyChar!='.'&&e.KeyChar!=8)
                e.Handled = true;
        }



        

    }
}
