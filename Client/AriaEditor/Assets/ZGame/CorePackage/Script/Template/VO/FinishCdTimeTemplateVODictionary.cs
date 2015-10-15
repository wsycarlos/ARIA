/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FinishCdTimeTemplateVODictionary : ClientTemplateDictionary<FinishCdTimeTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, FinishCdTimeTemplateVO>();
        foreach (FinishCdTimeTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[FinishCdTimeTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}