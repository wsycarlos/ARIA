
using System;
using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class AndroidUtils : IDeviceInfo
{
    private static IDeviceInfo _instance;
    public static IDeviceInfo GetInstance()
    {
        if (null == _instance)
        {
            _instance = new AndroidUtils();
        }

        return _instance;
    }

    public void JavaCall(string methodName, params object[] data)
    {
#if UNITY_ANDROID

        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call(methodName, data);
        }
        catch (Exception e)
        {
        }

#endif
    }

    public string GetLocaltype()
    {
        return "zh-Hans";
    }

    private string _clientVersion;
    public string clientVersion
    {
        get { return _clientVersion; }
        set { _clientVersion = value; }
    }

    public string GetDevice()
    {
        return DeviceDef.DeviceType.ANDROID.ToString().ToLower();
    }

    public string GetDeviceType()
    {
        return "";
    }

    public string GetDeviceVersion()
    {
        return "";
    }

    public string GetDeviceGuid()
    {
        return "";
    }

    public string GetDeviceMac()
    {
        return "";
    }

    public string GetDeviceImei()
    {
        return "";
    }

    public string GetDeviceUserAgent()
    {
        return "";
    }

    public string GetDviceConnectType()
    {
        return "";
    }

    public int GetDeviceJailbroken()
    {
        return 0;
    }

    public string GetChannelCode()
    {
        return "";
    }

    public string GetChannelUserid()
    {
        return "";
    }

    public string GetPromotionCode()
    {
        return "";
    }

    public string GetCppVersion()
    {
        return ""; ;
    }

    public string GetResVersion()
    {
        return "";
    }

    public string GetAppType()
    {
        return "";
    }

    public int GetLanguage()
    {
        return -1;
    }

    public bool CheckMirophone()
    {
        return true;
    }

    public bool IsIOS8()
    {
        return false;
    }
}