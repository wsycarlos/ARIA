
using System.Xml.Serialization;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    [XmlRoot("config")]
    public class JenkinsConfigMeta
    {
        public JenkinsRunConfig RunConfig;
    }
}
