/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TalentTemplateVODictionary : ClientTemplateDictionary<TalentTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, TalentTemplateVO>();
        foreach (TalentTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[TalentTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}