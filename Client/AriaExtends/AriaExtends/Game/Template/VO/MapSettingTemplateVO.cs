/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MapSettingTemplateVO : TemplateVO{

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 无事件
	/// </summary>
	[ExcelCellBinding(2)]
	public int normalRate;

	/// <summary>
	/// 黑市商人
	/// </summary>
	[ExcelCellBinding(3)]
	public int blackMarketRate;

	/// <summary>
	/// 巢穴
	/// </summary>
	[ExcelCellBinding(4)]
	public int campRate;

	/// <summary>
	/// 矿
	/// </summary>
	[ExcelCellBinding(5)]
	public int mineRate;

	/// <summary>
	/// 流浪英雄
	/// </summary>
	[ExcelCellBinding(6)]
	public int heroRate;

	/// <summary>
	/// 龙
	/// </summary>
	[ExcelCellBinding(7)]
	public int dragonRate;

	/// <summary>
	/// 劫掠
	/// </summary>
	[ExcelCellBinding(8)]
	public int robRate;

	/// <summary>
	/// 玩家城市
	/// </summary>
	[ExcelCellBinding(9)]
	public int playerRate;

	/// <summary>
	/// 降敌
	/// </summary>
	[ExcelCellBinding(10)]
	public int surrender;

	/// <summary>
	/// 需要探索度
	/// </summary>
	[ExcelCellBinding(11)]
	public int needExploreDegree;


}