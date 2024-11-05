using Asset;
using Grpc.Net.Client;
using Maint;

namespace GrpcClient;

public static class GrpcAssetsHelper
{
    public static bool SetRul(SetRulRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("ASSET_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: ASSET_GRPC_ROUTE"));
            GrpcAssets.GrpcAssetsClient client = new GrpcAssets.GrpcAssetsClient(channel);
            var result = client.SetRul(request).Result;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}