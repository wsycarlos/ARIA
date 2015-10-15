using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 用来保存UI编辑器的相关配置文件
/// </summary>
public class UIDataObject : ScriptableObject
{
    //用来生成prefab的代码
    //public string UIPrefabPath = "Assets/ZGame/UIPackage/Export/Prefabs/";
    //生成ui逻辑代码类的配置路径
    public string UIScriptPath = "Assets/Scripts/";
    //代码生成路径
    public string ScriptOutPath(string fname = "")
    {
        string filename = Application.dataPath + "/../../HobExtends\\HobExtends\\src\\com\\morefungame\\hob\\Game\\UI\\Abstract/" + fname;
        return filename;
    }

    public string BundleOutPath = "";
    public List<GameObject> panels = new List<GameObject>();
    public GameObject onlyAct = null;
    public List<GameObject> controls = new List<GameObject>();
    public GameObject onlyActCtl = null;
    public int FaceDepth = 0;
    public int PanelDepth = 5;
    public int PopupDepth = 10;
    public int ControlDepth = 5;
}
