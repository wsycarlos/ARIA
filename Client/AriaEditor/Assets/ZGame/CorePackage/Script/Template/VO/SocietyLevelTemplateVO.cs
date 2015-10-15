/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class SocietyLevelTemplateVO : TemplateVO{

	/// <summary>
	/// 需要贡献值
	/// </summary>
	[ExcelCellBinding(1)]
	public long needExp;

	/// <summary>
	/// 公会人数上限
	/// </summary>
	[ExcelCellBinding(2)]
	public int numberMax;


}