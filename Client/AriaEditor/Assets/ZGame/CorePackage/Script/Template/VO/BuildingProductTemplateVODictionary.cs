/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingProductTemplateVODictionary : ClientTemplateDictionary<BuildingProductTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BuildingProductTemplateVO>();
        foreach (BuildingProductTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BuildingProductTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}