/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class ConstantTemplateVO : TemplateVO{

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string key;

	/// <summary>
	/// 值
	/// </summary>
	[ExcelCellBinding(2)]
	public string value;


}