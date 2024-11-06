using Asset;
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
            List<Work> works = workRepository.All(x=>x.AssetId == asset.Id).OrderByDescending(x=>x.RegisterDate).Take(1).ToList();
            List<Defect> defects = defectRepository.All(x=>x.AssetId == asset.Id && x.RegisterDate > works[0].RegisterDate).ToList();
            defects = defects.Where(x => x.Failure == null).ToList();
            GetRulRequest request = new GetRulRequest();
            request.AssetId = id;
            request.Defects = JsonConvert.SerializeObject(defects.Select(x => new LearningRequestItem()
                { Datetime = x.RegisterDate.ToString(), Type = x.CategoryId ?? 0 }));
            request.Failures= "[]";
            request.Works = JsonConvert.SerializeObject(works.Select(x => new LearningRequestItem()
                { Datetime = x.RegisterDate.ToString(), Type = 0 }));
            
            int hours = GrpcLearningHelper.GetRul(request);
            DateTime rul = DateTime.Now.AddHours(hours);;
            asset.Rul = rul;
            Repository.Save(asset);
            return new OkObjectResult("");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ConflictObjectResult(e.Message);
        }

    }
}