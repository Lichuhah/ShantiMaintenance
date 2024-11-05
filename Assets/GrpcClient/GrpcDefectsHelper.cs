using Grpc.Net.Client;
using Maint;

namespace GrpcClient;

public static class GrpcDefectsHelper
{
    public static List<string> GetDefects(GetDefectsDataRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("MAINT_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: MAINT_GRPC_ROUTE"));
            GrpcDefects.GrpcDefectsClient client = new GrpcDefects.GrpcDefectsClient(channel);
            var maints = client.GetData(request).Result.ToList();;
            return maints;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}