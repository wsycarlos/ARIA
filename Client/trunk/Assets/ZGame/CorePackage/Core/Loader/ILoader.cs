using UnityEngine;
using System.Collections;
using System;

namespace Loader
{
    /// <summary>
    /// 加载器，用来加载各种资源
    /// </summary>
    public interface ILoader 
    {
        event Action<float> OnProgress;
        event Action<ILoader> OnComplete;

        object LoadedAsset { get; }
        String AssetName { get; }

        void Init(string assetName,int version = 1);
        void Update();
        void StartLoad();
        void StopLoad();
    }
}
