using Assets.Infrastructure;
using Assets.Infrastructure.Requests;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Assets.Base;

[Consumes("application/json")]
[Produces("application/json")]
public class BaseController<Dto,Entity>: Controller where Entity:BaseEntity where Dto:BaseDto
{
    protected BaseRepository<Entity> Repository { get; set; }
    protected BaseParser<Dto, Entity> Parser { get; set; }

    protected BaseContext Context { get; set; }
    public BaseController(BaseRepository<Entity> repository, BaseParser<Dto, Entity> parser, BaseContext context)
    {
        Repository = repository;
        Parser = parser;
        Context = context;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(OkObjectResult))]
    public virtual ActionResult<AllRequestResponse<Dto>> GetAll([FromQuery] AllRequestOptions? options)
    {
        try
        {
            AllRequestResponse<Dto> response = new AllRequestResponse<Dto>();
            response.Data = Repository.All(options).Select(x => Parser.ToDto(x)).ToList();
            response.TotalCount = Repository.TotalCount();
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            return new ConflictObjectResult(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public virtual ActionResult<Dto> Get(int id)
    {
        try
        {
            Entity entity = Repository.Get(id);
            return new OkObjectResult(Parser.ToDto(entity));
        }
        catch (Exception ex)
        {
            return new ConflictObjectResult(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public virtual ActionResult<bool> Delete(int id)
    {
        try
        {
            Repository.Delete(Repository.Get(id));
            return new OkResult();
        }
        catch (Exception ex)
        {
            return new ConflictObjectResult(ex.Message);
        }
    }

    [HttpPost]
    public virtual ActionResult<Dto> Create([FromBody] Dto dto)
    {
        try
        {
            Entity entity = Parser.ToEntity(dto);
            Repository.Save(entity);
            return new OkObjectResult(Parser.ToDto(entity));
        }
        catch (Exception ex)
        {
            return new ConflictObjectResult(ex.Message);
        }   
    }

    [HttpPut("{id:int}")]
    public virtual ActionResult<Dto> Update(int id, [FromBody] Dto dto)
    {           
        try
        {
            Entity entity = Parser.ToEntity(dto);
            entity.Id = id;
            Repository.Save(entity);
            return new OkObjectResult(Parser.ToDto(entity));
        }
        catch (Exception ex)
        {
            return new ConflictObjectResult(ex.Message);
        }   
    }
}