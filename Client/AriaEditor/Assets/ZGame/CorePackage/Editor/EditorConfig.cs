using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EditorConfig : ScriptableObject
{
    private static string Path = "Assets/ZGame/CorePackage/Editor/";
    private static EditorConfig instance;
    public static EditorConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = AssetDatabase.LoadAssetAtPath(Path + "editorConfig.asset", typeof(EditorConfig)) as EditorConfig;
                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<EditorConfig>();
                    AssetDatabase.CreateAsset(instance, "editorConfig.asset");
                }
            }
            return instance;
        }
    }

    public string MainPath;
    public string BundlePath;
    public string EditorPath;
    public string ExcelPath;
    public string BuidlSettingsPath;
}