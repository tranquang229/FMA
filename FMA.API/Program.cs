using System.Security.Claims;
using FMA.API.Extensions;
using FMA.API.Middlewares;
using FMA.DAL.Context;
using FMA.Entities.Common.Settings;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
// jwt
// configure strongly typed settings object
services.AddCors();
services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));

// configure DI for application services
services.AddSingleton<DapperContext>();
services.AddDependencies();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//services.AddAuthorization(config =>
//{
//    config.AddPolicy("Manager", policyBuilder =>
//    {
//        policyBuilder.UserRequireCustomClaim(ClaimTypes.Email);
//        policyBuilder.UserRequireCustomClaim(ClaimTypes.DateOfBirth);
//    });
//});
//services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();

services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
