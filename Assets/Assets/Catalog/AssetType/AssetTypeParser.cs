using Assets.Base;

namespace Assets.Catalog;

public class AssetTypeParser: BaseParser<AssetTypeDto,AssetType>
{
    public override AssetType ToEntity(AssetTypeDto dto)
    {
        return new AssetType()
        {
            Id = dto.Id,
        };
    }

    public override AssetTypeDto ToDto(AssetType entity)
    {
        return new AssetTypeDto()
        {
            Id = entity.Id,
        };
    }
}