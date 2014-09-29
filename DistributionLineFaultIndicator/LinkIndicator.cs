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
    public partial class LinkIndicator : Form
    {
        public LinkIndicator()
        {
            InitializeComponent();
        }

        private void LinkIndicator_Load(object sender, EventArgs e)
        {
            Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
            comboBoxComID.Items.Clear();
            foreach (string s in pc.Ports.SerialPortNames)
            {
                this.comboBoxComID.Items.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataCollection.Channel2.comID = comboBoxComID.Text;
            DataCollection.Channel2.baud = comboBoxBaud.Text;
            DataCollection.Channel2.parity = comboBoxParity.Text;
            DataCollection.Channel2.stopBits = comboBoxStopBits.Text;
            DataCollection.Channel2.ip = textBoxIP.Text;
            DataCollection.Channel2.port = textBoxPort.Text;
        }
    }
}
