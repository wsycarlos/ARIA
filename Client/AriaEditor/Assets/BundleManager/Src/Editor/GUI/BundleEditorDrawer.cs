using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


// In order to compatiblity with Unity4.0 
// which dose not surpport CustomEditor(type, bool) overload
// I have to strip out the GUI logic of BundleEditor into BundleEditorDrawer,
// And create two more editor class for asset bundle and scene bundle.
public static class BundleEditorDrawer
{
	public static Editor CurrentBundleEditor = null;

	static SceneBundleInpectorObj sbInspectorObj = null;
	static AssetBundleInspectorObj abInpsectorObj = null;

	static BundleData currentBundle = null;

	static Vector2 m_ScrollViewPosition = Vector2.zero;
	
	static bool m_FoldoutIncludes = true;
	static bool m_FoldoutMetaFiles = true;
	
	static string m_CurSelectAsset = "";
	static bool m_IsMetaListSelect = false;
	static double m_LastClickTime = 0;


	//
	public static void ShowBundle(BundleData newBundle)
	{
		// Show dummy object in inspector
		if(sbInspectorObj == null)
		{
			sbInspectorObj = ScriptableObject.CreateInstance<SceneBundleInpectorObj>();
			sbInspectorObj.hideFlags = HideFlags.DontSave;
		}

		if(abInpsectorObj == null)
		{
			abInpsectorObj = ScriptableObject.CreateInstance<AssetBundleInspectorObj>();
			abInpsectorObj.hideFlags = HideFlags.DontSave;
		}

		if(newBundle != null)
			Selection.activeObject = newBundle.sceneBundle? (Object)sbInspectorObj : (Object)abInpsectorObj;
		else
			Selection.activeObject = null;
		
		// Update bundle
		if(newBundle == currentBundle)
			return;
		
		currentBundle = newBundle;
		
		Refresh();
	}

	public static void Refresh()
	{
		if(currentBundle != null && Selection.activeObject != null)
			Selection.activeObject.name = currentBundle.name;

		if(CurrentBundleEditor != null)
			CurrentBundleEditor.Repaint();
	}

	public static void DrawInspector()
	{
		if(currentBundle == null)
		{
			GUILayout.FlexibleSpace();
			GUILayout.Label("Select bundle to check its content.");
			GUILayout.FlexibleSpace();
			return;
		}
		
		m_ScrollViewPosition = EditorGUILayout.BeginScrollView(m_ScrollViewPosition);
		{
			// Bundle type and version
			BundleBuildState buildStates = BundleManager.GetBuildStateOfBundle(currentBundle.name);
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label(currentBundle.sceneBundle ? "Scene bundle" : "Asset bundle", BMGUIStyles.GetBuildinStyle("BoldLabel"));
				GUILayout.FlexibleSpace();
				GUILayout.Label("Version " + buildStates.version, BMGUIStyles.GetBuildinStyle("BoldLabel"));
			}
			EditorGUILayout.EndHorizontal();
            string path = EditorGUILayout.TextField("Path ", currentBundle.path);
		    if (currentBundle.path != path)
		    {
		        currentBundle.path = path;
		        foreach (var child in currentBundle.children)
		        {
		            BundleData bd = BundleManager.GetBundleData(child);
		            if (bd != null)
		                bd.path = path;
		        }
                BMDataAccessor.SaveBundleData();
		    }
			GUILayout.BeginHorizontal();
			{
				string sizeStr = "Build Size " + (buildStates.size == -1 ? "Unkown" : Mathf.CeilToInt(buildStates.size / 1024f) + " KB");
				GUILayout.Label(sizeStr, BMGUIStyles.GetBuildinStyle("BoldLabel"));
				GUILayout.FlexibleSpace();
				GUILayout.Label("Priority", EditorStyles.boldLabel);
				currentBundle.priority = EditorGUILayout.Popup(currentBundle.priority, new string[]{"0","1","2","3","4","5","6","7","8","9"}, GUILayout.MaxWidth(40));
			}
			GUILayout.EndHorizontal();
			
			GUILayout.Space(5);
			
			EditorGUILayout.BeginVertical(BMGUIStyles.GetBuildinStyle("Wizard Box"));
			{
				GUI_Inlcudes();
				GUI_DependencyList();
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndScrollView();

		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();
			if(GUILayout.Button("Refresh"))
				BundleManager.RefreshAll();
		}
		GUILayout.EndHorizontal();
	}

