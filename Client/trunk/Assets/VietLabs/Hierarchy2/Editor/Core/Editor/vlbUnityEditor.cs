using UnityEditor;
using UnityEngine;

namespace vietlabs
{
    public static class vlbUnityEditor
    {
        public static void BreakPrefab(this GameObject go, string tempName = "vlb_dummy.prefab") {
            var go2 = PrefabUtility.FindRootGameObjectWithSameParentPrefab(go);

            PrefabUtility.DisconnectPrefabInstance(go2);
            var prefab = PrefabUtility.CreateEmptyPrefab("Assets/" + tempName);
            PrefabUtility.ReplacePrefab(go2, prefab, ReplacePrefabOptions.ConnectToPrefab);
            PrefabUtility.DisconnectPrefabInstance(go2);
            AssetDatabase.DeleteAsset("Assets/" + tempName);

            //temp fix to hide Inspector's dirty looks
            Selection.instanceIDs = new int[] { };
        }
        public static void SelectPrefab(this GameObject go)
        {
            var prefab = PrefabUtility.GetPrefabParent(PrefabUtility.FindRootGameObjectWithSameParentPrefab(go));
            Selection.activeObject = prefab;
            EditorGUIUtility.PingObject(prefab.GetInstanceID());
        }
        public static void Reparent(this Transform t, string undo, Transform parent)
        {
            if (t == null || t == parent || t.parent == parent) return;
            t.gameObject.layer = parent.gameObject.layer;
            Undo.SetTransformParent(t.transform, parent, undo);
        }

        public static void SetLocalTransform(this Transform t, string undo, Vector3? pos = null, Vector3? scl = null, Vector3? rot = null)
        {
            Undo.RecordObject(t, undo);
            if (scl != null) t.localScale = scl.Value;
            if (rot != null) t.localEulerAngles = rot.Value;
            if (pos != null) t.localPosition = pos.Value;
        }
        public static void SetLocalPosition(this Transform t, Vector3 pos, string undo)
        {
            t.SetLocalTransform(undo, pos);
        }
        public static void SetLocalScale(this Transform t, Vector3 scl, string undo)
        {
            t.SetLocalTransform(undo, null, scl);
        }
        public static void SetLocalRotation(this Transform t, Vector3 rot, string undo)
        {
            t.SetLocalTransform(undo, null, null, rot);
        }

        public static void ResetLocalTransform(this Transform t, string undo)
        {
            SetLocalTransform(t, undo, Vector3.zero, Vector3.one, Vector3.zero);
        }
        public static void ResetLocalPosition(this Transform t, string undo)
        {
            t.SetLocalTransform(undo, Vector3.zero);
        }
        public static void ResetLocalScale(this Transform t, string undo)
        {
            t.SetLocalTransform(undo, null, Vector3.one);
        }
        public static void ResetLocalRotation(this Transform t, string undo)
        {
            t.SetLocalTransform(undo, null, null, Vector3.zero);
        }

        public static Transform NewTransform(string name, string undo, Transform p, Vector3? pos = null, Vector3? scl = null, Vector3? rot = null) {
            var t = new GameObject { name = name }.transform;
            Undo.RegisterCreatedObjectUndo(t.gameObject, undo);
            t.Reparent(undo, p);
            t.SetLocalTransform(undo, pos ?? Vector3.zero, scl ?? Vector3.one, rot ?? Vector3.zero);
            return t;
        }
        public static GameObject NewPrimity(this PrimitiveType type, string name, string undo, Transform p, Vector3? pos = null, Vector3? scl = null, Vector3? rot = null) {
            var primity = GameObject.CreatePrimitive(type);
            Undo.RegisterCreatedObjectUndo(primity, undo);
            primity.transform.Reparent(undo, p);
            primity.name = name;
            primity.transform.SetLocalTransform(undo, pos ?? Vector3.zero, scl ?? Vector3.one, rot ?? Vector3.zero);
            return primity;
        }
        
        static public bool IsAsset(this Object obj) {
            return obj != null && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(obj));
        }
        public static void Ping(this Object obj)
        {
            EditorGUIUtility.PingObject(obj);

            if (obj is MonoBehaviour)
            {
                EditorGUIUtility.PingObject(MonoScript.FromMonoBehaviour(obj as MonoBehaviour));
            }
            else if (obj is ScriptableObject)
            {
                EditorGUIUtility.PingObject(MonoScript.FromScriptableObject(obj as ScriptableObject));
            }
        }
        static public void RecordUndo(this Object go, string undoKey, bool full = false) {
            if (string.IsNullOrEmpty(undoKey)) return;
            if (full) {
                Undo.RegisterCompleteObjectUndo(go, undoKey);
            } else {
                Undo.RecordObject(go, undoKey);
            }
        }
    }    
}