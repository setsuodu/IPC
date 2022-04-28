using System;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

public class Demo1 : MonoBehaviour
{
    #region IPC

    //user32.dll中的SendMessage
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, ref COPYDATASTRUCT lParam);
    //user32.dll中的获得窗体句柄
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string strClassName, string strWindowName);

    // 宏定义 
    private const ushort IPC_VER = 1;
    private const ushort IPC_CMD_GF_SOCKET = 1;
    private const ushort IPC_SUB_GF_SOCKET_SEND = 1;
    //private const int IPC_SUB_GF_CLIENT_READY = 1;
    //private const int IPC_CMD_GF_CONTROL = 2;
    private const int IPC_BUFFER = 10240; //最大缓冲长度
    private const int IDT_ASYNCHRONISM = 0x0201;
    private const uint WM_COPYDATA = 0x004A; //74

    // 数据包头配合使用
    public unsafe struct IPC_Head
    {
        public ushort wVersion;
        public ushort wPacketSize;
        public ushort wMainCmdID;
        public ushort wSubCmdID;
    }
    public unsafe struct IPC_Buffer
    {
        public IPC_Head Head;  //IPC_Head结构
        public fixed byte cbBuffer[IPC_BUFFER]; //指针。存放json数据 利用byte[]接收存放 
    }

    private const string winTitle = "winform"; //窗体标题
    private static IntPtr hWndPalaz; //目标窗体
    private static IntPtr m_hWnd; //游戏本身句柄 

    #endregion

    void Start()
    {
        IntPtr hWndPalaz = FindWindow(null, winTitle);
        Debug.Log($"获得游戏本身句柄：{(int)hWndPalaz}");
        m_hWnd = FindWindow("UnityWndClass", null);
    }

    public static void SendJson(string json)
    {
        if (hWndPalaz == null)
        {
            Debug.LogError("找不到目标窗口");
            return;
        }
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        //把json转换为指针发送
        IntPtr pData = Marshal.AllocHGlobal(2 * bytes.Length);
        Marshal.Copy(bytes, 0, pData, bytes.Length);
        SendData(m_hWnd, IPC_CMD_GF_SOCKET, IPC_SUB_GF_SOCKET_SEND, pData, (ushort)bytes.Length);
    }

    /// <summary>
    /// SendMessage发送
    /// </summary>
    /// <param name="hWndServer">指针</param>
    /// <param name="wMainCmdID">主命令</param>
    /// <param name="wSubCmdID">次命令</param>
    /// <param name="pData">json转换的指针</param>
    /// <param name="wDataSize">数据大小</param>
    /// <returns></returns>
    public unsafe static bool SendData(IntPtr hWndServer, ushort wMainCmdID, ushort wSubCmdID, IntPtr pData, ushort wDataSize)
    {
        //给IPCBuffer结构赋值
        IPC_Buffer IPCBuffer;
        IPCBuffer.Head.wVersion = IPC_VER;
        IPCBuffer.Head.wSubCmdID = wSubCmdID;
        IPCBuffer.Head.wMainCmdID = wMainCmdID;
        IPCBuffer.Head.wPacketSize = (ushort)Marshal.SizeOf(typeof(IPC_Head));

        //内存操作
        if (pData != null)
        {
            //效验长度
            if (wDataSize > 1024) return false;
            //拷贝数据
            IPCBuffer.Head.wPacketSize += wDataSize;

            byte[] bytes = new byte[IPC_BUFFER];
            Marshal.Copy(pData, bytes, 0, wDataSize);

            for (int i = 0; i < IPC_BUFFER; i++)
            {
                IPCBuffer.cbBuffer[i] = bytes[i];
            }
        }

        //发送数据
        COPYDATASTRUCT CopyDataStruct;
        IPC_Buffer* pPCBuffer = &IPCBuffer;
        CopyDataStruct.lpData = (IntPtr)pPCBuffer;
        CopyDataStruct.dwData = (IntPtr)IDT_ASYNCHRONISM;
        CopyDataStruct.cbData = IPCBuffer.Head.wPacketSize;
        SendMessage(hWndServer, WM_COPYDATA, (int)m_hWnd, ref CopyDataStruct);
        return true;
    }
}