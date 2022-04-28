using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using UnityEngine;

public class ProcessCommunication : MonoBehaviour
{
    public string content = "";
    public int count;

    void OnGUI()
    {
        content = GUILayout.TextArea(content, GUILayout.Width(200));
        if (GUILayout.Button("SendData"))
        {
            SendData(content + "    " + (++count));
        }
    }

    static void SendData(string str)
    {
        try
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "clouddeskpipeTest", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None))
            {
                pipeClient.Connect();
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.WriteLine(str);
                    sw.Flush();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}