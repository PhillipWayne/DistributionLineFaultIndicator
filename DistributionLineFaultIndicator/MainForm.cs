using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using Excel=Microsoft.Office.Interop.Excel;



namespace DistributionLineFaultIndicator
{
    public partial class MainForm : Form
    {
        Monitor monitor = new Monitor();
        Indicator indicator = new Indicator();
        OpenFileDialog openF = new OpenFileDialog();
        SaveFileDialog saveF = new SaveFileDialog();

        FileInfo fileInfo;
        DirectoryInfo directoryInfo;

        string Openpath = @"";
        string Openpathname = @"";
        int DataTy = 0;


        LinkMonitor linkMonitor = new LinkMonitor();

        //Thread ThreadComSend1;
        //Thread ThreadComSend2;

        Thread threadConnect1;
        Thread threadConnect2;
        Thread threadSend1;
        Thread threadSend2;
        Thread threadRev1;
        Thread threadRev2;

        //public SerialPort serialPort1 = new SerialPort();
        //public SerialPort serialPort2 = new SerialPort();

        private static Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static Socket socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public MainForm()
        {
            InitializeComponent();
            DataCollection.initializeData();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;    //线程可以对其他线程创建的UI进行修改

        }

        private void toolStripButtonMonitor_Click(object sender, EventArgs e)
        {
            monitor.TopLevel = false;
            //monitor.FormBorderStyle = FormBorderStyle.None;
            monitor.Dock = DockStyle.Fill;

            this.splitContainer2.Panel1.Controls.Add(monitor);
            //this.splitContainer1.Panel2.Controls.Add(monitor);
            if (indicator.Visible == true)
                indicator.Hide();
            monitor.Show();
            toolStripButtonMonitor.Checked = true;
            toolStripButtonIndtr.Checked = false;

            //form2.Parent = this.panel1; // 子窗体的父容器
            //form2.Show();

        }

