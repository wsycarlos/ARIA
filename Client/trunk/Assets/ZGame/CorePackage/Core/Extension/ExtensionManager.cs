
using System;
using CLRSharp;
using Game.UI;
using Loader;
using UnityEngine;
using System.IO;
using Thrift.Protocol;
using ZGame;


namespace Game.Core
{
    /// <summary>
    /// 负责初始化动态更新相关的环境
    /// </summary>
    public class ExtensionManager : MonoBehaviour
    {
        static ExtensionManager mInstance = null;

        private CLRSharp_Environment _env;
        private TextAsset _dll;
        private TextAsset _pdb;

        private MemoryStream _msDll;
        private MemoryStream _msPdb;
        private AssetBundle _ab;

        public static ExtensionManager instance
        {
            get { return mInstance; }
        }

        public CLRSharp_Environment env
        {
            get { return _env; }
        }

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            Init();
            mInstance = this;
        }


        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Show"))
            {
                //long timeBegin = DateTime.Now.Ticks;
                //Debug.Log("fuck test begin " + timeBegin);
                ////for (int i = 0; i < 100000; i++)
                //{
                //    TProtocol protocol = ActionManager.Instance.GetConnect();
                //    TestThrift.Client client = new TestThrift.Client(protocol);
                //    client.GetHuman(new HumanVO() { Id = 10001, Name = "testthrift", Player = new Player() { Id = 100001, Name = "Player", Cash = 10000, Gold = 100000, Uuid = 1000000000012 } });
                //}

                //Debug.Log("fuck test end " + (DateTime.Now.Ticks - timeBegin));
                UIManager.Instance.ShowUI("TestPanel", null, UILayer.Panel);
            }

            if (GUI.Button(new Rect(130, 10, 100, 50), "Hide"))
            {
                UIManager.Instance.HideUI(UILayer.Panel);
            }
        }

        void OnDestroy()
        {
            mInstance = null;
        }

        private void Init()
        {
            _ab = AssetBundleManager.GetBundle("extends", AssetBundleType.EXTENDS);

            if (null == _ab)
            {
                D.error("Script Bundle initialized fail!");
                return;
            }

            //创建CLRSharp环境
            _env = new CLRSharp_Environment(new Logger());

            //加载L#模块
            string extensionDllName = ExtensionConfig.Instance.extensionDllName;
            string extensionPdbName = ExtensionConfig.Instance.extensionPdbName;
            if (string.IsNullOrEmpty(extensionDllName))
            {
                D.error("L# Extension dll name can not be empty!");
                return;
            }

            _dll = _ab.LoadAsset<TextAsset>(ExtensionConfig.Instance.extensionDllName);

            if (!string.IsNullOrEmpty(extensionPdbName))
            {
                _pdb = _ab.LoadAsset<TextAsset>(ExtensionConfig.Instance.extensionPdbName);
            }

            if (null == _dll)
            {
                D.error("L# Extension Manager Failed to initialized!");
                return;
            }

            if (null != _pdb)
            {
                _msPdb = new MemoryStream(_pdb.bytes);
            }

            _msDll = new MemoryStream(_dll.bytes);
            _env.LoadModule(_msDll, _msPdb, new Mono.Cecil.Pdb.PdbReaderProvider());//Pdb
        }
    }

    public class Logger : CLRSharp.ICLRSharp_Logger//实现L#的LOG接口
    {
        public void Log(string str)
        {
            D.log(str);
        }

        public void Log_Error(string str)
        {
            D.error(str);
        }

        public void Log_Warning(string str)
        {
            D.warn(str);
        }
    }
}