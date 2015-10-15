using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreatNewItem
{
    [MenuItem("UGUI/UI/Canvas", false, 0)]
    private static void NewCanvas()
    {
        GameObject go = new GameObject("NEW_UI_CANVAS");
        Canvas canvas = go.AddComponent<Canvas>();
        CanvasScaler canvasScaler = go.AddComponent<CanvasScaler>();
        GraphicRaycaster graph = go.AddComponent<GraphicRaycaster>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(640, 960);

        Selection.activeGameObject = go;
    }

    [MenuItem("UGUI/UI/Image", false, 1)]
    public static void NewImage()
    {
        GameObject parent = Selection.activeGameObject;

        if (null != parent)
        {
            if (null != parent.GetComponent<RectTransform>())
            {
                Selection.activeGameObject = UGUISettings.AddImage(parent).gameObject;
            }
            else
            {
                Debug.LogError("Image object must be added to a gameobject with a 'RectTransform' component");
            }
        }
        else
        {
            Debug.LogError("You must select a game object first.");
        }
    }

    [MenuItem("UGUI/UI/Text", false, 2)]
    public static void NewText()
    {
        GameObject parent = Selection.activeGameObject;

        if (null != parent)
        {
            if (null != parent.GetComponent<RectTransform>())
            {
                Selection.activeGameObject = UGUISettings.AddText(parent).gameObject;
            }
            else
            {
                Debug.LogError("Text object must be added to a gameobject with a 'RectTransform' component");
            }
        }
        else
        {
            Debug.LogError("You must select a game object first.");
        }
    }

    [MenuItem("UGUI/UI/Panel", false, 3)]
    public static void NewPanel()
    {
        GameObject parent = Selection.activeGameObject;

        if (null != parent)
        {
            if (null != parent.GetComponent<RectTransform>())
            {
                Selection.activeGameObject = UGUISettings.AddPanel(parent).gameObject;
            }
            else
            {
                Debug.LogError("Panel object must be added to a gameobject with a 'RectTransform' component");
            }
        }
        else
        {
            Debug.LogError("You must select a game object first.");
        }
    }

    [MenuItem("UGUI/UI/Grid", false, 4)]
    public static void NewGridLayout()
    {
        GameObject parent = Selection.activeGameObject;

        if (null != parent)
        {
            if (null != parent.GetComponent<RectTransform>())
            {
                Selection.activeGameObject = UGUISettings.AddGridLayout(parent).gameObject;
            }
            else
            {
                Debug.LogError("Panel object must be added to a gameobject with a 'RectTransform' component");
            }
        }
        else
        {
            Debug.LogError("You must select a game object first.");
        }
    }

    [MenuItem("UGUI/UI/ScrollRect", false, 5)]
    public static void NewScrollview()
    {
        GameObject parent = Selection.activeGameObject;

        if (null != parent)
        {
            if (null != parent.GetComponent<RectTransform>())
            {
                Selection.activeGameObject = UGUISettings.AddScrollRect(parent).gameObject;
                //Selection.activeGameObject.AddComponent<Mask>();
            }
            else
            {
                Debug.LogError("Panel object must be added to a gameobject with a 'RectTransform' component");
            }
        }
        else
        {
            Debug.LogError("You must select a game object first.");
        }
    }
}
