using TodoMiniAPI.Common;

namespace TodoMiniAPI.Data.Entities;

public class Tag : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Todo>? Todos { get; } = new List<Todo>();
}