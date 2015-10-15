/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HeroTemplateVODictionary : ClientTemplateDictionary<HeroTemplateVO>
{
    protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, HeroTemplateVO>();
        foreach (HeroTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[HeroTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}