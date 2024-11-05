using Assets.Base;
using Assets.Infrastructure;
using Assets.Maintenance;
using Google.Protobuf;
using GrpcClient;
using Learning;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Assets.Catalog;

[Route("assets")]
public class AssetController: BaseController<AssetDto, Asset>
{
    public AssetController(BaseContext context): base(new AssetRepository(context), new AssetParser(), context)
    {
   
    }

    [HttpGet("plan/{id}")]
    public ActionResult StartPlan(int id)
    {
        try
        {
            WorkRepository workRepository = new WorkRepository(Context);
            DefectRepository defectRepository = new DefectRepository(Context);
            Asset asset = Repository.Get(id);
            List<Work> works = workRepository.All(x=>x.AssetId == asset.Id).ToList();
            List<Defect> defects = defectRepository.All(x=>x.AssetId == asset.Id).ToList();
            List<Failure> failures = defects.Where(x => x.Failure != null).Select(x => x.Failure).ToList();
            defects = defects.Where(x => x.Failure == null).ToList();
            StartLearningRequest request = new StartLearningRequest();
            request.AssetId = id;
            request.Defects = JsonConvert.SerializeObject(defects.Select(x => new LearningRequestItem()
                { Datetime = x.RegisterDate.ToString(), Type = x.CategoryId ?? 0 }));
            request.Failures= JsonConvert.SerializeObject(failures.Select(x => new LearningRequestItem()
                { Datetime = x.RegisterDate.ToString(), Type = x.CategoryId ?? 0 }));
            request.Works = JsonConvert.SerializeObject(works.Select(x => new LearningRequestItem()
                { Datetime = x.RegisterDate.ToString(), Type = 0 }));
            bool result = GrpcLearningHelper.StartPlan(request);
            return result ? new OkObjectResult("") : new ConflictResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ConflictObjectResult(e.Message);
        }

    }
}