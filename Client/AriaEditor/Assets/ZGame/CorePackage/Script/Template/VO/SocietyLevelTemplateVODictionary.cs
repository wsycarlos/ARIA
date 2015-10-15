/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SocietyLevelTemplateVODictionary : ClientTemplateDictionary<SocietyLevelTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, SocietyLevelTemplateVO>();
        foreach (SocietyLevelTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[SocietyLevelTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}