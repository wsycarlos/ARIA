/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OfficerTemplateVODictionary : ClientTemplateDictionary<OfficerTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, OfficerTemplateVO>();
        foreach (OfficerTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[OfficerTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}