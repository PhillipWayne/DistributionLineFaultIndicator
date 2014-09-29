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
    public partial class ComConnectTest : Form
    {
        int recLen;
        public ComConnectTest()
        {
            InitializeComponent();
            recLen = DataCollection._ComStructData.RxLen;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null)
                return;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(textBox1.Text);
            for (int i = 0; i < byteArray.Length; i++)
            {
                DataCollection._ComStructData.TXBuffer[i] = byteArray[i];
                DataCollection._ComStructData.TxLen = byteArray.Length;
                DataCollection._ComStructData.TX_TASK = true;
            }
            textBox2.AppendText(textBox1.Text + "\n");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (recLen != DataCollection._ComStructData.RxLen)
            {
                byte[] byteArray = new byte[DataCollection._ComStructData.RxLen];
                for (int i = 0; i < DataCollection._ComStructData.RxLen; i++)
                {
                    byteArray[i] = DataCollection._ComStructData.RXBuffer[i];
                }
                string str = System.Text.Encoding.Default.GetString (byteArray);
                recLen = DataCollection._ComStructData.RxLen;
                textBox3.AppendText(str + "\n");
            }
            textBox5.Text = DataCollection.trial.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataCollection.trial = int.Parse(textBox4.Text);

        }
    }
}
