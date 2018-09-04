using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace all_in_one.QRCode
{
    class VbarApi
    {
        public VbarApi()
        {     
        }
        IntPtr dev;
        //打开设备dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vbar_open(String addr, long parm);
        //背光控制dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_backlight", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vbar_backlight(IntPtr dev, bool toset);
        //蜂鸣器控制dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_beep", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vbar_beep(IntPtr dev, int times);
        //判断设备是否已连接dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_is_connected", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vbar_is_connected(IntPtr dev);
        //添加要支持的码制dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_add_symbol_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vbar_add_symbol_type(IntPtr dev, int type);
        //扫码dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_scan", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool vbar_scan(IntPtr dev, ref int type, [MarshalAs(UnmanagedType.LPArray)] byte[] result_buffer, ref int size);
        //关闭设备dll
        [DllImport("libvbar.dll", EntryPoint = "vbar_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vbar_close(IntPtr dev);

        //连接设备
        public bool openDevice(string addr)
        {
            dev = vbar_open(addr, 0);
            if (dev == IntPtr.Zero)
            {
                //MessageBox.Show("连接设备失败，请检查设备是否插入或重试!", "设备连接信息");
                return false;
            }
            else
            {
                //MessageBox.Show("连接设备成功!", "设备连接信息");
                return true;
            }
        }
        //背光控制
        public bool Backlight(bool state)
        {
            if (vbar_backlight(dev, state))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Beep(int times)
        {
            if (vbar_beep(dev, times))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //判断设备是否连接
        public bool IsConnected()
        {
            if (vbar_is_connected(dev))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //关闭设备
        public void CloseDevice()
        {
            vbar_close(dev);
        }

        //码制类型
        public enum CodeType
        {
            VBAR_SYM_NONE = 0,        /* 空类型, 用于清空 */
            VBAR_SYM_QRCODE = 1,   //qrcode
            VBAR_SYM_EAN8 = 2,        //ean8
            VBAR_SYM_EAN13 = 3,     //ean13
            VBAR_SYM_ISBN13 = 4,   //isbn13
            VBAR_SYM_CODE39 = 5,   //code39
            VBAR_SYM_CODE93 = 6,   //code93
            VBAR_SYM_CODE128 = 7,  //code128
            VBAR_SYM_DATABAR = 8,  //databar
            VBAR_SYM_DATABAR_EXP = 9, //databar_exp
            VBAR_SYM_PDF417 = 10,  //pdf417
            VBAR_SYM_DATAMATRIX = 11,  //datamatrix
            VBAR_SYM_ITF = 12,   //itf
            VBAR_SYM_ISBN10 = 13,   //isbn10
            VBAR_SYM_UPCA = 15,   //upca
        }
        //添加码制
        public bool AddSymbolType(int type)
        {
            if (vbar_add_symbol_type(dev, type))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //扫码

        public bool Scan(out byte[] result)
        {
            int c_type = 0;
            int c_size = 0;
            byte[] c_result = new byte[256];
            if (vbar_scan(dev, ref  c_type, c_result, ref  c_size))
            {
                result = c_result;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}
