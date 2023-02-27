using Asp.Versioning.Builder;

namespace LearningMinimalAPi.Controllers.v1
{
    public static class AuthController
    {
        public static RouteGroupBuilder MapAuthEndpoints(WebApplication app, ApiVersionSet versionSet)
        {
            var group = app
                .MapGroup("api/v{version:apiVersion}/auth")
                .WithApiVersionSet(versionSet)
                .HasApiVersion(1.0)
                .MapToApiVersion(1.0);

            group.MapGet("login", () => "Hello World!!");

            return group;
        }
    }
}
