using Asset;
using aUTH;
using Grpc.Net.Client;

namespace GrpcClient;

public static class GrpcAuthenticationHelper
{
    public static bool CheckJwtToken(CheckJwtTokenRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("AUTH_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: AUTH_GRPC_ROUTE"));
            GrpcAuth.GrpcAuthClient client = new GrpcAuth.GrpcAuthClient(channel);
            var result = client.CheckJwtToken(request).Result;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}