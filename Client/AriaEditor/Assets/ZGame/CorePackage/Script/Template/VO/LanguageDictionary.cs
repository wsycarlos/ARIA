using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class LanguageDictionary : ScriptableObject
{
    public List<LanguageVO> itemList;

    protected Dictionary<string, LanguageVO> itemDic;

    protected void InitDictionary()
    {
        itemDic = new Dictionary<string, LanguageVO>();

        foreach (LanguageVO item in itemList)
        {
            if (itemDic.ContainsKey(item.key))
            {
                Debug.LogWarning("LanguageVO ID duplicate!! " + item.key);
            }
            else
            {
                itemDic.Add(item.key, item);
            }
        }
    }

    public virtual LanguageVO this[string id]
    {
        get
        {
            if (itemDic == null)
            {
                InitDictionary();
            }

            if (itemDic.ContainsKey(id))
            {
                return itemDic[id];
            }

            return null;
        }
    }
}