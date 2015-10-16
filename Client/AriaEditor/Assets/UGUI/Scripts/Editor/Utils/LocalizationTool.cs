using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Excel;
using Game;
using Game.Template.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class LocalizationTool : EditorWindow
{
    static int idx = 0;

    public static void ImportTemplates(string path, string language = "")
    {
        if (!Directory.Exists(Path.GetFullPath(path)))
        {
            Directory.CreateDirectory(Path.GetFullPath(path));
        }

        FieldInfo[] props = typeof(LocalizationTypeDef).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
        //加载程序集
        Assembly assembly = Assembly.GetAssembly(typeof(LanguageDictionary));
        //遍历类型加载模板
        foreach (FieldInfo prop in props)
        {
            LocalizationTypeDef p = (LocalizationTypeDef)prop.GetValue(null);
            object[] attrs = prop.GetCustomAttributes(typeof(LocalizationDefAttribute), false);
            if (attrs.Length > 0)
            {
                LocalizationDefAttribute def = attrs[0] as LocalizationDefAttribute;

                if (language != def.localizeType)
                {
                    continue;
                }

                LanguageDictionary dict = GetAssetData<LanguageDictionary>(def.dataName, def.localizeType, path);
                List<LanguageVO> dataList = LoadExcel(def.path, dict, def.offset, def.index);
                Type type = Type.GetType("Game.Template.Editor.LocalizationTemplateVODataWriter", true, true);
                TemplateManager.Instance.WriteToFile(Activator.CreateInstance(type) as ITemplaterWriter, dataList, def.dataName + "_" + def.localizeType);
            }
        }
    }

    public static T GetAssetData<T>(string assetName, string type, string path) where T : ScriptableObject, new()
    {
        Debug.Log("GetAssetData: " + assetName);
        string sourcePath = path + assetName + "_" + type + ".asset";
        T assetData = AssetDatabase.LoadAssetAtPath(sourcePath, typeof(T)) as T;
        if (assetData == null)
        {
            assetData = ScriptableObject.CreateInstance(typeof(T)) as T;
            AssetDatabase.CreateAsset(assetData, sourcePath);
            Debug.Log("create new asset " + assetName);
        }
        return assetData;
    }

    public static List<LanguageVO> LoadExcel(string xlsName, LanguageDictionary templateDic, int defaultOffset, int SheetIndex = 0)
    {
        string filename = EditorConfig.Instance.ExcelPath + "/" + xlsName;

        if (!filename.EndsWith(".xls"))
        {
            filename += ".xls";
        }

        Debug.Log(filename + " SheetIndex: " + SheetIndex);
        if (!System.IO.File.Exists(filename))
        {
            EditorUtility.DisplayDialog("Error", "file invalid", "ok");
            throw new Exception("filename " + filename + " not exist");
        }
        System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        IExcelDataReader excel = ExcelReaderFactory.CreateBinaryReader(fs);
        DataSet dataSet = excel.AsDataSet();
        DataTable table;
        bool first = true;

        table = dataSet.Tables[SheetIndex];
        first = true;

        if (templateDic.itemList == null)
        {
            templateDic.itemList = new List<LanguageVO>();
        }

        templateDic.itemList.Clear();
        idx = 0;
        try
        {
            foreach (DataRow row in table.Rows)
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                templateDic.itemList.Add(GetVOFromDataRow<LanguageVO>(row, defaultOffset));

                idx++;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("row index " + idx);
            throw e;
        }

        //必须setDirty
        EditorUtility.SetDirty(templateDic);
        AssetDatabase.SaveAssets();
        EditorUtility.ClearProgressBar();

        return templateDic.itemList;
    }

    private static T GetVOFromDataRow<T>(DataRow row, int defaultOffset) where T : LanguageVO, new()
    {
        T instance = new T();

        FieldInfo[] props = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);//| BindingFlags.DeclaredOnly
        foreach (FieldInfo prop in props)
        {
            LoadPropertiesFromDataRow(instance, prop, row, defaultOffset);
        }
        return instance;
    }

    private static void LoadPropertiesFromDataRow(LanguageVO item, FieldInfo prop, DataRow row, int defaultOffset)
    {
        object[] attrs = prop.GetCustomAttributes(typeof(ExcelCellBindingAttribute), false);
        if (attrs.Length > 0)
        {//将对应的值付给他
            int offset = 0;
            object val = new object();
            try
            {
                offset = (attrs[0] as ExcelCellBindingAttribute).offset;
                offset = -1 == offset ? defaultOffset : offset;
                val = row[offset];
                if (prop.FieldType == typeof(string))
                {
                    //Debug.Log("---------------" + val);
                    prop.SetValue(item, chkStringRow(val));
                    //Debug.Log("-----------------------");
                }
                else
                {
                    //Debug.Log("-------------------------" + val);
                    val = ConvertFieldVal(chkRow(val), prop.FieldType);
                    prop.SetValue(item, val);
                }

            }
            catch (Exception e)
            {
                Debug.LogError("name = " + prop.Name + " offset " + offset + " " + val + " idx " + idx);
                Debug.LogError(e.Message);
                //                throw e;
            }
        }
        else
        {//类似于枚举的定义，如稀有度之类的设定
            attrs = prop.GetCustomAttributes(typeof(ExcelCollectionMappingAttribute), false);
            if (attrs.Length > 0)
            {
                System.Type type = (attrs[0] as ExcelCollectionMappingAttribute).type;
                string val = (attrs[0] as ExcelCollectionMappingAttribute).valstr;

                string[] valueArray = val.Split(';');
                Array tempa = Array.CreateInstance(type, valueArray.Length);
                for (int i = 0; i < tempa.Length; i++)
                {
                    //遍历字段并赋值
                    string[] values = valueArray[i].Split(',');
                    if (values.Length > 1)
                    {
                        //获取属性
                        object obj = Activator.CreateInstance(type);
                        FieldInfo[] props = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                        foreach (FieldInfo p in props)
                        {
                            object[] attrDynamics = p.GetCustomAttributes(typeof(ExcelCellBindingDynamicAttribute), false);
                            if (attrDynamics.Length > 0)
                            {
                                int index = (attrDynamics[0] as ExcelCellBindingDynamicAttribute).index;
                                //Debug.Log(p.Name + ":" + chkRow(row[int.Parse(values[index])]));
                                p.SetValue(obj, ConvertFieldVal(chkRow(row[int.Parse(values[index])]), p.FieldType));
                            }
                        }

                        tempa.SetValue(obj, i);
                    }
                    else
                    { //直接赋值
                        int idx = int.Parse(valueArray[i]);
                        tempa.SetValue(ConvertFieldVal(chkRow(row[idx]), type), i);
                    }
                }
                prop.SetValue(item, tempa);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    private static object chkRow(object obj)
    {
        if (obj is DBNull || obj == null || obj.ToString() == "")
            return -1;
        return obj;
    }

    private static string chkStringRow(object obj)
    {
        if (obj is DBNull || obj == null)
            return "";
        return obj as string;
    }

    private static object ConvertFieldVal(object val, Type fieldType)
    {
        if (fieldType == typeof(int))
        {
            return Convert.ToInt32(val);
        }
        else if (fieldType == typeof(long))
        {
            return Convert.ToInt64(val);
        }
        else if (fieldType == typeof(bool))
        {
            return Convert.ToInt32(val) == 1;
        }
        else if (fieldType == typeof(float))
        {
            return Convert.ToSingle(val);
        }
        else if (fieldType == typeof(double))
        {
            return Convert.ToDouble(val);
        }
        else if (fieldType == typeof(string))
        {
            return val as string;
        }
        else
        {
            Debug.LogError("not support to convert this type:" + fieldType.FullName);
        }

        return null;
    }
}