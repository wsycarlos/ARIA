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
        pathDic.Clear();

        GUILayout.Label("-------------项目目录设定---------------", EditorStyles.largeLabel);
        GUILayout.Space(5f);

        EditorConfig.Instance.MainPath = DrawEditorConfigProperty("MainPath", EditorConfig.Instance.MainPath);
        if (EditorConfig.Instance.MainPath != "")
        {
            pathDic.Add("MainPath", EditorConfig.Instance.MainPath);
            if (CheckIsMyPath(EditorConfig.Instance.MainPath))
                myPath = EditorConfig.Instance.MainPath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.BundlePath = DrawEditorConfigProperty("BundlePath", EditorConfig.Instance.BundlePath);
        if (EditorConfig.Instance.BundlePath != "")
        {
            pathDic.Add("BundlePath", EditorConfig.Instance.BundlePath);
            if (CheckIsMyPath(EditorConfig.Instance.BundlePath))
                myPath = EditorConfig.Instance.BundlePath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.EditorPath = DrawEditorConfigProperty("Editor", EditorConfig.Instance.EditorPath);
        if (EditorConfig.Instance.EditorPath != "")
        {
            pathDic.Add("Editor", EditorConfig.Instance.EditorPath);
            if (CheckIsMyPath(EditorConfig.Instance.EditorPath))
                myPath = EditorConfig.Instance.EditorPath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.ExcelPath = DrawEditorConfigProperty("ExcelPath", EditorConfig.Instance.ExcelPath);
        if (EditorConfig.Instance.ExcelPath != "")
        {
            pathDic.Add("ExcelPath", EditorConfig.Instance.ExcelPath);
            if (CheckIsMyPath(EditorConfig.Instance.ExcelPath))
                myPath = EditorConfig.Instance.ExcelPath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.BuidlSettingsPath = DrawEditorConfigProperty("BuidlSettingsPath", EditorConfig.Instance.BuidlSettingsPath);
        if (EditorConfig.Instance.BuidlSettingsPath != "")
        {
            pathDic.Add("BuidlSettingsPath", EditorConfig.Instance.BuidlSettingsPath);
            if (CheckIsMyPath(EditorConfig.Instance.BuidlSettingsPath))
                myPath = EditorConfig.Instance.BuidlSettingsPath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.CodeGenarateWritePath = DrawEditorConfigProperty("CodeGenarateWritePath", EditorConfig.Instance.CodeGenarateWritePath);
        if (EditorConfig.Instance.BuidlSettingsPath != "")
        {
            pathDic.Add("CodeGenarateWritePath", EditorConfig.Instance.CodeGenarateWritePath);
            if (CheckIsMyPath(EditorConfig.Instance.CodeGenarateWritePath))
                myPath = EditorConfig.Instance.CodeGenarateWritePath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.CodeGenarateReaderPath = DrawEditorConfigProperty("CodeGenarateReaderPath", EditorConfig.Instance.CodeGenarateReaderPath);
        if (EditorConfig.Instance.CodeGenarateReaderPath != "")
        {
            pathDic.Add("CodeGenarateReaderPath", EditorConfig.Instance.CodeGenarateReaderPath);
            if (CheckIsMyPath(EditorConfig.Instance.CodeGenarateReaderPath))
                myPath = EditorConfig.Instance.CodeGenarateReaderPath;
        }

        GUILayout.Space(5f);

        EditorConfig.Instance.CodeGenarateDataDicPath = DrawEditorConfigProperty("CodeGenarateDataDicPath", EditorConfig.Instance.CodeGenarateDataDicPath);
        if (EditorConfig.Instance.CodeGenarateDataDicPath != "")
        {
            pathDic.Add("CodeGenarateDataDicPath", EditorConfig.Instance.CodeGenarateDataDicPath);
            if (CheckIsMyPath(EditorConfig.Instance.CodeGenarateDataDicPath))
                myPath = EditorConfig.Instance.CodeGenarateDataDicPath;
        }
    }
}
