using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using UPay.Application.Boilerplate;
using UPay.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.ConfigureApplicationService();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// var configuration = new ConfigurationBuilder()
//     .SetBasePath(builder.Environment.ContentRootPath)
//     .AddJsonFile("appsettings.json")
//     .Build();
//
// var connectionString = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null ? 
//     configuration.GetConnectionString("DefaultConnection") :
//     configuration.GetConnectionString("DefaultConnectionLocal");

builder.Services.AddDbContext<UPayDbContext>(options =>
        options
            .UseInMemoryDatabase("UPayDb")
            // .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging() // todo: Only for development
            .EnableDetailedErrors() // todo: Only for development
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();