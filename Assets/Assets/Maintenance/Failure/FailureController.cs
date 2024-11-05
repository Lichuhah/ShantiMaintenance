using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("failures")]
public class FailureController: BaseController<FailureDto, Failure>
{
    private BaseContext _context;
    public FailureController(BaseContext context): base(new FailureRepository(context), new FailureParser(), context)
    {
        _context = context;
    }
    
}