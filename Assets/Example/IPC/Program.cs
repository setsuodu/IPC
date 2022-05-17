using System;
using System.Text;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;
using System.IO;

public class Program : MonoBehaviour
{
    private static NamedPipeServerStream pipeServer;
    private static bool _cancelled;
    public static string PipeName { private get; set; } = "clouddeskpipeTest";

    void Start()
    {
        Loom.RunAsync(WaitData);
    }

    void OnDestroy()
    {
        Close();
    }

    static void WaitData()
    {
        print("pipeServer initialization");
        while (!_cancelled)
        {
            try
            {
                var buffer = new byte[1024];
                pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 2);
                pipeServer.WaitForConnection();

                var len = pipeServer.Read(buffer, 0, buffer.Length);
                string con = Encoding.UTF8.GetString(buffer, 0, len);

                print("pipeServer content:" + con);

                Thread.Sleep(50);

                print("pipeServer waiting:");
            }
            catch (Exception ex)
            {
                pipeServer?.Dispose();
                print("pipeServer exception:" + ex.Message);
            }
        }
    }

    static void Close()
    {
        if (pipeServer != null && pipeServer.IsConnected)
            return;
        _cancelled = true;
        try
        {
            var pipe = new NamedPipeClientStream(PipeName);
            pipe.Connect(500);
            pipe.Flush();
            pipe.Close();
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            pipeServer?.Close();
        }
    }
}