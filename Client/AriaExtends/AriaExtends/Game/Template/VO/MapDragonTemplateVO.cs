/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MapDragonTemplateVO : TemplateVO{

	/// <summary>
	/// 地图ID
	/// </summary>
	[ExcelCellBinding(1)]
	public int mapId;

	/// <summary>
	/// 权重
	/// </summary>
	[ExcelCellBinding(2)]
	public int rate;

	/// <summary>
	/// 守卫怪物ID
	/// </summary>
	[ExcelCellBinding(3)]
	public int monsterId;

	/// <summary>
	/// 掉落组id
	/// </summary>
	[ExcelCellBinding(4)]
	public int dropId;


}