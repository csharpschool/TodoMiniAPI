using TodoMiniAPI.Common;

namespace TodoMiniAPI.Data.Entities;

public class Todo : IEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StatusId { get; set; } = 1;
    public Status? Status { get; set; }
    public List<Tag>? Tags { get; } = new List<Tag>();
}