        private void toolStripButtonIndtr_Click(object sender, EventArgs e)
        {
            indicator.TopLevel = false;
            indicator.Dock = DockStyle.Fill;
            this.splitContainer2.Panel1.Controls.Add(indicator);
            if (monitor.Visible == true)
                monitor.Hide();
            indicator.Show();
            toolStripButtonMonitor.Checked = false;
            toolStripButtonIndtr.Checked = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openF.Filter = "System Files(*.sys)|*.sys|所有文件(*.*)|*.*";
            openF.InitialDirectory = System.Environment.CurrentDirectory;
            //string savesysfilepath = @"";
            if (openF.ShowDialog() == DialogResult.OK)
            {
                if (openF.SafeFileName != "config.sys")
                {
                    MessageBox.Show("选择的配置文件无效", "信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    fileInfo = new FileInfo(openF.FileName);
                    directoryInfo = new DirectoryInfo(fileInfo.DirectoryName);
                    Openpath = fileInfo.DirectoryName;
                    Openpathname = openF.FileName;

                    WriteReadAllFile.WriteReadParamIniFile(openF.FileName, 0);
                    
                    
                    foreach (int j in DataCollection.yxDatas.Keys)
                    {
                        Dictionary<string, string> nameMap = new Dictionary<string, string>();  //遥信数据名称和地址的映射
                        for (int i = 0; i < DataCollection.yxDatas[j].num; i++)
                        {
                            nameMap.Add(DataCollection.yxDatas[j].addr[i], DataCollection.yxDatas[j].name[i]);
                        }
                        DataCollection.findNameMap.Add(j, nameMap);
                    }

                    
                        toolStripButtonMonitor.Enabled = true;
                    //timerOpenProj.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("打开配置文件异常", "信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void toolStripButtonLink_Click(object sender, EventArgs e)
        {
            if (linkMonitor.ShowDialog() == DialogResult.OK)
            {
                //ProcessSerial();
                //ThreadComSend1 = new Thread(new ThreadStart(ComSendProc));
                //ThreadComSend1.Start();
                //serialPort1.DataReceived+=serialPort1_DataReceived;
                threadConnect1 = new Thread(new ThreadStart(socketConnect1));
                threadConnect1.Start();
            }
        }

        #region AboutCom
        /*
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int DataLength = serialPort1.BytesToRead;
            int i = 0;
            while (i < DataLength)
            {
                int len = serialPort1.Read(DataCollection._ComStructData.RXBuffer, i, DataLength);
                i += len;
            }
            DataCollection._ComStructData.RxLen = DataLength;
            ParseComReceData();
        }
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int DataLength = serialPort2.BytesToRead;
            int i = 0;
            while (i < DataLength)
            {
                int len = serialPort2.Read(DataCollection._ComStructData2.RXBuffer, i, DataLength);
                i += len;
            }
            DataCollection._ComStructData2.RxLen = DataLength;
            ParseComReceData2();
        }
        private void ComSendProc()
        {
            while (true)
            {
                if (DataCollection._ComStructData.TX_TASK)
                {
                    DataCollection._ComStructData.TX_TASK = false;
                    serialPort1.Write(DataCollection._ComStructData.TXBuffer, 0, DataCollection._ComStructData.TxLen);
                }
            }
        }
        private void ComSendProc2()
        {
            while (true)
            {
                if (DataCollection._ComStructData2.TX_TASK)
                {
                    DataCollection._ComStructData2.TX_TASK = false;
                    serialPort2.Write(DataCollection._ComStructData2.TXBuffer, 0, DataCollection._ComStructData2.TxLen);
                }
            }
        }
        private void ProcessSerial()       //开启与设置监测单元方向串口
        {
            serialPort1.PortName = DataCollection.Channel1.comID;
            serialPort1.BaudRate = Convert.ToInt32(DataCollection.Channel1.baud);
            if (DataCollection.Channel1.parity == "奇")
                serialPort1.Parity = Parity.Odd;
            else if (DataCollection.Channel1.parity == "偶")
                serialPort1.Parity = Parity.Even;
            else if (DataCollection.Channel1.parity == "无")
                serialPort1.Parity = Parity.None;
            if (DataCollection.Channel1.stopBits=="1")
                serialPort1.StopBits=StopBits.One;
            else if (DataCollection.Channel1.stopBits == "1.5")
                serialPort1.StopBits = StopBits.OnePointFive;
            else if (DataCollection.Channel1.stopBits == "2")
                serialPort1.StopBits = StopBits.Two;
            else if (DataCollection.Channel1.stopBits == "无")
                serialPort1.StopBits = StopBits.None;
            serialPort1.DataBits = 8;
            try
            {
                serialPort1.Open();
                toolStripButtonUnLink.Enabled = true;
                toolStripButtonLink.Enabled = false;
                toolStripStatusLabel1.Text = "监测单元：已连接";
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = true;
                timerComSend.Enabled = true;
            }
            catch (IOException ee)                  //异常 计算机无此串口
            {
                MessageBox.Show(ee.Message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ee)  //异常 串口被占用
            {
                MessageBox.Show(ee.Message, "信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void ProcessSerial2()       //开启与设置故障指示器方向串口
        {
            serialPort2.PortName = DataCollection.Channel2.comID;
            serialPort2.BaudRate = Convert.ToInt32(DataCollection.Channel2.baud);
            if (DataCollection.Channel2.parity == "奇")
                serialPort2.Parity = Parity.Odd;
            else if (DataCollection.Channel2.parity == "偶")
                serialPort2.Parity = Parity.Even;
            else if (DataCollection.Channel2.parity == "无")
                serialPort2.Parity = Parity.None;
            if (DataCollection.Channel2.stopBits == "1")
                serialPort2.StopBits = StopBits.One;
            else if (DataCollection.Channel2.stopBits == "1.5")
                serialPort2.StopBits = StopBits.OnePointFive;
            else if (DataCollection.Channel2.stopBits == "2")
                serialPort2.StopBits = StopBits.Two;
            else if (DataCollection.Channel2.stopBits == "无")
                serialPort2.StopBits = StopBits.None;
            serialPort2.DataBits = 8;
            try
            {
                serialPort2.Open();
                toolStripButtonUnLink2.Enabled = true;
                toolStripButtonLink2.Enabled = false;
                toolStripStatusLabel2.Text = "指示器单元：已连接";
                //DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = true;
                timerComSendIndtr.Enabled = true;
            }
            catch (IOException ee)                  //异常 计算机无此串口
            {
                MessageBox.Show(ee.Message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException ee)  //异常 串口被占用
            {
                MessageBox.Show(ee.Message, "信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }





*/
        #endregion


        private void toolStripButtonUnLink_Click(object sender, EventArgs e)
        {
            //serialPort1.Close();
            socket1.Close();
            if (threadConnect1 != null)
                threadConnect1.Abort();
            if (threadSend1 != null)
                threadSend1.Abort();
            if (threadRev1 != null)
                threadRev1.Abort();
            toolStripButtonLink.Enabled = true;
            toolStripButtonUnLink.Enabled = false;
            toolStripStatusLabel1.Text = "监测单元：已断开";
        }

        private void toolStripButtonUnLink2_Click(object sender, EventArgs e)
        {
            //serialPort2.Close();
            socket2.Close();
            if (threadConnect2 != null)
                threadConnect2.Abort();
            if (threadSend2 != null)
                threadSend2.Abort();
            if (threadRev2 != null)
                threadRev2.Abort();
            toolStripButtonLink2.Enabled = true;
            toolStripButtonUnLink2.Enabled = false;
            toolStripStatusLabel2.Text = "指示器单元：已断开";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("    版本号：v0.30      Released by 2014-11-12 ","软件版本",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void PtlComFrameProc()          //构造监测单元通信的一般报文
        {
            //if (!DataCollection._ComTaskFlag.FirstON_S)
            //{
            //    //	第一次开机处理
            //    DataCollection._ComTaskFlag.FirstON_S = true;		//	主机开机处理过标志(正常情况下该标志一直为1)
            //    DataCollection._ComTaskFlag.C_P1_NA_F = false;     //104U格式中STARTDT命令
            //    DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = false;
            //    return;
            //}


            if (DataCollection.ComTaskFlag.C_RQ_NA_LINKREQ_F == true)    //请求链路状态
            {
                DataCollection.ComTaskFlag.C_RQ_NA_LINKREQ_F = false;
                DataCollection.ComStructData.TxLen = 0;

                DataCollection.DataField.TXFieldLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(1);
                DataCollection.ComStructData.TX_TASK = true;

            }
            else if (DataCollection.ComTaskFlag.C_RQ_NA_LINKREQ_F1 == true)    //链路复位
            {
                DataCollection.ComTaskFlag.C_RQ_NA_LINKREQ_F1 = false;
                DataCollection.ComStructData.TxLen = 0;
                //DataCollection._ComStructData.TxLen = EncodeFrame(1, 0, 0, ref DataCollection._ComStructData.TXBuffer[0],ref DataCollection._DataField.TXBuffer[0]);
                DataCollection.DataField.TXFieldLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;
                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(2);
                DataCollection.ComStructData.TX_TASK = true;

            }
            else if (DataCollection.ComTaskFlag.C_IC_NA_1 == true)  //总召唤
            {
                DataCollection.ComTaskFlag.C_IC_NA_1 = false;
                DataCollection.ComStructData.TxLen = 0;

                DataCollection.DataField.TXFieldLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(10);

                DataCollection.ProtocoltyFlag.ZZFlag = false;
                DataCollection.ComStructData.TX_TASK = true;
                DataCollection.ComStructData.FCB = (!DataCollection.ComStructData.FCB);
                DataCollection.class2Delay = DataCollection.class2Delay_default;    //循环总招重新计时

            }
            else if(DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F== true)//链路请求确认
            {
                DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F = false;
                DataCollection.ComStructData.TxLen = 0;

                DataCollection.DataField.TXFieldLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(150);
                DataCollection.ComStructData.TX_TASK = true;
            }
            else if (DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F1 == true)//链路复位确认
            {
                DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F1 = false;
                DataCollection.ComStructData.TxLen = 0;

                DataCollection.DataField.TXFieldLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(151);
                DataCollection.ComStructData.TX_TASK = true;
            }

            else if (DataCollection.ComTaskFlag.C_CS_NA_1 == true)
            {

                DataCollection.ComTaskFlag.C_CS_NA_1 = false;
                DataCollection.ComStructData.TxLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;
                DataCollection.ZDYtype = 7;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(12);
                
                DataCollection.ComStructData.TX_TASK = true;

            }

            else if (DataCollection.ComTaskFlag.Comfirm == true)  //确认帧
            {

                DataCollection.ComTaskFlag.Comfirm = false;
                DataCollection.ComStructData.TxLen = 0;
                DataCollection.DataField.TXFieldVSQ = 1;

                DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(153);
                DataCollection.ComStructData.TX_TASK = true;

            }
            //else if (DataCollection._ComTaskFlag.YK_Sel_1_D == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Sel_1_D = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(15);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "选择<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "选择<合>";
            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.YK_Exe_1_D == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Exe_1_D = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(15);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "执行<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "执行<合>";
            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.YK_Cel_1_D == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Cel_1_D = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(16);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "撤销<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "撤销<合>";
            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.YK_Sel_1 == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Sel_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(17);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "选择<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "选择<合>";

            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.YK_Exe_1 == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Exe_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(17);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "执行<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "执行<合>";

            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.YK_Cel_1 == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Cel_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ParamInfoAddr = DataCollection.YkStartPos;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(18);

            //    if (DataCollection.YkState == 1)
            //        DataCollection.ComFrameMsg = "撤销<分>";
            //    else if (DataCollection.YkState == 2)
            //        DataCollection.ComFrameMsg = "撤销<合>";

            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
            //}
            //else if (DataCollection._ComTaskFlag.VERSION_1 == true)    //读版本号
            //{
            //    DataCollection._ComTaskFlag.VERSION_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 10, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //DataCollection._DataField.TXFieldLen, 1, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.TXFieldLen = 0;
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 5;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(5, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);
            //    DataCollection.ComFrameMsg = "请求版本号";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.CALLTIME_1 == true)
            //{
            //    DataCollection._ComTaskFlag.CALLTIME_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    DataCollection._DataField.TXFieldLen = 0;
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 6;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(6, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "请求时间";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.READ_P_1 == true)
            //{
            //    DataCollection._ComTaskFlag.READ_P_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    DataCollection.ZDYtype = 2;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(2, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "读参数";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.SET_PARAM_CON == true)  //设置参数
            //{
            //    DataCollection._ComTaskFlag.SET_PARAM_CON = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 8, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                         DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection.ZDYtype = 1;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(1, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "设置";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}

            //else if (DataCollection._ComTaskFlag.AloneCallYx_1 == true)
            //{
            //    DataCollection._ComTaskFlag.AloneCallYx_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 12, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                          DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(39);
            //    DataCollection.ComFrameMsg = "请求遥信";
            //    //发送单招
            //    DataCollection._ChangeFlag.AlongCall = true;
            //    AlongCallTime = 0;

            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ThreadIndex.ComThreadID = 11;
            //}
            //else if (DataCollection._ComTaskFlag.AloneCallYc_1 == true)
            //{
            //    DataCollection._ComTaskFlag.AloneCallYc_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 13, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                          DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(38);
            //    DataCollection.ComFrameMsg = "请求遥测";
            //    //发送单招
            //    DataCollection._ChangeFlag.AlongCall = true;
            //    AlongCallTime = 0;

            //    DataCollection._ComStructData.TX_TASK = true;
            //    DataCollection._ThreadIndex.ComThreadID = 12;

            //}
            //else if (DataCollection._ComTaskFlag.Reset_1 == true)
            //{
            //    DataCollection._ComTaskFlag.Reset_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 17, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                          DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(13);

            //    DataCollection.ComFrameMsg = "复位";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_Start_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_Start_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                              DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "升级";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMStart_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMStart_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                              DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "ARM升级";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}

            //else if (DataCollection._ComTaskFlag.UpdateCode_JY_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_JY_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //{
            //    //    DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                              DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    //}
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                             DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                             DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "校验";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMJY_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMJY_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //{
            //    //    DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                              DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    //}
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                             DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                             DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "ARM校验";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}

            //else if (DataCollection._ComTaskFlag.UpdateCode_OK_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_OK_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;


            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                             DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                            DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                            DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "更新代码";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMOK_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMOK_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;


            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                             DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                            DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                            DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "ARM更新代码";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}


            //else if (DataCollection._ComTaskFlag.CallRecordData == true)
            //{
            //    DataCollection._ComTaskFlag.CallRecordData = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 20, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.TXBuffer[0],
            //    //                                          DataCollection._DataField.TXFieldLen, DataCollection._DataField.TXFieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection.ZDYtype = 4;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(4, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);
            //    DataCollection.ComFrameMsg = "请求记录数据";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}


            //else if (DataCollection._ComTaskFlag.READ_Hard_1 == true)
            //{

            //    DataCollection._ComTaskFlag.READ_Hard_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(37);
            //    DataCollection._DataField.TXFieldLen = 0;
            //    DataCollection._DataField.TXFieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 8;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(8, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.TXBuffer,
            //                                              DataCollection._DataField.TXFieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.TXFieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "请求硬件状态";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.Transmit == true)
            //{

            //    DataCollection._ComTaskFlag.Transmit = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(40);

            //    DataCollection.ComFrameMsg = "数据转发";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            else if (DataCollection.ComTaskFlag.CALL_1 == true)  //召唤一级数据
            {

                DataCollection.ComTaskFlag.CALL_1 = false;
               
                    DataCollection.ComStructData.TxLen = 0;

                    DataCollection.DataField.TXFieldLen = 0;
                    DataCollection.DataField.TXFieldVSQ = 1;

                    DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(3);

                    DataCollection.ComStructData.TX_TASK = true;
                    DataCollection.ComStructData.FCB = (!DataCollection.ComStructData.FCB);
                    //    DataCollection._ThreadIndex.ComThreadID = 9;
               
            }
            else if (DataCollection.ComTaskFlag.CALL_2 == true)  //召唤二级数据
            {
                
                    DataCollection.ComTaskFlag.CALL_2 = false;
                    DataCollection.ComStructData.TxLen = 0;

                    DataCollection.DataField.TXFieldLen = 0;
                    DataCollection.DataField.TXFieldVSQ = 1;

                    DataCollection.ComStructData.TxLen = Protocolty101.EncodeFrame(4);

                    DataCollection.ComStructData.TX_TASK = true;
                    DataCollection.ComStructData.FCB = (!DataCollection.ComStructData.FCB);
                    //     DataCollection._ThreadIndex.ComThreadID = 9;
                
            }

        }


        private void PtlComFrameProc2() //构造与故障指示器方向通信的报文
        {

        }


        private void toolStripButtonLink2_Click(object sender, EventArgs e)
        {
            LinkIndicator linkIndicator = new LinkIndicator();
            if (linkIndicator.ShowDialog() == DialogResult.OK)
            {
                //ProcessSerial2();
                //ThreadComSend2 = new Thread(new ThreadStart(ComSendProc2));
                //ThreadComSend2.Start();
                //serialPort2.DataReceived += serialPort2_DataReceived;
                threadConnect2 = new Thread(new ThreadStart(socketConnect2));
                threadConnect2.Start();

            }

        }


        private void socketConnect1()           //监测单元通讯连接函数
        {
            for (int i = 0; i < 20;i++ )//重连20次
            {
                try
                {
                    toolStripStatusLabel1.Text = "监测单元：尝试连接中";
                    toolStripButtonLink.Enabled = false;
                    toolStripButtonUnLink.Enabled = true;
                    socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket1.Connect(IPAddress.Parse(DataCollection.Channel1.ip), Convert.ToInt16(DataCollection.Channel1.port));
                    threadSend1 = new Thread(new ThreadStart(dataSend1));
                    threadSend1.Start();
                    threadRev1 = new Thread(new ThreadStart(dataRev1));
                    threadRev1.Start();
                    toolStripStatusLabel1.Text = "监测单元：已连接";
                    return;
                }
                catch
                {
                    socket1.Close();
                    if (threadSend1 != null)
                        threadSend1.Abort();
                    if (threadRev1 != null)
                        threadRev1.Abort();
                }
                Thread.Sleep(3000);
            }
            socket1.Close();
            if (threadSend1 != null)
                threadSend1.Abort();
            if (threadRev1 != null)
                threadRev1.Abort();
            toolStripButtonLink.Enabled = true;
            toolStripButtonUnLink.Enabled = false;
            toolStripStatusLabel1.Text = "监测单元：连接失败";
        }

        private void socketConnect2()           //故障指示器通讯连接函数
        {
            try
            {
                toolStripStatusLabel2.Text = "指示器单元：尝试连接中";
                toolStripButtonLink2.Enabled = false;
                toolStripButtonUnLink2.Enabled = true;
                socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket2.Connect(IPAddress.Parse(DataCollection.Channel2.ip), Convert.ToInt16(DataCollection.Channel2.port));
                threadSend2 = new Thread(new ThreadStart(dataSend2));
                threadSend2.Start();
                threadRev2 = new Thread(new ThreadStart(dataRev2));
                threadRev2.Start();
                toolStripStatusLabel2.Text = "指示器单元：已连接";
            }
            catch
            {
                socket2.Close();
                if (threadSend2 != null)
                    threadSend2.Abort();
                if (threadRev2 != null)
                    threadRev2.Abort();
                toolStripButtonLink2.Enabled = true;
                toolStripButtonUnLink2.Enabled = false;
                toolStripStatusLabel2.Text = "指示器单元：连接失败";
                threadConnect2.Abort();
            }

        }

        //监测单元网口发送函数
        private void dataSend1()
        {
            try 
            {
                while (true)    //处理事物
                {
                    PtlComFrameProc();
                    if (DataCollection.ComStructData.TX_TASK == true)
                    {

                        DataCollection.ComStructData.TX_TASK = false;
                        socket1.Send(DataCollection.ComStructData.TXBuffer, 0, DataCollection.ComStructData.TxLen, SocketFlags.None);
                        if (checkBoxRoll.Checked == false)
                        {
                            outputDisplay(1, DataCollection.ComStructData.TxLen);//打印输出发送报文
                        }
                    }
                    Thread.Sleep(20);
                }
            }
            catch
            {
                toolStripStatusX.Text = "监测单元方向报文发送错误";
                socket1.Close();
                if (threadConnect1 != null)
                    threadConnect1.Abort();
                if (threadRev1 != null)
                    threadRev1.Abort();
                toolStripButtonLink.Enabled = true;
                toolStripButtonUnLink.Enabled = false;
                toolStripStatusLabel1.Text = "监测单元：已断开";
                threadSend1.Abort();
            }
        }

        //监测单元网口接收函数
        private void dataRev1()
        {
            try
            {
                while (true)
                {
                    int x=socket1.Available;
                    DataCollection.ComStructData.RxLen = socket1.Receive(DataCollection.ComStructData.RXBuffer);
                    if (DataCollection.ComStructData.RxLen == 0)  //远方主动正常断开
                    {
                        socket1.Close();
                        if (threadConnect1 != null)
                            threadConnect1.Abort();
                        if (threadSend1 != null)
                            threadSend1.Abort();
                        toolStripButtonLink.Enabled = true;
                        toolStripButtonUnLink.Enabled = false;
                        toolStripStatusLabel1.Text = "监测单元：已断开";
                        threadRev1.Abort();
                    }
                    //if (DataCollection._ComStructData.RXBuffer[0] == 0x69&&DataCollection._ComStructData.RXBuffer[3]==0x69
                    //    && (DataCollection._ComStructData.RXBuffer[1] + (DataCollection._ComStructData.RXBuffer[2] >> 8)) == (DataCollection._ComStructData.RxLen-6))//是否为69开头的单帧自定义报文
                    //{
                    //    ProtocoltyParam.DecodeFrame();
                    //    outputDisplay(2, DataCollection._ComStructData.RxLen);//打印输出接收报文
                    //}
                    //else if((DataCollection._ComStructData.RXBuffer[0] == 0x10&&DataCollection._ComStructData.RxLen==5&&DataCollection.linklen==1)
                    //    ||(DataCollection._ComStructData.RXBuffer[0] == 0x10&&DataCollection._ComStructData.RxLen==6&&DataCollection.linklen==2))//是否为10开头的101单帧固定长报文
                    //{
                    //    DataTy = Protocolty101.DecodeFrame();
                    //    System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                    //    processRevTele(DataTy);
                    //    outputDisplay(2, DataCollection._ComStructData.RxLen);//打印输出接收报文
                    //}
                    //else if (DataCollection._ComStructData.RXBuffer[0] == 0x68 && DataCollection._ComStructData.RXBuffer[3] == 0x68 && DataCollection._ComStructData.RXBuffer[1] == DataCollection._ComStructData.RXBuffer[2]
                    //    && DataCollection._ComStructData.RXBuffer[1] + 6 == DataCollection._ComStructData.RxLen)//是否为68开头的101单帧报文
                    //{
                    //    DataTy = Protocolty101.DecodeFrame();
                    //    System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                    //    processRevTele(DataTy);
                    //    outputDisplay(2, DataCollection._ComStructData.RxLen);//打印输出接收报文
                    //}

                    //解析服务器发来的检测单元上线信息
                    int len = DataCollection.ComStructData.RxLen;
                    if (DataCollection.ComStructData.RXBuffer[0]==0x77)//判断是否为服务器发来的监测单元上下线通知
                    {
                        int num = len / 4;
                        for (int i = 0; i < num; i++)
                        {
                            if (DataCollection.ComStructData.RXBuffer[3 + i * 4] == 0x11)//上线通知
                            {
                                if (!DataCollection.onLineMon.Contains(DataCollection.ComStructData.RXBuffer[1 + i * 4]))
                                {
                                    DataCollection.onLineMon.Add(DataCollection.ComStructData.RXBuffer[1 + i * 4] + (DataCollection.ComStructData.RXBuffer[2 + i * 4]<<8));
                                }
                            }
                            else if (DataCollection.ComStructData.RXBuffer[3 + i * 4] == 0x22)//下线通知
                            {
                                if (DataCollection.onLineMon.Contains(DataCollection.ComStructData.RXBuffer[1 + i * 4]))
                                {
                                    DataCollection.onLineMon.Remove(DataCollection.ComStructData.RXBuffer[1 + i * 4] + (DataCollection.ComStructData.RXBuffer[2 + i * 4] << 8));
                                }
                            }
                        } 
                        continue;
                    }

                    
                    //可解析多帧的方案
                    while (len != 0)
                    {
                        int oldlen = len;
                        if (DataCollection.ComStructData.RXBuffer[0] == 0x69 && DataCollection.ComStructData.RXBuffer[3] == 0x69)//检测到69开头的自定义帧
                        {
                            int lenTele = DataCollection.ComStructData.RXBuffer[1] + (DataCollection.ComStructData.RXBuffer[2] >> 8) + 6;
                            if (lenTele > len)
                            {
                                toolStripStatusX.Text = "监测单元方向报文接收传输错误";
                                break;
                            }
                            DataCollection.ComStructData.RxLen = lenTele;
                            ProtocoltyParam.DecodeFrame();
                            outputDisplay(2, lenTele);//打印输出接收报文
                            byte[] temp = new byte[len - lenTele];
                            //复制保存余下报文
                            Array.Copy(DataCollection.ComStructData.RXBuffer, lenTele, temp, 0, len - lenTele);
                            //删除已解析的第一帧报文，拷贝余下报文从第0位开始（左移余下报文）
                            Array.Copy(temp, 0, DataCollection.ComStructData.RXBuffer, 0, temp.Length);
                            len -= lenTele;
                        }
                        else if (DataCollection.ComStructData.RXBuffer[0] == 0x10 && DataCollection.ComStructData.RXBuffer[4] == 0x16 && DataCollection.linklen == 1)//检测到10开头的101固定帧，并且链路长度为1
                        {
                            if (len < 5)
                            {
                                toolStripStatusX.Text = "监测单元方向报文接收传输错误";
                                break;
                            }
                            DataCollection.ComStructData.RxLen = 5;
                            DataTy = Protocolty101.DecodeFrame();
                            System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                            processRevTele(DataTy);
                            outputDisplay(2, 5);//打印输出接收报文
                            byte[] temp = new byte[len - 5];
                            //复制保存余下报文
                            Array.Copy(DataCollection.ComStructData.RXBuffer, 5, temp, 0, len - 5);
                            //删除已解析的第一帧报文，拷贝余下报文从第0位开始（左移余下报文）
                            Array.Copy(temp, 0, DataCollection.ComStructData.RXBuffer, 0, temp.Length);
                            len -= 5;
                        }
                        else if (DataCollection.ComStructData.RXBuffer[0] == 0x10 && DataCollection.ComStructData.RXBuffer[5] == 0x16 && DataCollection.linklen == 2)//检测到10开头的101固定帧，并且链路长度为2
                        {
                            if (len < 6)
                            {
                                toolStripStatusX.Text = "监测单元方向报文接收传输错误";
                                break;
                            }
                            DataCollection.ComStructData.RxLen = 6;
                            DataTy = Protocolty101.DecodeFrame();
                            System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                            processRevTele(DataTy);
                            outputDisplay(2, 6);//打印输出接收报文
                            byte[] temp = new byte[len - 6];
                            //复制保存余下报文
                            Array.Copy(DataCollection.ComStructData.RXBuffer, 6, temp, 0, len - 6);
                            //删除已解析的第一帧报文，拷贝余下报文从第0位开始（左移余下报文）
                            Array.Copy(temp, 0, DataCollection.ComStructData.RXBuffer, 0, temp.Length);
                            len -= 6;
                        }
                        else if (DataCollection.ComStructData.RXBuffer[0] == 0x68 && DataCollection.ComStructData.RXBuffer[3] == 0x68)//检测到68开头的101帧
                        {
                            int lenTele = DataCollection.ComStructData.RXBuffer[1] + 6;
                            if (lenTele > len)
                            {
                                toolStripStatusX.Text = "监测单元方向报文接收传输错误";
                                break;
                            }
                            DataCollection.ComStructData.RxLen = lenTele;
                            DataTy = Protocolty101.DecodeFrame();
                            System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                            processRevTele(DataTy);
                            outputDisplay(2, lenTele);//打印输出接收报文
                            byte[] temp = new byte[len - lenTele];
                            //复制保存余下报文
                            Array.Copy(DataCollection.ComStructData.RXBuffer, lenTele, temp, 0, temp.Length);
                            //删除已解析的第一帧报文，拷贝余下报文从第0位开始（左移余下报文）
                            Array.Copy(temp, 0, DataCollection.ComStructData.RXBuffer, 0, temp.Length);
                            len -= lenTele;
                        }
                        if (len == oldlen)//防止无法解析的错误报文导致的死循环
                            break;
                    }
                }
            }
            catch
            {
                toolStripStatusX.Text = "监测单元方向报文接收错误";
                socket1.Close();
                if (threadConnect1 != null)
                    threadConnect1.Abort();
                if (threadSend1 != null)
                    threadSend1.Abort();
                toolStripButtonLink.Enabled = true;
                toolStripButtonUnLink.Enabled = false;
                toolStripStatusLabel1.Text = "监测单元：已断开";
                threadRev1.Abort();
            }
        }

        private void dataSend2()
        {
            while (true)    //处理事物
            {
                PtlComFrameProc2();
                if (DataCollection.ComStructData2.TX_TASK == true)
                {
                    try
                    {
                        DataCollection.ComStructData2.TX_TASK = false;
                        socket2.Send(DataCollection.ComStructData2.TXBuffer, 0, DataCollection.ComStructData2.TxLen, SocketFlags.None);
                    }
                    catch
                    {
                        toolStripStatusX.Text = "指示器方向报文发送错误";
                    }

                }
                else
                {
                }
                Thread.Sleep(5);
            }
        }

        private void dataRev2()
        {

        }

        private void processRevTele(int DataTy)   //根据收到的报文，跟新数据内存
        {
;
            int DevAddr = 0;
            byte TargetBoard = 0, seqflag = 0, seq = 0, SQflag = 0;

            if (DataTy == 2)  //链路状态正常
            {
                DataCollection.linkState = 1;
                DataCollection.ComTaskFlag.C_RQ_NA_LINKREQ_F1 = true;  //链路复位
                return;
            }
            else if (DataTy == 3)  //复位链路确认
            {
                if(DataCollection.linkState == 1)//区分链路确认与一般确认
                    DataCollection.linkState = 2;
                return;

                //if (DataCollection._ProtocoltyFlag.ZZFirstFlag == true)
                //{
                //    DataCollection._ProtocoltyFlag.ZZFirstFlag = false;//清第一次总召标志
                //    DataCollection._ProtocoltyFlag.ZZFlag = true;//发送总召唤标志
                //    DataCollection._ComTaskFlag.C_IC_NA_1 = true;  //发总召唤

                //    //ProtoZZTime = DataCollection._ProtocoltyFlag.ZZTime;//设置总召唤循环时间
                //}

            }
            //else if (DataTy == 4)     //  总召激活
            //{
            //    DataCollection.ComFrameMsg = "总召激活";

            //}
            //else if (DataTy == 5)     //  总召结束
            //{
            //    DataCollection.ComFrameMsg = "总召结束";

            //}
            //else if (DataTy == 35)     //  遥测（单独召测) 短浮点型，5字节
            //{
            //    DataCollection.ComFrameMsg = "遥测";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 40)     //   短浮点型，5字节
            //{
            //    DataCollection.ComFrameMsg = "遥测";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 36)     // 带品质描述归一化值 ，3字节
            //{
            //    DataCollection.ComFrameMsg = "遥测";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 38)     // 带品质描述归一化值 ，3字节
            //{
            //    DataCollection.ComFrameMsg = "遥测";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 42)     //不带品质描述归一化值，2字节
            //{
            //    DataCollection.ComFrameMsg = "遥测";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 41)     //           8字节
            //{
            //    DataCollection.ComFrameMsg = "扰动事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.RaoDongEvent = true;
            //    //DataCollection._FrameTime.T2 = 10;
            //}
            //else if (DataTy == 37)     //          6字节
            //{
            //    DataCollection.ComFrameMsg = "扰动事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.RaoDongEvent = true;
            //    //DataCollection._FrameTime.T2 = 10;
            //}
            //else if (DataTy == 39)     //          6字节
            //{
            //    DataCollection.ComFrameMsg = "扰动事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.RaoDongEvent = true;
            //    //DataCollection._FrameTime.T2 = 10;
            //}
            //else if (DataTy == 43)  //         5字节
            //{
            //    DataCollection.ComFrameMsg = "扰动事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.RaoDongEvent = true;
            //    //DataCollection._FrameTime.T2 = 10;
            //}

            //else if (DataTy == 50)    //遥信（单独召测)
            //{
            //    DataCollection.ComFrameMsg = "遥信";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 51)    //正常遥信
            //{
            //    DataCollection.ComFrameMsg = "遥信";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 52)     //遥信变位
            //{
            //    DataCollection.ComFrameMsg = "变位事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.YxBianWeiOfNoTimeEvent = true;

            //}
            //else if (DataTy == 53)    //正常双点遥信
            //{
            //    DataCollection.ComFrameMsg = "遥信";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 54)     //双点变位事件
            //{
            //    DataCollection.ComFrameMsg = "变位事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.YxBianWeiOfNoTimeEvent = true;

            //}
            //else if (DataTy == 56)
            //{
            //    DataCollection.ComFrameMsg = "变位事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.YxBianWeiOfTimeEvent = true;
            //}
            //else if (DataTy == 58)
            //{
            //    DataCollection.ComFrameMsg = "变位事件";
            //    DataCollection._ChangeFlag.ChangInfoViewUpdate = true;
            //    DataCollection._Message.YxBianWeiOfTimeEvent = true;
            //}
            //else if (DataTy == 10)     //
            //{
            //    DataCollection.ComFrameMsg = "选择应答";
            //    DataCollection._Message.YkDocmentView = true;
            //}
            //else if (DataTy == 11)     //
            //{
            //    DataCollection.ComFrameMsg = "执行成功";
            //    DataCollection._Message.YkDocmentView = true;
            //}
            //else if (DataTy == 12)     //
            //{
            //    DataCollection.ComFrameMsg = "遥控撤销（确认）";
            //    DataCollection._Message.YkDocmentView = true;
            //}
            //else if (DataTy == 7)
            //{
            //    DataCollection.ComFrameMsg = "复位确认";

            //}

            //else if (DataTy == 100)     //
            //{
            //    DataCollection.ComFrameMsg = "参数设置（确认）";
            //    DataCollection._Message.ParamAck = true;

            //}
            //else if (DataTy == 101)     //
            //{
            //    DataCollection.ComFrameMsg = "参数设置（否认）";

            //}
            //else if (DataTy == 102)   //
            //{
            //    DataCollection.ComFrameMsg = "参数读取";
            //    DataCollection._Message.ReadParam = true;
            //}
            //else if (DataTy == 103)  //升级
            //{
            //    if (DataCollection.ParamInfoAddr < 1000)
            //        DataCollection.ComFrameMsg = "升级(应答)";
            //    else if (DataCollection.ParamInfoAddr == 1000)
            //    {
            //        DataCollection.ComFrameMsg = "校验(应答)";
            //        DataCollection._Message.CodeUpdateJY = true;
            //    }
            //}
            //else if (DataTy == 104)  //记录
            //{
            //    DataCollection.ComFrameMsg = "历史记录";
            //    DataCollection._Message.CallHisDataDocmentView = true;
            //}
            //else if (DataTy == 105)     //版本号
            //{
            //    DataCollection.ComFrameMsg = "版本号";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 106)
            //{
            //    DataCollection.ComFrameMsg = "时间";  //时间
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 108)   //器件状态
            //{
            //    DataCollection.ComFrameMsg = "器件状态";
            //    DataCollection._Message.CallDataDocmentView = true;
            //}
            //else if (DataTy == 109)  //升级
            //{
            //    if (DataCollection.ParamInfoAddr < 1000)
            //        DataCollection.ComFrameMsg = "升级(应答)";
            //    else if (DataCollection.ParamInfoAddr == 1000)
            //    {
            //        DataCollection.ComFrameMsg = "校验(应答)";
            //        DataCollection._Message.CodeUpdateJY = true;
            //    }
            //}
            //else if (DataTy == 29)     //
            //{
            //    DataCollection.ComFrameMsg = "转发回复";

            //}
            //else if (DataTy == 114)
            //{
            //    DataCollection.ComFrameMsg = "无所召唤的数据";

            //}
            //else if (DataTy == 115)
            //{
            //    DataCollection.ComFrameMsg = "以数据响应";

            //}
          
                //if (DataCollection._ProtocoltyFlag.ACD == 1)
                //{

                //    DataCollection._ComTaskFlag.CALL_1 = true;
                //}
                //else if (DataCollection._ProtocoltyFlag.ACD == 2)
                //{

                //    DataCollection._ComTaskFlag.CALL_2 = true;
                //}



                if (DataTy == 150)  //收到下位机链路状态请求
                {
                    DataCollection.linkState = 3;
                    DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F = true;
                    return;
                }
                else if (DataTy == 151)  //收到下位机链路复位请求
                {
                    DataCollection.linkState = 4;
                    DataCollection.ComTaskFlag.C_RQ_NA_LINKCOM_F1 = true;
                    return;
                }
                else if (DataTy == 152)  //收到下位机初始化完成确认
                {
                    DataCollection.linkState = 5;
                    return;
                }



                if (DataTy == 6)
                {
                    if (DataCollection.ComStructData.RXBuffer[7 + DataCollection.linklen] == 7)//对时成功
                        DataCollection.montrParamState = 1;
                    else
                        DataCollection.montrParamState = 2;
                    return;
                }


            byte[] bytes = new byte[4];
            int data = 0;
            int StartPos = 0;
            int index = 0;
            string DataInfo;

            switch (DataTy)
            {
                case 51:     //单点遥信响应站召唤，类型标识1  
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection.DataField.Buffer[2] << 16) +DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);    
                    if(DataCollection.inflen==2)
                        StartPos = DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);

                    for (int j = 0; j < DataCollection.DataField.FieldVSQ; j++)
                    {

                        if (DataCollection.inflen == 3)
                        {
                            if ((DataCollection.DataField.Buffer[j + 3]&0x01) == 0)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                        }
                        if (DataCollection.inflen == 2)
                        {
                            if ((DataCollection.DataField.Buffer[j + 2] & 0x01) == 0)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                        }
                        StartPos++;
                    }
                    break;

                case 52:     //突发，单点遥信变位，类型标识1
                    for (int i = 0; i < DataCollection.DataField.FieldVSQ; i++)
                    {
                        if (DataCollection.inflen == 3) 
                        { 
                            StartPos = (DataCollection.DataField.Buffer[index + 2] << 16) + DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);
                            if ((DataCollection.DataField.Buffer[index + 3] & 0x01) == 0)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                            index += 4;
                        }
                        else if (DataCollection.inflen == 2)
                        {
                            StartPos = DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);
                            if ((DataCollection.DataField.Buffer[index + 2] & 0x01) == 0)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                            index += 3;
                        }
                    }
                    DataCollection.ComTaskFlag.Comfirm = true;
                    break;

