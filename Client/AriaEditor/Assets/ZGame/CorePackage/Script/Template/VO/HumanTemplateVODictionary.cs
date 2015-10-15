/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HumanTemplateVODictionary : ClientTemplateDictionary<HumanTemplateVO>
{
    protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, HumanTemplateVO>();
        foreach (HumanTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[HumanTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}