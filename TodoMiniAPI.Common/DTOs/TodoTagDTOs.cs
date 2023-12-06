namespace TodoMiniAPI.Common.DTOs;
public class TodoTagPostDTO
{
    public int TodoId { get; set; }
    public int TagId { get; set; }
}
public class TodoTagPutDTO : TodoTagPostDTO
{
}

/*public class TodoTagGetDTO : TodoTagPostDTO
{
}*/
