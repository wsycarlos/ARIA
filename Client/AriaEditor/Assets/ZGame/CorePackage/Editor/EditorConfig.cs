using UnityEditor;
using UnityEngine;

public class EditorConfig : ScriptableObject
{
    private static readonly string Path = "Assets/ZGame/CorePackage/Editor/";
    private static EditorConfig instance;
    public string BuidlSettingsPath;
    public string BundlePath;
    public string CodeGenarateDataDicPath;
    public string CodeGenarateReaderPath;

    public string CodeGenarateWritePath;
    public string EditorPath;
    public string ExcelPath;

    public string MainPath;

    public string BundleConfigPath;

    public static EditorConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance =
                    AssetDatabase.LoadAssetAtPath(Path + "editorConfig.asset", typeof(EditorConfig)) as EditorConfig;
                if (instance == null)
                {
                    instance = CreateInstance<EditorConfig>();
                    AssetDatabase.CreateAsset(instance, "editorConfig.asset");
                }
            }
            return instance;
        }
    }
}