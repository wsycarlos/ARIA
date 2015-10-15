/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：3/31/2015 2:22:20 PM
* 文件名：AttributeUtil
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


using System.Reflection;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class AttributeUtil
    {
        public static FieldInfo[] GetFieldInfos(System.Object target, BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static)
        {
            return target.GetType().GetFields(flag);
        }

        public static T GetConfigAttribute<T>(FieldInfo field) where T : class
        {
            object[] attrs = field.GetCustomAttributes(typeof(T), false);
            T def = default(T);
            if (attrs.Length > 0)
            {
                def = attrs[0] as T;
            }

            return def as T;
        }
    }
}
