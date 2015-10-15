/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class TalentTemplateVO : TemplateVO{

	/// <summary>
	/// 类型
	/// </summary>
	[ExcelCellBinding(1)]
	public int type;

	/// <summary>
	/// 名称
	/// </summary>
	[ExcelCellBinding(2)]
	public string name;

	/// <summary>
	/// 计算位置
	/// </summary>
	[ExcelCellBinding(3)]
	public string calcPos;

	/// <summary>
	/// 天赋描述文本
	/// </summary>
	[ExcelCellBinding(4)]
	public string descTxt;

	/// <summary>
	/// 参数1
	/// </summary>
	[ExcelCellBinding(5)]
	public string param1;

	/// <summary>
	/// 参数2
	/// </summary>
	[ExcelCellBinding(6)]
	public string param2;

	/// <summary>
	/// 参数3
	/// </summary>
	[ExcelCellBinding(7)]
	public string param3;

	/// <summary>
	/// 魔免参数
	/// </summary>
	[ExcelCellBinding(10)]
	public int magicalResist;

	/// <summary>
	/// 技能动画
	/// </summary>
	[ExcelCellBinding(11)]
	public int atkAnimation;

	/// <summary>
	/// 技能起手特效
	/// </summary>
	[ExcelCellBinding(12)]
	public string atkEffect;

	/// <summary>
	/// 技能承受特效
	/// </summary>
	[ExcelCellBinding(13)]
	public string defEffect;


}