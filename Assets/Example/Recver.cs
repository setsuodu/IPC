using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Recver : MonoBehaviour
{
    void Start()
    {
        
    }

    //钩子回调
    private unsafe int Hook(int nCode, int wParam, int lParam)
    {
        try
        {
            IntPtr p = new IntPtr(lParam);
            CWPSTRUCT m = (CWPSTRUCT)Marshal.PtrToStructure(p, typeof(CWPSTRUCT));

            /*
            if (m.message == 74)
            {
                COPYDATASTRUCT entries = (COPYDATASTRUCT)Marshal.PtrToStructure((IntPtr)m.lparam, typeof(COPYDATASTRUCT));
                IPC_Buffer entries1 = (IPC_Buffer)Marshal.PtrToStructure((IntPtr)entries.lpData, typeof(IPC_Buffer));

                IntPtr intp = new IntPtr(entries1.cbBuffer);
                string str = new string((sbyte*)intp);
                print("json数据：" + str);
                mOutput.text = str;
            }
            if (CallNextProc)
            {
                return CallNextHookEx(idHook, nCode, wParam, lParam);
            }
            else
            {
                //return 1;
                return CallNextHookEx(idHook, nCode, wParam, lParam);
            }
            */
            return 0;
        }
        catch (Exception ex)
        {
            print(ex.Message);
            return 0;
        }
    }
}