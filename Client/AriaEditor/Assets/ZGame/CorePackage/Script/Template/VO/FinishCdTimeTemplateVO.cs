/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class FinishCdTimeTemplateVO : TemplateVO{

	/// <summary>
	/// 配置时间（分钟）
	/// </summary>
	[ExcelCellBinding(1)]
	public int min;

	/// <summary>
	/// 配置价格（钻石）
	/// </summary>
	[ExcelCellBinding(2)]
	public int needDiamond;


}