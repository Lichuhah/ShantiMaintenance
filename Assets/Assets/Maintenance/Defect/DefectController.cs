using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("defects")]
public class DefectController: BaseController<DefectDto, Defect>
{
    private BaseContext _context;
    public DefectController(BaseContext context): base(new DefectRepository(context), new DefectParser(), context)
    {
        _context = context;
    }
    
}