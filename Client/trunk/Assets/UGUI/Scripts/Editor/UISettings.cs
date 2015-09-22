
using UnityEditor;

/// <summary>
/// 功能概述：
/// </summary>
public class UISettings
{
    static public bool minimalisticLook
    {
        get { return GetBool("UI Minimalistic", false); }
        set { SetBool("UI Minimalistic", value); }
    }

    /// <summary>
    /// Save the specified boolean value in settings.
    /// </summary>

    static public void SetBool(string name, bool val) { EditorPrefs.SetBool(name, val); }

    /// <summary>
    /// Get the previously saved boolean value.
    /// </summary>

    static public bool GetBool(string name, bool defaultValue) { return EditorPrefs.GetBool(name, defaultValue); }
}