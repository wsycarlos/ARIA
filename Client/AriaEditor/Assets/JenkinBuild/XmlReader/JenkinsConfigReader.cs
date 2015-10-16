
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

            string xmlStream = null;
#if UNITY_WEBPLAYER
            FileStream stream = File.OpenRead(fileName);
            byte[] byData = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            char[] charData = new Char[(int)stream.Length];
            stream.Read(byData, 0, (int)stream.Length);
            Decoder d = Encoding.UTF8.GetDecoder();
            d.GetChars(byData, 0, byData.Length, charData, 0);
            xmlStream = new string(charData);
#else
            xmlStream = File.ReadAllText(fileName);
#endif
            return (JenkinsConfigMeta)DeserializeFromString(xmlStream, typeof(JenkinsConfigMeta));
        }
    }
}
