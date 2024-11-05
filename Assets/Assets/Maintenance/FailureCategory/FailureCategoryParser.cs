using Assets.Base;

namespace Assets.Maintenance;

public class FailureCategoryParser: BaseParser<FailureCategoryDto,FailureCategory>
{
    public override FailureCategory ToEntity(FailureCategoryDto dto)
    {
        return new FailureCategory()
        {
            Id = dto.Id,
        };
    }

    public override FailureCategoryDto ToDto(FailureCategory entity)
    {
        return new FailureCategoryDto()
        {
            Id = entity.Id,
        };
    }
}