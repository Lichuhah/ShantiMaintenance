using System.Globalization;
using Grpc.Net.Client;
using Maint;

namespace GrpcClient;

public static class GrpcMaintsHelper
{
    public static List<string> GetMaints(GetMaintsDataRequest request)
    {
        using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("MAINT_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: MAINT_GRPC_ROUTE"));
        GrpcMaints.GrpcMaintsClient client = new GrpcMaints.GrpcMaintsClient(channel);
        var maints = client.GetData(request).Result.ToList();;
        return maints;
    }
    
    public static DateTime GetLastMaints(GetLastMaintRequest request)
    {
        using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("MAINT_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: MAINT_GRPC_ROUTE"));
        GrpcMaints.GrpcMaintsClient client = new GrpcMaints.GrpcMaintsClient(channel);
        var maints = client.GetLastMaint(request).Result;
        return DateTime.Parse(maints, CultureInfo.InvariantCulture);
    }
    
    public static bool CreateMaints(CreateMaintRequest request)
    {
        using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("MAINT_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: MAINT_GRPC_ROUTE"));
        GrpcMaints.GrpcMaintsClient client = new GrpcMaints.GrpcMaintsClient(channel);
        var result = client.CreateMaint(request).Result;
        return result;
    }
}