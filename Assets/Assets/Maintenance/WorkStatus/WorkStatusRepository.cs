using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class WorkStatusRepository: BaseRepository<WorkStatus>
{
    public WorkStatusRepository(BaseContext context) : base(context)
    {
    }
}