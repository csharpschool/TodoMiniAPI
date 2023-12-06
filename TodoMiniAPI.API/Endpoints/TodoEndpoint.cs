namespace TodoMiniAPI.API.Endpoints;

public class TodoEndpoint : IEndpoint
{
    public void Register(WebApplication app) => app.Register<Todo, TodoPostDTO, TodoPutDTO, TodoGetDTO>();
}