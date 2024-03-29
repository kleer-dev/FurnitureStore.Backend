using System.Reflection;
using FurnitureStore.Application;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Application.Interfaces;
using FurnitureStore.Auth;
using FurnitureStore.Auth.Interfaces;
using FurnitureStore.Domain;
using FurnitureStore.Persistence;
using FurnitureStore.Persistence.DbContexts;
using FurnitureStore.Persistence.Initializers;
using FurnitureStore.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup()
    .LoadConfigurationFromFile("nlog.config", false)
    .GetCurrentClassLogger();
logger.Debug("Init main");
    
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers(options =>
        {
            options.CacheProfiles.Add("QueryCache",
                new CacheProfile
                {
                    Duration = 300,
                    Location = ResponseCacheLocation.Any,
                });
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IFurnitureStoreDbContext).Assembly));
    });

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddSecureAuth(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    }).AddEntityFrameworkStores<FurnitureStoreDbContext>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    });

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider
                .GetRequiredService<FurnitureStoreDbContext>();
            DbInitializer.Initialize(context);
            
            var logContext = serviceProvider
                .GetRequiredService<LogDbContext>();
            LogDbInitializer.Initialize(logContext);

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<User>>();
            var rolesManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole<long>>>();
            var jwtGenerator = scope.ServiceProvider
                .GetRequiredService<IJwtGenerator>();
            await RoleInitializer.InitializerAsync(userManager, rolesManager, jwtGenerator);
        }
        catch (Exception e)
        {
            logger.Error(e, "Stopped program because of exception");
            throw;
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseResponseCaching();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}