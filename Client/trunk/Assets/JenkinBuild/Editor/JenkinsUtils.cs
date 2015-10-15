
namespace JenkinBuild
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class JenkinsUtils
    {
        public static void RefreshConfig(LocalizationType type, bool isDebug, BuildPlatform platform)
        {
            JenkinsBuildConfig.Instance.RefreshConfig(type, isDebug, platform);
        }
    }
}
