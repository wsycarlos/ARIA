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
    
    
    public class BuildingProductTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                BuildingProductTemplateVO vo = new BuildingProductTemplateVO();
                vo.id = data.ReadInt();
                vo.goldSpeed = data.ReadInt();
                vo.rarityCurrencySpeed = data.ReadInt();
                vo.campTimeCoef = data.ReadInt();
                list.Add(vo);
            }
            return list;
        }
    }
}
