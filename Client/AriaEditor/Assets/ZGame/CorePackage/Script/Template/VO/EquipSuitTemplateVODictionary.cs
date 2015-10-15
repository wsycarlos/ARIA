/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EquipSuitTemplateVODictionary : ClientTemplateDictionary<EquipSuitTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, EquipSuitTemplateVO>();
        foreach (EquipSuitTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[EquipSuitTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}