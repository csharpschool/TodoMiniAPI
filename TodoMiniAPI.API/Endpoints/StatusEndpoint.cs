namespace TodoMiniAPI.API.Endpoints;

public class StatusEndpoint : IEndpoint
{
    public void Register(WebApplication app) => app.Register<Status, StatusPostDTO, StatusPutDTO, StatusGetDTO>();
}