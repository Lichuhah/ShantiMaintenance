using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class FailureCategoryRepository: BaseRepository<FailureCategory>
{
    public FailureCategoryRepository(BaseContext context) : base(context)
    {
    }
}