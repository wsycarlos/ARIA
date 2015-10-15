
using Game.Core;

namespace Game.Manager
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class SimulationManager : ExtensionScripter
    {
        public override void Start()
        {
            InvokeRepeating("DoCountDown", 0.5f, 1f);
        }

        public override void Awake()
        {
            _extensionName = "Game.Proxy.SilulationProxy";
            extensionName = null;
            base.Awake();
        }

        void DoCountDown()
        {
            MemberCall("DoCountDown", methodctor);
        }
    }
}

