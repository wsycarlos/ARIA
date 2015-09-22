/* ========================================================================
* Copyright © 2013-2014, HOD Studio. All rights reserved.
*
* 作  者：HanXusheng 时间：12/16/2014 8:03:35 PM
* 文件名：UguiMathUtils
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
public class UguiMathUtils
{
    public static void Normalize(float v, out int vector)
    {
        if (v > 0.75f)
        {
            vector = 2;
        }
        else if (v < 0.25f)
        {
            vector = 0;
        }
        else
        {
            vector = 1;
        }
    }

    public static String DecimalToBinaryString(int data, int length = 2)
    {
        return Convert.ToString(data, 2).PadLeft(length, '0');
    }

    public static int BinaryStringToDecimal(string data, int baseValie)
    {
        try
        {
            return Convert.ToUInt16(data, baseValie);
        }
        catch (Exception e)
        {
            throw (e);
        }

        return 0;
    }
}
