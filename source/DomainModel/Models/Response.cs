namespace DomainModel.Models;

public class Response<TRes>
{
    public bool Success { get; set; }
    public string? Message { get; set; } = null;
    public TRes? Value { get; set; }
    public Response() { }
    public Response(bool success, string? message, TRes? value = default)
    {
        Success = success;
        Message = message;
        Value = value;
    }
}

public class Response
{
    public bool Success { get; set; }
    public string? Message { get; set; } = null;
    public Response() { }
    public Response(bool success, string? message)
    {
        Success = success;
        Message = message;
    }
}

public class ResponseId
{
    public bool Success { get; set; }
    public string? Message { get; set; } = null;
    public int? Id { get; set; }
    public ResponseId() { }
    public ResponseId(bool success, string? message, int id)
    {
        Success = success;
        Message = message;
        Id = id;
    }
}

public class PagedResponse<T>
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public List<T> Data { get; set; }

    public PagedResponse()
    {
        Data = new List<T>();
    }
}



