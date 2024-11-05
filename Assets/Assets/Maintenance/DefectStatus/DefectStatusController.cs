using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("defect-statuses")]
public class DefectStatusController: BaseController<DefectStatusDto, DefectStatus>
{
    private BaseContext _context;
    public DefectStatusController(BaseContext context): base(new DefectStatusRepository(context), new DefectStatusParser(), context)
    {
        _context = context;
    }
    
}