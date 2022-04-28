using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace TestHwnd
{
    // 主窗体，发送消息
    public partial class Main : MonoBehaviour
    {
        public IntPtr hwndTest;
        public int IwndTest;
        public IntPtr hwndfrmTest;

        public Main()
        {
            //InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Test test = new Test();
            //test.Show(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string strTest = "25425";
            Win32API.COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)100;
            cds.lpData = strTest;
            byte[] sarr = System.Text.Encoding.UTF8.GetBytes(strTest);
            int len = sarr.Length;
            cds.cbData = len + 1;

            Win32API.My_lParam lp = new Win32API.My_lParam();
            lp.i = 3;
            lp.s = "test";

            if (hwndTest != (IntPtr)0)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    Win32API.SendMessage(hwndTest, 0x60, 1, 3);//传递2个整型参数成功
                }
                if (DateTime.Now.Second % 3 == 0)
                {
                    Win32API.SendMessage(hwndTest, 0x61, 5, ref lp);//传递整型参数和结构类型成功，这个方法加以改变后可以传递对象
                }
                if (DateTime.Now.Second % 5 == 0)
                {
                    Win32API.SendMessage(hwndTest, 0x62, 5, ref cds);//传递整型参数和不定长的字符串成功
                }
                if (DateTime.Now.Second % 7 == 0)
                {
                    Win32API.PostMessage(hwndTest, 0x63, 5, 6);//传递2个整型参数成功
                }
                if (DateTime.Now.Second % 9 == 0)
                {
                    Win32API.PostMessage(hwndTest, 0x64, 3, ref lp);//传递整型参数成功，但是传递参数lp失败，3可以传递成功。
                }
                if (DateTime.Now.Second % 11 == 0)
                {
                    Win32API.PostMessage(hwndTest, 0x65, 3, ref cds);//传递整型参数成功，传递参数cds失败，3可以传递成功。
                }
            }
        }
    }
}