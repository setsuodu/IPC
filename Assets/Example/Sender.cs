using System;
using System.Collections;
using System.Collections.Generic;
using TestHwnd;
using UnityEngine;
using UnityEngine.UI;

public class Sender : MonoBehaviour
{
    public Button btn1;
    public Button btn2;

    void Awake()
    {
        btn1.onClick.AddListener(OnBtn1);
        btn2.onClick.AddListener(OnBtn2);
    }

    void OnBtn1()
    {
        Debug.Log(1);
        Win32API.SendMessage(hwnd_recver, 0x60, 1, 3);
    }

    void OnBtn2()
    {
        Debug.Log(2);
        Win32API.SendMessage(hwnd_recver, 0x63, 5, 6);
    }

    public IntPtr hwnd_recver;
    public int Iwnd_recver;
}