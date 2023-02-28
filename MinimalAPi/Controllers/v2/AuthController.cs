using Asp.Versioning.Builder;
using MinimalApi.UseCases.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Controllers.v2
{
    public static class AuthController
    {
        public static RouteGroupBuilder MapAuthEndpoints(WebApplication app, ApiVersionSet versionSet)
        {
            var group = app
                .MapGroup("api/v{version:apiVersion}/auth")
                .WithApiVersionSet(versionSet)
                .HasApiVersion(2.0)
                .MapToApiVersion(2.0)
                .WithOpenApi()
                .WithDisplayName("Auth");

            group.MapPost("login", Login.Handle); 
            group.MapGet("logout", [Authorize] async (string email) => "Hello World!!");

            return group;
        }
    }
}
