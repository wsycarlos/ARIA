/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ClubTipsTemplateVODictionary : ClientTemplateDictionary<ClubTipsTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, ClubTipsTemplateVO>();
        foreach (ClubTipsTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[ClubTipsTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}