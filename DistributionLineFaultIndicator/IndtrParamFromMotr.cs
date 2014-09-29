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
    public partial class IndtrParamFromMotr : Form
    {
        public IndtrParamFromMotr()
        {
            InitializeComponent();
        }

        private void IndtrParamFromMotr_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            int index = getIndex();
            textBoxSuDuanSwitch.Text = DataCollection.quickBreakSwitch[index].ToString();
            textBoxSuDuanSet.Text = DataCollection.quickBreakValue[index].ToString();
            textBoxSuDuanTimeSet.Text = DataCollection.quickBreakTime[index].ToString();
            textBoxGuoLiuSwitch.Text = DataCollection.overCurrentSwitch[index].ToString();
            textBoxGuoLiuSet.Text = DataCollection.overCurrentValue[index].ToString();
            textBoxGuoLiuTimeSet.Text = DataCollection.overCurrentTime[index].ToString();
            textBoxWuLiuSet.Text = DataCollection.freeCurrentValue[index].ToString();
            textBoxWuLiuTimeSet.Text = DataCollection.freeCurrentTime[index].ToString();
            textBoxYongLiuTimeSet.Text = DataCollection.flashyFlowTime[index].ToString();
            textBoxSelfAdapSwitch.Text = DataCollection.selfAdaptionSwitch[index].ToString();
            textBoxIndicatorAddr.Text = DataCollection.indtrAdds[index].ToString();
            textBoxRate.Text = DataCollection.rate[index].ToString();
            textBoxBandWidth.Text = DataCollection.bandWidth[index].ToString();
            textBoxTgz.Text = DataCollection.tgz[index].ToString();
            textBoxTfgs.Text = DataCollection.tfgs[index].ToString();
            textBoxRes3.Text = DataCollection.res3[index].ToString();
            textBoxRes4.Text = DataCollection.res4[index].ToString();

            textBoxManualreset.Text = DataCollection.manualReset[index].ToString();
            textBoxJiaoZhun.Text = DataCollection.calibration[index].ToString();
            textBoxRes1.Text = DataCollection.res1[index].ToString();
            textBoxRes2.Text = DataCollection.res2[index].ToString();
        }

        private int getIndex()
        {
            switch (comboBoxIndtr.Text)
            {
                case "指示器1":
                    return 0;
                case "指示器2":
                    return 1;
                case "指示器3":
                    return 2;
                case "指示器4":
                    return 3;
                case "指示器5":
                    return 4;
                case "指示器6":
                    return 5;
                case "指示器7":
                    return 6;
                case "指示器8":
                    return 7;
                case "指示器9":
                    return 8;
                default:
                    return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index=getIndex();
            DataCollection.quickBreakSwitch[index]=UInt16.Parse(textBoxSuDuanSwitch.Text);
            DataCollection.quickBreakValue[index]=UInt16.Parse(textBoxSuDuanSet.Text);
            DataCollection.quickBreakTime[index]=UInt16.Parse(textBoxSuDuanTimeSet.Text);
            DataCollection.overCurrentSwitch[index]=UInt16.Parse(textBoxGuoLiuSwitch.Text);
            DataCollection.overCurrentValue[index]=UInt16.Parse(textBoxGuoLiuSet.Text);
            DataCollection.overCurrentTime[index]=UInt16.Parse(textBoxGuoLiuTimeSet.Text);
            DataCollection.freeCurrentValue[index]=UInt16.Parse(textBoxWuLiuSet.Text);
            DataCollection.freeCurrentTime[index] =UInt16.Parse(textBoxWuLiuTimeSet.Text);
            DataCollection.flashyFlowTime[index]=UInt16.Parse(textBoxYongLiuTimeSet.Text);
            DataCollection.selfAdaptionSwitch[index] =UInt16.Parse(textBoxSelfAdapSwitch.Text);
            DataCollection.indtrAdds[index]=UInt16.Parse(textBoxIndicatorAddr.Text);
            DataCollection.rate[index]=UInt16.Parse(textBoxRate.Text);
            DataCollection.bandWidth[index]=UInt16.Parse(textBoxBandWidth.Text);
            DataCollection.tgz[index]=UInt16.Parse(textBoxTgz.Text);
            DataCollection.tfgs[index]=UInt16.Parse(textBoxTfgs.Text);
            DataCollection.res3[index]=UInt16.Parse(textBoxRes3.Text);
            DataCollection.res4[index] = UInt16.Parse(textBoxRes4.Text);
            DataCollection._ComStructData.TxLen = ProtocoltyParam.ParamEncodeFrame(1,index);
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = getIndex();
            DataCollection.manualReset[index]=byte.Parse(textBoxManualreset.Text );
            DataCollection.calibration[index]=byte.Parse(textBoxJiaoZhun.Text );
            DataCollection.res1[index]=byte.Parse(textBoxRes1.Text) ;
            DataCollection.res2[index]=byte.Parse(textBoxRes2.Text) ;
            DataCollection._ComStructData.TxLen = ProtocoltyParam.ParamEncodeFrame(2,index);
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void comboBoxIndtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

    }
}
