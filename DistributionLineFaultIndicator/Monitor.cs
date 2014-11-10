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
        MotrParamFromMotr motrParamFromMotr;
        IndtrParamFromMotr indtrParamFromMotr;
        public Monitor()
        {
            InitializeComponent();
        }

        //界面启动初始化
        private void Monitor_Load(object sender, EventArgs e)
        {
            timerRefresh.Enabled = true;
        }

        //标签页切换
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (comboBoxMon.Text == "")
                return;
            ListViewItem lv;
            //遥信
            if (tabControl1.SelectedIndex == 0)
            {
                listView1.Items.Clear();
                for (int i = 0; i < DataCollection.yxDatas[DataCollection.currentMon].num; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView1.Items.Count.ToString());
                    lv.SubItems.Add(DataCollection.yxDatas[DataCollection.currentMon].name[i]);
                    lv.SubItems.Add(DataCollection.yxDatas[DataCollection.currentMon].addr[i]);
                    lv.SubItems.Add(DataCollection.yxDatas[DataCollection.currentMon].value[i]);
                    listView1.Items.Add(lv);
                }
            }
            //遥测
            else if (tabControl1.SelectedIndex == 1)
            {
                listView2.Items.Clear();
                for (int i = 0; i < DataCollection.ycDatas[DataCollection.currentMon].num; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView2.Items.Count.ToString());
                    lv.SubItems.Add(DataCollection.ycDatas[DataCollection.currentMon].name[i]);
                    lv.SubItems.Add(DataCollection.ycDatas[DataCollection.currentMon].addr[i]);
                    lv.SubItems.Add(DataCollection.ycDatas[DataCollection.currentMon].value[i]);
                    listView2.Items.Add(lv);
                } 
            }
            //遥信变位事件
            else
            {
                listView3.Items.Clear();
                for (int i = 0; i < DataCollection.events[DataCollection.currentMon].addr.Count; i++)
                {
                    lv = new ListViewItem();
                    lv.SubItems.Add(listView3.Items.Count.ToString());
                    lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].name[i]);
                    lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].addr[i]);
                    lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].value[i]);
                    lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].date[i]);
                    listView3.Items.Add(lv);
                } 
            }
            
           
        }
        
        //显示刷新
        private void refresh()
        {
            if (comboBoxMon.Text == "")
                return;
            //遥信标签页
            if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < DataCollection.yxDatas[DataCollection.currentMon].num; i++)
                {
                    listView1.Items[i].SubItems[4].Text = DataCollection.yxDatas[DataCollection.currentMon].value[i];
                }
            }
            //遥测标签页
            else if (tabControl1.SelectedIndex == 1)
            {
                for (int i = 0; i < DataCollection.ycDatas[DataCollection.currentMon].num; i++)
                {
                    listView2.Items[i].SubItems[4].Text = DataCollection.ycDatas[DataCollection.currentMon].value[i];
                }
            }
            //遥信变位标签页
            else
            {
                ListViewItem lv;
                if (listView3.Items.Count < DataCollection.events[DataCollection.currentMon].addr.Count)
                {
                    for (int i = listView3.Items.Count; i < DataCollection.events[DataCollection.currentMon].addr.Count; i++)
                    {
                        lv = new ListViewItem();
                        lv.SubItems.Add(listView3.Items.Count.ToString());
                        lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].name[i]);
                        lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].addr[i]);
                        lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].value[i]);
                        lv.SubItems.Add(DataCollection.events[DataCollection.currentMon].date[i]);
                        listView3.Items.Add(lv);
                    } 
                }
                
            }
        }

        //刷新按钮
        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //总招按钮
        private void button1_Click(object sender, EventArgs e)
        {
            DataCollection._ComTaskFlag.C_IC_NA_1 = true;
        }

        //下设监视电源参数按钮
        private void buttonMontrParam_Click(object sender, EventArgs e)
        {
            if (motrParamFromMotr == null || motrParamFromMotr.IsDisposed)
            {
                motrParamFromMotr = new MotrParamFromMotr();
            }
            motrParamFromMotr.Show();
            motrParamFromMotr.Focus();
        }

        //下设指示器参数按钮
        private void buttonIndtrParam_Click(object sender, EventArgs e)
        {
            if (indtrParamFromMotr == null || indtrParamFromMotr.IsDisposed)
            {
                indtrParamFromMotr = new IndtrParamFromMotr();
            }
            indtrParamFromMotr.Show();
            indtrParamFromMotr.Focus();
        }


        //链路启动按钮
        private void button3_Click(object sender, EventArgs e)
        {
            DataCollection.linkState = 0;
            LinkStart linkStart = new LinkStart();
            linkStart.ShowDialog();
        }

        //循环总招选项框
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

        //窗口关闭
        private void Monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timerRefresh.Enabled = false;
        }

        //自动刷新定时器
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if(comboBoxMon.Text!="")
                refresh();
        }

        //右键菜单删除所有遥信变位事件选项
        private void ToolStripMenuItemDeleteAll_Click(object sender, EventArgs e)
        {
            if (comboBoxMon.Text == "")
                return;
            DataCollection.events[DataCollection.currentMon].date.RemoveRange(0, DataCollection.events[DataCollection.currentMon].addr.Count);
            DataCollection.events[DataCollection.currentMon].name.RemoveRange(0, DataCollection.events[DataCollection.currentMon].addr.Count);
            DataCollection.events[DataCollection.currentMon].value.RemoveRange(0, DataCollection.events[DataCollection.currentMon].addr.Count);
            DataCollection.events[DataCollection.currentMon].addr.RemoveRange(0, DataCollection.events[DataCollection.currentMon].addr.Count);  //因为前三句引用了DataCollection.Event.addr.Count，所以此句要放在最后
            listView3.Items.Clear();
        }


        //右键菜单删除选中遥信变位事件选项
        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxMon.Text == "")
                return;
            if (listView3.SelectedItems.Count == 0)
                return;
            ListView.SelectedListViewItemCollection selectedListViewItemCollection = listView3.SelectedItems;
            foreach (ListViewItem lv in selectedListViewItemCollection)
            {
                DataCollection.events[DataCollection.currentMon].name.Remove(lv.SubItems[2].Text);
                DataCollection.events[DataCollection.currentMon].addr.Remove(lv.SubItems[3].Text);
                DataCollection.events[DataCollection.currentMon].value.Remove(lv.SubItems[4].Text);
                DataCollection.events[DataCollection.currentMon].date.Remove(lv.SubItems[5].Text);
            }

            listView3.Items.Clear();
            refresh();
        }

        //循环总召间隔文本框变化
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DataCollection.class2Delay_default = int.Parse(textBox1.Text) * 1000;
                DataCollection.class2Delay = DataCollection.class2Delay_default;
                timer1.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                timer1.Enabled = false;
            }
        }

        private void ToolStripMenuItemReset_Click(object sender, EventArgs e)
        {
            if (comboBoxMon.Text == "")
                return;
            if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < DataCollection.yxDatas[DataCollection.currentMon].num; i++)
                {
                    DataCollection.yxDatas[DataCollection.currentMon].value[i] = "null";
                }
                refresh();
            }
            //遥测
            else if (tabControl1.SelectedIndex == 1)
            {
                for (int i = 0; i < DataCollection.ycDatas[DataCollection.currentMon].num; i++)
                {
                    DataCollection.ycDatas[DataCollection.currentMon].value[i] = "null";
                }
                refresh();
            }
        }
    }
}
