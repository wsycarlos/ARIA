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
    
    
    public class LocalizationTemplateVODataWriter : ITemplaterWriter
    {
        
        public ByteArray GenerateByteArray(object data)
        {
            List<LanguageVO> list = data as List<LanguageVO>;
            ByteArray ba = new ByteArray();
            int length = list.Count;
            ba.WriteInt(length);
            for (int i = 0; (i < length); i = (i + 1))
            {
                ba.WriteUTF(list[i].Value);
                ba.WriteUTF(list[i].key);
            }
            return ba;
        }
    }
}
