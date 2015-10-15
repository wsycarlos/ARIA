using UnityEngine;
using System.Collections.Generic;

public abstract class ClientTemplateDictionary<T> : ScriptableObject where T : TemplateVO
{
    public List<T> itemList;
    protected Dictionary<int, T> itemDic;

    protected abstract void InitDictionary();

    public virtual T this[int id]
    {
        get
        {
            if (itemDic == null)
            {
                InitDictionary();
            }

            if (itemDic.ContainsKey(id))
            {
                return itemDic[id];
            }

            return null;
        }
    }
}
