/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class GuideTemplateVO : TemplateVO{

	/// <summary>
	/// 是否已完成，0未完成，1已完成
	/// </summary>
	[ExcelCellBinding(1)]
	public int isComplete;

	/// <summary>
	/// 是否强制
	/// </summary>
	[ExcelCellBinding(2)]
	public int isForce;

	/// <summary>
	/// 如果未完成则返回到索引值
	/// </summary>
	[ExcelCellBinding(3)]
	public int backIndex;

	/// <summary>
	/// 引导路径,场景|物体路径(ReportPanel/FightButton)
	/// </summary>
	[ExcelCellBinding(4)]
	public string guidePath;

	/// <summary>
	/// 参数类型，1位文本对话框，则后序参数为（文本，坐标，坐标，对齐类型），2位其他
	/// </summary>
	[ExcelCellBinding(5)]
	public int paramType;

	/// <summary>
	/// 参数1
	/// </summary>
	[ExcelCellBinding(6)]
	public string param1;

	/// <summary>
	/// 参数2
	/// </summary>
	[ExcelCellBinding(7)]
	public string param2;

	/// <summary>
	/// 参数3
	/// </summary>
	[ExcelCellBinding(8)]
	public string param3;

	/// <summary>
	/// 参数4
	/// </summary>
	[ExcelCellBinding(9)]
	public string param4;


}