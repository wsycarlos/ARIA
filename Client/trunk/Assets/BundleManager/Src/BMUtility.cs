using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class BMUtility
{
	public static void Swap<T>(ref T a, ref T b)
	{
		T temp = a;
		a = b;
		b = temp;
	}

	public static string InterpretPath(string origPath, BuildPlatform platform)
	{
		var matches = Regex.Matches(origPath, @"\$\((\w+)\)");
		foreach(Match match in matches)
		{
			string var = match.Groups[1].Value;
			origPath = origPath.Replace(@"$(" + var + ")", EnvVarToString(var, platform));
		}
		
		return origPath;
	}
	
	private static string EnvVarToString(string varString, BuildPlatform platform)
	{
		switch(varString)
		{
		case "DataPath":
			return Application.dataPath;
		case "PersistentDataPath":
			return Application.persistentDataPath;
		case "StreamingAssetsPath":
			return Application.streamingAssetsPath;
		case "Platform":
			return platform.ToString();
		default:
			Debug.LogError("Cannot solve enviroment var " + varString);
			return "";
		}
	}
}
