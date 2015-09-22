/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/12/2014 2:38:36 PM
* 文件名：ChangeAphal
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
/// </summary
[RequireComponent(typeof(Toggle))]
public class ToggleAllChildren : MonoBehaviour
{
    [HideInInspector]
    private Toggle _tog;

    void Start()
    {
        _tog = gameObject.GetComponent<Toggle>();
        _tog.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(_tog.isOn);
    }

    void OnDestroy()
    {
        _tog.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool change)
    {
        _tog.targetGraphic.gameObject.SetActive(!change);
        _tog.graphic.gameObject.SetActive(change);
    }
}