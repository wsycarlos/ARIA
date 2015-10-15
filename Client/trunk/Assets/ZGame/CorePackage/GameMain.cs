using System.Collections.Generic;
using Game.Core;
using Game.Manager;
using UnityEngine;
using Loader;
using ZGame;

public class GameMain : MonoBehaviour
{


    private QueueLoader loader;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        Dispoe();
    }

    private void Dispoe()
    {
        AssetBundleManager.Instance.Dispose();
    }

    private void Main()
    {
        InitManager();
    }

    private void InitManager()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        //测试用
        Caching.CleanCache();
#endif
        ServerListManager.instance.OnServerStatusChange += OnServerStatus;
    }

    private void OnServerStatus(bool obj)
    {
        if (obj)
        {
            //服务器列表加载成功,启动资源加载
            gameObject.AddComponent<ResUpdateManager>();
            gameObject.AddComponent<AssetBundleManager>();
            ResUpdateManager.Instance.Init(LoadAsset);
        }
        else
        {
            //服务器列表加载失败
            D.error("Server list load failed!");
        }
    }

    private void InitGame()
    {
        //初始化L#环境，需要在其他外部代码初始化之前初始化
        gameObject.AddComponent<ExtensionManager>();
        //外部代码初始化操作
        gameObject.AddComponent<GameManager>().GameStart();
    }

    private void LoadAsset()
    {
        loader = new QueueLoader();
        loader.AddLoader(AssetBundleManager.GetBundleLoader("ui", AssetBundleType.UI));
        loader.AddLoader(AssetBundleManager.GetBundleLoader("uishared", AssetBundleType.UI));
        loader.AddLoader(AssetBundleManager.GetBundleLoader("extends", AssetBundleType.EXTENDS));
        loader.AddLoader(AssetBundleManager.GetBundleLoader("data", AssetBundleType.DATA));
        loader.AddLoader(AssetBundleManager.GetBundleLoader("template", AssetBundleType.DATA));
        loader.OnComplete += AssetLoadComplete;
        loader.StartLoad();
    }

    private void AssetLoadComplete(ILoader ldr)
    {
        loader.OnComplete -= AssetLoadComplete;
        if (loader == null) return;
        var dic = loader.LoadedAsset as Dictionary<string, ILoader>;
        if (dic != null)
        {
            CacheAssetBundle(dic);
            InitGame();
        }
        else
        {
            Debug.LogError("------------assetbundle load failed----------------");
        }
        loader = null;
    }

    private static void CacheAssetBundle(Dictionary<string, ILoader> dic)
    {
        AssetBundleManager.CacheBundle("ui", dic["ui.bd"].LoadedAsset as AssetBundle, AssetBundleType.UI);
        AssetBundleManager.CacheBundle("uishared", dic["uishared.bd"].LoadedAsset as AssetBundle, AssetBundleType.UI);
        AssetBundleManager.CacheBundle("extends", dic["extends.bd"].LoadedAsset as AssetBundle, AssetBundleType.EXTENDS);
        AssetBundleManager.CacheBundle("data", dic["data.bd"].LoadedAsset as AssetBundle, AssetBundleType.DATA);
        AssetBundleManager.CacheBundle("template", dic["template.bd"].LoadedAsset as AssetBundle, AssetBundleType.DATA);
    }

    private void Update()
    {
        if (loader != null)
        {
            loader.Update();
        }
    }

}

