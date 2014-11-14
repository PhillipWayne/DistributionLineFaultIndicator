using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionLineFaultIndicator
{
    class ProtocoltyParam
    {
        public static int EncodeFrame(byte dataty)
        {
            int len = 0;
            switch (dataty)
            {
                case 1://向监视器下发监视端系统参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] = 34;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection.ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9]=0;
                    DataCollection.ComStructData.TXBuffer[10]=1;
                    DataCollection.ComStructData.TXBuffer[11]=11;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=DataCollection.SystemParam.AddrByteNum_101;
                    DataCollection.ComStructData.TXBuffer[17]=0;
                    DataCollection.ComStructData.TXBuffer[18]=DataCollection.SystemParam.CauseByteNum_101;
                    DataCollection.ComStructData.TXBuffer[19]=0;
                    DataCollection.ComStructData.TXBuffer[20]=DataCollection.SystemParam.PubAddByteNum_101;
                    DataCollection.ComStructData.TXBuffer[21]=0;
                    DataCollection.ComStructData.TXBuffer[22]=(byte)DataCollection.SystemParam.Addr;
                    DataCollection.ComStructData.TXBuffer[23]=(byte)(DataCollection.SystemParam.Addr>>8);
                    DataCollection.ComStructData.TXBuffer[24]=(byte)DataCollection.SystemParam.HeartBeatTime;
                    DataCollection.ComStructData.TXBuffer[25]=(byte)(DataCollection.SystemParam.HeartBeatTime>>8);
                    DataCollection.ComStructData.TXBuffer[26]=(byte)DataCollection.SystemParam.BeatCycle;
                    DataCollection.ComStructData.TXBuffer[27]=(byte)(DataCollection.SystemParam.BeatCycle>>8);
                    DataCollection.ComStructData.TXBuffer[28]=(byte)DataCollection.SystemParam.ComFrameSTime;
                    DataCollection.ComStructData.TXBuffer[29]=(byte)(DataCollection.SystemParam.ComFrameSTime>>8);
                    DataCollection.ComStructData.TXBuffer[30]=(byte)DataCollection.SystemParam.NormalVoltageRating;
                    DataCollection.ComStructData.TXBuffer[31]=(byte)(DataCollection.SystemParam.NormalVoltageRating>>8);
                    DataCollection.ComStructData.TXBuffer[32]=(byte)DataCollection.SystemParam.NormalCurrentRating;
                    DataCollection.ComStructData.TXBuffer[33]=(byte)(DataCollection.SystemParam.NormalCurrentRating>>8);
                    DataCollection.ComStructData.TXBuffer[34]=(byte)DataCollection.SystemParam.PubAddr_101;
                    DataCollection.ComStructData.TXBuffer[35]=(byte)(DataCollection.SystemParam.PubAddr_101>>8);
                    DataCollection.ComStructData.TXBuffer[36]=(byte)DataCollection.SystemParam.RequestTime;
                    DataCollection.ComStructData.TXBuffer[37]=(byte)(DataCollection.SystemParam.RequestTime>>8);

                    DataCollection.ComStructData.TXBuffer[38]=GetSumCheck(1, 4, 34);
                    DataCollection.ComStructData.TXBuffer[39]=0x16;
                    len = 40;
                    break;

                case 2://向监视器下发监视端ip参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =40;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection.ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=5;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=2; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=DataCollection.GPRSComSet.main_IP[0];
                    DataCollection.ComStructData.TXBuffer[17]=DataCollection.GPRSComSet.main_IP[1];
                    DataCollection.ComStructData.TXBuffer[18]=DataCollection.GPRSComSet.main_IP[2];
                    DataCollection.ComStructData.TXBuffer[19]=DataCollection.GPRSComSet.main_IP[3];
                    DataCollection.ComStructData.TXBuffer[20]=DataCollection.GPRSComSet.res_IP[0];
                    DataCollection.ComStructData.TXBuffer[21]=DataCollection.GPRSComSet.res_IP[1];
                    DataCollection.ComStructData.TXBuffer[22]=DataCollection.GPRSComSet.res_IP[2];
                    DataCollection.ComStructData.TXBuffer[23]=DataCollection.GPRSComSet.res_IP[3];
                    DataCollection.ComStructData.TXBuffer[24]=(byte)DataCollection.GPRSComSet.main_Port;
                    DataCollection.ComStructData.TXBuffer[25]=(byte)(DataCollection.GPRSComSet.main_Port>>8);
                    DataCollection.ComStructData.TXBuffer[26]=(byte)DataCollection.GPRSComSet.res_Port;
                    DataCollection.ComStructData.TXBuffer[27]=(byte)(DataCollection.GPRSComSet.res_Port>>8);
                    for(int i=0;i<16;i++)
                    {
                        DataCollection.ComStructData.TXBuffer[28+i]=(byte)DataCollection.GPRSComSet.APN[i];
                    }
                    DataCollection.ComStructData.TXBuffer[44]=GetSumCheck(1,4,40);
                    DataCollection.ComStructData.TXBuffer[45]=0x16;
                    len = 46;
                    break;

                case 3://向监视器下发读取监视端系统参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =14;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=2;//应用层功能码，参数读取
                    DataCollection.ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=0;
                    DataCollection.ComStructData.TXBuffer[17]=0;
                   
                    DataCollection.ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection.ComStructData.TXBuffer[19]=0x16;
                    len =20;

                    break;
                case 4://向监视器下发读取监视端ip参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =14;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=2;//应用层功能码，参数读取
                    DataCollection.ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=2; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=0;
                    DataCollection.ComStructData.TXBuffer[17]=0;
                   
                    DataCollection.ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection.ComStructData.TXBuffer[19]=0x16;
                    len =20;
                    break;

                case 5://向监视器下发读版本号指令
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =14;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=5;//应用层功能码，读版本号
                    DataCollection.ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=0;
                    DataCollection.ComStructData.TXBuffer[17]=0;
                   
                    DataCollection.ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection.ComStructData.TXBuffer[19]=0x16;
                    len =20;
                    break;
                case 6://向监视器下发设监视端地址指令
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] = 30;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3] = 0x69;
                    DataCollection.ComStructData.TXBuffer[4] = 1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5] = (byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6] = (byte)(DataCollection.currentMon >> 8);
                    DataCollection.ComStructData.TXBuffer[7] = 1;//应用层功能码，参数下载
                    DataCollection.ComStructData.TXBuffer[8] = 1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9] = 0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10] = 1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11] = 9;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12] = 0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13] = 0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14] = 3; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15] = 0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16] = (byte)DataCollection.MonitorAddrs.addrA1;
                    DataCollection.ComStructData.TXBuffer[17] = (byte)(DataCollection.MonitorAddrs.addrA1>>8);
                    DataCollection.ComStructData.TXBuffer[18] = (byte)DataCollection.MonitorAddrs.addrA2;
                    DataCollection.ComStructData.TXBuffer[19] = (byte)(DataCollection.MonitorAddrs.addrA2>>8);
                    DataCollection.ComStructData.TXBuffer[20] = (byte)DataCollection.MonitorAddrs.addrA3;
                    DataCollection.ComStructData.TXBuffer[21] = (byte)(DataCollection.MonitorAddrs.addrA3>>8);
                    DataCollection.ComStructData.TXBuffer[22] = (byte)DataCollection.MonitorAddrs.addrB1;
                    DataCollection.ComStructData.TXBuffer[23] = (byte)(DataCollection.MonitorAddrs.addrB1>>8);
                    DataCollection.ComStructData.TXBuffer[24] = (byte)DataCollection.MonitorAddrs.addrB2;
                    DataCollection.ComStructData.TXBuffer[25] = (byte)(DataCollection.MonitorAddrs.addrB2>>8);
                    DataCollection.ComStructData.TXBuffer[26] = (byte)DataCollection.MonitorAddrs.addrB3;
                    DataCollection.ComStructData.TXBuffer[27] = (byte)(DataCollection.MonitorAddrs.addrB3>>8);
                    DataCollection.ComStructData.TXBuffer[28] = (byte)DataCollection.MonitorAddrs.addrC1;
                    DataCollection.ComStructData.TXBuffer[29] = (byte)(DataCollection.MonitorAddrs.addrC1>>8);
                    DataCollection.ComStructData.TXBuffer[30] = (byte)DataCollection.MonitorAddrs.addrC2;
                    DataCollection.ComStructData.TXBuffer[31] = (byte)(DataCollection.MonitorAddrs.addrC2>>8);
                    DataCollection.ComStructData.TXBuffer[32] = (byte) DataCollection.MonitorAddrs.addrC3;
                    DataCollection.ComStructData.TXBuffer[33] = (byte)(DataCollection.MonitorAddrs.addrC3>>8);

                    DataCollection.ComStructData.TXBuffer[34] = GetSumCheck(1, 4, 30);
                    DataCollection.ComStructData.TXBuffer[35] = 0x16;
                    len = 36;
                    break;
                case 7://向监视器下发读取监视端地址参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] = 14;
                    DataCollection.ComStructData.TXBuffer[2] = 0;
                    DataCollection.ComStructData.TXBuffer[3] = 0x69;
                    DataCollection.ComStructData.TXBuffer[4] = 1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5] = (byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6] = (byte)(DataCollection.currentMon >> 8);
                    DataCollection.ComStructData.TXBuffer[7] = 2;//应用层功能码，参数读取
                    DataCollection.ComStructData.TXBuffer[8] = 1;//应用层功能码，给监测单元
                    DataCollection.ComStructData.TXBuffer[9] = 0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10] = 1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11] = 1;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12] = 0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13] = 0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14] = 3; //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15] = 0; //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16] = 0;
                    DataCollection.ComStructData.TXBuffer[17] = 0;

                    DataCollection.ComStructData.TXBuffer[18] = GetSumCheck(1, 4, 14);
                    DataCollection.ComStructData.TXBuffer[19] = 0x16;
                    len = 20;
                    break;

                default:
                    break;
            }
            return len;
        }

        public static int ParamEncodeFrame(byte dataty,int index)
        {
            int len = 0;
            switch (dataty)
            {
                case 1://向监视器下发故障指示器参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =46;
                    DataCollection.ComStructData.TXBuffer[2] =0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection.ComStructData.TXBuffer[8]=2;//应用层功能码，给故障指示器
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=17;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=(byte)(DataCollection.indtrAddrLocal[index]); //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=(byte)(DataCollection.indtrAddrLocal[index]>>8); //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=(byte)DataCollection.quickBreakSwitch[index];
                    DataCollection.ComStructData.TXBuffer[17]=(byte)(DataCollection.quickBreakSwitch[index]>>8);
                    DataCollection.ComStructData.TXBuffer[18]=(byte)DataCollection.quickBreakValue[index];
                    DataCollection.ComStructData.TXBuffer[19]=(byte)(DataCollection.quickBreakValue[index]>>8);
                    DataCollection.ComStructData.TXBuffer[20]=(byte)DataCollection.quickBreakTime[index];
                    DataCollection.ComStructData.TXBuffer[21]=(byte)(DataCollection.quickBreakTime[index]>>8);
                    DataCollection.ComStructData.TXBuffer[22]=(byte)DataCollection.overCurrentSwitch[index];
                    DataCollection.ComStructData.TXBuffer[23]=(byte)(DataCollection.overCurrentSwitch[index]>>8);
                    DataCollection.ComStructData.TXBuffer[24]=(byte)DataCollection.overCurrentValue[index];
                    DataCollection.ComStructData.TXBuffer[25]=(byte)(DataCollection.overCurrentValue[index]>>8);
                    DataCollection.ComStructData.TXBuffer[26]=(byte)DataCollection.overCurrentTime[index];
                    DataCollection.ComStructData.TXBuffer[27]=(byte)(DataCollection.overCurrentTime[index]>>8);
                    DataCollection.ComStructData.TXBuffer[28]=(byte)DataCollection.freeCurrentValue[index];
                    DataCollection.ComStructData.TXBuffer[29]=(byte)(DataCollection.freeCurrentValue[index]>>8);
                    DataCollection.ComStructData.TXBuffer[30]=(byte)DataCollection.freeCurrentTime[index];
                    DataCollection.ComStructData.TXBuffer[31]=(byte)(DataCollection.freeCurrentTime[index]>>8);
                    DataCollection.ComStructData.TXBuffer[32]=(byte)DataCollection.flashyFlowTime[index];
                    DataCollection.ComStructData.TXBuffer[33]=(byte)(DataCollection.flashyFlowTime[index]>>8);
                    DataCollection.ComStructData.TXBuffer[34]=(byte)DataCollection.selfAdaptionSwitch[index];
                    DataCollection.ComStructData.TXBuffer[35]=(byte)(DataCollection.selfAdaptionSwitch[index]>>8);
                    DataCollection.ComStructData.TXBuffer[36]=(byte)DataCollection.indtrAdds[index];
                    DataCollection.ComStructData.TXBuffer[37]=(byte)(DataCollection.indtrAdds[index]>>8);
                    DataCollection.ComStructData.TXBuffer[38]=(byte)DataCollection.rate[index];
                    DataCollection.ComStructData.TXBuffer[39]=(byte)(DataCollection.rate[index]>>8);
                    DataCollection.ComStructData.TXBuffer[40]=(byte)DataCollection.bandWidth[index];
                    DataCollection.ComStructData.TXBuffer[41]=(byte)(DataCollection.bandWidth[index]>>8);
                    DataCollection.ComStructData.TXBuffer[42]=(byte)DataCollection.tgz[index];
                    DataCollection.ComStructData.TXBuffer[43]=(byte)(DataCollection.tgz[index]>>8);
                    DataCollection.ComStructData.TXBuffer[44]=(byte)DataCollection.tfgs[index];
                    DataCollection.ComStructData.TXBuffer[45]=(byte)(DataCollection.tfgs[index]>>8);
                    DataCollection.ComStructData.TXBuffer[46]=(byte)DataCollection.res3[index];
                    DataCollection.ComStructData.TXBuffer[47]=(byte)(DataCollection.res3[index]>>8);
                    DataCollection.ComStructData.TXBuffer[48]=(byte)DataCollection.res4[index];
                    DataCollection.ComStructData.TXBuffer[49]=(byte)(DataCollection.res4[index]>>8);
                    
                    DataCollection.ComStructData.TXBuffer[50]=GetSumCheck(1,4,46);
                    DataCollection.ComStructData.TXBuffer[51]=0x16;
                    len =52;
                    break;

                case 2://向监视器下发故障指示器标志位
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =16;
                    DataCollection.ComStructData.TXBuffer[2] =0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=9;//应用层功能码，标志位下载
                    DataCollection.ComStructData.TXBuffer[8]=2;//应用层功能码，给故障指示器
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=4;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=(byte)(DataCollection.indtrAddrLocal[index]); //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=(byte)(DataCollection.indtrAddrLocal[index]>>8); //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16] = (byte)(DataCollection.manualReset[index]);
                    DataCollection.ComStructData.TXBuffer[17] = (byte)(DataCollection.calibration[index]);
                    DataCollection.ComStructData.TXBuffer[18]=(byte)DataCollection.res1[index];
                    DataCollection.ComStructData.TXBuffer[19]=(byte)DataCollection.res2[index];
                    
                    DataCollection.ComStructData.TXBuffer[20]=GetSumCheck(1,4,16);
                    DataCollection.ComStructData.TXBuffer[21]=0x16;
                    len =22;
                    break;
                case 3://向监视器下发读取故障指示器参数
                    DataCollection.ComStructData.TXBuffer[0] = 0x69;
                    DataCollection.ComStructData.TXBuffer[1] =14;
                    DataCollection.ComStructData.TXBuffer[2] =0;
                    DataCollection.ComStructData.TXBuffer[3]=0x69;
                    DataCollection.ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection.ComStructData.TXBuffer[5]=(byte)DataCollection.currentMon;  //LinkAddress
                    DataCollection.ComStructData.TXBuffer[6]=(byte)(DataCollection.currentMon>>8);
                    DataCollection.ComStructData.TXBuffer[7]=2;//应用层功能码，参数读取
                    DataCollection.ComStructData.TXBuffer[8]=2;//应用层功能码，给故障指示器
                    DataCollection.ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection.ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection.ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection.ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection.ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection.ComStructData.TXBuffer[14]=(byte)(DataCollection.indtrAddrLocal[index]); //信息体地址低
                    DataCollection.ComStructData.TXBuffer[15]=(byte)(DataCollection.indtrAddrLocal[index]>>8); //信息体地址高
                    DataCollection.ComStructData.TXBuffer[16]=0;
                    DataCollection.ComStructData.TXBuffer[17]=0;
                    
                    DataCollection.ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection.ComStructData.TXBuffer[19]=0x16;
                    len =20;
                    break;
                case 4://向监视器下发读取故障指示器标志位
                    break;


                default:
                    break;
            }
            return len;
        }

        public static void DecodeFrame()
        {
            if ((GetSumCheck(2, 4, DataCollection.ComStructData.RxLen - 6) != DataCollection.ComStructData.RXBuffer[DataCollection.ComStructData.RxLen - 2]))  //校验
            {
                DataCollection.montrParamState = 5;
                DataCollection.indtrParamState = 5;
            }
            if(DataCollection.ComStructData.RXBuffer[7]==2&&DataCollection.ComStructData.RXBuffer[8]==1)//应用层功能码：参数读取,给监测单元
            {
                if (DataCollection.ComStructData.RXBuffer[14] == 1 && DataCollection.ComStructData.RxLen==40)//信息体地址低位为1：监视端系统参数
                {
                    DataCollection.SystemParam.AddrByteNum_101=DataCollection.ComStructData.RXBuffer[16];
                    DataCollection.SystemParam.CauseByteNum_101=DataCollection.ComStructData.RXBuffer[18];
                    DataCollection.SystemParam.PubAddByteNum_101=DataCollection.ComStructData.RXBuffer[20];
                    DataCollection.SystemParam.Addr = (ushort)(DataCollection.ComStructData.RXBuffer[22] + (DataCollection.ComStructData.RXBuffer[23] << 8));
                    DataCollection.SystemParam.HeartBeatTime=(ushort)(DataCollection.ComStructData.RXBuffer[24]+(DataCollection.ComStructData.RXBuffer[25]<<8));
                    DataCollection.SystemParam.BeatCycle=(ushort)(DataCollection.ComStructData.RXBuffer[26]+(DataCollection.ComStructData.RXBuffer[27]<<8));
                    DataCollection.SystemParam.ComFrameSTime=(ushort)(DataCollection.ComStructData.RXBuffer[28]+(DataCollection.ComStructData.RXBuffer[29]<<8)) ;
                    DataCollection.SystemParam.NormalVoltageRating=(ushort)(DataCollection.ComStructData.RXBuffer[30]+(DataCollection.ComStructData.RXBuffer[31]<<8));
                    DataCollection.SystemParam.NormalCurrentRating=(ushort)(DataCollection.ComStructData.RXBuffer[32]+(DataCollection.ComStructData.RXBuffer[33]<<8));
                    DataCollection.SystemParam.PubAddr_101=(ushort)(DataCollection.ComStructData.RXBuffer[34]+(DataCollection.ComStructData.RXBuffer[35]<<8));
                    DataCollection.SystemParam.RequestTime=(ushort)(DataCollection.ComStructData.RXBuffer[36]+(DataCollection.ComStructData.RXBuffer[37]<<8));
                    DataCollection.montrParamState = 4;//读取成功
                    DataCollection.montrUpdate = 1;//显示需要更新
                }
                else if (DataCollection.ComStructData.RXBuffer[14] == 2 && DataCollection.ComStructData.RxLen == 46)//信息体地址低位为2：监视端ip参数
                {

                    DataCollection.GPRSComSet.main_IP[0]=DataCollection.ComStructData.RXBuffer[16];
                    DataCollection.GPRSComSet.main_IP[1]=DataCollection.ComStructData.RXBuffer[17];
                    DataCollection.GPRSComSet.main_IP[2]=DataCollection.ComStructData.RXBuffer[18];
                    DataCollection.GPRSComSet.main_IP[3]=DataCollection.ComStructData.RXBuffer[19];
                    DataCollection.GPRSComSet.res_IP[0]=DataCollection.ComStructData.RXBuffer[20];
                    DataCollection.GPRSComSet.res_IP[1]=DataCollection.ComStructData.RXBuffer[21];
                    DataCollection.GPRSComSet.res_IP[2]=DataCollection.ComStructData.RXBuffer[22];
                    DataCollection.GPRSComSet.res_IP[3]=DataCollection.ComStructData.RXBuffer[23];
                    DataCollection.GPRSComSet.main_Port=(ushort)(DataCollection.ComStructData.RXBuffer[24]+(DataCollection.ComStructData.RXBuffer[25]<<8));
                    DataCollection.GPRSComSet.res_Port=(ushort)(DataCollection.ComStructData.RXBuffer[26]+(DataCollection.ComStructData.RXBuffer[27]<<8));
                    for (int i = 0; i < 16; i++)
                    {
                        DataCollection.GPRSComSet.APN[i] = (char)DataCollection.ComStructData.RXBuffer[28 + i];
                    }
                    DataCollection.montrParamState = 4;//读取成功
                    DataCollection.montrUpdate = 1;//显示需要更新
                }
                else if (DataCollection.ComStructData.RXBuffer[14] == 3 && DataCollection.ComStructData.RxLen ==36 )//信息体地址低位为3：监视端地址参数
                {

                    DataCollection.MonitorAddrs.addrA1 = DataCollection.ComStructData.RXBuffer[16]+(DataCollection.ComStructData.RXBuffer[17]<<8);
                    DataCollection.MonitorAddrs.addrA2 = DataCollection.ComStructData.RXBuffer[18+(DataCollection.ComStructData.RXBuffer[19]<<8)];
                    DataCollection.MonitorAddrs.addrA3 = DataCollection.ComStructData.RXBuffer[20]+(DataCollection.ComStructData.RXBuffer[21]<<8);
                    DataCollection.MonitorAddrs.addrB1 = DataCollection.ComStructData.RXBuffer[22]+(DataCollection.ComStructData.RXBuffer[23]<<8);
                    DataCollection.MonitorAddrs.addrB2= DataCollection.ComStructData.RXBuffer[24]+(DataCollection.ComStructData.RXBuffer[25]<<8);
                    DataCollection.MonitorAddrs.addrB3= DataCollection.ComStructData.RXBuffer[26]+(DataCollection.ComStructData.RXBuffer[27]<<8);
                    DataCollection.MonitorAddrs.addrC1= DataCollection.ComStructData.RXBuffer[28]+(DataCollection.ComStructData.RXBuffer[29]<<8);
                    DataCollection.MonitorAddrs.addrC2= DataCollection.ComStructData.RXBuffer[30]+(DataCollection.ComStructData.RXBuffer[31]<<8);
                    DataCollection.MonitorAddrs.addrC3= DataCollection.ComStructData.RXBuffer[32]+(DataCollection.ComStructData.RXBuffer[33]<<8);
                    DataCollection.montrParamState = 4;//读取成功
                    DataCollection.montrUpdate = 1;//显示需要更新
                }
                else if (DataCollection.ComStructData.RxLen == 19)//读取失败
                {
                    if (DataCollection.ComStructData.RXBuffer[14] == 1)//监视端系统参数读取失败
                        DataCollection.montrParamState = 3;
                    else if (DataCollection.ComStructData.RXBuffer[14] == 2)//监视端ip参数读取失败
                        DataCollection.montrParamState = 3;
                    else if (DataCollection.ComStructData.RXBuffer[14] == 3)//监视端地址参数读取失败
                        DataCollection.montrParamState = 3;
                }
                
            }

            if(DataCollection.ComStructData.RXBuffer[7]==1&&DataCollection.ComStructData.RXBuffer[8]==1)//应用层功能码：参数下载,给监测单元
            {
                if (DataCollection.ComStructData.RXBuffer[14] == 1 && DataCollection.ComStructData.RxLen == 19)//信息体地址低位为1：监视端系统参数设置响应
                {
                    if (DataCollection.ComStructData.RXBuffer[16] == 0)//否认
                    {
                        DataCollection.montrParamState = 2;
                    }
                    else if (DataCollection.ComStructData.RXBuffer[16] == 1)//确认
                    {
                        DataCollection.montrParamState = 1;
                    }
                }
                else if (DataCollection.ComStructData.RXBuffer[14] == 2 && DataCollection.ComStructData.RxLen == 19)//信息体地址低位为2：监视端ip参数设置响应
                {
                    if (DataCollection.ComStructData.RXBuffer[16] == 0)//否认
                    {
                        DataCollection.montrParamState = 2;
                    }
                    else if (DataCollection.ComStructData.RXBuffer[16] == 1)//确认
                    {
                        DataCollection.montrParamState = 1;
                    }
                }
                else if (DataCollection.ComStructData.RXBuffer[14] == 3 && DataCollection.ComStructData.RxLen == 19)//信息体地址低位为3：监视端地址参数设置响应
                {
                    if (DataCollection.ComStructData.RXBuffer[16] == 0)//否认
                    {
                        DataCollection.montrParamState = 2;
                    }
                    else if (DataCollection.ComStructData.RXBuffer[16] == 1)//确认
                    {
                        DataCollection.montrParamState = 1;
                    }
                }
            }


            if (DataCollection.ComStructData.RXBuffer[7] == 2 && DataCollection.ComStructData.RXBuffer[8] == 2)//应用层功能码：参数读取,通过监视端给故障指示器
            {
                if (DataCollection.ComStructData.RxLen == 52)//读取成功
                {
                    int index = DataCollection.ComStructData.RXBuffer[14] - 1;
                    DataCollection.quickBreakSwitch[index] = (ushort)(DataCollection.ComStructData.RXBuffer[16] + (DataCollection.ComStructData.RXBuffer[17] << 8));
                    DataCollection.quickBreakValue[index] = (ushort)(DataCollection.ComStructData.RXBuffer[18] + (DataCollection.ComStructData.RXBuffer[19] << 8));
                    DataCollection.quickBreakTime[index] = (ushort)(DataCollection.ComStructData.RXBuffer[20] + (DataCollection.ComStructData.RXBuffer[21] << 8));
                    DataCollection.overCurrentSwitch[index] = (ushort)(DataCollection.ComStructData.RXBuffer[22] + (DataCollection.ComStructData.RXBuffer[23] << 8));
                    DataCollection.overCurrentValue[index] = (ushort)(DataCollection.ComStructData.RXBuffer[24] + (DataCollection.ComStructData.RXBuffer[25] << 8));
                    DataCollection.overCurrentTime[index] = (ushort)(DataCollection.ComStructData.RXBuffer[26] + (DataCollection.ComStructData.RXBuffer[27] << 8));
                    DataCollection.freeCurrentValue[index] = (ushort)(DataCollection.ComStructData.RXBuffer[28] + (DataCollection.ComStructData.RXBuffer[29] << 8));
                    DataCollection.freeCurrentTime[index] = (ushort)(DataCollection.ComStructData.RXBuffer[30] + (DataCollection.ComStructData.RXBuffer[31] << 8));
                    DataCollection.flashyFlowTime[index] = (ushort)(DataCollection.ComStructData.RXBuffer[32] + (DataCollection.ComStructData.RXBuffer[33] << 8));
                    DataCollection.selfAdaptionSwitch[index] = (ushort)(DataCollection.ComStructData.RXBuffer[34] + (DataCollection.ComStructData.RXBuffer[35] << 8));
                    DataCollection.indtrAdds[index] = (ushort)(DataCollection.ComStructData.RXBuffer[36] + (DataCollection.ComStructData.RXBuffer[37] << 8));
                    DataCollection.rate[index] = (ushort)(DataCollection.ComStructData.RXBuffer[38] + (DataCollection.ComStructData.RXBuffer[39] << 8));
                    DataCollection.bandWidth[index] = (ushort)(DataCollection.ComStructData.RXBuffer[40] + (DataCollection.ComStructData.RXBuffer[41] << 8));
                    DataCollection.tgz[index] = (ushort)(DataCollection.ComStructData.RXBuffer[42] + (DataCollection.ComStructData.RXBuffer[43] << 8));
                    DataCollection.tfgs[index] = (ushort)(DataCollection.ComStructData.RXBuffer[44] + (DataCollection.ComStructData.RXBuffer[45] << 8));
                    DataCollection.res3[index] = (ushort)(DataCollection.ComStructData.RXBuffer[46] + (DataCollection.ComStructData.RXBuffer[47] << 8));
                    DataCollection.res4[index] = (ushort)(DataCollection.ComStructData.RXBuffer[48] + (DataCollection.ComStructData.RXBuffer[49] << 8));
                    DataCollection.indtrParamState = 4;//读取成功
                    DataCollection.indtrUpdate = 1;
                }
                else if (DataCollection.ComStructData.RxLen == 19)//读取失败
                {
                    DataCollection.indtrParamState = 3;
                }
                

            }

            if (DataCollection.ComStructData.RXBuffer[7] == 1 && DataCollection.ComStructData.RXBuffer[8] == 2 && DataCollection.ComStructData.RxLen==19)//向监视器下发指示器参数响应
            {
                if (DataCollection.ComStructData.RXBuffer[16] == 1) // 参数设置确认
                {
                    DataCollection.indtrParamState = 1;
                }
                else if (DataCollection.ComStructData.RXBuffer[16] == 0)//参数设置否认
                {
                    DataCollection.indtrParamState = 2;
                }
            }
            if (DataCollection.ComStructData.RXBuffer[7] == 9 && DataCollection.ComStructData.RXBuffer[8] == 2 && DataCollection.ComStructData.RxLen == 19)//向监视器下发指示器标志位响应
            {
                if (DataCollection.ComStructData.RXBuffer[16] == 1) // 参数设置确认
                {
                    DataCollection.indtrParamState = 1;
                }
                else if (DataCollection.ComStructData.RXBuffer[16] == 0)//参数设置否认
                {
                    DataCollection.indtrParamState = 2;
                }
            }
            if (DataCollection.ComStructData.RXBuffer[7] == 5 && DataCollection.ComStructData.RxLen==57)//读版本号响应
            {
                byte[] bytes = new byte[4];
                bytes[0] = DataCollection.ComStructData.RXBuffer[16];
                bytes[1] = DataCollection.ComStructData.RXBuffer[17];
                bytes[2] = DataCollection.ComStructData.RXBuffer[18];
                bytes[3] = DataCollection.ComStructData.RXBuffer[19];
                DataCollection.Version.VER_FACNO = System.Text.Encoding.Default.GetString(bytes).Replace("\0","");

                bytes=new byte[24];
                for (int i = 0; i < 24;i++ )
                    bytes[i] = DataCollection.ComStructData.RXBuffer[i+20];
                DataCollection.Version.VER_DEVNO = System.Text.Encoding.Default.GetString(bytes).Replace("\0", "");

                bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                    bytes[i] = DataCollection.ComStructData.RXBuffer[i + 44];
                DataCollection.Version.VER_SOFTNO = System.Text.Encoding.Default.GetString(bytes).Replace("\0", "");

                for (int i = 0; i < 3;i++ )
                    DataCollection.Version.VER_SOFTDATE[i] = DataCollection.ComStructData.RXBuffer[i + 52];

                DataCollection.montrParamState = 4;
                DataCollection.montrUpdate = 1;
            }


        }

        //效验函数
        public static byte GetSumCheck(int type, int start, int len)
        {
            byte byTempSum = 0;
            if (type == 1)//发送
            {
                for (int j = 0; j < len; j++)
                {
                    byTempSum += DataCollection.ComStructData.TXBuffer[start + j];
                }
            }
            else if (type == 2)//接收
            {
                for (int j = 0; j < len; j++)
                {
                    byTempSum += DataCollection.ComStructData.RXBuffer[start + j];
                }
            }
            return byTempSum;
        }
    }
}
