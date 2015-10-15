
using System.Xml.Serialization;
using ZGame;


namespace Game.Platform
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class Server : IData
    {
        [XmlAttribute]
        public int id;

        [XmlAttribute]
        public string serverName;

        [XmlAttribute]
        public string serverIp;

        //[XmlAttribute]
        //public string resourceIp;

        [XmlAttribute]
        public string serverPort;

        [XmlAttribute]
        public int suggested;

        [XmlAttribute]
        public bool serverNew;

        [XmlAttribute]
        public int serverState;

        [XmlAttribute]
        public string requestClientVersion;

        [XmlAttribute]
        public string heartbeat;
    }

    public class ResServer
    {
        [XmlAttribute]
        public int id;

        [XmlAttribute]
        public string serverIp;

        [XmlAttribute]
        public string serverPort;

        [XmlAttribute]
        public int serverState;

        [XmlAttribute]
        public string bundleversion;
    }
}
