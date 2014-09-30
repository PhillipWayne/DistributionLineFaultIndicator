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
    public partial class MotrParamFromMotr : Form
    {
        public MotrParamFromMotr()
        {
            InitializeComponent();
        }

        private void buttonSysParam_Click(object sender, EventArgs e)
        {
            DataCollection.SystemParam.AddrByteNum_101 = byte.Parse(textBoxAddrByteNum.Text);
            DataCollection.SystemParam.CauseByteNum_101 = byte.Parse(textBoxCauseByteNum.Text);
            DataCollection.SystemParam.PubAddByteNum_101 = byte.Parse(textBoxPubAddByteNum.Text);
            DataCollection.SystemParam.Addr = UInt16.Parse(textBoxAddr.Text);
            DataCollection.SystemParam.HeartBeatTime = UInt16.Parse(textBoxHeartBeatTime.Text);
            DataCollection.SystemParam.BeatCycle = UInt16.Parse(textBoxBeatCycle.Text);
            DataCollection.SystemParam.ComFrameSTime = UInt16.Parse(textBoxComFrameSTime.Text);
            DataCollection.SystemParam.NormalVoltageRating = UInt16.Parse(textBoxNormalVoltageRating.Text);
            DataCollection.SystemParam.NormalCurrentRating = UInt16.Parse(textBoxNormalCurrentRating.Text);
            DataCollection.SystemParam.PubAddr_101 = UInt16.Parse(textBoxPubAddr.Text);
            DataCollection.SystemParam.RequestTime = UInt16.Parse(textBoxRequestTime.Text);
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(1);            //向监视器下发监视端系统参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void buttonIpConfig_Click(object sender, EventArgs e)
        {
            DataCollection._GPRSComSet.main_IP[0] = byte.Parse(textBoxMainIP1.Text);
            DataCollection._GPRSComSet.main_IP[1] = byte.Parse(textBoxMainIP2.Text);
            DataCollection._GPRSComSet.main_IP[2] = byte.Parse(textBoxMainIP3.Text);
            DataCollection._GPRSComSet.main_IP[3] = byte.Parse(textBoxMainIP4.Text);
            DataCollection._GPRSComSet.main_Port = UInt16.Parse(textBoxMainPort.Text);

            DataCollection._GPRSComSet.res_IP[0] = byte.Parse(textBoxResIP1.Text);
            DataCollection._GPRSComSet.res_IP[1] = byte.Parse(textBoxResIP2.Text);
            DataCollection._GPRSComSet.res_IP[2] = byte.Parse(textBoxResIP3.Text);
            DataCollection._GPRSComSet.res_IP[3] = byte.Parse(textBoxResIP4.Text);
            DataCollection._GPRSComSet.res_Port = UInt16.Parse(textBoxResPort.Text);
            
            if (textBoxAPN.Text.Length < 16)
            {
                for (int i = 0; i < 16; i++)
                {
                    if(i<textBoxAPN.Text.Length)
                        DataCollection._GPRSComSet.APN[i] = textBoxAPN.Text[i];
                    else
                        DataCollection._GPRSComSet.APN[i] = '0';
                }
            }
            else if (textBoxAPN.Text.Length > 16)
            {
                for (int i = 0; i < 16; i++)
                    DataCollection._GPRSComSet.APN[i] = textBoxAPN.Text[i];
            }

            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(2);      //向监视器下发监视端ip参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void MotrParamFromMotr_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            textBoxAddrByteNum.Text=DataCollection.SystemParam.AddrByteNum_101.ToString();
            textBoxCauseByteNum.Text = DataCollection.SystemParam.CauseByteNum_101.ToString();
            textBoxPubAddByteNum.Text = DataCollection.SystemParam.PubAddByteNum_101.ToString();
            textBoxAddr.Text = DataCollection.SystemParam.Addr.ToString();
            textBoxHeartBeatTime.Text = DataCollection.SystemParam.HeartBeatTime.ToString();
            textBoxBeatCycle.Text = DataCollection.SystemParam.BeatCycle.ToString();
            textBoxComFrameSTime.Text = DataCollection.SystemParam.ComFrameSTime.ToString();
            textBoxNormalVoltageRating.Text = DataCollection.SystemParam.NormalVoltageRating.ToString();
            textBoxNormalCurrentRating.Text = DataCollection.SystemParam.NormalCurrentRating.ToString();
            textBoxPubAddr.Text = DataCollection.SystemParam.PubAddr_101.ToString();
            textBoxRequestTime.Text = DataCollection.SystemParam.RequestTime.ToString();

            textBoxMainIP1.Text = DataCollection._GPRSComSet.main_IP[0].ToString();
            textBoxMainIP2.Text = DataCollection._GPRSComSet.main_IP[1].ToString();
            textBoxMainIP3.Text = DataCollection._GPRSComSet.main_IP[2].ToString();
            textBoxMainIP4.Text = DataCollection._GPRSComSet.main_IP[3].ToString();
            textBoxMainPort.Text = DataCollection._GPRSComSet.main_Port.ToString();

            textBoxResIP1.Text=DataCollection._GPRSComSet.res_IP[0].ToString();
            textBoxResIP2.Text=DataCollection._GPRSComSet.res_IP[1].ToString();
            textBoxResIP3.Text=DataCollection._GPRSComSet.res_IP[2].ToString();
            textBoxResIP4.Text=DataCollection._GPRSComSet.res_IP[3].ToString();
            textBoxResPort.Text = DataCollection._GPRSComSet.res_Port.ToString();

        }

        private void buttonReadSysParam_Click(object sender, EventArgs e)
        {
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(3);      //向监视器下发读取监视端系统参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void buttonReadIpConfig_Click(object sender, EventArgs e)
        {
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(4);      //向监视器下发读取监视端ip参数
            DataCollection._ComStructData.TX_TASK = true;
        }
    }
}
