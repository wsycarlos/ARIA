/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapDragonTemplateVODictionary : ClientTemplateDictionary<MapDragonTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapDragonTemplateVO>();
        foreach (MapDragonTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapDragonTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}