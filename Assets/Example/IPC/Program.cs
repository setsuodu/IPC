using System;
using System.Text;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;

public class Program : MonoBehaviour
{
    private static NamedPipeServerStream pipeServer;
    private static bool _cancelled;
    public static string PipeName { private get; set; } = "clouddeskpipeTest";

    void Start()
    {
        Loom.Initialize();
        Loom.RunAsync(WaitData);
    }

    void OnDestroy()
    {
        pipeServer?.Close();
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
                //StreamReader sr = new StreamReader(pipeServer);
                //sr.Close();
                //string con = sr.ReadLine();
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
}