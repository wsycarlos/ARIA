
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// Jenkins版本发布
    /// </summary>
    public class JenkinsVersionBuildTools
    {
        private static string[] levels = { "Assets/Main.unity" };
        private static string outputPath = "/../../../share/Demo/";

        #region 对外提供的发布接口

        public static void BuildPc()
        {
            Build(BuildTarget.StandaloneWindows, SDKType.self, LocalizationType.en, "game.exe", true);
        }

        /// <summary>
        /// 发布Sevenga平台下中文测试版
        /// </summary>
        public static void BuildAndroidCnDebugSevenga()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.cn, "game_sevenga_debug_cn.apk", true);
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文测试版
        /// </summary>
        public static void BuildAndroidEnDebugSevenga()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.en, "game_sevenga_debug_en.apk", true);
        }

        /// <summary>
        /// 发布Sevenga平台下安卓中文正式版
        /// </summary>
        public static void BuildAndroidCNReleaseSevenga()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.cn, "game_sevenga_release_cn.apk", false);
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文正式版
        /// </summary>
        public static void BuildAndroidENReleaseSevenag()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.en, "game_sevenga_release_en.apk", false);
        }

        /// <summary>
        /// 发布Sevenga平台下安卓中文正式测试版
        /// </summary>
        public static void BuildAndroidCNReleaseSevengaTest()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.cn, "game_sevenga_release_test_cn.apk", false);
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文正式测试版
        /// </summary>
        public static void BuildAndroidENReleaseSevengaTest()
        {
            Build(BuildTarget.Android, SDKType.Sevenga, LocalizationType.en, "game_sevenga_release_test_en.apk", false);
        }

        #endregion


        #region 发布时帮助方法

        private static void Build(BuildTarget target, SDKType sdkType, LocalizationType type, string packageName, bool isDebug)
        {
            SwitchPlatform(target);
            LoadJenkinsConfig(type, isDebug, target);
            SetBundleVersion();
            SetBundleVersionCode();
            SetBundleIdentifier();
            CopyBundleFiles(target);
            CopyPluginFiles(sdkType, target);
            CopySDKFiles(sdkType, target, isDebug);
            ExportPackage(target, packageName);
        }

        /// <summary>
        /// 切换平台
        /// </summary>
        /// <param name="target"></param>
        private static void SwitchPlatform(BuildTarget target)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);
        }

        /// <summary>
        /// 刷新配置文件
        /// </summary>
        public static void LoadJenkinsConfig(LocalizationType type, bool isDebug, BuildTarget platform)
        {
            BuildPlatform buildPlatform = GetBuildPlatform(platform);
            JenkinsUtils.RefreshConfig(type, isDebug, buildPlatform);
        }

        /// <summary>
        /// 设置bundleVersion
        /// </summary>
        private static void SetBundleVersion()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            PlayerSettings.bundleVersion = JenkinsBuildConfig.Instance.bundleversion;
#endif
        }

        /// <summary>
        /// 设置bundleVersionCode， For Android
        /// </summary>
        private static void SetBundleVersionCode()
        {
#if UNITY_ANDROID
            PlayerSettings.Android.bundleVersionCode = int.Parse(JenkinsBuildConfig.Instance.bundleversioncode);
#endif
        }

        /// <summary>
        /// 设置bundleIdentifier，不同平台不同
        /// </summary>
        private static void SetBundleIdentifier()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            PlayerSettings.bundleIdentifier = JenkinsBuildConfig.Instance.bundleIdentifier;
#endif
        }

        /// <summary>
        /// 拷贝Plugin文件
        /// </summary>
        private static void CopyPluginFiles(SDKType type, BuildTarget target)
        {
            switch (type)
            {
                case SDKType.Sevenga:
                    break;
                default:
                    return;
            }

            BuildPlatform platform = GetBuildPlatform(target);
            string baseTo = EditorConfig.Instance.MainPath + "/Assets/Plugins";
            string from = EditorConfig.Instance.SDKPath + "/" + type.ToString() + "/" + platform.ToString() + "/Plugins";
            string to = baseTo + "/" + platform.ToString();
            CopyFiles(target, baseTo, from, to);
        }

        /// <summary>
        /// 拷贝SDK配置文件
        /// </summary>
        private static void CopySDKFiles(SDKType type, BuildTarget target, bool isDebug)
        {
            switch (type)
            {
                case SDKType.Sevenga:
                    break;
                default:
                    return;
            }

            string baseTo = Application.streamingAssetsPath + "/SevengaSDK.xml";
            string from = EditorConfig.Instance.SDKPath + "/" + type.ToString() + "/" + GetBuildPlatform(target).ToString() + "/SDKConfig" + (isDebug ? "/Debug" : "/Release") + "/SevengaSDK.xml";
            FileUtil.CopyFileOrDirectory(from, baseTo);
        }

        /// <summary>
        /// 导入Bundle文件
        /// </summary>
        /// <param name="target"></param>
        private static void CopyBundleFiles(BuildTarget target)
        {
            CopyFiles(target, Application.streamingAssetsPath, GetBundlePath(target), Application.streamingAssetsPath + "/" + GetBundleFolder(target));
        }

        /// <summary>
        /// 导出安装包
        /// </summary>
        private static void ExportPackage(BuildTarget target, string name)
        {
            string pname = EditorConfig.Instance.EditorPath + outputPath + GetBundleFolder(target) + "/" + name;
            BuildPipeline.BuildPlayer(levels,
                pname,
                target,
                BuildOptions.None);
        }

        private static void CopyFiles(BuildTarget target, string baseTo, string from, string to)
        {
            try
            {
                FileUtil.DeleteFileOrDirectory(baseTo);
                EditorUtility.DisplayProgressBar("删除文件", from, 0);
                Directory.CreateDirectory(baseTo);
                EditorUtility.DisplayProgressBar("重建目录", from, 0.5f);
                FileUtil.CopyFileOrDirectory(from, to);
                EditorUtility.DisplayProgressBar("拷贝文件", from, 1);
                EditorUtility.ClearProgressBar();
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static string GetBundlePath(BuildTarget targetPlatform)
        {
            return EditorConfig.Instance.MainPath + "/AssetBundle/" + JenkinsBuildConfig.Instance.language + "/" + GetBundleFolder(targetPlatform);
        }

        private static string GetBundleFolder(BuildTarget targetPlatform)
        {
            string folder = "";
            switch (targetPlatform)
            {
                case BuildTarget.iOS:
                    folder = "ios";
                    break;
                case BuildTarget.Android:
                    folder = "android";
                    break;
                default:
                    folder = "pc";
                    break;
            }

            return folder;
        }

        private static BuildPlatform GetBuildPlatform(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                    return BuildPlatform.pc;
                case BuildTarget.Android:
                    return BuildPlatform.android;
                case BuildTarget.iOS:
                    return BuildPlatform.ios;
                case BuildTarget.WebPlayer:
                    return BuildPlatform.web;
                case BuildTarget.WP8Player:
                    return BuildPlatform.wp8;
            }

            return BuildPlatform.pc;
        }

        #endregion
    }
}

