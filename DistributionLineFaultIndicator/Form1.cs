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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();      
            DataCollection.initializeData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Configuration configuration = new Configuration();
            //configuration.ShowDialog();
        }

        private void refresh()
        {
            textBoxHeartBeatA.Text=DataCollection.heartBeats[0].ToString();
            textBoxHeartBeatB.Text=DataCollection.heartBeats[1].ToString();
            textBoxHeartBeatC.Text=DataCollection.heartBeats[2].ToString();

            textBoxShortCircuitA.Text = DataCollection.shortCircuist[0].ToString();
            textBoxShortCircuitB.Text = DataCollection.shortCircuist[1].ToString();
            textBoxShortCircuitC.Text = DataCollection.shortCircuist[2].ToString();

            textBoxGroundFaultA.Text = DataCollection.groundFaults[0].ToString();
            textBoxGroundFaultB.Text = DataCollection.groundFaults[1].ToString();
            textBoxGroundFaultC.Text = DataCollection.groundFaults[2].ToString();

            textBoxPowerOnA.Text = DataCollection.powerOns[0].ToString();
            textBoxPowerOnB.Text = DataCollection.powerOns[1].ToString();
            textBoxPowerOnC.Text = DataCollection.powerOns[2].ToString();

            textBoxPowerOffA.Text = DataCollection.powerOffs[0].ToString();
            textBoxPowerOffB.Text = DataCollection.powerOffs[1].ToString();
            textBoxPowerOffC.Text = DataCollection.powerOffs[2].ToString();

            textBoxShortCircuitTypeA.Text = DataCollection.shortCircuitTypes[0].ToString();
            textBoxShortCircuitTypeB.Text = DataCollection.shortCircuitTypes[1].ToString();
            textBoxShortCircuitTypeC.Text = DataCollection.shortCircuitTypes[2].ToString();

            textBoxLoadCurrentA.Text=DataCollection.loadCurrents[0].ToString();
            textBoxLoadCurrentB.Text=DataCollection.loadCurrents[1].ToString();
            textBoxLoadCurrentC.Text=DataCollection.loadCurrents[2].ToString();

            textBoxCellVoltageA.Text = DataCollection.cellVoltages[0].ToString();
            textBoxCellVoltageB.Text = DataCollection.cellVoltages[1].ToString();
            textBoxCellVoltageC.Text = DataCollection.cellVoltages[2].ToString();      
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //AddressConfig addressConfig = new AddressConfig();
            //addressConfig.ShowDialog();
        }
    }
}
