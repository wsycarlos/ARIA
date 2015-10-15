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
    
    
    public class HeroTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                HeroTemplateVO vo = new HeroTemplateVO();
                vo.id = data.ReadInt();
                vo.name = data.ReadUTF();
                vo.image = data.ReadUTF();
                vo.quality = data.ReadInt();
                vo.attack = data.ReadInt();
                vo.defense = data.ReadInt();
                vo.speed = data.ReadInt();
                vo.leadershipBase = data.ReadInt();
                vo.leadershipPurLevel = data.ReadInt();
                vo.skillId = data.ReadInt();
                vo.rate = data.ReadInt();
                vo.giftType = data.ReadInt();
                vo.giftNum = data.ReadInt();
                vo.giftLikability = data.ReadInt();
                vo.fightLikability = data.ReadInt();
                vo.buyFavorDiamond = data.ReadInt();
                vo.buyFavor = data.ReadInt();
                vo.monsterId = data.ReadInt();
                vo.showInHandbook = data.ReadInt();
                vo.chineseName = data.ReadUTF();
                list.Add(vo);
            }
            return list;
        }
    }
}
