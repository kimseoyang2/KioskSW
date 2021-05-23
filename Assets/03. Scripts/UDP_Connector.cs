using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class UDP_Connector : MonoBehaviour
{
    UdpClient ReceivePort, SendPort;
    IPEndPoint remoteEndPoint;

    public class MyUDPEvent : UnityEvent<string> { }
    public MyUDPEvent OnReceiveMessage = new MyUDPEvent();


    void Start()
    {
        OnReceiveMessage.AddListener(receiveMsg);
    }

    public void Init()
    {
        ReceivePort = new UdpClient(7777);
        ReceivePort.BeginReceive(OnReceive, null);
        Send();
    }

    public void Send(string msg = "receive from Unity")
    {
        SendPort = new UdpClient();

        //string remoteIP = IF_SendIP.text;
        
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
        SendPort.Send(data, data.Length, remoteEndPoint);
    }

    public void StartReceive()
    {
        ReceivePort.BeginReceive(OnReceive, null);
    }


    void OnReceive(IAsyncResult ar)
    {
        try
        {
            
            IPEndPoint ipEndPoint = null;
            byte[] data = ReceivePort.EndReceive(ar, ref ipEndPoint);
            string message = System.Text.Encoding.UTF8.GetString(data);
            print(message);
        }
        catch (SocketException e) { }

        ReceivePort.BeginReceive(OnReceive, null);

    



    }

   
    void receiveMsg(string msg)
    {

        ReceivePort.BeginReceive(OnReceive, null);

   
    }

    void Update()
    {
        //OnReceiveMessage.Invoke(message);
    }

    public void ShutDown()
    {
        OnReceiveMessage.RemoveAllListeners();
        if (ReceivePort != null)
            ReceivePort.Close();
        if (SendPort != null)
            SendPort.Close();
        ReceivePort = null;
        SendPort = null;
    }
}


