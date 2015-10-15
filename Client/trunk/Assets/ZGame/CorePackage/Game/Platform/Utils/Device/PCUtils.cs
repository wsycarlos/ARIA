
/// <summary>
/// 功能概述：
/// </summary>

public class PCUtils : IDeviceInfo
{
    private static IDeviceInfo _instance;
    public static IDeviceInfo GetInstance()
    {
        if (null == _instance)
        {
            _instance = new PCUtils();
        }

        return _instance;
    }

    /// <summary>
    /// 设备语言类型 en zh-han jp
    /// </summary>
    /// <returns>The localtype.</returns>
    public string GetLocaltype()
    {
        string localType = System.Globalization.CultureInfo.InstalledUICulture.Name;
        return "zh-Hans";
    }

    /// <summary>
    /// 获取当前客户端版本号，读取的是plist中的配置版本号
    /// </summary>
    /// <returns>The client version.</returns>
    public string clientVersion
    {
        get { return "1.0.1"; }
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
        return DeviceDef.DeviceType.PC.ToString().ToLower();
    }

    /// <summary>
    /// 设备型号，例如：ipad/iphone/htc，全小写
    /// </summary>
    /// <returns></returns>
    public string GetDeviceType()
    {
        return "";
    }

    /// <summary>
    /// 操作系统版本，全小写
    /// </summary>
    /// <returns></returns>
    public string GetDeviceVersion()
    {
        return "";
    }

    /// <summary>
    /// 设备唯一识别id，md5( )，32位小写
    /// </summary>
    /// <returns></returns>
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
        return -1;
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
        return true;
    }

    public bool IsIOS8()
    {
        return false;
    }
}