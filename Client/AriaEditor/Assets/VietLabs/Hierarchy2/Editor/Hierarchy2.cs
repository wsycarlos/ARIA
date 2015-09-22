/*
------------------------------------------------
    Hierarchy2 for Unity3d by VietLabs
------------------------------------------------
    version : 1.3.8 Beta 3
    release : 04 Sep 2014
    require : Unity3d 4.3+
    website : http://vietlabs.net/hierarchy2
--------------------------------------------------

Powerful extension to add the most demanding features
to Hierarchy panel packed in a single, lightweight,
concise and commented C# source code that fully 
integrated into Unity Editor 

--------------------------------------------------
*/

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

namespace vietlabs
{

    [InitializeOnLoad]
    class Hierarchy2
    {
        //------------------------------ CONFIG -----------------------------------------

        internal static bool HLEnabled = true;
        internal static bool HLFull = false;
		public static bool UseDkVisible = true; 

        internal static bool AllowRenameLockedGO = true;
        internal static bool AllowDragLockedGO = false;

        internal static bool ShowStaticIcon     = false;
        internal static bool ShowLockIcon       = true;
        internal static bool ShowChildCount     = true;
        internal static bool ShowEyeIcon        = true;

        internal static float IconsOffset       = 0f;
        internal static bool AllowAltShortcuts = true;
        internal static bool AllowShiftShortcuts = true;
        internal static bool AllowOtherShortcuts = true;

        //Ignore (don't show greenbars) for scripts in these folders
        internal static string[] IgnoreScriptPaths = {
			".dll",
			"Daikon Forge",
			"FlipbookGames",
			"iTween",
			"NGUI",
			"PlayMaker",
			"TK2DROOT",
			"VietLabs"
		};

        internal static readonly Color[] HLColors = { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta };
		
        /*[MenuItem("Window/Hierarchy2/Toggle Highlight")]
        static void ToggleHighlight() { HLEnabled = !HLEnabled; }

        [MenuItem("Window/Hierarchy2/Toggle Highlight Full")]
        static void ToggleHighlightMode() { HLEnabled = true; HLFull = !HLFull; }*/

        [MenuItem("Window/Hierarchy2/Reset")]
        static void Reset()
        {
            Hierarchy2Api.RootGOList.ForEach(rootGO =>
            {
                rootGO.hideFlags = 0;
                rootGO.ForeachChild(child =>
                {
                    child.hideFlags = 0;
                });
            });
        }

        //---------------------------- ROOT CACHE ---------------------------------------

        static Hierarchy2()
        {
            EditorApplication.hierarchyWindowChanged    += Hierarchy2Api.UpdateRoot;
            EditorApplication.hierarchyWindowItemOnGUI  += HierarchyItemCB;
            EditorApplication.playmodeStateChanged      += OnPlayModeChanged;

            Undo.undoRedoPerformed += () =>
            {
                //TODO : narrow down & only perform on correct undo targets

                //BUGFIXED : Quick + dirty patch to force Hierarchy refresh to show correct children
                Hierarchy2Api.RootGOList.ForEach(rootGO => rootGO.ForeachChild(child =>
                {
                    child.ToggleFlag(HideFlags.NotEditable);
                    child.ToggleFlag(HideFlags.NotEditable);
                }, true));

                vlbEditorWindow.Hierarchy.Repaint();
            };
        }
        static void OnPlayModeChanged()
        {
            if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
            { //stop playing
                Hierarchy2Api.ltCamera = null;
                Hierarchy2Api.ltCameraInfo = null;
            }
        }

        //-------------------------------- CONTEXT ---------------------------------------

        internal static void Context_BuiltIn(GenericMenu menu, GameObject go, string category = "")
        {
            menu.Add(category + "Copy %C", () =>
            {
                Selection.activeGameObject = go;
                Unsupported.CopyGameObjectsToPasteboard();
            });

            menu.Add(category + "Paste %V", () =>
            {
                Selection.activeGameObject = go;
                Unsupported.PasteGameObjectsFromPasteboard();
            });

            menu.AddSep(category);
            menu.Add(category + "Rename _F2", () =>
            {
                Selection.activeGameObject = go;
                go.Rename();
            });
            menu.Add(category + "Duplicate %D", () =>
            {
                Selection.activeGameObject = go;
                Unsupported.DuplicateGameObjectsUsingPasteboard();
            });

            menu.Add(category + "Delete _Delete", () =>
            {
                Selection.activeGameObject = go;
                Unsupported.DeleteGameObjectSelection();
            });
        }
        internal static void Context_Basic(GenericMenu menu, GameObject go, string category = "Edit/")
        {
            //basic tools
            menu.Add(category + "Lock _L", () => go.SetSmartLock(false, false), go.IsLock());
            menu.Add(category + "Visible _A , V", () => go.ToggleActive(false), go.activeSelf);
            menu.Add(category + "Combine Children _C", () => go.ToggleCombine(), go.IsCombined());

            //goto tools
            menu.AddSep(category);
            menu.Add(category + "Goto Parent _[", () => go.transform.PingParent());
            menu.Add(category + "Goto Child _]", () => go.transform.PingChild());
            menu.Add(category + "Goto Sibling _\\", () => go.transform.PingSibling());

            //transform tools
            Context_Transform(menu, go, category);
        }
        internal static void Context_Transform(GenericMenu menu, GameObject go, string category = "Transform/")
        {
            var lcPos = go.transform.localPosition != Vector3.zero;
            var lcScl = go.transform.localScale != Vector3.one;
            var lcRot = go.transform.localRotation != Quaternion.identity;

            var cnt = (lcPos ? 1 : 0) + (lcScl ? 1 : 0) + (lcRot ? 1 : 0);
            if (cnt > 0) menu.AddSep(category);

            menu.AddIf(lcPos, category + "Reset Position #P", () => Hierarchy2Api.ResetLocalPosition(go));
            menu.AddIf(lcRot, category + "Reset Rotation #R", () => Hierarchy2Api.ResetLocalRotation(go));
            menu.AddIf(lcScl, category + "Reset Scale #S", () => Hierarchy2Api.ResetLocalScale(go));

            if (cnt > 0) menu.AddSep(category);
            menu.AddIf(cnt > 0, category + "Reset Transform #T", () => Hierarchy2Api.ResetTransform(go));
        }
        internal static void Context_Special(GenericMenu menu, GameObject go, string category = "Edit/")
        {
            // Prefab specific
            //var isPrefab = PrefabUtility.GetPrefabObject(go) != null;

            //Debug.Log("--->"+PrefabUtility.GetPrefabObject(go) +":"+ isPrefab);

            var t = PrefabUtility.GetPrefabType(go);

            if (t != PrefabType.None)
            {
                menu.AddSep(category);
                menu.Add(category + "Select Prefab", (t == PrefabType.MissingPrefabInstance) ? (Action)null : go.SelectPrefab);
                menu.Add(category + "Break Prefab #B", () => go.BreakPrefab());
            }

            // Camera specific
            var cam = go.GetComponent<Camera>();
            if (cam != null)
            {
                menu.AddSep(category);
                menu.Add(category + ((Hierarchy2Api.ltCamera != null) ? "Stop " : "") + "Look through #L", () => Hierarchy2Api.ToggleLookThroughCamera(cam));
                menu.Add(category + "Capture SceneView #C", () => Hierarchy2Api.CameraCaptureSceneView(cam));
            }
        }

