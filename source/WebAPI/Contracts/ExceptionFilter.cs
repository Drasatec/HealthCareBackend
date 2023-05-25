using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Contracts;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new Error()
        {
            StatusCode = "",
            Message = ""
        };
        context.Result = new JsonResult(error) { StatusCode = 500};
        
    }
}
