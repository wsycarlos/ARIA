/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingLevelTemplateVODictionary : ClientTemplateDictionary<BuildingLevelTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BuildingLevelTemplateVO>();
        foreach (BuildingLevelTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BuildingLevelTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}