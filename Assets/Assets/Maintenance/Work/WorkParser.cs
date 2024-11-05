using Assets.Base;

namespace Assets.Maintenance;

public class WorkParser: BaseParser<WorkDto,Work>
{
    public override Work ToEntity(WorkDto dto)
    {
        return new Work()
        {
            Id = dto.Id,
        };
    }

    public override WorkDto ToDto(Work entity)
    {
        return new WorkDto()
        {
            Id = entity.Id,
        };
    }
}