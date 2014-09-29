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
    public partial class Configuration : Form
    {
        int id;
        public Configuration(int id)
        {
            InitializeComponent();
            this.id = id;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            DataCollection.flashyFlowTime[id]=UInt16.Parse(textBoxFlashyFlowTime.Text);
            DataCollection.freeCurrentTime[id] = UInt16.Parse(textBoxFreeCurrentTime.Text);
            DataCollection.freeCurrentValue[id] = UInt16.Parse(textBoxFreeCurrentValue.Text);

            DataCollection.overCurrentSwitch[id] = UInt16.Parse(textBoxOverCurrentSwitch.Text);
            DataCollection.overCurrentTime[id] = UInt16.Parse(textBoxOverCurrentTime.Text);
            DataCollection.overCurrentValue[id] = UInt16.Parse(textBoxOverCurrentValue.Text);
            DataCollection.quickBreakSwitch[id] = UInt16.Parse(textBoxQuickBreakSwitch.Text);
            DataCollection.quickBreakTime[id] = UInt16.Parse(textBoxQuickBreakTime.Text);
            DataCollection.quickBreakValue[id] = UInt16.Parse(textBoxQuickBreakValue.Text);
            DataCollection.selfAdaptionSwitch[id] = UInt16.Parse(textBoxSelfAdaptionSwitch.Text);


            //textBoxCalibration.Text = DataCollection.calibration[getIndex()].ToString();
            //textBoxManualReset.Text = DataCollection.manualReset[getIndex()].ToString();
        }

        private void display()
        {
            textBoxCalibration.Text = DataCollection.calibration[id].ToString();
            textBoxFlashyFlowTime.Text = DataCollection.flashyFlowTime[id].ToString();
            textBoxFreeCurrentTime.Text = DataCollection.freeCurrentTime[id].ToString();
            textBoxFreeCurrentValue.Text = DataCollection.freeCurrentValue[id].ToString();
            textBoxManualReset.Text = DataCollection.manualReset[id].ToString();
            textBoxOverCurrentSwitch.Text = DataCollection.overCurrentSwitch[id].ToString();
            textBoxOverCurrentTime.Text = DataCollection.overCurrentTime[id].ToString();
            textBoxOverCurrentValue.Text = DataCollection.overCurrentValue[id].ToString();
            textBoxQuickBreakSwitch.Text = DataCollection.quickBreakSwitch[id].ToString();
            textBoxQuickBreakTime.Text = DataCollection.quickBreakTime[id].ToString();
            textBoxQuickBreakValue.Text = DataCollection.quickBreakValue[id].ToString();
            textBoxSelfAdaptionSwitch.Text = DataCollection.selfAdaptionSwitch[id].ToString();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
        }


    }
}
