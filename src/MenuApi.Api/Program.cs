using MenuApi.Api.OpenApi;
using MenuApi.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();
builder.Services.AddOpenApiConfiguration();


var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();


app.Run();