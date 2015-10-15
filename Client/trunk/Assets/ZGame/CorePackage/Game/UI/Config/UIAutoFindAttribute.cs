
namespace Game.UI
{
    /// <summary>
    /// 功能概述：
    /// UI逻辑类中使用的标注
    /// 标注的对象会在逻辑类初始化时会自动查找并赋值为实例化UI中相同名称的对象
    /// </summary
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class UIAutoFindAttribute : System.Attribute
    {
    }
}
