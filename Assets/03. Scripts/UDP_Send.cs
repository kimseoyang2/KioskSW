using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
public class UDP_Send : MonoBehaviour
{

    // 1. Declare Variables
    Thread receiveThread; //1
    UdpClient Server; //2
    int port; //3

    // Start is called before the first frame update
    void Start()
    {
        port = 64000; //1 
        print("OPEN");

        InitUDP(); //4
    }

    // 3. InitUD
    private void InitUDP()
    {
        print("UDP Initialized");

        receiveThread = new Thread(new ThreadStart(Send)); //1 
        receiveThread.IsBackground = true; //2
        receiveThread.Start(); //3

    }

    public void Send()
    {
        Server = new UdpClient(port);
        string msg = "Receive Fro Unity";
        print("Send to Python");
        //IPEndPoint anyIP1 = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port1); //3
        //byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
        //Sever.Send(data, data.Length, anyIP1);
        //print("Send to Python");

        try
        {
            Server.Connect("127.0.0.1", port);
            byte[] SendBytes = Encoding.ASCII.GetBytes(msg);
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
            Server.Send(SendBytes, SendBytes.Length);
            print("Send to Python");

        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
}
