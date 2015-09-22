using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace vietlabs {
    public static class vlbEditorWindow {
        private static Dictionary<string, EditorWindow> WindowDict;
        static public void ClearDefinitionCache() {
            WindowDict = new Dictionary<string, EditorWindow>();
        }
        static public EditorWindow GetEditorWindowByName(this string className, string pck = "UnityEditor")
        {
            if (WindowDict == null) WindowDict = new Dictionary<string, EditorWindow>();
            var hasCache = WindowDict.ContainsKey(className);
            var window = hasCache ? WindowDict[className] : null;

            if (hasCache) {
                if (window != null) return window;
                WindowDict.Remove(className);
            }
            var typeT       = className.GetTypeByName(pck);
            //var objArray    = Resources.FindObjectsOfTypeAll(typeT);

            window = EditorWindow.GetWindow(typeT);
            if (window != null) WindowDict.Add(className, window);
            return window;
        }

        static public EditorWindow Inspector {
            get { return "UnityEditor.InspectorWindow".GetEditorWindowByName(); }
        }
        static public EditorWindow Hierarchy {
            get {
                var window =
#if UNITY_4_5 || UNITY_4_6
                "UnityEditor.SceneHierarchyWindow".GetEditorWindowByName();
#else
                "UnityEditor.HierarchyWindow".GetEditorWindowByName(); 
#endif
                return window;
            }
        }

        internal static T AsDropdown<T>(this Rect rect) where T : EditorWindow {
            var edw = ScriptableObject.CreateInstance<T>();
            var r2 = GUIUtility.GUIToScreenPoint(rect.XY_AsVector2());
            rect.x = r2.x;
            rect.y = r2.y;

            edw.ShowAsDropDown(rect.h(18f), rect.WH_AsVector2());
            edw.Focus();
            edw.GetField("m_Parent")
                .Invoke("AddToAuxWindowList", "UnityEditor.GUIView".GetTypeByName("UnityEditor"));
            edw.wantsMouseMove = true;
            return edw;
        }
    }
}