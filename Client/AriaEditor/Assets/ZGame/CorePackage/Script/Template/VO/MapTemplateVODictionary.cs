/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapTemplateVODictionary : ClientTemplateDictionary<MapTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapTemplateVO>();
        foreach (MapTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}