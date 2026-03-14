// Punto de entrada principal de la API HTTP.

using Store.Api;
using Store.Api.Extensions;
using Store.Application;
using Store.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
.AddInfrastructure(builder.Configuration)
.AddApplication();


var app = builder.Build();

// Expone OpenAPI solo en entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store API V1");
    c.RoutePrefix = "Swagger"; // Hace que Swagger UI esté disponible en la raíz (http://localhost:5000/Swagger)
});

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
