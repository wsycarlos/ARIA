/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class EquipTemplateVO : TemplateVO{

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(1)]
	public string name;

	/// <summary>
	/// 品质
	/// </summary>
	[ExcelCellBinding(2)]
	public int quality;

	/// <summary>
	/// 部位
	/// </summary>
	[ExcelCellBinding(3)]
	public int pos;

	/// <summary>
	/// 顺序
	/// </summary>
	[ExcelCellBinding(4)]
	public int seq;

	/// <summary>
	/// 属性1
	/// </summary>
	[ExcelCellBinding(5)]
	public string property1;

	/// <summary>
	/// 范围1
	/// </summary>
	[ExcelCellBinding(6)]
	public string valueRange1;

	/// <summary>
	/// 属性2
	/// </summary>
	[ExcelCellBinding(7)]
	public string property2;

	/// <summary>
	/// 范围2
	/// </summary>
	[ExcelCellBinding(8)]
	public string valueRange2;

	/// <summary>
	/// 描述
	/// </summary>
	[ExcelCellBinding(9)]
	public string desc;

	/// <summary>
	/// 天赋
	/// </summary>
	[ExcelCellBinding(10)]
	public string talent;


}