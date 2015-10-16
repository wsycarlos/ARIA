/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BornTemplateVO : TemplateVO{

	/// <summary>
	/// 阵营
	/// </summary>
	[ExcelCellBinding(1)]
	public int camp;

	/// <summary>
	/// 类型
	/// </summary>
	[ExcelCellBinding(2)]
	public int type;

	/// <summary>
	/// 初始物id
	/// </summary>
	[ExcelCellBinding(3)]
	public int initId;

	/// <summary>
	/// 数量
	/// </summary>
	[ExcelCellBinding(4)]
	public int num;


}