/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapMonsterTemplateVODictionary : ClientTemplateDictionary<MapMonsterTemplateVO>
{
    protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapMonsterTemplateVO>();
        foreach (MapMonsterTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapMonsterTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}