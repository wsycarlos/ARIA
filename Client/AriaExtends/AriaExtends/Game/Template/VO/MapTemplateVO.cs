/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MapTemplateVO : TemplateVO{

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 是否开放
	/// </summary>
	[ExcelCellBinding(2)]
	public int isOpen;

	/// <summary>
	/// 所属阵营
	/// </summary>
	[ExcelCellBinding(3)]
	public int camp;

	/// <summary>
	/// 出现英雄ID
	/// </summary>
	[ExcelCellBinding(4)]
	public string heroConfig;

	/// <summary>
	/// 怪物种类
	/// </summary>
	[ExcelCellBinding(5)]
	public string monsterConfig;

	/// <summary>
	/// 特产资源ID
	/// </summary>
	[ExcelCellBinding(6)]
	public int specialty;

	/// <summary>
	/// 英雄最小等级
	/// </summary>
	[ExcelCellBinding(7)]
	public int needHeroLevel;

	/// <summary>
	/// 每小时经验
	/// </summary>
	[ExcelCellBinding(8)]
	public int everyHourExp;

	/// <summary>
	/// 每小时金币
	/// </summary>
	[ExcelCellBinding(9)]
	public int everyHourGold;

	/// <summary>
	/// 木材
	/// </summary>
	[ExcelCellBinding(10)]
	public int everyHourWood;

	/// <summary>
	/// 水晶
	/// </summary>
	[ExcelCellBinding(11)]
	public int everyHourCrystal;

	/// <summary>
	/// 宝石
	/// </summary>
	[ExcelCellBinding(12)]
	public int everyHourStone;

	/// <summary>
	/// 硫磺
	/// </summary>
	[ExcelCellBinding(13)]
	public int everyHourSulfur;

	/// <summary>
	/// 钻石
	/// </summary>
	[ExcelCellBinding(14)]
	public int everyHourDiamond;


}