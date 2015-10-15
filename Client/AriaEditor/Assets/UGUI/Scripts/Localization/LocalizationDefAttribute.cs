using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    /// <summary>
    /// 描述配置文件
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class LocalizationDefAttribute : System.Attribute
    {
        public string path;
        public int index;
        public string dataName;
        public string localizeType;
        public int offset;

        public LocalizationDefAttribute(string xlsPath, int xlsIndex, string dataName, string localizeType, int offset)
        {
            this.path = xlsPath;
            this.index = xlsIndex;
            this.dataName = dataName;
            this.localizeType = localizeType;
            this.offset = offset;
        }
    }

    /// <summary>
    /// 模板类型描述文件
    /// </summary>
    public enum LocalizationTypeDef
    {
        [LocalizationDef("Localization", 0, "LocalizationData", "cn", 1)]
        LocalizationDictionaryZHCN,
        [LocalizationDef("Localization", 0, "LocalizationData", "en", 2)]
        LocalizationDictionaryENUS,
    }
}