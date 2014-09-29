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
    public partial class Indicator : Form
    {
        int id;
        public Indicator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxIndicator.Text == "")
                return;
            Configuration configuration = new Configuration(id);
            configuration.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBoxIndicator.Text == "")
                return;
            AddressConfig addressConfig = new AddressConfig(id);
            addressConfig.ShowDialog();
        }


        

        private void comboBoxIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxIndicator.Text)
            {
                case "指示器1（1路）":
                    id=0;
                    break;
                case "指示器2（1路）":
                    id=1;
                    break;
                case "指示器3（1路）":
                    id=2;
                    break;
                case "指示器4（2路）":
                    id=3;
                    break;
                case "指示器5（2路）":
                    id=4;
                    break;
                case "指示器6（2路）":
                    id=5;
                    break;
                case "指示器7（3路）":
                    id=6;
                    break;
                case "指示器8（3路）":
                    id=7;
                    break;
                case "指示器9（3路）":
                    id=8;
                    break;
            }
            refresh();
        }


        private void refresh()
        {
            textBoxHeartBeat.Text = DataCollection.heartBeats[id].ToString();
            textBoxShortCircuit.Text = DataCollection.shortCircuist[id].ToString();
            textBoxGroundFault.Text = DataCollection.groundFaults[id].ToString();
            textBoxPowerOn.Text = DataCollection.powerOns[id].ToString();
            textBoxPowerOff.Text = DataCollection.powerOffs[id].ToString();
            textBoxShortCircuitType.Text = DataCollection.shortCircuitTypes[id].ToString();
            textBoxLoadCurrent.Text = DataCollection.loadCurrents[id].ToString();
            textBoxCellVoltage.Text = DataCollection.cellVoltages[id].ToString();
        }


    }
}
