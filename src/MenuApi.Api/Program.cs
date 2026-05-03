using MenuApi.Api.Endpoints;
using MenuApi.Api.OpenApi;
using MenuApi.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using MenuApi.Application;
using MenuApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Convert.FromBase64String(jwtSettings["SigningKey"]!);


builder.Services.AddApplication();
builder.Services.AddInfrastructure();

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


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapScalarApiReference();
app.UseCors("All");
app.MapOpenApi();
app.UseHttpsRedirection();


app.MapMenuEndpoints();

app.Run();