        internal static void Context_Components(GenericMenu menu, GameObject go)
        {
            var listTemp = go.GetComponents<Component>().ToList();
            var scripts = new List<MonoBehaviour>();
            var compList = new List<Component>();
            var missing = 0;

            foreach (var c in listTemp)
            {
                if (c is Transform) continue;

                if (c == null)
                {
                    missing++;
                    continue;
                }

                if (c is MonoBehaviour)
                {
                    scripts.Add((MonoBehaviour)c);
                    continue;
                }

                compList.Add(c);
            }

            var total = scripts.Count + compList.Count + missing;
            var prefix = "Components [" + (total) + "]/";

            if (scripts.Count > 0)
            {
                foreach (var script in scripts)
                {
                    var behaviour = script;
                    var title = prefix + behaviour.GetTitle() + "/";
                    menu.Add(title + "Reveal", script.Ping);
                    menu.Add(title + "Edit", script.OpenScript);
                    menu.AddSep(title);
                    menu.Add(title + "Isolate", () => Hierarchy2Api.Isolate_Component(behaviour));
                }
            }

            if (compList.Count > 0)
            {
                if (scripts.Count > 0) menu.AddSep(prefix);

                foreach (var c in compList)
                {
                    var comp = c;
                    menu.Add(prefix + comp.GetTitle(), () => Hierarchy2Api.Isolate_Component(comp));
                }
            }

            if (missing > 0)
            {
                if (compList.Count + scripts.Count > 0)
                {
                    menu.AddSep(prefix);
                    menu.Add(prefix + "+" + missing + " Missing Behaviour" + (missing > 1 ? "s" : ""), null);
                }
                else
                {
                    menu.Add("+" + missing + " Missing Behaviour" + (missing > 1 ? "s" : ""), null);
                }
            }
        }
        internal static void Context_Create(GenericMenu menu, GameObject go, string category = "Create/")
        {
            menu.Add("New Empty Child #N", () => Hierarchy2Api.CreateEmptyChild(go));
            menu.Add("New Empty Sibling", () => Hierarchy2Api.CreateEmptySibling(go));
            menu.Add(category + "Parent", () => Hierarchy2Api.CreateParentAtMyPosition(go));
            menu.Add(category + "Parent at Origin", () => Hierarchy2Api.CreateParentAtOrigin(go));

            menu.AddSep(category);

            var list = new[] { "Quad", "Plane", "Cube", "Cylinder", "Capsule", "Sphere" };
            var key = new[] { " #1", " #2", " #3", " #4", " #5", " #6" };
            var types = new[] {
			PrimitiveType.Quad,
			PrimitiveType.Cube,
			PrimitiveType.Sphere,
			PrimitiveType.Plane,
			PrimitiveType.Cylinder,
			PrimitiveType.Capsule
		};

            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                var name = list[i];

                menu.Add(category + name + key[i], () =>
                {
                    Selection.activeGameObject = go;
                    vlbUnityEditor.NewPrimity(
                        type,
                        "New".GetNewName(go.transform, name),
                        "New" + name, go.transform
                    );
                });
            }
        }
        internal static void Context_Isolate(GenericMenu menu, GameObject go, string category = "Isolate/")
        {
            menu.Add(category + "Missing Behaviours &M", () => Hierarchy2Api.Isolate_MissingBehaviours());
            menu.Add(category + "Has Behaviour &B", () => Hierarchy2Api.Isolate_ObjectsHasScript());
            if (Selection.instanceIDs.Length > 1) menu.Add(category + "Selected Objects &S", () => Hierarchy2Api.Isolate_SelectedObjects());
            menu.AddSep(category);
            menu.Add(category + "Locked Objects &L", () => Hierarchy2Api.Isolate_LockedObjects());
            menu.Add(category + "InActive Objects &I", () => Hierarchy2Api.Isolate_InActiveObjects());
            menu.Add(category + "Combined Objects &Y", () => Hierarchy2Api.Isolate_CombinedObjects());
            menu.AddSep(category);

            var type = "UnityEditorInternal.InternalEditorUtility".GetTypeByName("UnityEditor");
            var layers = (string[])(type.GetProperty("layers", BindingFlags.Static | BindingFlags.Public).GetValue(null, null));
            var tags = (string[])(type.GetProperty("tags", BindingFlags.Static | BindingFlags.Public).GetValue(null, null));

            for (var i = 0; i < layers.Length; i++)
            {
                var idx = i;
                menu.Add(category + "Layer/" + layers[idx], () => Hierarchy2Api.Isolate_Layer(layers[idx]));
            }

            for (var i = 0; i < tags.Length; i++)
            {
                var idx = i;
                menu.Add(category + "Tag/" + tags[idx], () => Hierarchy2Api.Isolate_Tag(tags[idx]));
            }
        }

        //-------------------------------- SHORTCUTS ---------------------------------------

        static void Key_Handler(Event evt, Transform t)
        {
            var go = t.gameObject;

            switch (evt.keyCode)
            { //TO PARENT AND BACK
                case KeyCode.Comma:
                case KeyCode.LeftBracket: t.PingParent(true); break;
                case KeyCode.Period:
                case KeyCode.RightBracket: t.PingChild(true); break;
                case KeyCode.Backslash: t.PingSibling(true); break;

                case KeyCode.L:
                    go.SetSmartLock(false, false);
                    Event.current.Use();

                    var oFocus = EditorWindow.focusedWindow;
                    vlbEditorWindow.Inspector.Repaint();
                    if (EditorWindow.focusedWindow != oFocus && oFocus != null) oFocus.Focus();
                    break;

                case KeyCode.A:
                case KeyCode.V:
                    Event.current.Use();
                    go.ToggleActive(true);
                    break;

                case KeyCode.C:
                    Event.current.Use();
                    go.ToggleCombine();
                    Selection.activeGameObject = t.gameObject;
                    //vlbEditorWindow.Hierarchy.Focus();
                    break;
            }
        }
        static void ShiftKey_Handler(Event evt, GameObject go)
        {
            var dict = new Dictionary<KeyCode, PrimitiveType> {
			    {KeyCode.Alpha1,		PrimitiveType.Quad},
			    {KeyCode.Alpha2,		PrimitiveType.Plane},
			    {KeyCode.Alpha3,		PrimitiveType.Cube},
			    {KeyCode.Alpha4,		PrimitiveType.Cylinder},
			    {KeyCode.Alpha5,		PrimitiveType.Capsule},
			    {KeyCode.Alpha6,		PrimitiveType.Sphere}
		    };

            if (dict.ContainsKey(evt.keyCode))
            {
                go.RevealChildrenInHierarchy();

                var type = dict[evt.keyCode];
                type.NewPrimity(
                    "New".GetNewName(go.transform.parent, type.ToString()),
                    "New" + type + "Child",
                    go.transform.parent
                ).transform.PingAndUseEvent();
                return;
            }

            switch (evt.keyCode)
            {
                case KeyCode.N: Hierarchy2Api.CreateEmptyChild(go, true); break;

                case KeyCode.L:
		            if (go.GetComponent<Camera>() != null) Hierarchy2Api.CameraLookThrough(go.GetComponent<Camera>()); break;
                case KeyCode.C: 
					if (go.GetComponent<Camera>() != null) Hierarchy2Api.CameraCaptureSceneView(go.GetComponent<Camera>()); break;
                case KeyCode.P: Hierarchy2Api.ResetLocalPosition(go); break;
                case KeyCode.R: Hierarchy2Api.ResetLocalRotation(go); break;
                case KeyCode.S: Hierarchy2Api.ResetLocalScale(go); break;
                case KeyCode.T: Hierarchy2Api.ResetTransform(go); break;
                case KeyCode.B: go.BreakPrefab(); break;
            }

            //if (evt.type == EventType.used) {
            //Selection.activeGameObject = null;
            //Hierarchy2Utils.RefreshInspector();
            //Selection.activeGameObject = go;
            //}
        }
        static void AltKey_Handler(Event evt, GameObject go)
        {//commands

            switch (evt.keyCode){
                case KeyCode.M: Hierarchy2Api.Isolate_MissingBehaviours(true); break;
                case KeyCode.B: Hierarchy2Api.Isolate_ObjectsHasScript(true); break;
                case KeyCode.S: Hierarchy2Api.Isolate_SelectedObjects(true); break;

                case KeyCode.L: Hierarchy2Api.Isolate_LockedObjects(true); break;
                case KeyCode.I:
                case KeyCode.V: Hierarchy2Api.Isolate_InActiveObjects(true); break;
                case KeyCode.Y: Hierarchy2Api.Isolate_CombinedObjects(true); break;
            }
        }

        //-------------------------------- FUNCTIONS ---------------------------------------

