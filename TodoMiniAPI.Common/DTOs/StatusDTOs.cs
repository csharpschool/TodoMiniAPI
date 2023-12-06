namespace TodoMiniAPI.Common.DTOs;
public class StatusPostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class StatusPutDTO : StatusPostDTO
{
    public int Id { get; set; }
}
public class StatusGetDTO : StatusPutDTO
{
}