using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP_POST : MonoBehaviour
{

    // 1. Declare Variables
    Thread receiveThread; //1
    
    UdpClient client; //2
    UdpClient Sever;
    int port; //3
   

    public GameObject Player; //4
    AudioSource jumpSound; //5
    bool jump; //6

    

    // 2. Initialize variables
    void Start()
    {
        port = 64000; //1
      
        jump = false; //2 
        jumpSound = gameObject.GetComponent<AudioSource>(); //3

        InitUDP(); //4
    }

    // 3. InitUDP
   public void InitUDP()
    {
        print("UDP Initialized");

        receiveThread = new Thread(new ThreadStart(ReceiveData)); //1 
        receiveThread.IsBackground = true; //2
        receiveThread.Start(); //3

       

    }

    public void Send()
    {
        Sever = new UdpClient(port);
        string msg = "Receive Fro Unity";
        //IPEndPoint anyIP1 = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port1); //3
        //byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
        //Sever.Send(data, data.Length, anyIP1);
        //print("Send to Python");

        try
        {
            Sever.Connect("127.0.0.1", port);
            byte[] SendBytes = Encoding.ASCII.GetBytes(msg);
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
            Sever.Send(SendBytes, SendBytes.Length);
            
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
    
    // 4. Receive Data
    public void ReceiveData()
    {
        client = new UdpClient(port); //1
        while (true) //2
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
                byte[] data = client.Receive(ref anyIP); //4

                string text = Encoding.UTF8.GetString(data); //5
                print(">> " + text);

                jump = true; //팝업창 띄우기

            }
            catch (Exception e)
            {
                print(e.ToString()); //7
            }
        }
    }

    // 5. Make the Player Jump
    public void Popup()
    {
       
    }

    // 6. Check for variable value, and make the Player Jump!
    void Update()
    {
       
    }
}