using System.Reflection;
using FurnitureStore.Application;
using FurnitureStore.Application.Common.Mappings;
using FurnitureStore.Application.Interfaces;
using FurnitureStore.Auth;
using FurnitureStore.Domain;
using FurnitureStore.Persistence;
using FurnitureStore.WebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

logger.Debug("Init Main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

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
        }
        catch (Exception)
        {

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