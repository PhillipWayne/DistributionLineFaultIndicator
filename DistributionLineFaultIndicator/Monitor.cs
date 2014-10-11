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
    public partial class Monitor : Form
    {
        public Monitor()
        {
            InitializeComponent();
        }

        private void Monitor_Load(object sender, EventArgs e)
        {
            ListViewItem lv;
            listView1.Items.Clear();
            for (int i = 0; i < DataCollection.YxData.num; i++)
            {
                lv = new ListViewItem();
                lv.SubItems.Add(listView1.Items.Count.ToString());
                lv.SubItems.Add(DataCollection.YxData.name[i]);
                lv.SubItems.Add(DataCollection.YxData.addr[i]);
                lv.SubItems.Add(DataCollection.YxData.value[i]);
                listView1.Items.Add(lv);
            }
            timerRefresh.Enabled = true;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            ListViewItem lv;
            if (tabControl1.SelectedIndex == 0)
            {
                listView1.Items.Clear();
                for (int i = 0; i < DataCollection.YxData.num; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView1.Items.Count.ToString());
                    lv.SubItems.Add(DataCollection.YxData.name[i]);
                    lv.SubItems.Add(DataCollection.YxData.addr[i]);
                    lv.SubItems.Add(DataCollection.YxData.value[i]);
                    listView1.Items.Add(lv);
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                listView2.Items.Clear();
                for (int i = 0; i < DataCollection.YcData.num; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView2.Items.Count.ToString()); 
                    lv.SubItems.Add(DataCollection.YcData.name[i]);
                    lv.SubItems.Add(DataCollection.YcData.addr[i]);
                    lv.SubItems.Add(DataCollection.YcData.value[i]);
                    listView2.Items.Add(lv);
                } 
            }
            else
            {
                listView3.Items.Clear();
                for (int i = 0; i < DataCollection.Event.addr.Count; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView3.Items.Count.ToString());
                    lv.SubItems.Add(DataCollection.Event.name[i]);
                    lv.SubItems.Add(DataCollection.Event.addr[i]);
                    lv.SubItems.Add(DataCollection.Event.value[i]);
                    lv.SubItems.Add(DataCollection.Event.date[i]);
                    listView3.Items.Add(lv);
                } 
            }
            
           
        }

        private void refresh()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < DataCollection.YxData.num; i++)
                {
                    listView1.Items[i].SubItems[4].Text = DataCollection.YxData.value[i];
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                for (int i = 0; i < DataCollection.YcData.num; i++)
                {
                    listView2.Items[i].SubItems[4].Text = DataCollection.YcData.value[i];
                }
            }
            else
            {
                ListViewItem lv;
                if (listView3.Items.Count < DataCollection.Event.addr.Count)
                {
                    for (int i = listView3.Items.Count; i < DataCollection.Event.addr.Count; i++)
                    {
                        lv = new ListViewItem();
                        lv.SubItems.Add(listView3.Items.Count.ToString());
                        lv.SubItems.Add(DataCollection.Event.name[i]);
                        lv.SubItems.Add(DataCollection.Event.addr[i]);
                        lv.SubItems.Add(DataCollection.Event.value[i]);
                        lv.SubItems.Add(DataCollection.Event.date[i]);
                        listView3.Items.Add(lv);
                    } 
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataCollection._ComTaskFlag.C_IC_NA_1 = true;
        }

        private void buttonMontrParam_Click(object sender, EventArgs e)
        {
            MotrParamFromMotr motrParamFromMotr = new MotrParamFromMotr();
            motrParamFromMotr.ShowDialog();
        }

        private void buttonIndtrParam_Click(object sender, EventArgs e)
        {
            IndtrParamFromMotr indtrParamFromMotr = new IndtrParamFromMotr();
            indtrParamFromMotr.ShowDialog(); 
        }



        private void button3_Click(object sender, EventArgs e)
        {
            DataCollection.linkState = 0;
            LinkStart linkStart = new LinkStart();
            linkStart.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DataCollection.class2Delay_default=int.Parse(textBox1.Text)*1000;
                DataCollection.class2Delay = DataCollection.class2Delay_default;
                timer1.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                timer1.Enabled = false;
            }
        }


        //定时器，发送总招
        private void timer1_Tick(object sender, EventArgs e)
        {
            DataCollection.class2Delay-=1000;
            if (DataCollection.class2Delay <= 0)
            {
                DataCollection._ComTaskFlag.C_IC_NA_1 = true;
                DataCollection.class2Delay = DataCollection.class2Delay_default;
            }
        }

        private void Monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timerRefresh.Enabled = false;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            refresh();
        }



   
    }
}
