using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SportStore.Microservice.Notification.Api.Settings.Swagger
{
    public class CustomHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {
                //var versionParameter = operation.Parameters.Single(p => p.Name == "version");
                //operation.Parameters.Remove(versionParameter);
            }
        }
    }
}