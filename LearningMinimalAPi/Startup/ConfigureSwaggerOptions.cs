using Asp.Versioning.ApiExplorer;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace LearningMinimalAPi.Startup
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var text = new StringBuilder("An example application with OpenAPI, Swashbuckle, and API versioning.");
            var info = new OpenApiInfo()
            {
                Title = "Example API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact() { Name = "Bill Mei", Email = "bill.mei@somewhere.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                text.Append(" This API version has been deprecated.");
            }

            if (description.SunsetPolicy is SunsetPolicy policy)
            {
                if (policy.Date is DateTimeOffset when)
                {
                    text.Append(" The API will be sunset on ")
                        .Append(when.Date.ToShortDateString())
                        .Append('.');
                }

                if (policy.HasLinks)
                {
                    text.AppendLine();

                    for (var i = 0; i < policy.Links.Count; i++)
                    {
                        var link = policy.Links[i];

                        if (link.Type == "text/html")
                        {
                            text.AppendLine();

                            if (link.Title.HasValue)
                            {
                                text.Append(link.Title.Value).Append(": ");
                            }

                            text.Append(link.LinkTarget.OriginalString);
                        }
                    }
                }
            }

            info.Description = text.ToString();

            return info;
        }
    }
}
