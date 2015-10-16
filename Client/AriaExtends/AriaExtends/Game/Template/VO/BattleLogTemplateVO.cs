/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class BattleLogTemplateVO : TemplateVO{

	/// <summary>
	/// 文案key
	/// </summary>
	[ExcelCellBinding(1)]
	public string key;

	/// <summary>
	/// 日志模式
	/// </summary>
	[ExcelCellBinding(2)]
	public int logPattern;


}