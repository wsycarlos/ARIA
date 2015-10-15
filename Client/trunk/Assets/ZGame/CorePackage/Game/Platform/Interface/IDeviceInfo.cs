
public interface IDeviceInfo
{
    /// <summary>
    /// 语言版本类型
    /// </summary>
    /// <returns></returns>
    string GetLocaltype();

    /// <summary>
    /// 客户端版本
    /// </summary>
    /// <returns></returns>
    string clientVersion { set; get; }

    /// <summary>
    /// 终端设备分类，全小写。 枚举为：pc，ios，android
    /// </summary>
    /// <returns></returns>
    string GetDevice();
    /// <summary>
    /// 设备型号，例如：ipad/iphone/htc，全小写
    /// </summary>
    /// <returns></returns>
    string GetDeviceType();
    /// <summary>
    /// 操作系统版本，全小写
    /// </summary>
    /// <returns></returns>
    string GetDeviceVersion();
    /// <summary>
    /// 设备唯一识别id，md5( )，32位小写
    /// </summary>
    /// <returns></returns>
    string GetDeviceGuid();
    /// <summary>
    /// 设备MAC地址，去冒号，全小写
    /// </summary>
    /// <returns></returns>
    string GetDeviceMac();
    /// <summary>
    /// 设备IMEI串号，全小写
    /// </summary>
    /// <returns></returns>
    string GetDeviceImei();
    /// <summary>
    /// 设备User-Agent标识
    /// </summary>
    /// <returns></returns>
    string GetDeviceUserAgent();
    /// <summary>
    /// 设备接入网络环境，全小写，例如：wifi，3g，2g，wwan
    /// </summary>
    /// <returns></returns>
    string GetDviceConnectType();
    /// <summary>
    /// IOS设备属性：是否越狱 1越狱 0未越狱 -1未知
    /// </summary>
    /// <returns></returns>
    int GetDeviceJailbroken();
    /// <summary>
    /// 渠道编号
    /// </summary>
    /// <returns></returns>
    string GetChannelCode();
    /// <summary>
    /// 如果是渠道用户登陆，请记录对应的渠道用户ID
    /// </summary>
    /// <returns></returns>
    string GetChannelUserid();
    /// <summary>
    /// 推广码，即F值
    /// </summary>
    /// <returns></returns>
    string GetPromotionCode();
    /// <summary>
    /// cpp版本
    /// </summary>
    /// <returns></returns>
    string GetCppVersion();
    /// <summary>
    /// res版本
    /// </summary>
    /// <returns></returns>
    string GetResVersion();
    /// <summary>
    /// appType
    /// </summary>
    /// <returns></returns>
    string GetAppType();
    /// <summary>
    /// 语言
    /// </summary>
    /// <returns></returns>
    int GetLanguage();
    /// <summary>
    /// 检测系统是否允许访问麦克风
    /// </summary>
    /// <returns></returns>
    bool CheckMirophone();
    /// <summary>
    /// ios使用，判断是否是ios8
    /// </summary>
    /// <returns></returns>
    bool IsIOS8();
}