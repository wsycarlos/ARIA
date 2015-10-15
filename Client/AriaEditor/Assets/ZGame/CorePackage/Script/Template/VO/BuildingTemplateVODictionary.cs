/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingTemplateVODictionary : ClientTemplateDictionary<BuildingTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BuildingTemplateVO>();
        foreach (BuildingTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BuildingTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}