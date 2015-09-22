/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/13/2014 4:15:24 PM
* 文件名：Localize
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
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 功能概述：
/// </summary>
[RequireComponent(typeof(Text))]
public class Localize : MonoBehaviour
{
    [HideInInspector]
    public string key = "";

    [HideInInspector]
    public LocalizationType type = LocalizationType.en;

    void Start()
    {
        gameObject.GetComponent<Text>().text = UGUIUtils.GetLocalization(key);
    }
}
