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
    
    
    public class HumanTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                HumanTemplateVO vo = new HumanTemplateVO();
                vo.id = data.ReadInt();
                vo.exp = data.ReadInt();
                list.Add(vo);
            }
            return list;
        }
    }
}
