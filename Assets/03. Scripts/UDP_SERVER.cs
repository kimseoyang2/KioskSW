using UnityEngine;
using System.Collections;
// UDP 통신에 필요한 라이브러리
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP_SERVER : MonoBehaviour
{

    // 1. 변수 선언
    Socket socket;
    EndPoint clientEnd; // 클라이언트
    IPEndPoint ipEnd; // 탐지 포트
    string recvStr; // 받은 문자열
    string sendStr; // 보낸 문자열
    byte[] recvData = new byte[1024]; // 받은 데이터 : 무조건 바이트 형 
    byte[] sendData = new byte[1024]; // 발송된 데이터 : 무조건 바이트 형
    int recvLen; //수신한 데이터 길이
    Thread connectThread; // 연결 스레드


    // 초기화
    public void InitSocket()
    {
        // 
        ipEnd = new IPEndPoint(IPAddress.Any, 64000);
        // 
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // 서버이므로 ip 바인딩 필요함(Python에서 Bind 쓰면 Already Used 발생해서 못썼음)
        socket.Bind(ipEnd);
        // 클라이언트 선언
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        clientEnd = (EndPoint)sender;
        print(" waiting for UDP dgram ");

        // 스레드 연결
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();



    }

    public void StartBtnClicked()
    {
        InitSocket();
    }

    public void SocketSend()
    {
        string sendStr = "Hello From Unity..... ";
        // 보낸 캐시 비우기
        sendData = new byte[1024];
        // 데이터 형식 변환
        sendData = Encoding.ASCII.GetBytes(sendStr);
        // 지정한 클라이언트로 보내기
       
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);

        Debug.Log("Data send!");

    }

    // 서버 수신
    void SocketReceive()
    {
        // 수신 LOOP
        while (true)
        {
            // 
            recvData = new byte[1024];
            // 클라이언트 가져오기, 클라이언트 데이터를 가지고 온다. REF로 변 
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            print("message from : " + clientEnd.ToString()); // 클라이언트 정보 출력
                                                             // 출력
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            print(recvStr);
            // 데이터 전송
            
            
        }
    }

    // 연결 닫기
    public void SocketQuit()
    {
        // 스레드 닫기
        if (connectThread!=null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        // 마지막으로 socket 닫기
        if (socket!= null)
            socket.Close();
        print("disconnect");
    }

    // Use this for initialization
    void Start()
    {
         // 여기에서 초기화
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }

}