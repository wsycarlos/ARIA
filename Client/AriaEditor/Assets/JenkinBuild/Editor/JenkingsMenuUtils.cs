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
        /// 打包中文版IOSBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/IOS/CN")]
        public static void BuildIOSBundlesCN()
        {
            JenkinsBundleBuildTools.BuildIOSBundles(LocalizationType.cn);
        }

        /// <summary>
        /// 打包英文版IOSBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/IOS/EN")]
        public static void BuildIOSBundlesEN()
        {
            JenkinsBundleBuildTools.BuildIOSBundles(LocalizationType.en);
        }

        /// <summary>
        /// 打包中文版PCBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/PC/CN")]
        public static void BuildPcBundlesCN()
        {
            JenkinsBundleBuildTools.BuildPcBundles(LocalizationType.cn);
        }

        /// <summary>
        /// 打包英文版PCBundle
        /// </summary>
        [MenuItem("Jenkins/Bundle/PC/EN")]
        public static void BuildPcBundlesEN()
        {
            JenkinsBundleBuildTools.BuildPcBundles(LocalizationType.en);
        }

        [MenuItem("Jenkins/CodeGenetator", false, 0)]
        public static void GenerateCode()
        {
            JenkinsBundleBuildTools.CodeGenerator();
        }
    }
}
