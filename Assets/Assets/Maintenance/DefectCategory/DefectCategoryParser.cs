using Assets.Base;

namespace Assets.Maintenance;

public class DefectCategoryParser: BaseParser<DefectCategoryDto,DefectCategory>
{
    public override DefectCategory ToEntity(DefectCategoryDto dto)
    {
        return new DefectCategory()
        {
            Id = dto.Id,
        };
    }

    public override DefectCategoryDto ToDto(DefectCategory entity)
    {
        return new DefectCategoryDto()
        {
            Id = entity.Id,
        };
    }
}