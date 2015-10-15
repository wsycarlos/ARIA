using UnityEngine;
using System.Collections;
using System;

namespace Loader
{
    public class SceneLoader : ILoader
    {
        private string assetName;
        private int version;
        private AsyncOperation async;

        #region 接口实现

        public event System.Action<float> OnProgress;

        public event System.Action<ILoader> OnComplete;

        /// <summary>
        /// 场景加载结束，自动进入，不需要保存场景引用，也无法保留
        /// </summary>
        public object LoadedAsset
        {
            get { throw new InvalidOperationException("scene Loader 无法取出加载场景"); }
        }

        public string AssetName
        {
            get { return assetName; }
        }

        public void Init(string assetName, int version = 1)
        {
            this.assetName = assetName;
            this.version = version;
        }

        public void StartLoad()
        {
            async = Application.LoadLevelAsync(assetName);
        }

        public void StopLoad()
        {
            throw new System.Exception("不能停止该进程");
        }

        #endregion

       public void Update()
        {
            if (async == null)
                return;

            if (async.isDone)
            {
                if (OnComplete != null)
                    OnComplete(this);
            }
            else
            {
                if (OnProgress != null)
                    OnProgress(async.progress);
            }

        }
    }
}