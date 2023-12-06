namespace TodoMiniAPI.API.Endpoints;

public class TagEndpoint : IEndpoint
{
    public void Register(WebApplication app) => app.Register<Tag, TagPostDTO, TagPutDTO, TagGetDTO>();
}