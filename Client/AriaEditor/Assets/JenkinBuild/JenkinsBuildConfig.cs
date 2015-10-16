
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

        public string _language;
        public string _debug;
        public string _unityplatform;
        public string _sdkPlatform;
        public string _serverurl;
        public string _bundleIdentifier;
        public string _bundleversion;
        public string _bundleversioncode;

        public string Bundleversion
        {
            get { return _bundleversion; }
        }

        public LocalizationType Language { get { return (LocalizationType)Enum.Parse(typeof(LocalizationType), _language); } }

        public bool Debug { get { return Boolean.Parse(_debug); } }

        public BuildPlatform Unityplatform { get { return (BuildPlatform)Enum.Parse(typeof(BuildPlatform), _unityplatform); } }

#if UNITY_EDITOR
        public void RefreshConfig(LocalizationType type, bool isDebug, BuildPlatform platform)
        {
            string isDebugStr = isDebug ? "_debug.xml" : "_release.xml";
            //TODO Fix me
            string path = Application.dataPath + "/../../Settings/BuildSettings/jenkins_" + platform.ToString() + "_" + type.ToString() + isDebugStr;
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

            Instance._language = meta.RunConfig.language;
            Instance._debug = meta.RunConfig.debug;
            Instance._unityplatform = meta.RunConfig.unityplatform;
            Instance._sdkPlatform = meta.RunConfig.sdkplatform;
            Instance._serverurl = meta.RunConfig.serverurl;
            Instance._bundleIdentifier = meta.RunConfig.bundleIdentifier;
            Instance._bundleversion = meta.RunConfig.bundleversion;
            Instance._bundleversioncode = meta.RunConfig.bundleversioncode;
            UnityEditor.EditorUtility.SetDirty(_instance);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}
