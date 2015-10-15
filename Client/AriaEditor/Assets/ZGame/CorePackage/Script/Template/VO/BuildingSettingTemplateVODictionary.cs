/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingSettingTemplateVODictionary : ClientTemplateDictionary<BuildingSettingTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BuildingSettingTemplateVO>();
        foreach (BuildingSettingTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BuildingSettingTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}