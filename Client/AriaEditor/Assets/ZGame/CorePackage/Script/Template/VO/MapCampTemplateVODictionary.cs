/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapCampTemplateVODictionary : ClientTemplateDictionary<MapCampTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MapCampTemplateVO>();
        foreach (MapCampTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MapCampTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}