        internal static void EditLock(Rect r, GameObject go) {
            const HideFlags flag = HideFlags.NotEditable;
            var isSet = go.GetFlag(flag);

            GUI.DrawTexture(r, vlbEditorSkin.icoLock(isSet));
            var evt = Event.current;

            if (r.HasLeftMouseDown()) {

                if (vlbEvent.NoModifier(autoUseEvent: true))
                {
                    go.SetSmartLock(false, false);
                }
                else if (vlbEvent.Alt(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) {
                        go.SetSmartLock(true, false);
                    } else {
                        go.ToggleSiblingLock();
                    }

                }
                else if (vlbEvent.Ctrl(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) {
                        go.ToggleLock();
                    } else {
                        go.SetSmartLock(false, true);
                    }
                }

                if (evt.type == EventType.used) vlbEditorWindow.Inspector.Repaint();
            }

            if (r.HasRightMouseDown() && vlbEvent.NoModifier(true)) {//right-Click
                var menu = new GenericMenu();

                menu.Add("Toggle Lock", go.InvertLock);
                //menu.Add("Toggle Lock Children",	go.InvertLock);
                menu.AddSep("");
                menu.Add(Hierarchy2Api.RootGOList.HasFlag(flag),
                        "Recursive Unlock",
                        "Recursive Lock",
                        has => Hierarchy2Api.RecursiveLock(!has));
                menu.ShowAsContext();
            }
        }
        internal static void EditActive(Rect r, GameObject go)
	    {
		    if (go == null) return;
            var isSet = go.activeSelf;
            GUI.DrawTexture(r, vlbEditorSkin.icoEye(isSet));

            if (r.HasLeftMouseDown())
            {
                if (vlbEvent.NoModifier(autoUseEvent: true))
                {
                    go.ToggleActive(false);
                }
                else if (vlbEvent.Alt(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) {
                        go.ToggleActive(true);
                    } else {
                        go.SetActiveSibling(isSet, false);
                    }
                }
                else if (vlbEvent.Ctrl(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1)
                    {
                        //go.ToggleActive(false);
                        //Toggle active me only
                    } else {
                        go.SetActiveChildren(!isSet, false);
                    }
                }

            }


            if (r.HasRightMouseDown())
            {
                Event.current.Use();
                var menu = new GenericMenu();
                if (go.HasChild())
                {
                    menu.Add(go.HasActiveChild(), "Hide children", "Show children", has => go.SetActiveChildren(!has, false));
                    menu.AddIf(go.HasGrandChild(), go.HasActiveChild(), "Hide all children", "Show all children", has => go.SetActiveChildren(!has, false));
                }
                menu.AddIf(go.HasSibling(Hierarchy2Api.RootGOList), go.HasActiveSibling(Hierarchy2Api.RootGOList), "Hide siblings", "Show siblings", has => go.ForeachSibling(Hierarchy2Api.RootGOList, item => item.SetActive(!has)));
                if (menu.GetItemCount() > 0) menu.AddSep(null);
                if (go.transform.parent != null)
                {
                    menu.Add(go.HasActiveParent(), "Hide parents", "Show parents", (has) => go.SetActiveParents(!has));
                }
                menu.Add(Hierarchy2Api.RootGOList.HasActive(), "Recursive Hide", "Recursive Show", (has) => Hierarchy2Api.RootGOList.SetActive(!has, true));
                menu.ShowAsContext();
            }
        }
        internal static void EditCombine(Rect r, GameObject go) {
		    if (go == null) return;
            const HideFlags flag = HideFlags.HideInHierarchy;
            var count = go.transform.childCount;
            if (count == 0) return; //don't display childCount if GO does not contains child

            //calculate size needed for display childCount text
            var isSet = go.HasFlagChild(flag);
            var w = (count < 10 ? 14 : count < 100 ? 18 : count < 1000 ? 28 : 33) + (isSet ? 6 : 0);
            r.x += r.width - w;
            r.width = w;

            var countStr = count < 1000 ? (string.Empty + count) : "999+";

            if (isSet) {
                GUI.Label(r, countStr, EditorStyles.miniButtonMid);
            } else {
                GUI.Label(r, countStr);
            }

            if (r.HasLeftMouseDown())
            {
                if (vlbEvent.NoModifier(true)) {
                    go.ToggleCombine();
                }

                if (vlbEvent.Alt(true)) {
                    go.SetCombineSibling(!isSet);
                }

                if (vlbEvent.Ctrl(true)) {
                    go.ToggleCombineChildren();
                }
            }

            if (r.HasRightMouseDown() && vlbEvent.NoModifier(true))
            {
                var menu = new GenericMenu();
                menu.Add(Hierarchy2Api.RootGOList.HasFlagChild(flag),
                    "Recursive Expand",
                    "Recursive Combine",
                    has => Hierarchy2Api.RecursiveCombine(!has)
                );
                menu.ShowAsContext();
            }
        }
        internal static void EditLevelStrip(Rect r, GameObject go)
        {
            var c = go.ParentCount() - (HLFull ? 1 : 0);

            if (c < 0) return; //don't highlight level 0 on full mode

            if (go.numScriptMissing() > 0 || go.numScript() > 0)
            {
                var color = go.numScriptMissing() > 0 ? Color.red : Color.green;
                GUI.DrawTexture(r.dx(-26f).w(36f), color.Adjust(-0.4f).GetTexture2D());
            }

            if (HLFull)
            {
                r.width = r.x + r.width;
                r.x = 0;
            }
            else
            {
                var w = 18 * 4 + c * 5f;
                r.x = r.x + r.width - w;
                r.width = 5f;
            }

            GUI.DrawTexture(r, HLColors[c%HLColors.Length].Alpha(0.5f).Adjust(0.3f).GetTexture2D());
        }
        internal static void EditStatic(Rect r, GameObject go) {
            GUI.DrawTexture(r, vlbEditorSkin.icoStatic(go.isStatic));
            var evt = Event.current;

            if (r.HasLeftMouseDown())
            {

                if (vlbEvent.NoModifier(autoUseEvent: true))
                {
                    go.SetStatic(false, false);
                }
                else if (vlbEvent.Alt(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1)
                    {
                        go.SetStatic(true, false);
                    }
                    else
                    {
                        go.ToggleSiblingStatic();
                    }

                }
                else if (vlbEvent.Ctrl(autoUseEvent: true))
                {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1)
                    {
                        go.ToggleStatic();
                    }
                    else
                    {
                        go.SetStatic(false, false);
                    }
                }

                if (evt.type == EventType.used) vlbEditorWindow.Inspector.Repaint();
            }

