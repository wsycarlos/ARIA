using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace vietlabs
{
    static public class vlbObject
    {
        public static Bounds GetBound(this GameObject go, bool deep = false) {
            if (go == null) return default(Bounds);

            Bounds? b = null;
            var min = Vector3.zero;
            var max = Vector3.zero;
            bool first = true;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                var t = go.transform.GetChild(i);
                var mf = t.GetComponent<MeshFilter>();
                if (mf == null) continue;

                var tmpB = mf.sharedMesh.bounds;
                var c = tmpB.center;
                var sz = tmpB.extents;

                var points = new[] {
                new Vector3(c.x - sz.x, c.y - sz.y, c.z - sz.z),
                new Vector3(c.x + sz.x, c.y - sz.y, c.z - sz.z),
                new Vector3(c.x - sz.x, c.y - sz.y, c.z + sz.z),
                new Vector3(c.x + sz.x, c.y - sz.y, c.z + sz.z),
                new Vector3(c.x - sz.x, c.y + sz.y, c.z - sz.z),
                new Vector3(c.x + sz.x, c.y + sz.y, c.z - sz.z),
                new Vector3(c.x - sz.x, c.y + sz.y, c.z + sz.z),
                new Vector3(c.x + sz.x, c.y + sz.y, c.z + sz.z)
            };


            for (var j = 0; j < points.Length; j++)
            {
                var p = t.TransformPoint(points[j]);
                if (first)
                {
                    min = p;
                    max = p;
                    first = false;
                }
                else
                {
                    min.x = Mathf.Min(min.x, p.x);
                    min.y = Mathf.Min(min.y, p.y);
                    min.z = Mathf.Min(min.z, p.z);

                    max.x = Mathf.Max(max.x, p.x);
                    max.y = Mathf.Max(max.y, p.y);
                    max.z = Mathf.Max(max.z, p.z);
                }
            }

            tmpB = new Bounds((min + max) / 2f, (max - min));
            if (b == null)
            {
                b = tmpB;
            }
            else
            {
                var val = b.Value;
                val.Encapsulate(tmpB);
                b = val;
            }
        }

        return (b != null) ? b.Value : default(Bounds);
        }
	    public static bool GetFlag(this Object go, HideFlags flag) { return (go != null) && (go.hideFlags & flag) > 0; }
        public static void SetFlag(this Object go, HideFlags flag, bool value) {
            if (go == null) return;
            if (value) go.hideFlags |= flag;
            else go.hideFlags &= ~flag;
        }
        public static void ToggleFlag(this Object go, HideFlags flag) {
            go.SetFlag(flag, !go.GetFlag(flag));
        }
        public static void SetFlag(this Object[] list, HideFlags flag, Func<int, Object, bool> func) {
            for (var i = 0; i < list.Length; i++) {
                if (list[i] != null) list[i].SetFlag(flag, func(i, list[i]));
            }
        }

        private static Transform[] GetChildrenTransform(Transform t, bool deep = false, bool includeMe = false, bool activeOnly = false)
        {
            var children = new ArrayList();

            if (includeMe && (t.gameObject.activeSelf || !activeOnly)) children.Add(t);

            foreach (Transform child in t)
            {
                if (deep) children.AddRange(GetChildrenTransform(child, true, includeMe, activeOnly));
                if ((!deep || !includeMe) && (child.gameObject.activeSelf || !activeOnly)) children.Add(child);
            }

            return (Transform[])children.ToArray(typeof(Transform));
        }

        public static GameObject[] GetChildren(this GameObject go, bool deep = false, bool includeMe = false, bool activeOnly = false)
        {
            Transform[] children = GetChildrenTransform(go.transform, deep, includeMe, activeOnly);
            return children.Select(child => child.gameObject).ToArray();
        }
        public static T[] GetChildren<T>(this Component comp, bool deep = false, bool includeMe = false, bool activeOnly = false) where T : Component
        {
            Transform[] children = GetChildrenTransform(comp.transform);
            return children.Select(child => child.GetComponent<T>()).ToArray();
        }

        public static T[] GetSiblings<T>(this Component comp, GameObject[] rootList) where T : Component
        {
            if (comp.transform.parent == null) return rootList.Select(item => item.GetComponent<T>()).ToArray();
            var list = GetChildrenTransform(comp.transform.parent);
            list.Remove(comp.transform);
            return list.Select(item => item.GetComponent<T>()).ToArray();
        }
        public static GameObject[] GetSiblings(this GameObject go, GameObject[] rootList)
        {
            if (go.transform.parent == null) return rootList;
            var list = GetChildrenTransform(go.transform.parent);
            list.Remove(go.transform);
            return list.Select(item => item.gameObject).ToArray();
        }
        public static GameObject[] GetParents(this GameObject go)
        {
            var p = go.transform.parent;
            var list = new List<GameObject>();
            while (p != null)
            {
                list.Add(p.gameObject);
                p = p.parent;
            }
            return list.ToArray();
        }

        public static int ParentCount(this GameObject go)
        {
            if (go == null || go.transform == null) return 0;
            var p = go.transform.parent;
            var cnt = 0;

            while (p != null)
            {
                cnt++;
                p = p.parent;
            }
            return cnt;
        }
        public static bool HasChild(this GameObject go)
        {
            return go.transform.childCount > 0;
        }



        public static void ForeachSibling(this GameObject go, List<GameObject> rootList, Action<GameObject> func)
        {
            ForeachSibling2(go, rootList, item => { func(item); return true; });
        }
        public static void ForeachSibling2(this GameObject go, List<GameObject> rootList, Func<GameObject, bool> func)
        {
            var p = go.transform.parent;

            if (p != null)
            {
                foreach (Transform t in go.transform.parent)
                {
                    if (t.gameObject != go)
                    {
                        if (!func(t.gameObject)) break;
                    }
                }
            }
            else
            {
                foreach (var child in rootList)
                {
                    if (child != go)
                    {
                        if (!func(child)) break;
                    }
                }
            }
        }
        public static void ForeachParent(this GameObject go, Action<GameObject> func)
        {
            ForeachParent2(go, item => { func(item); return true; });
        }
        public static void ForeachParent2(this GameObject go, Func<GameObject, bool> func)
        {
            var p = go.transform.parent;
            while (p != null)
            {
                if (!func(p.gameObject)) break;
                p = p.parent;
            }
        }
        
        public static void ForeachChild<T>(this Transform p, Func<T, bool> action, bool deep = false) where T : Component
        {
            foreach (Transform child in p)
            {
                var t = child.GetComponent<T>();
                if (deep) child.ForeachChild(action, true);

                if (t != null)
                {
                    if (!action(t)) return; //stop if enough
                }
            }
        }
        public static void ForeachChild2(this GameObject go, Func<GameObject, bool> action, bool deep = false)
        {
            go.transform.ForeachChild<Transform>(t => action(t.gameObject), deep);
        }
        public static void ForeachChild(this GameObject go, Action<GameObject> action, bool deep = false)
        {
            go.transform.ForeachChild<Transform>(t =>
            {
                action(t.gameObject);
                return true;
            }, deep);
        }

    }    
}

