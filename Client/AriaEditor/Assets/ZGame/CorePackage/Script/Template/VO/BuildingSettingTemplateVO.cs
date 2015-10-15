/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BuildingSettingTemplateVO : TemplateVO{

	/// <summary>
	/// 广场雕塑增加人口
	/// </summary>
	[ExcelCellBinding(1)]
	public int fuxingSquirePopu;

	/// <summary>
	/// 兽栏人口
	/// </summary>
	[ExcelCellBinding(2)]
	public int beastiaryPopu;

	/// <summary>
	/// 英雄官邸英雄数量
	/// </summary>
	[ExcelCellBinding(3)]
	public int herohouseNumber;

	/// <summary>
	/// 矿每级需要矿工数
	/// </summary>
	[ExcelCellBinding(4)]
	public int mineNeedminer;

	/// <summary>
	/// 矿工人数上限
	/// </summary>
	[ExcelCellBinding(5)]
	public int minerMax;

	/// <summary>
	/// 仓库上限
	/// </summary>
	[ExcelCellBinding(6)]
	public int currencyMax;

	/// <summary>
	/// 仓库保护比例
	/// </summary>
	[ExcelCellBinding(7)]
	public int currencyProtectRate;

	/// <summary>
	/// 仓库保护基数
	/// </summary>
	[ExcelCellBinding(8)]
	public int currencyProtectVal;

	/// <summary>
	/// 城墙防御值
	/// </summary>
	[ExcelCellBinding(9)]
	public int def;

	/// <summary>
	/// 市场订单数
	/// </summary>
	[ExcelCellBinding(10)]
	public int myOrderNumber;


}