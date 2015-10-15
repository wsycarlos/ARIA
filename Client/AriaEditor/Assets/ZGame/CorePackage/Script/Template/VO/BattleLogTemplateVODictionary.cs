/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleLogTemplateVODictionary : ClientTemplateDictionary<BattleLogTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, BattleLogTemplateVO>();
        foreach (BattleLogTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[BattleLogTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}