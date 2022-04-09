using GNB.IBM.Application.Interfaces;
using GNB.IBM.Application.Services;
using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Repositories;
using GNB.IBM.Core.Repositories.Base;
using GNB.IBM.Infrastructure.Repositories;
using GNB.IBM.Infrastructure.Repositories.Base;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureCustomServices(builder.Services, builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureCustomServices(IServiceCollection services, IConfiguration configuration)
{
    // Add Core Layer
    services.Configure<ExternalServicesSettings>(
        configuration.GetSection(ExternalServicesSettings.ExternalServices)
    );

    // Add Infrastructure Layer
    services.AddHttpClient();
    services.AddSingleton(typeof(GNB.IBM.Core.Interfaces.IHttpHandler<>), typeof(GNB.IBM.Infrastructure.Services.HttpHandler<>));
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<IConversionRateRepository, ConversionRateRepository>();

    // Add Application Layer
    services.AddScoped<IConversionRateService, ConversionRateService>();

    // Add WebAPI Layer
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("serilog.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}