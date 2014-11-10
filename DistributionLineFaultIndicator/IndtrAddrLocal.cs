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
    public partial class IndtrAddrLocal : Form
    {

        TextBox control;
        public IndtrAddrLocal()
        {
            InitializeComponent();
        }

        private void IndtrAddrLocal_Load(object sender, EventArgs e)
        {
            string[] row = { DataCollection.indtrAddrLocal[0].ToString(), DataCollection.indtrAddrLocal[1].ToString(), DataCollection.indtrAddrLocal[2].ToString(), DataCollection.indtrAddrLocal[3].ToString(),
                           DataCollection.indtrAddrLocal[4].ToString(),DataCollection.indtrAddrLocal[5].ToString(),DataCollection.indtrAddrLocal[6].ToString(),DataCollection.indtrAddrLocal[7].ToString(),
                           DataCollection.indtrAddrLocal[8].ToString()};
            //给dataGridView1控件添加数据
            dataGridView1.Rows.Add(row);    
            // 改变DataGridView1的第一列列头内容
            // 改变DataGridView1的第一行行头内容
            dataGridView1.Rows[0].HeaderCell.Value = "地址";
            // 改变DataGridView1的左上头部单元内容
            //dataGridView1.TopLeftHeaderCell.Value = "左上";



           

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9;i++ )
                DataCollection.indtrAddrLocal[i] = ushort.Parse((string)dataGridView1[i, 0].Value);
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void validating_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
                e.Handled = true;
        }



        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().BaseType.Name == "TextBox")
            {
                control = (TextBox)e.Control;
                //需要限制输入数字的单元格
                control.KeyPress += new KeyPressEventHandler(validating_KeyPress);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (int.Parse((string)e.FormattedValue) > 65535)
            {
                e.Cancel = true;
                MessageBox.Show("输入值超出限定！");
            }
                
                
        }

    }
}
