
using System.Xml.Serialization;


namespace Game.Platform
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class Operator
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string managerAccountURL;

        [XmlAttribute]
        public string registerURL;
    }
}
