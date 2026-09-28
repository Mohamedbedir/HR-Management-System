using HR.Core;
using HR.Core.Middlewares;
using HR.Data.AppMetaData;
using HR.Data.Entities;
using HR.Infrastructure;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.DataSeeding;
using HR.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
})  ;
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

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

//Add dependencies for identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<HRAppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };
    });

//builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
//{
//    options.TokenLifespan = TimeSpan.FromMinutes(2);
//});

#region RareLimit
builder.Services.AddRateLimiter(opt =>
{
    opt.AddSlidingWindowLimiter("Fixed",opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueLimit = 0;
        opt.SegmentsPerWindow = 1;
        opt.QueueLimit = 0;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    opt.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType= "text/plain";
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.",token);
    };
});

#endregion

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
var roleManager =service.GetRequiredService<RoleManager<IdentityRole<int>>>();
var loggerfactory = service.GetRequiredService<ILoggerFactory>();
try
{
    await dbcontext.Database.MigrateAsync();
    await DbSeeder.SeedDepartments(dbcontext);
    await DbSeeder.SeedPositions(dbcontext);
    await DbSeeder.SeedLeaveTypes(dbcontext);
    await DbSeeder.SeedRolesAsync(roleManager);


}
catch (Exception ex)
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
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
