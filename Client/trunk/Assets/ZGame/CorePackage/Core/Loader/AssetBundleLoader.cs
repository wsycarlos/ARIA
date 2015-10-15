using System;
using UnityEngine;
using ZGame;

namespace Loader
{
    /// <summary>
    /// AssetBundle加载器
    /// </summary>
    public class AssetBundleLoader : ILoader
    {
        private bool isDone = false;
        private string assetName;
        private int version;

        private AssetBundle thisAssetBundle;
        private WWW curWWW;

        public bool IsDone { get { return isDone; } }

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
        }

        public AssetBundleType type = AssetBundleType.NONE;
        public string bundleId;

        #region 实现接口

        public event Action<float> OnProgress;

        public event Action<ILoader> OnComplete;

        public object LoadedAsset
        {
            get { return thisAssetBundle; }
        }

        public string AssetName
        {
            get { return System.IO.Path.GetFileName(assetName); }
        }

        public void Init(string asset, int version = 1)
        {
            this.assetName = asset;
            this.version = version;
        }

        public void StartLoad()
        {
            if (AssetBundleManager.ContainsBundle(bundleId, type))
            {
                thisAssetBundle = AssetBundleManager.GetBundle(bundleId, type);
                OnComplete(this);
                isLoading = false;
                isDone = true;
                return;
            }
            isDone = false;
            isLoading = true;
            //开始下载
            DownloadAssetBundle(assetName, version);
        }

        public void StopLoad()
        {
            if (null != curWWW)
            {
                curWWW.Dispose();
                curWWW = null;
            }
            isLoading = false;
        }

        //private void LoadAssetBundle(string bundleName)
        //{
        //    thisAssetBundle = GameFactory.LoadLocalBundle(bundleName);
        //    if (OnComplete != null)
        //    {
        //        OnComplete(this);
        //    }
        //}

        private void DownloadAssetBundle(string bundleName, int version)
        {
            // Wait for the Caching system to be ready
            while (!Caching.ready)
            {
                //D.error("caching system is not ready");
                //GameConsole.Log("[AssetBundleLoader]:caching system is not ready");
            }

            string url = null;
            if (bundleName.Contains("://"))
            {
                url = bundleName;
            }
            else
            {
                url = "file://" + bundleName;
            }

            ////重置version
            //if (ResUpdateManager.Instance.ConfigLoaded)
            //{
            //    version = ResUpdateManager.Instance.GetVersion(bundleId);
            //}
            Debug.Log("[AssetBundleLoader]:start load assetBundle:" + url + " | ver : " + version);
            curWWW = WWW.LoadFromCacheOrDownload(url, version);

        }

        #endregion

        public void Update()
        {
            if (curWWW == null)
                return;

            if (!String.IsNullOrEmpty(curWWW.error))
            {
                isDone = true;
                Debug.LogError("[AssetBundleLoader]:WWW download:" + curWWW.error);
                StopLoad();
                if (OnComplete != null)
                {
                    thisAssetBundle = null;

                    OnComplete(this);
                }

                return;
            }
            //保存
            if (curWWW.isDone)
            {
                isDone = true;
                if (OnComplete != null)
                {
                    thisAssetBundle = curWWW.assetBundle;
                    //FIXME 默认全部Cache!!
                    AssetBundleManager.CacheBundle(bundleId, thisAssetBundle, type);
                    OnComplete(this);
                }
                StopLoad();
            }
            else
            {
                if (OnProgress != null)
                {
                    OnProgress(curWWW.progress);
                }
            }
        }

    }
}
