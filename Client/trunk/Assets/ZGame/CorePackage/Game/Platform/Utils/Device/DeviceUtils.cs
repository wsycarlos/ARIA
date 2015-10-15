
/// <summary>
/// 功能概述：
/// </summary>
public class DeviceUtils
{
    public static IDeviceInfo GetDeviceInfo()
    {
        IDeviceInfo deviceInfo = null;
#if UNITY_IPHONE
        deviceInfo = IosUtils.GetInstance();
#elif UNITY_ANDROID
        deviceInfo = AndroidUtils.GetInstance();
#elif UNITY_STANDALONE_WIN
        deviceInfo = PCUtils.GetInstance();
#elif UNITY_STANDALONE_OSX
        deviceInfo = MacUtils.GetInstance();
#endif
        return deviceInfo;
    }
}
