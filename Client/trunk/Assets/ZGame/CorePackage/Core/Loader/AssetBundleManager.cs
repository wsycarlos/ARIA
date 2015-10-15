using System;
using System.Collections.Generic;
using System.Reflection;
using JenkinBuild;
using Loader;
using UnityEngine;

namespace ZGame
{
    /// <summary>
    /// AssetBundle管理器
    /// </summary>
    public class AssetBundleManager : MonoBehaviour
    {
        protected static AssetBundleManager instance;

        public static AssetBundleManager Instance
        {
            get
            {
                return instance;
            }
        }

        //用于缓存bundle
        public static List<Dictionary<string, AssetBundle>> cache;// = new List<Dictionary<string, AssetBundle>>();
        public static Dictionary<AssetBundleType, string> pathDic;// = new Dictionary<AssetBundleType, string>();

        protected static List<AssetBundleLoader> loaders = new List<AssetBundleLoader>();
        protected static Dictionary<ILoader, List<System.Action<GameObject, string>>> assetLoadedDict = new Dictionary<ILoader, List<System.Action<GameObject, string>>>();

        private static void Init()
        {
            if (pathDic == null)
            {
                InitConfig();
            }

            if (cache == null)
            {
                InitCache();
            }

        }

        private static void InitCache()
        {
            cache = new List<Dictionary<string, AssetBundle>>();
            //初始化cache
            for (int i = 0; i < Enum.GetValues(typeof(AssetBundleType)).Length - 1; i++)
            {
                cache.Add(new Dictionary<string, AssetBundle>());
            }
        }

        private static void InitConfig()
        {
            pathDic = new Dictionary<AssetBundleType, string>();
            FieldInfo[] props = typeof(AssetBundleType).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
            foreach (FieldInfo prop in props)
            {
                AssetBundleType assetType = (AssetBundleType)prop.GetValue(null);
                object[] attrs = prop.GetCustomAttributes(typeof(AssetDefAttribute), false);
                if (attrs.Length > 0)
                {
                    AssetDefAttribute def = attrs[0] as AssetDefAttribute;
                    pathDic.Add(assetType, def.path == "" ? assetType.ToString().ToLower() : def.path);
                    //Debug.Log(assetType.ToString() + "|" + def.path + "|");
                }
            }

        }



        void Awake()
        {
            instance = this;
            Init();
        }

        void Update()
        {
            if (loaders != null)
            {
                for (int i = 0; i < loaders.Count; i++)
                {
                    if (loaders[i] != null)
                    {
                        if (!loaders[i].IsLoading && !loaders[i].IsDone)
                        {
                            loaders[i].StartLoad();
                        }
                        else if (loaders[i].IsLoading)
                        {
                            loaders[i].Update();
                        }
                    }
                }
            }


        }

        ///// <summary>
        ///// 我操,不卸载bundle Editor里面退出会爆掉
        ///// </summary>
        //void OnApplicationQuit()
        //{
        //    UnloadAllBundle(true);
        //}

        public static void LoadBundleAsset(string bid, AssetBundleType type, System.Action<GameObject, string> onComplete, object data = null)
        {
            //D.log("[AssetBundleManager] start to load bundle " + type.ToString() + "  " + bid);
            if (ContainsBundle(bid, type))
            {
                //D.log("[AssetBundleManager] already has ,so callback");
                //创建一个GameObject
                LoadCallback(bid, onComplete, GetBundle(bid, type));
            }
            else
            {
                //D.log("[AssetBundleManager] add loader complete call back");
                AssetBundleLoader loader = loaders.Find(i => { return i.bundleId == bid; });
                if (loader != null)
                {
                    assetLoadedDict[loader].Add(onComplete);
                }
                else
                {

                    loader = GetBundleLoader(bid, type);

                    //CheckDependenceLoader(loader);

                    loader.OnComplete += OnLoaderComplete;
                    //                    loader.StartLoad();
                    loaders.Add(loader);
                    assetLoadedDict[loader] = new List<Action<GameObject, string>>();
                    assetLoadedDict[loader].Add(onComplete);
                }

            }
        }
        static void AddAbLoader(AssetBundleLoader loader)
        {
            if (ContainsBundle(loader.bundleId, loader.type))
                return;
            AssetBundleLoader ld = loaders.Find(i => { return i.bundleId == loader.bundleId; });
            if (ld == null)
            {
                loader.OnComplete += OnLoaderComplete;
                //                loader.StartLoad();
                loaders.Add(loader);
            }

        }

        //static void CheckDependenceLoader(AssetBundleLoader loader)
        //{
        //    if (ResUpdateManager.Instance.ConfigLoaded)
        //    {
        //        List<string> preload = ResUpdateManager.Instance.getDependList(loader.bundleId);
        //        foreach (var kkk in preload)
        //        {
        //            AddAbLoader(GetBundleLoader(kkk, loader.type));
        //        }
        //    }

        //}

        /// <summary>
        /// 异步加载没有明显效果(原意：用来异步加载bundle，减少卡顿)
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="onComplete"></param>
        /// <param name="adb"></param>
        private static void LoadCallback(string bid, System.Action<GameObject, string> onComplete, AssetBundle adb)
        {
            //asyncLoadDic.Add(adb.LoadAsync(bid, typeof(GameObject)), onComplete);
            //Debug.Log(adb.Load(bid));
            GameObject ga = null;
            //FIXME 模型bundle中prefab没有前缀P_的情况处理
            if (bid.Contains("P_") && !adb.Contains(bid))
                ga = adb.LoadAsset<GameObject>(bid.Substring(2));
            else
            {
                ga = adb.LoadAsset<GameObject>(bid);
            }

            GameObject go = GameObject.Instantiate(ga) as GameObject;
            onComplete(go, bid);
        }

