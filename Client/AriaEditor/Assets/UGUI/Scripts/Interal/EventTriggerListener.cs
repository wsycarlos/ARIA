/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/5/2014 8:13:34 PM
* 文件名：EventTriggerListener
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
using UnityEngine.EventSystems;

/// <summary>
/// 功能概述：
/// </summary>
public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISelectHandler, IUpdateSelectedHandler
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null)
        {
            listener = go.AddComponent<EventTriggerListener>();
        }

        return listener;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
        {
            onEnter(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
        {
            onExit(gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
        {
            onUp(gameObject);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
        {
            onSelect(gameObject);
        }
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
        {
            onUpdateSelect(gameObject);
        }
    }
}