	static bool HasFocuse()
	{
		if(EditorWindow.focusedWindow == null)
			return false;
		else
			return EditorWindow.focusedWindow.title == "UnityEditor.InspectorWindow";
	}
	
	static void GUI_Inlcudes()
	{
		if(currentBundle.includeGUIDs.Count > 0)
		{
#if !(UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
			m_FoldoutIncludes = EditorGUILayout.Foldout(m_FoldoutIncludes, "INCLUDE", BMGUIStyles.GetCustomStyle("CFoldout"));
#else
			m_FoldoutIncludes = EditorGUILayout.Foldout(m_FoldoutIncludes, "INCLUDE");
#endif
		}
		else
		{
			GUILayout.Label("INCLUDE", BMGUIStyles.GetCustomStyle("UnfoldableTitle"));
		}
		
		if(!m_FoldoutIncludes)
			return;
		
		EditorGUILayout.BeginVertical();
		{
			foreach(var guid in currentBundle.includeGUIDs)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guid);
				bool isCurrentPathSelect = m_CurSelectAsset == assetPath && !m_IsMetaListSelect;
				AssetItemState itemState = GUI_AssetItem(assetPath, isCurrentPathSelect, GetSharedIconOfInlucde(guid));
				if(itemState != AssetItemState.None)
				{
					if(!isCurrentPathSelect)
					{
						m_IsMetaListSelect = false;
						m_CurSelectAsset = assetPath;
					}
					else if(itemState != AssetItemState.RClicked) // Only left click can disable selection
					{
						if(EditorApplication.timeSinceStartup - m_LastClickTime < 2f)
						{
							// Double clicked
							EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(assetPath, typeof( Object )));
						}
						else
						{
							m_CurSelectAsset = "";
						}
					}
					
					m_LastClickTime = EditorApplication.timeSinceStartup;
					Refresh();
					
					// Right click
					if(itemState == AssetItemState.RClicked)
					{
						GenericMenu rightClickMenu = new GenericMenu();
						rightClickMenu.AddItem(new GUIContent("Delete"), false, GUI_DeleteMenuCallback);
						rightClickMenu.DropDown(new Rect( Event.current.mousePosition.x, Event.current.mousePosition.y, 0, 0) );
					}
				}
			}
		}EditorGUILayout.EndVertical();
	}
	
	static void GUI_DependencyList()
	{
		if(currentBundle.dependGUIDs.Count > 0)
		{
#if !(UNITY_4_2 || UNITY_4_1 || UNITY_4_0)
			m_FoldoutMetaFiles = EditorGUILayout.Foldout(m_FoldoutMetaFiles, "DEPEND", BMGUIStyles.GetCustomStyle("CFoldout"));
#else
			m_FoldoutMetaFiles = EditorGUILayout.Foldout(m_FoldoutMetaFiles, "DEPEND");
#endif
		}
		else
		{
			GUILayout.Label("DEPEND", BMGUIStyles.GetCustomStyle("UnfoldableTitle"));
			return;
		}
		
		if(m_FoldoutMetaFiles)
		{
			EditorGUILayout.BeginVertical();
			{
				foreach(string guid in currentBundle.dependGUIDs)
				{
					string assetPath = AssetDatabase.GUIDToAssetPath(guid);
					bool isCurrentPathSelect = m_CurSelectAsset == assetPath && m_IsMetaListSelect;
					if( GUI_AssetItem( assetPath, isCurrentPathSelect, GetSharedIconOfDepend(guid) ) != AssetItemState.None )
					{
						if(!isCurrentPathSelect)
						{
							m_IsMetaListSelect = true;
							m_CurSelectAsset = assetPath;
						}
						else
						{
							if(EditorApplication.timeSinceStartup - m_LastClickTime < 2f)
							{
								// Double clicked
								EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(assetPath, typeof( Object )));
							}
							else
							{
								m_CurSelectAsset = "";
							}
						}
						
						m_LastClickTime = EditorApplication.timeSinceStartup;
						Refresh();
					}
				}
				
			}EditorGUILayout.EndVertical();
		}
	}
	
	enum AssetItemState{None, RClicked, LClicked};
	static AssetItemState GUI_AssetItem(string assetPath, bool isSelect)
	{
		return GUI_AssetItem(assetPath, isSelect, null);
	}
	
	static AssetItemState GUI_AssetItem(string assetPath, bool isSelect, Texture shareStateIcon)
	{	
		GUIContent assetContent = new GUIContent(Path.GetFileNameWithoutExtension(assetPath), AssetDatabase.GetCachedIcon(assetPath));
		EditorGUIUtility.SetIconSize(new Vector2( 16f, 16f));
		
		GUILayout.BeginHorizontal(GetItemStyle(isSelect, HasFocuse()));
		{
			GUILayout.Space(20);
			GUIStyle labelStyel = GetLabelStyle(isSelect, assetContent.image != null);
			GUILayout.Label(assetContent, labelStyel, GUILayout.MaxHeight(18), GUILayout.ExpandWidth(true));
			GUILayout.FlexibleSpace();
			if(shareStateIcon != null)
			{
				EditorGUIUtility.SetIconSize(new Vector2( 27f, 12f));
				GUILayout.Label(shareStateIcon);
			}
		}
		GUILayout.EndHorizontal();
		
		EditorGUIUtility.SetIconSize(Vector2.zero);
		
		bool mouseBtnClicked = Event.current.type == EventType.MouseUp && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
		if(mouseBtnClicked)
		{
			#if UNITY_EDITOR_OSX
			if((Event.current.button == 0 && Event.current.control == true)
			   || Event.current.button == 1)
				return AssetItemState.RClicked;
			else
				return AssetItemState.LClicked;
			#else
			if(Event.current.button == 1)
				return AssetItemState.RClicked;
			else
				return AssetItemState.LClicked;
			#endif
		}
		else
			return AssetItemState.None;
	}
	
	static void GUI_DeleteMenuCallback()
	{
		BundleManager.RemovePathFromBundle(m_CurSelectAsset, currentBundle.name);
		Refresh();
	}
	
	static GUIStyle GetLabelStyle(bool selected, bool exist)
	{
		if(!exist)
			return BMGUIStyles.GetCustomStyle("CAssetLabelRed");
		
		if(selected)
			return BMGUIStyles.GetCustomStyle("CAssetLabelActive");
		else
			return BMGUIStyles.GetCustomStyle("CAssetLabelNormal");
	}
	
	static GUIStyle GetItemStyle(bool selected, bool focused)
	{
		if(!selected)
		{
			return BMGUIStyles.GetCustomStyle("TreeItemUnSelect");
		}
		else
		{
			if(focused)
				return BMGUIStyles.GetCustomStyle("TreeItemSelectBlue");
			else
				return BMGUIStyles.GetCustomStyle("TreeItemSelectGray");
		}
	}
	
	static Texture2D GetSharedIconOfDepend(string guid)
	{
		var bundleList = BundleManager.GetIncludeBundles(guid);
		if(bundleList != null && bundleList.Count > 0)
		{			
			foreach(BundleData bundle in bundleList)
			{
				if(bundle.name == currentBundle.name)
					continue;

				if( BundleManager.IsBundleDependOn(currentBundle.name, bundle.name) )
					return BMGUIStyles.GetIcon("sharedAsset");
			}
		}
		
		bundleList = BundleManager.GetRelatedBundles(guid);
		if(bundleList != null && bundleList.Count > 1)
		{
			foreach(BundleData bundle in bundleList)
			{
				if(bundle.name == currentBundle.name)
					continue;

				if( BundleManager.IsBundleDependOn(currentBundle.name, bundle.name))
					return BMGUIStyles.GetIcon("sharedAsset");

				if( !BundleManager.IsBundleDependOn(bundle.name, currentBundle.name) && 
				   !BundleManager.IsBundleDependOn(currentBundle.name, bundle.name))
					return BMGUIStyles.GetIcon("duplicatedDepend");
			}
		}
		
		return null;
	}
	
	static Texture2D GetSharedIconOfInlucde(string guid)
	{
		var bundleList = BundleManager.GetRelatedBundles(guid);
		if(bundleList != null && bundleList.Count > 0)
		{
			foreach(BundleData bundle in bundleList)
			{
				if(bundle.name == currentBundle.name)
					continue;
				if( BundleManager.IsBundleDependOn(bundle.name, currentBundle.name) )
					return BMGUIStyles.GetIcon("dependedAsset");
				else if( BundleManager.IsBundleDependOn(currentBundle.name, bundle.name) )
					return BMGUIStyles.GetIcon("sharedAsset");
			}
		}
		
		bundleList = BundleManager.GetIncludeBundles(guid);
		if(bundleList != null && bundleList.Count > 1)
			return BMGUIStyles.GetIcon("duplicatedInclude");
		
		return null;
	}
}
