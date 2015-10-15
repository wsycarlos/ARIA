using UnityEngine;
using System.Collections;

public enum TemplateType
{
    [TemplateDef("human", 0, "HumanTemplateVOData", typeof(HumanTemplateVO))]
    HumanTemplateVODictionary,
    [TemplateDef("constant", 0, "ConstantTemplateVOData", typeof(ConstantTemplateVO))]
    ConstantTemplateVODictionary,
    [TemplateDef("building", 0, "BuildingTemplateVOData", typeof(BuildingTemplateVO))]
    BuildingTemplateVODictionary,
    [TemplateDef("building", 1, "BuildingLevelTemplateVOData", typeof(BuildingLevelTemplateVO))]
    BuildingLevelTemplateVODictionary,
    [TemplateDef("building", 2, "BuildingSpecialTemplateVOData", typeof(BuildingSpecialTemplateVO))]
    BuildingSpecialTemplateVODictionary,
    [TemplateDef("building", 3, "BuildingProductTemplateVOData", typeof(BuildingProductTemplateVO))]
    BuildingProductTemplateVODictionary,
    [TemplateDef("building", 4, "BuildingSettingTemplateVOData", typeof(BuildingSettingTemplateVO))]
    BuildingSettingTemplateVODictionary,
}