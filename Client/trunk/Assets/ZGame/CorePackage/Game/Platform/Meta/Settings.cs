
using System.Xml.Serialization;


namespace Game.Platform
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    [XmlRoot("setting")]
    public class Settings
    {
        public Operator operators;

        [XmlArray("servers")]
        public Server[] server;

        [XmlArray("resservers")]
        public ResServer[] resServer;

        public PlatFormUrl platformUrl;

        [XmlArrayAttribute("appMap")]
        public App[] app;
    }
}
