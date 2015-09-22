
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class ExcelRowMappingAttribute : System.Attribute
{
    public string valstr;
    
    public ExcelRowMappingAttribute(string val)
    {
        valstr = val;
    }
}
