using Asp.Versioning.Builder;

namespace MinimalApi.Controllers.v2
{
    public static class MapV2Controllers
    {
        public static void MapV2Endpoints(this WebApplication app, ApiVersionSet versionSet)
        {
            AuthController.MapAuthEndpoints(app, versionSet);
        }
    }
}
