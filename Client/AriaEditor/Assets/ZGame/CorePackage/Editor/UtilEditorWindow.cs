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
        "辅助工具"
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
        else if (toolbarInt == 1)
        {
            HelperTools();
        }
    }

    void HelperTools()
    {
        EditorGUILayout.PrefixLabel("---场景对象操作---");
        EditorGUILayout.ObjectField("当前对象:", Selection.activeGameObject, typeof(GameObject), true);

        if (GUILayout.Button("解除当前对象prefab关联"))
        {
            if (Selection.activeGameObject != null)
            {
                PrefabUtility.DisconnectPrefabInstance(Selection.activeGameObject);
            }
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("排列子对象"))
        {
            SortChildren();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Apply选中的PrefabInstance"))
        {
            if (Selection.gameObjects != null)
            {
                foreach (GameObject ga in Selection.gameObjects)
                {
                    PrefabUtility.RecordPrefabInstancePropertyModifications(ga);
                    Object obj = PrefabUtility.GetPrefabParent(ga);
                    if (obj != null)
                        PrefabUtility.ReplacePrefab(ga, obj, ReplacePrefabOptions.ConnectToPrefab | ReplacePrefabOptions.ReplaceNameBased);
                }
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("替换场景中shader"))
        {
            ChangeAllShadersToMobileInTheScene();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("初始化选中声音文件配置"))
        {
            SetupSoundInit();
        }
    }

    void ChangeAllShadersToMobileInTheScene()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer one in renderers)
        {
            if (one.sharedMaterial != null)
            {
                if (one.sharedMaterial.shader.name == "Transparent/Cutout/Diffuse")
                {
                    Color c = one.sharedMaterial.color;
                    float alphac = one.sharedMaterial.GetFloat("_Cutoff");
                    one.sharedMaterial.shader = Shader.Find("echoLogin/Unlit/Cutout/Discard-Color");
                    one.sharedMaterial.SetVector("_echoRGBA", new Vector4(c.r, c.g, c.b, c.a));
                    one.sharedMaterial.SetFloat("_echoCutoff", alphac);
                }
                else if (one.sharedMaterial.shader.name == "Diffuse")
                {
                    Color c = one.sharedMaterial.color;
                    one.sharedMaterial.shader = Shader.Find("echoLogin/Unlit/20-Color");
                    one.sharedMaterial.SetVector("_echoRGBA", new Vector4(c.r, c.g, c.b, c.a));
                }
            }
        }
    }

    void SortChildren()
    {
        if (Selection.activeGameObject != null)
        {
            List<GameObject> children = new List<GameObject>();

            for (int i = 0; i < Selection.activeGameObject.transform.childCount; i++)
            {
                children.Add(Selection.activeGameObject.transform.GetChild(i).gameObject);
            }

            children.Sort(SortByName);

            for (int i = 0; i < children.Count; i++)
            {
                int j = i / 6;
                int xx = i % 6;
                children[i].transform.localPosition = new Vector3(xx * 5f, 0f, -j * 5f);
            }
        }
    }

    static public int SortByName(GameObject a, GameObject b)
    {
        return string.Compare(a.name, b.name);
    }

    void ProjectSetting()
    {
        string myPath = "";
        pathDic.Clear();

        GUILayout.Label("-------------项目目录设定---------------", EditorStyles.largeLabel);
        GUILayout.Space(2f);

        EditorConfig.Instance.MainPath = DrawEditorConfigProperty("MainPath", EditorConfig.Instance.MainPath);
        if (EditorConfig.Instance.MainPath != "")
        {
            pathDic.Add("MainPath", EditorConfig.Instance.MainPath);
            if (CheckIsMyPath(EditorConfig.Instance.MainPath))
                myPath = EditorConfig.Instance.MainPath;
        }

        EditorConfig.Instance.BundlePath = DrawEditorConfigProperty("BundlePath", EditorConfig.Instance.BundlePath);
        if (EditorConfig.Instance.BundlePath != "")
        {
            pathDic.Add("BundlePath", EditorConfig.Instance.BundlePath);
            if (CheckIsMyPath(EditorConfig.Instance.BundlePath))
                myPath = EditorConfig.Instance.BundlePath;
        }

        EditorConfig.Instance.EditorPath = DrawEditorConfigProperty("Editor", EditorConfig.Instance.EditorPath);
        if (EditorConfig.Instance.EditorPath != "")
        {
            pathDic.Add("Editor", EditorConfig.Instance.EditorPath);
            if (CheckIsMyPath(EditorConfig.Instance.EditorPath))
                myPath = EditorConfig.Instance.EditorPath;
        }

        EditorConfig.Instance.ExcelPath = DrawEditorConfigProperty("ExcelPath", EditorConfig.Instance.ExcelPath);
        if (EditorConfig.Instance.ExcelPath != "")
        {
            pathDic.Add("ExcelPath", EditorConfig.Instance.ExcelPath);
        }

        EditorConfig.Instance.BuidlSettingsPath = DrawEditorConfigProperty("BuidlSettingsPath", EditorConfig.Instance.BuidlSettingsPath);
        if (EditorConfig.Instance.BuidlSettingsPath != "")
        {
            pathDic.Add("BuidlSettingsPath", EditorConfig.Instance.BuidlSettingsPath);
            if (CheckIsMyPath(EditorConfig.Instance.BuidlSettingsPath))
                myPath = EditorConfig.Instance.BuidlSettingsPath;
        }
    }

    void SetupSoundInit()
    {
        Object[] audioclips = GetSelectedAudioclips();
        Selection.objects = new Object[0];
        foreach (AudioClip audioclip in audioclips)
        {
            string path = AssetDatabase.GetAssetPath(audioclip);
            AudioImporter audioImporter = AssetImporter.GetAtPath(path) as AudioImporter;
            audioImporter.threeD = false;
            AssetDatabase.ImportAsset(path);
        }
    }
    static Object[] GetSelectedAudioclips()
    {
        return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
    }
}
