// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Lecture 4
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


public class Client : MonoBehaviour
{

    [HideInInspector] public PlayerController Player1;
    [HideInInspector] public PlayerController2 Player2;

    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static Socket client_socket;

    private static Socket ServerIP;
    private static IPEndPoint server;
    private static EndPoint remoteServer;

    //Lecture 5
    private float[] pos;
    private byte[] bpos;

    private float[] prevPos;
    public bool changePos;

    private char[] msg;
    private string[] msgOut;
    private static int rec = 0;
    private static byte[] buffer = new byte[512];

    public int playerNum = -1;

    public MessagesManager messagesManager;
    public CameraController cameraController;

    public static void RunClient()
    {
        IPAddress ip = IPAddress.Parse(PlayerPrefs.GetString("ServerIP"));
        remoteEP = new IPEndPoint(ip, 11112);

        client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        ServerIP = new Socket(ip.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        server = new IPEndPoint(IPAddress.Any, 0);
        remoteServer = (EndPoint)server;

        byte[] msg2 = Encoding.ASCII.GetBytes("INIT");
        client_socket.SendTo(msg2, remoteEP);

    }

    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        Player2 = GameObject.Find("Player2").GetComponent<PlayerController2>();

        RunClient();

        //Lecture 05
        client_socket.Blocking = false;

        pos = new float[] { Player1.currentForce.x, Player1.currentForce.y, Player1.currentForce.z };
        bpos = new byte[pos.Length * 4];
        
        prevPos = new float[] { Player1.currentForce.x, Player1.currentForce.y, Player1.currentForce.z };

    }

    // Update is called once per frame
    void Update()
    {
        if (playerNum == 1)
        {
            pos = new float[] { Player1.currentForce.x, Player1.currentForce.y, Player1.currentForce.z };
            prevPos = pos;
        
            Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
        
            client_socket.SendTo(bpos, remoteEP);
        }
        else if (playerNum == 2)
        {
            pos = new float[] { Player2.currentForce.x, Player2.currentForce.y, Player2.currentForce.z };
            prevPos = pos;
        
            Buffer.BlockCopy(pos, 0, bpos, 0, bpos.Length);
        
            client_socket.SendTo(bpos, remoteEP);
        }

        try
        {
            rec = client_socket.ReceiveFrom(buffer, ref remoteServer);
            string ClientMessage = Encoding.ASCII.GetString(buffer, 0, rec);

            if (ClientMessage.Contains("PID"))
            {
                playerNum = int.Parse(ClientMessage.Substring(5));
                if (playerNum == 1)
                {
                    cameraController.Player = Player1.transform;
                }
                else if (playerNum == 2)
                {
                    cameraController.Player = Player2.transform;
                }
            }
            else if (ClientMessage.Substring(0, 3) == "MSG")
            {
                messagesManager.SendMessageToChat(ClientMessage.Substring(4));
            }
            else if (ClientMessage.Substring(0, 2) == "HS")
            {
                messagesManager.SendHSToPanel(ClientMessage.Substring(3));
            }
            else
            {
                if (playerNum == 1)
                {
                    pos = new float[rec / 4];
                    Buffer.BlockCopy(buffer, 0, pos, 0, rec);
            
                    Player2.rb.AddTorque(new Vector3(pos[0], pos[1], pos[2]));
                }
                else if (playerNum == 2)
                {
                    pos = new float[rec / 4];
                    Buffer.BlockCopy(buffer, 0, pos, 0, rec);
            
                    Player1.rb.AddTorque(new Vector3(pos[0], pos[1], pos[2]));
                }
            
            }
        }
        catch
        {

        }
    }

    public void SendClientMessage(string _text)
    {
        byte[] msg2 = Encoding.ASCII.GetBytes(_text);
        client_socket.SendTo(msg2, remoteEP);
    }
}
