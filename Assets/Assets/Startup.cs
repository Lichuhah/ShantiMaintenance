using System.Text.Json;
using Assets.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assets;

public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseSwagger();
        app.UseSwaggerUI();
//app.MapGrpcService<GrpcAssetsService>();
        app.UseRouting();
        app.UseCors("_myAllowSpecificOrigins");
/*app.UseWhen(context => context.Request.Method != "POST" ||
                       (context.Request.ContentType != null &&
                        !context.Request.ContentType.StartsWith("application/grpc")), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        if (context.Request.Method != "OPTIONS")
        {
            var token = context.Request.Cookies["Shanti.Token"] ?? context.Request.Headers["Authorization"];
            bool result = !string.IsNullOrEmpty(token) &&
                          GrpcAuthenticationHelper.CheckJwtToken(new CheckJwtTokenRequest() { Jwt = token });
            if (!result)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized access");
            }
            else
            {
                await next();
            }
        }
        else await next();
    });
});*/
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseHttpsRedirection();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddGrpc();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        services.AddCors(options =>
        {
            options.AddPolicy("_myAllowSpecificOrigins",
                policy =>
                {
                    policy.SetIsOriginAllowed(_ => true);
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
        });
        services.AddDbContext<BaseContext>(options =>
            options.UseNpgsql("host=localhost;port=5431;database=shanti;user id=postgres;password=postgres").UseSnakeCaseNamingConvention());
    }
}