
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
