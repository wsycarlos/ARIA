/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class DropTemplateVO : TemplateVO{

	/// <summary>
	/// 备注
	/// </summary>
	[ExcelCellBinding(1)]
	public string descript;

	/// <summary>
	/// 内容
	/// </summary>
	[ExcelCellBinding(2)]
	public string content;


}