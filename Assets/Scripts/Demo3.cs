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

    // 这个不会返回名称
    //[DllImport("user32", CharSet = CharSet.Unicode)]
    //static extern int GetWindowText();
    //[DllImport("user32.dll")]
    //public static extern int GetWindowTextLength(IntPtr hWnd);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [Out] StringBuilder lParam);
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    //[DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
    //public static extern bool ChooseColorA([In, Out] CHOOSECOLOR pChoosecolor);//对应的win32API
    //public class CHOOSECOLOR
    //{
    //    public Int32 lStructSize = Marshal.SizeOf(typeof(CHOOSECOLOR));
    //    public IntPtr hwndOwner;
    //    public IntPtr hInstance;
    //    public Int32 rgbResult;
    //    public IntPtr lpCustColors;
    //    public Int32 Flags;
    //    public IntPtr lCustData = IntPtr.Zero;
    //    public WndProc lpfnHook;
    //    public string lpTemplateName;
    //}

    const int SWP_SHOWWINDOW = 0x0040;
    const int GWL_EXSTYLE = -20;
    const int GWL_STYLE = -16;
    const int WS_CAPTION = 0x00C00000;
    const int WS_BORDER = 0x00800000;
    const int WS_EX_LAYERED = 0x80000;
    const int LWA_COLORKEY = 0x1;
    const int LWA_ALPHA = 0x2;
    const int WM_GETTEXT = 0xD;

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
        Demo3.SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);
        Debug.Log(sb.ToString());
    }
}