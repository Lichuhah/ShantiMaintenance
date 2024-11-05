using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class TechMapRepository: BaseRepository<TechMap>
{
    public TechMapRepository(BaseContext context) : base(context)
    {
    }
}