using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Loader
{
    public class QueueLoader : ILoader
    {
        class CompleteAction
        {
            public Action<Action<ILoader>, object> ExecuteAction;
            public object Arg;
        }
        /// <summary>
        /// 避免AssetBundleLoader重复列表,只在加载前添加loader起作用
        /// </summary>
        private Dictionary<string, AssetBundleLoader> assetBundleLoaderDic = new Dictionary<string, AssetBundleLoader>();
        private Queue<ILoader> loaderQueue = new Queue<ILoader>();
        private Dictionary<string, ILoader> assetDic = new Dictionary<string, ILoader>();

        public event Action<float> OnProgress;
        public event Action<ILoader> OnComplete;

        private ILoader curLoader;

        private float total = 0;
        private float current = 0;

        private CompleteAction completeAction;

        /// <summary>
        /// QueueLoader的Asset是一个Dictionary<String，Iloader>
        /// </summary>
        public object LoadedAsset
        {
            get { return assetDic; }
        }

        public string AssetName
        {
            get { throw new InvalidOperationException("queue loader can't use this"); }
        }

        public void Init(string assetName, int version = 1)
        {
            throw new InvalidOperationException("queue loader can't use this");
        }

        public void AddLoader(ILoader loader, Action<ILoader> onComplete = null)
        {
            if (loader is AssetBundleLoader)
            {
                if (!CheckInAssetBundleLoader(loader as AssetBundleLoader))
                {
                    Debug.LogWarning("[QueueLoader] try to add duplicate AssetBundleLoader : " + loader.AssetName);
                    return;
                }
            }
            //如果需要自己监听
            if (onComplete != null)
            {
                loader.OnComplete += onComplete;
            }
            loaderQueue.Enqueue(loader);
        }
        protected void AddAbLoader(AssetBundleLoader loader)
        {
            if (assetBundleLoaderDic.ContainsKey(loader.bundleId))
                return;
            assetBundleLoaderDic.Add(loader.bundleId, loader);
            loaderQueue.Enqueue(loader);
        }

        bool CheckInAssetBundleLoader(AssetBundleLoader loader)
        {
            if (assetBundleLoaderDic.ContainsKey(loader.bundleId))
                return false;
            ////检查依赖关系 添加
            //if (ResUpdateManager.Instance.ConfigLoaded)
            //{
            //    List<string> preload = ResUpdateManager.Instance.getDependList(loader.bundleId);
            //    foreach (var kkk in preload)
            //    {
            //        AddAbLoader(AssetBundleManager.GetBundleLoader(kkk, loader.type));
            //    }
            //}
            assetBundleLoaderDic.Add(loader.bundleId, loader);
            return true;
        }

        public void AddSceneLoader(string sceneName, Action<ILoader> onComplete = null)
        {
            SceneLoader loader = new SceneLoader();
            loader.Init(sceneName);
            //如果需要自己监听
            if (onComplete != null)
            {
                loader.OnComplete += onComplete;
            }
            loaderQueue.Enqueue(loader);
        }

        public void AddCompleteAction(Action<Action<ILoader>, object> excute, object arg)
        {
            completeAction = new CompleteAction()
            {
                ExecuteAction = excute,
                Arg = arg,
            };
        }

        private void Loader_OnComplete(ILoader loader)
        {
            if (loader != null)
            {
                loader.OnComplete -= Loader_OnComplete;
                loader.OnProgress -= Loader_OnProgress;
                //保存加载素材
                D.log("[QueueLoader]-----------------loaded complete:" + loader.AssetName);
                //if (loader is BattleLoader)
                //{

                //}
                //else
                //{
                    if (assetDic.ContainsKey(loader.AssetName))
                    {
                        assetDic[loader.AssetName] = loader;
                    }
                    else
                        assetDic.Add(loader.AssetName, loader);
                //}

                curLoader = null;
                if (loaderQueue.Count > 0)
                {
                    curLoader = loaderQueue.Dequeue();
                    if (curLoader != null)
                    {
                        current++;
                        curLoader.OnComplete += Loader_OnComplete;
                        curLoader.OnProgress += Loader_OnProgress;
                        curLoader.StartLoad();
                    }
                }
                else
                {

                    if (completeAction != null)
                    {
                        D.log("[QueueLoader]ExecuteAction");
                        completeAction.ExecuteAction(Loader_OnComplete, completeAction.Arg);
                        completeAction = null;
                    }
                    else
                    {
                        if (OnComplete != null)
                        {
                            OnComplete(this);
                        }
                    }
                }
            }
            else
            {
                if (OnComplete != null)
                {
                    D.log("[QueueLoader]-----------------CompleteAction:");
                    OnComplete(this);
                }
            }

        }

        public void Update()
        {
            if (curLoader != null)
            {
                curLoader.Update();
            }
        }

        public void StartLoad()
        {
            //初始化进程
            current = 0;
            total = loaderQueue.Count + loaderQueue.Count * 0.2f;//假的，显得东西多
            //清空检测重复列表
            assetBundleLoaderDic.Clear();
            D.log("[QueueLoader]total progress:" + total);
            curLoader = loaderQueue.Dequeue();
            if (curLoader != null)
            {
                curLoader.OnComplete += Loader_OnComplete;
                curLoader.OnProgress += Loader_OnProgress;
                curLoader.StartLoad();
            }
            else
            {
                D.log("[QueueLoader]queue loader has no asset to load");
            }
        }

        private void Loader_OnProgress(float obj)
        {
            if (OnProgress != null)
            {
                OnProgress((current + obj) / total);
            }
        }

        public void StopLoad()
        {

        }
    }
}
