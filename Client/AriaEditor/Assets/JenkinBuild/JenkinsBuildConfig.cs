
using System;
using UnityEngine;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class JenkinsBuildConfig : ScriptableObject
    {
        private static string Path = "Assets/ZGame/AssetPackage/Resources/";
        private static JenkinsBuildConfig _instance;
        public static JenkinsBuildConfig Instance
        {
            get
            {
#if UNITY_EDITOR
                _instance = UnityEditor.AssetDatabase.LoadAssetAtPath(Path + "JenkinsBuildConfig.asset", typeof(JenkinsBuildConfig)) as JenkinsBuildConfig;
                if (null == _instance)
                {
                    _instance = ScriptableObject.CreateInstance<JenkinsBuildConfig>();
                    UnityEditor.AssetDatabase.CreateAsset(_instance, Path + "JenkinsBuildConfig.asset");
                }

                return _instance;
#endif
                if (null == _instance)
                {
                    _instance = Resources.Load<JenkinsBuildConfig>("JenkinsBuildConfig");
                }

                return _instance;
            }
        }

        public string language;
        public string debug;
        public string unityplatform;
        public string sdkPlatform;
        public string serverurl;
        public string bundleIdentifier;
        public string bundleversion;
        public string bundleversioncode;

        public LocalizationType Language { get { return (LocalizationType)Enum.Parse(typeof(LocalizationType), language); } }

        public bool Debug { get { return Boolean.Parse(debug); } }

        public BuildPlatform Unityplatform { get { return (BuildPlatform)Enum.Parse(typeof(BuildPlatform), unityplatform); } }

#if UNITY_EDITOR
        public void RefreshConfig(LocalizationType type, bool isDebug, BuildPlatform platform)
        {
            string isDebugStr = isDebug ? "_debug.xml" : "_release.xml";
            string path = Application.dataPath + "/../../BuildSettings/jenkins_" + platform.ToString() + "_" + type.ToString() + isDebugStr;
            RefreshConfig(path);
        }

        private void RefreshConfig(string path)
        {
            UnityEngine.Debug.Log("Refresh Build Config at path " + path);
            JenkinsConfigMeta meta = JenkinsConfigReader.ReadConfig(path);
            if (null == meta)
            {
                UnityEngine.Debug.LogError("JenkinsBuildConfig error : Get meta failed !");
                return;
            }

            Instance.language = meta.RunConfig.language;
            Instance.debug = meta.RunConfig.debug;
            Instance.unityplatform = meta.RunConfig.unityplatform;
            Instance.sdkPlatform = meta.RunConfig.sdkplatform;
            Instance.serverurl = meta.RunConfig.serverurl;
            Instance.bundleIdentifier = meta.RunConfig.bundleIdentifier;
            Instance.bundleversion = meta.RunConfig.bundleversion;
            Instance.bundleversioncode = meta.RunConfig.bundleversioncode;
            UnityEditor.EditorUtility.SetDirty(_instance);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}
