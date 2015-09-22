using System;

namespace vietlabs
{
    public static class vlbEditorType
    {
        public static Type BaseProjectWindowT {
            get { return "UnityEditor.BaseProjectWindow".GetTypeByName("UnityEditor"); }
        }
        public static Type FilteredHierarchyT {
            get { return "UnityEditor.FilteredHierarchy".GetTypeByName("UnityEditor"); }
        }
        public static Type SearchableEditorWindowT {
            get { return "UnityEditor.SearchableEditorWindow".GetTypeByName("UnityEditor"); }
        }
        public static Type SearchFilterT {
            get { return "UnityEditor.SearchFilter".GetTypeByName("UnityEditor"); }
        }
        public static Type TreeViewT {
            get { return "UnityEditor.TreeView".GetTypeByName("UnityEditor"); }
        }
        public static Type ITreeViewDataSourceT {
            get { return "UnityEditor.ITreeViewDataSource".GetTypeByName("UnityEditor"); }
        }
    }    
}


