namespace TodoMiniAPI.Common.DTOs;
public class TodoPostDTO
{
    public string Description { get; set; } = string.Empty;
    public int StatusId { get; set; } = 1;
}
public class TodoPutDTO : TodoPostDTO
{
    public int Id { get; set; }
}
public class TodoGetDTO : TodoPutDTO
{
    public StatusGetDTO? Status { get; set; }
    public List<TagGetDTO>? Tags { get; set; } = new List<TagGetDTO>();
}