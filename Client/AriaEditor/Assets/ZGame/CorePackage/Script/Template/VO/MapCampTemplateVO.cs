/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class MapCampTemplateVO : TemplateVO{

	/// <summary>
	/// 地图ID
	/// </summary>
	[ExcelCellBinding(1)]
	public int mapId;

	/// <summary>
	/// 流水号
	/// </summary>
	[ExcelCellBinding(2)]
	public int series;

	/// <summary>
	/// 对应建筑ID
	/// </summary>
	[ExcelCellBinding(3)]
	public int buildingId;

	/// <summary>
	/// 建筑等级
	/// </summary>
	[ExcelCellBinding(4)]
	public int buildingLevel;

	/// <summary>
	/// 权重
	/// </summary>
	[ExcelCellBinding(5)]
	public int rate;

	/// <summary>
	/// 守卫怪物ID
	/// </summary>
	[ExcelCellBinding(6)]
	public int monsterId;


}