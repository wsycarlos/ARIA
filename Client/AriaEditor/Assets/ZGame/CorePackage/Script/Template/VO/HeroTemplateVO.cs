/// <summary>
/// CodeGenerator, don't modify this file please.
/// </summary>
[System.Serializable]
[ExcelRowBinding]
public class HeroTemplateVO : TemplateVO
{

    /// <summary>
    /// 名称
    /// </summary>
    [ExcelCellBinding(1)]
    public string name;

    /// <summary>
    /// 图片
    /// </summary>
    [ExcelCellBinding(2)]
    public string image;

    /// <summary>
    /// 品质
    /// </summary>
    [ExcelCellBinding(3)]
    public int quality;

    /// <summary>
    ///  攻击
    /// </summary>
    [ExcelCellBinding(4)]
    public int attack;

    /// <summary>
    ///  防御
    /// </summary>
    [ExcelCellBinding(5)]
    public int defense;

    /// <summary>
    ///  速度
    /// </summary>
    [ExcelCellBinding(6)]
    public int speed;

    /// <summary>
    ///  基础领导力
    /// </summary>
    [ExcelCellBinding(7)]
    public int leadershipBase;

    /// <summary>
    ///  领导力每级
    /// </summary>
    [ExcelCellBinding(8)]
    public int leadershipPurLevel;

    /// <summary>
    ///  天赋id
    /// </summary>
    [ExcelCellBinding(9)]
    public int skillId;

    /// <summary>
    ///  权重
    /// </summary>
    [ExcelCellBinding(10)]
    public int rate;

    /// <summary>
    ///  礼物类型
    /// </summary>
    [ExcelCellBinding(11)]
    public int giftType;

    /// <summary>
    ///  礼物数量
    /// </summary>
    [ExcelCellBinding(12)]
    public int giftNum;

    /// <summary>
    ///  礼物好感度
    /// </summary>
    [ExcelCellBinding(13)]
    public int giftLikability;

    /// <summary>
    ///  战斗好感度
    /// </summary>
    [ExcelCellBinding(14)]
    public int fightLikability;

    /// <summary>
    ///  购买好感所需钻石
    /// </summary>
    [ExcelCellBinding(15)]
    public int buyFavorDiamond;

    /// <summary>
    ///  钻石购买得到的好感
    /// </summary>
    [ExcelCellBinding(16)]
    public int buyFavor;

    /// <summary>
    ///  怪物ID
    /// </summary>
    [ExcelCellBinding(17)]
    public int monsterId;

    /// <summary>
    /// 是否在图鉴显示
    /// </summary>
    [ExcelCellBinding(18)]
    public int showInHandbook;

    /// <summary>
    /// 中文名
    /// </summary>
    [ExcelCellBinding(19)]
    public string chineseName;


}