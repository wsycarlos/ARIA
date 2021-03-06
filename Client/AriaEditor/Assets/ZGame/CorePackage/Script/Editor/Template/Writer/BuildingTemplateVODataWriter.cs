// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Game.Template.Editor
{
    using System.Collections.Generic;
    
    
    public class BuildingTemplateVODataWriter : ITemplaterWriter
    {
        
        public ByteArray GenerateByteArray(object data)
        {
            List<BuildingTemplateVO> list = data as List<BuildingTemplateVO>;
            ByteArray ba = new ByteArray();
            int length = list.Count;
            ba.WriteInt(length);
            for (int i = 0; (i < length); i = (i + 1))
            {
                ba.WriteInt(list[i].id);
                ba.WriteUTF(list[i].name);
                ba.WriteInt(list[i].type);
                ba.WriteInt(list[i].buildingIndex);
                ba.WriteInt(list[i].camp);
                ba.WriteUTF(list[i].image);
                ba.WriteInt(list[i].levelMap);
                ba.WriteInt(list[i].affiliation);
                ba.WriteInt(list[i].openNeedThLevel);
                ba.WriteInt(list[i].isInitZore);
                ba.WriteInt(list[i].levelRule);
                ba.WriteInt(list[i].gold);
                ba.WriteInt(list[i].wood);
                ba.WriteInt(list[i].crystal);
                ba.WriteInt(list[i].stone);
                ba.WriteInt(list[i].sulfur);
                ba.WriteInt(list[i].diamond);
                ba.WriteInt(list[i].upgradeCdTime);
                ba.WriteInt(list[i].productActorId);
                ba.WriteInt(list[i].makeCurrencyId);
                ba.WriteUTF(list[i].desc);
                ba.WriteInt(list[i].isOpen);
            }
            return ba;
        }
    }
}
