using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMessage : MonoBehaviour
{
    // ¶¨ÒåÎ¯ÍÐ
    public delegate IntPtr CallBack(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    public IntPtr OldWndProc = IntPtr.Zero;
    private CallBack _WndProc = null;

    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, CallBack wndProc);
    [DllImport("user32.dll")]
    static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, int nIndex, CallBack wndProc);

    const int GWL_WNDPROC = -4;
    const int WM_SIZE = 0x0005;

    IntPtr CustomWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
    {
        Debug.LogError(msg);

        if (msg == WM_SIZE)
        {

        }
        return CallWindowProc(OldWndProc, hWnd, msg, wParam, lParam);
    }

    void Start()
    {
        this._WndProc = new CallBack(CustomWndProc);
        //OldWndProc = SetWindowLong(GetForegroundWindow(), GWL_WNDPROC, this._WndProc);
        OldWndProc = SendMessage(GetForegroundWindow(), GWL_WNDPROC, this._WndProc);
    }
}