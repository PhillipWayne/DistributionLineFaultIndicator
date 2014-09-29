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
    public partial class AddressConfig : Form
    {
        int id;
        public AddressConfig(int id)
        {
            InitializeComponent();
            this.id =id;
        }

        private void display()
        {
            textBoxIndtrAdds.Text = DataCollection.indtrAdds[id].ToString();
            textBoxMDMCFG0.Text = DataCollection.MDMCFG0[id].ToString();
            textBoxMDMCFG1.Text = DataCollection.MDMCFG1[id].ToString();
            textBoxMDMCFG2.Text = DataCollection.MDMCFG2[id].ToString();
            textBoxMDMCFG3.Text = DataCollection.MDMCFG3[id].ToString();
            textBoxMDMCFG4.Text = DataCollection.MDMCFG4[id].ToString();   
        }


      

        private void button1_Click(object sender, EventArgs e)
        {
            DataCollection.indtrAdds[id] = byte.Parse(textBoxIndtrAdds.Text);
            DataCollection.MDMCFG0[id] = byte.Parse(textBoxMDMCFG0.Text);
            DataCollection.MDMCFG1[id] = byte.Parse(textBoxMDMCFG1.Text);
            DataCollection.MDMCFG2[id] = byte.Parse(textBoxMDMCFG2.Text);
            DataCollection.MDMCFG3[id] = byte.Parse(textBoxMDMCFG3.Text);
            DataCollection.MDMCFG4[id] = byte.Parse(textBoxMDMCFG4.Text);   
        }

        private void AddressConfig_Load(object sender, EventArgs e)
        {
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
        }
      
    }
}
