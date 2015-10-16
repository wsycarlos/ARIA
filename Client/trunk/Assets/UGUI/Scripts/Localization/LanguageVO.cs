using System;

/// <summary>
/// 功能概述：
/// </summary>
[Serializable]
public class LanguageVO
{
    [ExcelCellBinding(0)]
    public string key;
    [ExcelCellBinding(-1)]
    public string Value;
}
