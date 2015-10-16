using Excel;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using Game.Template.Editor;
using UnityEditor;
using UnityEngine;
using vietlabs;


public class TemplateLoadEditor
{
    private static string assetPath = "Assets/ZGame/AssetPackage/Template/";

    public static void ImportTemplates()
    {
        FieldInfo[] props = typeof(TemplateType).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
        //加载程序集
        Assembly assembly = Assembly.GetAssembly(typeof(ClientTemplateDictionary<TemplateVO>));
        //遍历类型加载模板
        foreach (FieldInfo prop in props)
        {
            TemplateType p = (TemplateType)prop.GetValue(null);
            object[] attrs = prop.GetCustomAttributes(typeof(TemplateDefAttribute), false);
            if (attrs.Length > 0)
            {
                TemplateDefAttribute def = attrs[0] as TemplateDefAttribute;
                Type typeParam = def.dicType == null ? assembly.GetType(p.ToString()) : def.dicType;
                //获取泛型方法
                MethodInfo assetMethod = typeof(TemplateLoadEditor).GetMethod("GetAssetData");
                assetMethod = assetMethod.MakeGenericMethod(typeParam);

                MethodInfo loadMethod = typeof(TemplateLoadEditor).GetMethod("LoadExcel");
                loadMethod = loadMethod.MakeGenericMethod(def.dataType);
                //调用方法
                object data = assetMethod.Invoke(null, new object[] { def.dataName });
                loadMethod.Invoke(null, new object[] {
                    def.path,
                    data,
                    def.index
                });

                Type type = Type.GetType("Game.Template.Editor." + def.dataName + "Writer", true, true);
                TemplateManager.Instance.WriteToFile(Activator.CreateInstance(type) as ITemplaterWriter, data.GetField("itemList"), def.dataName);
            }
        }
    }



    /// <summary>
    /// 通用加载或创建数据模板的方法
    /// </summary>
    /// <typeparam name="T">模板类型</typeparam>
    /// <param name="assetName">文件名称</param>
    /// <returns></returns>
    public static T GetAssetData<T>(string assetName) where T : ScriptableObject, new()
    {
        Debug.Log("GetAssetData: " + assetName);
        T assetData = AssetDatabase.LoadAssetAtPath(assetPath + assetName + ".asset", typeof(T)) as T;
        if (assetData == null)
        {
            //注意：scriptable Object 无法实例化泛型类！
            assetData = ScriptableObject.CreateInstance(typeof(T)) as T;
            AssetDatabase.CreateAsset(assetData, assetPath + assetName + ".asset");
            Debug.Log("create new asset " + assetName);
        }
        return assetData;
    }
    static int idx = 0;
    public static void LoadExcel<T>(string xlsName, ClientTemplateDictionary<T> templateDic, int SheetIndex = 0) where T : TemplateVO, new()
    {
        //UTContext con = new UTContext();
        string filename = EditorConfig.Instance.ExcelPath + "/" + xlsName;//con.Evaluate(excelPath) + "\\" + xlsName;

        if (!filename.EndsWith(".xls"))
        {
            filename += ".xls";
        }

        Debug.Log(filename + " SheetIndex: " + SheetIndex);
        if (!System.IO.File.Exists(filename))
        {
            EditorUtility.DisplayDialog("Error", "file " + xlsName + "is not exist", "ok");
            return;
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
            templateDic.itemList = new System.Collections.Generic.List<T>();
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

                templateDic.itemList.Add(GetVOFromDataRow<T>(row));

                idx++;
                EditorUtility.DisplayProgressBar(xlsName + " | " + SheetIndex + " 数据更新", "更新中....", (float)idx / table.Rows.Count);

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
    }

    private static T GetVOFromDataRow<T>(DataRow row) where T : TemplateVO, new()
    {
        T instance = new T();

        FieldInfo[] props = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);//| BindingFlags.DeclaredOnly
        foreach (FieldInfo prop in props)
        {
            LoadPropertiesFromDataRow(instance, prop, row);
        }
        return instance;
    }

    private static void LoadPropertiesFromDataRow(TemplateVO item, FieldInfo prop, DataRow row)
    {
        object[] attrs = prop.GetCustomAttributes(typeof(ExcelCellBindingAttribute), false);
        if (attrs.Length > 0)
        {//将对应的值付给他
            int offset = 0;
            object val = new object();
            try
            {
                offset = (attrs[0] as ExcelCellBindingAttribute).offset;
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


}
