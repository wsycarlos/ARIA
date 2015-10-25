using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperWebSocket;

namespace Server
{
    class Program
    {
        static int ClientNum = 0;

        static void Main(string[] args)
        {
            WebSocketServer appServer = new WebSocketServer();

            string ip = args[0];
            int port = int.Parse(args[1]);

            if (!appServer.Setup(ip, port))
            {
                Console.WriteLine("Failed to setup!");
                return;
            }

            appServer.NewSessionConnected += appServer_NewSessionConnected;

            appServer.NewDataReceived += appServer_NewDataReceived;

            appServer.SessionClosed += appServer_SessionClosed;

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                return;
            }

            Console.WriteLine("Press Q to Stop Server");

            while (Console.ReadKey().KeyChar != 'q')
            {
                continue;
            }

            appServer.Stop();

            Console.WriteLine("The Server was Stopped!");
        }

        static void appServer_NewSessionConnected(WebSocketSession session)
        {
            session.Send("第一次给客户端发信息Unity3DServer: ");

            Console.WriteLine("客户端 :端口" + session.RemoteEndPoint.Port + "连接到服务器了!");

            ClientNum += 1;

            foreach (var ses in session.AppServer.GetAllSessions())
            {
                ses.Send("xxx加入了游戏");
            }
        }

        static void appServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            session.Send("欢迎登陆本系统LinMengUnity3DServer: ");

            Console.WriteLine("有客户端消息");

            Console.WriteLine("客户端数目" + ClientNum.ToString());
            foreach (var ses in session.AppServer.GetAllSessions())
            {
                ses.Send("给所有客户端广播发送的消息LinMeng广播电台");
            }
        }

        static void appServer_SessionClosed(WebSocketSession session, CloseReason closeRs)
        {
            session.Close();
            Console.WriteLine("客户端" + session.RemoteEndPoint.Port + "断开了连接！");
            ClientNum -= 1;

            Console.WriteLine("客户端数目" + ClientNum.ToString());
        }


    }
}