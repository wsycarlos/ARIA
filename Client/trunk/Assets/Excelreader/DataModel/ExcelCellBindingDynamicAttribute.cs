using UnityEngine;
using System.Collections;
using System;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class ExcelCellBindingDynamicAttribute : Attribute
{
    public int index;

    public ExcelCellBindingDynamicAttribute(int index)
    {
        this.index = index;
    }
}
