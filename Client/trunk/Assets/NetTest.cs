using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Net.Sockets;
using WebSocket4Net;
using WebSocket4Net.Command;
using WebSocket4Net.Protocol;

public class NetTest : MonoBehaviour {

    // 服务器IP地址和端口号
    private string ServerAdress;

    // 玩家名字
    private string PlayName = "LIN";
    //发送的消息
    private string message;


    //接收到 服务器发回来的消息

    private string ServerMessage;



    void Start()
    {
        // 服务器IP地址和端口号
        ServerAdress = "ws://socketinmono-wsycarlos.rhcloud.com:8000";
        message = "Clients Send Message";


        ServerMessage = "Server Message";
    }


    void Update()
    {/*
        if (websocket != null && websocket.State != WebSocketState.None)
        {
            Debug.Log(message);
            Debug.Log(websocket.State);
        }
      * */
    }





    public WebSocket websocket;

    void OnGUI()
    {
        //绘制2个 TxtBox文本输入框

        PlayName = GUI.TextField(new Rect(10, 10, 100, 20), PlayName);
        message = GUI.TextArea(new Rect(10, 40, 200, 200), message);
        ServerMessage = GUI.TextArea(new Rect(10, 250, 400, 200), ServerMessage);


        //连接到服务器
        if (GUI.Button(new Rect(250, 10, 150, 40), "Client连接"))
        {
            ClientSend();
        };


        if (websocket != null)
        {
            //测试向服务器发送消息
            if (GUI.Button(new Rect(250, 60, 150, 40), "SendMessageToServer"))
            {
                if (message.Length < 1 || PlayName.Length < 1)
                    return;

                websocket.Send("UnityClient Send" + PlayName + "Say:" + message);
            };


            //断开连接按钮
            if (GUI.Button(new Rect(250, 120, 150, 40), "CloseSocket"))
            {
                websocket.Close();
                websocket = null;
            };
        }

    }
    public void ClientSend()
    {
        websocket = new WebSocket(ServerAdress);
        websocket.Opened += new System.EventHandler(websocket_Opened);
        //   websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
        websocket.Closed += new System.EventHandler(websocket_Closed);
        websocket.MessageReceived += new System.EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);

        websocket.Open();
    }

    private void websocket_Opened(object sender, System.EventArgs e)
    {
        websocket.Send(PlayName + "Has Join The Game");
    }

    private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        ServerMessage += e.Message;
    }

    private void websocket_Closed(object sender, System.EventArgs e)
    {
        websocket.Close();
    }
     
         
     
 
}
