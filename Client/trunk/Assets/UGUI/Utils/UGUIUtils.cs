/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/17/2014 12:00:18 PM
* 文件名：UGUIUtils
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
public class UGUIUtils
{
    private static AssetBundle _bundle;
    private static LanguageDictionary _languageData;

    public static AssetBundle bundle
    {
        set { _bundle = value; }
    }

    public static LanguageDictionary languageData
    {
        set { _languageData = value; }
    }

    public static void OnClickCallBack(GameObject go, EventTriggerListener.VoidDelegate callback)
    {
        EventTriggerListener listener = EventTriggerListener.Get(go);

        if (null != listener)
        {
            listener.onClick += callback;
        }
    }

    public static void OffClickCallBack(GameObject go, EventTriggerListener.VoidDelegate callback)
    {
        EventTriggerListener listener = EventTriggerListener.Get(go);

        if (null != listener)
        {
            listener.onClick -= callback;
        }
    }

    /// <summary>
    /// 根据图片的名字取得对应的sprite
    /// </summary>
    /// <param name="spname"></param>
    /// <returns></returns>
    public static Sprite GetSprite(string spname)
    {
        if (null == _bundle)
        {
            return null;
        }

        Sprite sp = _bundle.LoadAsset<Sprite>(spname);

        if (null == sp)
        {
            return null;
        }

        return sp;
    }

    public static string GetLocalization(string key)
    {
        if (null == _languageData)
        {
            return key;
        }

        LanguageVO vo = _languageData[key];

        if (null != vo)
        {
            return vo.Value;
        }

        return key;
    }
}