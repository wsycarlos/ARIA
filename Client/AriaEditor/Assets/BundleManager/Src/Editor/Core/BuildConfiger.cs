using System.Text.RegularExpressions;
using JenkinBuild;
using UnityEditor;
using UnityEngine;

/**
 * Build settings
 */
public class BuildConfiger
{
    /**
     * Compress bundles
     */
    public static bool Compress
    {
        get { return BMDataAccessor.BMConfiger.compress; }
        set
        {
            if (BMDataAccessor.BMConfiger.compress != value)
            {
                BMDataAccessor.BMConfiger.compress = value;
                BundleManager.UpdateAllBundleChangeTime();
            }
        }
    }

    /**
     * Build deterministic Bundles
     */
    public static bool DeterministicBundle
    {
        get { return BMDataAccessor.BMConfiger.deterministicBundle; }
        set
        {
            if (BMDataAccessor.BMConfiger.deterministicBundle != value)
            {
                BMDataAccessor.BMConfiger.deterministicBundle = value;
                BundleManager.UpdateAllBundleChangeTime();
            }
        }
    }

    /** 
     * Target platform
     */
    public static BuildPlatform BundleBuildTarget
    {
        get
        {
            return BMDataAccessor.Urls.bundleTarget;
        }
        set
        {
            BMDataAccessor.Urls.bundleTarget = value;
        }
    }

    /** 
     * Target platform
     */
    public static bool UseEditorTarget
    {
        get
        {
            return BMDataAccessor.Urls.useEditorTarget;
        }
        set
        {
            BMDataAccessor.Urls.useEditorTarget = value;
        }
    }

    /**
     * Bundle file's suffix
     */
    public static string BundleSuffix
    {
        get { return BMDataAccessor.BMConfiger.bundleSuffix; }
        set { BMDataAccessor.BMConfiger.bundleSuffix = value; }
    }

    /**
     * Current output string for target platform
     */
    public static string BuildOutputStr
    {
        get
        {
            return BMDataAccessor.Urls.outputs[BMDataAccessor.Urls.bundleTarget.ToString()];
        }
        set
        {
            var urls = BMDataAccessor.Urls.outputs;
            string platformStr = BMDataAccessor.Urls.bundleTarget.ToString();
            string origValue = urls[platformStr];
            urls[platformStr] = value;
            if (origValue != value)
                BMDataAccessor.SaveUrls();
        }
    }

    internal static string InterpretedOutputPath
    {
        get
        {
            //�޸ĵ���bundle��Ŀ¼
            return InterpretPath(BuildOutputStr, BMDataAccessor.Urls.bundleTarget);

            //return BMDataAccessor.Urls.GetInterpretedOutputPath(BMDataAccessor.Urls.bundleTarget);
        }
    }

    internal static BuildOptions BuildOptions
    {
        get
        {
            return BMDataAccessor.BMConfiger.compress ? 0 : BuildOptions.UncompressedAssetBundle;
        }
    }

    internal static BuildTarget UnityBuildTarget
    {
        get
        {
            if (BuildConfiger.UseEditorTarget)
                BuildConfiger.UnityBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            switch (BundleBuildTarget)
            {
                case BuildPlatform.pc:
                    if (Application.platform == RuntimePlatform.OSXEditor)
                        return BuildTarget.StandaloneOSXIntel;
                    else
                        return BuildTarget.StandaloneWindows;
                case BuildPlatform.web:
                    return BuildTarget.WebPlayer;
                case BuildPlatform.ios:
                    return BuildTarget.iOS;
                case BuildPlatform.android:
                    return BuildTarget.Android;
                case BuildPlatform.webGL:
                    return BuildTarget.WebGL;
                default:
                    Debug.LogError("Internal error. Cannot find BuildTarget for " + BundleBuildTarget);
                    return BuildTarget.StandaloneWindows;
            }
        }
        set
        {
            switch (value)
            {
                case BuildTarget.StandaloneGLESEmu:
                case BuildTarget.StandaloneLinux:
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneLinuxUniversal:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    BundleBuildTarget = BuildPlatform.pc;
                    break;
                case BuildTarget.WebPlayer:
                case BuildTarget.WebPlayerStreamed:
                    BundleBuildTarget = BuildPlatform.web;
                    break;
                case BuildTarget.iOS:
                    BundleBuildTarget = BuildPlatform.ios;
                    break;
                case BuildTarget.Android:
                    BundleBuildTarget = BuildPlatform.android;
                    break;
                case BuildTarget.WebGL:
                    BundleBuildTarget = BuildPlatform.webGL;
                    break;
                default:
                    Debug.LogError("Internal error. Bundle Manager dosn't support for platform " + value);
                    BundleBuildTarget = BuildPlatform.pc;
                    break;
            }
        }
    }

    public static string InterpretPath(string origPath, BuildPlatform platform)
    {
        var matches = Regex.Matches(origPath, @"\$\((\w+)\)");
        foreach (Match match in matches)
        {
            string var = match.Groups[1].Value;
            origPath = origPath.Replace(@"$(" + var + ")", EnvVarToString(var, platform));
        }

        return origPath;
    }

    private static string EnvVarToString(string varString, BuildPlatform platform)
    {
        switch (varString)
        {
            case "DataPath":
                return Application.dataPath;
            case "PersistentDataPath":
                return Application.persistentDataPath;
            case "StreamingAssetsPath":
                return Application.streamingAssetsPath;
            case "Platform":
                return platform.ToString();
            case "BundlePath":
                return EditorConfig.Instance.BundlePath;
            case "Localization":
                return JenkinsBuildConfig.Instance.Language.ToString();
            case "BundleVersion":
                return JenkinsBuildConfig.Instance.Bundleversion;
            default:
                Debug.LogError("Cannot solve enviroment var " + varString);
                return "";
        }
    }
}