/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DropTemplateVODictionary : ClientTemplateDictionary<DropTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, DropTemplateVO>();
        foreach (DropTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[DropTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}