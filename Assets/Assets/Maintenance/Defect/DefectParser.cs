using Assets.Base;

namespace Assets.Maintenance;

public class DefectParser: BaseParser<DefectDto,Defect>
{
    public override Defect ToEntity(DefectDto dto)
    {
        return new Defect()
        {
            Id = dto.Id,
        };
    }

    public override DefectDto ToDto(Defect entity)
    {
        return new DefectDto()
        {
            Id = entity.Id,
        };
    }
}