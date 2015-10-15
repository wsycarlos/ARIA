/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapMineTemplateVODictionary : ClientTemplateDictionary<MapMineTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapMineTemplateVO>();
        foreach (MapMineTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapMineTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}