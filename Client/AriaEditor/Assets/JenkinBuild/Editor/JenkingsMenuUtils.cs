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

        [MenuItem("Jenkins/Bundle/Web Player/CN")]
        public static void BuildWebBundlesCN()
        {
            JenkinsBundleBuildTools.BuildWebBundle(LocalizationType.cn);
        }

        [MenuItem("Jenkins/Bundle/Web Player/EN")]
        public static void BuildWebBundlesEN()
        {
            JenkinsBundleBuildTools.BuildWebBundle(LocalizationType.en);
        }

        [MenuItem("Jenkins/Bundle/WP8 Player/CN")]
        public static void BuildWPBundlesCN()
        {
            JenkinsBundleBuildTools.BuildWPBundle(LocalizationType.cn);
        }

        [MenuItem("Jenkins/Bundle/WP8 Player/EN")]
        public static void BuildWPBundlesEN()
        {
            JenkinsBundleBuildTools.BuildWPBundle(LocalizationType.en);
        }

        [MenuItem("Jenkins/Bundle/WebGL/CN")]
        public static void BuildWebGLBundlesCN()
        {
            JenkinsBundleBuildTools.BuildWebGLBundle(LocalizationType.cn);
        }

        [MenuItem("Jenkins/Bundle/WebGL/EN")]
        public static void BuildWebGLBundlesEN()
        {
            JenkinsBundleBuildTools.BuildWebGLBundle(LocalizationType.en);
        }

        [MenuItem("Jenkins/CodeGenetator", false, 0)]
        public static void GenerateCode()
        {
            JenkinsBundleBuildTools.CodeGenerator();
        }
    }
}
