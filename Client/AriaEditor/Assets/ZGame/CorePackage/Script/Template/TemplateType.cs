using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TemplateType
{
    [TemplateDef("human", 0, "HumanTemplateVOData", typeof(HumanTemplateVO), typeof(List<HumanTemplateVO>), typeof(Dictionary<int, HumanTemplateVO>))]
    HumanTemplateVODictionary,
    [TemplateDef("constant", 0, "ConstantTemplateVOData", typeof(ConstantTemplateVO), typeof(List<ConstantTemplateVO>), typeof(Dictionary<int, ConstantTemplateVO>))]
    ConstantTemplateVODictionary,
    [TemplateDef("building", 0, "BuildingTemplateVOData", typeof(BuildingTemplateVO), typeof(List<BuildingTemplateVO>), typeof(Dictionary<int, BuildingTemplateVO>))]
    BuildingTemplateVODictionary,
    [TemplateDef("building", 1, "BuildingLevelTemplateVOData", typeof(BuildingLevelTemplateVO), typeof(List<BuildingLevelTemplateVO>), typeof(Dictionary<int, BuildingLevelTemplateVO>))]
    BuildingLevelTemplateVODictionary,
    [TemplateDef("building", 2, "BuildingSpecialTemplateVOData", typeof(BuildingSpecialTemplateVO), typeof(List<BuildingSpecialTemplateVO>), typeof(Dictionary<int, BuildingSpecialTemplateVO>))]
    BuildingSpecialTemplateVODictionary,
    [TemplateDef("building", 3, "BuildingProductTemplateVOData", typeof(BuildingProductTemplateVO), typeof(List<BuildingProductTemplateVO>), typeof(Dictionary<int, BuildingProductTemplateVO>))]
    BuildingProductTemplateVODictionary,
    [TemplateDef("building", 4, "BuildingSettingTemplateVOData", typeof(BuildingSettingTemplateVO), typeof(List<BuildingSettingTemplateVO>), typeof(Dictionary<int, BuildingSettingTemplateVO>))]
    BuildingSettingTemplateVODictionary,
}