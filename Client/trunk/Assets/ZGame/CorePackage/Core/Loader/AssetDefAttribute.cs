using UnityEngine;
using System.Collections;
using System;

namespace Loader
{
    /// <summary>
    /// 用来对不同类型的资源进行配置
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class AssetDefAttribute : Attribute
    {
        public string path;
        public AssetDefAttribute(string path = "")
        {
            this.path = path;
        }
    }
}