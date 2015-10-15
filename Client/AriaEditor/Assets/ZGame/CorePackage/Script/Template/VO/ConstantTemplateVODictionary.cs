/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConstantTemplateVODictionary : ClientTemplateDictionary<ConstantTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, ConstantTemplateVO>();
        foreach (ConstantTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[ConstantTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}