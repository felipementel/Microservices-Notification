using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SportStore.Microservice.Notification.Api.Settings.Swagger
{
    public static class SwaggerConfigs
    {
        public static void AddSwaggerConfigNotification(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sport Store - Notification - V1",
                    Version = "v1",
                    Description = "Notifications",
                    TermsOfService = new Uri("https://www.infnet.edu.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe Augusto",
                        Email = "felipementel@hotmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "BSD",
                        Url = new Uri("https://pt.wikipedia.org/wiki/Licen%C3%A7a_BSD"),
                    }
                });

                options.OperationFilter<CustomHeaderOperationFilter>();
                options.DocumentFilter<CustomSwaggerDocumentFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header | Bearer Schema. <br>
                      Informe: Bearer [espaço] o token obtido na controller de login <br>
                      Exemplo: Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            //AuthorizationUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
                            //TokenUrl = new Uri($"{adminApiConfiguration.IdentityServerBaseUrl}/connect/token"),
                            //Scopes = new Dictionary<string, string> {
                            //    { adminApiConfiguration.OidcApiName, adminApiConfiguration.ApiName }
                            // }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds(i => i.FullName);
            });
        }

        /// <summary>
        /// Configura o Swagger
        /// </summary>
        /// <param name="app">Aplicação</param>
        public static void UseConfigureSwaggerNotification(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.DisplayRequestDuration();
                options.DisplayOperationId();
                options.EnableDeepLinking();
                options.EnableValidator();
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                //Build a swagger endpoint for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}