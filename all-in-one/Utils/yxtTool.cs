using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Configuration;

namespace all_in_one
{
    class yxtTool
    {
       
        private static string dirPath = "D:\\shobd\\log";
        static yxtTool()
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
        public static string setDate(string dataStr)
        {
           // string rsDate = "";
            DateTime data=DateTime.Parse(dataStr);
            
            return data.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static void logWrite(string logMsg, string fileName)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                string sysLogFilePath = dirPath + "\\" + fileName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                fs = new FileStream(sysLogFilePath, FileMode.Append);
                sw = new StreamWriter(fs);
                logMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"\t"+ logMsg;
                sw.WriteLine(logMsg);
                sw.Flush();

            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public static void logWriteClientMsg(string logMsg, string dirName, string fileName)
        {
            bool flag = Directory.Exists(dirPath+"\\" + dirName);
            if (!flag)
            {
                Directory.CreateDirectory(dirPath + "\\" + dirName);
            }
            string ClientDataFilePath = dirPath + "\\" + dirName + "\\" + fileName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            FileStream fs = new FileStream(ClientDataFilePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            logMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + logMsg;
            sw.WriteLine(logMsg);
            sw.Flush();
            sw.Close();
        }


        public static void logWriteClientBytes(byte[] clientBytes,string type,string dirName)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                bool flag = Directory.Exists(dirPath + "\\" + dirName);
                if (!flag)
                {
                    Directory.CreateDirectory(dirPath + "\\" + dirName);
                }
                string ClientDataFilePath = dirPath + "\\" + dirName + "\\ClientDataLog" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                fs = new FileStream(ClientDataFilePath, FileMode.Append);
                sw = new StreamWriter(fs);
                string logMsg = type + "\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\t") + "";
                for (int i = 0; i < clientBytes.Length; i++)
                {
                    string tempStr = Convert.ToString(clientBytes[i], 16);
                    if (tempStr.Length == 1)
                    {
                        tempStr = "0" + tempStr;
                    }
                    logMsg += tempStr + " ";
                }
                sw.WriteLine(logMsg);
                sw.Flush();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
           
        }
        public static byte[] getCurTime()
        {
            byte[] timeBytes = new byte[6];
            string dateStr=DateTime.Now.ToString("yy-MM-dd-HH-mm-ss");
            string[] strs = dateStr.Split(new char[] { '-' });
            for (int i = 0; i < strs.Length; i++)
            {
                byte temp = byte.Parse(strs[i]);
                timeBytes[i] = temp;
            }
            return timeBytes;
        }

        public static string byteToInt(byte[] bytes,int len) 
        {
            UInt32 temp=0;
            for (int i = len - 1; i >= 0; i--)
            {
                if ((len - i) == 1) 
                {
                    temp+=bytes[i];
                }
                if ((len - i) == 2)
                {
                    temp += (UInt32)bytes[i]*256;
                }
                if ((len - i) == 3)
                {
                    temp += (UInt32)bytes[i] * 65536;
                }
                if ((len - i) == 4)
                {
                    temp += (UInt32)bytes[i] * 16777216;
                }
            }
                return temp + "";
        }



        public static byte[] intToBytes(int i, int len)
        {
            byte[] temp = BitConverter.GetBytes(i);
            switch (len)
            {
                case 2:
                    temp = BitConverter.GetBytes((ushort)i);
                    break;
                case 4:
                    temp = BitConverter.GetBytes((uint)i);
                    break;
                default:
                    temp = BitConverter.GetBytes((uint)i);
                    break;
            }
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return temp;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr +=bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public static string getIpHost()
        {
            System.Net.IPHostEntry myEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            string ipAddress = "";

            //事先不知道ip的个数，数组长度未知，因此用StringCollection储存  
            IPAddress[] localIPs;
            localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            String[] IpCollection = new string[1024];

            foreach (IPAddress ip in localIPs)
            {
                //根据AddressFamily判断是否为ipv4,如果是InterNetWorkV6则为ipv6  
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }           
            return ipAddress;
        }

    }
}
