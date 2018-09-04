using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace all_in_one
{
    class MD5Util
    {
        public static string StrToMD5(string str)
         {
            byte[] data = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);
 
            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
             {
                 OutString += OutBytes[i].ToString("x2");
             }
           // return OutString.ToUpper();
            Console.WriteLine(OutString.ToLower());
             return OutString.ToLower();
         }
        
    }
}
