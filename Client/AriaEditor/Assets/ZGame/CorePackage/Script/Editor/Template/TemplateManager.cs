using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using vietlabs;

namespace Game.Template.Editor
{

    /// <summary>
    /// 用来缓存一些模板数据
    /// </summary>
    public class TemplateManager
    {
        private static string Path = "Assets/ZGame/AssetPackage/Template/";

        protected TemplateManager()
        {
            //不允许外部实例化
        }

        private static TemplateManager _instance = null;

        public static TemplateManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new TemplateManager();
                }
                return _instance;
            }
        }

        public void WriteToFile(ITemplaterWriter write, object data, string fileName)
        {
            string path = Application.dataPath + "/ZGame/AssetPackage/Export/Template/" + fileName + ".bytes";
            ByteArray ba = write.GenerateByteArray(data);

            MemoryStream ms = new MemoryStream(ba.Bytes);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(ms.ToArray());
            fs.Close();
            ms.Close();

            Debug.Log(ba.Bytes.Length);

            AssetDatabase.Refresh();
        }

        private List<FiledPropObject> list;

        /// <summary>
        /// 生成配置表序列化和反序列化的数据类
        /// </summary>
        public void CodeGenerator()
        {
            FieldInfo[] props = typeof(TemplateType).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
            //遍历类型加载模板
            foreach (FieldInfo prop in props)
            {
                TemplateType p = (TemplateType)prop.GetValue(null);
                object[] attrs = prop.GetCustomAttributes(typeof(TemplateDefAttribute), false);
                if (attrs.Length > 0)
                {
                    TemplateDefAttribute def = attrs[0] as TemplateDefAttribute;
                    //read and write class
                    CodeGenerator(def.dataType, def.dataName);
                    //data dictionary class
                    CodeGenerator(def.dataName, def.DataLTypeistType, def.dataType, def.dataDicType);
                };
            }
            ////CodeGenerator("LocalizationData", typeof(List<LanguageVO>), typeof(LanguageVO), typeof(Dictionary<int, LanguageVO>));
        }

        private void CodeGenerator(System.Type type, string dataName)
        {
            list = AutoFindProp(type);

            TemplateCodeGenerator.WriteCodeGenerator(list, dataName + "Writer", "Write", TemplateVOType);
            TemplateCodeGenerator.ReadCodeGenerator(list, dataName + "Reader", "Read", TemplateVOType);
        }

        private void CodeGenerator(string dataName, Type type, Type subType, Type dataType)
        {
            TemplateCodeGenerator.DictionaryGenerator(dataName, type, subType, dataType);
        }

        static string TemplateVOType = null;
        private static List<FiledPropObject> AutoFindProp(System.Type obj)
        {
            TemplateVOType = obj.Name;
            List<FiledPropObject> list = new List<FiledPropObject>();
            FieldInfo[] props = obj.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);//| BindingFlags.DeclaredOnly 

            foreach (FieldInfo prop in props)
            {

                object[] attrs = prop.GetCustomAttributes(typeof(ExcelCellBindingAttribute), false);
                if (attrs.Length > 0)
                {
                    FiledPropObject filedObj = new FiledPropObject() { type = prop.FieldType.Name, name = prop.Name, offset = (attrs[0] as ExcelCellBindingAttribute).offset };
                    list.Add(filedObj);
                    //Debug.Log(prop.FieldType.Name);
                }
            }
            list.Sort(Sort);
            return list;
        }

        private static int Sort(FiledPropObject x, FiledPropObject y)
        {
            return x.offset - y.offset;
        }
    }
}