using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections;

#endif
public class UnityUtil
{

    public static string GetPlatformName()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return "android";

            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXWebPlayer:
                return "pc";

            case RuntimePlatform.IPhonePlayer:
                return "ios";



        }
        return "";
    }

    public static void SetMeshLayer(GameObject g, int layer, Shader shader = null)
    {
        if (g != null)
        {
            Renderer[] renders = g.GetComponentsInChildren<Renderer>();
            if (renders != null)
                foreach (Renderer r in renders)
                {
                    r.gameObject.layer = layer;
                    if (shader != null)
                        r.material.shader = shader;
                }
        }
    }

    public static void SetMeshAlpha(GameObject g, float a)
    {
        if (g != null)
        {
            Renderer[] renders = g.GetComponentsInChildren<Renderer>();
            if (renders != null)
                foreach (Renderer r in renders)
                {
                    if (r.sharedMaterial != null && r.sharedMaterial.HasProperty("_echoRGBA"))
                    {
                        Vector4 vec = r.sharedMaterial.GetVector("_echoRGBA");
                        r.sharedMaterial.SetVector("_echoRGBA", new Vector4(vec.x, vec.y, vec.z, a));
                    }
                }
        }
    }

    /// <summary>
    /// Adds the child.
    /// </summary>
    /// <param name='parent'>
    /// Parent.
    /// </param>
    /// <param name='child'>
    /// Child.
    /// </param>
    public static void AddChild(GameObject parent, GameObject child, bool setParentLayer = true, bool initZero = false)
    {
        if (child != null && parent != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            if (initZero)
            {
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
            }
            if (setParentLayer)
                child.layer = parent.layer;
        }
    }

    public static void SetLayer(GameObject g, int layer, bool includeChild)
    {
        g.layer = layer;
        if (includeChild)
        {
            for (int i = 0; i < g.transform.childCount; i++)
            {
                Transform tr = g.transform.GetChild(i);
                SetLayer(tr.gameObject, layer, true);
            }
        }
    }

    /// <summary>
    /// Adds the child.
    /// </summary>
    /// <returns>
    /// The child.
    /// </returns>
    /// <param name='parent'>
    /// Parent.
    /// </param>
    /// <param name='name'>
    /// Name.
    /// </param>
    public static GameObject AddChild(GameObject parent, string name)
    {
        GameObject go = new GameObject(name);
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            if (parent != null)
            {
                t.parent = parent.transform;
                go.layer = parent.layer;
            }
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.name = name;

        }
        return go;
    }

    public static GameObject AddChild(GameObject parent, GameObject prefab, string name)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            if (parent != null)
            {
                t.parent = parent.transform;
                go.layer = parent.layer;
            }
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.name = name;

        }
        return go;
    }

    /// <summary>
    /// Removes all child.
    /// </summary>
    /// <param name='go'>
    /// Go.
    /// </param>
    public static void removeAllChild(GameObject go)
    {
        if (go != null && go.transform != null)
        {
            int len = go.transform.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                GameObject gg = go.transform.GetChild(i).gameObject;
                gg.SetActive(false);
#if UNITY_EDITOR
                GameObject.DestroyImmediate(gg);//(go.transform.GetChild (0).gameObject);
#else
                GameObject.Destroy(go.transform.GetChild(i).gameObject);
#endif
            }
        }
    }

    public static void HideInHierarchyAllChild(GameObject go)
    {
        if (go != null && go.transform != null)
        {
            int len = go.transform.childCount;
            for (int i = 0; i < len; i++)
            {
                go.transform.GetChild(i).gameObject.hideFlags =
                 HideFlags.HideInHierarchy | HideFlags.NotEditable;
                //#if UNITY_EDITOR
                //                GameObject.DestroyImmediate (go.transform.GetChild (0).gameObject);
                //#else
                //               GameObject.Destroy (go.transform.GetChild (i).gameObject);
                //#endif
            }
        }
    }

    /// <summary>
    /// 寻找子节点
    /// </summary>
    /// <param name="name">节点名称</param>
    /// <returns></returns>
    public static GameObject FindChild(GameObject gameObject, string name)
    {
        Transform res = FindChild(gameObject.transform, name);
        if (res != null)
            return res.gameObject;
        else
            return null;
    }
    /// <summary>
    /// 寻找子的transform
    /// </summary>
    /// <param name="transform">要查找的节点的transform</param>
    /// <param name="name">名称</param>
    /// <returns></returns>
    public static Transform FindChild(Transform transform, string name)
    {
        Transform res = transform.FindChild(name);
        if (res == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                res = FindChild(transform.GetChild(i), name);
                if (res != null)
                    break;
            }
        }

        return res;
    }
}
