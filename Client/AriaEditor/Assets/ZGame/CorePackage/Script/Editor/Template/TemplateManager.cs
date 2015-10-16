using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Game.Template.Editor
{
    /// <summary>
    ///     用来缓存一些模板数据
    /// </summary>
    public class TemplateManager
    {
        private static string Path = "Assets/ZGame/AssetPackage/Template/";

        private static TemplateManager _instance;

        private static string TemplateVOType;

        private List<FiledPropObject> list;

        protected TemplateManager()
        {
            //不允许外部实例化
        }

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
            var path = Application.dataPath + "/ZGame/AssetPackage/Export/Template/" + fileName + ".bytes";
            var ba = write.GenerateByteArray(data);

            var ms = new MemoryStream(ba.Bytes);
            var fs = new FileStream(path, FileMode.OpenOrCreate);
            var w = new BinaryWriter(fs);
            w.Write(ms.ToArray());
            fs.Close();
            ms.Close();

            Debug.Log(ba.Bytes.Length);

            AssetDatabase.Refresh();
        }

        /// <summary>
        ///     生成配置表序列化和反序列化的数据类
        /// </summary>
        public void CodeGenerator()
        {
            var props =
                typeof(TemplateType).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly |
                                                BindingFlags.Static);
            //遍历类型加载模板
            foreach (var prop in props)
            {
                var p = (TemplateType)prop.GetValue(null);
                var attrs = prop.GetCustomAttributes(typeof(TemplateDefAttribute), false);
                if (attrs.Length > 0)
                {
                    var def = attrs[0] as TemplateDefAttribute;
                    try
                    {
                        //生成读写模板的代码类
                        CodeGenerator(def.dataType, def.dataName);
                        //生成缓存模板的字典类
                        CodeGenerator(def.dataName, def.DataLTypeistType, def.dataType, def.dataDicType);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        throw;
                    }
                }
                ;
            }

            AssetDatabase.Refresh();
        }

        private void CodeGenerator(Type type, string dataName)
        {
            list = AutoFindProp(type);

            TemplateCodeGenerator.WriteCodeGenerator(list, dataName + "Writer", "Write", TemplateVOType);
            TemplateCodeGenerator.ReadCodeGenerator(list, dataName + "Reader", "Read", TemplateVOType);
        }

        private void CodeGenerator(string dataName, Type type, Type subType, Type dataType)
        {
            TemplateCodeGenerator.DictionaryGenerator(dataName, type, subType, dataType);
        }

        private static List<FiledPropObject> AutoFindProp(Type obj)
        {
            TemplateVOType = obj.Name;
            var list = new List<FiledPropObject>();
            var props = obj.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //| BindingFlags.DeclaredOnly 

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(typeof(ExcelCellBindingAttribute), false);
                if (attrs.Length > 0)
                {
                    var filedObj = new FiledPropObject
                    {
                        type = prop.FieldType.Name,
                        name = prop.Name,
                        offset = (attrs[0] as ExcelCellBindingAttribute).offset
                    };
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