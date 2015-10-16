/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MapBlackMarketTemplateVO : TemplateVO{

	/// <summary>
	/// 权重
	/// </summary>
	[ExcelCellBinding(1)]
	public int rate;

	/// <summary>
	/// 物品类型
	/// </summary>
	[ExcelCellBinding(2)]
	public int itemType;

	/// <summary>
	/// 物品ID
	/// </summary>
	[ExcelCellBinding(3)]
	public int itemId;

	/// <summary>
	/// 物品数量
	/// </summary>
	[ExcelCellBinding(4)]
	public int itemNum;

	/// <summary>
	/// 价格货币类型
	/// </summary>
	[ExcelCellBinding(5)]
	public int currency;

	/// <summary>
	/// 价格数量
	/// </summary>
	[ExcelCellBinding(6)]
	public int price;


}