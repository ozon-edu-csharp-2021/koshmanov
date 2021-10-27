using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter:ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var resultObj = new
        {
            ExceptionType = context.Exception.GetType().FullName,
            Message = context.Exception.Message,
            StackTrace = context.Exception.StackTrace
        };
        var jasonResult = new JsonResult(resultObj)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.Result = jasonResult;
    }
}