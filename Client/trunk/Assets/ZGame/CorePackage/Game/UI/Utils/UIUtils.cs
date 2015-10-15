
using System.Reflection;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class UIUtils
    {
        /// <summary>
        /// 根据定义的UIAutoFind 属性 自动找到对应名字的GameObject对象
        /// </summary>
        /// <returns>
        /// The find U.
        /// </returns>
        /// <param name='obj'>
        /// If set to <c>true</c> object.
        /// </param>
        public static bool AutoFindUI<T>(T obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public);
            //FieldInfo[] props = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);//| BindingFlags.DeclaredOnly 
            bool allfind = true;
            //Debug.Log("UI props count" + props.Length);

            foreach (PropertyInfo prop in props)
            {
                Debug.Log(prop.Name);
                //object[] attrs = prop.GetCustomAttributes(typeof(UIAutoFindAttribute), false);
                //if (attrs.Length > 0)
                //{
                //    object gval = prop.GetValue(obj);

                //    //防止克隆的东西重复找 
                //    //FIXME 脚本里有如果直接绑定在物体上 GameObject 类型会不为空,但ToString = "null",加个判断
                //    if (gval != null && gval.ToString() != "null")
                //        continue;
                //    D.log("AutoFindUI : " + prop.Name + " type = " + prop.FieldType.ToString());
                //    //GameObject go = UnityUtil.FindChild(obj.gameObject, prop.Name);
                //    //if (prop.FieldType == typeof(GameObject))
                //    //{
                //    //    prop.SetValue(obj, go);

                //    //}
                //    //else
                //    //{
                //    //    //找相关ngui对象
                //    //    if (go != null)
                //    //    {
                //    //        prop.SetValue(obj, go.GetComponent(prop.FieldType));
                //    //    }
                //    //}

                //    //if (prop.GetValue(obj) == null)
                //    //{
                //    //    allfind = false;
                //    //    D.warn("[AutoFindUI]:" + obj.GetType().ToString() + " " + prop.Name + " not found!!!");
                //    //}
                //}
            }
            return allfind;
        }

        public static GameObject FindUI(GameObject gameObject, string key)
        {
            //Debug.Log("[UIUtils]FindUI:" + key);
            return UnityUtil.FindChild(gameObject, key);
        }
    }
}
