namespace Assets.Base;

public abstract class BaseParser<Dto, Entity> where Dto: BaseDto where Entity:BaseEntity
{
    public abstract Entity ToEntity(Dto dto);
    public abstract Dto ToDto(Entity? entity);
}