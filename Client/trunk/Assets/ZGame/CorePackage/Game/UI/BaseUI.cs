
using CLRSharp;
using Game.Core;

namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class BaseUI : ExtensionScripter
    {
        public void Show(object args)
        {
            MemberCall("Show", methodctor, MethodParamList.Make(env.GetType(typeof(object))), new object[] { args });
        }

        public void Hide()
        {
            MemberCall("Hide", methodctor);
        }
    }
}
