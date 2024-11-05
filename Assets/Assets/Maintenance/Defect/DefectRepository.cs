using Assets.Base;
using Assets.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assets.Maintenance;

public class DefectRepository: BaseRepository<Defect>
{
    public DefectRepository(BaseContext context) : base(context)
    {
    }
    
    protected override IQueryable<Defect> GetDbSet()
    {
        return _dbSet.Include(x=>x.Asset).Include(x=>x.Failure);
    }
}