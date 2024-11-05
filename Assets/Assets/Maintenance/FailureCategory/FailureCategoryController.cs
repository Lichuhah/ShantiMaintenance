using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("failure-categories")]
public class FailureCategoryController: BaseController<FailureCategoryDto, FailureCategory>
{
    private BaseContext _context;
    public FailureCategoryController(BaseContext context): base(new FailureCategoryRepository(context), new FailureCategoryParser(), context)
    {
        _context = context;
    }
    
}