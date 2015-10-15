
using Constants;
using Game.Platform;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using JenkinBuild;
using UnityEngine;


/// <summary>
/// 功能概述：
/// 管理服务器列表
/// </summary>
public class ServerListManager : MonoBehaviour
{
    public System.Action<bool> OnServerStatusChange;

    private static ServerListManager _instance;

    public static ServerListManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ServerManager");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<ServerListManager>();
            }

            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    /// <summary>
    /// 获取服务器列表
    /// </summary>
    /// <returns></returns>
    public List<Server> GetServerList(string clientVersion)
    {
        if (null == setting || null == setting.server)
        {
            return null;
        }
        return new List<Server>(setting.server); ;//new List<Server>(setting.server);//.FindAll(i => { return VersionUtil.Compare(i.requestClientVersion, clientVersion); });
    }

    public Server GetServeyByID(int id, string clientVersion)
    {
        if (null == GetServerList(clientVersion))
        {
            return null;
        }
        return GetServerList(clientVersion).Find(i => { return id == i.id; });
    }

    public Server GetDefaultServer(string clientVersion)
    {
        if (null == GetServerList(clientVersion))
        {
            return null;
        }
        return GetServerList(clientVersion)[0];
    }

    public App GetAppInfo()
    {
        if (null != setting.app && setting.app.Length > 0)
        {
            return setting.app[0];
        }
        return null;
    }

    static Settings setting;

    public static Settings Setting
    {
        get { return setting; }
        set { setting = value; }
    }

    IEnumerator Start()
    {
        D.log("Start to download server list");

        if (setting != null)
        {
            //FIXME 
            ServerChange(true);
            yield break;
        }
        //WWW请求服务器配置文件
        string url = GetSettingsUrl();


        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error) || string.IsNullOrEmpty(www.text))
        {
            D.log(www.error);
            ServerChange(false);
        }
        else
        {
            setting = (Settings)ConfigXmlSerializer.DeserializeFromString(www.text, typeof(Settings));
            ServerChange(true);
        }
        www.Dispose();
    }

    private void ServerChange(bool flag)
    {
        if (null != OnServerStatusChange)
        {
            OnServerStatusChange(flag);
        }
    }

    internal void ReGet()
    {
        setting = null;
        StartCoroutine(Start());
    }

    public bool NeedUpdataClient(string currentVersion)
    {
        return !currentVersion.Equals(setting.app[0].version.versionId) && setting.app[0].forceUpdate;
    }

    public bool ServersEmpty(string clientVersion)
    {
        return null == GetServerList(clientVersion) || GetServerList(clientVersion).Count <= 0;
    }

    public Server GetLatestServer()
    {
        IDeviceInfo deviceInfo = DeviceUtils.GetDeviceInfo();
        int serverID = PlayerPrefs.GetInt("serverid", -1);
        if (serverID > 0)
        {
            return GetServeyByID(serverID, deviceInfo.clientVersion);
        }
        else
        {
            return GetDefaultServer(deviceInfo.clientVersion);
        }
    }



    internal ResServer GetResouceServer()
    {
        if (setting == null || setting.resServer == null)
        {
            D.error("resources server is not find");
            return null;
        }
        //
        int i = 0;
        ResServer rs = setting.resServer[i];
        while (rs.serverState == 0)
        {
            if (i >= setting.resServer.Length)
            {
                break;
            }
            rs = setting.resServer[++i];
        }

        return rs;
    }

    private string GetSettingsUrl()
    {
        return JenkinsBuildConfig.Instance.serverurl;
    }
}
