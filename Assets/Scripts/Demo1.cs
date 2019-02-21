using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Demo1 : MonoBehaviour
{
    public Text m_text;

    void Start()
    {
        m_text.text = "哈哈";
    }

    #region WIN32API

    //[DllImport("user32", CharSet = CharSet.Unicode)]
    //static extern void GetWindowText();
    //static extern  int GetWindowText（HWND hWnd，LPTSTR lpString，Int nMaxCount）;

    #endregion
}
