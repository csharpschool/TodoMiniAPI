namespace TodoMiniAPI.API.Endpoints;

public class TodoTagEndpoint : IEndpoint
{
    //public void Register(WebApplication app) => app.Register<TodoTag, TodoTagPostDTO, TodoTagPutDTO, TodoTagGetDTO>();
    public void Register(WebApplication app)
    {
        app.MapPost($"/api/TodoTags", HttpPostAsync);
        app.MapDelete($"/api/TodoTags", HttpDeleteAsync);
    }

    public async Task<IResult> HttpDeleteAsync(DbService db, int todoId, int TagId)
    {
        try
        {
            if(!await db.DeleteTodoTagAsync(todoId, TagId)) return Results.NotFound();

            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't delete the {typeof(TodoTag).Name} entity.");
    }
    public async Task<IResult> HttpPostAsync(DbService db, TodoTagPostDTO dto)
    {
        try
        {
            var entity = await db.AddAsync<TodoTag, TodoTagPostDTO>(dto);
            if (await db.SaveChangesAsync())
                return Results.Created($"/todotags?todoId=entity.TodoId&tagId=entity.TagId", entity);
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't add the {typeof(TodoTag).Name}  to the book.");
    }
}