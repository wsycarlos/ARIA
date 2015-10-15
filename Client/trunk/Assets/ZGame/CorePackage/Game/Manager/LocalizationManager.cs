
using System.Collections.Generic;
using JenkinBuild;
using UnityEngine;

/// <summary>
/// 功能概述：
/// 负责处理游戏资源加载之前的多语言问题及游戏语言版本
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    private static LocalizationManager _instance;

    public static LocalizationManager instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new LocalizationManager();
            }
            return _instance;
        }
    }

    private LanguageDictionary _localizationData;
    private Dictionary<string, string> _itemDic;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        InitLocalizationData();
    }

    private void InitLocalizationData()
    {
        //LocalizationConfig config = Resources.Load<LocalizationConfig>("LocalizationConfig");
        //UGUIUtils.languageData = TemplateManager.Instance.LoadTemplate<LanguageDictionary>("LocalizationData_" + (LocalizationType)config.type);
    }

    public string GetValueBtKey(string key)
    {
        LocalizationType type = GetLocalizationType();
        if (null == _itemDic)
        {
            _localizationData = Resources.Load<LanguageDictionary>("LocalizationSO_" + type.ToString());

            _itemDic = new Dictionary<string, string>();
            foreach (LanguageVO item in _localizationData.itemList)
            {
                if (_itemDic.ContainsKey(item.key))
                {
                    Debug.LogWarning("[ConstantTemplateVODictionary]Item ID duplicate!! " + item.key);
                }
                else
                {
                    _itemDic.Add(item.key, item.Value);
                }
            }
        }

        if (_itemDic.ContainsKey(key))
        {
            return _itemDic[key];
        }

        return "";
    }

    public LocalizationType GetLocalizationType()
    {
        return JenkinsBuildConfig.Instance.Language;
    }
}
