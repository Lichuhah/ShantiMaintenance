using Assets.Base;
using Assets.Infrastructure;
using Assets.Maintenance;
using GrpcClient;
using Learning;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Assets.Catalog;

public class AssetTypeController: BaseController<AssetTypeDto, AssetType>
{
    private BaseContext _context;
    public AssetTypeController(BaseContext context): base(new AssetTypeRepository(context), new AssetTypeParser(), context)
    {
        _context = context;
    }
    
    [HttpGet("plan/{id}")]
    public ActionResult StartPlan(int id)
    {
        try
        {
            AssetType type = Repository.Get(id);
            /*AssetRepository assetRepository = new AssetRepository(Context);
            WorkRepository workRepository = new WorkRepository(Context);
            DefectRepository defectRepository = new DefectRepository(Context);
            List<Asset> assets = assetRepository.All(x => x.TypeId == type.Id).ToList();
            foreach (var asset in assets)
            {
                List<Work> works = workRepository.All(x=>x.AssetId == asset.Id).ToList();
                List<Defect> defects = defectRepository.All(x=>x.AssetId == asset.Id).ToList();
                List<Failure> failures = defects.Where(x => x.Failure != null).Select(x => x.Failure).ToList();
                defects = defects.Where(x => x.Failure == null).ToList();
                SetLearningRequest request = new SetLearningRequest();
                request.AssetId = asset.Id;
                request.TypeId = type.Id;
                request.Defects = JsonConvert.SerializeObject(defects.Select(x => new LearningRequestItem()
                    { Datetime = x.RegisterDate.ToString(), Type = x.CategoryId ?? 0 }));
                request.Failures= JsonConvert.SerializeObject(failures.Select(x => new LearningRequestItem()
                    { Datetime = x.RegisterDate.ToString(), Type = x.CategoryId ?? 0 }));
                request.Works = JsonConvert.SerializeObject(works.Select(x => new LearningRequestItem()
                    { Datetime = x.RegisterDate.ToString(), Type = 0 }));
                GrpcLearningHelper.SetPlanData(request);
            }*/

            StartLearningRequest startrequest = new StartLearningRequest();
            startrequest.Id = 0;
            startrequest.TypeId = id;
            bool result = GrpcLearningHelper.StartLearning(startrequest);
            return result ? new OkObjectResult("") : new ConflictResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ConflictObjectResult(e.Message);
        }

    }
}