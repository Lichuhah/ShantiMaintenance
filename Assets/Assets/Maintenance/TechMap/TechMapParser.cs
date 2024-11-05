using Assets.Base;

namespace Assets.Maintenance;

public class TechMapParser: BaseParser<TechMapDto,TechMap>
{
    public override TechMap ToEntity(TechMapDto dto)
    {
        return new TechMap()
        {
            Id = dto.Id,
        };
    }

    public override TechMapDto ToDto(TechMap entity)
    {
        return new TechMapDto()
        {
            Id = entity.Id,
        };
    }
}