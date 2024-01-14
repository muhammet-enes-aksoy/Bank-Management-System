using System.Data.Common;
using System.Net;
using System.Text.Json;
using Serilog;

namespace VbApi.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }


    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception,"UnexpectedError");
            Log.Fatal(                        
                $"Path={context.Request.Path} || " +                      
                $"Method={context.Request.Method} || " +
                $"Exception={exception.Message}"
            );
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal error!"));
        }
    }
}