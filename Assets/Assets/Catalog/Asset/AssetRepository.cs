using Assets.Base;
using Assets.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assets.Catalog;

public class AssetRepository: BaseRepository<Asset>
{
    public AssetRepository(BaseContext context) : base(context)
    {
    }
    
    protected override IQueryable<Asset> GetDbSet()
    {
        return _dbSet.Include(x=>x.Type);
    }
}