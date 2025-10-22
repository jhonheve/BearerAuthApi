using IdentityService.Application.Services;
using IdentityService.Domain.Repositories;
using IdentityService.Domain.Services;
using IdentityService.Infrastructure.Repositories;
using IdentityService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

/// With additional configuration sources, such as command line and environment variables
/// We could store sensitive configuration like connection strings or API keys outside of source code
builder.Configuration
    .AddCommandLine(args)
    .AddEnvironmentVariables("ASPNETCORE_");

// Register Identity Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsEnvironment("Local"))
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



//app.MapControllers();
app
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseHttpsRedirection()    
    .UseEndpoints(endpoints=>
    {
        endpoints
        .MapControllerRoute(
            name: "auth-api",
            pattern: "auth-api/{controller=Home}/{action=Get}/{id?}"
        );
    });



app.Run();
