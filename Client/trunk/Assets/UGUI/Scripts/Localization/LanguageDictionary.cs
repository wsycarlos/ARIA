/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/18/2014 7:44:09 PM
* 文件名：LanguageDictionary
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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