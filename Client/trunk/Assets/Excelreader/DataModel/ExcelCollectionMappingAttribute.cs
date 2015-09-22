
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class ExcelCollectionMappingAttribute : System.Attribute
{
    public System.Type type;
    public string valstr;

    public ExcelCollectionMappingAttribute(System.Type t, string val)
    {
        type = t;
        valstr = val;
    }
}
