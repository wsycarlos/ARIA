/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MarketTemplateVO : TemplateVO{

	/// <summary>
	/// 商品ID
	/// </summary>
	[ExcelCellBinding(1)]
	public int commodityId;

	/// <summary>
	/// 商品类型
	/// </summary>
	[ExcelCellBinding(2)]
	public int commodityType;

	/// <summary>
	/// 购买货币类型
	/// </summary>
	[ExcelCellBinding(3)]
	public int payment;

	/// <summary>
	/// 购买价格
	/// </summary>
	[ExcelCellBinding(4)]
	public int payPrice;

	/// <summary>
	/// 数量
	/// </summary>
	[ExcelCellBinding(5)]
	public int amount;


}