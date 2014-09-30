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
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] = 34;
                    DataCollection._ComStructData.TXBuffer[2] = 0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection._ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection._ComStructData.TXBuffer[9]=0;
                    DataCollection._ComStructData.TXBuffer[10]=1;
                    DataCollection._ComStructData.TXBuffer[11]=11;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=DataCollection.SystemParam.AddrByteNum_101;
                    DataCollection._ComStructData.TXBuffer[17]=0;
                    DataCollection._ComStructData.TXBuffer[18]=DataCollection.SystemParam.CauseByteNum_101;
                    DataCollection._ComStructData.TXBuffer[19]=0;
                    DataCollection._ComStructData.TXBuffer[20]=DataCollection.SystemParam.PubAddByteNum_101;
                    DataCollection._ComStructData.TXBuffer[21]=0;
                    DataCollection._ComStructData.TXBuffer[22]=(byte)DataCollection.SystemParam.Addr;
                    DataCollection._ComStructData.TXBuffer[23]=(byte)(DataCollection.SystemParam.Addr>>8);
                    DataCollection._ComStructData.TXBuffer[24]=(byte)DataCollection.SystemParam.HeartBeatTime;
                    DataCollection._ComStructData.TXBuffer[25]=(byte)(DataCollection.SystemParam.HeartBeatTime>>8);
                    DataCollection._ComStructData.TXBuffer[26]=(byte)DataCollection.SystemParam.BeatCycle;
                    DataCollection._ComStructData.TXBuffer[27]=(byte)(DataCollection.SystemParam.BeatCycle>>8);
                    DataCollection._ComStructData.TXBuffer[28]=(byte)DataCollection.SystemParam.ComFrameSTime;
                    DataCollection._ComStructData.TXBuffer[29]=(byte)(DataCollection.SystemParam.ComFrameSTime>>8);
                    DataCollection._ComStructData.TXBuffer[30]=(byte)DataCollection.SystemParam.NormalVoltageRating;
                    DataCollection._ComStructData.TXBuffer[31]=(byte)(DataCollection.SystemParam.NormalVoltageRating>>8);
                    DataCollection._ComStructData.TXBuffer[32]=(byte)DataCollection.SystemParam.NormalCurrentRating;
                    DataCollection._ComStructData.TXBuffer[33]=(byte)(DataCollection.SystemParam.NormalCurrentRating>>8);
                    DataCollection._ComStructData.TXBuffer[34]=(byte)DataCollection.SystemParam.PubAddr_101;
                    DataCollection._ComStructData.TXBuffer[35]=(byte)(DataCollection.SystemParam.PubAddr_101>>8);
                    DataCollection._ComStructData.TXBuffer[36]=(byte)DataCollection.SystemParam.RequestTime;
                    DataCollection._ComStructData.TXBuffer[37]=(byte)(DataCollection.SystemParam.RequestTime>>8);

                    DataCollection._ComStructData.TXBuffer[38]=GetSumCheck(1, 4, 34);
                    DataCollection._ComStructData.TXBuffer[39]=0x16;
                    len = 40;
                    break;

                case 2://向监视器下发监视端ip参数
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] =40;
                    DataCollection._ComStructData.TXBuffer[2] = 0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection._ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection._ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection._ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection._ComStructData.TXBuffer[11]=14;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=2; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=DataCollection._GPRSComSet.main_IP[0];
                    DataCollection._ComStructData.TXBuffer[17]=DataCollection._GPRSComSet.main_IP[1];
                    DataCollection._ComStructData.TXBuffer[18]=DataCollection._GPRSComSet.main_IP[2];
                    DataCollection._ComStructData.TXBuffer[19]=DataCollection._GPRSComSet.main_IP[3];
                    DataCollection._ComStructData.TXBuffer[20]=DataCollection._GPRSComSet.res_IP[0];
                    DataCollection._ComStructData.TXBuffer[21]=DataCollection._GPRSComSet.res_IP[1];
                    DataCollection._ComStructData.TXBuffer[22]=DataCollection._GPRSComSet.res_IP[2];
                    DataCollection._ComStructData.TXBuffer[23]=DataCollection._GPRSComSet.res_IP[3];
                    DataCollection._ComStructData.TXBuffer[24]=(byte)DataCollection._GPRSComSet.main_Port;
                    DataCollection._ComStructData.TXBuffer[25]=(byte)(DataCollection._GPRSComSet.main_Port>>8);
                    DataCollection._ComStructData.TXBuffer[26]=(byte)DataCollection._GPRSComSet.res_Port;
                    DataCollection._ComStructData.TXBuffer[27]=(byte)(DataCollection._GPRSComSet.res_Port>>8);
                    for(int i=0;i<16;i++)
                    {
                        DataCollection._ComStructData.TXBuffer[28+i]=(byte)DataCollection._GPRSComSet.APN[i];
                    }
                    DataCollection._ComStructData.TXBuffer[44]=GetSumCheck(1,4,40);
                    DataCollection._ComStructData.TXBuffer[45]=0x16;
                    len = 46;
                    break;

                case 3://向监视器下发读取监视端系统参数
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] =14;
                    DataCollection._ComStructData.TXBuffer[2] = 0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=2;//应用层功能码，参数读取
                    DataCollection._ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection._ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection._ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection._ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=0;
                    DataCollection._ComStructData.TXBuffer[17]=0;
                   
                    DataCollection._ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection._ComStructData.TXBuffer[19]=0x16;
                    len =20;

                    break;
                case 4://向监视器下发读取监视端ip参数
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] =14;
                    DataCollection._ComStructData.TXBuffer[2] = 0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=2;//应用层功能码，参数读取
                    DataCollection._ComStructData.TXBuffer[8]=1;//应用层功能码，给监测单元
                    DataCollection._ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection._ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection._ComStructData.TXBuffer[11]=1;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=2; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=0;
                    DataCollection._ComStructData.TXBuffer[17]=0;
                   
                    DataCollection._ComStructData.TXBuffer[18]=GetSumCheck(1,4,14);
                    DataCollection._ComStructData.TXBuffer[19]=0x16;
                    len =20;
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
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] =46;
                    DataCollection._ComStructData.TXBuffer[2] =0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=1;//应用层功能码，参数下载
                    DataCollection._ComStructData.TXBuffer[8]=2;//应用层功能码，给故障指示器
                    DataCollection._ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection._ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection._ComStructData.TXBuffer[11]=17;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=(byte)DataCollection.quickBreakSwitch[index];
                    DataCollection._ComStructData.TXBuffer[17]=(byte)(DataCollection.quickBreakSwitch[index]>>8);
                    DataCollection._ComStructData.TXBuffer[18]=(byte)DataCollection.quickBreakValue[index];
                    DataCollection._ComStructData.TXBuffer[19]=(byte)(DataCollection.quickBreakValue[index]>>8);
                    DataCollection._ComStructData.TXBuffer[20]=(byte)DataCollection.quickBreakTime[index];
                    DataCollection._ComStructData.TXBuffer[21]=(byte)(DataCollection.quickBreakTime[index]>>8);
                    DataCollection._ComStructData.TXBuffer[22]=(byte)DataCollection.overCurrentSwitch[index];
                    DataCollection._ComStructData.TXBuffer[23]=(byte)(DataCollection.overCurrentSwitch[index]>>8);
                    DataCollection._ComStructData.TXBuffer[24]=(byte)DataCollection.overCurrentValue[index];
                    DataCollection._ComStructData.TXBuffer[25]=(byte)(DataCollection.overCurrentValue[index]>>8);
                    DataCollection._ComStructData.TXBuffer[26]=(byte)DataCollection.overCurrentTime[index];
                    DataCollection._ComStructData.TXBuffer[27]=(byte)(DataCollection.overCurrentTime[index]>>8);
                    DataCollection._ComStructData.TXBuffer[28]=(byte)DataCollection.freeCurrentValue[index];
                    DataCollection._ComStructData.TXBuffer[29]=(byte)(DataCollection.freeCurrentValue[index]>>8);
                    DataCollection._ComStructData.TXBuffer[30]=(byte)DataCollection.freeCurrentTime[index];
                    DataCollection._ComStructData.TXBuffer[31]=(byte)(DataCollection.freeCurrentTime[index]>>8);
                    DataCollection._ComStructData.TXBuffer[32]=(byte)DataCollection.flashyFlowTime[index];
                    DataCollection._ComStructData.TXBuffer[33]=(byte)(DataCollection.flashyFlowTime[index]>>8);
                    DataCollection._ComStructData.TXBuffer[34]=(byte)DataCollection.selfAdaptionSwitch[index];
                    DataCollection._ComStructData.TXBuffer[35]=(byte)(DataCollection.selfAdaptionSwitch[index]>>8);
                    DataCollection._ComStructData.TXBuffer[36]=(byte)DataCollection.indtrAdds[index];
                    DataCollection._ComStructData.TXBuffer[37]=(byte)(DataCollection.indtrAdds[index]>>8);
                    DataCollection._ComStructData.TXBuffer[38]=(byte)DataCollection.rate[index];
                    DataCollection._ComStructData.TXBuffer[39]=(byte)(DataCollection.rate[index]>>8);
                    DataCollection._ComStructData.TXBuffer[40]=(byte)DataCollection.bandWidth[index];
                    DataCollection._ComStructData.TXBuffer[41]=(byte)(DataCollection.bandWidth[index]>>8);
                    DataCollection._ComStructData.TXBuffer[42]=(byte)DataCollection.tgz[index];
                    DataCollection._ComStructData.TXBuffer[43]=(byte)(DataCollection.tgz[index]>>8);
                    DataCollection._ComStructData.TXBuffer[44]=(byte)DataCollection.tfgs[index];
                    DataCollection._ComStructData.TXBuffer[45]=(byte)(DataCollection.tfgs[index]>>8);
                    DataCollection._ComStructData.TXBuffer[46]=(byte)DataCollection.res3[index];
                    DataCollection._ComStructData.TXBuffer[47]=(byte)(DataCollection.res3[index]>>8);
                    DataCollection._ComStructData.TXBuffer[48]=(byte)DataCollection.res4[index];
                    DataCollection._ComStructData.TXBuffer[49]=(byte)(DataCollection.res4[index]>>8);
                    
                    DataCollection._ComStructData.TXBuffer[50]=GetSumCheck(1,4,46);
                    DataCollection._ComStructData.TXBuffer[51]=0x16;
                    len =52;
                    break;

                case 2://向监视器下发故障指示器标志位
                    DataCollection._ComStructData.TXBuffer[0] = 0x69;
                    DataCollection._ComStructData.TXBuffer[1] =20;
                    DataCollection._ComStructData.TXBuffer[2] =0;
                    DataCollection._ComStructData.TXBuffer[3]=0x69;
                    DataCollection._ComStructData.TXBuffer[4]=1;//链路控制域
                    DataCollection._ComStructData.TXBuffer[5]=(byte)DataCollection.linkAddr;  //LinkAddress
                    DataCollection._ComStructData.TXBuffer[6]=(byte)(DataCollection.linkAddr>>8);
                    DataCollection._ComStructData.TXBuffer[7]=9;//应用层功能码，标志位下载
                    DataCollection._ComStructData.TXBuffer[8]=2;//应用层功能码，给故障指示器
                    DataCollection._ComStructData.TXBuffer[9]=0;//帧序列域
                    DataCollection._ComStructData.TXBuffer[10]=1;//帧序列域（序号）
                    DataCollection._ComStructData.TXBuffer[11]=4;//数据个数低位
                    DataCollection._ComStructData.TXBuffer[12]=0;//数据个数高位
                    DataCollection._ComStructData.TXBuffer[13]=0;//SQ
                    //数据域
                    DataCollection._ComStructData.TXBuffer[14]=1; //信息体地址低
                    DataCollection._ComStructData.TXBuffer[15]=0; //信息体地址高
                    DataCollection._ComStructData.TXBuffer[16]=(byte)DataCollection.calibration[index];
                    DataCollection._ComStructData.TXBuffer[17]=(byte)(DataCollection.calibration[index]>>8);
                    DataCollection._ComStructData.TXBuffer[18]=(byte)DataCollection.manualReset[index];
                    DataCollection._ComStructData.TXBuffer[19]=(byte)(DataCollection.manualReset[index]>>8);
                    DataCollection._ComStructData.TXBuffer[20]=(byte)DataCollection.res1[index];
                    DataCollection._ComStructData.TXBuffer[21]=(byte)(DataCollection.res1[index]>>8);
                    DataCollection._ComStructData.TXBuffer[22]=(byte)DataCollection.res2[index];
                    DataCollection._ComStructData.TXBuffer[23]=(byte)(DataCollection.res2[index]>>8);
                    
                    DataCollection._ComStructData.TXBuffer[24]=GetSumCheck(1,4,20);
                    DataCollection._ComStructData.TXBuffer[25]=0x16;
                    len =26;
                    break;
                case 3://向监视器下发读取故障指示器参数
                    break;
                case 4://向监视器下发读取故障指示器标志位
                    break;


                default:
                    break;
            }
            return len;
        }

        public static byte GetSumCheck(int type, int start, int len)
        {
            byte byTempSum = 0;
            if (type == 1)//send
            {
                for (int j = 0; j < len; j++)
                {
                    byTempSum += DataCollection._ComStructData.TXBuffer[start + j];
                }
            }
            else if (type == 2)//receive
            {
                for (int j = 0; j < len; j++)
                {
                    byTempSum += DataCollection._ComStructData.RXBuffer[start + j];
                }
            }
            return byTempSum;
        }
    }
}
