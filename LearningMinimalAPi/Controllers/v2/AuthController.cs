using Asp.Versioning.Builder;

namespace LearningMinimalAPi.Controllers.v2
{
    public static class AuthController
    {
        public static RouteGroupBuilder MapAuthEndpoints(WebApplication app, ApiVersionSet versionSet)
        {
            var group = app
                .MapGroup("api/v{version:apiVersion}/auth")
                .WithApiVersionSet(versionSet)
                .HasApiVersion(2.0)
                .MapToApiVersion(2.0);

            group.MapGet("login", () => "Hello Putito!!"); 
            group.MapGet("logout", () => "Hello World!!");

            return group;
        }
    }
}
