using sales_system.Configurations;
using sales_system.Middlewares;
using SalesSystem.Infra.Configuration;
using SalesSytem.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapControllers();

app.Run();