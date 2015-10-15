/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：3/31/2015 2:26:04 PM
* 文件名：UIAutoFindAttribute
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/



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
