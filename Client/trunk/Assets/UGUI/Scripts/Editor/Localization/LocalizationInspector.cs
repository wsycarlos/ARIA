#region Version Info
/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/13/2014 4:16:11 PM
* 文件名：LocalizationInspector
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// 功能概述：
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(Localize), true)]
public class LocalizationInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUI.changed = false;

        Localize localize = target as Localize;

        string key = EditorGUILayout.TextField("多语言key", localize.key);
        Enum type = EditorGUILayout.EnumPopup("多语言类型", localize.type);

        if (GUI.changed)
        {
            localize.key = key;
            localize.type = (LocalizationType)type;
            localize.gameObject.GetComponent<Text>().text = GetValue(key, (LocalizationType)type);

            //TODO Fuck ，不会自己刷新，ApplyModifiedProperties不起作用，还没找到原因
            localize.gameObject.SetActive(false);
            localize.gameObject.SetActive(true);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string GetValue(string key, LocalizationType type)
    {
        LanguageDictionary dict = localization(type);
        LanguageVO vo = dict.itemList.Find(i => { return i.key == key; });
        string result = null == vo ? "" : vo.Value;
        return string.IsNullOrEmpty(result) ? key : result;
    }

    private LanguageDictionary localization(LocalizationType type)
    {
        return LoadTemplate<LanguageDictionary>("LocalizationData", type);
    }

    private T LoadTemplate<T>(string path, LocalizationType type) where T : ScriptableObject
    {
        string sourcePath = "Assets/UGUI/Export/Template/Localization/" + type.ToString().ToLower() + "/" + path;
        T dic = AssetDatabase.LoadAssetAtPath(sourcePath + ".asset", typeof(T)) as T;

        if (dic == null)
        {
            return null;
        }
        else
        {
            return dic;
        }
    }
}