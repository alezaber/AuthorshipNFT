using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

public static class CustomErrorHandlerHelper
{


    public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment, ILogger logger)
    {
        if (environment.IsDevelopment())
        {
            app.Use((context, task) => WriteDevelopmentResponse(context, task, logger));
        }
        else
        {
            app.Use((context, task) => WriteProductionResponse(context, task, logger));
        }
    }

    private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next, ILogger logger)
        => WriteResponse(httpContext, includeDetails: true, logger);

    private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next, ILogger logger)
        => WriteResponse(httpContext, includeDetails: false, logger);

    private static async Task WriteResponse(HttpContext httpContext, bool includeDetails, ILogger logger)
    {
        // Try and retrieve the error from the ExceptionHandler middleware
        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        var ex = exceptionDetails?.Error;

        // Should always exist, but best to be safe!
        if (ex != null)
        {
            // ProblemDetails has it's own content type
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";

            // Get the details to display, depending on whether we want to expose the raw exception 
            var details = includeDetails ? ex.ToString() : null;

            int statusCode = 500;
           

            var problem = new
            {
                Status = statusCode,
                ex.Message,
                Detail = details
            };

            if (statusCode == 500)
            {
                logger.LogError(ex, ex.Message);
            }
            else
            {
                logger.LogWarning(ex, ex.Message);
            }

            httpContext.Response.StatusCode = statusCode;

            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            var stream = httpContext.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}