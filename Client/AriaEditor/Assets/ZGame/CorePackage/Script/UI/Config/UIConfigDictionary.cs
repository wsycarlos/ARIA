/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：3/31/2015 2:27:30 PM
* 文件名：UIConfigDictionary
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


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
