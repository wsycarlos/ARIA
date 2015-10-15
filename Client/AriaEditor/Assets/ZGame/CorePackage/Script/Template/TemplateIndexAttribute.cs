/// <summary>
/// 功能概述：
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class TemplateIndexAttribute : System.Attribute
{
    public int index;

    public TemplateIndexAttribute(int t)
    {
        index = t;
    }
}
