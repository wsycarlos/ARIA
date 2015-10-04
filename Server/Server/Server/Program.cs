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
            Dictionary<string, Player> PlayerList = new Dictionary<string, Player>();

            List<Player> Player__List = new List<Player>();

            var appServer = new WebSocketServer();

            //服务器IP地址
                string ip = args[0];
                int port = int.Parse(args[1]);

            //Setup the appServer
            if (!appServer.Setup(ip,port)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                return;
            }

            appServer.NewSessionConnected += new SessionHandler<WebSocketSession>(appServer_NewClientConnected);

            appServer.NewMessageReceived += new SessionHandler<WebSocketSession, string>(appServer_NewMessageReceived);

            appServer.SessionClosed += new SessionHandler<WebSocketSession, CloseReason>(appServer_SessionClosed);

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                return;
            }

            Console.WriteLine("服务器启动成功, 按 'q' 退出服务器!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
        }



        static void appServer_NewClientConnected(WebSocketSession session)
        {
            session.Send("第一次给客户端发信息Unity3DServer: ");

            Player ps = new Player(session.SessionID);

            session.Send(MakeDataToString.PlayerString(ps));

            Console.WriteLine("客户端 :端口" + session.RemoteEndPoint.Port + "连接到服务器了!");

            ClientNum += 1;

            foreach (var ses in session.AppServer.GetAllSessions())
            {
                ses.Send("xxx加入了游戏");
            }
        }



        static void appServer_NewMessageReceived(WebSocketSession session, string message)
        {
            session.Send("欢迎登陆本系统LinMengUnity3DServer: ");

            Console.WriteLine("有客户端消息" + message);

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

        public class Player
        {
            public string sessionID { get; set; }

            public string Name { get; set; }

            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public Player(string id)
            {
                this.sessionID = id;
                Name = sessionID.Substring(0, 6);
                X = -0.66666F;
                Y = 1.59666F;
                Z = 0;
            }
        }

        public class MakeDataToString
        {
            public static string PlayerString(Player p)
            {
                return IDstr(p) + Namestr(p) + Xstr(p) + Ystr(p) + Zstr(p);
            }

            public static string IDstr(Player p)
            {
                return "<id>" + p.sessionID + "</id>";
            }

            public static string Namestr(Player p)
            {
                return "<name>" + p.Name + "</name>";
            }

            public static string Xstr(Player p)
            {
                return "<X>" + p.X + "</X>";
            }

            public static string Ystr(Player p)
            {
                return "<Y>" + p.Y + "</Y>";
            }

            public static string Zstr(Player p)
            {
                return "<Z>" + p.Z + "</Z>";
            }
        }
    }
}