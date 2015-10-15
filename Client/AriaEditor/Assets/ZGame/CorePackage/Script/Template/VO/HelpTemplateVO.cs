/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class HelpTemplateVO : TemplateVO{

	/// <summary>
	/// 标题
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 描述
	/// </summary>
	[ExcelCellBinding(2)]
	public string desc;


}