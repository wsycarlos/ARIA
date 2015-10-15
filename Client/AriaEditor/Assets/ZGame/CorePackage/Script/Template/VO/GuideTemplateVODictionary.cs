/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GuideTemplateVODictionary : ClientTemplateDictionary<GuideTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, GuideTemplateVO>();
        foreach (GuideTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[GuideTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}