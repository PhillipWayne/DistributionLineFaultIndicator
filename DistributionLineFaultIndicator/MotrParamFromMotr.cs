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
            DataCollection.montrParamState = 0;
            labelState.Text = "参数下设中...";
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
            DataCollection.montrParamState = 0;
            labelState.Text = "参数下设中...";
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
                for (int i = 0; i < textBoxAPN.Text.Length; i++)
                {
                    DataCollection._GPRSComSet.APN[i] = textBoxAPN.Text[i]; 
                }
                DataCollection._GPRSComSet.APN[textBoxAPN.Text.Length] = '\0';

            }
            else 
            {
                MessageBox.Show("APN输入错误！");
                return;
            }

            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(2);      //向监视器下发监视端ip参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void MotrParamFromMotr_Load(object sender, EventArgs e)
        {
            DataCollection.montrParamState = 0;
            refresh();
            labelState.Text = "";
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

            textBoxAPN.Text = new string(DataCollection._GPRSComSet.APN);

            System.Diagnostics.Debug.WriteLine(DataCollection.Version.VER_FACNO + " " + DataCollection.Version.VER_DEVNO);
            if (DataCollection.Version.VER_SOFTNO != "")
            {
                labelVersion.Text = String.Format("{0} {1} {2} {3}年{4}月{5}日", DataCollection.Version.VER_FACNO, DataCollection.Version.VER_DEVNO, DataCollection.Version.VER_SOFTNO.ToString(),
                DataCollection.Version.VER_SOFTDATE[2].ToString("X2"), DataCollection.Version.VER_SOFTDATE[1].ToString("X2"), DataCollection.Version.VER_SOFTDATE[0].ToString("X2"));
            }
       

        }

        private void buttonReadSysParam_Click(object sender, EventArgs e)
        {
            labelState.Text = "参数读取中...";
            DataCollection.montrParamState = 0;
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(3);      //向监视器下发读取监视端系统参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void buttonReadIpConfig_Click(object sender, EventArgs e)
        {
            labelState.Text = "参数读取中...";
            DataCollection.montrParamState = 0;
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(4);      //向监视器下发读取监视端ip参数
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DataCollection.montrParamState == 1)
            {
                labelState.Text = "设置成功";
            }
            else if (DataCollection.montrParamState == 2)
            {
                labelState.Text = "设置失败";
            }
            else if (DataCollection.montrParamState == 3)
            { 
                 labelState.Text = "读取失败";
            }
            else if (DataCollection.montrParamState == 4)
            {
                labelState.Text = "读取成功";
            }
            else if (DataCollection.montrParamState == 5)
            {
                labelState.Text = "网络传输错误";
            }
            if (DataCollection.montrUpdate == 1)
            {
                DataCollection.montrUpdate = 0;
                refresh();
            }
        }

        private void MotrParamFromMotr_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void buttonReadVersion_Click(object sender, EventArgs e)
        {
            labelState.Text = "参数读取中...";
            DataCollection.montrParamState = 0;
            DataCollection._ComStructData.TxLen = ProtocoltyParam.EncodeFrame(5);      //向监视器下发读版本号
            DataCollection._ComStructData.TX_TASK = true;
        }

        private void buttonTimeSyn_Click(object sender, EventArgs e)
        {
            if (textBoxMilisecond.Text != "" &&textBoxSecond.Text!=""&& textBoxMinute.Text != "" && textBoxHour.Text != "" && textBoxDay.Text != "" && textBoxMonth.Text != "" && textBoxYear.Text!=""&& int.Parse(textBoxMilisecond.Text) < 1000 &&int.Parse(textBoxSecond.Text)<60&& int.Parse(textBoxMinute.Text) < 60 && int.Parse(textBoxHour.Text) < 24 && int.Parse(textBoxDay.Text) < 32
                && int.Parse(textBoxMonth.Text) < 13 && int.Parse(textBoxMonth.Text) != 0 && int.Parse(textBoxDay.Text)!=0)
            {
                labelState.Text = "参数下设中...";
                DataCollection.montrParamState = 0;
                int len = 0;
                DataCollection._DataField.TXBuffer[len++] = (byte)(int.Parse(textBoxMilisecond.Text) + int.Parse(textBoxSecond.Text)*1000);
                DataCollection._DataField.TXBuffer[len++] = (byte)((int.Parse(textBoxMilisecond.Text) + int.Parse(textBoxSecond.Text) * 1000) >> 8);
                DataCollection._DataField.TXBuffer[len++] = byte.Parse(textBoxMinute.Text);
                DataCollection._DataField.TXBuffer[len++] = byte.Parse(textBoxHour.Text);
                DataCollection._DataField.TXBuffer[len++] = byte.Parse(textBoxDay.Text);
                DataCollection._DataField.TXBuffer[len++] = byte.Parse(textBoxMonth.Text);
                DataCollection._DataField.TXBuffer[len++] = (byte)(int.Parse(textBoxYear.Text)%100);
                DataCollection._DataField.TXFieldLen = len;
                DataCollection._DataField.TXFieldVSQ = 1;
                DataCollection._ComTaskFlag.C_CS_NA_1 = true;
            }
            else
            {
                MessageBox.Show("请输入正确的时间！","时间格式错误",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxMilisecond.Text = DateTime.Now.Millisecond.ToString();
            textBoxSecond.Text = DateTime.Now.Second.ToString();
            textBoxMinute.Text = DateTime.Now.Minute.ToString();
            textBoxHour.Text = DateTime.Now.Hour.ToString();
            textBoxDay.Text = DateTime.Now.Day.ToString();
            textBoxMonth.Text = DateTime.Now.Month.ToString();
            textBoxYear.Text = DateTime.Now.Year.ToString();
        }



        private void validating_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9')&&e.KeyChar!=8)
                e.Handled = true;
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text==""||int.Parse(((TextBox)sender).Text) > 65535)
            {
                e.Cancel = true;
                ((TextBox)sender).BackColor = Color.Red;
                labelState.Text = "请输入0~65535！";
            }
            else
            {
                ((TextBox)sender).BackColor = SystemColors.Window;
                labelState.Text = "";
            }
        }

        private void IP_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text == "" || int.Parse(((TextBox)sender).Text) > 255)
            {
                e.Cancel = true;
                ((TextBox)sender).BackColor = Color.Red;
                labelState.Text = "请输入0~255！";
            }
            else
            {
                ((TextBox)sender).BackColor = SystemColors.Window;
                labelState.Text = "";
            }
        }

       
 
    }
}