            if (r.HasRightMouseDown() && vlbEvent.NoModifier(true))
            {//right-Click
                var menu = new GenericMenu();

                menu.Add("Toggle Static", go.InvertStatic);
                //menu.Add("Toggle Lock Children",	go.InvertLock);
                menu.AddSep("");
                menu.Add(Hierarchy2Api.RootGOList.hasStatic(),
                        "Recursive Static",
                        "Recursive UnStatic",
                        has => Hierarchy2Api.RecursiveStatic(!has));
                menu.ShowAsContext();
            }
        }

        //-------------------------------- GUI ---------------------------------------

        private static Camera LookThroughCam;
        private static GameObject RenameUnlock;

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            //var ofocus = EditorWindow.focusedWindow;

            var evt = Event.current;
            var r = selectionRect.dx(selectionRect.width).w(16f).h(16f);
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            var hasFocus = Hierarchy2Utils.HasFocusOnHierarchy;

            //Debug.Log(hasFocus + ":" + evt);

            if (vlbGUI.renameGO != null && go.GetInstanceID() == vlbGUI.renameGO.GetInstanceID())
            {
                go.Rename();
            }

            if (HLEnabled) EditLevelStrip(r.dx(-8f - IconsOffset), go);

			Hierarchy2Api.CheckRoot(go);//fixed for deactivated root GO can not be found by FindObjectOfType

            if (AllowRenameLockedGO && go == Selection.activeGameObject && hasFocus)
            {
                if (go.GetFlag(HideFlags.NotEditable) && Hierarchy2Utils.IsRenaming())
                {
                    RenameUnlock = go;
                    go.SetFlag(HideFlags.NotEditable, false);
                }
                else if (RenameUnlock != null && !Hierarchy2Utils.IsRenaming())
                {
                    RenameUnlock.SetFlag(HideFlags.NotEditable, true);
                    RenameUnlock = null;
                }
            }

            var offset = -16f;

            if (ShowLockIcon) {
                EditLock(r.dx(offset - IconsOffset), go);
                offset -= 16f;
            }

            if (ShowEyeIcon) {
                EditActive(r.dx(offset - IconsOffset), go); EditActive(r.dx(offset - IconsOffset), go);
                offset -= 16f;
            }
            if (ShowStaticIcon) {
                EditStatic(r.dx(offset - IconsOffset), go);
                offset -= 16f;
            }
            if (ShowChildCount) {
                EditCombine(r.dx(offset - IconsOffset), go);
            }

            if (hasFocus && Selection.activeGameObject == go && evt.type == EventType.keyUp && !Hierarchy2Utils.IsRenaming()) //evt.shift && 
            {
				var t = Selection.activeTransform;
	            if (t != null){
					if (AllowAltShortcuts && vlbEvent.Alt(true)) {
						AltKey_Handler(evt, go);
					}
					else if (AllowShiftShortcuts && vlbEvent.Shift(true)) {
						ShiftKey_Handler(evt, go);
					}
					else if (AllowAltShortcuts && vlbEvent.NoModifier(true)) {
						Key_Handler(evt, t);
					}
	            }
	            


                /*if (Event.current.control)
                {
                    //ignore Ctrl 
                }
                else if (Event.current.alt)
                {
                    if (AllowAltShortcuts && !Event.current.shift)
                    {
                        AltKey_Handler(evt, go);
                    }
                }
                else if (Event.current.shift)
                {
                    if (AllowShiftShortcuts) ShiftKey_Handler(evt, t.gameObject);
                }
                else if (AllowOtherShortcuts)
                {
                    Key_Handler(evt, t);
                }*/
            }

            if (!AllowDragLockedGO && go.GetFlag(HideFlags.NotEditable) && selectionRect.Contains(Event.current.mousePosition))
            {
                if (selectionRect.HasLeftMouseDown() && vlbEvent.NoModifier(true)) {
                    if (!Selection.instanceIDs.Contains(go.GetInstanceID())) {
                        Selection.activeGameObject = go;
                    }
                }
                if (evt.type == EventType.mouseDrag) Event.current.Use();
            }

            var rect = selectionRect.dl(-selectionRect.x).dr(-55f);
            //GUI.DrawTexture(rect, Color.red.Adjust(0.2f).Alpha(0.1f).GetTexture2D());

            if (rect.HasRightMouseDown() && vlbEvent.NoModifier(true)) {
                //DefaultContext(new GenericMenu(), go).ShowAsContext(); 
                var menu = new GenericMenu();
                Context_BuiltIn(menu, go);
                Context_Special(menu, go, "");
                menu.AddSep("");

                Context_Basic(menu, go);
                Context_Isolate(menu, go);
                Context_Components(menu, go);
                menu.AddSep("");
                Context_Create(menu, go);

                menu.ShowAsContext();
            }

            //if (ofocus != EditorWindow.focusedWindow) {
                //Debug.LogWarning("Focus changed :: " + ofocus + ":::::>>>" + EditorWindow.focusedWindow);
            //}
        }
    }

    internal static class Hierarchy2Utils
    {
        static BindingFlags _flags;
        static internal SceneView current
        {
            get
            {
                if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.GetType() == typeof(SceneView))
                {
                    return (SceneView)EditorWindow.focusedWindow;
                }
                _flags = BindingFlags.Instance | BindingFlags.NonPublic;
                return SceneView.lastActiveSceneView ?? (SceneView)SceneView.sceneViews[0];
            }
        }
        static internal Camera SceneCamera { get { return current.camera; } }
        static public void Refresh()
        {//hacky way to force SceneView increase drawing frame
            Transform t = Selection.activeTransform;
            if (t == null)
            {
                t = (Camera.main != null) ? Camera.main.transform : new GameObject("$t3mp$").transform;
            }

            Vector3 op = t.position;
            t.position += new Vector3(1, 1, 1); //make some dirty
            t.position = op;

            if (t.name == "$t3mp$") Object.DestroyImmediate(t.gameObject, true);
        }

        static T GetAnimT<T>(string name)
        {
            if (current == null) return default(T);
            object animT = typeof(SceneView).GetField(name, _flags).GetValue(current);
            return (T)animT.GetType().GetField("m_Value", _flags).GetValue(animT);
        }
        static void SetAnimT<T>(string name, T value)
        {
            if (current == null) return;

            var animT = current.GetField(name);
#if UNITY_4_5 || UNITY_4_6
            animT.SetProperty("target", value);
#else
            animT.Invoke("BeginAnimating", null, null, value, animT.GetField("m_Value"));
#endif
            //object animT = typeof(SceneView).GetField(name, _flags).GetValue(current);
            //var info = animT.GetType().GetField("m_Value", _flags);
            //animT.GetType().GetMethod("BeginAnimating", _flags).Invoke(animT, new object[] { value, (T)info.GetValue(animT) });
        }

        static public Vector3 m_Position
        {
            get { return GetAnimT<Vector3>("m_Position"); }
            set { SetAnimT("m_Position", value.FixNaN()); }
        }
        static public Quaternion m_Rotation
        {
            get { return GetAnimT<Quaternion>("m_Rotation"); }
            set { SetAnimT("m_Rotation", value); }
        }

        static public float cameraDistance
        {
            get { return (float)current.GetProperty("cameraDistance"); }
        }
        static public bool orthographic
        {
            get { return current.camera.orthographic; }
            set
            {
                //current.camera.orthographic = value;
#if UNITY_4_5 || UNITY_4_6
                SetAnimT("m_Ortho", value);
#else
                SetAnimT("m_Ortho", value ? 1f : 0f);
#endif
            }
        }
        static public float m_Size
        {
            get { return GetAnimT<float>("m_Size"); }
            set { SetAnimT("m_Size", (Single.IsInfinity(value) || (Single.IsNaN(value)) || value == 0) ? 100f : value); }
        }


        internal static bool HasFocusOnHierarchy {
            get
            {
                var fw = EditorWindow.focusedWindow;
                if (fw == null) return false;
                
                //Debug.Log(fw.GetType().ToString());

                return fw.GetType().ToString() ==
#if UNITY_4_5 || UNITY_4_6
                "UnityEditor.SceneHierarchyWindow";
#else
                "UnityEditor.HierarchyWindow";
#endif
            }
        }
        internal static bool hasStatic(this List<GameObject> list) {
            bool hasStatic = false;
            foreach (var go in list) {
                hasStatic = go.isStatic;
                if (hasStatic) break;
            }
            return hasStatic;
        }

        //-------------------------------- FLAG ----------------------------

        internal static void SetDeepFlag(this GameObject go, HideFlags flag, bool value, bool includeMe = true, bool recursive = true)
        {
            if (includeMe) go.SetFlag(flag, value);
            foreach (Transform t in go.transform)
            {
                if (recursive)
                {
                    SetDeepFlag(t.gameObject, flag, value);
                }
                else
                {
                    t.gameObject.SetFlag(flag, value);
                }
            }
        }
        internal static bool HasFlagChild(this GameObject go, HideFlags flag)
        {
            return go.GetChildren().Any(item => item.GetFlag(flag));
        }
        internal static bool HasFlagChild(this List<GameObject> list, HideFlags flag)
        {
            //var has = false;

            /*foreach (var child in list)
            {
                child.ForeachChild2(child2 =>
                {
                    has = child2.GetFlag(flag);
                    return !has;
                });
                if (has) break;
            }*/

            return list.Any(item => item.HasFlagChild(flag));
        }
        internal static bool HasFlag(this List<GameObject> list, HideFlags flag)
        {
            var hasActive = false;
            foreach (var go in list)
            {
                hasActive = go.GetFlag(flag);
                if (hasActive) break;
            }
            return hasActive;
        }
        internal static void SetDeepFlag(this List<GameObject> list, bool value, HideFlags flag, bool includeMe)
        {
            foreach (var go in list)
            {
                go.SetDeepFlag(flag, value, includeMe);
            }
        }

        //-------------------------------- ACTIVE ----------------------------

        internal static bool HasActiveChild(this GameObject go)
        {
            var has = false;
            go.ForeachChild2(child =>
            {
                has = child.activeSelf;
                return !has;
            });
            return has;
        }
        internal static bool HasGrandChild(this GameObject go)
        {
            var has = false;
            go.ForeachChild2(child =>
            {
                has = child.transform.childCount > 0;
                return !has;
            });
            return has;
        }

        internal static bool HasSibling(this GameObject go, List<GameObject> rootGOList)
        {
            var p = go.transform.parent;
            return p != null ? (p.childCount > 1) : (rootGOList.Count > 1);
        }
        internal static bool HasActiveSibling(this GameObject go, List<GameObject> rootGOList)
        {
            var has = false;
            go.ForeachSibling2(rootGOList, sibl =>
            {
                has = sibl.activeSelf;
                return !has;
            });
            return has;
        }
        internal static bool HasActiveParent(this GameObject go)
        {
            var has = false;
            go.ForeachParent2(p =>
            {
                has = p.activeSelf;
                return !has;
            });
            return has;
        }

        internal static bool HasActive(this List<GameObject> list)
        {
            bool hasActive = false;
            foreach (var go in list)
            {
                hasActive = go.activeSelf;
                if (hasActive) break;
            }
            return hasActive;
        }
        internal static void SetActive(this List<GameObject> list, bool value, bool deep)
        {
            foreach (var go in list)
            {
                if (deep) go.SetActiveChildren(value, false);
                if (go.activeSelf != value) go.SetActive(value);
            }
        }

        //------------------------------- FOREACH --------------------------
        internal static void ForeachSelected(this GameObject go, Action<GameObject, int> func)
        {
            var selected = Selection.objects;
            if (selected.Length == 0 || !selected.Contains(go) || (selected.Length == 1 && selected.Contains(go)))
            {
                func(go, -1);
                return;
            }

            var cnt = 0;
            foreach (var item in selected)
            {
                if (item is GameObject) func((GameObject)item, cnt++);
            }
        }

        //------------------------------- MISC --------------------------

        internal static void SetSearchFilter(this EditorWindow window, string term)
        {
            var sWindow = "UnityEditor.SearchableEditorWindow".GetTypeByName("UnityEditor");
            window.Invoke("SetSearchFilter", sWindow, null, new object[] { term, SearchableEditorWindow.SearchMode.All, true });
            window.SetField("m_HasSearchFilterFocus", true, sWindow);

            EditorGUI.FocusTextInControl("SearchFilter");
            window.Repaint();
        }

        static List<object> GetChildrenTreeItem(this object treeItem, Type itemType, bool deep) {
            var tempList = treeItem.GetField("m_Children", itemType);
            var result = new List<object>();

            //Debug.Log(treeItem + ":" + tempList);

            if (tempList is bool) return result;
            if (tempList == null) return result;

            var tempList2 = (IList) tempList;
            for (var i = 0; i < tempList2.Count; i++) {
                var item = tempList2[i];
                result.Add(item);

                if (deep)
                {
                    //Debug.Log("deep start <" + (item==null) + ">");
                    result.AddRange(item.GetChildrenTreeItem(itemType, true));
                    //Debug.Log("deep end");
                }

                item.SetField("m_Depth", 0, itemType);
                item.SetField("m_Children", new List<object>().ToArrayT(itemType), itemType);
            }
            return result;
        }

        public static Array ToArrayT(this IList content, Type itemType) {
            var result = Array.CreateInstance(itemType, content.Count);
            for (var i = 0; i < content.Count; i++) {
                result.SetValue(content[i], i);
            }
            return result;
        }

        public static IList NewListT(Type elmType) {
            var listType = typeof(List<>);
            var combinedType = listType.MakeGenericType(elmType);
            return (IList)Activator.CreateInstance(combinedType);
        }

        public static IList ToListT(this IList list, Type elmType = null) {
            if (elmType == null) elmType = list[0].GetType();
            var result = NewListT(elmType);
            for (var i = 0; i < list.Count; i++) {
                result.Add(list[i]);
            }
            return result;
        }

        internal static void SetSearchFilter(this EditorWindow window, int[] instanceIDs, string title)
        {
            if (window == null) {
                vlbEditorWindow.ClearDefinitionCache();
                window = vlbEditorWindow.Hierarchy;
            }

            if (instanceIDs.Length == 0) {
                window.Invoke("SetSearchFilter", null, null, new object[] { "Hierarchy2tempfixemptysearch", SearchableEditorWindow.SearchMode.All, false });
                window.SetSearchFilter("iso:" + title);
                return;
            }

#if UNITY_4_5 || UNITY_4_6
            //var treeViewSrcT    = "UnityEditor.TreeViewDataSource".GetTypeByName("UnityEditor");
            var treeViewItemT   = "UnityEditor.TreeViewItem".GetTypeByName("UnityEditor");
            var treeView        = vlbEditorWindow.Hierarchy.GetField("m_TreeView");
            var treeViewData    = treeView.GetProperty("data");
			var rootItem		= treeViewData.GetField("m_RootItem");
			var children        = rootItem.GetChildrenTreeItem(treeViewItemT, true);
			var expandIds		= treeViewData.Invoke("GetExpandedIDs"); //save the expand state to restore

	        for (var i = 0; i < children.Count; i++) { // expand all children
		        if (children[i] != null) treeViewData.Invoke("SetExpandedWithChildren", null, null, children[i], true);
	        }

			Debug.Log("ids :: " + instanceIDs.Length); 

			var children1 = (IList)treeViewData.Invoke("GetVisibleRows");
			var childrenList = NewListT(treeViewItemT);
	        for (var i = 0; i < children1.Count; i++) {
		        var child = children1[i];

		        if (instanceIDs.Contains((int) child.GetField("m_ID", treeViewItemT)))
		        {
					child.SetField("m_Depth", 0, treeViewItemT);
					childrenList.Add(child);
		        }
			        
	        }

			// restore the expand state for children
	        treeViewData.Invoke("SetExpandedIDs", null, null, expandIds);

            window.Invoke("SetSearchFilter", null, null, new object[] { "iso:" + title, SearchableEditorWindow.SearchMode.All, false });
            treeViewData.SetField("m_VisibleRows", childrenList.ToListT(treeViewItemT));
            treeView.SetField("m_AllowRenameOnMouseUp", false);
            treeView.Invoke("Repaint");
#else
			var TBaseProjectWindow = "UnityEditor.BaseProjectWindow".GetTypeByName("UnityEditor");
			var TFilteredHierarchy = "UnityEditor.FilteredHierarchy".GetTypeByName("UnityEditor");

			window.SetSearchFilter("iso:" + title);

			var instIDsParams = new object[] { instanceIDs };
			var fh = window.GetField("m_FilteredHierarchy", TBaseProjectWindow);
			//var sf = (SearchFilter)fh.GetField("m_SearchFilter", TFilteredHierarchy);

			//sf.ClearSearch();
			//sf.referencingInstanceIDs = instanceIDs;
			fh.Invoke("SetResults", TFilteredHierarchy, null, instIDsParams);

			var arr = (object[])fh.GetProperty("results", TFilteredHierarchy, null);//(FilteredHierarchyType.GetProperty("results").GetValue(fh, null));
			var list = new List<int>();

			//patch
			var nMissing = 0;
			foreach (var t in arr) {
				if (t == null) {
					nMissing++;
					continue;
				}
				var id = (int)t.GetField("instanceID");
				if (!list.Contains(id)) list.Add(id);
			}

			if (nMissing > 0) Debug.LogWarning("Filtered result may not be correct, missing " + nMissing + " results, please help report it to unity3d@vietlab.net");
			instanceIDs = list.ToArray();

			//reapply
			//sf.ClearSearch();
			//sf.referencingInstanceIDs = instanceIDs;
			fh.Invoke("SetResults", TFilteredHierarchy, null, new object[] { instanceIDs });
			window.Repaint();
#endif

        }

        internal static void PingAndUseEvent(this Transform obj, bool ping = true, bool useEvent = true)
        {
            if (obj == null) return;
            var go = obj.gameObject;

            if (useEvent) Event.current.Use();
            if (!ping) return;

            if (go != null && !go.GetFlag(HideFlags.HideInHierarchy))
            {
                Selection.activeObject = go;
                EditorGUIUtility.PingObject(go);
            }
            else
            {
                //Debug.Log("Can not ping a null or hidden target ---> " + go + ":" + go.hideFlags);
            }
        }
        internal static Transform NextSibling(this Transform t, List<GameObject> rootList)
        {
            if (t == null)
            {
                Debug.LogWarning("Transform should not be null ");
                return null;
            }

            var p = t.parent;
            if (t.parent != null)
            {
                var cnt = 0;
                while (p.GetChild(cnt) != t) cnt++;
                return (cnt < p.childCount - 1) ? p.GetChild(cnt + 1) : p.GetChild(0);
            }

            var idx = rootList.IndexOf(t.gameObject);
            if (idx != -1) return rootList[(idx < rootList.Count - 1) ? idx + 1 : 0].transform;
            Debug.LogWarning("Root Object not in RootList " + t + ":" + rootList);
            return t;
        }

        internal static bool IsExpanded(this GameObject go)
        {
            var mExpand = (int[])vlbEditorWindow.Hierarchy.GetField("m_ExpandedArray", "UnityEditor.BaseProjectWindow".GetTypeByName("UnityEditor"));
            return mExpand.Contains(go.GetInstanceID());
        }
        internal static void OpenScript(this Object obj)
        {
            AssetDatabase.OpenAsset(MonoScript.FromMonoBehaviour((MonoBehaviour)obj).GetInstanceID());
        }

        internal static int numScript(this GameObject go)
        {
	        if (go == null) return 0; //destroyed

            var list = go.GetComponents<MonoBehaviour>();
            if (list.Length == 0) return 0;

            var paths = Hierarchy2.IgnoreScriptPaths;
            var cnt = 0;
            for (int i = 0; i < list.Length; i++)
            {
                var mono = MonoScript.FromMonoBehaviour(list[i]);
                var monoPath = AssetDatabase.GetAssetPath(mono);

                for (var j = 0; j < paths.Length; j++)
                {
                    if (monoPath.Contains(paths[j]))
                    {
                        list[i] = null;
                        //Debug.Log("Ignoring ... " + monoPath);
                        break;
                    }
                }

                if (list[i] != null) cnt++;
            }

            return cnt;
        }
        internal static int numScriptMissing(this GameObject go)
        {
            if (go == null) return 0;
            var list = go.GetComponents<MonoBehaviour>();
            var cnt = 0;
            if (list.Any(item => item == null)) cnt++;
            return cnt;
        }
        internal static void AppendChildren(Transform t, ref List<GameObject> list, bool deep = false)
        {
            list.Add(t.gameObject);
            foreach (Transform child in t)
            {
                if (child != t)
                {
                    list.Add(child.gameObject);
                    if (deep && child.childCount > 0) AppendChildren(child, ref list, true);
                }
            }
        }

        internal static int[] GetFilterInstanceIDs(this List<GameObject> rootList, Func<GameObject, bool> func)
        {
            var list = new List<GameObject>();
            foreach (var child in rootList)
            {
                AppendChildren(child.transform, ref list, true);
            }
			
	        var result = new List<int>();
	        for (var i = 0; i < list.Count; i++)
	        {
		        var c = list[i];
		        var isValid = func(c);
		        if (isValid) result.Add(c.GetInstanceID());

		        //Debug.Log(i + ":" + list[i] + ":::" + isValid);
	        }

	        return result.ToArray();
	        //return (list.Where(item => func(item)).Select(item => item.GetInstanceID())).ToArray();
        }
        internal static string GetNewName(this string baseName, Transform p, string suffix = "")
        {
            var name = baseName.Contains(suffix) ? baseName : (baseName + suffix);
            if (p == null) return name;
            var namesList = new string[p.childCount];
            for (var i = 0; i < namesList.Length; i++)
            {
                namesList[i] = p.GetChild(i).name;
            }

            if (!namesList.Contains(name)) return name;
            var counter = 1;
            while (namesList.Contains(name + counter)) counter++;
            return name + counter;
        }

        //-------------------------------- UNDO SUPPORTED INSTANTIATE -----------------------------------

        static internal bool IsCombined(this GameObject go)
        {
            return go.HasFlagChild(HideFlags.HideInHierarchy);
        }
        static internal bool IsLock(this GameObject go)
        {
            return go.GetFlag(HideFlags.NotEditable);
        }

        internal static bool IsRenaming() {
            var oFocus = EditorWindow.focusedWindow;

#if UNITY_4_5 || UNITY_4_6
	        var result = false;
			var tvState = vlbEditorWindow.Hierarchy.GetField("m_TreeViewState");
	        if (tvState != null) {
		        var overlay = tvState.GetField("m_RenameOverlay");
		        if (overlay != null) result = (bool)overlay.GetField("m_IsRenaming");
	        }
#else 
            var hWindow = vlbEditorWindow.Hierarchy;
            var type = "UnityEditor.BaseProjectWindow".GetTypeByName("UnityEditor");
            var result = (int)hWindow.GetField("m_RealEditNameMode", type) == 2;
#endif

            if (oFocus != null && oFocus != EditorWindow.focusedWindow) {
                oFocus.Focus();
            }

            return result;
        }
        internal static void Rename(this GameObject go)
        {

            var hWindow = vlbEditorWindow.Hierarchy;

            if (Event.current != null && Event.current.keyCode == KeyCode.Escape)
            {
                vlbGUI.renameGO = null;
                vlbGUI.renameStep = 0;
                hWindow.Repaint();
                return;
            }

            if (vlbGUI.renameGO != go) {
                vlbGUI.renameGO = go;
                vlbGUI.renameStep = 2;
            }

			Debug.Log("Rename : " + go + ":" + vlbGUI.renameStep);

			if (!IsRenaming())
            {
                //not yet in edit name mode, try to do it now
                Selection.activeGameObject = go;

			#if UNITY_4_5 || UNITY_4_6
	            var treeView = vlbEditorWindow.Hierarchy.GetField("m_TreeView");
				var data	 = treeView.GetProperty("data");
				var gui		 = treeView.GetProperty("gui");
				var item	 = data.Invoke("FindItem", null, null, go.GetInstanceID());

				if (item != null && gui != null) {
					gui.Invoke("BeginRename", null, null, item, 0f);
				}
			#else
                var property = new HierarchyProperty(HierarchyType.GameObjects);
                property.Find(go.GetInstanceID(), null);

                hWindow.Invoke("BeginNameEditing", vlbEditorType.BaseProjectWindowT, null, go.GetInstanceID());
				hWindow.SetField("m_NameEditString", go.name, vlbEditorType.BaseProjectWindowT); //name will be missing without this line
                hWindow.Repaint();
			#endif
            }
            else
            {
                if (Event.current == null)
                {
                    vlbGUI.renameStep = 2;
                    //Debug.Log("How can Event.current be null ?");
                    return;
                }

                if (Event.current.type == EventType.repaint && vlbGUI.renameStep > 0)
                {
                    vlbGUI.renameStep--;
                    //hWindow.Repaint();
                }

                if (Event.current.type != EventType.repaint && vlbGUI.renameStep == 0)
                {
                    vlbGUI.renameGO = null;
                }
            }
            //}
        }

    }

    static class Hierarchy2Api
    {

        ///----------------------------------- TRANSFORM ------------------------------------------------
        static private bool _rootDirty;
        static private List<GameObject> _rootList;
        static internal List<GameObject> RootGOList
        {
            get
            {
                if (_rootList != null && !_rootDirty) return _rootList;

                _rootList = new List<GameObject>();
                _rootDirty = false;

                foreach (var o in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    var obj = (GameObject)o;
                    if (obj != null && obj.transform != null && obj.transform.parent == null) RootGOList.Add(obj);
                }

                return _rootList;
            }
        }
        static internal void CheckRoot(GameObject go)
        {
	        if (go != null && go.transform.parent == null && (!RootGOList.Contains(go)))
            {
                _rootList.Add(go);
            }
        }
        static internal void UpdateRoot() { _rootDirty = true; /*Debug.Log("----> refreshed " + vlbGUI.renameGO);*/ }

        ///----------------------------------- LOCK ------------------------------------------------
        static internal void SetLock(this GameObject go, bool value, bool deep = false, string undoKey = "h@-auto")
        {
            if (undoKey == "h@-auto") undoKey = value ? "Lock" : "UnLock";

            go.RecordUndo(undoKey, true);
            go.SetFlag(HideFlags.NotEditable, value);

            foreach (var c in go.GetComponents<Component>())
            {
                if (!(c is Transform))
                {
                    c.SetFlag(HideFlags.NotEditable, value);
                    c.SetFlag(HideFlags.HideInHierarchy, value);
                }
            }

            if (deep)
            {
                go.ForeachChild(child =>
                {
                    child.RecordUndo(undoKey, true);
                    child.SetFlag(HideFlags.NotEditable, value);
                    foreach (var c in child.GetComponents<Component>())
                    {
                        if (!(c is Transform))
                        {
                            c.SetFlag(HideFlags.NotEditable, value);
                            c.SetFlag(HideFlags.HideInHierarchy, value);
                        }
                    }
                }, true);
            }
        }
        static internal void ToggleLock(this GameObject go, string undoKey = "h@-auto") {   
            go.SetLock(!go.GetFlag(HideFlags.NotEditable), false, undoKey);
        }

        /*static internal void SetNaiveLock(this GameObject go, bool value, bool deep, bool invertMe) {
            var isLock = go.GetFlag(HideFlags.NotEditable);
            go.ForeachSelected((item, idx) => SetLock(item,
                (!invertMe || (item == go)) ? !isLock : isLock, deep)
            );
        }*/
        static internal void SetSmartLock(this GameObject go, bool invertMe, bool smartInvert)
        {//smart mode : auto-deepLock
            var isLock = go.GetFlag(HideFlags.NotEditable);
            var key = isLock ? "Lock" : "Unlock";

            go.ForeachSelected((item, idx) => item.SetLock(
                (!invertMe || (item == go)) ? !isLock : isLock,	// invert lock 
                idx == -1 && smartInvert == isLock,				// deep-lock if isLock=true
                key
            ));
        }
        static internal void InvertLock(this GameObject go)
        {
            go.ForeachSelected((item, idx) => item.ToggleLock("Invert Lock"));
        }
        static internal void ToggleSiblingLock(this GameObject go, bool deep = false)
        {
            var isLock = go.GetFlag(HideFlags.NotEditable);
            var key = isLock ? "Lock siblings" : "Unlock siblings";

            go.ToggleLock(key);
            go.ForeachSibling(RootGOList, sibl => sibl.ToggleLock(key));
        }
        static internal void RecursiveLock(bool value)
        {
            var key = value ? "Recursive Lock" : "Recursive Unlock";
            RootGOList.ForEach(
                rootGO => rootGO.SetLock(value, true, key)
            );
        }

        ///----------------------------------- ACTIVE ------------------------------------------------
		static internal bool SmartActiveGo(GameObject go, bool value) {
			if (Hierarchy2.UseDkVisible) {
				var c = go.GetComponent("dfControl");
				if (c != null) {
					c.SetProperty("IsVisible", value);
					return true;
				}
			}
			go.SetActive(value);
			return false;
		}
		static internal bool IsSmartActive(this GameObject go) {
			if (Hierarchy2.UseDkVisible) {
				var c = go.GetComponent("dfControl");
				if (c != null) return (bool)c.GetProperty("IsVisible");
			}
			return go.activeSelf;
		}
		static internal void SetGOActive(this GameObject go, bool value, bool? activeParents = null, string undoKey = "h@-auto")
        {
            //activeParents == null : activeParents if setActive==true
            if (undoKey == "h@-auto") undoKey = value ? "Show GameObject" : "Hide GameObject";

            //if (!string.IsNullOrEmpty(undoKey)) Undo.RecordObject(go, undoKey);
            go.RecordUndo(undoKey);
            go.SetActive(value);
			var smart = SmartActiveGo(go, value);

			if (!smart && (activeParents ?? value) && !go.activeInHierarchy)
            {
                go.ForeachParent2(p =>
                {
                    //if (!string.IsNullOrEmpty(undoKey)) Undo.RecordObject(p, undoKey);
                    p.RecordUndo(undoKey);
                    p.SetActive(true);
                    return !p.activeInHierarchy;
                });
            }
        }
        static internal void ToggleActive(this GameObject go, bool invertMe, bool? activeParents = null)
        {
            var isActive = go.activeSelf;
            var key = isActive ? "Hide Selected GameObjects" : "Show Selected GameObjects";

            go.ForeachSelected(
                (item, idx) => item.SetGOActive((!invertMe || (item == go)) ? !isActive : isActive, activeParents, key)
            );
        }
        static internal void SetActiveChildren(this GameObject go, bool value, bool? activeParents)
        {
            var key = value ? "Show Children" : "Hide Children";
            go.ForeachChild(child => child.SetGOActive(value, activeParents, key), true);
            go.SetGOActive(value, false, key);
        }
        static internal void SetActiveSibling(this GameObject go, bool value, bool? activeParents = null)
        {
            var key = value ? "Show siblings" : "Hide siblings";
            go.ForeachSibling(RootGOList, item => item.SetGOActive(value, activeParents, key));
            go.SetGOActive(!value, false, key);
        }
        static internal void SetActiveParents(this GameObject go, bool value)
        {
            var p = go.transform.parent;
            var key = value ? "Show Parents" : "Hide Parents";
            //if (go.activeSelf != value) go.SetActive(value);

            while (p != null)
            {
                if (p.gameObject.activeSelf != value)
                {
                    p.gameObject.RecordUndo(key);
                    p.gameObject.SetActive(value);
                }
                p = p.parent;
            }
        }


        ///----------------------------------- STATIC -----------------------------------------------
        static internal void SetGOStatic(GameObject go, bool value, bool deep = false, string undoKey = "h@-auto")
        {
            if (undoKey == "h@-auto") undoKey = value ? "Static" : "UnStatic";

            go.RecordUndo(undoKey, true);
            go.isStatic = value;

            if (deep) {
                go.ForeachChild(child => {
                    child.RecordUndo(undoKey, true);
                    child.isStatic = value;
                }, true);
            }
        }
        static internal void ToggleStatic(this GameObject go, string undoKey = "h@-auto") {
            SetGOStatic(go, !go.isStatic, false, undoKey);
        }
        static internal void SetStatic(this GameObject go, bool invertMe, bool smartInvert)
        {//smart mode : auto-deepLock
            var isStatic = go.isStatic;
            var key = isStatic ? "Static" : "UnStatic";

            go.ForeachSelected((item, idx) => SetGOStatic(item,
                (!invertMe || (item == go)) ? !isStatic : isStatic,	// invert static 
                idx == -1 && smartInvert == isStatic,				// deep-lock if isLock=true
                key
            ));
        }
        static internal void InvertStatic(this GameObject go)
        {
            go.ForeachSelected((item, idx) => item.ToggleStatic("Invert Static"));
        }
        static internal void ToggleSiblingStatic(this GameObject go, bool deep = false) {
            var isLock = go.isStatic;
            var key = isLock ? "Static siblings" : "UnStatic siblings";

            go.ToggleStatic(key);
            go.ForeachSibling(RootGOList, sibl => sibl.ToggleStatic(key));
        }
        static internal void RecursiveStatic(bool value) {
            var key = value ? "Recursive Static" : "Recursive Unstatic";
            RootGOList.ForEach(
                rootGO => SetGOStatic(rootGO, value, true, key)
            );
        }

        ///---------------------------------- SIBLINGS ------------------------------------------------
        static internal void SetCombine(this GameObject go, bool value, bool deep = false, string undoKey = "h@-auto")
        {
            if (undoKey == "h@-auto") undoKey = value ? "Combine GameObject" : "Expand GameObject";
            go.ForeachChild(child =>
            {
                //Undo.RegisterCompleteObjectUndo(child, undoKey);
                child.RecordUndo(undoKey, true);
                child.SetFlag(HideFlags.HideInHierarchy, value);
            }, deep);
        }
        static internal void ToggleCombine(this GameObject go, bool deep = false)
        {
            var isCombined = go.IsCombined();
            var key = isCombined ? "Combine Selected GameObjects" : "Expand Selected GameObjects";
            go.ForeachSelected((item, index) => item.SetCombine(!isCombined, deep, key));
            if (go.transform.childCount>0) go.transform.GetChild(0).Ping();
        }
        static internal void ToggleCombineChildren(this GameObject go)
        {
            var val = false;

            go.ForeachChild2(child =>
            {
                val = child.IsCombined();
                return !val;
            });

            var key = val ? "Expand Children" : "Combine Children";
            go.SetCombine(false, false, key);
            go.ForeachChild(child => child.SetCombine(!val, false, key));
        }
        static internal void SetCombineSibling(this GameObject go, bool value)
        {
            var key = value ? "Expand Siblings" : "Combine siblings";

            go.SetCombine(value, false, key);
            go.ForeachSibling(RootGOList, sibl => sibl.SetCombine(!value, false, key));
            if (!value) go.RevealChildrenInHierarchy(true);
        }
        static internal void RecursiveCombine(bool value)
        {
            var key = value ? "Recursive Combine" : "Recursive Expand";
            RootGOList.ForEach(rootGO =>
            {
                var list = rootGO.GetChildren(true);
                foreach (var child in list) child.RecordUndo(key, true);
                rootGO.SetDeepFlag(HideFlags.HideInHierarchy, value);
            });
        }

        ///------------------------ GOTO : SIBLING / PARENT / CHILD -------------------------------

        private static List<Transform> _pingList;

        static internal void PingParent(this Transform t, bool useEvent = false)
        {
            var p = t.parent;
            if (p == null) return;

            //clear history when select other GO
            if (_pingList == null || (_pingList.Count > 0 && _pingList[_pingList.Count - 1].parent != t))
            {
                _pingList = new List<Transform>();
            }

            _pingList.Add(t);
            p.PingAndUseEvent(true, useEvent);
        }
        static internal void PingChild(this Transform t, bool useEvent = false)
        {
            Transform pingT = null;

            if (_pingList == null) _pingList = new List<Transform>();

            if (_pingList.Count > 0)
            {
                var idx = _pingList.Count - 1;
                var c = _pingList[idx];
                _pingList.Remove(c);

                pingT = c;
            }
            else if (t.childCount > 0)
            {
                pingT = t.GetChild(0);
            }

            if (pingT != null) pingT.PingAndUseEvent(true, useEvent);
        }
        static internal void PingSibling(this Transform t, bool useEvent = false)
        {
            t.NextSibling(RootGOList).PingAndUseEvent(true, useEvent);
        }

        ///----------------------------------- CAMERA ---------------------------------------------------

        public static Camera ltCamera;
        public static CameraInfo ltCameraInfo;

        static void SceneCameraApply(bool orthor, Vector3 pos, Quaternion rot)
        {
            Hierarchy2Utils.orthographic = orthor;
            Hierarchy2Utils.m_Rotation = rot;
            Hierarchy2Utils.m_Position = pos;
            Hierarchy2Utils.Refresh();
        }
        static void UpdateLookThrough()
        {
            if (ltCamera != null)
            {
                if (EditorApplication.isPaused) return;

                var sceneCam = Hierarchy2Utils.SceneCamera;
                var hasChanged = ltCamera.transform.position != sceneCam.transform.position ||
                                ltCamera.orthographic != sceneCam.orthographic ||
                                ltCamera.transform.rotation != sceneCam.transform.rotation;

                if (hasChanged) CameraLookThrough(ltCamera);
            }
            else
            {
                EditorApplication.update -= UpdateLookThrough;
            }
        }

        static internal void CameraLookThrough(Camera cam)
        {
            //Undo.RecordObject(cam, "LookThrough");
            var sceneCam = Hierarchy2Utils.SceneCamera;

            if (EditorApplication.isPlaying)
            {
                if (ltCameraInfo == null)
                {
                    ltCameraInfo = new CameraInfo
                    {
                        orthor = Hierarchy2Utils.orthographic,
                        mRotation = Hierarchy2Utils.m_Rotation,
                        mPosition = Hierarchy2Utils.m_Position
                    };

                    EditorApplication.update -= UpdateLookThrough;
                    EditorApplication.update += UpdateLookThrough;
                }
            }
            else
            {
                ltCameraInfo = null;
                ltCamera = null;
            }

            sceneCam.CopyFrom(cam);
            var distance = Hierarchy2Utils.cameraDistance;
            SceneCameraApply(
                cam.orthographic,
                cam.transform.position - (cam.transform.rotation * new Vector3(0f, 0f, -distance)),
                cam.transform.rotation
            );

            //Hierarchy2Utils.orthographic = cam.orthographic;
            //Hierarchy2Utils.m_Rotation = cam.transform.rotation;
            //Hierarchy2Utils.m_Position = cam.transform.position - (cam.transform.rotation * new Vector3(0f, 0f, -distance));
            //Hierarchy2Utils.Refresh();
        }
        static internal void CameraCaptureSceneView(Camera cam)
        {
            ltCamera = null;
            ltCameraInfo = null;

            //Undo.RecordObject(cam, "CaptureSceneCamera");
            cam.RecordUndo("Capture Scene Camera");
            cam.CopyFrom(Hierarchy2Utils.SceneCamera);
        }
        static internal void ToggleLookThroughCamera(Camera c)
        {
            ltCamera = ltCamera == c ? null : c;

            if (ltCamera != null)
            {
                CameraLookThrough(ltCamera);
            }
            else if (ltCameraInfo != null)
            {
                SceneCameraApply(ltCameraInfo.orthor, ltCameraInfo.mPosition, ltCameraInfo.mRotation);
                ltCameraInfo = null;
                ltCamera = null;
            }
        }

        ///----------------------------------- ISOLATE ---------------------------------------------------

        internal static void Isolate_MissingBehaviours(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => item.numScriptMissing() > 0), "Missing"
            );
            if (useEvent) Event.current.Use();
        }
        internal static void Isolate_ObjectsHasScript(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => item.numScript() > 0), "Script"
            );
            if (useEvent) Event.current.Use();
        }
        internal static void Isolate_SelectedObjects(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(Selection.instanceIDs, "Selected");
            if (useEvent) Event.current.Use();
        }
        internal static void Isolate_LockedObjects(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => item.GetFlag(HideFlags.NotEditable)), "Locked"
            );
            if (useEvent) Event.current.Use();
        }
        internal static void Isolate_InActiveObjects(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => !item.activeSelf), "InActive"
            );
            if (useEvent) Event.current.Use();
        }
        internal static void Isolate_CombinedObjects(bool useEvent = false)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => item.HasFlagChild(HideFlags.HideInHierarchy)), "Combined"
            );
            if (useEvent) Event.current.Use();
        }

        internal static void Isolate_ComponentType(Type t)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => (item.GetComponent(t) != null)), t.ToString()
            );
        }
        internal static void Isolate_Component(Component c)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => (item.GetComponent(c.GetType()) != null)), c.GetTitle(false)
            );
        }
        internal static void Isolate_Layer(string layerName)
        {
            var layer = LayerMask.NameToLayer(layerName);
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => item.layer == layer), layerName
            );
        }
        internal static void Isolate_Tag(string tagName)
        {
            vlbEditorWindow.Hierarchy.SetSearchFilter(
                RootGOList.GetFilterInstanceIDs(item => (item.tag == tagName)), tagName
            );
        }

        ///----------------------------------- RESET TRANSFORM -------------------------------------------

        internal static void ResetLocalPosition(GameObject go)
        {
            Selection.activeGameObject = go;
            go.transform.ResetLocalPosition("ResetPosition");
        }
        internal static void ResetLocalRotation(GameObject go)
        {
            Selection.activeGameObject = go;
            go.transform.ResetLocalRotation("ResetRotation");
        }
        internal static void ResetLocalScale(GameObject go)
        {
            Selection.activeGameObject = go;
            go.transform.ResetLocalScale("ResetScale");
        }
        internal static void ResetTransform(GameObject go)
        {
            Selection.activeGameObject = go;
            go.transform.ResetLocalTransform("ResetTransform");
        }

        ///----------------------------------- CREATE GO -------------------------------------------

        internal static void CreateEmptyChild(GameObject go, bool useEvent = false)
        {
            //var willPing = go.transform.childCount == 0 || !go.IsExpanded();

            vlbUnityEditor.NewTransform(
                name: "New".GetNewName(go.transform, "Empty"),
                undo: "NewEmptyChild",
                p: go.transform
            );//.PingAndUseEvent(willPing, useEvent);

            if (useEvent) Event.current.Use();
        }
        internal static void CreateEmptySibling(GameObject go, bool useEvent = false)
        {
            vlbUnityEditor.NewTransform(
                name: "New".GetNewName(go.transform, "Empty"),
                undo: "NewEmptySibling",
                p: go.transform.parent
            );//.PingAndUseEvent(false, useEvent);
            if (useEvent) Event.current.Use();
        }
        internal static void CreateParentAtMyPosition(GameObject go, bool useEvent = false)
        {
            Selection.activeGameObject = go;
            var goT = go.transform;
            var p = vlbUnityEditor.NewTransform(
                name: "NewEmpty".GetNewName(goT.parent, "_parent"),
                undo: "NewParent1",
                p   : goT.parent,
                pos: goT.localPosition,
                scl: goT.localScale,
                rot: goT.localEulerAngles
            );

            goT.Reparent("NewParent1", p);
            //p.gameObject.RevealChildrenInHierarchy();

            if (useEvent) Event.current.Use();
        }
        internal static void CreateParentAtOrigin(GameObject go, bool useEvent = false)
        {
            Selection.activeGameObject = go;
            var goT = go.transform;
            var p = vlbUnityEditor.NewTransform(
                name: "NewEmpty".GetNewName(goT.parent, "_parent"),
                undo: "NewParent2",
                p: goT.parent
            );

            goT.Reparent("NewParent2", p);
            //p.gameObject.RevealChildrenInHierarchy();
            //p.Ping();
            if (useEvent) Event.current.Use();
        }
    }

    class CameraInfo
    {
        public bool orthor;
        public Vector3 mPosition;
        public Quaternion mRotation;
    }



    static internal class vlbGUI
    {
        internal static bool _willRepaint;
        internal static bool willRepaint
        {
            get { return _willRepaint; }
            set { _willRepaint = value; /* if (value) Debug.Log("set to " + value + " : " + Event.current); */ }
        }

        internal static GameObject renameGO;
        public static int renameStep;
    }


}