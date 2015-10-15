
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class GameObjectConfigDictionary<T> : ScriptableObject where T : GameObjectConfig
    {
        public List<T> itemList;

        protected Dictionary<string, T> nameDic;

        protected virtual void InitDictionary()//abstract
        {
            nameDic = new Dictionary<string, T>();
            foreach (T item in itemList)
            {
                if (nameDic.ContainsKey(item.name))
                {
                    D.error("nameDic ID duplicate!! ");
                }
                else
                {
                    nameDic.Add(item.name, item);
                }
            }
        }

        public virtual T this[string name]
        {
            get
            {
                if (nameDic == null)
                {
                    InitDictionary();
                }

                if (nameDic.ContainsKey(name))
                {
                    return nameDic[name];
                }

                return null;
            }
        }
    }
}
