using Grpc.Net.Client;
using Scheduler;

namespace GrpcClient;

public static class GrpcPlanHelper
{
    public static bool PlanMaints(PlanMaintsRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("PLAN_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: PLAN_GRPC_ROUTE"));
            GrpcPlan.GrpcPlanClient client = new GrpcPlan.GrpcPlanClient(channel);
            var result = client.PlanMaints(request).Result;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}