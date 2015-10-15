﻿using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// 描述配置文件
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class TemplateDefAttribute : System.Attribute
{
    public string path;
    public int index;
    public string dataName;
    public Type dataType;
    public Type dicType;

    public TemplateDefAttribute(string xlsPath, int xlsIndex, string dataName, Type dataType, Type dicType = null)
    {
        this.path = xlsPath;
        this.index = xlsIndex;
        this.dataName = dataName;
        this.dataType = dataType;
        this.dicType = dicType;
    }
}