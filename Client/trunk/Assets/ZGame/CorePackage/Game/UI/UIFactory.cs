
using Game.Core;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class UIFactory : PaxObjectFactory<UIConfig>
    {
        private static UIFactory instance;
        new public static UIFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIFactory();
                }
                return instance;
            }
        }

        protected override GameObject Create(string type, string name = "", GameObject parent = null)
        {
            GameObject prefab = assetBundle.LoadAsset<GameObject>(type);
            if (prefab == null)
            {
                D.error("[UIFactory]:the ui prefab not fond" + type);
                return null;
            }

            GameObject go = UnityUtil.AddChild(parent, prefab, name == "" ? type : name);
            string[] LogicType = GetLogicType(type);
            if (LogicType != null)
            {
                foreach (string logic in LogicType)
                {
                    ExtensionScripter.extensionName = logic;
                    go.AddComponent<BaseUI>();
                }
            }
            else
            {
                D.error("[UIFactory]:the ui{" + type + "} have no script attached");
            }
            //go.SetActive(false);
            return go;
        }
    }
}
