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

namespace DistributionLineFaultIndicator
{
    public partial class MainForm : Form
    {
        Monitor monitor = new Monitor();
        Indicator indicator = new Indicator();
        OpenFileDialog openF = new OpenFileDialog();

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
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

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
            this.splitContainer1.Panel2.Controls.Add(indicator);
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

        private void toolStripButtonTest_Click(object sender, EventArgs e)
        {
            ComConnectTest comConnectTest = new ComConnectTest();
            comConnectTest.Show();
        }

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
            MessageBox.Show("    版本号：v0.05      by 2014-9-28 ");
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


            if (DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F == true)    //请求链路状态
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = false;
                DataCollection._ComStructData.TxLen = 0;

                DataCollection._DataField.FieldLen = 0;
                DataCollection._DataField.FieldVSQ = 1;

                DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(1);
                DataCollection._ComStructData.TX_TASK = true;

            }
            else if (DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F1 == true)    //链路复位
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F1 = false;
                DataCollection._ComStructData.TxLen = 0;
                //DataCollection._ComStructData.TxLen = EncodeFrame(1, 0, 0, ref DataCollection._ComStructData.TXBuffer[0],ref DataCollection._DataField.Buffer[0]);
                DataCollection._DataField.FieldLen = 0;
                DataCollection._DataField.FieldVSQ = 1;
                DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(2);
                DataCollection._ComStructData.TX_TASK = true;

            }
            else if (DataCollection._ComTaskFlag.C_IC_NA_1 == true)  //总召唤
            {
                DataCollection._ComTaskFlag.C_IC_NA_1 = false;
                DataCollection._ComStructData.TxLen = 0;

                DataCollection._DataField.FieldLen = 0;
                DataCollection._DataField.FieldVSQ = 1;

                DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(10);

                DataCollection._ProtocoltyFlag.ZZFlag = false;
                DataCollection._ComStructData.TX_TASK = true;
                DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);

            }
            else if(DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F== true)//链路请求确认
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F = false;
                DataCollection._ComStructData.TxLen = 0;

                DataCollection._DataField.FieldLen = 0;
                DataCollection._DataField.FieldVSQ = 1;

                DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(150);
                DataCollection._ComStructData.TX_TASK = true;
            }
            else if (DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F1 == true)//链路复位确认
            {
                DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F1 = false;
                DataCollection._ComStructData.TxLen = 0;

                DataCollection._DataField.FieldLen = 0;
                DataCollection._DataField.FieldVSQ = 1;

                DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(151);
                DataCollection._ComStructData.TX_TASK = true;
            }

            //else if (DataCollection._ComTaskFlag.C_CS_NA_1 == true)
            //{

            //    DataCollection._ComTaskFlag.C_CS_NA_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    DataCollection.ZDYtype = 7;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(7, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "对时";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.YK_Sel_1_D == true)
            //{
            //    DataCollection._ComTaskFlag.YK_Sel_1_D = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    DataCollection._DataField.FieldVSQ = 1;
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

            //    DataCollection._DataField.FieldVSQ = 1;
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

            //    DataCollection._DataField.FieldVSQ = 1;
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

            //    DataCollection._DataField.FieldVSQ = 1;
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

            //    DataCollection._DataField.FieldVSQ = 1;
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

            //    DataCollection._DataField.FieldVSQ = 1;
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
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 10, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //DataCollection._DataField.FieldLen, 1, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.FieldLen = 0;
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 5;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(5, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);
            //    DataCollection.ComFrameMsg = "请求版本号";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.CALLTIME_1 == true)
            //{
            //    DataCollection._ComTaskFlag.CALLTIME_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    DataCollection._DataField.FieldLen = 0;
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 6;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(6, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

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
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(2, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "读参数";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.SET_PARAM_CON == true)  //设置参数
            //{
            //    DataCollection._ComTaskFlag.SET_PARAM_CON = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 8, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                         DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection.ZDYtype = 1;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(1, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "设置";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}

            //else if (DataCollection._ComTaskFlag.AloneCallYx_1 == true)
            //{
            //    DataCollection._ComTaskFlag.AloneCallYx_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 12, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                          DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
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
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 13, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                          DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
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
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 17, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                          DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(13);

            //    DataCollection.ComFrameMsg = "复位";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_Start_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_Start_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                              DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "升级";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMStart_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMStart_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                              DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "ARM升级";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}

            //else if (DataCollection._ComTaskFlag.UpdateCode_JY_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_JY_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //{
            //    //    DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                              DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    //}
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                             DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                             DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "校验";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMJY_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMJY_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //{
            //    //    DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                              DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    //}
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                             DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                             DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

            //    DataCollection.ComFrameMsg = "ARM校验";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}

            //else if (DataCollection._ComTaskFlag.UpdateCode_OK_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_OK_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;


            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                             DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 3;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(3, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                            DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                            DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "更新代码";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}
            //else if (DataCollection._ComTaskFlag.UpdateCode_ARMOK_1 == true)
            //{
            //    DataCollection._ComTaskFlag.UpdateCode_ARMOK_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;


            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 22, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                             DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.ZDYtype = 9;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(9, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                            DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                            DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);


            //    DataCollection.ComFrameMsg = "ARM更新代码";
            //    DataCollection._ComStructData.TX_TASK = true;

            //}


            //else if (DataCollection._ComTaskFlag.CallRecordData == true)
            //{
            //    DataCollection._ComTaskFlag.CallRecordData = false;
            //    DataCollection._ComStructData.TxLen = 0;
            //    //DataCollection._ComStructData.TxLen = EncodeFrame(2, 20, ref DataCollection._ComStructData.TXBuffer[0], ref DataCollection._DataField.Buffer[0],
            //    //                                          DataCollection._DataField.FieldLen, DataCollection._DataField.FieldVSQ, DataCollection.DevAddr, DataCollection.ParamInfoAddr, MTxSeq, MRxSeq);
            //    DataCollection.ZDYtype = 4;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(4, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);
            //    DataCollection.ComFrameMsg = "请求记录数据";
            //    DataCollection._ComStructData.TX_TASK = true;
            //}


            //else if (DataCollection._ComTaskFlag.READ_Hard_1 == true)
            //{

            //    DataCollection._ComTaskFlag.READ_Hard_1 = false;
            //    DataCollection._ComStructData.TxLen = 0;

            //    //DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(37);
            //    DataCollection._DataField.FieldLen = 0;
            //    DataCollection._DataField.FieldVSQ = 1;
            //    DataCollection.seqflag = 0;
            //    DataCollection.seq = 1;
            //    DataCollection.SQflag = 0;
            //    DataCollection.ZDYtype = 8;
            //    if (DataCollection.Framelen == 231)
            //        DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(50);
            //    else
            //        DataCollection._ComStructData.TxLen = protocoltyparam.EncodeFrame(8, DataCollection._ComStructData.TXBuffer, DataCollection._DataField.Buffer,
            //                                              DataCollection._DataField.FieldLen, DataCollection.DevAddr, DataCollection.addselect, DataCollection.seqflag, DataCollection.seq,
            //                                              DataCollection.SQflag, DataCollection._DataField.FieldVSQ, DataCollection.ParamInfoAddr);

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
            else if (DataCollection._ComTaskFlag.CALL_1 == true)  //召唤一级数据
            {

                DataCollection._ComTaskFlag.CALL_1 = false;
               
                    DataCollection._ComStructData.TxLen = 0;

                    DataCollection._DataField.FieldLen = 0;
                    DataCollection._DataField.FieldVSQ = 1;

                    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(3);

                    DataCollection._ComStructData.TX_TASK = true;
                    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
                    //    DataCollection._ThreadIndex.ComThreadID = 9;
               
            }
            else if (DataCollection._ComTaskFlag.CALL_2 == true)  //召唤二级数据
            {
                
                    DataCollection._ComTaskFlag.CALL_2 = false;
                    DataCollection._ComStructData.TxLen = 0;

                    DataCollection._DataField.FieldLen = 0;
                    DataCollection._DataField.FieldVSQ = 1;

                    DataCollection._ComStructData.TxLen = Protocolty101.EncodeFrame(4);

                    DataCollection._ComStructData.TX_TASK = true;
                    DataCollection._ComStructData.FCB = (!DataCollection._ComStructData.FCB);
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
            }
            catch
            {
                socket1.Close();
                if (threadSend1 != null)
                    threadSend1.Abort();
                if (threadRev1 != null)
                    threadRev1.Abort();
                toolStripButtonLink.Enabled = true;
                toolStripButtonUnLink.Enabled = false;
                toolStripStatusLabel1.Text = "监测单元：连接失败";
                threadConnect1.Abort();
            }

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




















        private void dataSend1()
        {
            while (true)    //处理事物
            {
                PtlComFrameProc();
                if (DataCollection._ComStructData.TX_TASK == true)
                {

                    try
                    {
                        DataCollection._ComStructData.TX_TASK = false;
                        socket1.Send(DataCollection._ComStructData.TXBuffer, 0, DataCollection._ComStructData.TxLen, SocketFlags.None);

                        byte[] x = new byte[DataCollection._ComStructData.TxLen];
                        Array.Copy(DataCollection._ComStructData.TXBuffer, 0, x, 0, DataCollection._ComStructData.TxLen);
                        StringBuilder str = new StringBuilder("发送;");
                        for (int i = 0; i < DataCollection._ComStructData.TxLen; i++)
                        {
                            str.Append(" ");
                            str.Append(x[i].ToString("X2"));
                        }
                        richTextBox1.AppendText(str+"\n");

                    }
                    catch
                    {
                        toolStripStatusX.Text = "监测单元方向报文发送错误";
                    }

                }
                else
                {
                }
                Thread.Sleep(5);
            }
        }

        private void dataRev1()
        {
            while (true)
            {
                try
                {
                    DataCollection._ComStructData.RxLen = socket1.Receive(DataCollection._ComStructData.RXBuffer);
                    if (DataCollection._ComStructData.RxLen > 0)
                    {
                        DataTy = Protocolty101.DecodeFrame();
                        System.Diagnostics.Debug.WriteLine("Dataty: " + DataTy);
                        processRevTele(DataTy);

                        
                        byte[] x = new byte[DataCollection._ComStructData.RxLen];
                        Array.Copy(DataCollection._ComStructData.RXBuffer, 0, x, 0, DataCollection._ComStructData.RxLen);
                        StringBuilder str = new StringBuilder("接收;");
                        for (int i = 0; i < DataCollection._ComStructData.RxLen; i++)
                        {
                            str.Append(" ");
                            str.Append(x[i].ToString("X2"));
                        }
                        richTextBox1.AppendText(str+"\n");

                        DataCollection.class2Delay = DataCollection.class2Delay_default;
                    }
                    else
                    {

                    }


                }
                catch
                {
                    toolStripStatusX.Text = "监测单元方向报文接收错误";
                }
                Thread.Sleep(1);
            }
        }

        private void dataSend2()
        {
            while (true)    //处理事物
            {
                PtlComFrameProc2();
                if (DataCollection._ComStructData2.TX_TASK == true)
                {
                    try
                    {
                        DataCollection._ComStructData2.TX_TASK = false;
                        socket2.Send(DataCollection._ComStructData2.TXBuffer, 0, DataCollection._ComStructData2.TxLen, SocketFlags.None);
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
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F1 = true;  //链路复位
            }
            else if (DataTy == 3)  //复位链路确认
            {
                DataCollection.linkState = 2;

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



                if (DataTy == 150)
                {
                    DataCollection.linkState = 3;
                    DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F = true;
                }
                else if (DataTy == 151)
                {
                    DataCollection.linkState = 4;
                    DataCollection._ComTaskFlag.C_RQ_NA_LINKCOM_F1 = true;
                }






            byte[] bytes = new byte[4];
            int data = 0;
            int StartPos = 0;
            int index = 0;

            switch (DataTy)
            {
                case 51:     //单点遥信响应站召唤，类型标识1  
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection._DataField.Buffer[2] << 16) +DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);    
                    if(DataCollection.inflen==2)
                        StartPos = DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);

                    for (int j = 0; j < DataCollection._DataField.FieldVSQ; j++)
                    {

                        if (DataCollection.inflen == 3)
                        {
                            if ((DataCollection._DataField.Buffer[j + 3]&0x01) == 0)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                        }
                        if (DataCollection.inflen == 2)
                        {
                            if ((DataCollection._DataField.Buffer[j + 2] & 0x01) == 0)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                        }
                        StartPos++;
                    }
                    break;

                case 52:     //突发，单点遥信变位，类型标识1
                    for (int i = 0; i < DataCollection._DataField.FieldVSQ; i++)
                    {
                        if (DataCollection.inflen == 3) 
                        { 
                            StartPos = (DataCollection._DataField.Buffer[index + 2] << 16) + DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);
                            if ((DataCollection._DataField.Buffer[index + 3] & 0x01) == 0)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                            index += 4;
                        }
                        else if (DataCollection.inflen == 2)
                        {
                            StartPos = DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);
                            if ((DataCollection._DataField.Buffer[index + 2] & 0x01) == 0)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                            index += 3;
                        }
                    }
                    break;

                case 36:         //归一化遥测响应站召唤,类型标识9
                    if (DataCollection.inflen == 2)
                        StartPos = DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection._DataField.Buffer[2] << 16) + DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);  
                    for (int j = 0; j < DataCollection._DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            data = DataCollection._DataField.Buffer[index + 2] + (DataCollection._DataField.Buffer[index + 3] << 8);

                        }
                        if (DataCollection.inflen == 3)
                        {
                            data = DataCollection._DataField.Buffer[index + 3] + (DataCollection._DataField.Buffer[index + 4] << 8);
                        }
                        if (data > 0x8000)
                            data = data - 65536;
                        DataCollection.YcData.value[StartPos-0x4001] = Convert.ToString(data);
                        StartPos++;
                        index += 3;
                    }
                    break;

                case 37:        //突发，归一化遥测扰动,类型标识9
                    for (int j = 0; j < DataCollection._DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            StartPos += DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);

                            data = DataCollection._DataField.Buffer[index + 2] + (DataCollection._DataField.Buffer[index + 3] << 8);
                            index += 5;
                            if (data > 0x8000)
                                data = data - 65536;
                            DataCollection.YcData.value[StartPos - 0x4001] = Convert.ToString(data);
                        }
                        else if (DataCollection.inflen == 3)
                        {
                            StartPos = DataCollection._DataField.Buffer[index + 2];
                            StartPos = StartPos << 16;
                            StartPos += DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);

                            data = DataCollection._DataField.Buffer[index + 3] + (DataCollection._DataField.Buffer[index + 4] << 8);
                            index += 6;
                            if (data > 0x8000)
                                data = data - 65536;
                            DataCollection.YcData.value[StartPos - 0x4001] = Convert.ToString(data);
                        }
                    }
                    break;

                case 53:     //双点遥信响应站召唤，类型标识3
                    if(DataCollection.inflen==3)
                        StartPos = (DataCollection._DataField.Buffer[2] << 16) +DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);    
                    if(DataCollection.inflen==2)
                        StartPos = DataCollection._DataField.Buffer[0] + (DataCollection._DataField.Buffer[1] << 8);
                    System.Diagnostics.Debug.WriteLine("FieldVSQ: " + DataCollection._DataField.FieldVSQ);
                    for (int j = 0; j < DataCollection._DataField.FieldVSQ; j++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            System.Diagnostics.Debug.WriteLine(String.Format("DataCollection._DataField.Buffer{0:d}: ",j + 2) + DataCollection._DataField.Buffer[j + 2].ToString("X2"));
                            System.Diagnostics.Debug.WriteLine("StartPos - 1:" + (StartPos - 1));
                            if ((DataCollection._DataField.Buffer[j + 2] & 0x03) == 1)
                                DataCollection.YxData.value[StartPos-1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";

                            
                            
                        }
                        if (DataCollection.inflen == 3)
                        {
                            if ((DataCollection._DataField.Buffer[j + 3] & 0x03) == 1)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                        }
                        StartPos++;
                    }
                    break;

                case 54:    //突变，双点遥信，类型标识3
                    for (int i = 0; i < DataCollection._DataField.FieldVSQ; i++)
                    {
                        if (DataCollection.inflen == 2)
                        {
                            StartPos += DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);
                            if ((DataCollection._DataField.Buffer[index + 2] & 0x03) == 1)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                            index += 3;
                        }
                        else if (DataCollection.inflen == 3)
                        {
                            StartPos = DataCollection._DataField.Buffer[index + 2];
                            StartPos = StartPos << 16;
                            StartPos += DataCollection._DataField.Buffer[index] + (DataCollection._DataField.Buffer[index + 1] << 8);
                            if ((DataCollection._DataField.Buffer[index + 3]& 0x03) == 1)
                                DataCollection.YxData.value[StartPos - 1] = "分";
                            else
                                DataCollection.YxData.value[StartPos - 1] = "合";
                            index += 4;
                        }
                    }
                    break;


                default:
                    break;
            }
        }

        private void timerC_RQ_NA_LINKREQ_Tick(object sender, EventArgs e)
        {
            if (DataCollection.linkState == 0)
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F = true;
            else if (DataCollection.linkState == 1)
                DataCollection._ComTaskFlag.C_RQ_NA_LINKREQ_F1 = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("ss");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
