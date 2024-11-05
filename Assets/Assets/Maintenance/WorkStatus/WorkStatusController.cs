using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("work-statuses")]
public class WorkStatusController: BaseController<WorkStatusDto, WorkStatus>
{
    private BaseContext _context;
    public WorkStatusController(BaseContext context): base(new WorkStatusRepository(context), new WorkStatusParser(), context)
    {
        _context = context;
    }
    
}