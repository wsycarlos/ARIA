using System;
using Game.Template;
using Loader;
using UnityEngine;
using ZGame;

namespace Game.Manager
{

    /// <summary>
    /// 用来缓存一些模板数据
    /// </summary>
    public class TemplateManager
    {
        protected TemplateManager()
        {
            //不允许外部实例化
            LoadTemplates();
        }

        private static TemplateManager _instance = null;

        private static long times;
        public static TemplateManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    times = DateTime.Now.Ticks;
                    Debug.Log(" begin " + times);
                    _instance = new TemplateManager();
                }
                return _instance;
            }
        }

        public void Test()
        {
            long endTime = DateTime.Now.Ticks;
            Debug.Log("end " + endTime);
            Debug.Log("total " + (endTime - times));
        }

        private AssetBundle adb;

        private LocalizationTemplateVODataDictionary _localizationData = new LocalizationTemplateVODataDictionary();
        private BuildingTemplateVODataDictionary _buildingInfoData = new BuildingTemplateVODataDictionary();
        private BuildingLevelTemplateVODataDictionary _buildingLevelData = new BuildingLevelTemplateVODataDictionary();
        private BuildingSettingTemplateVODataDictionary _buildSettingData = new BuildingSettingTemplateVODataDictionary();
        private BuildingSpecialTemplateVODataDictionary _buildingSpecialData = new BuildingSpecialTemplateVODataDictionary();
        private BuildingProductTemplateVODataDictionary _buildingProductData = new BuildingProductTemplateVODataDictionary();
        private HumanTemplateVODataDictionary _humanDict = new HumanTemplateVODataDictionary();
        private ConstantTemplateVODataDictionary _constantData = new ConstantTemplateVODataDictionary();

        #region

        public LocalizationTemplateVODataDictionary localizationData
        {
            get { return _localizationData; }
        }

        public BuildingTemplateVODataDictionary buildingInfoData
        {
            get { return _buildingInfoData; }
        }
        public BuildingLevelTemplateVODataDictionary buildingLevelData
        {
            get { return _buildingLevelData; }
        }
        public BuildingSettingTemplateVODataDictionary buildSettingData
        {
            get { return _buildSettingData; }
        }
        public BuildingSpecialTemplateVODataDictionary buildingSpecialData
        {
            get { return _buildingSpecialData; }
        }
        public BuildingProductTemplateVODataDictionary buildingProductData
        {
            get { return _buildingProductData; }
        }

        public HumanTemplateVODataDictionary humanDict
        {
            get { return _humanDict; }
        }

        public ConstantTemplateVODataDictionary constantData
        {
            get { return _constantData; }
        }
        #endregion

        public void LoadTemplates()
        {
            adb = AssetBundleManager.GetBundle("template", AssetBundleType.DATA);
            if (null == adb)
            {
                Debug.LogError("Template assetbundle is null");
                return;
            }

            LocalizationType lType = LocalizationManager.instance.GetLocalizationType();
            LoadTemplates(_localizationData, "LocalizationData_" + lType.ToString(), new LocalizationTemplateVODataReader());
            //初始化多语言
            UGUIUtils.languageData = new LanguageDictionary() { itemList = _localizationData.ItemList };

            LoadTemplates(_buildingInfoData, "BuildingTemplateData", new BuildingTemplateVODataReader());
            LoadTemplates(_buildingLevelData, "BuildingLevelTemplateData", new BuildingLevelTemplateVODataReader());
            LoadTemplates(_buildSettingData, "BuildingSettingTemplateData", new BuildingSettingTemplateVODataReader());
            LoadTemplates(_buildingSpecialData, "BuildingSpecialTemplateData", new BuildingSpecialTemplateVODataReader());
            LoadTemplates(_buildingProductData, "BuildingProductTemplateData", new BuildingProductTemplateVODataReader());
            LoadTemplates(_humanDict, "HumanTemplateData", new HumanTemplateVODataReader());
        }

        public void LoadTemplates(ITemplateDictionary dict, string dataName, ITemplateReader reader)
        {
            TextAsset txt = adb.LoadAsset<TextAsset>(dataName + ".bytes");
            if (null == txt)
            {
                Debug.LogError("Can not load the asset : " + dataName);
                return;
            }
            dict.Init(reader.GenerateByteArray(txt.bytes));
        }
    }
}