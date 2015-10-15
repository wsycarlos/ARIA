/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SocietyLivenessTemplateVODictionary : ClientTemplateDictionary<SocietyLivenessTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, SocietyLivenessTemplateVO>();
        foreach (SocietyLivenessTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[SocietyLivenessTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}