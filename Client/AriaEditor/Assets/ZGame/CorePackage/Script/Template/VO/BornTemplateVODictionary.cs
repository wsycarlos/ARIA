/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BornTemplateVODictionary : ClientTemplateDictionary<BornTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BornTemplateVO>();
        foreach (BornTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BornTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}