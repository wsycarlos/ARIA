/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EquipTemplateVODictionary : ClientTemplateDictionary<EquipTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, EquipTemplateVO>();
        foreach (EquipTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[EquipTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}