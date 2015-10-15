
using UnityEditor;

namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class JenkingsMenuUtils
    {
        /// <summary>
        /// 打包中文版AndroidBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/Android/CN")]
        public static void BuildAndroidBundlesCN()
        {
            JenkinsBundleBuildTools.BuildAndroidBundles(LocalizationType.cn);
        }

        /// <summary>
        /// 打包英文版AndroidBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/Android/EN")]
        public static void BuildAndroidBundlesEN()
        {
            JenkinsBundleBuildTools.BuildAndroidBundles(LocalizationType.en);
        }

        /// <summary>
        /// 打包中文版AndroidBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/PC/CN")]
        public static void BuildPcBundlesCN()
        {
            JenkinsBundleBuildTools.BuildPcBundles(LocalizationType.cn);
        }

        /// <summary>
        /// 打包英文版AndroidBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/PC/EN")]
        public static void BuildPcBundlesEN()
        {
            JenkinsBundleBuildTools.BuildPcBundles(LocalizationType.en);
        }

        /// <summary>
        /// 发布PC版本，中文
        /// </summary>
        [MenuItem("Jenkins/Build Versions/PC/Build PC")]
        public static void BuildPc()
        {
            JenkinsVersionBuildTools.BuildPc();
        }

        /// <summary>
        /// 发布Sevenga平台下中文测试版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/CN/Build Debug Version")]
        public static void BuildAndroidCnDebugSevenga()
        {
            JenkinsVersionBuildTools.BuildAndroidCnDebugSevenga();
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文测试版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/EN/Build Debug Version")]
        public static void BuildAndroidEnDebugSevenga()
        {
            JenkinsVersionBuildTools.BuildAndroidEnDebugSevenga();
        }

        /// <summary>
        /// 发布Sevenga平台下安卓中文正式版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/CN/Build Release Version")]
        public static void BuildAndroidCNReleaseSevenga()
        {
            JenkinsVersionBuildTools.BuildAndroidCNReleaseSevenga();
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文正式版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/EN/Build Release Version")]
        public static void BuildAndroidENReleaseSevenag()
        {
            JenkinsVersionBuildTools.BuildAndroidENReleaseSevenag();
        }

        /// <summary>
        /// 发布Sevenga平台下安卓中文正式测试版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/CN/Build Release Test Version")]
        public static void BuildAndroidCNReleaseSevengaTest()
        {
            JenkinsVersionBuildTools.BuildAndroidCNReleaseSevengaTest();
        }

        /// <summary>
        /// 发布Sevenga平台下安卓英文正式测试版
        /// </summary>
        [MenuItem("Jenkins/Build Versions/Android/Servenga/EN/Build Release Test Version")]
        public static void BuildAndroidENReleaseSevengaTest()
        {
            JenkinsVersionBuildTools.BuildAndroidENReleaseSevengaTest();
        }

        //[MenuItem("Jenkins/Refresh Config")]
        //public static void RefreshConfig()
        //{
        //    JenkinsVersionBuildTools.LoadJenkinsConfig(LocalizationType.en, true, BuildTarget.Android, false);
        //}
    }
}
