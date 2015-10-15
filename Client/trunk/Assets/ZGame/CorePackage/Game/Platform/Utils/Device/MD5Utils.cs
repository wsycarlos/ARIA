
using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 功能概述：
/// </summary>
public class MD5Utils
{
    public static string GetMD5Hash(String input)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString().ToLower();
    }
}
