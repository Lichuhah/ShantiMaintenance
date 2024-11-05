using Assets.Base;
using Assets.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assets.Maintenance;

public class WorkRepository: BaseRepository<Work>
{
    public WorkRepository(BaseContext context) : base(context)
    {
    }
    
    protected override IQueryable<Work> GetDbSet()
    {
        return _dbSet.Include(x=>x.Asset);
    }
}