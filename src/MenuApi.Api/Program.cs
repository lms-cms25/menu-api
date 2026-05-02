using MenuApi.Api.Abstractions;
using MenuApi.Api.Endpoints;
using MenuApi.Api.OpenApi;
using MenuApi.Api.Security;
using MenuApi.Api.Services;
using MenuApi.Application;
using MenuApi.Application.Abstractions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();
builder.Services.AddOpenApiConfiguration();

builder.Services.AddScoped<IMenuRepository, MockMenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

var app = builder.Build();

app.MapScalarApiReference();
app.UseCors("All");
app.MapOpenApi();
app.UseHttpsRedirection();
app.MapMenuEndpoints();

app.Run();