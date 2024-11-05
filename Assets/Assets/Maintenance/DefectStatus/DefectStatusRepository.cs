using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class DefectStatusRepository: BaseRepository<DefectStatus>
{
    public DefectStatusRepository(BaseContext context) : base(context)
    {
    }
}