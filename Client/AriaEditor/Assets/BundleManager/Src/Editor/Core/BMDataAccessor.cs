using System.Collections.Generic;
using System.IO;
using JenkinBuild;
using LitJson;
using UnityEditor;
using UnityEngine;

internal class BMDataAccessor
{
    static public List<BundleData> Bundles
    {
        get
        {
            if (m_Bundles == null)
                m_Bundles = loadObjectFromJsonFile<List<BundleData>>(BundleDataPath);

            if (m_Bundles == null)
                m_Bundles = new List<BundleData>();

            return m_Bundles;
        }
    }

    static public List<BundleBuildState> BuildStates
    {
        get
        {
            if (m_BuildStates == null)
                m_BuildStates = loadObjectFromJsonFile<List<BundleBuildState>>(BundleBuildStatePath);

            if (m_BuildStates == null)
                m_BuildStates = new List<BundleBuildState>();

            return m_BuildStates;
        }
    }

    static public BMConfiger BMConfiger
    {
        get
        {
            if (m_BMConfier == null)
                m_BMConfier = loadObjectFromJsonFile<BMConfiger>(BMConfigerPath);

            if (m_BMConfier == null)
                m_BMConfier = new BMConfiger();

            return m_BMConfier;
        }
    }

    static public BMUrls Urls
    {
        get
        {
            if (m_Urls == null)
                m_Urls = loadObjectFromJsonFile<BMUrls>(UrlDataPath);

            if (m_Urls == null)
                m_Urls = new BMUrls();

            return m_Urls;
        }
    }

    static public void Refresh()
    {
        m_Bundles = null;
        m_BuildStates = null;
        m_BMConfier = null;
        m_Urls = null;
    }

    static public void SaveBMConfiger()
    {
        saveObjectToJsonFile(BMConfiger, BMConfigerPath);
    }

    static public void SaveBundleData()
    {
        foreach (BundleData bundle in Bundles)
        {
            bundle.includeGUIDs.Sort(guidComp);
            bundle.includs = BundleManager.GUIDsToPaths(bundle.includeGUIDs);

            bundle.dependGUIDs.Sort(guidComp);
            bundle.dependAssets = BundleManager.GUIDsToPaths(bundle.dependGUIDs);
        }
        saveObjectToJsonFile(Bundles, BundleDataPath);
    }

    static public void SaveBundleBuildeStates()
    {
        saveObjectToJsonFile(BuildStates, BundleBuildStatePath);
    }

    static public void SaveUrls()
    {
        saveObjectToJsonFile(Urls, UrlDataPath);
    }


    static private T loadObjectFromJsonFile<T>(string path)
    {
        TextReader reader = new StreamReader(path);
        if (reader == null)
        {
            Debug.LogError("Cannot find " + path);
            reader.Close();
            return default(T);
        }

        T data = JsonMapper.ToObject<T>(reader.ReadToEnd());
        if (data == null)
        {
            Debug.LogError("Cannot read data from " + path);
        }

        reader.Close();
        return data;
    }

    static private void saveObjectToJsonFile<T>(T data, string path)
    {
        TextWriter tw = new StreamWriter(path);
        if (tw == null)
        {
            Debug.LogError("Cannot write to " + path);
            return;
        }

        string jsonStr = JsonFormatter.PrettyPrint(JsonMapper.ToJson(data));

        tw.Write(jsonStr);
        tw.Flush();
        tw.Close();

        BMDataWatcher.Active = false;
        AssetDatabase.ImportAsset(path);
        BMDataWatcher.Active = true;
    }

    static private int guidComp(string guid1, string guid2)
    {
        string fileName1 = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid1));
        string fileName2 = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid2));
        return fileName1.CompareTo(fileName2);
    }

    static private List<BundleData> m_Bundles = null;
    static private List<BundleBuildState> m_BuildStates = null;
    static private BMConfiger m_BMConfier = null;
    static private BMUrls m_Urls = null;

    public static List<BundleBuildState> BuildStatesFromOutputPath
    {
        get
        {
            //FIXME load bundle instead
            m_BuildStates = loadObjectFromJsonFile<List<BundleBuildState>>(BuildConfiger.InterpretedOutputPath + "/BuildStates.txt");
            return m_BuildStates;
        }
        set { m_BuildStates = value; }
    }

    public static string BundleDataPath
    {
        get
        {
            return EditorConfig.Instance.BundleConfigPath + "/" +
                   JenkinsBuildConfig.Instance.Language.ToString() + "/" +
                   "BundleData.txt";
        }
    }

    public static string BMConfigerPath
    {
        get
        {
            return EditorConfig.Instance.BundleConfigPath + "/" +
                   JenkinsBuildConfig.Instance.Language.ToString() + "/" +
                   "BMConfiger.txt";
        }
    }

    public static string BundleBuildStatePath
    {
        get
        {
            return EditorConfig.Instance.BundleConfigPath + "/" +
                   JenkinsBuildConfig.Instance.Language.ToString() + "/" +
                   "BuildStates.txt";
        }
    }

    public static string UrlDataPath
    {
        get
        {
            return EditorConfig.Instance.BundleConfigPath + "/" +
                   JenkinsBuildConfig.Instance.Language.ToString() + "/" +
                   "Urls.txt";
        }
    }
}
