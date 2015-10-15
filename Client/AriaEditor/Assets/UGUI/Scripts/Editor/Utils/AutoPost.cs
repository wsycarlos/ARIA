using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class AutoPost //: AssetPostprocessor
{
    //void OnPostprocessTexture(Texture2D texture)
    //{
    //    string AtlasName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;
    //    TextureImporter textureImporter = assetImporter as TextureImporter;
    //    textureImporter.textureType = TextureImporterType.Sprite;
    //    textureImporter.spritePackingTag = AtlasName;
    //    textureImporter.mipmapEnabled = false;
    //}

    [MenuItem("UGUI/Asset/Change to 2D Sprite", false, 1)]
    private static void OnPostprocessTexture()
    {
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            if (obj is Texture2D)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                TextureImporter texImporter = GetTextureSettings(path);
                TextureImporterSettings tis = new TextureImporterSettings();
                texImporter.ReadTextureSettings(tis);
                texImporter.SetTextureSettings(tis);
                AssetDatabase.ImportAsset(path);
            }
        }
    }

    private static TextureImporter GetTextureSettings(string path)
    {
        string AtlasName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePackingTag = AtlasName;
        textureImporter.mipmapEnabled = false;

        return textureImporter;
    }
}
