/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChargeTemplateVODictionary : ClientTemplateDictionary<ChargeTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, ChargeTemplateVO>();
        foreach (ChargeTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[ChargeTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}