#region Version Info
/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/4/2014 12:56:09 PM
* 文件名：UGUISettings
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/
#endregion

using UnityEngine;
using UnityEngine.UI;

public class UGUISettings
{
    static public Image AddImage(GameObject go)
    {
        Image image = UGUIAddItemTools.AddGraphicUIItem<Image>(go);
        return image;
    }

    static public Text AddText(GameObject go)
    {
        Text text = UGUIAddItemTools.AddGraphicUIItem<Text>(go);
        return text;
    }

    static public Image AddPanel(GameObject go)
    {
        Image panel = UGUIAddItemTools.AddGraphicUIItem<Image>(go);
        panel.name = "Panel";
        panel.color = new Color(1, 1, 1, 100 / 255f);
        RectTransform rect = panel.gameObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
        return panel;
    }

    static public GridLayoutGroup AddGridLayout(GameObject go)
    {
        GridLayoutGroup grid = UGUIAddItemTools.AddGraphicUIItem<GridLayoutGroup>(go);
        return grid;
    }

    static public ScrollRect AddScrollRect(GameObject go)
    {
        ScrollRect scrollRect = UGUIAddItemTools.AddGraphicUIItem<ScrollRect>(go);
        scrollRect.gameObject.AddComponent<Mask>();
        Image image = scrollRect.gameObject.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 3 / 255f);

        return scrollRect;
    }
}