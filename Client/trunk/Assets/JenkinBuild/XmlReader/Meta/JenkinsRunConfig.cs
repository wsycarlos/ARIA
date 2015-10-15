
using System.Xml.Serialization;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class JenkinsRunConfig
    {
        [XmlAttribute]
        public string debug;

        [XmlAttribute]
        public string language;

        [XmlAttribute]
        public string unityplatform;

        [XmlAttribute]
        public string sdkplatform;

        [XmlAttribute]
        public string serverurl;

        [XmlAttribute]
        public string bundleIdentifier;

        [XmlAttribute]
        public string bundleversion;

        [XmlAttribute]
        public string bundleversioncode;
    }
}
