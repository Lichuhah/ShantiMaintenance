using Assets.Base;

namespace Assets.Infrastructure.Requests;

public class AllRequestResponse<Dto> where Dto: BaseDto
{
    public List<Dto> Data { get; set; }
    public int TotalCount { get; set; }
}