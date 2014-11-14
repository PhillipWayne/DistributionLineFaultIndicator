using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistributionLineFaultIndicator
{
    class WriteReadAllFile
    {
        #region 本程序中用到的API函数声明

        [DllImport("kernel32.DLL")]

        private static extern int GetPrivateProfileString(string section, string key,
                                                          string def, StringBuilder retVal,
                                                          int size, string filePath);
        /*参数说明：section：INI文件中的段落名称；key：INI文件中的关键字；
          def：无法读取时候时候的缺省数值；retVal：读取数值；size：数值的大小；
          filePath：INI文件的完整路径和名称。*/

        [DllImport("kernel32.DLL")]
        private static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);
        /*参数说明：section：INI文件中的段落；key：INI文件中的关键字；
          val：INI文件中关键字的数值；filePath：INI文件的完整的路径和名称。*/

        #endregion

        public static StringBuilder temp = new StringBuilder(255);       //初始化 一个StringBuilder的类型
        public static string str;
        public static string str1;
        public static string str2;

        /*************************************************************************
         *  函数名：    WriteReadParamIniFile                                      *
         *  功能  ：    系统可写可读ini文件                                      *
         *  参数  ：    fname ：路径名                                           *
         *              Type  ：0--读,1--写                                      *
         *              k     ：那一种文件                                       *
         *  返回值：    无                                                       *
         *  修改日期：  2014-09-23                                               *
         *  作者    ：  陈玮                                                   *
         * **********************************************************************/
        public static void WriteReadParamIniFile(string fname, byte Type)
        {

            if (Type == 0)//read
            {
                GetPrivateProfileString("NUM", "MONITOR", "0", temp, 255, fname);
                int loopNum = int.Parse(temp.ToString());//共有loopNum个监测单元
                for (int i = 0; i < loopNum; i++)
                {
                    //读取监视器地址参数
                    str = String.Format("addr_{0:d}", i);
                    GetPrivateProfileString("MONITORADDR", str, "0", temp, 255, fname);
                    int linkAddr = int.Parse(temp.ToString());
                    //建立遥测数据内存
                    GetPrivateProfileString("NUM", "YCNUM", "0",
                                                     temp, 255, fname);
                    DataCollection.YcData ycdata = new DataCollection.YcData();
                    ycdata.num = int.Parse(temp.ToString());
                    ycdata.name = new string[ycdata.num];
                    ycdata.addr = new string[ycdata.num];
                    ycdata.value = new string[ycdata.num];

                    //配置遥测数据
                    for (int j = 0; j < ycdata.num; j++)
                    {
                        str1 = String.Format("YCNAME{0:d}", i);
                        str2 = String.Format("name_{0:d}", j);
                        GetPrivateProfileString(str1, str2, "无法读取对应数值！",
                                                     temp, 255, fname);
                        ycdata.name[j] = temp.ToString();

                        str1 = String.Format("YCADRRS{0:d}", i);
                        str2 = String.Format("addr_{0:d}", j);
                        GetPrivateProfileString(str1, str2, "无法读取对应数值！",
                                                     temp, 255, fname);
                        ycdata.addr[j] = temp.ToString();

                        ycdata.value[j] = "null";

                    }




                    //读遥信配置参数

                    GetPrivateProfileString("NUM", "YXNUM", "0",
                                                         temp, 255, fname);
                    DataCollection.YxData yxdata = new DataCollection.YxData();
                    yxdata.num = int.Parse(temp.ToString());
                    yxdata.name = new string[yxdata.num];
                    yxdata.addr = new string[yxdata.num];
                    yxdata.value = new string[yxdata.num];


                    for (int j = 0; j < yxdata.num; j++)
                    {
                        str1 = String.Format("YXNAME{0:d}", i);
                        str2 = String.Format("name_{0:d}", j);
                        GetPrivateProfileString(str1, str2, "无法读取对应数值！",
                                                     temp, 255, fname);
                        yxdata.name[j] = temp.ToString();

                        str1 = String.Format("YXADRRS{0:d}", i);
                        str2 = String.Format("addr_{0:d}", j);
                        GetPrivateProfileString(str1, str2, "无法读取对应数值！",
                                                     temp, 255, fname);
                        yxdata.addr[j] = temp.ToString();

                        yxdata.value[j] = "null";
                    }
                    DataCollection.ycDatas.Add(linkAddr, ycdata);
                    DataCollection.yxDatas.Add(linkAddr, yxdata);
                    DataCollection.Event eventPerMon = new DataCollection.Event();
                    eventPerMon.addr = new List<string>();
                    eventPerMon.date = new List<string>();
                    eventPerMon.name = new List<string>();
                    eventPerMon.value = new List<string>();
                    DataCollection.events.Add(linkAddr, eventPerMon);
                }

            }

        }
    }
}
