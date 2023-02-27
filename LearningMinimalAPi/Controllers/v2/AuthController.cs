using Asp.Versioning.Builder;
using LearningMinimalAPi.Models.Authentication;
using LearningMinimalAPi.UseCases.Authentication;
using Microsoft.AspNetCore.Authorization;

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

            group.MapPost("login", Login.UserLogin); 
            group.MapGet("logout", [Authorize] (string email) => "Hello World!!");

            return group;
        }
    }
}
