/// <summary>
/// 功能概述：
/// </summary>
public class LanguageTool
{
    private static string language = "en";
    static string assetPath = "Assets/ZGame/AssetPackage/Template/";

    public static void Export()
    {
        ExportChinese();
        ExportEnglish();
    }

    public static void ExportChinese()
    {
        language = "cn";
        LocalizationTool.ImportTemplates(assetPath, language);
    }

    public static void ExportEnglish()
    {
        language = "en";
        LocalizationTool.ImportTemplates(assetPath, language);
    }
}
