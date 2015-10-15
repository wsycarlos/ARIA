/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SocietyActionTemplateVODictionary : ClientTemplateDictionary<SocietyActionTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, SocietyActionTemplateVO>();
        foreach (SocietyActionTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[SocietyActionTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}