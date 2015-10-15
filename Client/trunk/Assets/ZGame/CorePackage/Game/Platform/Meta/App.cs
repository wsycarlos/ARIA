
using System;
using System.Xml.Serialization;


namespace Game.Platform
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class App
    {
        [XmlAttribute]
        public int id;

        [XmlAttribute]
        public string bundleID;

        [XmlAttribute]
        public string lastestVesion;

        [XmlAttribute]
        public bool forceUpdate;

        [XmlAttribute]
        public string updateUrl;

        public Version version;
    }
}
