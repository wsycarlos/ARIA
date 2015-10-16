// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Game.Template
{
    
    
    public class BuildingTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                BuildingTemplateVO vo = new BuildingTemplateVO();
                vo.id = data.ReadInt();
                vo.name = data.ReadUTF();
                vo.type = data.ReadInt();
                vo.buildingIndex = data.ReadInt();
                vo.camp = data.ReadInt();
                vo.image = data.ReadUTF();
                vo.levelMap = data.ReadInt();
                vo.affiliation = data.ReadInt();
                vo.openNeedThLevel = data.ReadInt();
                vo.isInitZore = data.ReadInt();
                vo.levelRule = data.ReadInt();
                vo.gold = data.ReadInt();
                vo.wood = data.ReadInt();
                vo.crystal = data.ReadInt();
                vo.stone = data.ReadInt();
                vo.sulfur = data.ReadInt();
                vo.diamond = data.ReadInt();
                vo.upgradeCdTime = data.ReadInt();
                vo.productActorId = data.ReadInt();
                vo.makeCurrencyId = data.ReadInt();
                vo.desc = data.ReadUTF();
                vo.isOpen = data.ReadInt();
                list.Add(vo);
            }
            return list;
        }
    }
}