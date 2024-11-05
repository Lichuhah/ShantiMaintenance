using Assets.Base;

namespace Assets.Catalog;

public class AssetParser: BaseParser<AssetDto, Asset>
{
    public override Asset ToEntity(AssetDto dto)
    {
        return new Asset()
        {
            Id = dto.Id,
            Name = dto.Name,
            Key = dto.Key
        };
    }

    public override AssetDto ToDto(Asset entity)
    {
        return new AssetDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Key = entity.Key,
            RUL = entity.Rul
        };
    }
}