/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BuildingProductTemplateVO : TemplateVO{

	/// <summary>
	/// 金币产出速度
	/// </summary>
	[ExcelCellBinding(1)]
	public int goldSpeed;

	/// <summary>
	/// 稀有资源产出速度
	/// </summary>
	[ExcelCellBinding(2)]
	public int rarityCurrencySpeed;

	/// <summary>
	/// 兵营时间作用系数
	/// </summary>
	[ExcelCellBinding(3)]
	public int campTimeCoef;


}