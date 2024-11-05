using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Catalog;

public class AssetRepository: BaseRepository<Asset>
{
    public AssetRepository(BaseContext context) : base(context)
    {
    }
}