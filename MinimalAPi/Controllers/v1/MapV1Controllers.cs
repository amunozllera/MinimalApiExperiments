using Asp.Versioning.Builder;

namespace MinimalApi.Controllers.v1
{
    public static class MapV1Controllers
    {
        public static void MapV1Endpoints(this WebApplication app, ApiVersionSet versionSet)
        {
            AuthController.MapAuthEndpoints(app, versionSet);
        }
    }
}
