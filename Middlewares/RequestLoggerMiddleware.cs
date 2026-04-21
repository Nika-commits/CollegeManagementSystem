namespace CollegeManagementSystem.Middlewares;

public class RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var requestDateTime = DateTime.UtcNow;
        var path = context.Request.Path;
        var requestMethod = context.Request.Method;

        await next(context);

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var role = context.User.Claims.Where(c => c.Type.Contains("role")).Select(c => c.Value)
                .ToList();
            if (role.Count != 0)
            {
                var userRoles = string.Join(",", role);
            }
        }

        var statusCode = context.Response.StatusCode;
        var responseTime = (DateTime.UtcNow - requestDateTime).TotalMilliseconds;
        logger.LogInformation(
            "{requestDateTime} {path} {requestMethod} at {responseTime}ms with status code {statusCode}",
            requestDateTime, path, requestMethod, responseTime, statusCode
        );
    }
}