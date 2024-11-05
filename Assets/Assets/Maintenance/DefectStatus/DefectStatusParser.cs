using Assets.Base;

namespace Assets.Maintenance;

public class DefectStatusParser: BaseParser<DefectStatusDto,DefectStatus>
{
    public override DefectStatus ToEntity(DefectStatusDto dto)
    {
        return new DefectStatus()
        {
            Id = dto.Id,
        };
    }

    public override DefectStatusDto ToDto(DefectStatus entity)
    {
        return new DefectStatusDto()
        {
            Id = entity.Id,
        };
    }
}