                case 36:         //归一化遥测响应站召唤,类型标识9
                    if (DataCollection.inflen == 2)
                        StartPos = DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection.DataField.Buffer[2] << 16) + DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);  
                    for (int j = 0; j < DataCollection.DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            data = DataCollection.DataField.Buffer[index + 2] + (DataCollection.DataField.Buffer[index + 3] << 8);

                        }
                        if (DataCollection.inflen == 3)
                        {
                            data = DataCollection.DataField.Buffer[index + 3] + (DataCollection.DataField.Buffer[index + 4] << 8);
                        }
                        if (data > 0x8000)
                            data = data - 65536;
                        DataCollection.ycDatas[DataCollection.linkAddr].value[StartPos - 0x4001] = Convert.ToString(data);
                        StartPos++;
                        index += 3;
                    }
                    break;

                case 37:        //突发，归一化遥测扰动,类型标识9
                    for (int j = 0; j < DataCollection.DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            StartPos += DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);

                            data = DataCollection.DataField.Buffer[index + 2] + (DataCollection.DataField.Buffer[index + 3] << 8);
                            index += 5;
                            if (data > 0x8000)
                                data = data - 65536;
                            DataCollection.ycDatas[DataCollection.linkAddr].value[StartPos - 0x4001] = Convert.ToString(data);
                        }
                        else if (DataCollection.inflen == 3)
                        {
                            StartPos = DataCollection.DataField.Buffer[index + 2];
                            StartPos = StartPos << 16;
                            StartPos += DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);

                            data = DataCollection.DataField.Buffer[index + 3] + (DataCollection.DataField.Buffer[index + 4] << 8);
                            index += 6;
                            if (data > 0x8000)
                                data = data - 65536;
                            DataCollection.ycDatas[DataCollection.linkAddr].value[StartPos - 0x4001] = Convert.ToString(data);
                        }
                    }
                    DataCollection.ComTaskFlag.Comfirm = true;
                    break;

                case 53:     //双点遥信响应站召唤，类型标识3
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection.DataField.Buffer[2] << 16) +DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);    
                    if(DataCollection.inflen==2)
                        StartPos = DataCollection.DataField.Buffer[0] + (DataCollection.DataField.Buffer[1] << 8);
                    System.Diagnostics.Debug.WriteLine("FieldVSQ: " + DataCollection.DataField.FieldVSQ);
                    for (int j = 0; j < DataCollection.DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            System.Diagnostics.Debug.WriteLine(String.Format("DataCollection._DataField.Buffer{0:d}: ",j + 2) + DataCollection.DataField.Buffer[j + 2].ToString("X2"));
                            System.Diagnostics.Debug.WriteLine("StartPos - 1:" + (StartPos - 1));
                            if ((DataCollection.DataField.Buffer[j + 2] & 0x03) == 1)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";

                            
                            
                        }
                        if (DataCollection.inflen == 3)
                        {
                            if ((DataCollection.DataField.Buffer[j + 3] & 0x03) == 1)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                        }
                        StartPos++;
                    }
                    break;

                case 54:    //突变，双点遥信，类型标识3
                    for (int i = 0; i < DataCollection.DataField.FieldVSQ; i++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            StartPos += DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);
                            if ((DataCollection.DataField.Buffer[index + 2] & 0x03) == 1)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                            index += 3;
                        }
                        else if (DataCollection.inflen == 3)
                        {
                            StartPos = DataCollection.DataField.Buffer[index + 2];
                            StartPos = StartPos << 16;
                            StartPos += DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8);
                            if ((DataCollection.DataField.Buffer[index + 3]& 0x03) == 1)
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "分";
                            else
                                DataCollection.yxDatas[DataCollection.linkAddr].value[StartPos - 1] = "合";
                            index += 4;
                        }
                    }
                    DataCollection.ComTaskFlag.Comfirm = true;
                    break;
                case 56://遥信变位，类型标示30，传送原因3

                    
                    for (int i = 0; i < DataCollection.DataField.FieldVSQ;i++ )
                    {
                        if (DataCollection.inflen == 2)
                        {
                            StartPos = DataCollection.DataField.Buffer[index] +(DataCollection.DataField.Buffer[index+1] << 8);
                            if (DataCollection.findNameMap[DataCollection.linkAddr].ContainsKey(StartPos.ToString()))
                            {
                                DataCollection.events[DataCollection.linkAddr].name.Add(DataCollection.findNameMap[DataCollection.linkAddr][StartPos.ToString()]);
                            }
                            else
                            {
                                DataCollection.events[DataCollection.linkAddr].name.Add("未知");
                            }

                            DataCollection.events[DataCollection.linkAddr].addr.Add(StartPos.ToString());


                            if (DataCollection.DataField.Buffer[index+2] == 0)
                                DataCollection.events[DataCollection.linkAddr].value.Add("分");
                            else
                                DataCollection.events[DataCollection.linkAddr].value.Add("合");

                            DataInfo = "";
                            DataInfo = String.Format("{0:d}", DataCollection.DataField.Buffer[index+9]&0x7f);   //年
                            DataInfo += "年";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index+8]&0x0f);  //月
                            DataInfo += "月";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index+7] & 0x1f);  //日+星期
                            DataInfo += "日";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index+6]&0x1f);  //时
                            DataInfo += "时";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index + 5] & 0x3f);  //分
                            DataInfo += "分";
                            int ms = (DataCollection.DataField.Buffer[index+4] << 8)
                                      + DataCollection.DataField.Buffer[index+3];

                            DataInfo += String.Format("{0:d}", ms/1000);
                            DataInfo += "秒";
                            DataInfo += String.Format("{0:d}", ms%1000);
                            DataInfo += "毫秒";
                            DataCollection.events[DataCollection.linkAddr].date.Add(DataInfo);
                            index += 10;
                        }
                        else if (DataCollection.inflen == 3)
                        {
                            StartPos = DataCollection.DataField.Buffer[index] + (DataCollection.DataField.Buffer[index + 1] << 8)
                                + (DataCollection.DataField.Buffer[index + 2] << 16);
                            if (DataCollection.findNameMap[DataCollection.linkAddr].ContainsKey(StartPos.ToString()))
                            {
                                DataCollection.events[DataCollection.linkAddr].name.Add(DataCollection.findNameMap[DataCollection.linkAddr][StartPos.ToString()]);
                            }
                            else
                            {
                                DataCollection.events[DataCollection.linkAddr].name.Add("未知");
                            }
                            DataCollection.events[DataCollection.linkAddr].addr.Add(StartPos.ToString());


                            if (DataCollection.DataField.Buffer[index + 3] == 0)
                                DataCollection.events[DataCollection.linkAddr].value.Add("分");
                            else
                                DataCollection.events[DataCollection.linkAddr].value.Add("合");

                            DataInfo = "";
                            DataInfo = String.Format("{0:d}", DataCollection.DataField.Buffer[index + 10] & 0x7f);   //年
                            DataInfo += "年";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index + 9] & 0x0f);  //月
                            DataInfo += "月";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index + 8] & 0x1f);  //日+星期
                            DataInfo += "日";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index + 7] & 0x1f);  //时
                            DataInfo += "时";
                            DataInfo += String.Format("{0:d}", DataCollection.DataField.Buffer[index + 6] & 0x3f);  //分
                            DataInfo += "分";
                            int ms = (DataCollection.DataField.Buffer[index + 5] << 8)
                                      + DataCollection.DataField.Buffer[index + 4];

                            DataInfo += String.Format("{0:d}", ms / 1000);
                            DataInfo += "秒";
                            DataInfo += String.Format("{0:d}", ms % 1000);
                            DataInfo += "毫秒";
                            DataCollection.events[DataCollection.linkAddr].date.Add(DataInfo);
                            index += 11;
                        }
                    }
                    DataCollection.ComTaskFlag.Comfirm = true;
                    break;


                default:
                    break;
            }
        }




        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void ToolStripMenuItemSaveEvent_Click(object sender, EventArgs e)
        {
            if (DataCollection.events[DataCollection.currentMon].addr.Count == 0)
            {
                MessageBox.Show("没有可保存的的事件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            saveF.Filter = "Excel 2007 Files(*.xlsx)|*.xlsx|Excel 2003 Files(*.xls)|*.xls|所有文件(*.*)|*.*";
            saveF.InitialDirectory = System.Environment.CurrentDirectory;  
            //选择保存路径              
            if (saveF.ShowDialog() != DialogResult.OK)         
            {                
                return;          
            }
            try
            {
                progressBar1.Value = 10;
                progressBar1.Visible = true;
                Application.DoEvents();
                progressBar1.Refresh();
                //创建一个EXCEL应用程序
                Excel.Application excel = new Excel.Application();
                //是否显示导出过程（显示创建后的EXCEL）      
                excel.Visible = false;
                //定义缺省值      
                Missing miss = Missing.Value;
                //创建一个新的工作簿           
                Excel.Workbooks workbooks = excel.Workbooks;
                Excel.Workbook workbook = workbooks.Add(miss);
                Excel.Worksheet sheet = workbook.ActiveSheet;
                //添加列名         

                excel.Cells[1, 1] = "序号";
                excel.Cells[1, 2] = "名称";
                excel.Cells[1, 3] = "地址";
                excel.Cells[1, 4] = "新值";
                excel.Cells[1, 5] = "日期";
                progressBar1.Value = 30;
                Application.DoEvents();
                progressBar1.Refresh();
                //填充数据         
                for (int i = 0; i < DataCollection.events[DataCollection.currentMon].addr.Count; i++)//所要添加的行数           
                {
                    //将数据填充到每一列的对应的单元格中    
                    excel.Cells[i + 2, 1] = i;
                    excel.Cells[i + 2, 2] = DataCollection.events[DataCollection.currentMon].name[i];
                    excel.Cells[i + 2, 3] = DataCollection.events[DataCollection.currentMon].addr[i];
                    excel.Cells[i + 2, 4] = DataCollection.events[DataCollection.currentMon].value[i];
                    excel.Cells[i + 2, 5] = DataCollection.events[DataCollection.currentMon].date[i];
                    progressBar1.Value += 60 / DataCollection.events[DataCollection.currentMon].addr.Count;
                }
                Application.DoEvents();
                progressBar1.Refresh();



                //设置表格样式        
                //设置列标题的背景颜色          
                Excel.Range er = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 5]];
                er.Cells.Interior.Color = Color.LightBlue;
                //重新选择单元格范围         
                int rowscount = DataCollection.events[DataCollection.currentMon].addr.Count;
                int columncount = 5;
                //将范围重新确定为每一行的第一个单元格    
                er = sheet.Range[sheet.Cells[1, 1], sheet.Cells[rowscount + 1, 1]];
                //设置范围内的单元格的背景颜色为淡蓝色     
                er.Cells.Interior.Color = Color.LightBlue;
                //选中EXCEL所有表格          
                er = sheet.Range[sheet.Cells[1, 1], sheet.Cells[rowscount + 1, columncount]];
                //让EXCEL中的所有单元格的列宽碎文字的长短自动调整         
                er.EntireColumn.AutoFit();
                // 让EXCEL的文本水平居中方式        
                er.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //在表的结尾添加数据导出的时间   
                er = sheet.Range[sheet.Cells[rowscount + 2, 1], sheet.Cells[rowscount + 3, columncount]];
                er.Merge(0);
                er.Value = "数据生成时间：" + DateTime.Now;
                ////设置单元格的背景颜色       
                //er.Cells.Interior.Color = Color.LightBlue;           
                // 文本水平居中方式     
                er.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //将文字的颜色设为红色     
                er.Font.Color = Color.Red;
                //保存文件      
                sheet.SaveAs(saveF.FileName, miss, miss, miss, miss, miss, Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);             //关闭表格   
                workbook.Close(false, miss, miss);
                workbooks.Close();
                //释放资源        
                excel.Quit();
                //保存成功  
                progressBar1.Visible = false;
                //MessageBox.Show("数据导出成功！\r\n" + saveF.FileName);
            }
            catch
            {
                MessageBox.Show("请安装微软Office 2007或更高版本！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                     
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("请联系intelligentest@hotmail.com咨询洽谈广告投放事宜，谢谢！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //打印显示手法报文函数,1为发送，2为接收
        private void outputDisplay(int type,int len)
        {
            if (checkBoxRoll.Checked)
                return;
            if (type == 1)
            {
                byte[] x = new byte[len];
                Array.Copy(DataCollection.ComStructData.TXBuffer, 0, x, 0, len);
                StringBuilder str = new StringBuilder("发送;");

                for (int i = 0; i < len; i++)
                {
                    Invoke(new Action(() =>
                    {
                        str.Append(" ");
                        str.Append(x[i].ToString("X2"));
                    }));
                }
                Invoke(new Action(() =>
                {
                    richTextBox1.AppendText(str + string.Format(" ({0})", System.DateTime.Now) + "\n");
                    //richTextBox1.Focus();
                    //设置光标的位置到文本尾 
                    //richTextBox1.Select(richTextBox1.TextLength, 0);
                    //滚动到控件光标处 
                    richTextBox1.ScrollToCaret();
                }));      
            }
            else if (type == 2)
            {
                byte[] x = new byte[len];
                Array.Copy(DataCollection.ComStructData.RXBuffer, 0, x, 0, len);
                StringBuilder str = new StringBuilder("接收;");
                for (int i = 0; i < len; i++)
                {
                    Invoke(new Action(() =>
                    {
                        str.Append(" ");
                        str.Append(x[i].ToString("X2"));
                    }));
                }
                Invoke(new Action(() =>
                {
                    richTextBox1.AppendText(str + string.Format(" ({0})", System.DateTime.Now) + "\n");
                    //richTextBox1.Focus();
                    //设置光标的位置到文本尾 
                    //richTextBox1.Select(richTextBox1.TextLength, 0);
                    //滚动到控件光标处 
                    richTextBox1.ScrollToCaret();
                }));
            }  
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripStatusX.Text = "";
        }

    }
}
