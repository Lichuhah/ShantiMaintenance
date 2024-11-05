using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("works")]
public class WorkController: BaseController<WorkDto, Work>
{
    private BaseContext _context;
    public WorkController(BaseContext context): base(new WorkRepository(context), new WorkParser(), context)
    {
        _context = context;
    }
    
}