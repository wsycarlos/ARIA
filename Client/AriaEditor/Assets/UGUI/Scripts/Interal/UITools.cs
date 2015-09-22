/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：6/9/2015 5:03:52 PM
* 文件名：NGUITools
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 功能概述：
/// </summary>
public class UITools
{
    /// <summary>
    /// Convenience function that marks the specified object as dirty in the Unity Editor.
    /// </summary>

    static public void SetDirty(UnityEngine.Object obj)
    {
#if UNITY_EDITOR
        if (obj)
        {
            //if (obj is Component) Debug.Log(NGUITools.GetHierarchy((obj as Component).gameObject), obj);
            //else if (obj is GameObject) Debug.Log(NGUITools.GetHierarchy(obj as GameObject), obj);
            //else Debug.Log("Hmm... " + obj.GetType(), obj);
            UnityEditor.EditorUtility.SetDirty(obj);
        }
#endif
    }

    /// <summary>
    /// Convenience function that converts Class + Function combo into Class.Function representation.
    /// </summary>

    static public string GetFuncName(object obj, string method)
    {
        if (obj == null) return "<null>";
        string type = obj.GetType().ToString();
        int period = type.LastIndexOf('/');
        if (period > 0) type = type.Substring(period + 1);
        return string.IsNullOrEmpty(method) ? type : type + "/" + method;
    }
}
