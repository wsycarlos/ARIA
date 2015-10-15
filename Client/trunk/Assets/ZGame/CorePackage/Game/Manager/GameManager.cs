
using Game.Core;

namespace Game.Manager
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class GameManager : ExtensionScripter
    {
        public override void Awake()
        {
            _extensionName = "Game.Proxy.GameProxy";
            extensionName = null;

            base.Awake();
        }

        public void GameStart()
        {
            //初始化网络相关
            gameObject.AddComponent<MessageManager>();
            //启动倒计时模拟器
            gameObject.AddComponent<SimulationManager>();
            //初始化多语言环境
            gameObject.AddComponent<LocalizationManager>();
            gameObject.AddComponent<Net.NetWorkManager>();
            //初始化UGUIBundle
            //UGUIUtils.bundle = AssetBundleManager.Instance.LoadBundleAsset("", AssetBundleType.UI);
            //初始化完成，通知外部代码启动
            MemberCall("GameStart", methodctor);
        }
    }
}
