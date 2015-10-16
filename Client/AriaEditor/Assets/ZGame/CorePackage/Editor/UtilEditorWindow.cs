using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using Object = UnityEngine.Object;

public enum EditorSDKType
{
    SEVENGA,
    SEVENGA_RELEASE,
}

public class UtilEditorWindow : EditorWindow
{
    public static UtilEditorWindow instance;
    public string[] paths = new string[3];
    public Dictionary<string, string> pathDic = new Dictionary<string, string>();
    public int toolbarInt = 0;
    public string[] toolbarStrings = new string[] 
    {
        "项目设置",
    };

    [MenuItem("ZEditor/Editor Config")]
    static void Init()
    {
        instance = (UtilEditorWindow)EditorWindow.GetWindow(typeof(UtilEditorWindow));
    }

    void OnDestroy()
    {
        AssetDatabase.SaveAssets();
    }


    string DrawEditorConfigProperty(string key, string value)
    {
        EditorGUILayout.BeginHorizontal();
        value = EditorGUILayout.TextField(key, value);
        if (GUILayout.Button("选择目录"))
        {
            value = EditorUtility.OpenFolderPanel("选择目录", value, "");
            //EditorConfig.Instance.SetDirty();
            EditorUtility.SetDirty(EditorConfig.Instance);
        }
        EditorGUILayout.EndHorizontal();

        if (!Directory.Exists(value))
        {
            EditorGUILayout.HelpBox("目录不存在请重新选择!! ", MessageType.Error, true);
        }

        return value;
    }

    bool CheckIsMyPath(string path)
    {
        if (Path.GetFullPath(path + "/Assets").Equals(Path.GetFullPath(Application.dataPath)))
        {
            return true;
        }
        return false;
    }

    void OnGUI()
    {
        if (instance == null)
            return;


        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);

        if (toolbarInt == 0)
        {
            ProjectSetting();
        }
    }

    void ProjectSetting()
    {
        string myPath = "";
        EditorConfig instance = EditorConfig.Instance;
        pathDic.Clear();

        GUILayout.Label("-------------项目目录设定---------------", EditorStyles.largeLabel);
        GUILayout.Space(5f);

        instance.MainPath = DrawEditorConfigProperty("MainPath", instance.MainPath);
        if (instance.MainPath != "")
        {
            pathDic.Add("MainPath", instance.MainPath);
            if (CheckIsMyPath(instance.MainPath))
                myPath = instance.MainPath;
        }

        GUILayout.Space(5f);

        instance.BundlePath = DrawEditorConfigProperty("BundlePath", instance.BundlePath);
        if (instance.BundlePath != "")
        {
            pathDic.Add("BundlePath", instance.BundlePath);
            if (CheckIsMyPath(instance.BundlePath))
                myPath = instance.BundlePath;
        }

        GUILayout.Space(5f);

        instance.EditorPath = DrawEditorConfigProperty("Editor", instance.EditorPath);
        if (instance.EditorPath != "")
        {
            pathDic.Add("Editor", instance.EditorPath);
            if (CheckIsMyPath(instance.EditorPath))
                myPath = instance.EditorPath;
        }

        GUILayout.Space(5f);

        instance.ExcelPath = DrawEditorConfigProperty("ExcelPath", instance.ExcelPath);
        if (instance.ExcelPath != "")
        {
            pathDic.Add("ExcelPath", instance.ExcelPath);
            if (CheckIsMyPath(instance.ExcelPath))
                myPath = instance.ExcelPath;
        }

        GUILayout.Space(5f);

        instance.BuidlSettingsPath = DrawEditorConfigProperty("BuidlSettingsPath", instance.BuidlSettingsPath);
        if (instance.BuidlSettingsPath != "")
        {
            pathDic.Add("BuidlSettingsPath", instance.BuidlSettingsPath);
            if (CheckIsMyPath(instance.BuidlSettingsPath))
                myPath = instance.BuidlSettingsPath;
        }

        GUILayout.Space(5f);

        instance.CodeGenarateWritePath = DrawEditorConfigProperty("CodeGenarateWritePath", instance.CodeGenarateWritePath);
        if (instance.BuidlSettingsPath != "")
        {
            pathDic.Add("CodeGenarateWritePath", instance.CodeGenarateWritePath);
            if (CheckIsMyPath(instance.CodeGenarateWritePath))
                myPath = instance.CodeGenarateWritePath;
        }

        GUILayout.Space(5f);

        instance.CodeGenarateReaderPath = DrawEditorConfigProperty("CodeGenarateReaderPath", instance.CodeGenarateReaderPath);
        if (instance.CodeGenarateReaderPath != "")
        {
            pathDic.Add("CodeGenarateReaderPath", instance.CodeGenarateReaderPath);
            if (CheckIsMyPath(instance.CodeGenarateReaderPath))
                myPath = instance.CodeGenarateReaderPath;
        }

        GUILayout.Space(5f);

        instance.CodeGenarateDataDicPath = DrawEditorConfigProperty("CodeGenarateDataDicPath", instance.CodeGenarateDataDicPath);
        if (instance.CodeGenarateDataDicPath != "")
        {
            pathDic.Add("CodeGenarateDataDicPath", instance.CodeGenarateDataDicPath);
            if (CheckIsMyPath(instance.CodeGenarateDataDicPath))
                myPath = instance.CodeGenarateDataDicPath;
        }

        GUILayout.Space(5f);

        instance.BundleConfigPath = DrawEditorConfigProperty("BundleConfigPath", instance.BundleConfigPath);
        if (instance.BundleConfigPath != "")
        {
            pathDic.Add("BundleConfigPath", instance.BundleConfigPath);
            if (CheckIsMyPath(instance.BundleConfigPath))
                myPath = instance.BundleConfigPath;
        }
    }
}
