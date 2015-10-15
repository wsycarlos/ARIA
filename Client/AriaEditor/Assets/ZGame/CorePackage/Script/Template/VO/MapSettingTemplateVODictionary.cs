/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapSettingTemplateVODictionary : ClientTemplateDictionary<MapSettingTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapSettingTemplateVO>();
        foreach (MapSettingTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapSettingTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}