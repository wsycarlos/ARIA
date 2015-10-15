/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorTemplateVODictionary : ClientTemplateDictionary<ActorTemplateVO>
{
 	protected override void InitDictionary()
    {
        itemDic = new Dictionary<int, ActorTemplateVO>();
        foreach (ActorTemplateVO item in itemList)
        {
            if (itemDic.ContainsKey(item.id))
            {
                Debug.LogWarning("[ActorTemplateVODictionary]Item ID duplicate!! " + item.id);
            }
            else
            {
                itemDic.Add(item.id, item);
            }
        }
    }
}