using Grpc.Net.Client;
using Learning;

namespace GrpcClient;

public class GrpcLearningHelper
{
    public static DateTime? GetRul(GetRulRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("LEARNING_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: PLAN_GRPC_ROUTE"));
            GrpcLearning.GrpcLearningClient client = new GrpcLearning.GrpcLearningClient(channel);
            var result = client.GetRul(request).Rul;
            if (result == "") return null;
            return DateTime.Parse(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static bool StartPlan(StartLearningRequest request)
    {
        try
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("LEARNING_GRPC_ROUTE") ?? throw new InvalidOperationException("No var: LEARNING_GRPC_ROUTE"));
            GrpcLearning.GrpcLearningClient client = new GrpcLearning.GrpcLearningClient(channel);
            var result = client.StartLearning(request).Result;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}