using UnityEngine;
using UnityEditor;
using System.Collections;

public class BMDataWatcher : AssetPostprocessor 
{
	public static bool Active = true;

	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		if(!Active)
			return;

		foreach(string asset in importedAssets)
		{
			if(asset == BMDataAccessor.BundleDataPath)
				BundleManager.RefreshAll();
			else if(asset == BMDataAccessor.BundleBuildStatePath)
				BundleManager.RefreshAll();
			else if(asset == BMDataAccessor.BMConfigerPath)
				BMDataAccessor.Refresh();
			else if(asset == BMDataAccessor.UrlDataPath)
				BMDataAccessor.Refresh();
		}
	}
}
