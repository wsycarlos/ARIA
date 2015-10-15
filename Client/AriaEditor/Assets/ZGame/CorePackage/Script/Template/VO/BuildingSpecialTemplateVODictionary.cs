/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingSpecialTemplateVODictionary : ClientTemplateDictionary<BuildingSpecialTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BuildingSpecialTemplateVO>();
        foreach (BuildingSpecialTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BuildingSpecialTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}