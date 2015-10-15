/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class ChargeTemplateVO : TemplateVO{

	/// <summary>
	/// 类型
	/// </summary>
	[ExcelCellBinding(1)]
	public int type;

	/// <summary>
	/// 商品Id
	/// </summary>
	[ExcelCellBinding(2)]
	public string goodsId;

	/// <summary>
	/// 商品名称
	/// </summary>
	[ExcelCellBinding(3)]
	public string goodsName;

	/// <summary>
	/// 兑换钻石数量
	/// </summary>
	[ExcelCellBinding(4)]
	public int diamondAmount;

	/// <summary>
	/// 商品价格
	/// </summary>
	[ExcelCellBinding(5)]
	public float price;

	/// <summary>
	/// 折扣
	/// </summary>
	[ExcelCellBinding(6)]
	public int discount;

	/// <summary>
	/// 折后价格
	/// </summary>
	[ExcelCellBinding(7)]
	public float realPrice;

	/// <summary>
	/// 真实货币类型
	/// </summary>
	[ExcelCellBinding(8)]
	public string currency;

	/// <summary>
	/// 充值描述
	/// </summary>
	[ExcelCellBinding(9)]
	public string desc;

	/// <summary>
	/// 图标ID
	/// </summary>
	[ExcelCellBinding(10)]
	public string icon_id;


}