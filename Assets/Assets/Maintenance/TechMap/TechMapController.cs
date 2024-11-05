using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("techmaps")]
public class TechMapController: BaseController<TechMapDto, TechMap>
{
    private BaseContext _context;
    public TechMapController(BaseContext context): base(new TechMapRepository(context), new TechMapParser(), context)
    {
        _context = context;
    }
    
}