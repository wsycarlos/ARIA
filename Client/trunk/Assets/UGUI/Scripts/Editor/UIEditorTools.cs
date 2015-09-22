
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class UIEditorTools
{
    static Texture2D mBackdropTex;
    static Texture2D mContrastTex;
    static Texture2D mGradientTex;
    static GameObject mPrevious;

    static bool mEndHorizontal = false;

    /// <summary>
    /// Returns a blank usable 1x1 white texture.
    /// </summary>

    static public Texture2D blankTexture
    {
        get
        {
            return EditorGUIUtility.whiteTexture;
        }
    }

    /// <summary>
    /// Returns a usable texture that looks like a dark checker board.
    /// </summary>

    static public Texture2D backdropTexture
    {
        get
        {
            if (mBackdropTex == null) mBackdropTex = CreateCheckerTex(
                new Color(0.1f, 0.1f, 0.1f, 0.5f),
                new Color(0.2f, 0.2f, 0.2f, 0.5f));
            return mBackdropTex;
        }
    }

    /// <summary>
    /// Create a checker-background texture
    /// </summary>

    static Texture2D CreateCheckerTex(Color c0, Color c1)
    {
        Texture2D tex = new Texture2D(16, 16);
        tex.name = "[Generated] Checker Texture";
        tex.hideFlags = HideFlags.DontSave;

        for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
        for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
        for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
        for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

        tex.Apply();
        tex.filterMode = FilterMode.Point;
        return tex;
    }

    /// <summary>
    /// Unity 4.3 changed the way LookLikeControls works.
    /// </summary>

    static public void SetLabelWidth(float width)
    {
        EditorGUIUtility.labelWidth = width;
    }

    /// <summary>
    /// Create an undo point for the specified objects.
    /// </summary>

    static public void RegisterUndo(string name, params UnityEngine.Object[] objects)
    {
        if (objects != null && objects.Length > 0)
        {
            UnityEditor.Undo.RecordObjects(objects, name);

            foreach (UnityEngine.Object obj in objects)
            {
                if (obj == null) continue;
                EditorUtility.SetDirty(obj);
            }
        }
    }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text) { return DrawHeader(text, text, false, UISettings.minimalisticLook); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key) { return DrawHeader(text, key, false, UISettings.minimalisticLook); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, bool detailed) { return DrawHeader(text, text, detailed, !detailed); }

    /// <summary>
    /// Draw a distinctly different looking header label
    /// </summary>

    static public bool DrawHeader(string text, string key, bool forceOn, bool minimalistic)
    {
        bool state = EditorPrefs.GetBool(key, true);

        if (!minimalistic) GUILayout.Space(3f);
        if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal();
        GUI.changed = false;

        if (minimalistic)
        {
            if (state) text = "\u25BC" + (char)0x200a + text;
            else text = "\u25BA" + (char)0x200a + text;

            GUILayout.BeginHorizontal();
            GUI.contentColor = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.7f) : new Color(0f, 0f, 0f, 0.7f);
            if (!GUILayout.Toggle(true, text, "PreToolbar2", GUILayout.MinWidth(20f))) state = !state;
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
        }
        else
        {
            text = "<b><size=11>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
        }

        if (GUI.changed) EditorPrefs.SetBool(key, state);

        if (!minimalistic) GUILayout.Space(2f);
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!forceOn && !state) GUILayout.Space(3f);
        return state;
    }

    static public void BeginContents() { BeginContents(UISettings.minimalisticLook); }

    static public void BeginContents(bool minimalistic)
    {
        if (!minimalistic)
        {
            mEndHorizontal = true;
            GUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
        }
        else
        {
            mEndHorizontal = false;
            EditorGUILayout.BeginHorizontal(GUILayout.MinHeight(10f));
            GUILayout.Space(10f);
        }
        GUILayout.BeginVertical();
        GUILayout.Space(2f);
    }

    /// <summary>
    /// End drawing the content area.
    /// </summary>

    static public void EndContents()
    {
        GUILayout.Space(3f);
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        if (mEndHorizontal)
        {
            GUILayout.Space(3f);
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(3f);
    }

    /// <summary>
    /// Draw a list of fields for the specified list of delegates.
    /// </summary>

    static public void DrawEvents(string text, UnityEngine.Object undoObject, List<EventDelegate> list)
    {
        DrawEvents(text, undoObject, list, null, null, false);
    }

    /// <summary>
    /// Draw a list of fields for the specified list of delegates.
    /// </summary>

    static public void DrawEvents(string text, UnityEngine.Object undoObject, List<EventDelegate> list, string noTarget, string notValid, bool minimalistic)
    {
        if (!UIEditorTools.DrawHeader(text, text, false, minimalistic)) return;

        if (!minimalistic)
        {
            UIEditorTools.BeginContents(minimalistic);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            EventDelegateEditor.Field(undoObject, list, notValid, notValid, minimalistic);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            UIEditorTools.EndContents();
        }
        else EventDelegateEditor.Field(undoObject, list, notValid, notValid, minimalistic);
    }

    /// <summary>
    /// Draw 18 pixel padding on the right-hand side. Used to align fields.
    /// </summary>

    static public void DrawPadding()
    {
        if (!UISettings.minimalisticLook)
            GUILayout.Space(18f);
    }
}
