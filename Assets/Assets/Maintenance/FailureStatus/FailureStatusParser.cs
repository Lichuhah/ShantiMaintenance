using Assets.Base;

namespace Assets.Maintenance;

public class FailureStatusParser: BaseParser<FailureStatusDto,FailureStatus>
{
    public override FailureStatus ToEntity(FailureStatusDto dto)
    {
        return new FailureStatus()
        {
            Id = dto.Id,
        };
    }

    public override FailureStatusDto ToDto(FailureStatus entity)
    {
        return new FailureStatusDto()
        {
            Id = entity.Id,
        };
    }
}