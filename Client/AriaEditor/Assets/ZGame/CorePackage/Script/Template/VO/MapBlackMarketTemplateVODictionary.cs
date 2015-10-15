/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapBlackMarketTemplateVODictionary : ClientTemplateDictionary<MapBlackMarketTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapBlackMarketTemplateVO>();
        foreach (MapBlackMarketTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapBlackMarketTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}