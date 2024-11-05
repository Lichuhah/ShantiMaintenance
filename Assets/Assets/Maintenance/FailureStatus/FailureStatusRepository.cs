using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class FailureStatusRepository: BaseRepository<FailureStatus>
{
    public FailureStatusRepository(BaseContext context) : base(context)
    {
    }
}