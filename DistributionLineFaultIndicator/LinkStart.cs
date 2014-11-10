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
    public partial class LinkStart : Form
    {

        public LinkStart()
        {
            InitializeComponent();
            DataCollection.linkState = 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            DataCollection.linkState = 0;
            textBoxState.Text = "已发送链路状态请求";
            DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = true;
            DataCollection.waitTime = int.Parse(textBoxWaitTime.Text)*1000;
            timerF.Interval = DataCollection.waitTime;
            timerF.Enabled = true;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (DataCollection.linkState)
            {
                case 1:
                    textBoxState.Text = "链路状态已确认，等待链路复位中";
                    break;
                case 2:
                    textBoxState.Text = "链路复位已确认！";
                    break;
                case 3:
                    textBoxState.Text = "本机已回复链路状态！";
                    break;
                case 4:
                    textBoxState.Text = "本机已复位链路，链路握手完成！";
                    break;
                case 5:
                    textBoxState.Text = "链路及下位机初始化完成！";
                    DataCollection.linkState = -1;
                    DataCollection._ComTaskFlag.C_IC_NA_1 = true;    //总招
                    break;
                default:
                    break;
            }
        }

        //本机回复链路握手条例
        private void timerF_Tick(object sender, EventArgs e)
        {
            if (DataCollection.linkState == 0)
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = true;
            }
            else if (DataCollection.linkState == 1)
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F1 = true;
            }
            else 
            {
                timerF.Enabled = false;
            }
        }

        private void LinkStart_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerF.Enabled = false;
            timer1.Enabled = false;
        }

        private void validating_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
                e.Handled = true;
        }


    }
}
