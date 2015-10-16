using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Game.UI;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class ReFreshUIConfig
{
    private static string _folderPath = Application.dataPath + "/ZGame/AssetPackage/Export/";
    private static string[] _path = { "Prefab/UI/:*prefab" };
    static List<UnityEngine.Object> _objects = new List<UnityEngine.Object>();

    public static void RefreshUIConfig()
    {
        _objects.Clear();
        foreach (string fileURL in _path)
        {
            string[] array = fileURL.Split(':');
            DirectoryInfo directoryInfo = new DirectoryInfo(_folderPath + array[0]);

            GetPrefabRecursively(directoryInfo, _objects, array[1]);
        }

        string sourcePath = "Assets/ZGame/AssetPackage/Export/Config/uiConfig.asset";
        UIConfigDictionary assetData = AssetDatabase.LoadAssetAtPath(sourcePath, typeof(UIConfigDictionary)) as UIConfigDictionary;

        if (null == assetData)
        {
            assetData = ScriptableObject.CreateInstance(typeof(UIConfigDictionary)) as UIConfigDictionary;
            AssetDatabase.CreateAsset(assetData, sourcePath);
        }

        assetData.itemList.Clear();
        int index = 0;
        foreach (UnityEngine.Object obj in _objects)
        {
            UIConfig def = new UIConfig() { isCache = false, id = index, name = obj.name, scripts = GetScriptname(obj.name) };
            assetData.itemList.Add(def);
            index++;
        }

        EditorUtility.SetDirty(assetData);
        AssetDatabase.SaveAssets();
    }

    private static string[] GetScriptname(string name)
    {
        string[] nameArray = name.Split('_');
        if (null == nameArray || nameArray.Length <= 1)
        {
            return new string[] { "Game.UI." + name };
        }

        string result = "";
        foreach (string s in nameArray)
        {
            string first = s.Substring(0, 1);
            result += first.ToUpper() + s.Substring(1, s.Length - 1).ToLower();
        }

        //需要设置命名空间
        return new string[] { "Game.UI." + result };
    }

    private static void GetPrefabRecursively(DirectoryInfo directoryInfo, List<UnityEngine.Object> objects, string fileURL)
    {
        foreach (FileInfo fileInfo in directoryInfo.GetFiles(fileURL))
        {
            string fileName = "Assets" + fileInfo.FullName.Replace('\\', '/').Replace(Application.dataPath, "");
            UnityEngine.Object newObj = AssetDatabase.LoadMainAssetAtPath(fileName);
            objects.Add(newObj);
            Debug.Log(fileName);
        }

        foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
        {
            GetPrefabRecursively(subDirectoryInfo, objects, fileURL);
        }
    }
}
