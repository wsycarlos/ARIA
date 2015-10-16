/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class ClubTipsTemplateVO : TemplateVO{

	/// <summary>
	/// 内容
	/// </summary>
	[ExcelCellBinding(1)]
	public string content;

	/// <summary>
	/// 显示等级限制上限
	/// </summary>
	[ExcelCellBinding(2)]
	public int levelUpper;

	/// <summary>
	/// 显示等级限制下限
	/// </summary>
	[ExcelCellBinding(3)]
	public int levelLower;


}