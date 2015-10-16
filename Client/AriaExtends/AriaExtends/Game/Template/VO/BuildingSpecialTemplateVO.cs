/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BuildingSpecialTemplateVO : TemplateVO{

	/// <summary>
	/// 建筑id
	/// </summary>
	[ExcelCellBinding(1)]
	public int buildingTempId;

	/// <summary>
	/// 等级
	/// </summary>
	[ExcelCellBinding(2)]
	public int level;

	/// <summary>
	/// 所需主城等级
	/// </summary>
	[ExcelCellBinding(3)]
	public int upgradeNeedLevel;

	/// <summary>
	/// 时间
	/// </summary>
	[ExcelCellBinding(4)]
	public int upgradeCdTime;

	/// <summary>
	/// 金币
	/// </summary>
	[ExcelCellBinding(5)]
	public int gold;

	/// <summary>
	/// 木材
	/// </summary>
	[ExcelCellBinding(6)]
	public int wood;

	/// <summary>
	/// 宝石
	/// </summary>
	[ExcelCellBinding(7)]
	public int stone;

	/// <summary>
	/// 水晶
	/// </summary>
	[ExcelCellBinding(8)]
	public int crystal;

	/// <summary>
	/// 硫磺
	/// </summary>
	[ExcelCellBinding(9)]
	public int sulfur;

	/// <summary>
	/// 钻石
	/// </summary>
	[ExcelCellBinding(10)]
	public int diamond;


}