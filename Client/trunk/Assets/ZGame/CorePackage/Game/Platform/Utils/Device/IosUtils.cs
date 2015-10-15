
using System.Runtime.InteropServices;
/// <summary>
/// 功能概述：
/// </summary>
public class IosUtils : IDeviceInfo
{
    [DllImport("__Internal")]
    private static extern string _getLocaltype();
    [DllImport("__Internal")]
    private static extern string _getClientVersion();
    [DllImport("__Internal")]
    private static extern string _getDeviceType();
    [DllImport("__Internal")]
    private static extern string _getDeviceVersion();
    [DllImport("__Internal")]
    private static extern string _getDeviceGuid();
    //[DllImport("__Internal")]
    //private static extern string _getDeviceMac();
    [DllImport("__Internal")]
    private static extern string _getDeviceImei();
    //[DllImport("__Internal")]
    //private static extern string _getDeviceUserAgent();
    //[DllImport("__Internal")]
    //private static extern string _getDviceConnectType();
    [DllImport("__Internal")]
    private static extern int _getDeviceJailbroken();
    [DllImport("__Internal")]
    private static extern string _getChannelCode();
    [DllImport("__Internal")]
    private static extern string _getChannelUserid();
    [DllImport("__Internal")]
    private static extern string _getPromotionCode();
    [DllImport("__Internal")]
    private static extern string _getCppVersion();
    [DllImport("__Internal")]
    private static extern string _getResVersion();
    [DllImport("__Internal")]
    private static extern string _getAppType();
    [DllImport("__Internal")]
    private static extern int _getLanguage();

    [DllImport("__Internal")]
    private static extern bool _checkMicrophone();

    [DllImport("__Internal")]
    private static extern bool _ios8Check();

    private static IDeviceInfo _instance;
    public static IDeviceInfo GetInstance()
    {
        if (null == _instance)
        {
            _instance = new IosUtils();
        }

        return _instance;
    }

    /// <summary>
    /// 设备语言类型 en zh-han jp
    /// </summary>
    /// <returns>The localtype.</returns>
    public string GetLocaltype()
    {
        return _getLocaltype();
    }

    /// <summary>
    /// 获取当前客户端版本号，读取的是plist中的配置版本号
    /// </summary>
    /// <returns>The client version.</returns>
    public string clientVersion
    {
        get
        {
            return _getClientVersion();
        }
        set
        {

        }
    }

    /// <summary>
    /// 终端设备分类， 具体区分是pc ios android
    /// </summary>
    /// <returns>The device.</returns>
    public string GetDevice()
    {
        return DeviceDef.DeviceType.IOS.ToString().ToLower();
    }

    /// <summary>
    /// 设备型号，例如：ipad/iphone/htc，全小写
    /// </summary>
    /// <returns></returns>
    public string GetDeviceType()
    {
        return _getDeviceType().ToLower();
    }

    /// <summary>
    /// 操作系统版本，全小写
    /// </summary>
    /// <returns></returns>
    public string GetDeviceVersion()
    {
        return _getDeviceVersion().ToLower();
    }

    /// <summary>
    /// 设备唯一识别id，md5( )，32位小写
    /// </summary>
    /// <returns></returns>
    public string GetDeviceGuid()
    {
        return MD5Utils.GetMD5Hash(_getDeviceGuid());
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
        //return _getDeviceUserAgent();
    }

    public string GetDviceConnectType()
    {
        return "";
    }

    public int GetDeviceJailbroken()
    {
        return _getDeviceJailbroken();
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
        return "";
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
        return _checkMicrophone();
    }

    public bool IsIOS8()
    {
        return _ios8Check();
    }
}
