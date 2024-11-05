using Assets.Base;

namespace Assets.Catalog;

public class AssetDto: BaseDto
{
    public string Name { get; set; }
    public string Key { get; set; }
    
    public DateTime? RUL { get; set; }
}