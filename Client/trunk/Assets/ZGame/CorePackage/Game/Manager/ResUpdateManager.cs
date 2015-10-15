using Constants;
using Game.Platform;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using LitJson;

public class DownloadFileInfo
{
    public string name = "";
    public string path = "";
    public bool isDownloaded = false;
    public int version;
}


public class ResUpdateManager : MonoBehaviour
{

    public WWWProgress progress;
    //public static string resServer;// = "localhost/AssetBundle/";//"192.168.1.3";
    public static string ServerRootUrl
    {
        get
        {
            ResServer server = ServerListManager.instance.GetResouceServer();
            if (null != server)
            {
                return server.serverIp + LocalizationManager.instance.GetLocalizationType().ToString() + "/" +
                       UnityUtil.GetPlatformName() + "/" + server.bundleversion + "/";
            }

            return null;
        }
    }

    private static string _serverRootUrl;

    public static string localRootUrl
    {
        get
        {
#if UNITY_EDITOR
            return Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets")) + "/AssetBundle/" + LocalizationManager.instance.GetLocalizationType().ToString() + "/";
#else
            return Application.streamingAssetsPath + "/";
#endif
        }
    }
    public static string sdcardRootUrl
    {
        get
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            return Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/AssetBundle/" + LocalizationManager.instance.GetLocalizationType().ToString() + "/";
#elif UNITY_IPHONE || UNITY_ANDROID
            return Application.persistentDataPath + "/AssetBundle/";
#endif
            return "";
        }
    }

    List<BundleData> bundles = null;
    List<BundleBuildState> buildStates = null;

    //    BMConfiger bmConfiger = null;
    //    BMUrls bmUrl = null;

    private Dictionary<string, BundleData> bundleDict = null;//new Dictionary<string, BundleData>();
    private Dictionary<string, BundleBuildState> buildStatesDict = null;//new Dictionary<string, BundleBuildState>();

    static ResUpdateManager instance = null;
    static string manualUrl = "";

    string downloadRootUrl = null;
    //    BuildPlatform curPlatform;
    private Action InitOverCallBack = null;

    private List<DownloadFileInfo> downloadedFiles;
    private Dictionary<string, DownloadFileInfo> downloadedFilesDict;// = new Dictionary<string, DownloadFileInfo>();
    private int downloadFileSize;
    public static bool Part2PackageDownloaded = true;
    private List<DownloadFileInfo> part2FileList;
    private int part2FileSize;
    private LocalizationType _type;// 

    public DownloadFileInfo GetDownloadedFileInfo(string name)
    {
        if (downloadedFilesDict != null && downloadedFilesDict.ContainsKey(name) && downloadedFilesDict[name].isDownloaded)
        {
            return downloadedFilesDict[name];
        }
        return null;
    }

    void Awake()
    {
        instance = this;
        progress = FindObjectOfType<WWWProgress>();
        //if (progress == null)
        //    progress = gameObject.AddComponent<WWWProgress>();
        DontDestroyOnLoad(instance.gameObject);
        _type = LocalizationManager.instance.GetLocalizationType();
    }

    /// <summary>
    /// 初始化,读取bmdata列表信息
    /// 执行顺序
    /// LoadLastDownloadList
    /// LoadLocalConfig
    /// LoadServerConfig
    /// DownloadFiles
    /// Part2Check
    /// </summary>
    public void Init(Action callback)
    {
        InitOverCallBack = callback;
        //StartCoroutine(LoadServerList());
        StartCoroutine(LoadLastDownloadList());
    }

    public void InitLocal(Action callback)
    {
        InitOverCallBack = callback;
        //StartCoroutine(LoadLocalConfig(true));
        //StartCoroutine(LoadServerList(true));
        StartCoroutine(LoadLastDownloadList(true));
    }

    /// <summary>
    /// 读取上次下载列表
    /// sdcard中的downlist.txt
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadLastDownloadList(bool skipServer = false)
    {
        WWW downlistWWW = new WWW("file://" + sdcardRootUrl + "downlist.txt");
        if (progress != null)
        {
            progress.StartUpdate(downlistWWW, LocalizationManager.instance.GetValueBtKey("load_downlist"));
        }

        yield return downlistWWW;
        if (progress != null) progress.StopUpdate();
        if (downlistWWW.error != null)
        {
            D.warn(downlistWWW.error);
            D.warn("Last DownloadList NotFound!!!");
        }
        else
        {
            //TODO 出错处理
            downloadedFiles = JsonMapper.ToObject<List<DownloadFileInfo>>(downlistWWW.text);
            downloadedFilesDict = new Dictionary<string, DownloadFileInfo>();
            foreach (var df in downloadedFiles)
            {
                downloadedFilesDict.Add(df.name, df);
            }
        }
        downlistWWW.Dispose();
        StartCoroutine(LoadLocalConfig(skipServer));
    }


    string getWWWUrl(string path)
    {
        string url = "";
        if (path.Contains("://"))
        {
            url = path;
        }
        else
        {
            url = "file://" + path;
        }
        return url;
    }

    /// <summary>
    /// 读取本包里的BM.Data
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadLocalConfig(bool skipServer = false)
    {
        const string verNumKey = "BMDataVersion";
        string bmDataUrl = getWWWUrl(localRootUrl + UnityUtil.GetPlatformName() + "/BM.data");
        int lastBMDataVersion = 0;
        if (PlayerPrefs.HasKey(verNumKey))
            lastBMDataVersion = PlayerPrefs.GetInt(verNumKey);

        // Download and cache new data version
        WWW initDataWWW;

        initDataWWW = WWW.LoadFromCacheOrDownload(bmDataUrl, lastBMDataVersion + 1);
        D.log("start load BM.data from local url. " + bmDataUrl);
        if (progress != null)
        {
            progress.StartUpdate(initDataWWW, LocalizationManager.instance.GetValueBtKey("load_config"));
        }

        yield return initDataWWW;
        if (progress != null) progress.StopUpdate();
        if (initDataWWW.error != null)
        {
            initDataWWW.Dispose();
            D.log("Cannot load BM.data from local url. Try load it from cache.");

            initDataWWW = WWW.LoadFromCacheOrDownload(bmDataUrl, lastBMDataVersion);
            yield return initDataWWW;

            if (initDataWWW.error != null)
            {
                D.error("Try load BM.data from sdcard.\nError: " + initDataWWW.error);
                //yield break;
                //尝试从sd卡中读取
                bmDataUrl = "file://" + sdcardRootUrl + UnityUtil.GetPlatformName() + "/BM.data";
                initDataWWW = WWW.LoadFromCacheOrDownload(bmDataUrl, lastBMDataVersion + 1);
                yield return initDataWWW;
                if (initDataWWW.error != null)
                {
                    D.error("load BM.data failed.\nError: " + initDataWWW.error);
                    //本地读取失败
                }
            }
        }
        else
        {
            D.log("lastBMDataVersion add , ver = " + lastBMDataVersion);
            // Update cache version number
            PlayerPrefs.SetInt(verNumKey, lastBMDataVersion + 1);

        }

        if (initDataWWW.error == null)
        {
            InitBMData(initDataWWW);
            initDataWWW.assetBundle.Unload(true);
        }

        initDataWWW.Dispose();

        if (skipServer)
        {
            StartCoroutine(Part2Check());
        }
        else
            StartCoroutine(LoadServerConfig());

    }

    void InitBMData(WWW initDataWWW)
    {
        TextAsset ta = initDataWWW.assetBundle.LoadAsset<TextAsset>("BundleData_" + LocalizationManager.instance.GetLocalizationType().ToString());

        //兼容
        if (null == ta)
        {
            ta = initDataWWW.assetBundle.LoadAsset<TextAsset>("BundleData");
        }
        bundles = JsonMapper.ToObject<List<BundleData>>(ta.text);

        if (bundleDict == null)
            bundleDict = new Dictionary<string, BundleData>();
        foreach (var bundle in bundles)
            bundleDict.Add(bundle.name, bundle);

        // Build States
        ta = initDataWWW.assetBundle.LoadAsset<TextAsset>("BuildStates_" + LocalizationManager.instance.GetLocalizationType().ToString());

        //兼容
        if (null == ta)
        {
            ta = initDataWWW.assetBundle.LoadAsset<TextAsset>("BuildStates");
        }
        buildStates = JsonMapper.ToObject<List<BundleBuildState>>(ta.text);
        if (buildStatesDict == null)
            buildStatesDict = new Dictionary<string, BundleBuildState>();
        foreach (var buildState in buildStates)
            buildStatesDict.Add(buildState.bundleName, buildState);

        //downlist中过期的文件过滤
        if (downloadedFiles != null)
        {
            for (int i = downloadedFiles.Count - 1; i >= 0; i--)
            {
                var df = downloadedFiles[i];
                if (buildStatesDict.ContainsKey(df.name) &&
                    //防止分包下载的版本号作怪
                    CheckFileExistLocalRoot(df.path + "/" + df.name) &&
                    buildStatesDict[df.name].version != df.version)
                {
                    downloadedFiles.RemoveAt(i);
                    downloadedFilesDict.Remove(df.name);
                    //TODO 删除文件?
                    D.warn("df.name download file is old.");
                }
            }
        }
    }

    void OnLoadServerConfigFailedYes()
    {
        StartCoroutine(LoadServerConfig());
    }

    void OnLoadServerConfigFailedNo()
    {
        if (progress != null) progress.SetText("Skip Load Server Config File,Try To Start Game ...");
        //Debug.Log("Skip Load Server Config File,Try To Start Game ...");
        DoInitOverCallBack();
    }
    void ExitGame()
    {
        if (progress != null) progress.SetText("Exiting Game ...");
        Application.Quit();
    }

    void DoInitOverCallBack()
    {
        if (!ConfigLoaded)
        {
            Messenger.Broadcast<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, "Config Load All Failed !Try Again?", OnLoadServerConfigFailedYes, ExitGame);
            return;
        }
        if (InitOverCallBack != null)
            InitOverCallBack();
    }

    /// <summary>
    /// 读取res server上的BM.Data
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadServerConfig()
    {
        //下载服务器列表对比是否有新资源下载

        WWW initDataWWWServer;
        initDataWWWServer = new WWW(ServerRootUrl + "BM.data");

        if (progress != null)
        {
            progress.StartUpdate(initDataWWWServer, LocalizationManager.instance.GetValueBtKey("load_server_config"));
        }
        yield return initDataWWWServer;
        if (progress != null) progress.StopUpdate();
        if (initDataWWWServer.error != null)
        {
            //TODO 下载服务端 bmdata失败处理
            D.error(initDataWWWServer.error);
            D.error("Download Failed " + initDataWWWServer.url);
            if (progress != null) progress.SetText("  Download Config Failed !");
            if (progress != null) progress.SetProgress(1f);
            Messenger.Broadcast<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, "Download Config Failed !Try Again?", OnLoadServerConfigFailedYes, OnLoadServerConfigFailedNo);
            yield break;
        }
        else
        {
            //保存BMData以备用
            RecordFile(initDataWWWServer.bytes, UnityUtil.GetPlatformName() + "/BM.Data");
            if (downloadedFiles == null)
            {
                downloadedFiles = new List<DownloadFileInfo>();
                downloadedFilesDict = new Dictionary<string, DownloadFileInfo>();
            }
            downloadFileSize = 0;
            //对比列表
            string localizationType = LocalizationManager.instance.GetLocalizationType().ToString();
            TextAsset ta = initDataWWWServer.assetBundle.LoadAsset<TextAsset>("BundleData_" + localizationType);
            List<BundleData> serverBundles = JsonMapper.ToObject<List<BundleData>>(ta.text);
            Dictionary<string, BundleData> serverBundlesDic = new Dictionary<string, BundleData>();
            foreach (var bundle in serverBundles)
            {
                serverBundlesDic.Add(bundle.name, bundle);
            }

            // Build States
            ta = initDataWWWServer.assetBundle.LoadAsset<TextAsset>("BuildStates_" + localizationType);
            List<BundleBuildState> serverbuildStates = JsonMapper.ToObject<List<BundleBuildState>>(ta.text);
            Dictionary<string, BundleBuildState> serverBuildStatesDict = new Dictionary<string, BundleBuildState>();
            foreach (var sbuildState in serverbuildStates)
            {
                serverBuildStatesDict.Add(sbuildState.bundleName, sbuildState);

                if (downloadedFilesDict.ContainsKey(sbuildState.bundleName))
                {
                    //之前下载过
                    if (downloadedFilesDict[sbuildState.bundleName].version != sbuildState.version)
                    {
                        downloadedFilesDict[sbuildState.bundleName].isDownloaded = false;
                        downloadedFilesDict[sbuildState.bundleName].version = sbuildState.version;
                        downloadFileSize += Convert.ToInt32(sbuildState.size);
                    }
                }
                else
                {
                    if (serverBundlesDic.ContainsKey(sbuildState.bundleName))
                    {
                        if (bundleDict != null && buildStatesDict != null
                            && buildStatesDict.ContainsKey(sbuildState.bundleName) &&
                            buildStatesDict[sbuildState.bundleName].version == sbuildState.version
                            && bundleDict.ContainsKey(sbuildState.bundleName))
                        {
                            //本包里面有但不需更新
                            D.log("Skip Bundle " + sbuildState.bundleName + "  current version is " + buildStatesDict[sbuildState.bundleName].version + "/" + sbuildState.version);
                        }
                        else
                        {
                            AddNewDownloadFile(sbuildState.bundleName, serverBundlesDic[sbuildState.bundleName].path, sbuildState.version);
                            downloadFileSize += Convert.ToInt32(sbuildState.size);
                        }
                    }
                    else
                    {
                        D.error("ServerBundlesList may have error!!!---file not contain:" + sbuildState.bundleName);
                    }
                }
            }

            if (bundles == null)
                bundles = serverBundles;
            if (bundleDict == null)
                bundleDict = serverBundlesDic;

            if (buildStates == null)
                buildStates = serverbuildStates;
            if (buildStatesDict == null)
                buildStatesDict = serverBuildStatesDict;

            initDataWWWServer.assetBundle.Unload(true);

        }
        initDataWWWServer.Dispose();
        StartCoroutine(DownloadFiles());
    }

    void AddNewDownloadFile(string name, string path, int ver)
    {
        DownloadFileInfo df = new DownloadFileInfo();
        df.name = name;
        df.path = path;
        df.version = ver;
        downloadedFiles.Add(df);
        downloadedFilesDict.Add(df.name, df);
    }

    void AddNewDownloadFile(DownloadFileInfo df)
    {
        if (downloadedFilesDict.ContainsKey(df.name))
        {
            DownloadFileInfo ab = downloadedFiles.Find(delegate(DownloadFileInfo obj)
            {
                return (obj.name == df.name);
            });

            if (ab != null)
            {
                ab.path = df.path;
                ab.version = df.version;
                ab.isDownloaded = df.isDownloaded;
            }
            D.warn("[RUM]AddNewDownloadFile change " + df.name);
        }
        else
        {
            downloadedFiles.Add(df);
            downloadedFilesDict.Add(df.name, df);
            D.warn("[RUM]AddNewDownloadFile new " + df.name);
        }
    }
    void OnDownloadFilesYes()
    {
        StartCoroutine(DownloadFiles());
    }

    void OnDownloadFilesNo()
    {
        if (progress != null) progress.SetText("Skip Download File,Try To Start Game ...");
        if (InitOverCallBack != null)
            InitOverCallBack();
    }

    /// <summary>
    /// 下载更新文件
    /// </summary>
    /// <returns></returns>
    IEnumerator DownloadFiles()
    {
        if (downloadedFiles != null)
        {
            int i = 1;
            bool newfileD = false;
            UpdateProgressEndStr(downloadFileSize);
            foreach (var file in downloadedFiles)
            {
                if (!file.isDownloaded)
                {
                    WWW www = new WWW(ServerRootUrl + file.path + "/" + file.name + ".bd");
                    if (progress != null)
                    {
                        progress.StartUpdate(www, LocalizationManager.instance.GetValueBtKey("downloading_files"));
                    }
                    Debug.Log(string.Format("download file {0}/{1}", i, downloadedFiles.Count));

                    D.log("Start to download " + www.url);
                    yield return www;
                    if (www.error != null)
                    {
                        //下载失败处理
                        SaveDownloadedFilelist();
                        D.error("Download failed " + www.url);
                        if (progress != null) progress.StopUpdate();
                        Messenger.Broadcast<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, "Download File Failed !Try Again?", OnDownloadFilesYes, OnDownloadFilesNo);
                        if (progress != null) progress.SetProgress(1f);
                        yield break;

                    }
                    else
                    {
                        if (RecordFile(www.bytes, UnityUtil.GetPlatformName() + "/" + file.path + "/" + file.name + ".bd"))
                        {
                            file.isDownloaded = true;
                            newfileD = true;
                            D.error("Download over " + www.url);
                            downloadFileSize -= www.size;
                            UpdateProgressEndStr(downloadFileSize);
                        }
                        else
                        {
                            SaveDownloadedFilelist();
                            Messenger.Broadcast<string, Action, Action>(ApplicationEvents.RES_UPDATE_WARNING, "Save File Failed !Maybe Not Enough SDCARD memory!Try Again?", OnDownloadFilesYes, OnDownloadFilesNo);
                            D.error("RecordFile failed " + www.url);
                            yield break;
                        }
                    }
                }
                i++;
            }
            if (progress != null) progress.StopUpdate();
            SaveDownloadedFilelist();
            //FIXME clearcache 如果有新文件下载
            if (newfileD)
            {
                Caching.CleanCache();
            }
        }

        //打完收工,进游戏
        if (InitOverCallBack != null)
            InitOverCallBack();

        ////FIXME 下载中出错的话的重新下载处理
        //StartCoroutine(Part2Check());
    }

    void SaveDownloadedFilelist()
    {
        if (downloadedFiles != null)
        {
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(JsonMapper.ToJson(downloadedFiles));
            if (!RecordFile(info, "downlist.txt"))
                D.error("Record downlist.txt failed! ");
        }
    }

    /// <summary>
    /// 检查扩展资源列表是否全部下载,如果本地没有扩展资源列表文件那么视为包中已经包含扩展资源
    /// </summary>
    /// <returns></returns>
    IEnumerator Part2Check()
    {
        WWW downlistWWW = new WWW(getWWWUrl(localRootUrl + UnityUtil.GetPlatformName() + "/part2package.txt"));
        //if(progress!=null)progress.StartUpdate(downlistWWW, " checking extra resource package");
        yield return downlistWWW;
        //if(progress!=null)progress.StopUpdate();
        if (downlistWWW.error != null)
        {
            D.warn(downlistWWW.error);
            D.warn("Last part2package NotFound!!!");
        }
        else
        {

            part2FileList = JsonMapper.ToObject<List<DownloadFileInfo>>(downlistWWW.text);
            part2FileSize = 0;
            for (int i = part2FileList.Count - 1; i >= 0; i--)
            {
                DownloadFileInfo df = part2FileList[i];
                int fcheck = CheckFileExist(df.path + "/" + df.name);
                if (fcheck == 0)
                {
                    Part2PackageDownloaded = false;
                    df.isDownloaded = false;
                    D.warn(df.name + " Not Exist!");
                    if (buildStatesDict.ContainsKey(df.name))
                        part2FileSize += Convert.ToInt32(buildStatesDict[df.name].size);
                }
                else
                {
                    df.isDownloaded = true;
                    part2FileList.RemoveAt(i);
                    if (fcheck == 2)
                    {
                        AddNewDownloadFile(df);
                    }
                }

            }
        }

        downlistWWW.Dispose();

        SaveDownloadedFilelist();

        //if(progress!=null)progress.SetText("Update Over, Starting Game ...");
        //打完收工,进游戏
        if (InitOverCallBack != null)
            InitOverCallBack();
    }

    int CheckFileExist(string filename)
    {
        //        string path1 = Path.GetFullPath(localRootUrl + UnityUtil.GetPlatformName() + "/" + filename);
        //        string path2 =Path.GetFullPath(sdcardRootUrl + UnityUtil.GetPlatformName() + "/" + filename);
        if (File.Exists(Path.GetFullPath(localRootUrl + UnityUtil.GetPlatformName() + "/" + filename + ".bd")))
            return 1;
        else if (File.Exists(Path.GetFullPath(sdcardRootUrl + UnityUtil.GetPlatformName() + "/" + filename + ".bd")))
            return 2;
        return 0;
    }

    bool CheckFileExistLocalRoot(string filename)
    {
        if (File.Exists(Path.GetFullPath(localRootUrl + UnityUtil.GetPlatformName() + "/" + filename + ".bd")))
            return true;

        return false;
    }

    bool CheckFileExistSdcardRoot(string filename)
    {
        if (File.Exists(Path.GetFullPath(sdcardRootUrl + UnityUtil.GetPlatformName() + "/" + filename + ".bd")))
            return true;
        return false;
    }

    private Action part2Callback;

    void Part2Callbak()
    {
        if (part2Callback != null)
            part2Callback();
        part2Callback = null;
    }
    /// <summary>
    /// 下载扩展资源
    /// </summary>
    /// <param name="callback"></param>
    public void StartDownloadPart2Files(Action callback)
    {
        part2Callback = callback;
        StartCoroutine(DownloadPart2Files());
    }

    void UpdateProgressEndStr(int size)
    {
        if (size > 0 && progress != null)
        {
            progress.SetEndStr("(~" + (size / 1048576f).ToString("####M") + ")");
        }
    }
    IEnumerator DownloadPart2Files()
    {
        //UIManager.ShowResUpdatePanel();
        if (downloadedFiles == null)
        {
            downloadedFiles = new List<DownloadFileInfo>();
            downloadedFilesDict = new Dictionary<string, DownloadFileInfo>();
        }

        if (part2FileList != null)
        {
            int i = 1;
            bool newfileD = false;

            foreach (var file in part2FileList)
            {
                if (!file.isDownloaded)
                {
                    WWW www = new WWW(ServerRootUrl + file.path + "/" + file.name + ".bd");
                    //if(progress!=null)progress.StartUpdate(www, "  download file ", i, part2FileList.Count);
                    D.log("Start to download " + www.url);
                    UpdateProgressEndStr(part2FileSize);
                    yield return www;
                    if (www.error != null)
                    {
                        //下载失败处理
                        SaveDownloadedFilelist();
                        D.error("Download failed " + www.url);
                        //if(progress!=null)progress.StopUpdate();
                        //UIManager.ShowUI(UIType.INFO_POPUP, "Download File Failed !Try Again?", UILayer.Top);
                        //Messenger.AddListener(AppEvents.INFO_POPUP_CONFIRM, OnDownloadPart2FilesYes);
                        //Messenger.AddListener(AppEvents.INFO_POPUP_OK, OnDownloadPart2FilesNo);
                        //if(progress!=null)progress.SetProgress(1f);
                        yield break;

                    }
                    else
                    {
                        if (RecordFile(www.bytes, UnityUtil.GetPlatformName() + "/" + file.path + "/" + file.name + ".bd"))
                        {
                            file.isDownloaded = true;
                            newfileD = true;
                            D.error("Download over " + www.url);
                            part2FileSize -= www.size;
                            UpdateProgressEndStr(part2FileSize);
                        }
                        else
                        {
                            SaveDownloadedFilelist();
                            //UIManager.ShowUI(UIType.INFO_POPUP, "Save File Failed !Maybe Not Enough SDCARD memory!Try Again?", UILayer.Top);
                            //Messenger.AddListener(AppEvents.INFO_POPUP_CONFIRM, OnDownloadPart2FilesYes);
                            //Messenger.AddListener(AppEvents.INFO_POPUP_OK, OnDownloadPart2FilesNo);
                            D.error("RecordFile failed " + www.url);
                            yield break;
                        }
                    }
                }
                i++;

                AddNewDownloadFile(file);
            }
            //if(progress!=null)progress.StopUpdate();
            SaveDownloadedFilelist();
            //            //FIXME clearcache 如果有新文件下载
            //            if (newfileD)
            //            {
            //                Caching.CleanCache();
            //            }
        }
        //FIXME 下载中出错的话的重新下载处理

        //        UIManager.HideResUpdatePanel();
        Part2Callbak();
        //        if(progress!=null)progress.SetText("Update Over, Starting Game ...");
        //        //打完收工,进游戏
        //        if (InitOverCallBack != null)
        //            InitOverCallBack();

    }

    public static ResUpdateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ResUpdateManager Manager").AddComponent<ResUpdateManager>();

            }

            return instance;
        }
    }

    public static void SetManualUrl(string url)
    {
        if (instance != null)
        {
            D.error("Cannot use SetManualUrl after accessed DownloadManager.Instance. Make sure call SetManualUrl before access to DownloadManager.Instance.");
            return;
        }

        manualUrl = url;
    }
    public bool ConfigLoaded
    {
        get
        {
            return bundles != null && buildStates != null;//&& bmConfiger != null;
        }
    }
    public List<string> getDependList(string bundle)
    {
        if (!ConfigLoaded)
        {
            D.error("getDependList() should be call after download manger inited");
            return null;
        }

        List<string> res = new List<string>();

        if (bundleDict == null || !bundleDict.ContainsKey(bundle))
        {
            D.error("Cannot find parent bundle [" + bundle + "], Please check your bundle config.");
            return res;
        }

        while (bundleDict[bundle].parent != "")
        {
            bundle = bundleDict[bundle].parent;
            if (bundleDict.ContainsKey(bundle))
            {
                res.Add(bundle);
            }
            else
            {
                D.error("Cannot find parent bundle [" + bundle + "], Please check your bundle config.");
                break;
            }
        }

        res.Reverse();
        return res;
    }

    public bool RecordFile(byte[] bytes, string filename)
    {
        D.log("Application.dataPath: " + Application.dataPath);

        try
        {
            string fullpath = sdcardRootUrl + filename;
            string dirp = Path.GetDirectoryName(fullpath);

            if (!System.IO.Directory.Exists(dirp))
                System.IO.Directory.CreateDirectory(dirp);

            System.IO.FileStream fs = System.IO.File.Create(System.IO.Path.GetFullPath(fullpath));

            // Add some information to the file.
            fs.Write(bytes, 0, bytes.Length);

            D.log("Saved: " + fullpath);
            fs.Close();

            return true;
        }
        catch (Exception ex)
        {
            D.error(ex.Message);
            return false;
        }

    }

    public int GetVersion(string bundleId)
    {
        if (buildStatesDict != null && buildStatesDict.ContainsKey(bundleId))
            return buildStatesDict[bundleId].version;
        return 0;
    }
}
