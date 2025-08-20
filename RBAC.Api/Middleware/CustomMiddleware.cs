using System.IdentityModel.Tokens.Jwt;

namespace RBAC.Api.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            string? userId = null;
            string? roles = null;

            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    try
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(token);

                        // 👇 Extract userId (subject claim)
                        userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                        // 👇 Extract roles (multiple possible)
                        var roleClaims = jwtToken.Claims.Where(c => c.Type == "role" || c.Type == "roles").Select(c => c.Value);
                        roles = roleClaims.Any() ? string.Join(",", roleClaims) : null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Invalid JWT token while processing request {Path}", path);
                    }
                }
            }

            // ✅ Structured logging with ID + Role
            _logger.LogInformation(
                "Incoming request {Method} {Path} from UserId={UserId} Roles={Roles}",
                method,
                path,
                userId ?? "anonymous",
                roles ?? "none"
            );

            await _next(context);
        }
    }
}
