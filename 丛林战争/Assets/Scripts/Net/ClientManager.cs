using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

public class ClientManager : BaseManager {
    public const string IP = "47.98.40.164"; //连接IP可修改为远程服务端ip
    private const int PORT = 2233;
    private Socket clientSocket;
    private Massege msg=new Massege();
    public ClientManager(GameFacade facade) : base(facade) { }
    public override void OnInit()
    {
        base.OnInit();       
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务端！" + e);
        }
    }
    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
    }
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallback);
            Start();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    private void OnProcessDataCallback(ActionCode actionCode,string data)
    {
        Debug.Log(data);
        Debug.Log(actionCode);
        facade.HandleReponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Massege.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭服务端连接！" + e);
        }
    }

}
