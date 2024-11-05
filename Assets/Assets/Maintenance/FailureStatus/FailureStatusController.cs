using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("failure-statuses")]
public class FailureStatusController: BaseController<FailureStatusDto, FailureStatus>
{
    private BaseContext _context;
    public FailureStatusController(BaseContext context): base(new FailureStatusRepository(context), new FailureStatusParser(), context)
    {
        _context = context;
    }
    
}