
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class ExcelCellBindingAttribute : System.Attribute
{
    public int offset;
    
    public ExcelCellBindingAttribute(int t)
    {
        offset = t;
    }
}