        public static void StopLoad(string bid)
        {
            AssetBundleLoader loader = loaders.Find(i => { return i.bundleId == bid; });
            if (loader != null)
            {
                loader.StopLoad();
                //D.log("[AssetBundleManager] stop load bundle " + bid);

                if (assetLoadedDict.ContainsKey(loader))
                {
                    assetLoadedDict[loader] = null;
                    assetLoadedDict.Remove(loader);
                    loader.OnComplete -= OnLoaderComplete;
                    loaders.Remove(loader);
                }
            }
        }

        private static void OnLoaderComplete(ILoader ldr)
        {
            AssetBundleLoader loader = ldr as AssetBundleLoader;

            if (null != loader)
            {

                loader.OnComplete -= OnLoaderComplete;
                loaders.Remove(loader);


                if (assetLoadedDict.ContainsKey(ldr))
                {

                    List<Action<GameObject, string>> callbacks = assetLoadedDict[ldr];
                    foreach (System.Action<GameObject, string> complete in callbacks)
                    {
                        if (loader.LoadedAsset == null)
                        {
                            //D.log("[AssetBundleManager] loaded asset is null------");
                            complete(null, loader.bundleId);
                        }
                        else
                        {
                            LoadCallback(loader.bundleId, complete, loader.LoadedAsset as AssetBundle);
                            //GameObject go = GameObject.Instantiate((loader.LoadedAsset as AssetBundle).Load(loader.bundleId)) as GameObject;
                            //complete(go, loader.bundleId);
                        }
                    }
                    assetLoadedDict[ldr] = null;
                    assetLoadedDict.Remove(ldr);
                }

            }
        }


        private static string extension = ".bd";
        public static AssetBundleLoader GetBundleLoader(string bid, AssetBundleType bundleType)
        {
            AssetBundleLoader loader = new AssetBundleLoader();


            if (bundleType == AssetBundleType.NONE)
            {
                Debug.LogWarning("bundle type is not valid");
                return null;
            }

            string assetName = GetBundlePath(bundleType, bid, extension);
            loader.Init(assetName, GetVersion(bid));
            loader.type = bundleType;
            loader.bundleId = bid;
            return loader;
        }

        private static int GetVersion(string bundleId)
        {
            return ResUpdateManager.Instance.GetVersion(bundleId);
        }

        public static Dictionary<string, AssetBundle> GetBundleDic(AssetBundleType type)
        {
            return cache[(int)type];
        }

        /// <summary>
        /// 获取加载路径
        /// </summary>
        /// <param name="p">路径类型</param>
        /// <param name="id">资源id</param>
        /// <param name="extensionName">扩展名</param>
        /// <returns></returns>
        public static string GetBundlePath(AssetBundleType p, string id, string extensionName = ".bd")
        {
#if UNITY_EDITOR
            return Application.dataPath + "/../AssetBundle/" + JenkinsBuildConfig.Instance.language + "/" + UnityUtil.GetPlatformName() + "/" + pathDic[p] + "/" + id + extensionName;

#else
            DownloadFileInfo df = ResUpdateManager.Instance.GetDownloadedFileInfo(id);
            if (df != null)
                return ResUpdateManager.sdcardRootUrl + UnityUtil.GetPlatformName() + "/" + df.path + "/" + id + extensionName;
            else
                return Application.streamingAssetsPath + "/" + UnityUtil.GetPlatformName() + "/" + pathDic[p] + "/" + id + extensionName;
#endif
        }

        public static void CacheBundle(string id, AssetBundle bundle, AssetBundleType type)
        {

            if (ContainsBundle(id, type))
            {
                return;
            }

            if (bundle == null)
            {
                //D.warn("---------------------------asset Bundle is null-------------------------" + id);
                return;
            }

            int index = (int)type;

            if (index < 0 || index >= cache.Count)
            {
                Debug.LogWarning("not a valid type");
                return;
            }

            cache[index].Add(id, bundle);
        }

        public static bool ContainsBundle(string id, AssetBundleType type)
        {
            if (cache[(int)type].ContainsKey(id))
            {
                return true;
            }

            return false;
        }

        public static AssetBundle GetBundle(string id, AssetBundleType type)
        {
            if (cache[(int)type].ContainsKey(id))
            {
                return cache[(int)type][id];
            }
            return null;
        }

        public static void UnloadBundle(string id, bool unloadAllLoadedObjects, AssetBundleType type)
        {
            int index = (int)type;
            if (cache[index].ContainsKey(id))
            {
                cache[index][id].Unload(unloadAllLoadedObjects);
                cache[index].Remove(id);
            }
        }

        public static void UnloadAllBundle(bool unloadAllLoadedObjects)
        {
            if (cache != null)
                foreach (Dictionary<string, AssetBundle> dic in cache)
                {
                    foreach (AssetBundle one in dic.Values)
                    {
                        one.Unload(unloadAllLoadedObjects);
                    }
                    dic.Clear();
                }
        }

        public static void UnloadAllBundle(bool unloadAllLoadedObjects, AssetBundleType type)
        {
            foreach (AssetBundle one in cache[(int)type].Values)
            {
                one.Unload(unloadAllLoadedObjects);
            }
            cache[(int)type].Clear();
        }

        internal void Dispose()
        {
            UnloadAllBundle(true);
        }
    }


}