
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class ExtensionConfig : MonoBehaviour
    {
        /// <summary>
        /// 外部代码的dll名字
        /// </summary>
        public string extensionDllName;
        /// <summary>
        /// 外部代码用于调试时的pdb名字，正式版留空
        /// </summary>
        public string extensionPdbName;

        private static ExtensionConfig _instance;

        public static ExtensionConfig Instance
        {
            get { return _instance; }
        }

        protected ExtensionConfig()
        {

        }

        void Awake()
        {
            _instance = this;
        }
    }
}
