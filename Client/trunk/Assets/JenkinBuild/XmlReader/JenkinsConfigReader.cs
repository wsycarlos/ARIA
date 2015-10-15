
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using Object = System.Object;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class JenkinsConfigReader
    {
        #region Deserialize from string
        public static Object DeserializeFromString(string xmlString, Type type)
        {
            return DeserializeFromString(xmlString, type, null, null);
        }

        public static Object DeserializeFromString(string xmlString, Type type, XmlRootAttribute root, XmlAttributeOverrides overrides)
        {
            if (!string.IsNullOrEmpty(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(type, overrides, null, root, string.Empty);
                UTF8Encoding encoding = new UTF8Encoding();
                MemoryStream stream = new MemoryStream(encoding.GetBytes(xmlString));
                return serializer.Deserialize(stream);
            }
            else
            {
                Debug.LogError("Xml string is empty");
            }

            return null;
        }
        #endregion

        #region Serialize to file
        public static void SerializeToFile(string filePath, Object obj)
        {
            SerializeToFile(filePath, obj, null, null);
        }

        public static void SerializeToFile(string filePath, Object obj, XmlRootAttribute root, XmlAttributeOverrides overrides)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType(), overrides, null, root, string.Empty);

            StreamWriter streamWriter = new StreamWriter(filePath, false, new UTF8Encoding());
            xmlSerializer.Serialize(streamWriter, obj);

            streamWriter.Close();
        }
        #endregion

        public static JenkinsConfigMeta ReadConfig(string fileName)
        {
            return (JenkinsConfigMeta)DeserializeFromString(File.ReadAllText(fileName), typeof(JenkinsConfigMeta));
        }
    }
}
