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
    // �Ӵ��������Ϣ�Լ�����
    public partial class Test : MonoBehaviour
    {
        Main main;

        public Test()
        {
            //InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //main = this.Owner as Main;
            //main.hwndTest = this.Handle;
        }

        /*
        ///��д�������Ϣ������DefWndProc�����м����Լ�������Ϣ�ļ��Ĵ������
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //�����Զ�����ϢMYMESSAGE������ʾ�����
                case 0x60:
                    {
                        label1.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + m.LParam.ToInt32().ToString();
                    }
                    break;
                case 0x61:
                    {
                        Win32API.My_lParam ml = new Win32API.My_lParam();
                        Type t = ml.GetType();
                        ml = (Win32API.My_lParam)m.GetLParam(t);
                        label2.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + ml.i.ToString() + ":" + ml.s;
                    }
                    break;
                case 0x62:
                    {
                        Win32API.COPYDATASTRUCT mystr = new Win32API.COPYDATASTRUCT();
                        Type mytype = mystr.GetType();
                        mystr = (Win32API.COPYDATASTRUCT)m.GetLParam(mytype);
                        string str2 = mystr.lpData;
                        label3.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + str2;
                    }
                    break;
                case 0x63:
                    {
                        label4.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + m.LParam.ToInt32().ToString();
                    }
                    break;
                case 0x64:
                    {
                        Win32API.My_lParam ml = new Win32API.My_lParam();
                        Type t = ml.GetType();
                        ml = (Win32API.My_lParam)m.GetLParam(t);
                        label5.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + ml.i.ToString() + ":" + ml.s;
                    }
                    break;
                case 0x65:
                    {
                        Win32API.COPYDATASTRUCT mystr = new Win32API.COPYDATASTRUCT();
                        Type mytype = mystr.GetType();
                        mystr = (Win32API.COPYDATASTRUCT)m.GetLParam(mytype);
                        string str2 = mystr.lpData;
                        label6.Text = DateTime.Now.ToString() + "-" + m.WParam.ToInt32().ToString() + "-" + str2;
                    }
                    break;
                //default:
                //    base.DefWndProc(ref m);
                //    break;
            }
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            main.hwndTest = (IntPtr)(0);
            //this.Close();
        }
    }
}