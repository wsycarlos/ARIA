using UnityEngine;
using System.Collections;

namespace Loader
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum AssetBundleType
    {
        NONE = -1,
        [AssetDef("ui")]
        UI,
        [AssetDef("data")]
        DATA,
        [AssetDef("extends")]
        EXTENDS,
    }
}