using Assets.Base;
using Assets.Infrastructure;

namespace Assets.Maintenance;

public class DefectCategoryRepository: BaseRepository<DefectCategory>
{
    public DefectCategoryRepository(BaseContext context) : base(context)
    {
    }
}