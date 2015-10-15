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
    
    
    public class ActorTemplateVODataReader : ITemplateReader
    {
        
        public System.Collections.Generic.List<object> GenerateByteArray(byte[] bytes)
        {
            ByteArray data = new ByteArray(bytes);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            int length = data.ReadInt();
            for (int i = 0; (i < length); i = (i + 1))
            {
                ActorTemplateVO vo = new ActorTemplateVO();
                vo.id = data.ReadInt();
                vo.name = data.ReadUTF();
                vo.image = data.ReadUTF();
                vo.DeadlyEnemy = data.ReadInt();
                vo.camp = data.ReadInt();
                vo.type = data.ReadInt();
                vo.level = data.ReadInt();
                vo.attack = data.ReadInt();
                vo.defense = data.ReadInt();
                vo.speed = data.ReadInt();
                vo.hp = data.ReadInt();
                vo.minDamage = data.ReadInt();
                vo.maxDamage = data.ReadInt();
                vo.population = data.ReadInt();
                vo.skillId = data.ReadInt();
                vo.baseBuildTime = data.ReadInt();
                vo.buildGold = data.ReadInt();
                vo.buildWood = data.ReadInt();
                vo.buildCrystal = data.ReadInt();
                vo.buildStone = data.ReadInt();
                vo.buildSulfur = data.ReadInt();
                vo.buildDiamond = data.ReadInt();
                vo.killExp = data.ReadInt();
                list.Add(vo);
            }
            return list;
        }
    }
}
