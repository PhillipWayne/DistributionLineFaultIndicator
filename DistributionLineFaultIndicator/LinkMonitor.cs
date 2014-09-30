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

            DataCollection.linklen=int.Parse(textBoxLALen.Text); 
            DataCollection.cotlen=int.Parse(textBoxCOTLen.Text);
            DataCollection.publen=int.Parse(textBoxPALen.Text);
            DataCollection.inflen=int.Parse(textBoxInALen.Text);
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





        

    }
}
