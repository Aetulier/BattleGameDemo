using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

public class Test : MonoBehaviour
{

    public const string IP = "127.0.0.1";
    private const int PORT = 2233;
    private Socket clientSocket;
    private byte[] data = new byte[1024];
    private int startIndex = 0;

    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }
    // Start is called before the first frame update
    void Start()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Rec();
            Debug.Log("连接到服务器");
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务端！" + e);
        }
    }
    private void Rec()
    {
        clientSocket.BeginReceive(data, startIndex,RemainSize, SocketFlags.None, ReceiveCallback, null);
    }
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);

            Rec();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Massege.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SendRequest(RequestCode.User, ActionCode.Login, "123,123");
        }
    }
}
