using Assets.Base;

namespace Assets.Maintenance;

public class WorkStatusParser: BaseParser<WorkStatusDto,WorkStatus>
{
    public override WorkStatus ToEntity(WorkStatusDto dto)
    {
        return new WorkStatus()
        {
            Id = dto.Id,
        };
    }

    public override WorkStatusDto ToDto(WorkStatus entity)
    {
        return new WorkStatusDto()
        {
            Id = entity.Id,
        };
    }
}