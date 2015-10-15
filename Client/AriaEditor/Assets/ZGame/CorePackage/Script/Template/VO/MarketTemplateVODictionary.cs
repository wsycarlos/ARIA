/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MarketTemplateVODictionary : ClientTemplateDictionary<MarketTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, MarketTemplateVO>();
        foreach (MarketTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[MarketTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}