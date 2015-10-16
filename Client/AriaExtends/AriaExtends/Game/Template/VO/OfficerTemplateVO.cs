/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class OfficerTemplateVO : TemplateVO{

	/// <summary>
	/// 事务官名字
	/// </summary>
	[ExcelCellBinding(1)]
	public string officerName;

	/// <summary>
	/// 描述
	/// </summary>
	[ExcelCellBinding(2)]
	public string desc;

	/// <summary>
	/// 参数
	/// </summary>
	[ExcelCellBinding(3)]
	public string params1;

	/// <summary>
	/// 雇佣花费
	/// </summary>
	[ExcelCellBinding(4)]
	public int diamond;


}