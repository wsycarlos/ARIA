
using Game.Core;
using Game.Pool;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class PaxObjectFactory<T> where T : GameObjectConfig
    {
        protected AssetBundle assetBundle;
        protected GameObjectConfigDictionary<T> config;

        //protected Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

        private static PaxObjectFactory<T> instance;
        public static PaxObjectFactory<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaxObjectFactory<T>();
                }
                return instance;
            }
        }

        protected virtual GameObject CacheObject(string type, string name = "", GameObject parent = null)
        {
            if (!PoolManager.Instance.Contain(type))
            {
                PoolManager.Instance.CacheGameObject(type, Create(type, name, parent));
            }

            return PoolManager.Instance.CreateCacheObject(type);
        }

        protected virtual GameObject Create(string type, string name = "", GameObject parent = null)
        {
            GameObject prefab = assetBundle.LoadAsset<GameObject>(type);
            if (prefab == null)
            {
                D.error("[PaxObjectFactory]:the object prefab not fond");
                return null;
            }
            GameObject go = UnityUtil.AddChild(parent, prefab, name == "" ? type : name);
            string[] LogicType = GetLogicType(type);
            if (LogicType != null)
            {
                foreach (string logic in LogicType)
                {
                    ExtensionScripter.extensionName = logic;
                    go.AddComponent<ExtensionScripter>();
                }
            }
            else
            {
                D.log("[PaxObjectFactory]:" + type + "have no scripts attached");
            }

            return go;
        }

        public virtual GameObject CreateGameObject(string type, string name = "", GameObject parent = null, bool cache = false)
        {
            if (cache)
            {
                return CacheObject(type, name, parent);
            }
            else
            {
                return Create(type, name, parent);
            }

        }

        protected virtual string[] GetLogicType(string type)
        {
            if (config[type] != null)
            {
                return config[type].scripts;
            }
            return null;
        }

        public virtual void Init(AssetBundle bundle, GameObjectConfigDictionary<T> config)
        {
            this.assetBundle = bundle;
            this.config = config;
        }

        public virtual void Dispose()
        {
            this.assetBundle = null;
            this.config = null;
        }
    }
}
