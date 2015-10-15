/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class SocietyActionTemplateVO : TemplateVO{

	/// <summary>
	/// 增加贡献
	/// </summary>
	[ExcelCellBinding(1)]
	public long giveContribution;

	/// <summary>
	/// 增加活跃度
	/// </summary>
	[ExcelCellBinding(2)]
	public long giveLiveness;


}