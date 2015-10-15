
using System.Collections.Generic;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class UIConfigDictionary : GameObjectConfigDictionary<UIConfig>
    {
        protected override void InitDictionary()
        {
            nameDic = new Dictionary<string, UIConfig>();
            foreach (UIConfig item in itemList)
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
    }
}
