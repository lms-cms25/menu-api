using MenuApi.Api.Abstractions;
using MenuApi.Api.Endpoints;
using MenuApi.Api.OpenApi;
using MenuApi.Api.Security;
using MenuApi.Api.Services;
using MenuApi.Application;
using MenuApi.Application.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Convert.FromBase64String(jwtSettings["SigningKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Token misslyckades: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCorsConfiguration();
builder.Services.AddOpenApiConfiguration();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IMenuRepository, MockMenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapScalarApiReference();
app.UseCors("All");
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapMenuEndpoints();

app.Run();