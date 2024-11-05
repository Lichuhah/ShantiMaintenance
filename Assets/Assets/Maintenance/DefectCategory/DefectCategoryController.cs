using Assets.Base;
using Assets.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Maintenance;

[Route("defect-categories")]
public class DefectCategoryController: BaseController<DefectCategoryDto, DefectCategory>
{
    private BaseContext _context;
    public DefectCategoryController(BaseContext context): base(new DefectCategoryRepository(context), new DefectCategoryParser(), context)
    {
        _context = context;
    }
    
}