/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class SocietyLivenessTemplateVO : TemplateVO{

	/// <summary>
	/// 活跃等级
	/// </summary>
	[ExcelCellBinding(1)]
	public string livenessLevel;

	/// <summary>
	/// 需要活跃度
	/// </summary>
	[ExcelCellBinding(2)]
	public int needLiveness;

	/// <summary>
	/// 每日请求数
	/// </summary>
	[ExcelCellBinding(3)]
	public int haveAmount;


}