using Assets.Base;

namespace Assets.Maintenance;

public class FailureParser: BaseParser<FailureDto,Failure>
{
    public override Failure ToEntity(FailureDto dto)
    {
        return new Failure()
        {
            Id = dto.Id,
        };
    }

    public override FailureDto ToDto(Failure entity)
    {
        return new FailureDto()
        {
            Id = entity.Id,
        };
    }
}