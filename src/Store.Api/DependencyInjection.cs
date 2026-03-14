using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Api;

public static class DependencyInjection
{
    /// <summary>
    /// Registra los componentes de presentación de la API (controllers, OpenAPI/Swagger, etc.).
    /// </summary>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();

        services.AddSwaggerGen(a =>
        {
            a.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
            {
                Version = "v1",
                Title = "Store API",
                Description = "API HTTP para la gestión de clientes y pedidos de la tienda.",
                Contact = new Microsoft.OpenApi.OpenApiContact
                {
                    Name = "Software Developer",
                    Email = "jorgelachapeller@hotmail.com"
                }
            });

            // Integra los comentarios XML en Swagger para que se muestren las descripciones en la UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            a.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            a.DocInclusionPredicate(
                (docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var attrOnClass =
                        methodInfo.DeclaringType?.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                    var attrOnMethod = methodInfo.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                    var groupName = attrOnMethod?.GroupName ?? attrOnClass?.GroupName;

                    if (docName == "v1")
                        return true;

                    if (string.IsNullOrEmpty(groupName))
                        return false;

                    return string.Equals(groupName, docName, StringComparison.OrdinalIgnoreCase);
                }
            );

            a.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Ingrese su token JWT (sin el prefijo 'Bearer').",
                }
            );

            a.AddSecurityRequirement(doc =>
            {
                var schemeRef = new OpenApiSecuritySchemeReference("Bearer", doc);
                return new OpenApiSecurityRequirement { { schemeRef, new List<string>() } };
            });


        });
        return services;
    }
}
