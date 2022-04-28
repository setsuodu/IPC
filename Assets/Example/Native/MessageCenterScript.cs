using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.PInvoke;
using Debug = UnityEngine.Debug;

public class MessageCenterScript : MonoBehaviour
{
    /// <summary>
    ///  注意，不知道的不要传空字符串.传NULL
    /// </summary>
    private const string ReceiveWindowClassName = null;
    //private const string ReceiveWindowWindowName = "Wemew_Client_MainWindow";
    private const string ReceiveWindowWindowName = "noname";

    private IntPtr _hookId;
    public Text ReceiveMessageText;
    public Text SendMessageText;
    private NativeMethods.CBTProc proc;

    void Start()
    {
        proc = OnReceivedMessage;
        var curProcess = Process.GetCurrentProcess();
        var curModule = curProcess.MainModule;
        var windowHandle = NativeMethods.GetModuleHandle(curModule.ModuleName);
        _hookId = NativeMethods.SetWindowsHookEx(NativeMethods.HookType.WH_CALLWNDPROC, proc, windowHandle, (uint)AppDomain.GetCurrentThreadId());
    }

    void FixedUpdate()
    {
        var dataProtocol = new DataProtocol
        {
            MessageCode = "1000",
            MessageContent = "消息内容,可以发送业务JSON字符字符串",
            SendTimeLocal = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        SendMessageToHost(dataProtocol);
    }

    void OnDestroy()
    {
        if (_hookId != IntPtr.Zero)
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }
    }

    public Text printText;

    /// <summary>
    ///     此函数用于接收消息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    private IntPtr OnReceivedMessage(int code, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            var m = (NativeMethods.CWPSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.CWPSTRUCT));
            printText.text = $"收到消息：{m.message}";

            if (m.message == NativeMethods.WM_COPYDATA)
            {
                var copyDataStruct = (NativeMethods.COPYDATASTRUCT)Marshal.PtrToStructure(m.lparam, typeof(NativeMethods.COPYDATASTRUCT));
                var protocolStruct = (NativeMethods.ProtocolStruct)Marshal.PtrToStructure(copyDataStruct.lpData, typeof(NativeMethods.ProtocolStruct));
                var receivedMessage = "接收到消息:" + protocolStruct.Message;
                ReceiveMessageText.text = receivedMessage;
                Debug.Log(receivedMessage);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
        return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
    }

    /// <summary>
    ///     此函数用于发送数据到Host程序
    /// </summary>
    private void SendMessageToHost(DataProtocol dataProtocol)
    {
        var hTargetWnd = NativeMethods.FindWindow(null, ReceiveWindowWindowName);
        if (hTargetWnd == IntPtr.Zero)
        {
            const string errorMessage = "当前未发现HOST程序.";
            Debug.Log(errorMessage);
            SendMessageText.text = errorMessage;
            return;
        }

        NativeMethods.ProtocolStruct protocolStruct;
        //var messageString = JsonUtility.ToJson(dataProtocol);
        var messageString = Guid.NewGuid().ToString("N");
        protocolStruct.Message = messageString;
        var protocolStructSize = Marshal.SizeOf(protocolStruct);
        var protocolStructLpData = Marshal.AllocHGlobal(protocolStructSize);
        try
        {
            Marshal.StructureToPtr(protocolStruct, protocolStructLpData, true);
            var cds = new NativeMethods.COPYDATASTRUCT
            {
                cbData = protocolStructSize,
                lpData = protocolStructLpData
            };

            NativeMethods.SendMessage(hTargetWnd, NativeMethods.WM_COPYDATA, IntPtr.Zero, ref cds);

            var result = Marshal.GetLastWin32Error();
            var sendResultMessage = $"发送消息:{messageString} 返回:{result}";
            SendMessageText.text = sendResultMessage;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
        finally
        {
            Marshal.FreeHGlobal(protocolStructLpData);
        }
    }
}