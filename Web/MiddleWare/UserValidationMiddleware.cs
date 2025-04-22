using Application.Interfaces;

public class UserValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserAvailableService _userAvailableService;

    public UserValidationMiddleware(RequestDelegate next, IUserAvailableService userAvailableService)
    {
        _next = next;
        _userAvailableService = userAvailableService;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User.FindFirst("Id")?.Value;
        if (userIdClaim != null)
        {
            if (!int.TryParse(userIdClaim, out int userId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("ID de usuario inválido");
                return;
            }

            var isUserAvailable = _userAvailableService.IsUserAvailable(userId);
            if (!isUserAvailable)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Usuario inhabilitado");
                return;
            }
        }
        await _next(context);
    }
}