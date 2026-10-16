using SalesSystem.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Application and Infra services

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();



app.MapGet("/health", () => Results.Ok("Healthy"));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
