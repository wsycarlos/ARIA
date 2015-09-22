using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace vietlabs {
    public static class vlbEditor {
        public static void Move(this Component c, int delta)
        {
            while (delta > 0)
            {
                ComponentUtility.MoveComponentDown(c);
                delta--;
            }

            while (delta < 0)
            {
                ComponentUtility.MoveComponentUp(c);
                delta++;
            }
        }
        public static Editor[] InspectorComponentEditors
        {
            get
            {
                return ActiveEditorTracker.sharedTracker.activeEditors;
            }
        }

        public static bool GetEditorFlag(this Object obj, HideFlags flag)
        {
            return (obj as Editor).target.GetFlag(flag);
        }
        public static void SetEditorFlag(this Object obj, HideFlags flag, bool value)
        {
            (obj as Editor).target.SetFlag(flag, value);
        }

        public static void RevealChildrenInHierarchy(this GameObject go, bool pingMe = false)
        {
            if (go.transform.childCount == 0) return;
#if UNITY_4_5
            var tree = vlbEditorWindow.Hierarchy.GetField("m_TreeView");
            //var c = go.transform.childCount > 0 ? go.transform.GetChild(0).gameObject : go;
            //tree.Invoke("RevealNode", null, null, c.GetInstanceID());
            var item = tree.Invoke("FindNode", null, null, go.GetInstanceID());
            if (item != null) {
                tree.GetProperty("data").Invoke("SetExpanded", "UnityEditor.ITreeViewDataSource".GetTypeByName("UnityEditor"), null, item, true);
            }
            //vlbEditorWindow.Hierarchy.Repaint();
#else
            foreach (Transform child in go.transform)
            {
                if (child == go.transform) continue;
                vlbEditorWindow.Hierarchy.Invoke("PingTargetObject", null, null, new object[] { child.GetInstanceID() });
                if (pingMe) vlbEditorWindow.Hierarchy.Invoke("PingTargetObject", null, null, new object[] { go.GetInstanceID() });
                return;
            }
#endif
        }
        public static void SetEditorEnable(this Object editor, bool isEnable)
        {
            EditorUtility.SetObjectEnabled((editor as Editor).target, isEnable);
        }
        public static bool GetEditorEnable(this Object editor)
        {
            if (editor == null) return false;
            return EditorUtility.GetObjectEnabled((editor as Editor).target) == 1;
        }
        public static void ToggleEditorEnable(this Object editor)
        {
            if (editor != null) editor.SetEditorEnable(!editor.GetEditorEnable());
        }

        static internal string GetTitle(this Object obj, bool nicify = true)
        {
            if (obj == null) return "Null";

            var name = obj is MonoBehaviour
                ? MonoScript.FromMonoBehaviour((MonoBehaviour)obj).name
                : ObjectNames.GetClassName(obj);

            return nicify ? name : ObjectNames.NicifyVariableName(name);
        }
        static internal Type GetComponentTypeByName(this string cName)
        {
            var _tempGO = new GameObject();
            _tempGO.SetFlag(HideFlags.HideAndDontSave, true);
            UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(_tempGO, "Assets/VietLabs/Hierarchy2/Editor/Core/Editor/vlbEditor.cs (90,13)", cName);
            var c = _tempGO.GetComponent(cName);
            var t = c.GetType();
            Object.DestroyImmediate(_tempGO);
            return t;
        }
    }
}

