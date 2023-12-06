using AutoMapper;

namespace TodoMiniAPI.Common.DTOs;
public class TagPostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class TagPutDTO : TagPostDTO
{
    public int Id { get; set; }
}
public class TagGetDTO : TagPutDTO
{
    // Adding this collection causes a circular reference when the API creates the response
    //public List<TodoGetDTO>? Todos { get; set; } = new List<TodoGetDTO>();
}