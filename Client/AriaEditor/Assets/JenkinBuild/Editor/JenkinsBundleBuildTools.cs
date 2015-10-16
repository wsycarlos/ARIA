
using Game.Template.Editor;
using JenkinBuild;
using UnityEditor;

/// <summary>
/// 功能概述：
/// 该类用于Jenkins发布时使用，区别于CommandLineBuild类，功能相近，为了了方便管理分开处理
/// </summary>
public class JenkinsBundleBuildTools
{
    public static void CodeGenerator()
    {
        TemplateManager.Instance.CodeGenerator();
    }

    public static void BuildAndroidBundles(LocalizationType type)
    {
        //刷新配置文件
        JenkinsUtils.RefreshConfig(type, true, BuildPlatform.android);
        //构建
        BuildBundles(BuildTarget.Android);
    }

    public static void BuildPcBundles(LocalizationType type)
    {
        //刷新配置文件
        JenkinsUtils.RefreshConfig(type, true, BuildPlatform.pc);
        //构建
        BuildBundles(BuildTarget.StandaloneWindows);
    }

    private static void BuildBundles(BuildTarget target)
    {
        //切换平台
        EditorUserBuildSettings.SwitchActiveBuildTarget(target);
        //导出多语言配置
        LanguageTool.Export();
        //导出模板配置
        TemplateLoadEditor.ImportTemplates();
        //刷新UI配置
        //ReFreshUIConfig.RefreshUIConfig();
        //刷新BundleManager
        //RefreshBundle();
        //构建导出
        //ExportBundleData();
    }

    /// <summary>
    /// 用于刷新BundleManager
    /// </summary>
    private static void RefreshBundle()
    {
        BundleManager.getInstance().Init();
    }

    /// <summary>
    /// 导出文件
    /// </summary>
    private static void ExportBundleData()
    {
        if (BundleManager.RefreshBuildStateFromOutputPath())
        {
            BuildHelper.BuildAll();
            BuildHelper.ExportBMDatasToOutput();
        }
    }
}
