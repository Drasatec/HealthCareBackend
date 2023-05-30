using System.Text.Json;
namespace DomainModel.Contracts;
public class Error
{
    public string? StatusCode { get; set; }
    public string? Message { get; set; }
    public Error() { }
    public Error(string statusCode , string massage)
    {
        this.StatusCode = statusCode;
        this.Message = massage;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
