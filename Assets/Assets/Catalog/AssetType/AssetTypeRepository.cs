using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Catalog;

public class AssetTypeRepository: BaseRepository<AssetType>
{
    public AssetTypeRepository(BaseContext context) : base(context)
    {
    }
}