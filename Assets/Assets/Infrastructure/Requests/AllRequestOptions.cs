using Microsoft.AspNetCore.Mvc;

namespace Assets.Infrastructure.Requests;

public class AllRequestOptions
{
    [FromQuery(Name = "limit")]
    public int? Limit { get; set; }
    [FromQuery(Name = "page")]
    public int? Page { get; set; }
}