using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class FailureRepository: BaseRepository<Failure>
{
    public FailureRepository(BaseContext context) : base(context)
    {
    }
}