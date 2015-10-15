/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class ActorTemplateVO : TemplateVO{

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 图片
	/// </summary>
	[ExcelCellBinding(2)]
	public string image;

	/// <summary>
	/// 死敌id
	/// </summary>
	[ExcelCellBinding(3)]
	public int DeadlyEnemy;

	/// <summary>
	/// 所属阵营
	/// </summary>
	[ExcelCellBinding(4)]
	public int camp;

	/// <summary>
	/// 兵种类型
	/// </summary>
	[ExcelCellBinding(5)]
	public int type;

	/// <summary>
	/// 兵种等级
	/// </summary>
	[ExcelCellBinding(6)]
	public int level;

	/// <summary>
	///  攻击
	/// </summary>
	[ExcelCellBinding(7)]
	public int attack;

	/// <summary>
	///  防御
	/// </summary>
	[ExcelCellBinding(8)]
	public int defense;

	/// <summary>
	///  速度
	/// </summary>
	[ExcelCellBinding(9)]
	public int speed;

	/// <summary>
	///  生命
	/// </summary>
	[ExcelCellBinding(10)]
	public int hp;

	/// <summary>
	///  最小伤害
	/// </summary>
	[ExcelCellBinding(11)]
	public int minDamage;

	/// <summary>
	///  最大伤害
	/// </summary>
	[ExcelCellBinding(12)]
	public int maxDamage;

	/// <summary>
	/// 占用人口
	/// </summary>
	[ExcelCellBinding(13)]
	public int population;

	/// <summary>
	///  天赋id
	/// </summary>
	[ExcelCellBinding(14)]
	public int skillId;

	/// <summary>
	///  基础生产时间
	/// </summary>
	[ExcelCellBinding(15)]
	public int baseBuildTime;

	/// <summary>
	///  生产所需金币
	/// </summary>
	[ExcelCellBinding(16)]
	public int buildGold;

	/// <summary>
	///  生产所需木材
	/// </summary>
	[ExcelCellBinding(17)]
	public int buildWood;

	/// <summary>
	///  生产所需水晶
	/// </summary>
	[ExcelCellBinding(18)]
	public int buildCrystal;

	/// <summary>
	///  生产所需宝石
	/// </summary>
	[ExcelCellBinding(19)]
	public int buildStone;

	/// <summary>
	///  生产所需硫磺
	/// </summary>
	[ExcelCellBinding(20)]
	public int buildSulfur;

	/// <summary>
	///  生产所需钻石
	/// </summary>
	[ExcelCellBinding(21)]
	public int buildDiamond;

	/// <summary>
	///  击杀所获得的经验
	/// </summary>
	[ExcelCellBinding(22)]
	public int killExp;


}