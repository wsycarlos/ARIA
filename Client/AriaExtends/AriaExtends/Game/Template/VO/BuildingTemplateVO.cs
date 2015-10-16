/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BuildingTemplateVO : TemplateVO{

	/// <summary>
	/// 建筑名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 类型
	/// </summary>
	[ExcelCellBinding(2)]
	public int type;

	/// <summary>
	/// 标记建筑位置
	/// </summary>
	[ExcelCellBinding(3)]
	public int buildingIndex;

	/// <summary>
	/// 阵营阵营
	/// </summary>
	[ExcelCellBinding(4)]
	public int camp;

	/// <summary>
	/// 图片图片
	/// </summary>
	[ExcelCellBinding(5)]
	public string image;

	/// <summary>
	/// 等级上限
	/// </summary>
	[ExcelCellBinding(6)]
	public int levelMap;

	/// <summary>
	/// 建筑归属	(1玩家, 2中立) 建筑归属
	/// </summary>
	[ExcelCellBinding(7)]
	public int affiliation;

	/// <summary>
	/// 解锁所需主城等级
	/// </summary>
	[ExcelCellBinding(8)]
	public int openNeedThLevel;

	/// <summary>
	/// 是否解锁后是0级
	/// </summary>
	[ExcelCellBinding(9)]
	public int isInitZore;

	/// <summary>
	/// 等级规则0：无限制,1：不超过主城等级,2：特殊规则,3：无升级操作
	/// </summary>
	[ExcelCellBinding(10)]
	public int levelRule;

	/// <summary>
	/// 金
	/// </summary>
	[ExcelCellBinding(11)]
	public int gold;

	/// <summary>
	/// 木
	/// </summary>
	[ExcelCellBinding(12)]
	public int wood;

	/// <summary>
	/// 水晶
	/// </summary>
	[ExcelCellBinding(13)]
	public int crystal;

	/// <summary>
	/// 宝石
	/// </summary>
	[ExcelCellBinding(14)]
	public int stone;

	/// <summary>
	/// 硫磺
	/// </summary>
	[ExcelCellBinding(15)]
	public int sulfur;

	/// <summary>
	/// 钻石
	/// </summary>
	[ExcelCellBinding(16)]
	public int diamond;

	/// <summary>
	/// 升级基础时间消耗
	/// </summary>
	[ExcelCellBinding(17)]
	public int upgradeCdTime;

	/// <summary>
	/// 生产兵种id
	/// </summary>
	[ExcelCellBinding(18)]
	public int productActorId;

	/// <summary>
	/// 生产资源id
	/// </summary>
	[ExcelCellBinding(19)]
	public int makeCurrencyId;

	/// <summary>
	/// 建筑描述
	/// </summary>
	[ExcelCellBinding(20)]
	public string desc;

	/// <summary>
	/// 是否开放
	/// </summary>
	[ExcelCellBinding(21)]
	public int isOpen;


}