
using System;
using System.Collections.Generic;
using Loader;
using UnityEngine;
using ZGame;

namespace Game.UI
{
    public enum UILayer
    {
        Control = 0,
        Scene = 1,
        Face = 2,
        Panel = 3,
        Popup = 4,
        Tips = 5,
        Tips2 = 6,
        Tips3 = 7,
        Guide,//= 7,
        Waiting,// = 8,
        Other, //=9,
        Top,//= 10,
    }

    /// <summary>
    /// 功能概述：
    /// UI管理器，负责管理面板UI
    /// </summary>
    public class UIManager
    {
        //对应ui
        private Dictionary<string, GameObject> uiDic;
        private Dictionary<UILayer, GameObject> curUI;
        private AssetBundle uiBundle;
        private UIConfigDictionary uiConfig;

        private static UIManager instance;

        public static UIManager Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new UIManager();
                }
                return instance;
            }
        }

        public UIManager()
        {
            Init();
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        public void Init()
        {
            uiBundle = AssetBundleManager.GetBundle("ui", AssetBundleType.UI);
            if (uiBundle == null)
            {
                D.error("[UIManager]uiBundle未载入,无法创建面板!!");
            }
            AssetBundle configBundle = AssetBundleManager.GetBundle("data", AssetBundleType.DATA);

            if (configBundle.Contains("uiConfig"))
            {
                uiConfig = configBundle.LoadAsset<UIConfigDictionary>("uiConfig");
            }
            if (uiConfig == null)
            {
                D.error("[UIManager]uiConfig未载入,无法创建面板!!");
            }

            uiDic = new Dictionary<string, GameObject>();
            curUI = new Dictionary<UILayer, GameObject>();
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                curUI.Add(layer, null);
            }

            UIFactory.Instance.Init(uiBundle, uiConfig);
        }

        public GameObject ShowUI(string uiname, object args, UILayer layer)
        {
            GameObject go = GetUI(uiname, layer);
            if (go == null)
            {
                D.error("--------UI-not-found---------");
            }

            if (curUI[layer] != go)
            {
                if (curUI[layer] != null)
                    curUI[layer].GetComponent<BaseUI>().Hide();
                curUI[layer] = go;
            }
            //用来刷新数据
            curUI[layer].GetComponent<BaseUI>().Show(args);

            return curUI[layer];
        }

        public void HideUI(UILayer layer, string panelType = "")
        {
            if (curUI != null && curUI[layer] != null)
            {
                //当指定panel名称时，如果当前显示不是指定panel，则不执行隐藏操作
                if (panelType != "" && panelType != curUI[layer].name)
                {
                    return;
                }
                D.log("[UIManager]:HideUI" + curUI[layer].name);
                curUI[layer].GetComponent<BaseUI>().Hide();
            }
        }

        /// <summary>
        /// 从Prefab创建UI
        /// </summary>
        /// <param name="type">面板类型</param>
        /// <param name="LogicType">逻辑类</param>
        /// <param name="layer">层级</param>
        /// <returns></returns>
        public GameObject GetUI(string type, UILayer layer)
        {
            GameObject go = null;
            if (uiDic.ContainsKey(type))
            {
                go = uiDic[type];
            }
            else
            {
                if (layer != UILayer.Control)
                {
                    go = UIFactory.Instance.CreateGameObject(type);
                    go.name = type;
                    uiDic[type] = go; //创建完成后设置出事状态
                    SetUILayer(go, layer);
                }
                else
                {
                    //如果是控件则不需要缓存，需要反复创建
                    go = UIFactory.Instance.CreateGameObject(type);//, "", null, true缓存这里目前不好用

                }
            }
            return go;
        }

        /// <summary>
        /// 设置对应UI的深度值
        /// </summary>
        /// <param name="go">ui</param>
        /// <param name="layer">所在层</param>
        private void SetUILayer(GameObject go, UILayer layer)
        {
            Canvas canvas = go.GetComponent<Canvas>();
            if (null != canvas)
            {
                canvas.sortingOrder = (int)layer;
            }
        }
    }
}
