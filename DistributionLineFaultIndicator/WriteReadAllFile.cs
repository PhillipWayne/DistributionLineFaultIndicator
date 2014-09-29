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

                GetPrivateProfileString("NUM", "YCNUM", "无法读取对应数值！",
                                                     temp, 255, fname);
                DataCollection.YcData.num = int.Parse(temp.ToString());
                DataCollection.YcData.name = new string[DataCollection.YcData.num];
                DataCollection.YcData.addr = new string[DataCollection.YcData.num];
                DataCollection.YcData.value = new string[DataCollection.YcData.num];


                for (int j = 0; j < DataCollection.YcData.num; j++)
                {
                    str = String.Format("name_{0:d}", j);
                    GetPrivateProfileString("YCNAME", str, "无法读取对应数值！",
                                                 temp, 255, fname);
                    DataCollection.YcData.name[j] = temp.ToString();

                    str = String.Format("addr_{0:d}", j);
                    GetPrivateProfileString("YCADRRS", str, "无法读取对应数值！",
                                                 temp, 255, fname);
                    DataCollection.YcData.addr[j] = temp.ToString();

                    DataCollection.YcData.value[j] = "null";

                }





                //读遥信配置参数

                GetPrivateProfileString("NUM", "YXNUM", "无法读取对应数值！",
                                                     temp, 255, fname);
                DataCollection.YxData.num = int.Parse(temp.ToString());
                DataCollection.YxData.name = new string[DataCollection.YxData.num];
                DataCollection.YxData.addr = new string[DataCollection.YxData.num];
                DataCollection.YxData.value = new string[DataCollection.YxData.num];


                for (int j = 0; j < DataCollection.YxData.num; j++)
                {
                    str = String.Format("name_{0:d}", j);
                    GetPrivateProfileString("YXNAME", str, "无法读取对应数值！",
                                                 temp, 255, fname);
                    DataCollection.YxData.name[j] = temp.ToString();

                    str = String.Format("addr_{0:d}", j);
                    GetPrivateProfileString("YXADRRS", str, "无法读取对应数值！",
                                                 temp, 255, fname);
                    DataCollection.YxData.addr[j] = temp.ToString();

                    DataCollection.YxData.value[j] = "null";

                }


            }

        }
    }
}
