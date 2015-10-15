/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：3/31/2015 2:23:34 PM
* 文件名：GameObjectConfigDictionary
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


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
