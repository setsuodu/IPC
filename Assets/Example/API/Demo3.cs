using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class Demo3 : MonoBehaviour
{
    static Demo3 _instance;
    public static Demo3 Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject();
                obj.AddComponent<Demo3>();
                obj.name = "Demo3";
                _instance = obj.GetComponent<Demo3>();
            }
            return _instance;
        }
    }

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hwnd, int _nIndex);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow(); //当前活动窗口
    [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
    private static extern int SetForegroundWindow(IntPtr hwnd); //激活窗口

    [DllImport("user32.dll")]
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, int dwFlags);

    [DllImport("user32", CharSet = CharSet.Unicode)]
    public static extern bool SetWindowText(IntPtr hwnd, string title);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [Out] StringBuilder lParam);
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam); //wParam:消息，lParam:指针

    public const int SWP_SHOWWINDOW = 0x0040;
    public const int GWL_EXSTYLE = -20;
    public const int GWL_STYLE = -16;
    public const int WS_CAPTION = 0x00C00000;
    public const int WS_BORDER = 0x00800000;
    public const int WS_EX_LAYERED = 0x80000;
    public const int LWA_COLORKEY = 0x1;
    public const int LWA_ALPHA = 0x2;
    public const int WM_SETTEXT = 0x000C;
    public const int WM_GETTEXT = 0xD;
    public const uint WM_COPYDATA = 0x004A; //74

    private IntPtr handle;

    public Rect screenPosition;

    void Start()
    {
        handle = GetForegroundWindow();

        //SetWindowLong(handle, GWL_EXSTYLE, WS_EX_LAYERED);
        //SetWindowLong(handle, GWL_STYLE, GetWindowLong(handle, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);

        //SetWindowPos(handle, -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);

        //把黑色透明化（不工作）
        //SetLayeredWindowAttributes(handle, 0, 100, LWA_COLORKEY);

        //把整个窗口透明化，工作
        //SetLayeredWindowAttributes(handle, 0, 100, LWA_ALPHA);

        SetWindowText(handle, "编辑器");
    }

    // 获取窗口名
    public void GetWindowText(IntPtr handle)
    {
        // needs to be big enough for the whole text
        StringBuilder sb = new StringBuilder(ushort.MaxValue);
        SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);
        Debug.Log(sb.ToString());
    }

    // 修改窗口名
    public void SetWindowText(IntPtr handle)
    {
        SendMessage(handle, WM_SETTEXT, IntPtr.Zero, new StringBuilder("Hello World!"));
    }
}