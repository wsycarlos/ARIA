using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UGUITools
{
    static public T AddGraphicUIItem<T>(GameObject go) where T : UIBehaviour
    {
        T layout = AddChild<T>(go);
        return layout;
    }

    static public T AddChild<T>(GameObject parent) where T : Component
    {
        GameObject go = AddChild(parent);
        go.name = GetTypeName<T>();
        return go.AddComponent<T>();
    }

    static public GameObject AddChild(GameObject parent)
    {
        return AddChild(parent, true);
    }

    static public GameObject AddChild(GameObject parent, bool undo)
    {
        GameObject go = new GameObject();

        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }

        return go;
    }

    static public string GetTypeName<T>()
    {
        string[] path = typeof(T).ToString().Split('.');
        return path[path.Length - 1];
    }
}
