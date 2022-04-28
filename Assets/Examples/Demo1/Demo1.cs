using System;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

public class Demo1 : MonoBehaviour
{
    #region IPC

    public IntPtr m_hWnd;
    /// <summary>
    /// 发送windows消息方便user32.dll中的SendMessage函数使用
    /// </summary>
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;
    }
    //user32.dll中的SendMessage
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, ref COPYDATASTRUCT lParam);
    //user32.dll中的获得窗体句柄
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string strClassName, string strWindowName);
    //宏定义 
    private const ushort IPC_VER = 1;
    private const int IDT_ASYNCHRONISM = 0x0201;
    private const uint WM_COPYDATA = 0x004A;
    private const ushort IPC_CMD_GF_SOCKET = 1;
    private const ushort IPC_SUB_GF_SOCKET_SEND = 1;
    private const int IPC_SUB_GF_CLIENT_READY = 1;
    private const int IPC_CMD_GF_CONTROL = 2;
    private const int IPC_BUFFER = 10240;//最大缓冲长度
    //查找的窗体
    private IntPtr hWndPalaz;

    //数据包头配合使用
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
        public fixed byte cbBuffer[IPC_BUFFER]; //指针        存放json数据 利用byte[]接收存放 
    }

    #endregion

    /// <summary>
    /// 发送把json转换为指针传到SendData()方法
    /// </summary>
    private void sendJson()
    {
        IntPtr hWndPalaz = FindWindow(null, "New Unity Project");//就是窗体的的标题
        if (hWndPalaz != null)
        {
            Debug.Log("获得游戏本身句柄：" + (int)hWndPalaz);
            //获得游戏本身句柄 
            m_hWnd = FindWindow("UnityWndClass", null);
            //发送用户准备好消息（这个是个json插件我就不提供了你们自己搞自己的json new一个实例这里不改会报错）
            JsonModel model = new JsonModel();
            model.name = "mike";
            model.age = 12;
            model.area = "NY";
            string json = JsonUtility.ToJson(model);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            IntPtr pData = Marshal.AllocHGlobal(2 * bytes.Length);
            Marshal.Copy(bytes, 0, pData, bytes.Length);
            SendData(m_hWnd, IPC_CMD_GF_SOCKET, IPC_SUB_GF_SOCKET_SEND, pData, (ushort)bytes.Length);
        }
        else
        {
            Debug.Log("找不到窗口");
        }
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
    public unsafe bool SendData(IntPtr hWndServer, ushort wMainCmdID, ushort wSubCmdID, IntPtr pData, ushort wDataSize)
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
        SendMessage(hWndServer, 0x004A, (int)m_hWnd, ref CopyDataStruct);
        return true;
    }

    void Update()
    {
        sendJson();//一直发送方便测试
    }
}

public class JsonModel
{
    public string name;
    public int age;
    public string area;
}