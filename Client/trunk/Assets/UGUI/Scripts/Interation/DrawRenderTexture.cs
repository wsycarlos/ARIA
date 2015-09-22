/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：1/16/2015 2:47:59 PM
* 文件名：DrawRenderTexture
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

/// <summary>
/// 功能概述：
/// </summary>
public class DrawRenderTexture : MonoBehaviour
{
    public RenderTexture t;

    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = t;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 100, 100), t);
    }
}
