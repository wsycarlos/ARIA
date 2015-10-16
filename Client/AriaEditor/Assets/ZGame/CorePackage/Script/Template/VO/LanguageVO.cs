/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/18/2014 7:45:13 PM
* 文件名：LanguageVO
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
