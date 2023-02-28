using MinimalApi.Controllers.v1;
using MinimalApi.Controllers.v2;
using MinimalApi.Startup;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var logger = builder.AddLogging();

var services = builder.Services;
services.AddApiAuthentication(builder.Configuration);
services.AddEndpointsApiExplorer();
services.AddVersioning();

services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", Authentication.GetSwaggerSecuritySchema());
    options.OperationFilter<SwaggerDefaultValues>();
});

var app = builder.Build();
var versionSet = app.InstanceVersionSet();

app.MapV1Endpoints(versionSet);
app.MapV2Endpoints(versionSet);

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var descriptions = app.DescribeApiVersions();

    foreach (var description in descriptions)
    {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = description.GroupName.ToUpperInvariant();
        options.SwaggerEndpoint(url, name);
    }
});
try
{
    logger.Information("Starting app...\n");
    app.Run();
    logger.Information("Started app\n");
}
catch (Exception e)
{
    logger.Error($"Application don't start by {e}");
}