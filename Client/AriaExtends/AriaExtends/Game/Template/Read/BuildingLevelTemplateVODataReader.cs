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
    
    
    public class BuildingLevelTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                BuildingLevelTemplateVO vo = new BuildingLevelTemplateVO();
                vo.id = data.ReadInt();
                vo.time = data.ReadInt();
                vo.gold = data.ReadInt();
                vo.wood = data.ReadInt();
                vo.stone = data.ReadInt();
                vo.crystal = data.ReadInt();
                vo.sulfur = data.ReadInt();
                vo.diamond = data.ReadInt();
                list.Add(vo);
            }
            return list;
        }
    }
}
