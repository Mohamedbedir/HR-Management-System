using HR.Core;
using HR.Core.Middlewares;
using HR.Infrastructure;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.DataSeeding;
using HR.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection Sql Server
builder.Services.AddDbContext<HRAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add dependencies from other layers
builder.Services.AddInfrastructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies();

#region Localization
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> SupportedCulture = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        new CultureInfo("fr-FR"),
        new CultureInfo("ar-EG")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = SupportedCulture;
    options.SupportedUICultures = SupportedCulture;
});
#endregion

var app = builder.Build();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var dbcontext = service.GetRequiredService<HRAppDbContext>();
var loggerfactory = service.GetRequiredService<ILoggerFactory>();
try
{
    await dbcontext.Database.MigrateAsync();
    await DbSeeder.SeedDepartments(dbcontext);
    await DbSeeder.SeedPositions(dbcontext);
    await DbSeeder.SeedLeaveTypes(dbcontext);

}
catch(Exception ex)
{
    var logger = loggerfactory.CreateLogger<Program>();
    logger.LogError(ex, "An Error Occurred During Applying Migrations Database");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

#endregion
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
